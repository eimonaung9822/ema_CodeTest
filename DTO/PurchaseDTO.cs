using CodeTest.Models;

namespace CodeTest.DTO
{
    public class PurchaseDTO
    {
        
        public int Id { get; set; }

        public int CouponId { get; set; }

        public int MemberId { get; set; }

        public int ReceiptNo { get; set; }

        public List<ItemDTO> ItemLists { get; set; }

        public string Name { get; set; }

        public string Phno { get; set; }

        public DateTime Date { get; set; }

        public bool IsDeleted { get; set; }
    }
}
