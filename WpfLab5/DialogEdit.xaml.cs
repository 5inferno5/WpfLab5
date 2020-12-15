using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;


namespace WpfLab5
{
    /// <summary>
    /// Interaction logic for DialogEdit.xaml
    /// </summary>
    public partial class DialogEdit : Window
    {
        public DialogEdit()
        {
            InitializeComponent();
            CommandBinding OK_EditDialog = new CommandBinding(MyCommands.OK_EditDialog);
            OK_EditDialog.Executed += OK_EditDialog_Executed;
            CommandBinding Cancel_EditDialog = new CommandBinding(MyCommands.Cancel_EditDialog);
            Cancel_EditDialog.Executed += Cancel_EditDialog_Executed;

            this.CommandBindings.AddRange(new[] { OK_EditDialog, Cancel_EditDialog });

            List<string> colors = new List<string> { "blue", "pink", "green", "black", "yellow", "red" };
            foreach (string item in colors)
            {
                cmbLineColor.Items.Add(item);
                cmbFill.Items.Add(item);
            }

            cmbFill.SelectedIndex = 1;
            cmbLineColor.SelectedIndex = 0;
        }

        void Cancel_EditDialog_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            this.DialogResult = false;
        }

        void OK_EditDialog_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            this.DialogResult = true;

        }
    }
}
