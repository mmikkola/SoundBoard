using System;
using System.Collections.Generic;
using System.IO;
using Gtk;
using NAudio.Wave;
using SharpDX.DirectInput;

public partial class MainWindow: Gtk.Window
{
    private WaveOut _selectedOutput;
    private WaveOut _primaryOutput;
    private Dictionary<int, string> _keyToFileMap;
    private Gtk.ListStore _listData;

	public MainWindow() : base(Gtk.WindowType.Toplevel)
	{
		Build();
		SetupTreeView();
		FillDevices();
        _selectedOutput = new WaveOut();
        _primaryOutput = new WaveOut();
        devComboBox.Changed += OnDevComboBoxChanged;
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
        var ts = (o as TreeView).Selection;
        TreeModel model;
        TreeIter iter;
        ts.GetSelected(out model, out iter);
        var file = (string)model.GetValue(iter,0);

    }

    private void Play(string file)
    {
        var ws = new Mp3FileReader(file);
        var ws2 = new Mp3FileReader(file);
        _selectedOutput.Init(ws);
        _selectedOutput.Play();
        _primaryOutput.Init(ws2);
        _primaryOutput.Play();
    }

    private void Stop()
    {
        _primaryOutput.Stop();
        _selectedOutput.Stop();
    }
}