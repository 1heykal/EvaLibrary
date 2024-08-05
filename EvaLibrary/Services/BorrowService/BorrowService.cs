using EvaLibrary.DbContexts;
using EvaLibrary.Entities;
using Microsoft.EntityFrameworkCore;

namespace EvaLibrary.Services.BorrowService;

public class BorrowService : IBorrowService
{
    private readonly ApplicationDbContext _context;

    public BorrowService(ApplicationDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }
    
    public List<Borrow> GetAllBorrows()
    {
        return _context.Borrows.Include(b => b.Book)
            .Include(b => b.Member)
            .ToList();
    }

    public Borrow? GetBorrowById(int id)
    {
        return _context.Borrows
            .Include(b => b.Book)
            .Include(b => b.Member)
            .FirstOrDefault(b => b.Id == id);
    }

    public void AddBorrow(Borrow borrow)
    {
        _context.Borrows.Add(borrow);
        _context.SaveChanges();
    }

    public void UpdateBorrow(Borrow borrow)
    {
        _context.Borrows.Update(borrow);
        _context.SaveChanges();
    }

    public void DeleteBorrow(Borrow borrow)
    {
        _context.Borrows.Remove(borrow);
        _context.SaveChanges();
    }
}