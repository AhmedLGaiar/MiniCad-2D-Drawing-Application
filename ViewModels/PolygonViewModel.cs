using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.Windows;
using WPF_Final_Project.Views;

namespace WPF_Final_Project.ViewModels
{
    public partial class PolygonViewModel : ObservableObject
    {
        [ObservableProperty]
        private double _xPosition;

        [ObservableProperty]
        private double _yPosition; 

        [ObservableProperty]
        private ObservableCollection<Point> _points = new ObservableCollection<Point>(); 

        public Window PolygonWindow { get; set; }

        // Commands
        public RelayCommand AddPointCommand { get; }
        public RelayCommand AddOrEditPolygonCommand { get; }

        public PolygonViewModel()
        {
            AddPointCommand = new RelayCommand(AddPoint);
            AddOrEditPolygonCommand = new RelayCommand(AddOrEditPolygon);
        }

        private void AddPoint()
        {
            Points.Add(new Point(XPosition, YPosition));

            XPosition = 0;
            YPosition = 0;
        }

        private void AddOrEditPolygon()
        {
            if (Points.Count < 3)
            {
                MessageBox.Show("A polygon must have at least 3 points.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            OnPolygonAddedOrEdited?.Invoke(Points, XPosition, YPosition);

            PolygonWindow?.Close();
        }

        public Action<ObservableCollection<Point>, double, double> OnPolygonAddedOrEdited { get; set; }
    }
}