using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WPF_Final_Project.Accounts;
using WPF_Final_Project.Views;
using WPF_Final_Project.DataBase;

namespace WPF_Final_Project.AccountsViewModels
{
    public partial class CreateAccountViewModel : ObservableObject
    {
        [ObservableProperty]
        private string _userName;

        [ObservableProperty]
        private string _firstName;

        [ObservableProperty]
        private string _lastName;

        [ObservableProperty]
        private string _email;

        [ObservableProperty]
        private string _phone;

        [ObservableProperty]
        private Gender _gender;

        [ObservableProperty]
        private string _password;

        [ObservableProperty]
        private string _confirmPassword;

        [RelayCommand]
        private void CreateAccount()
        {
            if (string.IsNullOrEmpty(UserName) || string.IsNullOrEmpty(FirstName) || string.IsNullOrEmpty(LastName) ||
                string.IsNullOrEmpty(Password) || string.IsNullOrEmpty(ConfirmPassword))
            {
                MessageBox.Show("Please fill in all required fields.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (Password != ConfirmPassword)
            {
                MessageBox.Show("Passwords do not match.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            var newUser = new User
            {
                UserName = UserName,
                Fname = FirstName,
                Lname = LastName,
                email = Email,
                Phone = Phone,
                gender = Gender,
                Password = Password
            };

            using (var context = new DrawingContext())
            {
                context.Users.Add(newUser);
                context.SaveChanges();
            }

            MessageBox.Show("Account created successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);

            var loginWindow = new LoginPage();
            loginWindow.Show();

            Application.Current.Windows.OfType<CreateAccountPage>().FirstOrDefault()?.Close();
        }

        [RelayCommand]
        private void NavigateToLogin()
        {
            var loginWindow = new LoginPage();
            loginWindow.Show();

            Application.Current.Windows.OfType<CreateAccountPage>().FirstOrDefault()?.Close();
        }
    }
}
