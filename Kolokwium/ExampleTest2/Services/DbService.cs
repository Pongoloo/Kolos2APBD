using ExampleTest2.Data;
using ExampleTest2.DTOs;
using ExampleTest2.Models;
using Microsoft.EntityFrameworkCore;

namespace ExampleTest2.Services;

public class DbService : IDbService
{
    private readonly DatabaseContext _context;
    public DbService(DatabaseContext context)
    {
        _context = context;
    }


    private async Task<Character> GetCharacters(int characterId)
    {
        return await _context.Characters
            .Include(ch => ch.Backpacks)
            .ThenInclude(bp => bp.Item)
            .Include(c => c.CharacterTitles)
            .ThenInclude(ct => ct.Title)
            .FirstOrDefaultAsync(c => c.Id == characterId);
    }
    public async Task<CharacterDTO> GetCharacterInfo(int characterId)
    {
        var character = await GetCharacters(characterId);
        var characterDto = new CharacterDTO
        {
            FirstName = character.FirstName,
            LastName = character.LastName,
            CurrentWeight = character.CurrentWeight,
            MaxWeight = character.MaxWeight,
            backpackItems = character.Backpacks
                .Select(b => new BackpackItemDTO
                {
                    amount = b.Amount,
                    itemName = b.Item.Name,
                    itemWeight = b.Item.Weight
                }).ToList(),
            titles = character.CharacterTitles
                .Select(t => new TitleDTO
                {
                    AcquiredAt = t.AcquiredAt,
                    Name = t.Title.Name
                }).ToList()
        };
        return characterDto;
    }

    public async Task<bool> DoesCharacterExist(int characterId)
    {
        return await _context.Characters.AnyAsync(c => c.Id == characterId);
    }

    public async Task<bool> DoesItemExist(int itemId)
    {
       return await _context.Items.AnyAsync(c => c.Id == itemId);
    }

    private async Task<int> GetItemsWeightSum(List<int> itemIds)
    {
        int sumWeight = 0;
        foreach (var itemId in itemIds)
        {
            var item = await _context.Items.FirstOrDefaultAsync(i => i.Id==itemId);
            sumWeight += item.Weight;
        }

        return sumWeight;
    }
    public async Task<bool> CanCharacterCarryItems(List<int> itemIds, int characterId)
    {
        var character = await _context.Characters.FirstOrDefaultAsync(c => c.Id==characterId);
        var characterCurrentWeight = character.CurrentWeight;
        var characterMaxWeight = character.MaxWeight;
        var itemsWeightSum = await GetItemsWeightSum(itemIds);
        

        return characterCurrentWeight + itemsWeightSum <= characterMaxWeight;
    }

    public async Task AddItemsWeightToCharacter(int characterId, List<int> itemIds)
    {
        var itemsWeightSum = await GetItemsWeightSum(itemIds);
        var character = await _context.Characters.FirstOrDefaultAsync(c => c.Id==characterId);
        character.CurrentWeight += itemsWeightSum;
        await _context.SaveChangesAsync();
    }

    public async Task<List<Backpack>> AddItemsToCharacterBackpack(int characterId, List<int> itemIds)
    {
        List<Backpack> result = new List<Backpack>();
        foreach (var itemId in itemIds)
        {
            var backpack = new Backpack
            {
                CharacterId = characterId,
                ItemId = itemId,
                Amount = 1
            };
            result.Add(backpack);
            await _context.Backpacks.AddAsync(backpack);
        }

        await _context.SaveChangesAsync();

        return result;
    }
}