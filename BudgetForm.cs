using System;
using System.Windows.Forms;

public partial class BudgetForm : Form
{
    public BudgetForm()
    {
        InitializeComponent();
    }

    private void btnSave_Click(object sender, EventArgs e)
    {
        var budget = new Budget
        {
            Category = txtCategory.Text,
            Amount = decimal.Parse(txtAmount.Text),
            Month = dtpMonth.Value
        };

        DatabaseHelper.SaveBudget(budget);
        MessageBox.Show("Budget saved!");
        this.Close();
    }
}