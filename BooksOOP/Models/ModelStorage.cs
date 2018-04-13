using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BooksOOP.Models.BookStorages;

// Имитация базы данных приложения. Доступна для всего проекта, хранит экземпляры хранилищ
namespace BooksOOP.Models
{
    public static class ModelStorage
    {
        private static IList<Catalog> _catalogModel;
        private static Wastepaper _wastepapaer;
        private static Sales _sales;
        private static TableStand _tablestand;

        static ModelStorage()
        {
            _wastepapaer = new Wastepaper("Wasterpaper",100);
            _tablestand = new TableStand("Tablestand");
            _catalogModel = new List<Catalog>();
            _sales = new Sales("Sales");
        }

        public static void AddBookStorage(Catalog model)
        {
            _catalogModel.Add(model);
        }

        public static IList<Catalog> GetCatalogs()
        {
            return _catalogModel;
        }

        public static TableStand GetTablestand()
        {
            return _tablestand;
        }

        public static Wastepaper GetWastepaper()
        {
            return _wastepapaer;
        }

        public static Catalog GetBookStorage(string name)
        {
            return _catalogModel.FirstOrDefault(b => b.Name == name);
        }

        public static Sales GetSales()
        {
            return _sales;
        }
    }
}
