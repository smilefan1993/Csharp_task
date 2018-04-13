using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BooksOOP.Views;
using BooksOOP.Models;
using BooksOOP.Models.BookStorages;

//Контроллер макулатуры
namespace BooksOOP.Controllers
{
    class WastepaperController : Controller
    {
        public WastepaperController()
            : base(new ConsoleView())
        {
            //Замапированные роуты
            _routes.Add(new Route("catalog", "WastepaperController", "CatalogView"));
            _routes.Add(new Route("menu", "MainController", "Index"));
            _routes.Add(new Route("view", "WastepaperController", "View"));
            _routes.Add(new Route("index", "WastepaperController", "Index"));
        }

        public void IndexAction()
        {
            Wastepaper wastepaper = ModelStorage.GetWastepaper();
            if (wastepaper.RequiredWeight == 0)
            {
                _view.Show("Weight doesn't set, type 'weight' to set");
                string command = Console.ReadLine();
                if (command == "weight")
                {
                    _view.Show("type weight:");
                    string weight = Console.ReadLine();
                    wastepaper.RequiredWeight = Int32.Parse(weight);
                    _view.Show("Weight are set. Press 'Enter' to return");
                    Console.ReadLine();
                    Route nextController = _routes.FirstOrDefault(b => b.Id == "menu");
                    redirect(nextController.Action, nextController.Controller, null);
                }
            }
            else
            {
                if (wastepaper.RequiredWeight < wastepaper.GetWeight())
                {
                    _view.Show("Wastepaper was collect, press 'Enter' to return");
                    Console.ReadLine();
                    Route nextController = _routes.FirstOrDefault(b => b.Id == "menu");
                    redirect(nextController.Action, nextController.Controller, null);
                }
                else
                {
                    _view.Show(new string[] { $"Total weight of wastepaper are {wastepaper.GetWeight()}",
                                              $"{wastepaper.RequiredWeight - wastepaper.GetWeight()} are left to collect",
                                              $"type 'view' to view catalogs with book",
                                              $"type 'menu' to back to menu"});
                    string input = Console.ReadLine();
                    Route nextController = _routes.FirstOrDefault(b => b.Id == input);
                    redirect(nextController.Action, nextController.Controller, null);
                }
            }
        }

        public void ViewAction()
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
                _view.Show(new string[] { "Type name of the book, to collect it into Wastepaper" ,
                                          "Type 'menu' to return to the menu"});
                string bookName = Console.ReadLine();
                Book removeBook = currentCatalog.GetByName(bookName);
                Wastepaper wastepaper = ModelStorage.GetWastepaper();
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
