using CodeTest.Models;
using Microsoft.AspNetCore.Mvc;

namespace CodeTest.IServices
{
    public interface IMobileService
    {
        Task<string> ExchangePoint(int memberId, int couponId, int requirePoint);
        Task<Byte[]> GenerateQRCode(int memberId);
        Task<List<Coupon>> GetExchangedCouponsList(int memberId);
        Task<List<Coupon>> GetUsedCouponsList(int memberId);
    }
}
