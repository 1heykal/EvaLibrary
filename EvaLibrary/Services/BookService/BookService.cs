using EvaLibrary.DbContexts;
using EvaLibrary.Entities;
using Microsoft.EntityFrameworkCore;

namespace EvaLibrary.Services.BookService;

public class BookService : IBookService
{
    private readonly ApplicationDbContext _context;

    public BookService(ApplicationDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }
    
    public List<Book> GetAllBooks()
    {
        return _context.Books.Include(b => b.Author).ToList();
    }

    public Book? GetBookById(int id)
    {
        return _context.Books.Include(b => b.Author).FirstOrDefault(b => b.Id == id);
    }

    public void AddBook(Book book)
    {
        _context.Books.Add(book);
        _context.SaveChanges();
    }

    public void UpdateBook(Book book)
    {
        _context.Books.Update(book);
        _context.SaveChanges();
    }

    public void DeleteBook(Book book)
    {
        _context.Books.Remove(book);
        _context.SaveChanges();
    }
}