using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;

namespace WPF_Final_Project.Shapes
{
    [Serializable]
    public class AddPolygon : AddShapes
    {
        public ObservableCollection<Point> Points { get; set; } = new ObservableCollection<Point>();
        public double X { get; set; }
        public double Y { get; set; }
    }
}