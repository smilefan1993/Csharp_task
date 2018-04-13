using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BooksOOP.Views;
using BooksOOP.Models;

//Контроллер главного меню
namespace BooksOOP.Controllers
{
    public class MainController : Controller
    {
        public MainController()
            : base (new ConsoleView())
        {
            //Замапированные роуты
            _routes.Add(new Route("1", "CatalogController", "Index"));
            _routes.Add(new Route("2", "TablestandController", "Index"));
            _routes.Add(new Route("3", "WastepaperController", "Index"));
            _routes.Add(new Route("4", "SalesController", "Index"));
        }

        public void IndexAction()
        {
            string[] viewString = {"MainMenu:",
                                    "1. Catalog",
                                    "2. Tablestand",
                                    "3. Wastepaper",
                                    "4. Sales"};
            _view.Show(viewString);
            string routeId = Console.ReadLine();
            Route nextController = _routes.FirstOrDefault(b => b.Id == routeId);
            redirect(nextController.Action, nextController.Controller, null);
        }
    }
}
