using System;
using System.Windows.Forms;

public partial class DashboardForm : Form
{
    public DashboardForm()
    {
        InitializeComponent();
    }

    private void DashboardForm_Load(object sender, EventArgs e)
    {
        var totalIncome = DatabaseHelper.GetTotalIncome();
        var totalExpenses = DatabaseHelper.GetTotalExpenses();
        var balance = totalIncome - totalExpenses;

        lblTotalIncome.Text = $"Total Income: {totalIncome:C}";
        lblTotalExpenses.Text = $"Total Expenses: {totalExpenses:C}";
        lblBalance.Text = $"Balance: {balance:C}";
    }
}