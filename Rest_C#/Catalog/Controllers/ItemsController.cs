using Microsoft.AspNetCore.Mvc;
using Catalog.Repositories;
using Catalog.Entities;
using System.Collections.Generic;
using System;
using System.Linq;
using Catalog.DTOS;
namespace Catalog.Controllers
{
    [ApiController]
    [Route("items")]
    public class ItemsController:ControllerBase
    {
        private readonly IMemRepositories repositories;
        public ItemsController(InMemRepositories repository)
        {
            repositories= repository;
        }

        [HttpGet]
        public IEnumerable<ItemDto> GetItems()
        {
            var items=repositories.GetItems().Select(item =>item.AsDto());
            // {
            //     Id=item.Id,
            //     Name=item.Name,
            //     Price=item.Price,
            //     CreatedDate=item.CreatedDate
            // });
            return items;
        }

        [HttpGet("{id}")]
        public ActionResult<ItemDto> GetItem(Guid id)
        {
            var item= repositories.GetItem(id).AsDto();
            if(item is null)
            {
                return NotFound();
            }
            
            return item; 
        }


        [HttpPost]
        public ActionResult<ItemDto> CreateItem(CreatedItemDto itemDto)
        {
            Item item = new Item()
            {
                Id=Guid.NewGuid(),
                Name=itemDto.Name,
                Price=itemDto.Price,
                CreatedDate=DateTimeOffset.UtcNow
            };
            repositories.CreateItem(item);
            // return item.AsDto();
            return CreatedAtAction(nameof(GetItem), new {id =item.Id},item.AsDto());
        }

        [HttpPut("{id}")]
        public ActionResult<ItemDto> UpdateItem(Guid id,UpdatedItemDto updatedItemDto)
        {
            // repositories.UpdateItem(item);
            var existing_item =repositories.GetItem(id);
            if(existing_item==null)
            {
                return NotFound();
            }
            var update_item = existing_item with
            {
                Name= updatedItemDto.Name,
                Price= updatedItemDto.Price
            };
            repositories.UpdateItem(update_item);
            // var item = new Item()
            // {
            //     Id=id,
            //     Name=updatedItemDto.Name,
            //     Price=updatedItemDto.Price,
            //     CreatedDate=updatedItemDto.CreatedDate
            // };
            // repositories.UpdateItem(item);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteItem(Guid id)
        {
            var existing_item= repositories.GetItem(id);
            if(existing_item==null)
            {
                return NotFound();
            }
            else
            {
            repositories.RemoveItem(id);
            }
            return NoContent();
        }
    }
} 