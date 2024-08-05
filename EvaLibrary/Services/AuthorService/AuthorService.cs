using EvaLibrary.DbContexts;
using EvaLibrary.Entities;
using Microsoft.EntityFrameworkCore;

namespace EvaLibrary.Services.AuthorService;

public class AuthorService : IAuthorService
{
    private readonly ApplicationDbContext _context;

    public AuthorService(ApplicationDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }
    
    public List<Author> GetAllAuthors()
    {
        return _context.Authors.Include(b => b.Books).ToList();
    }

    public Author? GetAuthorById(int id)
    {
        return _context.Authors.Include(b => b.Books).FirstOrDefault(b => b.Id == id);
    }

    public void AddAuthor(Author author)
    {
        _context.Authors.Add(author);
        _context.SaveChanges();
    }

    public void UpdateAuthor(Author author)
    {
        _context.Authors.Update(author);
        _context.SaveChanges();
    }

    public void DeleteAuthor(Author author)
    {
        _context.Authors.Remove(author);
        _context.SaveChanges();
    }
}