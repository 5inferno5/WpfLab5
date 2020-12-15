using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml.Serialization;

namespace WpfLab5
{
    /// <summary>
    /// Interaction logic for UserControl1.xaml
    /// </summary>
    public partial class UserControl1 : UserControl
    {
        public UserControl1()
        {
            InitializeComponent();
        }
        public int WCross
        {
            get { return (int)GetValue(WCrossProperty); }
            set { SetValue(WCrossProperty, value); }
        }

        public static readonly DependencyProperty WCrossProperty =
            DependencyProperty.Register("WCross", typeof(int), typeof(UserControl1), new PropertyMetadata(3));

        [XmlAttribute]
        public SolidColorBrush WBrush
        {
            get { return (SolidColorBrush)GetValue(WBrushProperty); }
            set { SetValue(WBrushProperty, value); }
        }
        public static readonly DependencyProperty WBrushProperty =
            DependencyProperty.Register("WBrush", typeof(SolidColorBrush), typeof(UserControl1), new PropertyMetadata(new SolidColorBrush(Colors.Black)));

        [XmlAttribute]
        public SolidColorBrush WBack
        {
            get { return (SolidColorBrush)GetValue(WBackProperty); }
            set { SetValue(WBackProperty, value); }
        }
        public static readonly DependencyProperty WBackProperty =
            DependencyProperty.Register("WBack", typeof(SolidColorBrush), typeof(UserControl1), new PropertyMetadata(new SolidColorBrush(Colors.Yellow)));

    }
}
