﻿// <auto-generated />
using System;
using AIRWIZZ.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace AIRWIZZ.Migrations
{
    [DbContext(typeof(AirwizzContext))]
    partial class AirwizzContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("AIRWIZZ.Data.Entities.Arrival", b =>
                {
                    b.Property<int>("Arrival_Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Arrival_Id"));

                    b.Property<string>("Arrival_City")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Arrival_Time")
                        .HasColumnType("datetime2");

                    b.Property<float>("Duration")
                        .HasColumnType("real");

                    b.Property<int>("Flight_Id")
                        .HasColumnType("int");

                    b.HasKey("Arrival_Id");

                    b.HasIndex("Flight_Id");

                    b.ToTable("Arrivals");
                });

            modelBuilder.Entity("AIRWIZZ.Data.Entities.Booking", b =>
                {
                    b.Property<int>("Booking_Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Booking_Id"));

                    b.Property<int>("Arrival_id")
                        .HasColumnType("int");

                    b.Property<int>("Book_status_result")
                        .HasColumnType("int");

                    b.Property<DateTime>("Booking_Date")
                        .HasColumnType("datetime2");

                    b.Property<int>("Departure_Id")
                        .HasColumnType("int");

                    b.Property<int>("Flight_Id")
                        .HasColumnType("int");

                    b.Property<int>("Passenger_Id")
                        .HasColumnType("int");

                    b.Property<int>("Seat_Id")
                        .HasColumnType("int");

                    b.Property<int>("User_id")
                        .HasColumnType("int");

                    b.HasKey("Booking_Id");

                    b.HasIndex("Arrival_id");

                    b.HasIndex("Departure_Id");

                    b.HasIndex("Flight_Id");

                    b.HasIndex("Passenger_Id");

                    b.HasIndex("Seat_Id");

                    b.HasIndex("User_id");

                    b.ToTable("Bookings");
                });

            modelBuilder.Entity("AIRWIZZ.Data.Entities.CurrencyConversion", b =>
                {
                    b.Property<int>("Currency_Code")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Currency_Code"));

                    b.Property<float>("Currency_Rate")
                        .HasColumnType("real");

                    b.Property<DateTime>("Last_Updated")
                        .HasColumnType("datetime2");

                    b.HasKey("Currency_Code");

                    b.ToTable("CurrencyConversions");
                });

            modelBuilder.Entity("AIRWIZZ.Data.Entities.Departure", b =>
                {
                    b.Property<int>("Departure_Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Departure_Id"));

                    b.Property<string>("Departure_City")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Departure_Time")
                        .HasColumnType("datetime2");

                    b.Property<float>("Duration")
                        .HasColumnType("real");

                    b.Property<int>("Flight_Id")
                        .HasColumnType("int");

                    b.Property<float>("Price")
                        .HasColumnType("real");

                    b.HasKey("Departure_Id");

                    b.HasIndex("Flight_Id");

                    b.ToTable("Departures");
                });

            modelBuilder.Entity("AIRWIZZ.Data.Entities.Flight", b =>
                {
                    b.Property<int>("Flight_Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Flight_Id"));

                    b.Property<string>("Airline")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Flight_Number")
                        .HasColumnType("int");

                    b.Property<float>("Total_Price")
                        .HasColumnType("real");

                    b.HasKey("Flight_Id");

                    b.ToTable("Flights");
                });

            modelBuilder.Entity("AIRWIZZ.Data.Entities.Passenger", b =>
                {
                    b.Property<int>("Passenger_Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Passenger_Id"));

                    b.Property<DateOnly>("Date_Of_Birth")
                        .HasColumnType("date");

                    b.Property<string>("First_name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Last_name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nationality")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Passport_Number")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("User_Id")
                        .HasColumnType("int");

                    b.HasKey("Passenger_Id");

                    b.HasIndex("User_Id");

                    b.ToTable("Passengers");
                });

            modelBuilder.Entity("AIRWIZZ.Data.Entities.Payment", b =>
                {
                    b.Property<int>("Payment_Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Payment_Id"));

                    b.Property<float>("Amount")
                        .HasColumnType("real");

                    b.Property<int>("Booking_Id")
                        .HasColumnType("int");

                    b.Property<int>("Currency_Type")
                        .HasColumnType("int");

                    b.Property<DateTime>("Payment_Date")
                        .HasColumnType("datetime2");

                    b.Property<int>("Payment_Method_type")
                        .HasColumnType("int");

                    b.Property<int>("Payment_status_result")
                        .HasColumnType("int");

                    b.Property<int>("User_Id")
                        .HasColumnType("int");

                    b.HasKey("Payment_Id");

                    b.HasIndex("Booking_Id")
                        .IsUnique();

                    b.HasIndex("User_Id");

                    b.ToTable("payments");
                });

            modelBuilder.Entity("AIRWIZZ.Data.Entities.SeatPlan", b =>
                {
                    b.Property<int>("Seat_Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Seat_Id"));

                    b.Property<int>("Flight_Id")
                        .HasColumnType("int");

                    b.Property<int>("Seat_Class_type")
                        .HasColumnType("int");

                    b.Property<int>("Seat_Number")
                        .HasColumnType("int");

                    b.Property<bool>("Seat_status")
                        .HasColumnType("bit");

                    b.HasKey("Seat_Id");

                    b.HasIndex("Flight_Id");

                    b.ToTable("SeatPlans");
                });

            modelBuilder.Entity("AIRWIZZ.Data.Entities.User", b =>
                {
                    b.Property<int>("user_id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("user_id"));

                    b.Property<DateTime?>("Date_joined")
                        .HasColumnType("datetime2");

                    b.Property<int>("User_role")
                        .HasColumnType("int");

                    b.Property<int>("currency_preference")
                        .HasColumnType("int");

                    b.Property<string>("email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("password")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("user_name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("user_id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("AIRWIZZ.Data.Entities.Arrival", b =>
                {
                    b.HasOne("AIRWIZZ.Data.Entities.Flight", "Flight")
                        .WithMany()
                        .HasForeignKey("Flight_Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Flight");
                });

            modelBuilder.Entity("AIRWIZZ.Data.Entities.Booking", b =>
                {
                    b.HasOne("AIRWIZZ.Data.Entities.Arrival", "Arrival")
                        .WithMany("Arrival_Bookings")
                        .HasForeignKey("Arrival_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AIRWIZZ.Data.Entities.Departure", "Departure")
                        .WithMany("Departure_Bookings")
                        .HasForeignKey("Departure_Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AIRWIZZ.Data.Entities.Flight", "Flight")
                        .WithMany("Flights_Bookings")
                        .HasForeignKey("Flight_Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AIRWIZZ.Data.Entities.Passenger", "Passenger")
                        .WithMany("Passenger_Bookings")
                        .HasForeignKey("Passenger_Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AIRWIZZ.Data.Entities.SeatPlan", "SeatPlan")
                        .WithMany("Seat_Bookings")
                        .HasForeignKey("Seat_Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AIRWIZZ.Data.Entities.User", "User")
                        .WithMany("User_Bookings")
                        .HasForeignKey("User_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Arrival");

                    b.Navigation("Departure");

                    b.Navigation("Flight");

                    b.Navigation("Passenger");

                    b.Navigation("SeatPlan");

                    b.Navigation("User");
                });

            modelBuilder.Entity("AIRWIZZ.Data.Entities.Departure", b =>
                {
                    b.HasOne("AIRWIZZ.Data.Entities.Flight", "Flight")
                        .WithMany()
                        .HasForeignKey("Flight_Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Flight");
                });

            modelBuilder.Entity("AIRWIZZ.Data.Entities.Passenger", b =>
                {
                    b.HasOne("AIRWIZZ.Data.Entities.User", "User")
                        .WithMany()
                        .HasForeignKey("User_Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("AIRWIZZ.Data.Entities.Payment", b =>
                {
                    b.HasOne("AIRWIZZ.Data.Entities.Booking", "Booking")
                        .WithOne("Payment")
                        .HasForeignKey("AIRWIZZ.Data.Entities.Payment", "Booking_Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AIRWIZZ.Data.Entities.User", "User")
                        .WithMany()
                        .HasForeignKey("User_Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Booking");

                    b.Navigation("User");
                });

            modelBuilder.Entity("AIRWIZZ.Data.Entities.SeatPlan", b =>
                {
                    b.HasOne("AIRWIZZ.Data.Entities.Flight", "Flight")
                        .WithMany()
                        .HasForeignKey("Flight_Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Flight");
                });

            modelBuilder.Entity("AIRWIZZ.Data.Entities.Arrival", b =>
                {
                    b.Navigation("Arrival_Bookings");
                });

            modelBuilder.Entity("AIRWIZZ.Data.Entities.Booking", b =>
                {
                    b.Navigation("Payment")
                        .IsRequired();
                });

            modelBuilder.Entity("AIRWIZZ.Data.Entities.Departure", b =>
                {
                    b.Navigation("Departure_Bookings");
                });

            modelBuilder.Entity("AIRWIZZ.Data.Entities.Flight", b =>
                {
                    b.Navigation("Flights_Bookings");
                });

            modelBuilder.Entity("AIRWIZZ.Data.Entities.Passenger", b =>
                {
                    b.Navigation("Passenger_Bookings");
                });

            modelBuilder.Entity("AIRWIZZ.Data.Entities.SeatPlan", b =>
                {
                    b.Navigation("Seat_Bookings");
                });

            modelBuilder.Entity("AIRWIZZ.Data.Entities.User", b =>
                {
                    b.Navigation("User_Bookings");
                });
#pragma warning restore 612, 618
        }
    }
}
