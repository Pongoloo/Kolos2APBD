using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace ExampleTest2.Models;

public class Title
{
    [Key]
    public int Id { get; set; }
    [MaxLength(100)]
    public string Name { get; set; } = string.Empty;
    
    
    public ICollection<Character_title> CharacterTitles { get; set; } = new HashSet<Character_title>();
}