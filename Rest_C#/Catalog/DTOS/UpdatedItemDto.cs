using System;
using System.ComponentModel.DataAnnotations;

namespace Catalog.DTOS
{
    public record UpdatedItemDto
    {
        [Required]
        public String Name { get; init; }
        [Required]
        [Range(1,1000)]
        public decimal Price { get; init; }

        // public DateTimeOffset CreatedDate{get;init;}

    }
}