using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BooksOOP.Views;
using BooksOOP.Models;
using BooksOOP.Models.BookStorages;

namespace BooksOOP.Controllers
{
    class SalesController : Controller
    {
        public SalesController()
            : base(new ConsoleView())
        {
            //Замапированные роуты
            _routes.Add(new Route("menu", "MainController", "Index"));
            _routes.Add(new Route("view", "MainController", "View"));
        }

        public void IndexAction()
        {
            Sales sales = ModelStorage.GetSales();
            _view.Show(new string[] { $"Total sallary = {sales.GetTotalSallary()}",
                                      $"type 'view' to watch all saled books",
                                      $"type 'menu' to back to the menu" });
            string command = Console.ReadLine();
            Route nextController = _routes.FirstOrDefault(b => b.Id == command);
            redirect(nextController.Action, nextController.Controller, null);
        }

        public void ViewAction()
        {
            Sales sales = ModelStorage.GetSales();
            if (sales.GetBooks().Count != 0)
            {
                _view.Show("Following books was selled:");
                foreach (Book book in sales.GetBooks())
                {
                    _view.Show(book.Name);
                }
            }
            else
            {
                _view.Show("None of the books was selled");
            }

            _view.Show("type 'menu' to return on the menu");
            string command = Console.ReadLine();
            Route nextController = _routes.FirstOrDefault(b => b.Id == command);
            redirect(nextController.Action, nextController.Controller, null);
        }
    }
}
