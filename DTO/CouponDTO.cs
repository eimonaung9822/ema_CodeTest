using System.ComponentModel.DataAnnotations;

namespace CodeTest.DTO
{
    public class CouponDTO
    {
        [Key]
        public int Id { get; set; }

        public Guid CouponCode { get; set; }

        public string CouponName { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public String Image { get; set; }

        public decimal DiscountAmount { get; set; }

        public int AvailableQty { get; set; }
        public int RequirPoints { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
    }
}
