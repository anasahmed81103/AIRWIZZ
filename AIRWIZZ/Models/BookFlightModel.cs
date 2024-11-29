using AIRWIZZ.Data.enums;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations;

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

		[Required]
		public string? Passport_Number { get; set; }

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
		public int SeatNumber { get; set; }

		[Required]
		public SeatClass SeatClassType { get; set; }

		
		public bool IsAvailable { get; set; } 

		// Additional data for dropdowns
		//public List<SelectListItem>? SeatList { get; set; }
	}


}

