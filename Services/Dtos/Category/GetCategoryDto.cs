using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Dtos.Category
{
    public class GetCategoryDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}
