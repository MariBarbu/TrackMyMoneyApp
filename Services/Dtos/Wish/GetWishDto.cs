using DataLayer.Entities.Eums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Dtos.Wish
{
    public class GetWishDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public bool Available { get; set; }
        public WishStatus Status { get; set; }
    }
}
