using DevExpress.XtraEditors;
using RestSharp;
using SurveySystem.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SurveySystem.Views
{
    public partial class Login : DevExpress.XtraEditors.XtraForm
    {
        public Login()
        {
            InitializeComponent();
        }
        private void tileBar_SelectedItemChanged(object sender, TileItemEventArgs e)
        {
            navigationFrame.SelectedPageIndex = tileBarGroupTables.Items.IndexOf(e.Item);
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            GetUsersFromApi(txtUsername.Text, txtPassword.Text);
        }

        private async void GetUsersFromApi(string username, string password)
        {
            try
            {
                var options = new RestClientOptions("https://localhost:44338/api/")
                {
                    ThrowOnAnyError = true,
                    Timeout = 1000
                };
                var client = new RestClient(options);
                var request = new RestRequest("User/GetLoginInfo/" + username + "/" + password, Method.Get);
                RestResponse response = client.Execute(request);

                if (string.IsNullOrEmpty(response.Content))
                {
                    XtraMessageBox.Show("Your username or password is incorrect. Please try again or try to reset your password");
                }
                else
                {

                    HttpClient client2 = new HttpClient();
                    HttpResponseMessage response2 = await client2.GetAsync("https://localhost:44338/api/User/GetLoginInfo/" + username + "/" + password);
                    response2.EnsureSuccessStatusCode();
                    string responseBody = await response2.Content.ReadAsStringAsync();

                    // JSon Response - Login Details
                    string[] loginDetails = responseBody.Split(':');

                    // User Token
                    string userTokenString = loginDetails[1];
                    string[] userTokenStringArray = userTokenString.Split('"');
                    string userToken = userTokenStringArray[1];

                    // User Id
                    string userIdString = loginDetails[3];
                    string[] userIdStringArray = userIdString.Split('"');
                    string userIdStringReadyToBeTrimmed = userIdStringArray[0];
                    string userIdTypeString = userIdStringReadyToBeTrimmed.Remove(userIdStringReadyToBeTrimmed.Length - 1);
                    int userId = Convert.ToInt32(userIdTypeString);

                    // LoginDetail Object Set
                    LoginDetail userDetails = new LoginDetail();
                    userDetails.Token = userToken;
                    userDetails.Id = userId;
                    userDetails.Email = "";
                    userDetails.Password = "";
                    userDetails.FullName = "";
                    userDetails.Mobile = "";
                    userDetails.Address = "";
                    userDetails.ImagePath = "";
                    userDetails.AddedBy = "1";
                    userDetails.BirthDate = DateTime.Now;
                    userDetails.Position = "";
                    userDetails.Job = "";
                    userDetails.Department = "";
                    userDetails.Region = "";

                    Form1 form1 = new Form1();
                    SurveyCreate surveyCreate = new SurveyCreate();
                    SurveyEdit surveyEdit = new SurveyEdit();

                    form1.loginDetails = userDetails;
                    surveyCreate.loginDetails = userDetails;
                    surveyEdit.loginDetails = userDetails;

                    form1.Show();
                    this.Hide();
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
            }

        }

        private void btnUserlogin_Click(object sender, EventArgs e)
        {
            XtraMessageBox.Show("User login is not active yet");
        }
    }
}