using System;
using System.Windows.Forms;

public partial class LoginForm : Form
{
    public LoginForm()
    {
        InitializeComponent();
    }

    private void btnLogin_Click(object sender, EventArgs e)
    {
        var username = txtUsername.Text;
        var password = txtPassword.Text;

        var user = DatabaseHelper.AuthenticateUser(username, password);
        if (user != null)
        {
            MessageBox.Show("Login successful!");
            var dashboard = new DashboardForm();
            dashboard.Show();
            this.Hide();
        }
        else
        {
            MessageBox.Show("Invalid credentials.");
        }
    }
}