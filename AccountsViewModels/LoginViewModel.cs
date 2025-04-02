using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Windows;
using WPF_Final_Project.Accounts;
using WPF_Final_Project.DataBase;
using WPF_Final_Project.ViewModels;


namespace WPF_Final_Project.AccountsViewModels
{
    public partial class LoginViewModel : ObservableObject
    {

        [ObservableProperty]
        private string _userName;

        [ObservableProperty]
        private string _password;

        [RelayCommand]
        private void Login()
        {
            if (string.IsNullOrEmpty(UserName) || string.IsNullOrEmpty(Password))
            {
                MessageBox.Show("Please enter both username and password.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            using (var context = new DrawingContext())
            {
                var user = context.Users.FirstOrDefault(u => u.UserName == UserName && u.Password == Password);
                if (user != null)
                {
                    var main = new MainWindow();
                    if (main.DataContext is MainViewModel mainViewModel)
                    {
                        mainViewModel.CurrentUserId = user.UserID;
                    }

                    var recent = new RecentFilesWindow();
                    recent.Show();

                    Application.Current.Windows.OfType<LoginPage>().FirstOrDefault()?.Close();
                }
                else
                {
                    MessageBox.Show("Invalid username or password.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        [RelayCommand]
        private void NavigateToCreateAccount()
        {
            var createAccountWindow = new CreateAccountPage();
            createAccountWindow.Show();

            Application.Current.Windows.OfType<LoginPage>().FirstOrDefault()?.Close();
        }

    }
}


