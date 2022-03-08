using System;
using System.Windows.Forms;

public partial class UserSettingsForm : Form
{
    public UserSettingsForm()
    {
        InitializeComponent();
    }

    private void btnUpdateProfile_Click(object sender, EventArgs e)
    {
        var user = new User
        {
            Username = txtUsername.Text,
            Password = txtPassword.Text,
            Email = txtEmail.Text
        };

        DatabaseHelper.UpdateUser(user);
        MessageBox.Show("Profile updated!");
        this.Close();
    }
}