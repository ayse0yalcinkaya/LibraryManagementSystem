using LibraryManagementSystem.Models;
using LibraryManagementSystem.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagementSystem.Controllers
{
    public class BooksController : Controller
    {
        private readonly IBookRepository _bookRepository;

        public BooksController(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        public async Task<IActionResult> Index()
        {
            var books = await _bookRepository.GetAllAsync();
            return View(books);
        }

        public async Task<IActionResult> Details(int id)
        {
            var book = await _bookRepository.GetByIdAsync(id);
            if (book == null)
            {
                return NotFound();
            }
            return View(book);
        }

        // test verisi kullanılacak metod
        public async Task<IActionResult> AddTestBook()
        {
            // test kitap verileri
            var newBook = new Book
            {
                Title = "Kuyucaklı Yusuf",
                Author = "Sabahattin Ali",
                PublicationYear = 2019,
                ISBN = "0123456789",
                Genre = "Roman",
                Publisher = "BKM Kitapevi",
                PageCount = 219,
                Summary = "İnsan dediğin mahluk hiçbir şey değiştiremez. Bunun için, gönlünün rahat olmasını istersen, gördüğün fenalıkların bile bir hikmeti olduğunu düşün ve yeryüzünde olmayan iyilikleri oraya getirmek sevdasına kapılma... Sonra en mühimi: Kendini halinden şikâyet etmeye alıştırma. Ömrünün sonuna kadar dövünsen bu hayatın cefası tükenmez.\r\n\r\nDehşet dolu bir olayın gölgesinde büyümüş köylü bir gencin mücadelesini anlatan Kuyucaklı Yusuf, taşra ve taşralılık üzerine zamanının ötesinde bir roman.\r\n\r\n ",
                Language = "Türkçe",
                AvailableCopies = 10
            };

            // veritabanı ekleme
            await _bookRepository.AddAsync(newBook);

            // kitap bşarıyla eklenice yönlendir
            return RedirectToAction("Index");
        }

        //create aksiyonu
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Book book)
        {
            if (ModelState.IsValid)
            {
                await _bookRepository.AddAsync(book);
                return RedirectToAction(nameof(Index));
            }
            return View(book);
        }

        //edit aksiyonu
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var book = await _bookRepository.GetByIdAsync(id);
            if (book == null)
            {
                return NotFound();
            }
            return View(book);
        }

        
        [HttpPost] 
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Book book)
        {
            if (ModelState.IsValid)
            {
                await _bookRepository.UpdateAsync(book);
                return RedirectToAction(nameof(Index));
            }
            return View(book);
        }


        //silme aksiyonu
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var book = await _bookRepository.GetByIdAsync(id);
            if (book == null)
            {
                return NotFound();
            }
            return View(book);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _bookRepository.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }


    }
}
