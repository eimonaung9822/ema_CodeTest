using AutoMapper;
using BitMiracle.LibTiff.Classic;
using CodeTest.IServices;
using CodeTest.Models;
using IronBarCode;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using TestCode.DataAccess;
using TestCode.DTO;
using TestCode.Models;

namespace CodeTest.Services
{
    public class MobileService : IMobileService
    {
        private readonly ItemDbContext _context;

        private IDistributedCache _distributedCache;

        private readonly IMapper _mapper;

        public MobileService(ItemDbContext context, IDistributedCache distributedCache, IMapper mapper)
        {
            _context = context;
            _distributedCache = distributedCache;
            _mapper = mapper;
        }
        public async Task<string> ExchangePoint(int memberId, int couponId, int requirePoint)
        {
            MyCouponDTO myCouponDTO = new MyCouponDTO();
            if (memberId != 0 && couponId != 0)
            {
                var memberDetail = _context.MemberDetails.Where(x => x.Id == memberId).FirstOrDefault();

                if (requirePoint <= memberDetail.TotalPoint)
                {
                    memberDetail.TotalPoint -= requirePoint;
                    _distributedCache.SetString("Member" + memberDetail.Id, memberDetail.TotalPoint + "");

                    myCouponDTO.CouponId = couponId;
                    myCouponDTO.MemberId = memberId;

                    MyCoupon myCoupon = _mapper.Map<MyCoupon>(myCouponDTO);

                    await _context.MyCoupon.AddAsync(myCoupon);

                    _context.MemberDetails.Update(memberDetail);

                    await _context.SaveChangesAsync();

                   
                }
                else
                {
                    return "Sorry, Not Enough Points!!!!";
                }
            }
            return null;
        }

        public async Task<List<Coupon>> GetExchangedCouponsList(int memberId)
        {
            var myCoupons = await _context.MyCoupon.AsNoTracking().Select(x=>x.CouponId).ToListAsync();

            var couponsList = _context.Coupons.AsNoTracking().Where(x=>myCoupons.Contains(x.Id)).ToList();
            
            return couponsList;
        }

        public async Task<List<Coupon>> GetUsedCouponsList(int memberId)
        {
            var purchase = await _context.Purchases.AsNoTracking().Select(x => x.CouponId).ToListAsync();

            var couponsList = _context.Coupons.AsNoTracking().Where(x => purchase.Contains(x.Id)).ToList();

            return couponsList;
        }

        public async Task<Byte[]> GenerateQRCode(int memberId)
        {
            QRCodeWriter.CreateQrCode(memberId.ToString(), 500, QRCodeWriter.QrErrorCorrectionLevel.Medium).SaveAsPng("MyQR.png");

            Byte[] b = System.IO.File.ReadAllBytes("MyQR.png");   // You can use your own method over here.
            return b;

        }
    }
}
