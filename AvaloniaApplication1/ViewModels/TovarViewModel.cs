using Microsoft.Data.SqlClient;
using ReactiveUI.Fody.Helpers;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Text;
using System.Threading.Tasks;
using AvaloniaApplication1.Views;

namespace AvaloniaApplication1.ViewModels
{
    public class TovarViewModel : ViewModelBase
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



        public ReactiveCommand<Unit, Unit> LoadImageCommand { get; }
        public ReactiveCommand<Unit, Unit> OPEN { get; }
        public ReactiveCommand<Unit, Unit> AddTovar { get; set; }
        public ReactiveCommand<Unit, Unit> AddImage { get; set; }
        [Reactive]
        public string? name1 { get; set; }
        [Reactive]
        public string? opisanie { get; set; }
        [Reactive]
        public string? Colichestvo { get; set; }
        [Reactive]
        public string? Cost { get; set; }
        [Reactive]
        public string? TextBlock92 { get; set; } = "Добавление товара";
        public TovarViewModel()
        {
            OPEN = ReactiveCommand.Create(() => 
            {
                var se2 = new SeeTovar2();
                se2.Show();
            });
            AddTovar = ReactiveCommand.Create(() =>
            {
                if (name1 == "" || opisanie == "" || Colichestvo == "" || Cost == "")
                {
                    TextBlock92 = "Заполните все текстовые поля";
                }
                else
                {
                    string name = name1;
                    string opisanie1 = opisanie;
                    string Colichestvo1 = Colichestvo;
                    string Cost1 = Cost;

                    if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(Cost1))
                    {
                        TextBlock92 = "Заполните все текстовые поля";
                        return;
                    }

                    using (SqlConnection connection = new SqlConnection(_connectionString))
                    {
                        connection.Open();
                        //добавляем товар
                        string insertQuery = "INSERT INTO Tovars (Name, Opisanie, Kolichestvo, Cost) VALUES (@Name, @Opisanie, @Kolichestvo, @Cost)";
                        SqlCommand insertCommand = new SqlCommand(insertQuery, connection);
                        insertCommand.Parameters.AddWithValue("@Name", name);
                        insertCommand.Parameters.AddWithValue("@Opisanie", opisanie1);
                        insertCommand.Parameters.AddWithValue("@Kolichestvo", Colichestvo1);
                        insertCommand.Parameters.AddWithValue("@Cost", Cost1);

                        int result = insertCommand.ExecuteNonQuery();

                        if (result > 0)
                        {
                            TextBlock92 = "Товар успешно добавлен";
                            name1 = "";
                            opisanie = "";
                            Colichestvo = "";
                            Cost = "";
                        }
                        else
                        {
                            TextBlock92 = "Ошибка добавления";
                        }
                    }
                }
            });
        }
    }
}
