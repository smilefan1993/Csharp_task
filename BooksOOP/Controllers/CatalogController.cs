using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using BooksOOP.Views;
using BooksOOP.Models;
using BooksOOP.Models.BookStorages;

//Реализация каталог контроллера - сразу говорю, валидацию типов(как и валидацию ввода) нигде не делал,

namespace BooksOOP.Controllers
{
    class CatalogController : Controller
    {
        public CatalogController()
            : base(new ConsoleView())
        {
            _routes.Add(new Route("1", "CatalogController", "NewCatalog"));
            _routes.Add(new Route("2", "CatalogController", "ViewCatalogs"));
            _routes.Add(new Route("3", "MainController", "Index"));
            _routes.Add(new Route("back", "CatalogController", "Index"));
            _routes.Add(new Route("5", "CatalogController", "ViewCatalog"));
            _routes.Add(new Route("add", "CatalogController", "AddBook"));
            _routes.Add(new Route("book", "CatalogController", "ViewBook"));
        }

        public void IndexAction()
        {
            string[] viewString = {"Catalog Menu:",
                                    "1. Create catalog",
                                    "2. View catalog list",
                                    "3. Back" };
            _view.Show(viewString);
            string routeId = Console.ReadLine();
            Route nextController = _routes.FirstOrDefault(b => b.Id == routeId);
            redirect(nextController.Action, nextController.Controller, null);
        }

        public void NewCatalogAction()
        {
            _view.Show("Set name for catalog:");
            string catalogName = Console.ReadLine();
            if (catalogName == "")
            {
                Console.WriteLine("Name cannot be empty!");
                Route nextController = _routes.FirstOrDefault(b => b.Id == "1");
                redirect(nextController.Action, nextController.Controller, null);
            } 
            else
            {
                ModelStorage.AddBookStorage(new Catalog(catalogName));
                _view.Show($"Catalog with name {catalogName} has been create");
                Route nextController = _routes.FirstOrDefault(b => b.Id == "back");
                redirect(nextController.Action, nextController.Controller, null);
            }            
        }

        public void ViewCatalogsAction()
        {
            _view.Show("Following catalogs was been created:");
            IList<Catalog> catalogs = ModelStorage.GetCatalogs();
            if (catalogs.Count != 0)
            {
                string[] viewString = new string[catalogs.Count];
                string[] catalogsName = new string[catalogs.Count];
                int lines = 0;
                foreach (Catalog catalog in catalogs)
                {
                    viewString[lines] = lines.ToString() + ". " + catalog.Name;
                    catalogsName[lines] = catalog.Name;
                    lines++;
                }

                _view.Show("Choose following catalogs");
                _view.Show(viewString);
                string chooseCatalog = Console.ReadLine();
                Route nextController = _routes.FirstOrDefault(b => b.Id == "5");
                redirect(nextController.Action, nextController.Controller, new object[] { catalogsName[Int32.Parse(chooseCatalog)] });
            }
            else
            {
                _view.Show(new string[] { "Catalogs doesnt exists",
                                          "Press 'Enter' to return"});
                Console.ReadLine();
                Route nextController = _routes.FirstOrDefault(b => b.Id == "back");
                redirect(nextController.Action, nextController.Controller, null);
            }
        }

        public void ViewCatalogAction(string catalogName)
        {
            _view.Show("Current catalog: " + catalogName);
            Catalog currentCatalog = ModelStorage.GetBookStorage(catalogName);
            if (currentCatalog.GetBooks().Count == 0)
            {
                _view.Show("Catalog doesnt have any books");
            }
            else
            {
                _view.Show("Catalog have following books:");
                foreach(IBook book in currentCatalog.GetBooks())
                {
                    _view.Show(book.Name);
                }
            }

            _view.Show(new string[] { "Type book name to show the info",
                                      "Type 'add' for adding book into catalog"});
            string command = Console.ReadLine();
            if (command == "add")
            {
                Route nextController = _routes.FirstOrDefault(b => b.Id == "add");
                redirect(nextController.Action, nextController.Controller, new object[] { currentCatalog });
            }
            else
            { 
                Route nextController = _routes.FirstOrDefault(b => b.Id == "book");
                redirect(nextController.Action, nextController.Controller, new object[] { command, currentCatalog });
            }
        }

        public void AddBookAction(IBookStorage catalog)
        {
            string[] viewString = {"Select genre of the book",
                                   "1. Detective",
                                   "2. Horror"};
            _view.Show(viewString);
            string genre = Console.ReadLine();
            Book book;
            switch (genre)
            {
                case "1":
                    book = new Detective();
                    break;
                case "2":
                    book = new Horror();
                    break;
                default:
                    book = new Book();
                    break;
            }

            _view.Show("Enter book name");
            book.Name = Console.ReadLine();
            _view.Show("Enter pages count");
            book.Pages = Int32.Parse(Console.ReadLine());
            _view.Show("Enter weight");
            book.Weight = Int32.Parse(Console.ReadLine());
            _view.Show("Enter Author");
            book.Author = Console.ReadLine();
            _view.Show("Enter binding");
            book.Binding = Console.ReadLine();
            _view.Show("Enter price");
            book.Price = Int32.Parse(Console.ReadLine());
            _view.Show("Enter text");
            book.Text = Console.ReadLine();
            catalog.AddBook(book);
            _view.Show("Book added, press Enter to return");
            Console.ReadLine();
            Route nextController = _routes.FirstOrDefault(b => b.Id == "back");
            redirect(nextController.Action, nextController.Controller, null);
        }

        public void ViewBookAction(string book, IBookStorage catalog)
        {
            Book currentBook = catalog.GetByName(book);
            string[] viewString = { $"Book name:{currentBook.Name}" ,
                                    $"Book page:{currentBook.Pages}",
                                    $"Book Author:{currentBook.Author}",
                                    $"Book binding:{currentBook.Binding}",
                                    $"Book weight:{currentBook.Weight}",
                                    $"Book price:{currentBook.Price}",
                                    $"Book text:{currentBook.Text}",
                                    $"Book description:{currentBook.GetDescription()}"
                                };
            _view.Show(viewString);
            _view.Show(new string[] { "Type 'Delete' for deleting book",
                                      "Type 'sell', to sell the book",
                                      "Type 'find' to find info from text in the book",
                                      "Type 'back' to return back"});
            string command = Console.ReadLine();
            switch (command)
            {
                case "Delete":
                    catalog.DeleteBook(currentBook);
                    _view.Show("Current book was deleted, press Enter to return back"); 
                    break;
                case "find":
                    _view.Show("Type word to search:");
                    string word = Console.ReadLine();
                    _view.Show(new string[] { currentBook.SearchInfo(word),
                                              "press Enter to return back"});
                    break;
                case "sell":
                    Sales sales = ModelStorage.GetSales();
                    sales.AddBook(currentBook);
                    catalog.DeleteBook(currentBook);
                    _view.Show(new string[] { $"Book {currentBook.Name} was sold",
                                              "press Enter to return back"});
                    break;
                default:
                    break;
            }
            Console.ReadLine();
            Route nextController = _routes.FirstOrDefault(b => b.Id == "back");
            redirect(nextController.Action, nextController.Controller, null);
        }
    }
}
