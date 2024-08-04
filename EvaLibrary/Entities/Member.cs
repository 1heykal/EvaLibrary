using System.ComponentModel.DataAnnotations;

namespace EvaLibrary.Entities;

public class Member
{
    [Key]
    public int Id { get; set; }

    public string Name { get; set; }

    [DataType(DataType.Date)]
    public DateOnly JoinDate { get; set; }

    [DataType(DataType.Date)]
    public DateOnly BirthDate { get; set; }

    public ICollection<Book> Books { get; set; }
}