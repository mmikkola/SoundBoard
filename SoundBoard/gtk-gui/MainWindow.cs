
// This file has been generated by the GUI designer. Do not modify.

public partial class MainWindow
{
	private global::Gtk.UIManager UIManager;
	private global::Gtk.Action FileAction;
	private global::Gtk.VBox vbox1;
	private global::Gtk.MenuBar menubar2;
	private global::Gtk.HBox hbox1;
	private global::Gtk.Label label1;
	private global::Gtk.ComboBox combobox2;
	private global::Gtk.ScrolledWindow GtkScrolledWindow;
	private global::Gtk.TreeView treeview;

	protected virtual void Build ()
	{
		global::Stetic.Gui.Initialize (this);
		// Widget MainWindow
		this.UIManager = new global::Gtk.UIManager ();
		global::Gtk.ActionGroup w1 = new global::Gtk.ActionGroup ("Default");
		this.FileAction = new global::Gtk.Action ("FileAction", global::Mono.Unix.Catalog.GetString ("File"), null, null);
		this.FileAction.ShortLabel = global::Mono.Unix.Catalog.GetString ("File");
		w1.Add (this.FileAction, null);
		this.UIManager.InsertActionGroup (w1, 0);
		this.AddAccelGroup (this.UIManager.AccelGroup);
		this.Name = "MainWindow";
		this.Title = global::Mono.Unix.Catalog.GetString ("SoundBoard");
		this.WindowPosition = ((global::Gtk.WindowPosition)(4));
		// Container child MainWindow.Gtk.Container+ContainerChild
		this.vbox1 = new global::Gtk.VBox ();
		this.vbox1.Name = "vbox1";
		this.vbox1.Spacing = 6;
		// Container child vbox1.Gtk.Box+BoxChild
		this.UIManager.AddUiFromString ("<ui><menubar name=\'menubar2\'><menu name=\'FileAction\' action=\'FileAction\'/></menub" +
		"ar></ui>");
		this.menubar2 = ((global::Gtk.MenuBar)(this.UIManager.GetWidget ("/menubar2")));
		this.menubar2.Name = "menubar2";
		this.vbox1.Add (this.menubar2);
		global::Gtk.Box.BoxChild w2 = ((global::Gtk.Box.BoxChild)(this.vbox1 [this.menubar2]));
		w2.Position = 0;
		w2.Expand = false;
		w2.Fill = false;
		// Container child vbox1.Gtk.Box+BoxChild
		this.hbox1 = new global::Gtk.HBox ();
		this.hbox1.Name = "hbox1";
		this.hbox1.Spacing = 6;
		// Container child hbox1.Gtk.Box+BoxChild
		this.label1 = new global::Gtk.Label ();
		this.label1.Name = "label1";
		this.label1.LabelProp = global::Mono.Unix.Catalog.GetString ("label1");
		this.hbox1.Add (this.label1);
		global::Gtk.Box.BoxChild w3 = ((global::Gtk.Box.BoxChild)(this.hbox1 [this.label1]));
		w3.Position = 0;
		w3.Expand = false;
		w3.Fill = false;
		// Container child hbox1.Gtk.Box+BoxChild
		this.combobox2 = global::Gtk.ComboBox.NewText ();
		this.combobox2.Name = "combobox2";
		this.hbox1.Add (this.combobox2);
		global::Gtk.Box.BoxChild w4 = ((global::Gtk.Box.BoxChild)(this.hbox1 [this.combobox2]));
		w4.Position = 1;
		w4.Expand = false;
		w4.Fill = false;
		this.vbox1.Add (this.hbox1);
		global::Gtk.Box.BoxChild w5 = ((global::Gtk.Box.BoxChild)(this.vbox1 [this.hbox1]));
		w5.Position = 1;
		w5.Expand = false;
		w5.Fill = false;
		// Container child vbox1.Gtk.Box+BoxChild
		this.GtkScrolledWindow = new global::Gtk.ScrolledWindow ();
		this.GtkScrolledWindow.Name = "GtkScrolledWindow";
		this.GtkScrolledWindow.ShadowType = ((global::Gtk.ShadowType)(1));
		// Container child GtkScrolledWindow.Gtk.Container+ContainerChild
		this.treeview = new global::Gtk.TreeView ();
		this.treeview.CanFocus = true;
		this.treeview.Name = "treeview";
		this.GtkScrolledWindow.Add (this.treeview);
		this.vbox1.Add (this.GtkScrolledWindow);
		global::Gtk.Box.BoxChild w7 = ((global::Gtk.Box.BoxChild)(this.vbox1 [this.GtkScrolledWindow]));
		w7.Position = 2;
		this.Add (this.vbox1);
		if ((this.Child != null)) {
			this.Child.ShowAll ();
		}
		this.DefaultWidth = 784;
		this.DefaultHeight = 537;
		this.Show ();
		this.DeleteEvent += new global::Gtk.DeleteEventHandler (this.OnDeleteEvent);
	}
}