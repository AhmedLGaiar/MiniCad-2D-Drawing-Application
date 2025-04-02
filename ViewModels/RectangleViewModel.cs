using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Windows;

namespace WPF_Final_Project.ViewModels
{
    public class RectangleViewModel : ObservableObject
    {
        private double _width;
        public double Width
        {
            get => _width;
            set => SetProperty(ref _width, value);
        }

        private double _height;
        public double Height
        {
            get => _height;
            set => SetProperty(ref _height, value);
        }

        private double _xPosition;
        public double XPosition
        {
            get => _xPosition;
            set => SetProperty(ref _xPosition, value);
        }

        private double _yPosition;
        public double YPosition
        {
            get => _yPosition;
            set => SetProperty(ref _yPosition, value);
        }

        public RelayCommand AddOrEditRectangleCommand { get; }
        public Window RectangleWindow { get; set; }
        public Action<double, double, double, double> OnRectangleAddedOrEdited { get; set; }

        public RectangleViewModel()
        {
            AddOrEditRectangleCommand = new RelayCommand(AddOrEditRectangle);
        }

        private void AddOrEditRectangle()
        {
            if (Width <= 0 || Height <= 0 || XPosition < 0 || YPosition < 0)
            {
                MessageBox.Show("Please enter valid values.");
                return;
            }

            OnRectangleAddedOrEdited?.Invoke(Width, Height, XPosition, YPosition);

            RectangleWindow?.Close();
        }
    }
}