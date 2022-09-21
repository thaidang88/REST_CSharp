using Catalog.DTOS;
using Catalog.Entities;
namespace Catalog{
    public static class Extensions
    {
        public static ItemDto AsDto(this Item item)
        {
            if(item==null)
            {
                return null;
            }
            return new ItemDto
            {
                Id=item.Id,
                Name=item.Name,
                Price=item.Price,
                CreatedDate=item.CreatedDate
            };
        }
    }
}