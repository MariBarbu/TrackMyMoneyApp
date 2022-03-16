using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Dtos.Category
{
    public class GetCategoriesDto
    {
        public IList<GetCategoryDto> Categories { get; set; } = new List<GetCategoryDto>();
    }
}
