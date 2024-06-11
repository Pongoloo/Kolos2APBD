using System.Transactions;
using ExampleTest2.DTOs;
using ExampleTest2.Models;
using ExampleTest2.Services;
using Microsoft.AspNetCore.Mvc;

namespace ExampleTest2.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CharactersController : ControllerBase
{
    private readonly IDbService _dbService;
    public CharactersController(IDbService dbService)
    {
        _dbService = dbService;
    }

    [HttpGet("{characterId:int}")]
    public async Task<IActionResult> GetCharacter(int characterId)
    {
        if (!await _dbService.DoesCharacterExist(characterId))
        {
            return BadRequest("No character with such id exists");
        }

        var characterInfo = await _dbService.GetCharacterInfo(characterId);
        return Ok(characterInfo);
    }

    [HttpPost("{characterId:int}/backpacks")]
    public async Task<IActionResult> AddItems(List<int> itemIds,int characterId)
    {
        if (!await _dbService.DoesCharacterExist(characterId))
        {
            return BadRequest("No such character with id:" + characterId);
        }
        foreach (var itemId in itemIds)
        {
            if (!await _dbService.DoesItemExist(itemId))
            {
                return BadRequest("No item with such id - " + itemId);
            } 
        }

        if (!await _dbService.CanCharacterCarryItems(itemIds, characterId))
        {
            return BadRequest("Character of id:" + characterId + " can not carry that many items");
        }

        await _dbService.AddItemsWeightToCharacter( characterId,itemIds);
        var result = await _dbService.AddItemsToCharacterBackpack( characterId,itemIds);
        var backpackToSendDTos = result.Select(b => new BackpackToSendDTo
        {
            CharacterId = b.CharacterId,
            ItemId = b.ItemId,
            Amount = b.Amount
        }).ToList();
        return Created("api/backpacks", backpackToSendDTos);
    }
   
}