using System;
using System.Windows.Forms;

public partial class SignUpForm : Form
{
    public SignUpForm()
    {
        InitializeComponent();
    }

    private void btnSignUp_Click(object sender, EventArgs e)
    {
        var user = new User
        {
            Username = txtUsername.Text,
            Password = txtPassword.Text,
            Email = txtEmail.Text
        };

        DatabaseHelper.SaveUser(user);
        MessageBox.Show("Sign up successful!");
        this.Close();
    }
}