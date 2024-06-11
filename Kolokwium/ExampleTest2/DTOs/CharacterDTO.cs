using System.ComponentModel.DataAnnotations;

namespace ExampleTest2.DTOs;

public class CharacterDTO
{
    [MaxLength(50)]
    public string FirstName { get; set; }
    [MaxLength(120)]
    public string LastName { get; set; }

    public int CurrentWeight { get; set; }
    public int MaxWeight { get; set; }
    public ICollection<BackpackItemDTO> backpackItems { get; set; }
    public ICollection<TitleDTO> titles { get; set; }
}

public class BackpackItemDTO
{
    [MaxLength(100)]
    public string itemName { get; set; }
    public int itemWeight { get; set; }
    public int amount { get; set; }
}

public class TitleDTO
{
    [MaxLength(100)]
    public string Name { get; set; }
    public DateTime AcquiredAt { get; set; }
}