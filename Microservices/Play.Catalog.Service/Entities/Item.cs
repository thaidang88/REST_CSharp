using System;
using Play.Catalog.Common;
using Play.Catalog.Service;

namespace Play.Catalog.Service.Entities
{
    public class Item : IEntity
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        public DateTimeOffset CreatedDate { get; set; }

        public ItemDto AsDto()
        {
            return new ItemDto(this.Id, this.Name, this.Description, this.Price, this.CreatedDate);
        }
    }
}


