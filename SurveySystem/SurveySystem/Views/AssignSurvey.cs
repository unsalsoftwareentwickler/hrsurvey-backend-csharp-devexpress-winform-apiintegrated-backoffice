using DevExpress.XtraBars.Docking2010;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Grid;
using SurveySystem.Entities;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Windows.Forms;
using static DevExpress.Data.Helpers.FindSearchRichParser;
using static DevExpress.XtraEditors.Mask.MaskSettings;

namespace SurveySystem.Views
{
    public partial class AssignSurvey : DevExpress.XtraEditors.XtraForm
    {
        string globalToken = "";
        int globalUserId = 0;

        int selected_quizTopicId = 0;
        string selected_quizTitle = "";
        string selected_quizscheduleStartTime = "";
        string selected_quizscheduleEndTime = "";

        public AssignSurvey()
        {
            InitializeComponent();
        }
        
        public LoginDetail loginDetails { get; set; }
        public UserUpdate userUpdate { get; set; }
        public InitialAdd initialAdd { get; set; }

        private void AssignSurvey_Load_1(object sender, EventArgs e)
        {
            // Programa giriş yapan kullanıcının token ve id bilgileri
            globalToken = loginDetails.Token;
            globalUserId = loginDetails.Id;

            txtEmployeeId.Text = initialAdd.userId.ToString();
            txtEmail.Text = initialAdd.email.ToString();

        }

