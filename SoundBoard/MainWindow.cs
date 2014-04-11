using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using Gtk;
using NAudio.Wave;
using SharpDX.DirectInput;

public partial class MainWindow: Gtk.Window
{
    private WaveOut _selectedOutput;
    private WaveOut _primaryOutput;
    private Dictionary<SharpDX.DirectInput.Key, string> _keyToFileMap;
    private Gtk.ListStore _listData;
    private Keyboard _kb;
    private string _currentFilePlaying;
    private SharpDX.DirectInput.Key _currentlyPressed;
    private bool _playing;

	public MainWindow() : base(Gtk.WindowType.Toplevel)
	{
		Build();
		SetupTreeView();
		FillDevices();
        _playing = false;
        _selectedOutput = new WaveOut();
        _primaryOutput = new WaveOut();
        devComboBox.Changed += OnDevComboBoxChanged;
        var di = new DirectInput();
        _kb = new Keyboard(di);
        _kb.Acquire();
        _keyToFileMap = new Dictionary<SharpDX.DirectInput.Key,string>();
        (new Thread(() =>
        {   while(true)
            {
                foreach(var k in _keyToFileMap.Keys)
                {
                    if(_kb.GetCurrentState().IsPressed(k) && (_currentlyPressed != k))
                    {
                        var f = _keyToFileMap[k];
                        if(_playing && _currentFilePlaying.Equals(f))
                        {
                            Stop();
                        }
                        else
                        {
                            Stop();
                            Play(f);
                        }
                        _currentlyPressed = k;
                        break;
                    }
                }
                if(!_kb.GetCurrentState().IsPressed(_currentlyPressed))
                {
                    _currentlyPressed = SharpDX.DirectInput.Key.Yen;
                }
                Thread.Sleep(50);
            }
        })).Start();
	}

	protected void OnDeleteEvent(object sender, DeleteEventArgs a)
	{
		Application.Quit();
		a.RetVal = true;
	}

	private void SetupTreeView()
	{
		var titleCol = new TreeViewColumn() { Title = "Title" };
		var keyBindCol = new TreeViewColumn() { Title = "Keybinding" };
		treeview.AppendColumn(titleCol);
		treeview.AppendColumn(keyBindCol);

        var titleCell = new Gtk.CellRendererText();
        var keyBindCell = new Gtk.CellRendererText();

        titleCol.PackStart(titleCell, true);
        keyBindCol.PackStart(keyBindCell, true);

        titleCol.AddAttribute(titleCell, "text", 0);
        keyBindCol.AddAttribute(keyBindCell, "text", 1);
        _listData = new Gtk.ListStore(typeof(string), typeof(string));
        treeview.Model = _listData;
	}

	private void FillDevices()
	{
		for (int i = 0; i < WaveOut.DeviceCount; i++)
		{
			devComboBox.AppendText(WaveOut.GetCapabilities(i).ProductName);
		}
	}

	protected void OnDevComboBoxChanged(object sender, EventArgs e)
	{
        var box = sender as ComboBox;
        _selectedOutput.DeviceNumber = box.Active;
    }

    protected void OnExitClicked(object sender, EventArgs e)
    {
        Environment.Exit(0);
    }

    protected void OnFileOpenClicked(object sender, EventArgs e)
    {
        var fcd = new FileChooserDialog("Choose an mp3 to add", this, FileChooserAction.Open, "Open", ResponseType.Accept);
        if (fcd.Run() == (int)ResponseType.Accept)
        {
            _listData.AppendValues(fcd.Filename, "");
        }
        fcd.Destroy();
    }
 
    protected void OnRowActivated(object o, RowActivatedArgs args)
    {
        label2.Text = "Press a key to bind";
        SharpDX.DirectInput.Key pressed;
        for(;;)
        {
            var kbs = _kb.GetCurrentState();
            if (kbs.PressedKeys.Count == 1)
            {
                pressed = kbs.PressedKeys[0];
                _currentlyPressed = pressed;
                break;
            }
            else if (kbs.PressedKeys.Count > 1)
            {
                label2.Text = "Can only bind one key atm";
            }
            Thread.Sleep(50);
        }

        var ts = (o as TreeView).Selection;
        TreeModel model;
        TreeIter iter;
        ts.GetSelected(out model, out iter);
        var file = (string)model.GetValue(iter,0);
        lock (_keyToFileMap)
        {
            _keyToFileMap.Add(pressed, file);
        }
        model.SetValue(iter,1,pressed.ToString());
    }

    private void Play(string file)
    {
        var ws = new Mp3FileReader(file);
        var ws2 = new Mp3FileReader(file);
        _selectedOutput.Init(ws);
        _selectedOutput.Play();
        _primaryOutput.Init(ws2);
        _primaryOutput.Play();
        _currentFilePlaying = file;
        _playing = true;
    }

    private void Stop()
    {
        _playing = false;
        _primaryOutput.Stop();
        _selectedOutput.Stop();
    }


}