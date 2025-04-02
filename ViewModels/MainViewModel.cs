using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.Text.Json;
using System.Windows;
using System.Windows.Input;
using WPF_Final_Project.Accounts;
using WPF_Final_Project.Converters;
using WPF_Final_Project.DataBase;
using WPF_Final_Project.Shapes;
using WPF_Final_Project.Views;

namespace WPF_Final_Project.ViewModels
{
    public partial class MainViewModel : ObservableObject
    {
        public ObservableCollection<AddShapes> Shapes { get; set; } = new ObservableCollection<AddShapes>();

        private int _currentUserId;
        public int CurrentUserId
        {
            get => _currentUserId;
            set => SetProperty(ref _currentUserId, value);
        }

        private string _selectedShapeType;
        public string SelectedShapeType
        {
            get => _selectedShapeType;
            set => SetProperty(ref _selectedShapeType, value);
        }

        private AddShapes _selectedShape;
        public AddShapes SelectedShape
        {
            get => _selectedShape;
            set
            {
                SetProperty(ref _selectedShape, value);
                if (EditShapeCommand is RelayCommand editCommand)
                {
                    editCommand.NotifyCanExecuteChanged();
                }
                if (DeleteShapeCommand is RelayCommand deleteCommand)
                {
                    deleteCommand.NotifyCanExecuteChanged();
                }
            }
        }

        public ICommand OpenNewWindowCommand { get; }
        public ICommand SaveCommand { get; }
        public ICommand OpenCommand { get; }
        public ICommand EditShapeCommand { get; }
        public ICommand DeleteShapeCommand { get; }
        public ICommand ExitCommand { get; }
        public ICommand LogOutCommand { get; }

        public MainViewModel()
        {
            OpenNewWindowCommand = new RelayCommand(OpenNewWindow);
            SaveCommand = new RelayCommand(SaveShapes);
            OpenCommand = new RelayCommand(OpenShapes);
            EditShapeCommand = new RelayCommand(EditShape, CanEditShape);
            DeleteShapeCommand = new RelayCommand(DeleteShape, CanDeleteShape);
            ExitCommand = new RelayCommand(ExitApplication);
            LogOutCommand = new RelayCommand(LogOut);
        }

        private void OpenNewWindow()
        {
            if (!string.IsNullOrEmpty(SelectedShapeType))
            {
                switch (SelectedShapeType)
                {
                    case "Line":
                        var lineWindow = new LineWindow();
                        var lineViewModel = new LineViewModel()
                        {
                            LineWindow = lineWindow 
                        };
                        lineViewModel.OnLineAddedOrEdited = (x1, y1, x2, y2) =>
                        {
                            Shapes.Add(new AddLine
                            {
                                X1 = x1,
                                Y1 = y1,
                                X2 = x2,
                                Y2 = y2
                            });
                        };
                        lineWindow.DataContext = lineViewModel;
                        lineWindow.Show();
                        break;

                    case "Rectangle":
                        var rectangleWindow = new RectangleWindow();
                        var rectangleViewModel = new RectangleViewModel()
                        {
                            RectangleWindow = rectangleWindow 
                        };
                        rectangleViewModel.OnRectangleAddedOrEdited = (width, height, x, y) =>
                        {
                            Shapes.Add(new AddRectangle
                            {
                                Width = width,
                                Height = height,
                                X = x,
                                Y = y
                            });
                        };
                        rectangleWindow.DataContext = rectangleViewModel;
                        rectangleWindow.Show();
                        break;

                    case "Circle":
                        var circleWindow = new CircleWindow();
                        var circleViewModel = new CircleViewModel()
                        {
                            CircleWindow = circleWindow 
                        };
                        circleViewModel.OnCircleAddedOrEdited = (radius, x, y) =>
                        {
                            Shapes.Add(new AddCircle
                            {
                                Radius = radius,
                                X = x,
                                Y = y
                            });
                        };
                        circleWindow.DataContext = circleViewModel;
                        circleWindow.Show();
                        break;

                    case "Polygon":
                        var polygonWindow = new PolygonWindow();
                        var polygonViewModel = new PolygonViewModel()
                        {
                            PolygonWindow = polygonWindow 
                        };
                        polygonViewModel.OnPolygonAddedOrEdited = (points, x, y) =>
                        {
                            Shapes.Add(new AddPolygon
                            {
                                Points = new ObservableCollection<Point>(points),
                                X = x,
                                Y = y
                            });
                        };
                        polygonWindow.DataContext = polygonViewModel;
                        polygonWindow.Show();
                        break;

                    default:
                        MessageBox.Show("Please select a valid shape.");
                        break;
                }
            }
        }

