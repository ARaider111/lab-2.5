using System.Linq;
using System.Net.Mime;
using Avalonia.Controls;
using Avalonia.Interactivity;
using AvaloniaApplication9.Models;
using Microsoft.EntityFrameworkCore;

namespace AvaloniaApplication9;

public partial class MainWindow : Window
{
    private TextBox loginTBox;
    private TextBox passwordTBox;
    public MainWindow()
    {
        InitializeComponent();
        
        loginTBox = this.FindControl<TextBox>("LoginTBox");
        passwordTBox = this.FindControl<TextBox>("PasswordTBox");
    }
    

    private void AuthBtn_OnClick(object? sender, RoutedEventArgs e)
    {
        if (!string.IsNullOrWhiteSpace(loginTBox.Text) && !string.IsNullOrWhiteSpace(passwordTBox.Text))
        {
            var userAuth = Service.GetDbContext().Users
                .FirstOrDefault(u => u.Login == loginTBox.Text && u.Password == passwordTBox.Text);
            if (userAuth != null && userAuth.IdRole == 1)
            {
                new UserWindow().Show();
                Close();
            }
            else if (userAuth != null && userAuth.IdRole == 2)
            {
                new AdminWindow().Show();
                Close();
            }
        }
    }
    
    private void RegBtn_OnClick(object? sender, RoutedEventArgs e)
    {
        new RegistrationWindow().Show();
        Close();
    }

   
    
}