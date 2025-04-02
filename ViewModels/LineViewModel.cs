using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Windows;
using WPF_Final_Project.Views;

namespace WPF_Final_Project.ViewModels
{
    public class LineViewModel : ObservableObject
    {
        private double _x1;
        public double X1
        {
            get => _x1;
            set => SetProperty(ref _x1, value);
        }

        private double _y1;
        public double Y1
        {
            get => _y1;
            set => SetProperty(ref _y1, value);
        }

        private double _x2;
        public double X2
        {
            get => _x2;
            set => SetProperty(ref _x2, value);
        }

        private double _y2;
        public double Y2
        {
            get => _y2;
            set => SetProperty(ref _y2, value);
        }

        public RelayCommand AddOrEditLineCommand { get; }
        public Window LineWindow { get; set; }

        public Action<double, double, double, double> OnLineAddedOrEdited { get; set; }

        public LineViewModel()
        {
            AddOrEditLineCommand = new RelayCommand(AddOrEditLine);
        }

        private void AddOrEditLine()
        {
            if (X1 < 0 || Y1 < 0 || X2 < 0 || Y2 < 0)
            {
                MessageBox.Show("Please enter valid values.");
                return;
            }

            OnLineAddedOrEdited?.Invoke(X1, Y1, X2, Y2);

            LineWindow?.Close();
        }
    }
}