        private void EditShape()
        {
            if (SelectedShape == null)
            {
                MessageBox.Show("Please select a shape to edit.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            switch (SelectedShape)
            {
                case AddLine line:
                    var lineWindow = new LineWindow();
                    var lineViewModel = new LineViewModel
                    {
                        X1 = line.X1,
                        Y1 = line.Y1,
                        X2 = line.X2,
                        Y2 = line.Y2,
                        LineWindow = lineWindow 
                    };
                    lineViewModel.OnLineAddedOrEdited = (x1, y1, x2, y2) =>
                    {
                        line.X1 = x1;
                        line.Y1 = y1;
                        line.X2 = x2;
                        line.Y2 = y2;

                        RefreshShapesCollection();

                        OnPropertyChanged(nameof(Shapes));
                    };
                    lineWindow.DataContext = lineViewModel;
                    lineWindow.Show();
                    break;

                case AddRectangle rectangle:
                    var rectangleWindow = new RectangleWindow();
                    var rectangleViewModel = new RectangleViewModel
                    {
                        Width = rectangle.Width,
                        Height = rectangle.Height,
                        XPosition = rectangle.X,
                        YPosition = rectangle.Y,
                        RectangleWindow = rectangleWindow
                    };
                    rectangleViewModel.OnRectangleAddedOrEdited = (width, height, x, y) =>
                    {
                        rectangle.Width = width;
                        rectangle.Height = height;
                        rectangle.X = x;
                        rectangle.Y = y;

                        RefreshShapesCollection();

                        OnPropertyChanged(nameof(Shapes));
                    };
                    rectangleWindow.DataContext = rectangleViewModel;
                    rectangleWindow.Show();
                    break;

                case AddCircle circle:
                    var circleWindow = new CircleWindow();
                    var circleViewModel = new CircleViewModel
                    {
                        Radius = circle.Radius,
                        XPosition = circle.X,
                        YPosition = circle.Y,
                        CircleWindow = circleWindow 
                    };
                    circleViewModel.OnCircleAddedOrEdited = (radius, x, y) =>
                    {
                        circle.Radius = radius;
                        circle.X = x;
                        circle.Y = y;

                        RefreshShapesCollection();

                        OnPropertyChanged(nameof(Shapes));
                    };
                    circleWindow.DataContext = circleViewModel;
                    circleWindow.Show();
                    break;

                case AddPolygon polygon:
                    var polygonWindow = new PolygonWindow();
                    var polygonViewModel = new PolygonViewModel
                    {
                        Points = new ObservableCollection<Point>(polygon.Points),
                        XPosition = polygon.X,
                        YPosition = polygon.Y,
                        PolygonWindow = polygonWindow 
                    };
                    polygonViewModel.OnPolygonAddedOrEdited = (points, x, y) =>
                    {
                        polygon.Points = new ObservableCollection<Point>(points);
                        polygon.X = x;
                        polygon.Y = y;

                        RefreshShapesCollection();

                        OnPropertyChanged(nameof(Shapes));
                    };
                    polygonWindow.DataContext = polygonViewModel;
                    polygonWindow.Show();
                    break;

                default:
                    MessageBox.Show("Unsupported shape type.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    break;
            }
        }

        private void RefreshShapesCollection()
        {
            var shapes = new List<AddShapes>(Shapes);
            Shapes.Clear();
            foreach (var shape in shapes)
            {
                Shapes.Add(shape);
            }
        }

        private bool CanEditShape()
        {
            return SelectedShape != null;
        }

        private void SaveShapes()
        {
            if (CurrentUserId == 0)
            {
                MessageBox.Show("No user is logged in.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            string projectName = Microsoft.VisualBasic.Interaction.InputBox("Enter a name for your project:", "Save Project", "MyProject");

            if (string.IsNullOrWhiteSpace(projectName))
            {
                MessageBox.Show("Project name cannot be empty.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            try
            {
                var options = new JsonSerializerOptions
                {
                    WriteIndented = true,
                    Converters = { new ShapeConverter() }
                };
                string projectData = JsonSerializer.Serialize(Shapes, options);

                using (var context = new DrawingContext())
                {
                    var project = new Project
                    {
                        ProjectName = projectName,
                        ProjectData = projectData,
                        UserID = CurrentUserId 
                    };
                    context.Projects.Add(project);
                    context.SaveChanges();
                }

                MessageBox.Show("Project saved successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to save project: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void OpenShapes()
        {
            var recentFilesWindow = new RecentFilesWindow();
            recentFilesWindow.Owner = Application.Current.MainWindow;
            recentFilesWindow.ShowDialog(); 
        }

        private void DeleteShape()
        {
            if (SelectedShape == null)
            {
                MessageBox.Show("Please select a shape to delete.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            Shapes.Remove(SelectedShape);

            SelectedShape = null;

            OnPropertyChanged(nameof(Shapes));
        }

        private bool CanDeleteShape()
        {
            return SelectedShape != null;
        }

        public void OpenProject(Project project)
        {
            try
            {
                var options = new JsonSerializerOptions
                {
                    Converters = { new ShapeConverter() }
                };
                var shapes = JsonSerializer.Deserialize<ObservableCollection<AddShapes>>(project.ProjectData, options);

                Shapes.Clear();

                foreach (var shape in shapes)
                {
                    Shapes.Add(shape);
                }

                OnPropertyChanged(nameof(Shapes));
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to load project: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ExitApplication()
        {
            Application.Current.Shutdown();
        }

        private void LogOut()
        {
            Application.Current.Windows.OfType<MainWindow>().FirstOrDefault()?.Close();

            var loginWindow = new LoginPage();
            Application.Current.MainWindow = loginWindow;
            loginWindow.Show();
        }
    }
}