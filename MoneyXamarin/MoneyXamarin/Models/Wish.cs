using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace MoneyXamarin.Models
{
    public class Wish
    {
        [JsonPropertyName("id")]
        public Guid Id { get; set; }
        [JsonPropertyName("createdAt")]
        public DateTime CreatedAt { get; set; }
        [JsonPropertyName("name")]
        public string Name { get; set; }
        [JsonPropertyName("description")]
        public string Description { get; set; }
        [JsonPropertyName("price")]
        public decimal Price { get; set; }
        //public WishStatus Status { get; set; } = WishStatus.Active;
        [JsonPropertyName("moneyUserId")]
        public Guid MoneyUserId { get; set; }
        //public MoneyUser MoneyUser { get; set; }
    }
}
