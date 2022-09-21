using Microsoft.AspNetCore.Mvc;
using Catalog.Repositories;
using Catalog.Entities;
using System.Collections.Generic;
using System;
using System.Linq;
using Catalog.DTOS;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
namespace Catalog.Controllers
{
    [ApiController]
    [Route("itemsmogo")]
    public class ItemsasyncMongoController:ControllerBase
    {
        private readonly IMemRepositoriesAsync repositories;
        private readonly ILogger<ItemsasyncMongoController> logger;
        // public ItemsasyncMongoController(IMemRepositoriesAsync repository)
        // {
        //     // repositories= new InMemRepositoriesAsync();
        //     // repositories= new MongoRepositoriesAsync();
        //     this.repositories=repository;
        // }

        public ItemsasyncMongoController(IMemRepositoriesAsync repository, ILogger<ItemsasyncMongoController> logger)
        {
            // repositories= new InMemRepositoriesAsync();
            // repositories= new MongoRepositoriesAsync();
            this.repositories=repository;
            this.logger=logger;
        }

        [HttpGet]
        public async Task<IEnumerable<ItemDto>> GetItemsAsync()
        {
            var items=(await repositories.GetItemsAsync()).Select(item =>item.AsDto());
            logger.LogInformation($"{DateTime.UtcNow.ToString("hh:mm:ss")}: Retrieved :{items.Count()}");
            return items;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ItemDto>> GetItemAsync(Guid id)
        {
            var item= await repositories.GetItemAsync(id);
            if(item is null)
            {
                return NotFound();
            }              
            
            return item.AsDto(); 
        }


        [HttpPost]
        public async Task<ActionResult<ItemDto>> CreateItemAsync(CreatedItemDto itemDto)
        {
            Item item = new Item()
            {
                Id=Guid.NewGuid(),
                Name=itemDto.Name,
                Price=itemDto.Price,
                CreatedDate=DateTimeOffset.UtcNow
            };
            await repositories.CreateItemAsync(item);
            // return item.AsDto();
            return CreatedAtAction(nameof(GetItemAsync), new {id =item.Id},item.AsDto());
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ItemDto>> UpdateItemAsync(Guid id,UpdatedItemDto updatedItemDto)
        {
            // repositories.UpdateItem(item);
            var existing_item =await repositories.GetItemAsync(id);
            if(existing_item==null)
            {
                return NotFound();
            }
            var update_item = existing_item with
            {
                Name= updatedItemDto.Name,
                Price= updatedItemDto.Price
            };
            await repositories.UpdateItemAsync(update_item);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteItemAsync(Guid id)
        {
            var existing_item= await repositories.GetItemAsync(id);
            if(existing_item==null)
            {
                return NotFound();
            }
            // else
            {
            await repositories.RemoveItemAsync(id);
            }
            return NoContent();
        }
    }
}  