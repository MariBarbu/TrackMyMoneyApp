using DataLayer.Entities.Eums;
using System;
using System.Collections.Generic;

namespace Services.Dtos.Wish
{
    public class GetWishesDto
    {
        public IList<GetWishDto> Wishes = new List<GetWishDto>();
    }
}
