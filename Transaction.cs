using System;

public class Transaction
{
    public int Id { get; set; }
    public string Type { get; set; } // Income or Expense
    public string Category { get; set; }
    public decimal Amount { get; set; }
    public DateTime Date { get; set; }
}