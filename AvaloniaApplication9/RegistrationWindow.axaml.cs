using System.Linq;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using AvaloniaApplication9.Models;

namespace AvaloniaApplication9;

public partial class RegistrationWindow : Window
{
   private TextBox loginTBox;
    private TextBox passwordTBox;
    private TextBox nameTBox;
    private TextBox surnameTBox;
    private TextBox phonenumberTBox;
    private DatePicker birthdateDPicker;
    
    public RegistrationWindow()
    {
        InitializeComponent();

        loginTBox = this.FindControl<TextBox>("LoginTBox");
        passwordTBox = this.FindControl<TextBox>("PasswordTBox");
        nameTBox = this.FindControl<TextBox>("NameTBox");
        surnameTBox = this.FindControl<TextBox>("SurnameTBox");
        phonenumberTBox = this.FindControl<TextBox>("PhonenumberTBox");
        birthdateDPicker = this.FindControl<DatePicker>("BirthdateDPicker");

#if DEBUG
        this.AttachDevTools();
#endif
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }

    // Данный метод срабатыват при нажатии на кнопку Зарегистироваться
    private void RegBtn_OnClick(object? sender, RoutedEventArgs e)
    {
        // Проверяем, чтобы все поля были заполнены
        if (!string.IsNullOrWhiteSpace(loginTBox.Text) &&
            !string.IsNullOrWhiteSpace(passwordTBox.Text) &&
            !string.IsNullOrWhiteSpace(nameTBox.Text) &&
            !string.IsNullOrWhiteSpace(surnameTBox.Text) &&
            !string.IsNullOrWhiteSpace(phonenumberTBox.Text) &&
            !string.IsNullOrWhiteSpace(birthdateDPicker.SelectedDate.ToString()))
        {
            // Создадим экземпляр класса User
            var newUser = new User()
            {
                Id = Service.GetDbContext().Users.Max(q=>q.Id) + 1,
                Login = loginTBox.Text,
                Password = passwordTBox.Text,
                Name = nameTBox.Text,
                Surname = surnameTBox.Text,
                PhoneNumber = phonenumberTBox.Text,
                Birthdate = birthdateDPicker.SelectedDate.ToString(),
                IdRole = 1
            };
            // Добавим нового пользователя
            Service.GetDbContext().Users.Add(newUser);
            // Сохраним изменения
            Service.GetDbContext().SaveChanges();
            
            // Вернемся на окно авторизации
            new MainWindow().Show();
            Close();
        }
    }

    // Данный метод срабатывает при нажатии на кнопку Назад
    // Вернет пользователя на окно авторизации
    private void BackBtn_OnClick(object? sender, RoutedEventArgs e)
    {
        // Открываем окно MainWindow
        new MainWindow().Show();
        // Закрываем текущее окно
        Close();
    }
}