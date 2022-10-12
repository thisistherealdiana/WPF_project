using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Numerics;
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
using ClassLibrary;

namespace WpfApp
{
    class CustomClass : IDataErrorInfo, INotifyPropertyChanged
    {
        public V4MainCollection v4mc { get; set; }
        double minValue;
        double maxValue;
        int numOfElements;
        string info;
        public string Info
        {
            get { return info; }
            set
            {
                info = value;
                OnPropertyChanged("Info");
            }
        }
        //public string Info { get; set; }

        public int NumOfElements
        {
            get { return numOfElements; }
            set
            {
                numOfElements = value;
                OnPropertyChanged("NumOfElements");
            }
        }
        public double MinValue
        {
            get { return minValue; }
            set
            {
                minValue = value;
                OnPropertyChanged("MaxValue");
            }
        }
        public double MaxValue
        {
            get { return maxValue; }
            set
            {
                maxValue = value;
                OnPropertyChanged("MinValue");
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string property_name)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(property_name));
        }
        public string Error
        {
            get
            {
                return "Error Message";
            }
        }
        public string this [string property]
        {
            get
            {
                 string msg = null;
                 switch (property)
                 {
                    case "MinValue":
                        break;
                    case "MaxValue":
                        if (MaxValue <= MinValue)
                            msg = "MinValue должен быть меньше MaxValue";
                        break;
                    case "Info":
                        if ((Info == null) || (Info.Length <= 0) || v4mc.Contains(Info))
                            msg = "В коллекции уже есть элемент с таким ID";
                        break;
                    case "NumOfElements":
                        if ((NumOfElements <= 1) || (NumOfElements >= 5))
                            msg = "Число элементов должно быть больше 1 и меньше 5";
                        break;
                    default:
                        break;
                 }
                 return msg;
            }
        }

        public CustomClass(V4MainCollection v4mc)
        {
            this.v4mc = v4mc;
        }

        public void AddCustom()
        {
            V4DataCollection item = new V4DataCollection(Info, 0.9d);
            item.InitRandom(NumOfElements, 0.1f, 1.9f, MinValue, MaxValue);
            v4mc.Add(item);
            OnPropertyChanged("Info");
        }
    }
}
