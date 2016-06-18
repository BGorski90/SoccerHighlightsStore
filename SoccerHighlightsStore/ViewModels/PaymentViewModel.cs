using SoccerHighlightsStore.BusinessLayer.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SoccerHighlightsStore.Storefront.ViewModels
{
    public class PaymentViewModel
    {
        public IEnumerable<Video> Cart { get; set; }
        [DisplayFormat(DataFormatString = "{0:C}")]
        public decimal OrderValue { get; set; }
        [Required(ErrorMessage = "Please enter your first name")]
        [Display(Name = "First name")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Please enter your second name")]
        [Display(Name = "Second name")]
        public string SecondName { get; set; }
        [Required(ErrorMessage = "Please enter your e-mail address")]
        [Display(Name = "E-mail address")]
        [EmailAddress(ErrorMessage = "Please correct your e-mail format")]
        public string EmailAddress { get; set; }
        [Required(ErrorMessage = "Please enter your country")]
        public string Country { get; set; } //enum
        [Required(ErrorMessage = "Please enter your credit card number")]
        [RegularExpression("^[0-9]{8,}", ErrorMessage = "Please correct your credit card number format")]
        [Display(Name = "Credit card number")]
        public string CreditCardNumber { get; set; }
        [Required(ErrorMessage = "Please enter your CVV2 number (3 digits at the back of your card)")]
        [RegularExpression("^[0-9]{3}", ErrorMessage = "Please correct your CVV2 format")]
        [Display(Name = "CVV2 number")]
        public string CVV2 { get; set; }
        [Required(ErrorMessage = "Please enter your card's expiration date")]
        [DataType(DataType.Date)]
        [Display(Name = "Credit card valid until")]
        public DateTime CreditCardValidUntil  { get; set; }
    }
}