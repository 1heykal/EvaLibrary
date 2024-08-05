using EvaLibrary.Entities;

namespace EvaLibrary.Services.BorrowService;

public interface IBorrowService
{
    public List<Borrow> GetAllBorrows();

    public Borrow? GetBorrowById(int id);

    public void AddBorrow(Borrow borrow);

    public void UpdateBorrow(Borrow borrow);
    public void DeleteBorrow(Borrow borrow);
}