using System;
using Gtk;

public partial class MainWindow: Gtk.Window
{
	public MainWindow () : base (Gtk.WindowType.Toplevel)
	{
		Build ();
		SetupTreeView ();
		FillDevices ();
	}

	protected void OnDeleteEvent (object sender, DeleteEventArgs a)
	{
		Application.Quit ();
		a.RetVal = true;
	}

	private void SetupTreeView()
	{
		var titleCol = new TreeViewColumn () { Title = "Title" };
		var keyBindCol = new TreeViewColumn () { Title = "Keybinding" };
		treeview.AppendColumn (titleCol);
		treeview.AppendColumn (keyBindCol);

		var listStore = new Gtk.ListStore (typeof(string), typeof(string));
		treeview.Model = listStore;
	}
}
