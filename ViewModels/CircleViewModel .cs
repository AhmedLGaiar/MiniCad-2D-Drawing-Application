using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Windows;
using WPF_Final_Project.Views;

namespace WPF_Final_Project.ViewModels
{
    public class CircleViewModel : ObservableObject
    {
        private double _radius;
        public double Radius
        {
            get => _radius;
            set => SetProperty(ref _radius, value);
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

        public RelayCommand AddOrEditCircleCommand { get; }
        public Window CircleWindow { get; set; }

        public Action<double, double, double> OnCircleAddedOrEdited { get; set; }

        public CircleViewModel()
        {
            AddOrEditCircleCommand = new RelayCommand(AddOrEditCircle);
        }

        private void AddOrEditCircle()
        {
            if (Radius <= 0 || XPosition < 0 || YPosition < 0)
            {
                MessageBox.Show("Please enter valid values.");
                return;
            }

            OnCircleAddedOrEdited?.Invoke(Radius, XPosition, YPosition);

            CircleWindow?.Close();
        }
    }
}