using EvaLibrary.Entities;

namespace EvaLibrary.Services.AuthorService;

public interface IAuthorService
{
    public List<Author> GetAllAuthors();

    public Author? GetAuthorById(int id);

    public void AddAuthor(Author author);

    public void UpdateAuthor(Author author);
    public void DeleteAuthor(Author author);
}