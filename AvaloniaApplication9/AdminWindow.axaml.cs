using System.Linq;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using AvaloniaApplication9.Models;
using Microsoft.EntityFrameworkCore;

namespace AvaloniaApplication9;

public partial class AdminWindow : Window
{
   private DataGrid usersDGrid;
    private TextBox searchTBox;
    
    public AdminWindow()
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
    
    
    private void DeleteBtn_OnClick(object? sender, RoutedEventArgs e)
    {
        User? selectedUser = usersDGrid.SelectedItem as User;
        if (selectedUser != null)
        {
            Service.GetDbContext().Users.Remove(selectedUser);
            Service.GetDbContext().SaveChanges();
            usersDGrid.Items = Service.GetDbContext().Users.Include(q=>q.IdRoleNavigation).ToList();
        }
    }
    
    private async void EditBtn_OnClick(object? sender, RoutedEventArgs e)
    {
        User? selectedUser = usersDGrid.SelectedItem as User;
        if (selectedUser != null)
        {
            await new EditWindow(selectedUser).ShowDialog(this);
            usersDGrid.Items = Service.GetDbContext().Users.Include(q=>q.IdRoleNavigation).ToList();
        }
    }

}