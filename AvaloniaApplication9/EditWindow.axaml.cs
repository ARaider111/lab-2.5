using System.Linq;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using AvaloniaApplication9.Models;

namespace AvaloniaApplication9;

public partial class EditWindow : Window
{
   private TextBox loginTBox;
    private TextBox passwordTBox;
    private TextBox nameTBox;
    private TextBox surnameTBox;
    private TextBox phonenumberTBox;
    private ComboBox roleCBox;
    private User editUser;
    
    public EditWindow()
    {
        InitializeComponent();

#if DEBUG
        this.AttachDevTools();
#endif
    }

    public EditWindow(User editUser)
    {
        InitializeComponent();
        
        loginTBox = this.FindControl<TextBox>("LoginTBox");
        passwordTBox = this.FindControl<TextBox>("PasswordTBox");
        nameTBox = this.FindControl<TextBox>("NameTBox");
        surnameTBox = this.FindControl<TextBox>("SurnameTBox");
        phonenumberTBox = this.FindControl<TextBox>("PhonenumberTBox");
        roleCBox = this.FindControl<ComboBox>("RoleCBox");
        this.editUser = editUser;
        
        roleCBox.Items = Service.GetDbContext().Roles.ToList();

        loginTBox.Text = editUser.Login;
        passwordTBox.Text = editUser.Password;
        nameTBox.Text = editUser.Name;
        surnameTBox.Text = editUser.Surname;
        phonenumberTBox.Text = editUser.PhoneNumber;
        roleCBox.SelectedItem = editUser.IdRoleNavigation;
        
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
    
    private void EditBtn_OnClick(object? sender, RoutedEventArgs e)
    {
        // Проверяем, чтобы все поля были заполнены
        if (!string.IsNullOrWhiteSpace(loginTBox.Text) &&
            !string.IsNullOrWhiteSpace(passwordTBox.Text) &&
            !string.IsNullOrWhiteSpace(nameTBox.Text) &&
            !string.IsNullOrWhiteSpace(surnameTBox.Text) &&
            !string.IsNullOrWhiteSpace(phonenumberTBox.Text))
        {
            // Перезаписываем поля у изменяемого объекта
            editUser.Name = nameTBox.Text;
            editUser.Surname = surnameTBox.Text;
            editUser.Login = loginTBox.Text;
            editUser.Password = passwordTBox.Text;
            editUser.PhoneNumber = phonenumberTBox.Text;
            editUser.IdRoleNavigation = roleCBox.SelectedItem as Role;
            
            Service.GetDbContext().SaveChanges();
            Close();
        }
    }
    
    private void BackBtn_OnClick(object? sender, RoutedEventArgs e)
    {
        new MainWindow().Show();
        Close();
    }
}