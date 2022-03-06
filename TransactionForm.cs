using System;
using System.Windows.Forms;

public partial class TransactionForm : Form
{
    public TransactionForm()
    {
        InitializeComponent();
    }

    private void btnSave_Click(object sender, EventArgs e)
    {
        var transaction = new Transaction
        {
            Type = cmbType.SelectedItem.ToString(),
            Category = txtCategory.Text,
            Amount = decimal.Parse(txtAmount.Text),
            Date = dtpDate.Value
        };

        DatabaseHelper.SaveTransaction(transaction);
        MessageBox.Show("Transaction saved!");
        this.Close();
    }

    private void btnDelete_Click(object sender, EventArgs e)
    {
        var transactionId = int.Parse(txtTransactionId.Text);
        DatabaseHelper.DeleteTransaction(transactionId);
        MessageBox.Show("Transaction deleted!");
        this.Close();
    }
}