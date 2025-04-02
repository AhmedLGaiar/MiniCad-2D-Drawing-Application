using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using WPF_Final_Project.DataBase;

namespace WPF_Final_Project.ViewModels
{
    public partial class RecentFilesViewModel : ObservableObject
    {
        [ObservableProperty]
        private ObservableCollection<Project> _recentProjects = new ObservableCollection<Project>();

        [ObservableProperty]
        private Project _selectedProject;

        public RecentFilesViewModel()
        {
            LoadRecentProjects(); 
        }

        private void LoadRecentProjects()
        {
            using (var context = new DrawingContext())
            {
                var mainViewModel = Application.Current.Windows.OfType<MainWindow>().FirstOrDefault()?.DataContext as MainViewModel;
                if (mainViewModel != null)
                {
                    var projects = context.Projects
                        .Where(p => p.UserID == mainViewModel.CurrentUserId)
                        .OrderByDescending(p => p.CreatedAt) 
                        .Take(10)
                        .ToList();

                    RecentProjects.Clear();
                    foreach (var project in projects)
                    {
                        RecentProjects.Add(project);
                    }
                }
            }
        }

        [RelayCommand]
        private void OpenProject()
        {
            if (SelectedProject == null)
            {
                MessageBox.Show("Please select a project to open.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            var mainWindow = Application.Current.Windows.OfType<MainWindow>().FirstOrDefault();
            if (mainWindow != null && mainWindow.DataContext is MainViewModel mainViewModel)
            {
                mainViewModel.OpenProject(SelectedProject);
                mainWindow.Show(); 
                CloseRecentFilesWindow(); 
            }
            else
            {
                MessageBox.Show("Failed to open the MainWindow.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        [RelayCommand]
        private void NewProject()
        {
            var mainWindow = Application.Current.Windows.OfType<MainWindow>().FirstOrDefault();
            if (mainWindow != null && mainWindow.DataContext is MainViewModel mainViewModel)
            {
                mainViewModel.Shapes.Clear(); 
                mainWindow.Show(); 
                CloseRecentFilesWindow(); 
            }
            else
            {
                MessageBox.Show("Failed to open the MainWindow.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        [RelayCommand]
        private void DeleteProject()
        {
            if (SelectedProject == null)
            {
                MessageBox.Show("Please select a project to delete.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            var result = MessageBox.Show(
                $"Are you sure you want to delete the project '{SelectedProject.ProjectName}'?",
                "Confirm Delete",
                MessageBoxButton.YesNo,
                MessageBoxImage.Warning);

            if (result == MessageBoxResult.Yes)
            {
                using (var context = new DrawingContext())
                {
                    var projectToDelete = context.Projects.Find(SelectedProject.ProjectID);
                    if (projectToDelete != null)
                    {
                        context.Projects.Remove(projectToDelete);
                        context.SaveChanges();

                        RecentProjects.Remove(SelectedProject);

                    }
                    else
                    {
                        MessageBox.Show("Failed to find the project in the database.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
        }

        private void CloseRecentFilesWindow()
        {
            var recentFilesWindow = Application.Current.Windows.OfType<RecentFilesWindow>().FirstOrDefault();
            recentFilesWindow?.Close();
        }
    }
}