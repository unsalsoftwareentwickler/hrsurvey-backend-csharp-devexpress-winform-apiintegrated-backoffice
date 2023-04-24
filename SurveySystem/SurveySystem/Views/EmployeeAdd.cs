using DevExpress.XtraBars.Docking2010;
using DevExpress.XtraEditors;
using SurveySystem.Entities;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Windows.Forms;

namespace SurveySystem.Views
{
    public partial class EmployeeAdd : DevExpress.XtraEditors.XtraForm
    {
        string globalToken = "";
        int globalUserId = 0;

        public EmployeeAdd()
        {
            InitializeComponent();
        }

        public Survey survey { get; set; }
        public SurveyItem surveyItem { get; set; }
        public LoginDetail loginDetails { get; set; }
        private void SurveyEdit_Load(object sender, EventArgs e)
        {
            // Programa giriş yapan kullanıcının token ve id bilgileri
            globalToken = loginDetails.Token;
            globalUserId = loginDetails.Id;

            // Formdaki alanlar
            txtFullName.Text = string.Empty;
            txtMobile.Text = string.Empty;
            txtEmail.Text = string.Empty;
            txtAddress.Text = string.Empty;
            cmbxPosition.Text = string.Empty;
            cmbxDepartment.Text = string.Empty;
            cmbxRegion.Text = string.Empty;
            chkIsActive.Checked = false;

            cmbxRole.Properties.Items.Add("Student");
            cmbxRole.Properties.Items.Add("Admin");
            cmbxRole.Properties.Items.Add("Super Admin");

        }

