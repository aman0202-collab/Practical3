
using System;
using System.Collections.Generic;

class Expense
{
    public string Category { get; set; }
    public decimal Amount { get; set; }
    public DateTime Date { get; set; }
    public string Description { get; set; }

    public Expense(string category, decimal amount,
                   DateTime date, string description)
    {
        Category = category;
        Amount = amount;
        Date = date;
        Description = description;
    }
}

class Program
{
    static List<Expense> expenses = new List<Expense>();

    static void Main()
    {
        while (true)
        {
            Console.WriteLine("\n=== EXPENSE TRACKER ===");
            Console.WriteLine("1. Add Expense");
            Console.WriteLine("2. View Expenses");
            Console.WriteLine("3. Show Total Expense");
            Console.WriteLine("4. Exit");
            Console.Write("Enter choice: ");

            try
            {
                int choice = int.Parse(Console.ReadLine());

                switch (choice)
                {
                    case 1:
                        AddExpense();
                        break;

                    case 2:
                        ViewExpenses();
                        break;

                    case 3:
                        ShowTotal();
                        break;

                    case 4:
                        Console.WriteLine("Thank you!");
                        return;

                    default:
                        Console.WriteLine("Invalid menu choice.");
                        break;
                }
            }
            catch (FormatException)
            {
                Console.WriteLine(
                    "Error: Please enter a valid number.");
            }
            catch (OverflowException)
            {
                Console.WriteLine("Error: Number is too large.");
            }
        }
    }

    static void AddExpense()
    {
        try
        {
            Console.Write("Enter category: ");
            string category = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(category))
                throw new ArgumentException(
                    "Category cannot be empty.");

            Console.Write("Enter amount: ");
            decimal amount = decimal.Parse(Console.ReadLine());

            if (amount <= 0)
                throw new ArgumentException(
                    "Amount must be greater than zero.");

            Console.Write("Enter date (dd-MM-yyyy): ");
            DateTime date = DateTime.ParseExact(
                Console.ReadLine(),
                "dd-MM-yyyy",
                System.Globalization.CultureInfo.InvariantCulture);

            Console.Write("Enter description: ");
            string description = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(description))
                description = "No description";

            expenses.Add(new Expense(
                category, amount, date, description));

            Console.WriteLine("Expense added successfully!");
        }
        catch (FormatException)
        {
            Console.WriteLine(
                "Error: Enter a valid amount or date.");
        }
        catch (OverflowException)
        {
            Console.WriteLine("Error: Amount is too large.");
        }
        catch (ArgumentException ex)
        {
            Console.WriteLine("Error: " + ex.Message);
        }
        catch (Exception)
        {
            Console.WriteLine("Unexpected error occurred.");
        }
    }

    static void ViewExpenses()
    {
        if (expenses.Count == 0)
        {
            Console.WriteLine("No expenses recorded.");
            return;
        }

        Console.WriteLine("\n--- Expense List ---");

        foreach (Expense e in expenses)
        {
            Console.WriteLine(
                $"{e.Date:dd-MM-yyyy} | {e.Category} | " +
                $"Rs. {e.Amount:F2} | {e.Description}");
        }
    }

    static void ShowTotal()
    {
        decimal total = 0;

        foreach (Expense e in expenses)
        {
            total += e.Amount;
        }

        Console.WriteLine($"Total Expense: Rs. {total:F2}");
    }
}
