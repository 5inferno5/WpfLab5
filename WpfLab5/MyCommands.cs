using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace WpfLab5
{
    public static class MyCommands
    {
        public static RoutedUICommand Edit;
        public static RoutedUICommand About;
        public static RoutedUICommand OK_EditDialog;
        public static RoutedUICommand Cancel_EditDialog;
        static MyCommands()
        {
            InputGestureCollection igc_edit = new InputGestureCollection();
            igc_edit.Add(new KeyGesture(Key.E, ModifierKeys.Control, "Ctrl+E"));
            Edit = new RoutedUICommand("Edit", "Edit", typeof(MyCommands), igc_edit);

            InputGestureCollection igc_about = new InputGestureCollection();
            igc_about.Add(new KeyGesture(Key.I, ModifierKeys.Control, "Ctrl+I"));
            About = new RoutedUICommand("About", "About", typeof(MyCommands), igc_about);

            OK_EditDialog = new RoutedUICommand("OK_EditDialog", "OK_EditDialog", typeof(MyCommands));
            Cancel_EditDialog = new RoutedUICommand("Cancel_EditDialog", "Cancel_EditDialog", typeof(MyCommands));
        }
    }
}
