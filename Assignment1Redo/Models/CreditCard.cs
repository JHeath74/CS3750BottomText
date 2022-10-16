using System;
using System.ComponentModel.DataAnnotations;

namespace BottomTextLMS.Models
{
    public class CreditCard
    {
        [Key]
        public int ID { get; set; }

        [Required]
        [Display(Name = "Card Number")]
        [StringLength(16, ErrorMessage = "Card Number must be 16 digits.", MinimumLength = 16)]
        public string CreditCardNumber { get; set; }

        [Required]
        [Display(Name = "Name of Card Holder")]
        public string HolderName { get; set; }

        [Required]
        [Display(Name = "Expiration Date")]
        public DateTime ExpDate { get; set; }

        [Required]
        [RegularExpression(@"[0-9][0-9][0-9]", ErrorMessage = "CVC code must be 3 digits.")]
        public int CVC { get; set; }

        public int AmountToPay { get; set; }
    }
}