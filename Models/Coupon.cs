using System.ComponentModel.DataAnnotations;

namespace CodeTest.Models
{
    public class Coupon
    {
        [ConcurrencyCheck]
        [Key]
        public int Id { get; set; }
        
        public Guid CouponCode { get; set; }

        public string CouponName { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public String Image { get; set; }

        public decimal DiscountAmount { get; set; }

        public int AvailableQty { get; set; }
        public int RequirePoints { get; set; }      
        public bool IsActive { get; set; }

        public bool IsDeleted { get; set; }
    }
}
