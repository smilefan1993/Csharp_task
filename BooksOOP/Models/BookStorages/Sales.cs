using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//Хранилище продаж, хранит в себе сумму по всем проданным книгам
namespace BooksOOP.Models.BookStorages
{
    public class Sales : BookStorage
    {
        private int _totalSallary;

        public Sales(string name)
            : base(name)
        {
            _totalSallary = 0;
        }

        public override string AddBook(Book book)
        {
            base.AddBook(book);
            _totalSallary += book.Price;
            return "Book was sold";
        }

        public int GetTotalSallary()
        {
            return _totalSallary;
        }
    }
}
