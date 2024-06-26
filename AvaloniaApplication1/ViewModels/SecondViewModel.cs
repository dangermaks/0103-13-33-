using Microsoft.Data.SqlClient;
using ReactiveUI.Fody.Helpers;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Text;
using System.Threading.Tasks;

namespace AvaloniaApplication1.ViewModels
{
    public class SecondViewModel : ViewModelBase
    {
        private const string _connectionString = @"Data Source=(LOCALDB)\MSSQLLOCALDB;Initial Catalog=PC;Integrated Security=True";
        private static SqlConnection connectionSql = new SqlConnection(_connectionString);
        public static void ConnectionDB(string connectionString = _connectionString)
        {
            try
            {
                connectionSql.Open();
                connectionSql.Close();
            }
            catch
            {
                throw new Exception("error in line connectionString");
            }
        }
        public ReactiveCommand<Unit, Unit> Registr { get; set; }
        [Reactive]
        public string? login { get; set; }
        [Reactive]
        public string? password { get; set; }
        [Reactive]
        public string? surname { get; set; }
        [Reactive]
        public string? name { get; set; }
        [Reactive]
        public string? lastname { get; set; }
        [Reactive]
        public string? mail { get; set; }
        [Reactive]
        public string? TextBlock91 { get; set; }
        public SecondViewModel()
        {
            Registr = ReactiveCommand.Create(() =>
            {
                if (login == "" || password == "" || surname == "" || name == "" || lastname == "" || mail == "")
                {
                    TextBlock91 = "Заполните все поля";
                }
                else
                {
                    string name = login;
                    string password1 = password;

                    if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(password1))
                    {
                        TextBlock91 = "Введите логин и пароль";
                        return;
                    }

                    using (SqlConnection connection = new SqlConnection(_connectionString))
                    {
                        connection.Open();

                        // Проверяем, существует ли пользователь с таким именем
                        string checkQuery = "SELECT COUNT(*) FROM Users WHERE Name = @Name";
                        SqlCommand checkCommand = new SqlCommand(checkQuery, connection);
                        checkCommand.Parameters.AddWithValue("@Name", name);

                        int userCount = (int)checkCommand.ExecuteScalar();

                        if (userCount > 0)
                        {
                            TextBlock91 = "Пользователь с таким именем уже существует";
                            return;
                        }

                        // Если пользователь не существует, добавляем новую запись
                        string insertQuery = "INSERT INTO Users (Name, Password, Role) VALUES (@Name, @Password, 2)";
                        SqlCommand insertCommand = new SqlCommand(insertQuery, connection);
                        insertCommand.Parameters.AddWithValue("@Name", name);
                        insertCommand.Parameters.AddWithValue("@Password", password1);

                        int result = insertCommand.ExecuteNonQuery();

                        if (result > 0)
                        {
                            TextBlock91 = "Регистрация прошла успешно";
                            login = "";
                            password = "";
                        }
                        else
                        {
                            TextBlock91 = "Ошибка регистрации";
                        }
                    }
                }
            });
        }

    }
}
