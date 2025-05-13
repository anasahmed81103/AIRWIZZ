# ✈️ AirWizz Flight System (ASP.NET)

AirWizz is a comprehensive flight management system built using ASP.NET. It allows users to **search, book, track flights in real-time**, and manage bookings. Admins can manage flights, currencies, and user data. A DialogFlow-powered chatbot is also integrated for interactive navigation.

---

## 🌟 Features

- 🔍 Search for available flights
- 🎫 Book and cancel flights
- 📥 Download your ticket as a PDF
- ✈️ Track **100+ real-time flights** using AviationStack API
- 🔐 Admin panel for:
  - Adding, updating, and deleting flights
  - Managing currency conversions
- 🤖 Google Dialogflow-integrated chatbot for smooth user interaction

---

## 📁 Project Setup Instructions

### 1. 🔧 Configure Database Connection

Open Program.cs and appsettings.json , and **update the connection string** to match your local SQL Server setup:

json
"ConnectionStrings": {
  "DefaultConnection": "Server=YOUR_SERVER_NAME;Database=AirWizzDB;Trusted_Connection=True;"
}



### 2. 🗄️ Setup the Database
Open SQL Server Management Studio (SSMS) and create a new database (e.g., AirWizzDB).

In Visual Studio, open Package Manager Console and run:

Update-Database

This will create all the necessary tables based on Entity Framework migrations.




### 3. 🌐 Configure Flight Tracking API
   
To enable real-time flight tracking, add your AviationStack API key in:
/Services/FlightTrackingService.cs

Replace the placeholder key with your own API key.



### 4. 💱 Setup Currency Conversion

Manually add currency data into the Currencies table (only once).

Ensure all currency codes (e.g., PKR, USD, EUR) are defined in the CurrencyConversions ENUM:

public enum CurrencyConversions
{
    USD = 1,
    EUR = 2,
    PKR = 3,
    // Add others as needed
}




### 5. 🔐 Admin Access

To access the admin dashboard:

Go to the Users section in the database or app.

Create a new user with the following credentials:

Email: admin@airwizz.com
Password: 123

Assign admin privileges as needed.





### 🧠 Technologies Used

ASP.NET Core MVC

Entity Framework Core

SQL Server

AviationStack API (for real-time flight tracking)

Google DialogFlow (chatbot integration)

Razor Pages & Bootstrap (for frontend)






