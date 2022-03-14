using DataLayer.Entities.Eums;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataLayer.Entities
{
    public class Wish : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public WishStatus Status { get; set; } = WishStatus.Active;
        public Guid MoneyUserId { get; set; }
        public MoneyUser MoneyUser { get; set; }
    }
}
