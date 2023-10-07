using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace CodeTest.Models
{
    public class Purchase
    {
        [ConcurrencyCheck]
        [Key]
        public int Id { get; set; }

        public int CouponId { get; set; }

        public int MemberId { get; set; }

        public int ReceiptNo { get; set; }

        public List<Item> ItemLists { get; set; }

        public string Name { get; set; }

        public string Phno { get; set; }

        public DateTime Date { get; set; }

        public bool IsDeleted { get; set; }
    }


    public class Item
    {
       
        public int Id { get; set; }

        public int PurchaseId { get; set; }
        public bool isNonAlcohol { get; set; }

        public decimal Price { get; set; }

        public int Qty { get; set; }

        public decimal Amount { get; set; }
    }
}
