using Avalonia.Controls;
using AvaloniaApplication1.Views;
using Microsoft.Data.SqlClient;
using Microsoft.Win32;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using System;
using System.Collections.ObjectModel;
using System.Reactive;
using System.Reactive.Linq;
using System.Threading.Tasks;
using System.ComponentModel;
using AvaloniaApplication1;

namespace AvaloniaApplication1.ViewModels;

public class MainViewModel : ViewModelBase
{
    //Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=PC;Integrated Security=True
    private const string _connectionString = @"Data Source=(LOCALDB)\MSSQLLOCALDB;Initial Catalog=PC;Integrated Security=True";
    private static SqlConnection connectionSql = new SqlConnection(_connectionString);

    public ReactiveCommand<Unit, Unit> GoToSecondWindowCommand { get; }
    public ReactiveCommand<Unit, Unit> Login { get; }
    [Reactive]
    public string? Login1 { get; set; }
    [Reactive]
    public string? Password { get; set; }
    [Reactive]
    public string? TextBlock90 {  get; set; }

    public MainViewModel()
    {
        TextBlock90 = "Авторизация";
        GoToSecondWindowCommand = ReactiveCommand.Create(() =>
        {
            var secondWindow = new SecondWindow();
            secondWindow.Show();
        });
        Login = ReactiveCommand.Create(() =>
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                try
                {
                    connection.Open();
                    string query = "SELECT COUNT(1) FROM Users WHERE Name=@Name AND Password=@Password";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Name", Login1);
                        command.Parameters.AddWithValue("@Password", Password);
                        SqlDataReader reader = command.ExecuteReader();
                        if(reader.HasRows) 
                        {
                            reader.Close();

                            //проверяем роль
                            query = "SELECT Role FROM Users WHERE Name=@Name AND Password=@Password";
                            SqlCommand roleCommand = new SqlCommand(query, connection);
                            roleCommand.Parameters.AddWithValue("@Name", Login1);
                            roleCommand.Parameters.AddWithValue("@Password", Password);

                            string role = roleCommand.ExecuteScalar().ToString();

                            // Если роль соответствует, вход успешен
                            if(role == "1")
                            {
                                TextBlock90 = "Авторизация";
                                var tovarWindow1 = new TovarWindow();
                                tovarWindow1.Show();
                            }
                            else if (role == "2") 
                            {
                                TextBlock90 = "Авторизация";
                                var seeTovar = new SeeTovar();
                                seeTovar.Show();
                            }
                            else
                            {
                                TextBlock90 = "Ошибка";
                            }

                        }
                        else
                        {
                            TextBlock90 = "Ошибка";
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
                finally
                {
                    connection.Close();
                }
            }
        });
    }
}