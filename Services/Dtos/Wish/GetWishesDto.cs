using DataLayer.Entities.Eums;
using System;
using System.Collections.Generic;

namespace Services.Dtos.Wish
{
    public class GetWishesDto
    {
        public IList<GetWishDto> Wishes { get; set; } = new List<GetWishDto>();
    }
}
