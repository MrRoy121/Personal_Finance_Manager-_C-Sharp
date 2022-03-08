using System;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

public partial class ReportForm : Form
{
    public ReportForm()
    {
        InitializeComponent();
    }

    private void ReportForm_Load(object sender, EventArgs e)
    {
        var transactions = DatabaseHelper.GetTransactions();
        var expenseCategories = DatabaseHelper.GetExpenseCategories();

        foreach (var category in expenseCategories)
        {
            var total = DatabaseHelper.GetTotalByCategory(category);
            chartExpenses.Series["Expenses"].Points.AddXY(category, total);
        }
    }
}