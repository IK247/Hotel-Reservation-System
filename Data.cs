using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using MySqlConnector;
using System.Diagnostics;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace HRS_V2
{
    public class Data
    {
        private string connectionString = "datasource=127.0.0.1;port=3306;username=root;password=;database=hrs_v2;";

        public void RegisterUser(string emailAddress, string name)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    // Fetch the current value of NextUserID
                    int nextUserID;
                    using (MySqlCommand getNextUserIDCommand = new MySqlCommand("SELECT NextUserID FROM UserIDs", connection))
                    {
                        nextUserID = Convert.ToInt32(getNextUserIDCommand.ExecuteScalar());
                    }

                    // Use the fetched NextUserID as the User ID for the new user
                    using (MySqlCommand insertUserCommand = new MySqlCommand())
                    {
                        insertUserCommand.Connection = connection;
                        insertUserCommand.CommandText = "INSERT INTO Users (UserID, EmailAddress, Name) VALUES (@UserID, @Email, @Name)";
                        insertUserCommand.Parameters.AddWithValue("@UserID", nextUserID);
                        insertUserCommand.Parameters.AddWithValue("@Email", emailAddress);
                        insertUserCommand.Parameters.AddWithValue("@Name", name);
                        insertUserCommand.ExecuteNonQuery();
                    }

                    // Increment NextUserID in UserIDs table
                    using (MySqlCommand updateNextUserIDCommand = new MySqlCommand("UPDATE UserIDs SET NextUserID = NextUserID + 1", connection))
                    {
                        updateNextUserIDCommand.ExecuteNonQuery();
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("An error occurred: " + ex.Message);
                }
            }
        }


        public bool AddRoom(int roomID, string roomType, string roomNumber, string availability, string features)
        {
            string query = "INSERT INTO Rooms (RoomID, RoomType, RoomNumber, Availability, Features) VALUES (@RoomID, @RoomType, @RoomNumber, @Availability, @Features)";

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            using (MySqlCommand command = new MySqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@RoomID", roomID);
                command.Parameters.AddWithValue("@RoomType", roomType);
                command.Parameters.AddWithValue("@RoomNumber", roomNumber);
                command.Parameters.AddWithValue("@Availability", availability);
                command.Parameters.AddWithValue("@Features", features);
                connection.Open();
                int rowsAffected = command.ExecuteNonQuery();
                return rowsAffected > 0; 
            }
        }


        public int GetUserIdByEmail(string EmailAddress)
        {
            int userId = -1;

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                string query = "SELECT UserId FROM Users WHERE EmailAddress = @EmailAddress";

                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@EmailAddress", EmailAddress);

                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            userId = reader.GetInt32("UserId");
                        }
                    }
                }
            }

            return userId;
        }

        public bool CheckAdminCredentials(string username, string password)
        {
            string query = "SELECT COUNT(*) FROM Admins WHERE Username = @Username AND Password = @Password";

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            using (MySqlCommand command = new MySqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@Username", username);
                command.Parameters.AddWithValue("@Password", password);
                connection.Open();
                int result = Convert.ToInt32(command.ExecuteScalar());
                return result > 0; 
            }
        } 



        public DataTable GetAvailableRooms(DateTime checkInDate, DateTime checkOutDate, string roomType)
        {
            DataTable dataTable = new DataTable();

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    using (MySqlCommand command = new MySqlCommand())
                    {
                        command.Connection = connection;
                        command.CommandText = "SELECT * FROM Rooms WHERE RoomType = @RoomType AND Availability = 'Available'";
                        command.Parameters.AddWithValue("@RoomType", roomType);

                        using (MySqlDataAdapter adapter = new MySqlDataAdapter(command))
                        {
                            adapter.Fill(dataTable);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("An error occurred: " + ex.Message);
                }
            }

            return dataTable;
        }
        public DataTable GetBookingOverview()
        {
            DataTable bookingOverview = new DataTable();

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    using (MySqlCommand command = new MySqlCommand())
                    {
                        command.Connection = connection;
                        command.CommandText = "SELECT * FROM Bookings";

                        using (MySqlDataAdapter adapter = new MySqlDataAdapter(command))
                        {
                            adapter.Fill(bookingOverview);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("An error occurred: " + ex.Message);
                }
            }

            return bookingOverview;
        }

        public string GetUserEmailById(int userId)
        {
            string query = "SELECT EmailAddress FROM Users WHERE UserID = @UserId";

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            using (MySqlCommand command = new MySqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@UserId", userId);
                connection.Open();
                object result = command.ExecuteScalar();
                return result != null ? result.ToString() : null;
            }
        }

        private int GetRoomIdByBookingId(MySqlConnection connection, int bookingId)
        {
            int roomId = -1; 

            try
            {
                using (MySqlCommand command = new MySqlCommand())
                {
                    command.Connection = connection;
                    command.CommandText = "SELECT RoomID FROM Bookings WHERE BookingID = @BookingID";
                    command.Parameters.AddWithValue("@BookingID", bookingId);

                    object result = command.ExecuteScalar();

                    if (result != null && result != DBNull.Value)
                    {
                        roomId = Convert.ToInt32(result);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred while retrieving room ID: " + ex.Message);
            }

            return roomId;
        }


        public DataTable GetAllRooms()
        {
            DataTable dataTable = new DataTable();

            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();

                    string query = "SELECT * FROM Rooms";
                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        using (MySqlDataAdapter adapter = new MySqlDataAdapter(command))
                        {
                            adapter.Fill(dataTable);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                
                Console.WriteLine("Error: " + ex.Message);
            }

            return dataTable;
        }

        public DataTable GetAllBookings()
        {
            DataTable bookingData = new DataTable();

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                string query = "SELECT BookingID, RoomID, UserID, CheckInDate, CheckOutDate FROM Bookings";

                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    try
                    {
                        connection.Open();
                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            bookingData.Load(reader);
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Error fetching bookings: " + ex.Message);
                    }
                }
            }

            return bookingData;
        }


        public bool CancelBooking(int bookingId)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    
                    int roomId = GetRoomIdByBookingId(connection, bookingId);

                    
                    using (MySqlCommand deleteCommand = new MySqlCommand())
                    {
                        deleteCommand.Connection = connection;
                        deleteCommand.CommandText = "DELETE FROM Bookings WHERE BookingID = @BookingID";
                        deleteCommand.Parameters.AddWithValue("@BookingID", bookingId);
                        int rowsAffected = deleteCommand.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            
                            UpdateRoomAvailability(connection, roomId);
                            return true; 
                        }
                        else
                        {
                            return false; 
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("An error occurred while canceling the booking: " + ex.Message);
                    return false; 
                }
            }
        }

        public bool RegisterAdmin(string username, string password)
        {
            try
            {
                string query = "INSERT INTO admins (Username, Password) VALUES (@Username, @Password)";

                using (MySqlConnection connection = new MySqlConnection(connectionString))
                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Username", username);
                    command.Parameters.AddWithValue("@Password", password);

                    connection.Open();
                    int rowsAffected = command.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message);
                return false;
            }
        }


        private void UpdateRoomAvailability(MySqlConnection connection, int roomId)
        {
            
            using (MySqlCommand updateCommand = new MySqlCommand())
            {
                updateCommand.Connection = connection;
                updateCommand.CommandText = "UPDATE Rooms SET Availability = 'Available' WHERE RoomID = @RoomID";
                updateCommand.Parameters.AddWithValue("@RoomID", roomId);
                updateCommand.ExecuteNonQuery();
            }
        }
        public bool ChangeAvailability(int roomId)
        {
            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();

                    
                    string selectQuery = "SELECT Availability FROM Rooms WHERE RoomID = @RoomID";
                    using (MySqlCommand selectCommand = new MySqlCommand(selectQuery, connection))
                    {
                        selectCommand.Parameters.AddWithValue("@RoomID", roomId);
                        string currentAvailability = selectCommand.ExecuteScalar()?.ToString();

                        if (currentAvailability == "Available")
                        {
                           
                            string updateQuery = "UPDATE Rooms SET Availability = 'Reserved' WHERE RoomID = @RoomID";
                            using (MySqlCommand updateCommand = new MySqlCommand(updateQuery, connection))
                            {
                                updateCommand.Parameters.AddWithValue("@RoomID", roomId);
                                int rowsAffected = updateCommand.ExecuteNonQuery();

                                return rowsAffected > 0;
                            }
                        }
                        else if (currentAvailability == "Reserved")
                        {
                            
                            string updateQuery = "UPDATE Rooms SET Availability = 'Available' WHERE RoomID = @RoomID";
                            using (MySqlCommand updateCommand = new MySqlCommand(updateQuery, connection))
                            {
                                updateCommand.Parameters.AddWithValue("@RoomID", roomId);
                                int rowsAffected = updateCommand.ExecuteNonQuery();

                                return rowsAffected > 0;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
               
                Console.WriteLine("Error: " + ex.Message);
            }

            return false; 
        }

        public DataTable GetUserBookingHistory(int userId)
        {
            string query = "SELECT BookingID, CheckInDate, CheckOutDate FROM Bookings WHERE UserID = @UserId";

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            using (MySqlCommand command = new MySqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@UserId", userId);
                connection.Open();

                DataTable bookingHistoryData = new DataTable();
                using (MySqlDataAdapter adapter = new MySqlDataAdapter(command))
                {
                    adapter.Fill(bookingHistoryData);
                }

                return bookingHistoryData;
            }
        }

        public bool DoesUserExist(int userId)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                string query = "SELECT COUNT(*) FROM Users WHERE UserId = @UserId";

                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@UserId", userId);
                    int count = Convert.ToInt32(command.ExecuteScalar());

                    return count > 0;
                }
            }
        }



        public void MakeBooking(int userId, int roomId, DateTime checkInDate, DateTime checkOutDate)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    using (MySqlCommand command = new MySqlCommand())
                    {
                        command.Connection = connection;
                        command.CommandText = "INSERT INTO Bookings (UserID, RoomID, CheckInDate, CheckOutDate) VALUES (@UserID, @RoomID, @CheckIn, @CheckOut)";
                        command.Parameters.AddWithValue("@UserID", userId);
                        command.Parameters.AddWithValue("@RoomID", roomId);
                        command.Parameters.AddWithValue("@CheckIn", checkInDate);
                        command.Parameters.AddWithValue("@CheckOut", checkOutDate);
                        command.ExecuteNonQuery();
                    }

                    
                    using (MySqlCommand updateCommand = new MySqlCommand())
                    {
                        updateCommand.Connection = connection;
                        updateCommand.CommandText = "UPDATE Rooms SET Availability = 'Reserved' WHERE RoomID = @RoomID";
                        updateCommand.Parameters.AddWithValue("@RoomID", roomId);
                        updateCommand.ExecuteNonQuery();
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("An error occurred: " + ex.Message);
                    
                }
            }
        }

        public void GenerateBooking(int bookingId, int userId, int roomId, DateTime checkInDate, DateTime checkOutDate)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    using (MySqlCommand command = new MySqlCommand())
                    {
                        command.Connection = connection;
                        command.CommandText = "INSERT INTO Bookings (BookingID, UserID, RoomID, CheckInDate, CheckOutDate) VALUES (@BookingID, @UserID, @RoomID, @CheckIn, @CheckOut)";
                        command.Parameters.AddWithValue("@BookingID", bookingId);
                        command.Parameters.AddWithValue("@UserID", userId);
                        command.Parameters.AddWithValue("@RoomID", roomId);
                        command.Parameters.AddWithValue("@CheckIn", checkInDate);
                        command.Parameters.AddWithValue("@CheckOut", checkOutDate);
                        command.ExecuteNonQuery();
                    }

                    
                    using (MySqlCommand updateCommand = new MySqlCommand())
                    {
                        updateCommand.Connection = connection;
                        updateCommand.CommandText = "UPDATE Rooms SET Availability = 'Reserved' WHERE RoomID = @RoomID";
                        updateCommand.Parameters.AddWithValue("@RoomID", roomId);
                        updateCommand.ExecuteNonQuery();
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("An error occurred: " + ex.Message);
                    
                }
            }
        }


        
        public bool AdminLogin(string username, string password)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    using (MySqlCommand command = new MySqlCommand())
                    {
                        command.Connection = connection;
                        command.CommandText = "SELECT COUNT(*) FROM Admins WHERE Username = @Username AND Password = @Password";
                        command.Parameters.AddWithValue("@Username", username);
                        command.Parameters.AddWithValue("@Password", password);

                        int count = Convert.ToInt32(command.ExecuteScalar());
                        return count > 0;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("An error occurred: " + ex.Message);
                    return false;
                }
            }
        }

        public Room GetRoomById(int roomID)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    using (MySqlCommand command = new MySqlCommand())
                    {
                        command.Connection = connection;
                        command.CommandText = "SELECT * FROM Rooms WHERE RoomID = @RoomID";
                        command.Parameters.AddWithValue("@RoomID", roomID);

                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                int roomId = Convert.ToInt32(reader["RoomID"]);
                                string roomType = reader["RoomType"].ToString();
                                string features = reader["Features"].ToString();
                                string availability = reader["Availability"].ToString();

                                return new Room(roomId, roomType, features, availability);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("An error occurred: " + ex.Message);
                }
            }

            return null; 
        }

        public DataTable GetBookingsOverview()
        {
            DataTable dataTable = new DataTable();

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    using (MySqlCommand command = new MySqlCommand())
                    {
                        command.Connection = connection;
                        command.CommandText = "SELECT Bookings.BookingID, Users.Name AS GuestName, Rooms.RoomType, Bookings.CheckInDate, Bookings.CheckOutDate FROM Bookings INNER JOIN Users ON Bookings.UserID = Users.UserID INNER JOIN Rooms ON Bookings.RoomID = Rooms.RoomID";

                        using (MySqlDataAdapter adapter = new MySqlDataAdapter(command))
                        {
                            adapter.Fill(dataTable);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("An error occurred: " + ex.Message);
                }
            }

            return dataTable;
        }

        public void ManageRoomAvailability(int roomId, string availability)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    using (MySqlCommand command = new MySqlCommand())
                    {
                        command.Connection = connection;
                        command.CommandText = "UPDATE Rooms SET Availability = @Availability WHERE RoomID = @RoomID";
                        command.Parameters.AddWithValue("@Availability", availability);
                        command.Parameters.AddWithValue("@RoomID", roomId);
                        command.ExecuteNonQuery();
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("An error occurred: " + ex.Message);
                }
            }
        }

        public void AddNewRoom(string roomType)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    using (MySqlCommand command = new MySqlCommand())
                    {
                        command.Connection = connection;
                        command.CommandText = "INSERT INTO Rooms (RoomType, Availability) VALUES (@RoomType, 'Available')";
                        command.Parameters.AddWithValue("@RoomType", roomType);
                        command.ExecuteNonQuery();
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("An error occurred: " + ex.Message);
                }
            }
        }

        public bool UserLogin(string emailAddress, string password)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    using (MySqlCommand command = new MySqlCommand())
                    {
                        command.Connection = connection;
                        command.CommandText = "SELECT COUNT(*) FROM Users WHERE EmailAddress = @Email AND Password = @Password";
                        command.Parameters.AddWithValue("@Email", emailAddress);
                        command.Parameters.AddWithValue("@Password", password);

                        int count = Convert.ToInt32(command.ExecuteScalar());
                        return count > 0;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("An error occurred: " + ex.Message);
                    return false;
                }
            }
        }

    }
}
