using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AIRWIZZ.Migrations
{
    /// <inheritdoc />
    public partial class EntitiesMg : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
            name: "CurrencyConversions",
            columns: table => new
            {
                Currency_Code = table.Column<int>(type: "int", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                ConversionRate = table.Column<float>(type: "real", nullable: false),
                LastUpdated = table.Column<DateTime>(type: "datetime2", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_CurrencyConversions", x => x.Currency_Code);
            });

            migrationBuilder.CreateTable(
                name: "Flights",
                columns: table => new
                {
                    Flight_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FlightNumber = table.Column<int>(type: "int", nullable: false),
                    Airline = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TotalPrice = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Flights", x => x.Flight_Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    user_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    user_name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    User_role = table.Column<int>(type: "int", nullable: false),
                    currency_preference = table.Column<int>(type: "int", nullable: false),
                    Date_joined = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.user_id);
                });

            migrationBuilder.CreateTable(
                name: "Arrivals",
                columns: table => new
                {
                    Arrival_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ArrivalTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ArrivalCity = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Duration = table.Column<float>(type: "real", nullable: false),
                    FlightId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Arrivals", x => x.Arrival_Id);
                    table.ForeignKey(
                        name: "FK_Arrivals_Flights_FlightId",
                        column: x => x.FlightId,
                        principalTable: "Flights",
                        principalColumn: "Flight_Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Departures",
                columns: table => new
                {
                    Departure_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DepartureCity = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DepartureTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Duration = table.Column<float>(type: "real", nullable: false),
                    Price = table.Column<float>(type: "real", nullable: false),
                    FlightId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Departures", x => x.Departure_id);
                    table.ForeignKey(
                        name: "FK_Departures_Flights_FlightId",
                        column: x => x.FlightId,
                        principalTable: "Flights",
                        principalColumn: "Flight_Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SeatPlans",
                columns: table => new
                {
                    Seat_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SeatNumber = table.Column<int>(type: "int", nullable: false),
                    SeatClassType = table.Column<int>(type: "int", nullable: false),
                    IsAvailable = table.Column<bool>(type: "bit", nullable: false),
                    FlightId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SeatPlans", x => x.Seat_Id);
                    table.ForeignKey(
                        name: "FK_SeatPlans_Flights_FlightId",
                        column: x => x.FlightId,
                        principalTable: "Flights",
                        principalColumn: "Flight_Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
     name: "Bookings",
     columns: table => new
     {
         Booking_Id = table.Column<int>(type: "int", nullable: false)
             .Annotation("SqlServer:Identity", "1, 1"),
         Booking_Date = table.Column<DateTime>(type: "datetime2", nullable: false),
         Book_Status_Result = table.Column<int>(type: "int", nullable: false),
         Passenger_Id = table.Column<int>(type: "int", nullable: false),
         User_Id = table.Column<int>(type: "int", nullable: false),
         Flight_Id = table.Column<int>(type: "int", nullable: false),
         ArrivalId = table.Column<int>(type: "int", nullable: false),
         DepartureId = table.Column<int>(type: "int", nullable: false),
         Seat_Id = table.Column<int>(type: "int", nullable: false)
     },
     constraints: table =>
     {
         table.PrimaryKey("PK_Bookings", x => x.Booking_Id);

         table.ForeignKey(
             name: "FK_Bookings_Arrivals_ArrivalId",
             column: x => x.ArrivalId,
             principalTable: "Arrivals",
             principalColumn: "Arrival_Id",
             onDelete: ReferentialAction.Restrict); // Prevent cascade delete

         table.ForeignKey(
             name: "FK_Bookings_Departures_DepartureId",
             column: x => x.DepartureId,
             principalTable: "Departures",
             principalColumn: "Departure_id",
             onDelete: ReferentialAction.Restrict); // Prevent cascade delete

         table.ForeignKey(
             name: "FK_Bookings_Flights_Flight_Id",
             column: x => x.Flight_Id,
             principalTable: "Flights",
             principalColumn: "Flight_Id",
             onDelete: ReferentialAction.Restrict);

         table.ForeignKey(
             name: "FK_Bookings_SeatPlans_Seat_Id",
             column: x => x.Seat_Id,
             principalTable: "SeatPlans",
             principalColumn: "Seat_Id",
             onDelete: ReferentialAction.Restrict);

         table.ForeignKey(
             name: "FK_Bookings_Users_User_Id",
             column: x => x.User_Id,
             principalTable: "Users",
             principalColumn: "user_id",
             onDelete: ReferentialAction.Restrict);
     });


            migrationBuilder.CreateTable(
                name: "Passengers",
                columns: table => new
                {
                    Passenger_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    First_Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Last_Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Passport_Number = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Date_Of_Birth = table.Column<DateOnly>(type: "date", nullable: false),
                    Nationality = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Passengers", x => x.Passenger_Id);
                });

            migrationBuilder.CreateTable(
                name: "payments",
                columns: table => new
                {
                    Payment_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PaymentDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Amount = table.Column<float>(type: "real", nullable: false),
                    CurrencyType = table.Column<int>(type: "int", nullable: false),
                    PaymentMethodType = table.Column<int>(type: "int", nullable: false),
                    PaymentStatus = table.Column<int>(type: "int", nullable: false),
                    BookingId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_payments", x => x.Payment_Id);
                    table.ForeignKey(
                        name: "FK_payments_Bookings_BookingId",
                        column: x => x.BookingId,
                        principalTable: "Bookings",
                        principalColumn: "Booking_Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_payments_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "user_id",
                        onDelete: ReferentialAction.Cascade);
                });



        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Remove tables in reverse order
            migrationBuilder.DropTable(name: "Payments");
            migrationBuilder.DropTable(name: "Passengers");
            migrationBuilder.DropTable(name: "Bookings");
            migrationBuilder.DropTable(name: "SeatPlans");
            migrationBuilder.DropTable(name: "Departures");
            migrationBuilder.DropTable(name: "Arrivals");
            migrationBuilder.DropTable(name: "Users");
            migrationBuilder.DropTable(name: "Flights");
            migrationBuilder.DropTable(name: "CurrencyConversions");
        }
    }
}
