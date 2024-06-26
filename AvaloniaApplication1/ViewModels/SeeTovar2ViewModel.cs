using Microsoft.Data.SqlClient;
using ReactiveUI.Fody.Helpers;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive;
using System.Text;
using System.Threading.Tasks;
using DynamicData;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace AvaloniaApplication1.ViewModels
{
    public class SeeTovar2ViewModel : ViewModelBase
    {
        //Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=PC;Integrated Security=True
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
        public ReactiveCommand<Unit, Unit> too { get; set; }

        private ObservableCollection<Tovar> _tovar;
        public ObservableCollection<Tovar> Tovars
        {
            get => _tovar;
            set => this.RaiseAndSetIfChanged(ref _tovar, value);
        }
        private Tovar? _selectedItem;
        public Tovar? SelectedItem
        {
            get => _selectedItem;
            set => this.RaiseAndSetIfChanged(ref _selectedItem, value);
        }
        public void DeleteSelectedItem()
        {
            // открываем соединение
            connectionSql.Open();

            // Implement SQL query to delete selected item from the database
            string deleteQuery = "DELETE FROM Tovars WHERE Name = @Name";
            SqlCommand deleteCommand = new SqlCommand(deleteQuery, connectionSql);

            if (SelectedItem == null)
            {
                //
                return;
            }
            // добавляем параметр @Name
            deleteCommand.Parameters.AddWithValue("@Name", SelectedItem.Name);

            // выполнение команды
            if (deleteCommand.ExecuteNonQuery() > 0)
            {
                Tovars.Remove(SelectedItem);
                SelectedItem = null;
            }
            
            // закрываем соединение
            connectionSql.Close();
        }
        [Reactive]
        public string? delete { get; set; }
        public SeeTovar2ViewModel()
        {
            delete = "Выберите товар";
            too = ReactiveCommand.Create(() =>
            {
                delete = "Успешное удачление";

                DeleteSelectedItem();
            });
            Tovars = new ObservableCollection<Tovar>();

            var connectionStr = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=PC;Integrated Security=True;TrustServerCertificate=True";

            using (var connection = new SqlConnection(connectionStr))
            {
                connection.Open();
                var command = new SqlCommand("SELECT * FROM Tovars", connection);
                var reader = command.ExecuteReader();

                while (reader.Read())
                {
                    string name = reader.GetString(reader.GetOrdinal("Name"));
                    string cost = reader.GetString(reader.GetOrdinal("Cost"));
                    string opis = reader.GetString(reader.GetOrdinal("Opisanie"));
                    string koli = reader.GetString(reader.GetOrdinal("Kolichestvo"));

                    Tovars.Add(new Tovar(name, cost, opis, koli));
                }
            }
        }
    }
}
