using ExampleTest2.DTOs;
using ExampleTest2.Models;

namespace ExampleTest2.Services;

public interface IDbService
{
   public Task<CharacterDTO> GetCharacterInfo(int characterId);
   public Task<bool> DoesCharacterExist(int characterId);
   public Task<bool> DoesItemExist(int itemId);
   public Task<bool> CanCharacterCarryItems(List<int> itemIds, int characterId);
   public Task AddItemsWeightToCharacter(int characterId, List<int> itemIds);
   public Task<List<Backpack>> AddItemsToCharacterBackpack(int characterId, List<int> itemIds);
}