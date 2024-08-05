using EvaLibrary.Entities;

namespace EvaLibrary.Services.MemberService;

public interface IMemberService
{
    public List<Member> GetAllMembers();

    public Member? GetMemberById(int id);

    public void AddMember(Member member);

    public void UpdateMember(Member member);
    public void DeleteMember(Member member);
}