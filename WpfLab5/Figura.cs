using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace WpfLab5
{
    [Serializable]
    public class Figura
    {                
      
            [XmlAttribute]
            public double X { get; set; }

            [XmlAttribute]
            public double Y { get; set; }

            [XmlAttribute]
            public int WCross { get; set; }
            [XmlAttribute]
            public String WBrush { get; set; }
            [XmlAttribute]
            public String WBack { get; set; }

            public Figura() { }

            public Figura(double x, double y, int wCross, String wBrush, String wBack)
            {
                X = x;
                Y = y;
                WCross = wCross;
                WBrush = wBrush;
                WBack = wBack;
            }
        }
    }

