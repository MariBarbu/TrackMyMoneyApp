using System;
using System.Collections.Generic;
using System.Text;

namespace XamarinApp.Models
{
    public class Wish
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public bool Available { get; set; }
        public string Status { get; set; }
    }
}
