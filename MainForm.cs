using System;
using System.Windows.Forms;

public partial class MainForm : Form
{
    public MainForm()
    {
        InitializeComponent();
    }

    private void btnDashboard_Click(object sender, EventArgs e)
    {
        var dashboardForm = new DashboardForm();
        dashboardForm.Show();
    }

    private void btnTransactions_Click(object sender, EventArgs e)
    {
        var transactionForm = new TransactionForm();
        transactionForm.Show();
    }

    private void btnBudgets_Click(object sender, EventArgs e)
    {
        var budgetForm = new BudgetForm();
        budgetForm.Show();
    }

    private void btnReports_Click(object sender, EventArgs e)
    {
        var reportForm = new ReportForm();
        reportForm.Show();
    }

    private void btnSettings_Click(object sender, EventArgs e)
    {
        var userSettingsForm = new UserSettingsForm();
        userSettingsForm.Show();
    }
}