using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace WPF_Final_Project.Converters
{
    public class PointsToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is IEnumerable<Point> points)
            {
                return string.Join(" ", points.Select(p => $"{p.X},{p.Y}"));
            }
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}