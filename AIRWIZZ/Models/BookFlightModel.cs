using AIRWIZZ.Data.enums;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;  // For List

namespace AIRWIZZ.Models
{
    public class BookFlightModel
    {
        // Passed data
        public int Flight_Id { get; set; }
        public int Departure_Id { get; set; }
        public int Arrival_Id { get; set; }

        // Passenger information
        [Required]
        public string? First_Name { get; set; }

        [Required]
        public string? Last_Name { get; set; }

        [Required(ErrorMessage = "Passport number is required.")]
        [StringLength(9, MinimumLength = 6, ErrorMessage = "Passport number must be between 6 and 9 characters.")]
        public string Passport_Number { get; set; }


        [Required]
        public DateOnly Date_Of_Birth { get; set; }

        [Required]
        public string? Nationality { get; set; }

        // Payment information
        public DateTime PaymentDate { get; set; } = DateTime.Now;

        [Required]
        public float Amount { get; set; }

        [Required]
        public Currency CurrencyType { get; set; }

        [Required]
        public PaymentMethod PaymentMethodType { get; set; }

        [Required]
        public PaymentStatus PaymentStatus { get; set; } = PaymentStatus.Pending;

        // Seat information
        [Required]
        [Range(1, 100, ErrorMessage = "Seat number must be between 1 and 100.")]
        public int SeatNumber { get; set; }

        [Required]
        public SeatClass SeatClassType { get; set; }

        // Availability status
        public bool IsAvailable { get; set; }

        // List of booked seat numbers to validate against
        public List<int> BookedSeats { get; set; } = new List<int>();

        [Required(ErrorMessage = "Card number is required.")]
        [CreditCard(ErrorMessage = "Please enter a valid credit card number.")]
        [StringLength(16, MinimumLength = 13, ErrorMessage = "Card number must be between 13 and 16 digits.")]
        public string Card_Number { get; set; }

        [Required(ErrorMessage = "CVV is required.")]
        [RegularExpression(@"^\d{3,4}$", ErrorMessage = "CVV must be 3 or 4 digits.")]
        public string CVV { get; set; }

        // Additional data for dropdowns
        //public List<SelectListItem>? SeatList { get; set; }
    }
}
