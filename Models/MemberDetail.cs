using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace CodeTest.Models
{
    public class MemberDetail
    {
        [ConcurrencyCheck]
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public string MobileNo { get; set; }

        public int TotalPoint { get; set; }


        public decimal TotalPurchaseAmount { get; set; }


       

    }
}
