using CodeTest.IServices;
using CodeTest.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using TestCode.DataAccess;

namespace CodeTest.Services
{
    public class POSService : IPOSService
    {
        private readonly ItemDbContext _context;

      
        public POSService(ItemDbContext context)
        {
            _context = context;
        }

        public async Task<List<Purchase>> GetAllPurchases()
        {
            var purchaseList = await _context.Purchases.AsNoTracking().ToListAsync();
            foreach(var purchase in purchaseList)
            {
                purchase.ItemLists = _context.Items.AsNoTracking().Where(x => x.PurchaseId == purchase.Id).ToList();
            }
            return purchaseList;
        }

        public async Task<MemberDetail> SaveMember(MemberDetail memberDetail)
        {
            var data = await _context.MemberDetails.FirstOrDefaultAsync();
            if (data == null)
            {
                memberDetail.Id = 1;
            }
            else
            {
                memberDetail.Id = _context.MemberDetails.Max(x => x.Id) + 1;
            }

            await _context.MemberDetails.AddAsync(memberDetail);
            await _context.SaveChangesAsync();

            return memberDetail;
        }

        public async Task<Purchase> SavePurchase(Purchase purchase)
        {
            var data = await _context.Purchases.FirstOrDefaultAsync();
            if (data == null)
            {
                purchase.Id = 1;
            }
            else
            {
                purchase.Id = _context.Purchases.Max(x => x.Id) + 1;
            }

            await _context.Purchases.AddAsync(purchase);
            await _context.SaveChangesAsync();

            return purchase;
            //throw new NotImplementedException();
        }


        
    }
}
