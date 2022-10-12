using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;
using System.Windows.Data;
using ClassLibrary;

namespace Converters
{
    [ValueConversion(typeof(V4DataOnGrid), typeof(string))]
    public class V4dogConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value != null)
            {
                V4DataOnGrid items = (V4DataOnGrid)value;
                if (items.array.Length != 0)
                {
                    double max = items.array[0, 0].Magnitude;
                    double min = items.array[0, 0].Magnitude;
                    foreach (var item in items)
                    {
                        if (item.compl.Magnitude > max) max = item.compl.Magnitude;
                        if (item.compl.Magnitude < min) min = item.compl.Magnitude;

                    }
                    return "Max abs: " + max + "\nMin abs: " + min;
                }
            }
            return "";
        }
        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return value;
        }
    }

    [ValueConversion(typeof(DataItem), typeof(string))]
    public class DataItemConverter_1 : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value != null)
            {
                DataItem item = (DataItem)value;
                return "Coordinates: " + item.vect;
            }
            return "";
        }
        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return value;
        }
    }

    [ValueConversion(typeof(DataItem), typeof(string))]
    public class DataItemConverter_2 : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value != null)
            {
                DataItem item = (DataItem)value;
                return "Value: " + item.compl + "\nAbs. value: " + item.compl.Magnitude + "\n";
            }
            return "";
        }
        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return value;
        }
    }

    [ValueConversion(typeof(Complex), typeof(string))]
    public class MaxAbsConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value != null)
            {
                Complex item = (Complex)value;
                if (item == default) return "";
                return "Complex value with max. absolute value\nin V4MainCollection:\n" + item.ToString();
            }
            return "";
        }
        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return value;
        }
    }
}
