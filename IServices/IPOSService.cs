using CodeTest.Models;
using Microsoft.Extensions.Caching.Distributed;

namespace CodeTest.IServices
{
    public interface IPOSService
    {
        public Task<Purchase> SavePurchase(Purchase purchase);
        public Task<MemberDetail> SaveMember(MemberDetail memberDetail);
        public Task<List<Purchase>> GetAllPurchases();
    }
}
