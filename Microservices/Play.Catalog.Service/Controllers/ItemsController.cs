using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
// using Play.Catalog.Contracts;
// using Play.Catalog.Service.Dtos;
using Play.Catalog.Service.Entities;
using Play.Catalog.Common;

namespace Play.Catalog.Service.Controllers
{
    [ApiController]
    [Route("items")]
    public class ItemsController : ControllerBase
    {
      
        private static readonly List<ItemDto> items= new()
        {
            new ItemDto(Guid.NewGuid(),"dasd","asdas",5, DateTimeOffset.UtcNow),
            new ItemDto(Guid.NewGuid(),"dasddasda","asda",5, DateTimeOffset.UtcNow),
            new ItemDto(Guid.NewGuid(),"sdaxcx","xasadas",5, DateTimeOffset.UtcNow)
        };
        [HttpGet]
        public IEnumerable<ItemDto> GetItems()
        {
            return items;
        }

        // GET /items/{id}
        [HttpGet("{id}")]
        public  ItemDto GetById(Guid id)
        {
            var item =  items.Where(item=>item.Id==id).SingleOrDefault();

            // if (item == null)
            // {
            //     return NotFound();
            // }

            return item;
        }

        // POST /items
        [HttpPost]
        public ActionResult <ItemDto> Post(CreateItemDto createItemDto)
        {
            var item = new ItemDto
            (Guid.NewGuid(),createItemDto.Name,createItemDto.Description,createItemDto.Price,DateTimeOffset.UtcNow);

         

            return CreatedAtAction(nameof(GetById), new { id = item.Id }, item);
        }

        // PUT /items/{id}
        [HttpPut("{id}")]
        public IActionResult Put(Guid id, UpdateItemDto updateItemDto)
        {
           var item =  items.Where(item=>item.Id==id).SingleOrDefault();
           if(item==null)
           {
                return NotFound();
           }
           else
           {

            var updateItem= item with
            {
                Name= updateItemDto.Name,
                Description= updateItemDto.Description,
                Price= updateItemDto.Price,
                // CreatedDate= updateItemDto.CreatedDate
            };
            // item.Name=updateItemDto.Name;
            // item.Description= updateItemDto.Description;
            // item.Price=updateItemDto.Price;
            // item.CreatedDate=updateItemDto.CreatedDate;

            var index= items.FindIndex(item => item.Id==id);
            items[index]=updateItem;
            return NoContent();
           }
        }

        // DELETE /items/{id}
        [HttpDelete("{id}")]
        public  IActionResult Delete(Guid id)
        {
            var index= items.FindIndex(item => item.Id==id);
            if(index<0)
            {
                return NotFound();
            }
         
            items.RemoveAt(index);

            return NoContent();
        }
    }
}