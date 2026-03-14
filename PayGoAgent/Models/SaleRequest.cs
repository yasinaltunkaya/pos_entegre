using System.ComponentModel.DataAnnotations;

namespace PayGoAgent.Models
{
    public class SaleRequest
    {
        [Required]
        [Range(typeof(decimal), "0.01", "999999999")]
        public decimal Amount { get; set; }
    }
}