        private void AddEmployee(string token, int userRoleId, string fullName, string mobile, string email, string password,
            string address, DateTime dateOfBirth, string imagePath, string stripeSessionId, int billingPlanId, int paymentId,
            string paymentMode, string transactionDetail, bool isActive, bool isMigrationData, int addedBy, DateTime dateAdded, DateTime lastUpdatedDate,
            int lastUpdatedBy, string positionCode, string positionName, string jobCode, string jobName,string departmentCode, string departmentName,
            string regionCode, string regionName)
        {
            try
            {
                HttpClient hc = new HttpClient();
                hc.DefaultRequestHeaders.Authorization =
    new AuthenticationHeaderValue("Bearer", token);
                hc.BaseAddress = new Uri("https://localhost:44338/api/");

                UserAdd user = new UserAdd()
                {
                    userRoleId = userRoleId,
                    fullName = fullName,
                    mobile = mobile,
                    email = email,
                    password = password,
                    address = address,
                    dateOfBirth = dateOfBirth,
                    imagePath = imagePath,
                    stripeSessionId = stripeSessionId,
                    billingPlanId = billingPlanId,
                    paymentId = paymentId,
                    paymentMode = paymentMode,
                    transactionDetail = transactionDetail,
                    isActive = isActive,
                    isMigrationData = isMigrationData,
                    addedBy = addedBy,
                    dateAdded = dateAdded,
                    lastUpdatedDate = lastUpdatedDate,
                    lastUpdatedBy = lastUpdatedBy,
                    positionCode = positionCode,
                    positionName = positionName,
                    jobCode = jobCode,
                    jobName = jobName,
                    departmentCode = departmentCode,
                    departmentName = departmentName,
                    regionCode = regionCode,
                    regionName = regionName
                };

                var serializedSurvey = Newtonsoft.Json.JsonConvert.SerializeObject(user);
                //MessageBox.Show(serializedSurvey);
                StringContent stringContent = new StringContent(serializedSurvey, Encoding.UTF8, "application/json");
                var result = hc.PostAsync("User/CreateUser", stringContent).Result;
                if (result.IsSuccessStatusCode){MessageBox.Show("User created successfully");}
                else{MessageBox.Show(result.Content.ReadAsStringAsync().Result);}
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        private void windowsUIButtonPanelMain_ButtonClick(object sender, DevExpress.XtraBars.Docking2010.ButtonEventArgs e)
        {
            WindowsUIButton btn = e.Button as WindowsUIButton;
            if (btn.Tag != null && btn.Tag.Equals("Add"))
            {
                #region Date Expressions
                DateTime nowDate = DateTime.Now;
                string sqlFormattedDate = nowDate.ToString("yyyy-MM-dd HH:mm:ss.fff");

                DateTime now = DateTime.ParseExact(sqlFormattedDate, "yyyy-MM-dd HH:mm:ss.fff", System.Globalization.CultureInfo.InvariantCulture);
                #endregion

                int Cmbx_userRoleId = 0;
                if (cmbxRole.Text == "Admin"){ Cmbx_userRoleId = 1; }
                if (cmbxRole.Text == "Student") { Cmbx_userRoleId = 2; }
                if (cmbxRole.Text == "Super Admin"){ Cmbx_userRoleId = 3; }

                if(Cmbx_userRoleId == 0)
                {
                    XtraMessageBox.Show("Please provide User Role");
                }
                else
                {
                    AddEmployee(globalToken, Cmbx_userRoleId, txtFullName.Text, txtMobile.Text, txtEmail.Text,"string",txtAddress.Text, now, "", 
                        "", 0, 0, "", "",chkIsActive.Checked, false, globalUserId, now, now, globalUserId, cmbxPosition.Text,
                        cmbxPosition.Text, cmbxPosition.Text,cmbxPosition.Text,cmbxDepartment.Text, cmbxDepartment.Text, 
                        cmbxRegion.Text, cmbxRegion.Text);
                }
                

            }
            if (btn.Tag != null && btn.Tag.Equals("AddClose"))
            {
                #region Date Expressions
                DateTime nowDate = DateTime.Now;
                string sqlFormattedDate = nowDate.ToString("yyyy-MM-dd HH:mm:ss.fff");

                DateTime now = DateTime.ParseExact(sqlFormattedDate, "yyyy-MM-dd HH:mm:ss.fff", System.Globalization.CultureInfo.InvariantCulture);
                #endregion

                int Cmbx_userRoleId = 0;
                if (cmbxRole.Text == "Admin") { Cmbx_userRoleId = 1; }
                if (cmbxRole.Text == "Student") { Cmbx_userRoleId = 2; }
                if (cmbxRole.Text == "Super Admin") { Cmbx_userRoleId = 3; }

                if (Cmbx_userRoleId == 0)
                {
                    XtraMessageBox.Show("Please provide User Role");
                }
                else
                {
                    AddEmployee(globalToken, Cmbx_userRoleId, txtFullName.Text, txtMobile.Text, txtEmail.Text, "", txtAddress.Text, now, "",
                        "", 0, 0, "", "", chkIsActive.Checked, false, globalUserId, now, now, globalUserId, cmbxPosition.Text,
                        cmbxPosition.Text, cmbxPosition.Text, cmbxPosition.Text, cmbxDepartment.Text, cmbxDepartment.Text,
                        cmbxRegion.Text, cmbxRegion.Text);
                }
                this.Dispose();
            }
            if (btn.Tag != null && btn.Tag.Equals("ResetChanges"))
            {
                if (XtraMessageBox.Show("Your changes will be reset. Are you sure?", "Confirmation", MessageBoxButtons.YesNo) != System.Windows.Forms.DialogResult.No)
                {
                    txtFullName.Text = string.Empty;
                    txtMobile.Text = string.Empty;
                    txtEmail.Text = string.Empty;
                    txtAddress.Text= string.Empty;
                    cmbxPosition.Text = string.Empty;
                    cmbxDepartment.Text = string.Empty;
                    cmbxRegion.Text = string.Empty;
                    cmbxRole.Text = string.Empty;
                }
                else
                {
                    
                }
            }
        }

        private void SurveyEdit_FormClosed(object sender, FormClosedEventArgs e)
        {
            
        }
    }
}