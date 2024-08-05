using EvaLibrary.DbContexts;
using EvaLibrary.Entities;
using Microsoft.EntityFrameworkCore;

namespace EvaLibrary.Services.MemberService;

public class MemberService : IMemberService
{
    private readonly ApplicationDbContext _context;

    public MemberService(ApplicationDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }
    
    public List<Member> GetAllMembers()
    {
        return _context.Members.Include(m => m.Borrows).ToList();
    }

    public Member? GetMemberById(int id)
    {
        return _context.Members
            .Include(m => m.Borrows)
            .FirstOrDefault(m => m.Id == id);
    }

    public void AddMember(Member member)
    {
        _context.Members.Add(member);
        _context.SaveChanges();
    }

    public void UpdateMember(Member member)
    {
        _context.Members.Update(member);
        _context.SaveChanges();
    }

    public void DeleteMember(Member member)
    {
        _context.Members.Remove(member);
        _context.SaveChanges();
    }
}