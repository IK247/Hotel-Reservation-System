Hotel Reservation System
Overview

The Hotel Reservation System is a two-sided application built using C# with a MySQL database. The system consists of two main interfaces: User and Admin.

    User Side: Allows users to login with their email, view available rooms, and make bookings.
    Admin Side: Allows administrators to manage rooms, bookings, and users, including the ability to add, delete, and update room availability.

Features
Admin Features:

    Account Creation & Login: Admins can create an account and log in.
    Room Management:
        Add new rooms (e.g., single, double, suite).
        Manage room availability (e.g., mark rooms as available/unavailable).
    Booking Management:
        View all bookings made by users.
        Delete bookings when necessary.
    Booking Creation:
        Admins can create bookings for users.

User Features:

    User Login: Users can log in using their email address.
    Room Booking:
        Users can view available rooms and their details (room type, availability, description).
        Users can select a room and complete the booking process.
    Booking Confirmation:
        A confirmation email is automatically sent to the user upon successful booking.

Setup Instructions
Prerequisites

    MySQL: You will need a running MySQL database to store the data.
    Visual Studio: The project was developed using Visual Studio, but you can use any compatible C# IDE.

Installation

    Clone this repository:

    git clone https://github.com/IK247/Hotel-Reservation-System.git

    Import the provided SQL schema into your MySQL database. You can find it in the Database/ folder as hotel_reservation.sql.

    Open the project in Visual Studio.

    Set up the connection string in the code to point to your MySQL server:
        Update the connection string in Data.cs to reflect your MySQL server’s IP, username, and password.

Running the Application

    Admin: Log in as an admin and manage the rooms, availability, and bookings.
    User: Log in with your email and proceed to make a booking.

Technologies Used

    C#: Backend logic and UI.
    MySQL: Database to store user and room data.
    Windows Forms: Used for the graphical user interface.

Additional Notes

    The system sends confirmation emails upon booking.
    The system supports room types like Single Room, Double Room, and Suite.



License

This project is licensed under the MIT License.
