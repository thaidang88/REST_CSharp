using System;
using System.ComponentModel.DataAnnotations;

namespace Catalog.DTOS
{
    public record CreatedItemDto
    {
        [Required]
        public String Name { get; init; }
        [Required]
        [Range(1,1000)]
        public decimal Price { get; init; }

        

    }
}