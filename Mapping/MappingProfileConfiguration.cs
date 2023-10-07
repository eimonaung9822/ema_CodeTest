using AutoMapper;
using CodeTest.DTO;
using CodeTest.Models;
using TestCode.DTO;
using TestCode.Models;

namespace CodeTest.Mapping
{
    public class MappingProfileConfiguration:Profile
    {
        public MappingProfileConfiguration()
        {
            CreateMap<Purchase, PurchaseDTO>().ReverseMap();
            CreateMap<Item, ItemDTO>().ReverseMap();
            CreateMap<MemberDetail, MemberDetailDTO>().ReverseMap();
            CreateMap<MyCoupon, MyCouponDTO>().ReverseMap();
        }
        
    }
}
