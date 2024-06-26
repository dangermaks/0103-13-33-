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

namespace AvaloniaApplication1.ViewModels
{
    public class SeeTovarViewModel : ViewModelBase
    {
        public ReactiveCommand<Unit, Unit> too { get; set; }

        private ObservableCollection<Tovar> _tovar;
        public ObservableCollection<Tovar> Tovar
        {
            get => _tovar;
            set => this.RaiseAndSetIfChanged(ref _tovar, value);
        }
        [Reactive]
        public string? zakaz { get; set; }
        public SeeTovarViewModel()
        {
            zakaz = "Выберите товар";
            too = ReactiveCommand.Create(() =>
            {
                zakaz = "Успешный заказ";
            });
            Tovar = new ObservableCollection<Tovar>();

            var connectionStr = "Data Source=(LOCALDB)\\MSSQLLOCALDB;Initial Catalog=PC;Integrated Security=True";

            using (var connection = new SqlConnection(connectionStr))
            {
                connection.Open();
                var command = new SqlCommand("SELECT * FROM Tovars", connection);
                var reader = command.ExecuteReader();

                while (reader.Read())
                {
                    string name = reader.GetString(reader.GetOrdinal("Name"));
                    string cost = reader.GetInt32(reader.GetOrdinal("Cost")).ToString();
                    string opis = reader.GetString(reader.GetOrdinal("Opisanie"));
                    string koli = reader.GetInt32(reader.GetOrdinal("Kolichestvo")).ToString();

                    Tovar.Add(new Tovar(name, cost, opis, koli));
                }
            }
        }
    }
    public class Tovar
    {
        public string Cost { get; set; }
        public string Name { get; set; }
        public string Opis { get; set; }
        public string Koli { get; set; }

        public Tovar(string name, string cost, string opis, string koli)
        {
            Name = name;
            Cost = cost;
            Opis = opis;
            Koli = koli;
        }
    }
}
