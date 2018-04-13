using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BooksOOP.Views;
using BooksOOP.Models;
using BooksOOP.Models.BookStorages;

//По факту этот контроллер будет таким-же как и Wastepaper, за исключением того, что тут не будет ограничения по весу
namespace BooksOOP.Controllers
{
    class TablestandController : Controller
    {
        public TablestandController()
            : base(new ConsoleView())
        {
            //Замапированные роуты
            _routes.Add(new Route("catalog", "TablestandController", "CatalogView"));
            _routes.Add(new Route("menu", "MainController", "Index"));
            _routes.Add(new Route("index", "WastepaperController", "Index"));
        }

        public void IndexAction()
        {
            IList<Catalog> catalogs = ModelStorage.GetCatalogs();
            if (catalogs.Count != 0)
            {
                _view.Show("Choose catalog to take book over");
                string[] viewString = new string[catalogs.Count];
                string[] catalogsName = new string[catalogs.Count];
                int lines = 0;
                foreach (Catalog catalog in catalogs)
                {
                    viewString[lines] = lines.ToString() + ". " + catalog.Name;
                    catalogsName[lines] = catalog.Name;
                    lines++;
                }

                _view.Show(viewString);
                _view.Show("Type 'menu' for return into menu");
                string chooseCatalog = Console.ReadLine();
                if (chooseCatalog == "menu")
                {
                    Route nextController = _routes.FirstOrDefault(b => b.Id == chooseCatalog);
                    redirect(nextController.Action, nextController.Controller);
                }
                else
                {
                    Route nextController = _routes.FirstOrDefault(b => b.Id == "catalog");
                    redirect(nextController.Action, nextController.Controller, new object[] { catalogsName[Int32.Parse(chooseCatalog)] });

                }
            }
            else
            {
                _view.Show("Catalogs doesnt exists");
                _view.Show("Press 'Enter' to return");
                Console.ReadLine();
                Route nextController = _routes.FirstOrDefault(b => b.Id == "menu");
                redirect(nextController.Action, nextController.Controller, null);
            }
        }

        public void CatalogViewAction(string catalogName)
        {
            Catalog currentCatalog = ModelStorage.GetBookStorage(catalogName);
            if (currentCatalog.GetBooks().Count == 0)
            {
                _view.Show(new string[] { "This catalog doesn't have any books." ,
                                          "Press 'Enter' to return"});
                Console.ReadLine();
                Route nextController = _routes.FirstOrDefault(b => b.Id == "index");
                redirect(nextController.Action, nextController.Controller, null);
            }
            else
            {
                _view.Show("Catalog have following books:");
                foreach (IBook book in currentCatalog.GetBooks())
                {
                    _view.Show(book.Name);
                }
                _view.Show(new string[] { "Type name of the book, to collect it into tablestand" ,
                                          "Type 'menu' to return to the menu"});
                string bookName = Console.ReadLine();
                Book removeBook = currentCatalog.GetByName(bookName);
                TableStand wastepaper = ModelStorage.GetTablestand();
                _view.Show(wastepaper.AddBook(removeBook));
                currentCatalog.DeleteBook(removeBook);
                _view.Show("Press 'Enter' to return");
                Console.ReadLine();
                Route nextController = _routes.FirstOrDefault(b => b.Id == "index");
                redirect(nextController.Action, nextController.Controller, null);
            }
        }
    }
}
