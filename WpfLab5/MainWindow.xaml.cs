using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Win32;
using System.IO;
using System.Xml.Serialization;
using System.Collections.Generic;


namespace WpfLab5
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            this.MouseMove += MainWindow_MouseMove;
            this.MouseDown += MainWindow_MouseDown;

            CommandBinding bindingNew = new CommandBinding(ApplicationCommands.New);
            bindingNew.Executed += bindingNew_Executed;

            CommandBinding bindingOpen = new CommandBinding(ApplicationCommands.Open);
            bindingOpen.Executed += bindingOpen_Executed;

            CommandBinding bindingSave = new CommandBinding(ApplicationCommands.Save);
            bindingSave.Executed += bindingSave_Executed;
            bindingSave.CanExecute += bindingSave_CanExecute;

            CommandBinding bindingEdit = new CommandBinding(MyCommands.Edit);
            bindingEdit.Executed += bindingEdit_Executed;

            CommandBinding bindingClose = new CommandBinding(ApplicationCommands.Close);
            bindingClose.Executed += bindingClose_Executed;

            CommandBinding bindingAbout = new CommandBinding(MyCommands.About);
            bindingAbout.Executed += bindingAbout_Executed;


            this.CommandBindings.AddRange(new[] { bindingNew, bindingOpen, bindingSave, bindingEdit, bindingClose, bindingAbout });
        }


        private void bindingAbout_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            statusBar.Content = "Радевич Инна, Л.р. №5, группа 90321";
        }

        private void bindingClose_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }


        string lastThickness = "2";
        string lastLineColor = "blue";
        string lastFill = "red";

        private void bindingEdit_Executed(object sender, ExecutedRoutedEventArgs e)
        {

            DialogEdit de = new DialogEdit();
            de.txtThickness.Text = lastThickness;
            de.cmbLineColor.SelectedValue = lastLineColor;
            de.cmbFill.SelectedValue = lastFill;
            var result = de.ShowDialog();
            if (result == true)
            {
                foreach (var item in canvas.Children)
                {
                    if (item is UserControl1)
                        ((UserControl1)item).WCross = Int32.Parse(de.txtThickness.Text);
                    de.txtThickness.Text = de.txtThickness.Text;
                    Color color = (Color)ColorConverter.ConvertFromString(de.cmbLineColor.SelectedValue.ToString());
                    ((UserControl1)item).WBrush = new SolidColorBrush(color);
                    color = (Color)ColorConverter.ConvertFromString(de.cmbFill.SelectedValue.ToString());
                    ((UserControl1)item).WBack = new SolidColorBrush(color);

                }
                lastThickness = de.txtThickness.Text;
                lastLineColor = de.cmbLineColor.SelectedValue.ToString();
                lastFill = de.cmbFill.SelectedValue.ToString();
            }

        }

        private void bindingSave_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = canvas.Children.Count != 0;
        }

        private void bindingSave_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.FileName = "Round";
            sfd.DefaultExt = ".xml";
            sfd.Filter = "Xml|*.xml| Файлы(dat)|*.dat|Все файлы|*.*";
            var result = sfd.ShowDialog();

            if (result == true)
            {
                XmlSerializer xs = new XmlSerializer(typeof(List<Figura>));
                List<Figura> figuraList = new List<Figura>();

                foreach (var item in canvas.Children)
                {
                    if (item is UserControl1)
                    {
                        double x = Canvas.GetLeft((UserControl1)item);
                        double y = Canvas.GetTop((UserControl1)item);
                        int wCross = ((UserControl1)item).WCross;
                        String wBrush = ((UserControl1)item).WBrush.ToString();
                        String wBack = ((UserControl1)item).WBack.ToString();

                        figuraList.Add(new Figura(x, y, wCross, wBrush, wBack));
                    }
                }
                using (FileStream fs = new FileStream(sfd.FileName, FileMode.CreateNew))
                {
                    xs.Serialize(fs, figuraList);
                }

            }
        }


        private void bindingOpen_Executed(object sender, ExecutedRoutedEventArgs e)
        {

            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Xml|*.xml|Файлы(dat)|*.dat|Все файлы|*.*";
            var result = ofd.ShowDialog();

            if (result == true)
            {
                StreamReader sr = new StreamReader(ofd.FileName);
                XmlSerializer xs = new XmlSerializer(typeof(List<Figura>));
                canvas.Children.Clear();

                List<Figura> figuraList = new List<Figura>();

                using (FileStream fs = new FileStream(ofd.FileName, FileMode.Open, FileAccess.Read))
                {
                    figuraList = (List<Figura>)xs.Deserialize(fs);
                }

                foreach (var item in figuraList)
                {
                    UserControl1 figNew = new UserControl1();
                    double x = item.X;
                    double y = item.Y;
                    figNew.WCross = item.WCross;
                    Color color = (Color)ColorConverter.ConvertFromString(item.WBrush);
                    figNew.WBrush = new SolidColorBrush(color);
                    color = (Color)ColorConverter.ConvertFromString(item.WBack);
                    figNew.WBack = new SolidColorBrush(color);
                    Canvas.SetLeft(figNew, x);
                    Canvas.SetTop(figNew, y);
                    canvas.Children.Add(figNew);
                }
            }
        }

        private void bindingNew_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            canvas.Children.Clear();
        }

        private void MainWindow_MouseMove(object sender, MouseEventArgs e)
        {
            double x = e.GetPosition(canvas).X;
            double y = e.GetPosition(canvas).Y;

            if (x >= 0 && y >= 0)
                statusBar.Content = string.Format("x={0:0} y={1:0}", x, y);
            else
                statusBar.Content = "";
        }

        private void MainWindow_MouseDown(object sender, MouseButtonEventArgs e)
        {
            double x = e.GetPosition(canvas).X;
            double y = e.GetPosition(canvas).Y;
            UserControl1 round = new UserControl1();
            Canvas.SetLeft(round, x);
            Canvas.SetTop(round, y);
            canvas.Children.Add(round);
        }
    }
}
