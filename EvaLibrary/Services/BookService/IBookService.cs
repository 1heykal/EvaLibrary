using EvaLibrary.Entities;

namespace EvaLibrary.Services.BookService;

public interface IBookService
{
    public List<Book> GetAllBooks();

    public Book? GetBookById(int id);

    public void AddBook(Book book);

    public void UpdateBook(Book book);
    public void DeleteBook(Book book);
}