using System.Linq;
using System.Net.Mime;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Microsoft.EntityFrameworkCore;


namespace AvaloniaApplication9;

public partial class UserWindow : Window
{
    private DataGrid usersDGrid;
    private TextBox searchTBox;
    public UserWindow()
    {
        InitializeComponent();

        usersDGrid = this.FindControl<DataGrid>("UsersDGrid");
        searchTBox = this.FindControl<TextBox>("SearchTBox");
        
        usersDGrid.Items = Service.GetDbContext().Users.Include(q=>q.IdRoleNavigation).ToList();
        
#if DEBUG
        this.AttachDevTools();
#endif
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
    
    private void LogOutBtn_OnClick(object? sender, RoutedEventArgs e)
    {
        new MainWindow().Show();
        Close();
    }
    
    private void SearchBtn_OnClick(object? sender, RoutedEventArgs e)
    {
        if (string.IsNullOrWhiteSpace(searchTBox.Text))
        {
            usersDGrid.Items = Service.GetDbContext().Users.Include(q=>q.IdRoleNavigation).ToList();
        }
        else
        {
            usersDGrid.Items = Service.GetDbContext().Users
                .Where(q => q.Login.ToLower().Contains(searchTBox.Text.ToLower()) 
                            || q.Name.ToLower().Contains(searchTBox.Text.ToLower()) 
                            || q.Surname.ToLower().Contains(searchTBox.Text.ToLower())).ToList();   
        }
    }
}