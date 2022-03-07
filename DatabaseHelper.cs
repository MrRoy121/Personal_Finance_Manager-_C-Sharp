using System;
using System.Collections.Generic;
using System.Data.SQLite;

public static class DatabaseHelper
{
    private static string connectionString = "Data Source=finance_manager.db;Version=3;";

    public static void InitializeDatabase()
    {
        using (var connection = new SQLiteConnection(connectionString))
        {
            connection.Open();
            var command = connection.CreateCommand();
            command.CommandText = @"
                CREATE TABLE IF NOT EXISTS Users (
                    Id INTEGER PRIMARY KEY AUTOINCREMENT,
                    Username TEXT NOT NULL,
                    Password TEXT NOT NULL,
                    Email TEXT NOT NULL
                );
                CREATE TABLE IF NOT EXISTS Transactions (
                    Id INTEGER PRIMARY KEY AUTOINCREMENT,
                    Type TEXT NOT NULL,
                    Category TEXT NOT NULL,
                    Amount REAL NOT NULL,
                    Date TEXT NOT NULL
                );
                CREATE TABLE IF NOT EXISTS Budgets (
                    Id INTEGER PRIMARY KEY AUTOINCREMENT,
                    Category TEXT NOT NULL,
                    Amount REAL NOT NULL,
                    Month TEXT NOT NULL
                );
            ";
            command.ExecuteNonQuery();
        }
    }

    public static void SaveUser(User user)
    {
        using (var connection = new SQLiteConnection(connectionString))
        {
            connection.Open();
            var command = connection.CreateCommand();
            command.CommandText = @"
                INSERT INTO Users (Username, Password, Email)
                VALUES (@Username, @Password, @Email)
            ";
            command.Parameters.AddWithValue("@Username", user.Username);
            command.Parameters.AddWithValue("@Password", user.Password);
            command.Parameters.AddWithValue("@Email", user.Email);
            command.ExecuteNonQuery();
        }
    }

    public static User AuthenticateUser(string username, string password)
    {
        using (var connection = new SQLiteConnection(connectionString))
        {
            connection.Open();
            var command = connection.CreateCommand();
            command.CommandText = @"
                SELECT * FROM Users
                WHERE Username = @Username AND Password = @Password
            ";
            command.Parameters.AddWithValue("@Username", username);
            command.Parameters.AddWithValue("@Password", password);
            using (var reader = command.ExecuteReader())
            {
                if (reader.Read())
                {
                    return new User
                    {
                        Id = reader.GetInt32(0),
                        Username = reader.GetString(1),
                        Password = reader.GetString(2),
                        Email = reader.GetString(3)
                    };
                }
                else
                {
                    return null;
                }
            }
        }
    }

    public static void SaveTransaction(Transaction transaction)
    {
        using (var connection = new SQLiteConnection(connectionString))
        {
            connection.Open();
            var command = connection.CreateCommand();
            command.CommandText = @"
                INSERT INTO Transactions (Type, Category, Amount, Date)
                VALUES (@Type, @Category, @Amount, @Date)
            ";
            command.Parameters.AddWithValue("@Type", transaction.Type);
            command.Parameters.AddWithValue("@Category", transaction.Category);
            command.Parameters.AddWithValue("@Amount", transaction.Amount);
            command.Parameters.AddWithValue("@Date", transaction.Date.ToString("yyyy-MM-dd"));
            command.ExecuteNonQuery();
        }
    }

    public static void DeleteTransaction(int id)
    {
        using (var connection = new SQLiteConnection(connectionString))
        {
            connection.Open();
            var command = connection.CreateCommand();
            command.CommandText = @"
                DELETE FROM Transactions WHERE Id = @Id
            ";
            command.Parameters.AddWithValue("@Id", id);
            command.ExecuteNonQuery();
        }
    }

    public static void SaveBudget(Budget budget)
    {
        using (var connection = new SQLiteConnection(connectionString))
        {
            connection.Open();
            var command = connection.CreateCommand();
            command.CommandText = @"
                INSERT INTO Budgets (Category, Amount, Month)
                VALUES (@Category, @Amount, @Month)
            ";
            command.Parameters.AddWithValue("@Category", budget.Category);
            command.Parameters.AddWithValue("@Amount", budget.Amount);
            command.Parameters.AddWithValue("@Month", budget.Month.ToString("yyyy-MM"));
            command.ExecuteNonQuery();
        }
    }

    public static decimal GetTotalIncome()
    {
        using (var connection = new SQLiteConnection(connectionString))
        {
            connection.Open();
            var command = connection.CreateCommand();
            command.CommandText = @"
                SELECT SUM(Amount) FROM Transactions WHERE Type = 'Income'
            ";
            return Convert.ToDecimal(command.ExecuteScalar());
        }
    }

    public static decimal GetTotalExpenses()
    {
        using (var connection = new SQLiteConnection(connectionString))
        {
            connection.Open();
            var command = connection.CreateCommand();
            command.CommandText = @"
                SELECT SUM(Amount) FROM Transactions WHERE Type = 'Expense'
            ";
            return Convert.ToDecimal(command.ExecuteScalar());
        }
    }

    public static List<Transaction> GetTransactions()
    {
        var transactions = new List<Transaction>();
        using (var connection = new SQLiteConnection(connectionString))
        {
            connection.Open();
            var command = connection.CreateCommand();
            command.CommandText = "SELECT * FROM Transactions";
            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    transactions.Add(new Transaction
                    {
                        Id = reader.GetInt32(0),
                        Type = reader.GetString(1),
                        Category = reader.GetString(2),
                        Amount = reader.GetDecimal(3),
                        Date = DateTime.Parse(reader.GetString(4))
                    });
                }
            }
        }
        return transactions;
    }

    public static List<string> GetExpenseCategories()
    {
        var categories = new List<string>();
        using (var connection = new SQLiteConnection(connectionString))
        {
            connection.Open();
            var command = connection.CreateCommand();
            command.CommandText = "SELECT DISTINCT Category FROM Transactions WHERE Type = 'Expense'";
            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    categories.Add(reader.GetString(0));
                }
            }
        }
        return categories;
    }

    public static decimal GetTotalByCategory(string category)
    {
        using (var connection = new SQLiteConnection(connectionString))
        {
            connection.Open();
            var command = connection.CreateCommand();
            command.CommandText = @"
                SELECT SUM(Amount) FROM Transactions
                WHERE Category = @Category AND Type = 'Expense'
            ";
            command.Parameters.AddWithValue("@Category", category);
            return Convert.ToDecimal(command.ExecuteScalar());
        }
    }

    public static void UpdateUser(User user)
    {
        using (var connection = new SQLiteConnection(connectionString))
        {
            connection.Open();
            var command = connection.CreateCommand();
            command.CommandText = @"
                UPDATE Users SET Username = @Username, Password = @Password, Email = @Email
                WHERE Id = @Id
            ";
            command.Parameters.AddWithValue("@Username", user.Username);
            command.Parameters.AddWithValue("@Password", user.Password);
            command.Parameters.AddWithValue("@Email", user.Email);
            command.Parameters.AddWithValue("@Id", user.Id);
            command.ExecuteNonQuery();
        }
    }
}