        private void UpdateEmployee(string token, int userId, int userRoleId, string fullName, string mobile, string email, string password,
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

                UserUpdate user = new UserUpdate()
                {
                    userId = userId,
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
                var result = hc.PutAsync("User/UpdateUser", stringContent).Result;
                if (result.IsSuccessStatusCode){MessageBox.Show("User updated successfully");}
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
            if (btn.Tag != null && btn.Tag.Equals("Assign"))
            {
                #region Date Expressions
                DateTime nowDate = DateTime.Now;
                string sqlFormattedDate = nowDate.ToString("yyyy-MM-dd HH:mm:ss.fff");

                DateTime now = DateTime.ParseExact(sqlFormattedDate, "yyyy-MM-dd HH:mm:ss.fff", System.Globalization.CultureInfo.InvariantCulture);
                #endregion

                XtraMessageBox.Show("API'de aşağıdaki endpoint kontro edilmeli, 'POST etmiyor'." + "\n" +  "https://localhost:44338/api/Quiz/CreateQuizResponseInitial");

                /*
                if (txtEmployeeId.Text.Equals(0)) { XtraMessageBox.Show("You have not selected any users"); }
                else
                {
                    if (XtraMessageBox.Show("Are you sure you want to assign " + txtSurveyName.Text + " to user with ID: " + txtEmployeeId.Text + " ?", "Confirmation", MessageBoxButtons.YesNo) != System.Windows.Forms.DialogResult.No)
                    {
                        try
                        {
                            HttpClient hc = new HttpClient();
                            hc.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", globalToken);
                            hc.BaseAddress = new Uri("https://localhost:44338/api/");
                            InitialAdd initialAdd = new InitialAdd()
                            {
                                userId = Convert.ToInt16(txtEmployeeId.Text),
                                email = txtEmail.Text,
                                attemptCount = 0,
                                isExamined = false,
                                quizTopicId = Convert.ToInt16(txtSurveyId.Text),
                                quizTitle = txtSurveyName.Text,
                                quizMark = 0,
                                quizPassMarks = 0,
                                userObtainedQuizMark = 0,
                                quizTime = 0,
                                timeTaken = 0,
                                startTime = now,
                                endTime = now,
                                isActive = true,
                                isMigrationData = false,
                                addedBy = globalUserId,
                                dateAdded = now,
                                lastUpdatedDate = now,
                                lastUpdatedBy = globalUserId,
                                positionCode = "",
                                positionName = "",
                                jobCode = "",
                                jobName = "",
                                departmentCode = "",
                                departmentName = "",
                                regionCode = "",
                                regionName = ""
                        };
                            var serializedUser = Newtonsoft.Json.JsonConvert.SerializeObject(initialAdd);
                            StringContent stringContent = new StringContent(serializedUser, Encoding.UTF8, "application/json");
                            var result = hc.PostAsync("Quiz/CreateQuizResponseInitial", stringContent).Result;
                            if (result.IsSuccessStatusCode)
                            { MessageBox.Show("Survey assigned successfully"); }
                            else
                            { MessageBox.Show(result.ReasonPhrase); }
                        }
                        catch (Exception exc)
                        {
                            throw new Exception(exc.Message);
                        }
                    }
                    else
                    {
                        // do nothing
                    }
                }
                */


            }
            if (btn.Tag != null && btn.Tag.Equals("AssignClose"))
            {
                #region Date Expressions
                DateTime nowDate = DateTime.Now;
                string sqlFormattedDate = nowDate.ToString("yyyy-MM-dd HH:mm:ss.fff");

                DateTime now = DateTime.ParseExact(sqlFormattedDate, "yyyy-MM-dd HH:mm:ss.fff", System.Globalization.CultureInfo.InvariantCulture);
                #endregion

                /*
                int Cmbx_userRoleId = 0;
                if (cmbxRole.Text == "Admin") { Cmbx_userRoleId = 1; }
                if (cmbxRole.Text == "Student") { Cmbx_userRoleId = 2; }
                if (cmbxRole.Text == "Super Admin") { Cmbx_userRoleId = 3; }
                */

                XtraMessageBox.Show("API'de aşağıdaki endpoint kontro edilmeli, 'POST etmiyor'." + "\n" + "https://localhost:44338/api/Quiz/CreateQuizResponseInitial");

                /*
                if (Cmbx_userRoleId == 0)
                {
                    XtraMessageBox.Show("Please provide User Role");
                }
                else
                {
                    UpdateEmployee(globalToken, Convert.ToInt16(txtUserId.Text), Cmbx_userRoleId, txtFullName.Text, txtMobile.Text, txtEmail.Text, "string", txtAddress.Text, now, userUpdate.imagePath,
                        userUpdate.stripeSessionId, userUpdate.billingPlanId, userUpdate.paymentId, userUpdate.paymentMode, userUpdate.transactionDetail,
                        chkIsActive.Checked, userUpdate.isMigrationData, userUpdate.addedBy, now, now, userUpdate.lastUpdatedBy, cmbxPosition.Text,
                        cmbxPosition.Text, cmbxPosition.Text, cmbxPosition.Text, cmbxDepartment.Text, cmbxDepartment.Text,
                        cmbxRegion.Text, cmbxRegion.Text);
                }

                */

                this.Dispose();
            }
            if (btn.Tag != null && btn.Tag.Equals("ResetChanges"))
            {
                if (XtraMessageBox.Show("Your changes will be reset. Are you sure?", "Confirmation", MessageBoxButtons.YesNo) != System.Windows.Forms.DialogResult.No)
                {
                    /*
                    txtFullName.Text = string.Empty;
                    txtMobile.Text = string.Empty;
                    txtEmail.Text = string.Empty;
                    txtAddress.Text= string.Empty;
                    cmbxPosition.Text = string.Empty;
                    cmbxDepartment.Text = string.Empty;
                    cmbxRegion.Text = string.Empty;
                    cmbxRole.Text = string.Empty;
                    */
                }
                else
                {
                    
                }
            }
        }
        private void SurveyEdit_FormClosed(object sender, FormClosedEventArgs e)
        {
            
        }
        private void surveyAssignGridControl_source(string token, int userId)
        {
            try
            {
                IEnumerable<SurveySystem.Entities.SurveyList> empobj = null;
                HttpClient hc = new HttpClient();
                hc.DefaultRequestHeaders.Authorization =
    new AuthenticationHeaderValue("Bearer", token);
                hc.BaseAddress = new Uri("https://localhost:44338/api/");

                var consumeapi = hc.GetAsync("Quiz/GetQuizList/" + userId);
                consumeapi.Wait();

                var readdata = consumeapi.Result;
                if (readdata.IsSuccessStatusCode)
                {
                    var displaydata = readdata.Content.ReadAsAsync<IList<SurveySystem.Entities.SurveyList>>();
                    displaydata.Wait();
                    empobj = displaydata.Result;

                    // Grid Control
                    ChooseSurveyGridControl.DataSource = empobj;
                    ChooseSurveyGridControl.ForceInitialize();

                    // GridView
                    ChooseSurveyGridControlView.PopulateColumns();
                    GridColumn colQuizTopicId = ChooseSurveyGridControlView.Columns["quizTopicId"];
                    GridColumn colquizTitle = ChooseSurveyGridControlView.Columns["quizTitle"];
                    GridColumn colquizTime = ChooseSurveyGridControlView.Columns["quizTime"];
                    GridColumn colquizTotalMarks = ChooseSurveyGridControlView.Columns["quizTotalMarks"];
                    GridColumn colquizPassMarks = ChooseSurveyGridControlView.Columns["quizPassMarks"];
                    GridColumn colquizMarkOptionId = ChooseSurveyGridControlView.Columns["quizMarkOptionId"];
                    GridColumn colquizParticipantOptionId = ChooseSurveyGridControlView.Columns["quizParticipantOptionId"];
                    GridColumn colcertificateTemplateId = ChooseSurveyGridControlView.Columns["certificateTemplateId"];
                    GridColumn colallowMultipleInputByUser = ChooseSurveyGridControlView.Columns["allowMultipleInputByUser"];
                    GridColumn colallowMultipleAnswer = ChooseSurveyGridControlView.Columns["allowMultipleAnswer"];
                    GridColumn colallowMultipleAttempt = ChooseSurveyGridControlView.Columns["allowMultipleAttempt"];
                    GridColumn colallowCorrectOption = ChooseSurveyGridControlView.Columns["allowCorrectOption"];
                    GridColumn colallowQuizStop = ChooseSurveyGridControlView.Columns["allowQuizStop"];
                    GridColumn colallowQuizSkip = ChooseSurveyGridControlView.Columns["allowQuizSkip"];
                    GridColumn colallowQuestionSuffle = ChooseSurveyGridControlView.Columns["allowQuestionSuffle"];
                    GridColumn colquizscheduleStartTime = ChooseSurveyGridControlView.Columns["quizscheduleStartTime"];
                    GridColumn colquizscheduleEndTime = ChooseSurveyGridControlView.Columns["quizscheduleEndTime"];
                    GridColumn colisRunning = ChooseSurveyGridControlView.Columns["isRunning"];
                    GridColumn colquizPrice = ChooseSurveyGridControlView.Columns["quizPrice"];
                    GridColumn colisActive = ChooseSurveyGridControlView.Columns["isActive"];
                    GridColumn colquestionsCount = ChooseSurveyGridControlView.Columns["questionsCount"];

                    colQuizTopicId.Visible = false;
                    colquizTime.Visible = false;
                    colquizTotalMarks.Visible = false;
                    colquizPassMarks.Visible = false;
                    colquizMarkOptionId.Visible = false;
                    colquizParticipantOptionId.Visible = false;
                    colcertificateTemplateId.Visible = false;
                    colallowMultipleInputByUser.Visible = false;
                    colallowMultipleAnswer.Visible = false;
                    colallowMultipleAttempt.Visible = false;
                    colallowCorrectOption.Visible = false;
                    colallowQuizStop.Visible = false;
                    colallowQuizSkip.Visible = false;
                    colallowQuestionSuffle.Visible = false;
                    colquizscheduleStartTime.Visible = false;
                    colquizscheduleEndTime.Visible = false;
                    colisRunning.Visible = false;
                    colquizPrice.Visible = false;
                    colisActive.Visible = false;
                    colquestionsCount.Visible = false;

                    colquizTitle.Caption = "SURVEY";
                    colquizTitle.BestFit();

                    ChooseSurveyGridControlView.OptionsBehavior.ReadOnly = true;
                    ChooseSurveyGridControlView.OptionsBehavior.Editable = false;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private void btnChooseSurvey_Click(object sender, EventArgs e)
        {
            surveyAssignGridControl_source(globalToken, globalUserId);
        }

        private void ChooseSurveyGridControlView_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            selected_quizTopicId = Convert.ToInt16((sender as GridView).GetFocusedRowCellValue("quizTopicId"));
            selected_quizTitle = (sender as GridView).GetFocusedRowCellValue("quizTitle").ToString();
            selected_quizscheduleStartTime = (sender as GridView).GetFocusedRowCellValue("quizscheduleStartTime").ToString();
            selected_quizscheduleEndTime = (sender as GridView).GetFocusedRowCellValue("quizscheduleEndTime").ToString();

            txtSurveyId.Text = selected_quizTopicId.ToString();
            txtSurveyName.Text = selected_quizTitle;
            txtSurveyStartTime.Text = selected_quizscheduleStartTime;
            txtSurveyEndTime.Text = selected_quizscheduleEndTime;
        }
    }
}