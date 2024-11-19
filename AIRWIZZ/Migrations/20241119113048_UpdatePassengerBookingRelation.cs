using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AIRWIZZ.Migrations
{
    /// <inheritdoc />
    public partial class UpdatePassengerBookingRelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Arrivals_Flights_Flight_Id",
                table: "Arrivals");

            migrationBuilder.DropForeignKey(
                name: "FK_Bookings_Arrivals_Arrival_id",
                table: "Bookings");

            migrationBuilder.DropForeignKey(
                name: "FK_Bookings_Users_User_id",
                table: "Bookings");

            migrationBuilder.DropForeignKey(
                name: "FK_Departures_Flights_Flight_Id",
                table: "Departures");

            migrationBuilder.DropForeignKey(
                name: "FK_Passengers_Users_User_Id",
                table: "Passengers");

            migrationBuilder.DropForeignKey(
                name: "FK_payments_Bookings_Booking_Id",
                table: "payments");

            migrationBuilder.DropForeignKey(
                name: "FK_payments_Users_User_Id",
                table: "payments");

            migrationBuilder.DropForeignKey(
                name: "FK_SeatPlans_Flights_Flight_Id",
                table: "SeatPlans");

            migrationBuilder.DropIndex(
                name: "IX_Passengers_User_Id",
                table: "Passengers");

            migrationBuilder.DropColumn(
                name: "User_Id",
                table: "Passengers");

            migrationBuilder.RenameColumn(
                name: "password",
                table: "Users",
                newName: "Password");

            migrationBuilder.RenameColumn(
                name: "email",
                table: "Users",
                newName: "Email");

            migrationBuilder.RenameColumn(
                name: "user_name",
                table: "Users",
                newName: "UserName");

            migrationBuilder.RenameColumn(
                name: "currency_preference",
                table: "Users",
                newName: "Role");

            migrationBuilder.RenameColumn(
                name: "User_role",
                table: "Users",
                newName: "CurrencyPreference");

            migrationBuilder.RenameColumn(
                name: "Date_joined",
                table: "Users",
                newName: "DateJoined");

            migrationBuilder.RenameColumn(
                name: "user_id",
                table: "Users",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "Seat_status",
                table: "SeatPlans",
                newName: "IsAvailable");

            migrationBuilder.RenameColumn(
                name: "Seat_Number",
                table: "SeatPlans",
                newName: "SeatNumber");

            migrationBuilder.RenameColumn(
                name: "Seat_Class_type",
                table: "SeatPlans",
                newName: "SeatClassType");

            migrationBuilder.RenameColumn(
                name: "Flight_Id",
                table: "SeatPlans",
                newName: "FlightId");

            migrationBuilder.RenameColumn(
                name: "Seat_Id",
                table: "SeatPlans",
                newName: "SeatId");

            migrationBuilder.RenameIndex(
                name: "IX_SeatPlans_Flight_Id",
                table: "SeatPlans",
                newName: "IX_SeatPlans_FlightId");

            migrationBuilder.RenameColumn(
                name: "User_Id",
                table: "payments",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "Payment_status_result",
                table: "payments",
                newName: "PaymentStatus");

            migrationBuilder.RenameColumn(
                name: "Payment_Method_type",
                table: "payments",
                newName: "PaymentMethodType");

            migrationBuilder.RenameColumn(
                name: "Payment_Date",
                table: "payments",
                newName: "PaymentDate");

            migrationBuilder.RenameColumn(
                name: "Currency_Type",
                table: "payments",
                newName: "CurrencyType");

            migrationBuilder.RenameColumn(
                name: "Booking_Id",
                table: "payments",
                newName: "BookingId");

            migrationBuilder.RenameColumn(
                name: "Payment_Id",
                table: "payments",
                newName: "PaymentId");

            migrationBuilder.RenameIndex(
                name: "IX_payments_User_Id",
                table: "payments",
                newName: "IX_payments_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_payments_Booking_Id",
                table: "payments",
                newName: "IX_payments_BookingId");

            migrationBuilder.RenameColumn(
                name: "Last_name",
                table: "Passengers",
                newName: "Last_Name");

            migrationBuilder.RenameColumn(
                name: "First_name",
                table: "Passengers",
                newName: "First_Name");

            migrationBuilder.RenameColumn(
                name: "Total_Price",
                table: "Flights",
                newName: "TotalPrice");

            migrationBuilder.RenameColumn(
                name: "Flight_Number",
                table: "Flights",
                newName: "FlightNumber");

            migrationBuilder.RenameColumn(
                name: "Flight_Id",
                table: "Flights",
                newName: "FlightId");

            migrationBuilder.RenameColumn(
                name: "Flight_Id",
                table: "Departures",
                newName: "FlightId");

            migrationBuilder.RenameColumn(
                name: "Departure_Time",
                table: "Departures",
                newName: "DepartureTime");

            migrationBuilder.RenameColumn(
                name: "Departure_City",
                table: "Departures",
                newName: "DepartureCity");

            migrationBuilder.RenameColumn(
                name: "Departure_Id",
                table: "Departures",
                newName: "DepartureId");

            migrationBuilder.RenameIndex(
                name: "IX_Departures_Flight_Id",
                table: "Departures",
                newName: "IX_Departures_FlightId");

            migrationBuilder.RenameColumn(
                name: "Last_Updated",
                table: "CurrencyConversions",
                newName: "LastUpdated");

            migrationBuilder.RenameColumn(
                name: "Currency_Rate",
                table: "CurrencyConversions",
                newName: "ConversionRate");

            migrationBuilder.RenameColumn(
                name: "Currency_Code",
                table: "CurrencyConversions",
                newName: "CurrencyId");

            migrationBuilder.RenameColumn(
                name: "User_id",
                table: "Bookings",
                newName: "User_Id");

            migrationBuilder.RenameColumn(
                name: "Book_status_result",
                table: "Bookings",
                newName: "Book_Status_Result");

            migrationBuilder.RenameColumn(
                name: "Arrival_id",
                table: "Bookings",
                newName: "Arrival_Id");

            migrationBuilder.RenameIndex(
                name: "IX_Bookings_User_id",
                table: "Bookings",
                newName: "IX_Bookings_User_Id");

            migrationBuilder.RenameIndex(
                name: "IX_Bookings_Arrival_id",
                table: "Bookings",
                newName: "IX_Bookings_Arrival_Id");

            migrationBuilder.RenameColumn(
                name: "Flight_Id",
                table: "Arrivals",
                newName: "FlightId");

            migrationBuilder.RenameColumn(
                name: "Arrival_Time",
                table: "Arrivals",
                newName: "ArrivalTime");

            migrationBuilder.RenameColumn(
                name: "Arrival_City",
                table: "Arrivals",
                newName: "ArrivalCity");

            migrationBuilder.RenameColumn(
                name: "Arrival_Id",
                table: "Arrivals",
                newName: "ArrivalId");

            migrationBuilder.RenameIndex(
                name: "IX_Arrivals_Flight_Id",
                table: "Arrivals",
                newName: "IX_Arrivals_FlightId");

            migrationBuilder.AlterColumn<string>(
                name: "Password",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Arrivals_Flights_FlightId",
                table: "Arrivals",
                column: "FlightId",
                principalTable: "Flights",
                principalColumn: "FlightId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Bookings_Arrivals_Arrival_Id",
                table: "Bookings",
                column: "Arrival_Id",
                principalTable: "Arrivals",
                principalColumn: "ArrivalId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Bookings_Users_User_Id",
                table: "Bookings",
                column: "User_Id",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Departures_Flights_FlightId",
                table: "Departures",
                column: "FlightId",
                principalTable: "Flights",
                principalColumn: "FlightId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_payments_Bookings_BookingId",
                table: "payments",
                column: "BookingId",
                principalTable: "Bookings",
                principalColumn: "Booking_Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_payments_Users_UserId",
                table: "payments",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SeatPlans_Flights_FlightId",
                table: "SeatPlans",
                column: "FlightId",
                principalTable: "Flights",
                principalColumn: "FlightId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Arrivals_Flights_FlightId",
                table: "Arrivals");

            migrationBuilder.DropForeignKey(
                name: "FK_Bookings_Arrivals_Arrival_Id",
                table: "Bookings");

            migrationBuilder.DropForeignKey(
                name: "FK_Bookings_Users_User_Id",
                table: "Bookings");

            migrationBuilder.DropForeignKey(
                name: "FK_Departures_Flights_FlightId",
                table: "Departures");

            migrationBuilder.DropForeignKey(
                name: "FK_payments_Bookings_BookingId",
                table: "payments");

            migrationBuilder.DropForeignKey(
                name: "FK_payments_Users_UserId",
                table: "payments");

            migrationBuilder.DropForeignKey(
                name: "FK_SeatPlans_Flights_FlightId",
                table: "SeatPlans");

            migrationBuilder.RenameColumn(
                name: "Password",
                table: "Users",
                newName: "password");

            migrationBuilder.RenameColumn(
                name: "Email",
                table: "Users",
                newName: "email");

            migrationBuilder.RenameColumn(
                name: "UserName",
                table: "Users",
                newName: "user_name");

            migrationBuilder.RenameColumn(
                name: "Role",
                table: "Users",
                newName: "currency_preference");

            migrationBuilder.RenameColumn(
                name: "DateJoined",
                table: "Users",
                newName: "Date_joined");

            migrationBuilder.RenameColumn(
                name: "CurrencyPreference",
                table: "Users",
                newName: "User_role");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Users",
                newName: "user_id");

            migrationBuilder.RenameColumn(
                name: "SeatNumber",
                table: "SeatPlans",
                newName: "Seat_Number");

            migrationBuilder.RenameColumn(
                name: "SeatClassType",
                table: "SeatPlans",
                newName: "Seat_Class_type");

            migrationBuilder.RenameColumn(
                name: "IsAvailable",
                table: "SeatPlans",
                newName: "Seat_status");

            migrationBuilder.RenameColumn(
                name: "FlightId",
                table: "SeatPlans",
                newName: "Flight_Id");

            migrationBuilder.RenameColumn(
                name: "SeatId",
                table: "SeatPlans",
                newName: "Seat_Id");

            migrationBuilder.RenameIndex(
                name: "IX_SeatPlans_FlightId",
                table: "SeatPlans",
                newName: "IX_SeatPlans_Flight_Id");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "payments",
                newName: "User_Id");

            migrationBuilder.RenameColumn(
                name: "PaymentStatus",
                table: "payments",
                newName: "Payment_status_result");

            migrationBuilder.RenameColumn(
                name: "PaymentMethodType",
                table: "payments",
                newName: "Payment_Method_type");

            migrationBuilder.RenameColumn(
                name: "PaymentDate",
                table: "payments",
                newName: "Payment_Date");

            migrationBuilder.RenameColumn(
                name: "CurrencyType",
                table: "payments",
                newName: "Currency_Type");

            migrationBuilder.RenameColumn(
                name: "BookingId",
                table: "payments",
                newName: "Booking_Id");

            migrationBuilder.RenameColumn(
                name: "PaymentId",
                table: "payments",
                newName: "Payment_Id");

            migrationBuilder.RenameIndex(
                name: "IX_payments_UserId",
                table: "payments",
                newName: "IX_payments_User_Id");

            migrationBuilder.RenameIndex(
                name: "IX_payments_BookingId",
                table: "payments",
                newName: "IX_payments_Booking_Id");

            migrationBuilder.RenameColumn(
                name: "Last_Name",
                table: "Passengers",
                newName: "Last_name");

            migrationBuilder.RenameColumn(
                name: "First_Name",
                table: "Passengers",
                newName: "First_name");

            migrationBuilder.RenameColumn(
                name: "TotalPrice",
                table: "Flights",
                newName: "Total_Price");

            migrationBuilder.RenameColumn(
                name: "FlightNumber",
                table: "Flights",
                newName: "Flight_Number");

            migrationBuilder.RenameColumn(
                name: "FlightId",
                table: "Flights",
                newName: "Flight_Id");

            migrationBuilder.RenameColumn(
                name: "FlightId",
                table: "Departures",
                newName: "Flight_Id");

            migrationBuilder.RenameColumn(
                name: "DepartureTime",
                table: "Departures",
                newName: "Departure_Time");

            migrationBuilder.RenameColumn(
                name: "DepartureCity",
                table: "Departures",
                newName: "Departure_City");

            migrationBuilder.RenameColumn(
                name: "DepartureId",
                table: "Departures",
                newName: "Departure_Id");

            migrationBuilder.RenameIndex(
                name: "IX_Departures_FlightId",
                table: "Departures",
                newName: "IX_Departures_Flight_Id");

            migrationBuilder.RenameColumn(
                name: "LastUpdated",
                table: "CurrencyConversions",
                newName: "Last_Updated");

            migrationBuilder.RenameColumn(
                name: "ConversionRate",
                table: "CurrencyConversions",
                newName: "Currency_Rate");

            migrationBuilder.RenameColumn(
                name: "CurrencyId",
                table: "CurrencyConversions",
                newName: "Currency_Code");

            migrationBuilder.RenameColumn(
                name: "User_Id",
                table: "Bookings",
                newName: "User_id");

            migrationBuilder.RenameColumn(
                name: "Book_Status_Result",
                table: "Bookings",
                newName: "Book_status_result");

            migrationBuilder.RenameColumn(
                name: "Arrival_Id",
                table: "Bookings",
                newName: "Arrival_id");

            migrationBuilder.RenameIndex(
                name: "IX_Bookings_User_Id",
                table: "Bookings",
                newName: "IX_Bookings_User_id");

            migrationBuilder.RenameIndex(
                name: "IX_Bookings_Arrival_Id",
                table: "Bookings",
                newName: "IX_Bookings_Arrival_id");

            migrationBuilder.RenameColumn(
                name: "FlightId",
                table: "Arrivals",
                newName: "Flight_Id");

            migrationBuilder.RenameColumn(
                name: "ArrivalTime",
                table: "Arrivals",
                newName: "Arrival_Time");

            migrationBuilder.RenameColumn(
                name: "ArrivalCity",
                table: "Arrivals",
                newName: "Arrival_City");

            migrationBuilder.RenameColumn(
                name: "ArrivalId",
                table: "Arrivals",
                newName: "Arrival_Id");

            migrationBuilder.RenameIndex(
                name: "IX_Arrivals_FlightId",
                table: "Arrivals",
                newName: "IX_Arrivals_Flight_Id");

            migrationBuilder.AlterColumn<string>(
                name: "password",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "email",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<int>(
                name: "User_Id",
                table: "Passengers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Passengers_User_Id",
                table: "Passengers",
                column: "User_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Arrivals_Flights_Flight_Id",
                table: "Arrivals",
                column: "Flight_Id",
                principalTable: "Flights",
                principalColumn: "Flight_Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Bookings_Arrivals_Arrival_id",
                table: "Bookings",
                column: "Arrival_id",
                principalTable: "Arrivals",
                principalColumn: "Arrival_Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Bookings_Users_User_id",
                table: "Bookings",
                column: "User_id",
                principalTable: "Users",
                principalColumn: "user_id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Departures_Flights_Flight_Id",
                table: "Departures",
                column: "Flight_Id",
                principalTable: "Flights",
                principalColumn: "Flight_Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Passengers_Users_User_Id",
                table: "Passengers",
                column: "User_Id",
                principalTable: "Users",
                principalColumn: "user_id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_payments_Bookings_Booking_Id",
                table: "payments",
                column: "Booking_Id",
                principalTable: "Bookings",
                principalColumn: "Booking_Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_payments_Users_User_Id",
                table: "payments",
                column: "User_Id",
                principalTable: "Users",
                principalColumn: "user_id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SeatPlans_Flights_Flight_Id",
                table: "SeatPlans",
                column: "Flight_Id",
                principalTable: "Flights",
                principalColumn: "Flight_Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
