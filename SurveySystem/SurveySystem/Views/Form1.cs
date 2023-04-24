using DevExpress.Utils;
using DevExpress.XtraBars.Navigation;
using DevExpress.XtraBars.Ribbon.ViewInfo;
using DevExpress.XtraEditors;
using DevExpress.XtraExport.Helpers;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Card;
using DevExpress.XtraGrid.Views.Grid;
using SurveySystem.Entities;
using SurveySystem.Views;
using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection;
using System.Windows.Forms;
using static DevExpress.Data.Helpers.FindSearchRichParser;
using static DevExpress.XtraEditors.Mask.MaskSettings;
using static System.Windows.Forms.ImageList;

namespace SurveySystem
{
    public partial class Form1 : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        string globalToken = "";
        int globalUserId = 0;

        // QUIZ
        int selectedQuizId = 0;
        string selectedQuizTitle = "";
        float selectedQuizTime = 0;
        bool selectedAllowMultipleInputByUser = false;
        bool selectedAllowMultipleAnswer = false;
        bool selectedAllowMultipleAttempt = false;
        bool selectedAllowCorrectOption = false;
        bool selectedAllowQuizStop = false;
        bool selectedAllowQuizSkip = false;
        DateTime selectedQuizscheduleStartTime = DateTime.Now;
        DateTime selectedQuizscheduleEndTime = DateTime.Now;
        bool selectedIsRunning = false;
        bool selectedIsActive = false;

        // USER
        // USER LIST
        int selectedUserId = 0;
        string selectedUsername = "";
        string selectedEmail = "";
        string selectedPosition = "";
        string selectedDepartment = "";
        string selectedImagePath_User = "";

        // USER UPDATE
        int selectedUserRoleId = 0;
        string selectedMobile = "";
        string selectedAddress = "";
        bool selectedisActive= true;
        int selectedaddedBy = 0;
        string selectedpositionCode = "";
        string selectedjobCode = "";
        string selectedjobName = "";
        string selecteddepartmentCode = "";
        string selecteddepartmentName = "";
        string selectedregionCode = "";
        string selectedregionName = "";

        // Result
        int selectedId_Result = 0;
        string selectedUsername_Result = "";
        string selectedEmail_Result = "";
        int selectedQuizResponseInitialId_Result = 0;

        // Query
        int selectedUserId_initials = 0;
        string selectedUserEmail_initials = "";


        // _Query2
        string selectedImagePath_Query2 = "";

        int selectedUserRoleId_Query2 = 0;
        string selectedMobile_Query2 = "";
        string selectedAddress_Query2 = "";
        bool selectedisActive_Query2 = true;
        int selectedaddedBy_Query2 = 0;
        string selectedpositionCode_Query2 = "";
        string selectedjobCode_Query2 = "";
        string selectedjobName_Query2 = "";
        string selecteddepartmentCode_Query2 = "";
        string selecteddepartmentName_Query2 = "";
        string selectedregionCode_Query2 = "";
        string selectedregionName_Query2 = "";


        public LoginDetail loginDetails { get; set; }

        public Form1()
        {
            InitializeComponent();
        }
        // Create - Master Grid Load Event
        private void SurveyCreateMasterGrid_Load(object sender, EventArgs e)
        {
            //MessageBox.Show("token: " + globalToken + "\nid: " + globalUserId);
            surveyCreateMasterGridLoad(globalToken, globalUserId);
        }
        // Custom Method
        private void surveyCreateMasterGridLoad(string token, int userId)
        {
            try
            {
                IEnumerable<Survey> empobj = null;
                HttpClient hc = new HttpClient();
                hc.DefaultRequestHeaders.Authorization =
    new AuthenticationHeaderValue("Bearer", token);
                hc.BaseAddress = new Uri("https://localhost:44338/api/");

                var consumeapi = hc.GetAsync("Quiz/GetQuizList/" + userId);
                consumeapi.Wait();

                var readdata = consumeapi.Result;
                if (readdata.IsSuccessStatusCode)
                {
                    var displaydata = readdata.Content.ReadAsAsync<IList<Survey>>();
                    displaydata.Wait();
                    empobj = displaydata.Result;

                    // Grid Control
                    SurveyCreateMasterGrid.DataSource = empobj;
                    SurveyCreateMasterGrid.ForceInitialize();

                    // GridView
                    SurveyCreateMasterGridView.PopulateColumns();

                    GridColumn colQuizTopicId = SurveyCreateMasterGridView.Columns["quizTopicId"];
                    GridColumn colQuizTitle = SurveyCreateMasterGridView.Columns["quizTitle"];
                    GridColumn colQuizTime = SurveyCreateMasterGridView.Columns["quizTime"];
                    GridColumn colQuizTotalMarks = SurveyCreateMasterGridView.Columns["quizTotalMarks"];
                    GridColumn colQuizPassMarks = SurveyCreateMasterGridView.Columns["quizPassMarks"];
                    GridColumn colQuizMarkOptionId = SurveyCreateMasterGridView.Columns["quizMarkOptionId"];
                    GridColumn colQuizParticipantOptionId = SurveyCreateMasterGridView.Columns["quizParticipantOptionId"];
                    GridColumn colCertificateTemplateId = SurveyCreateMasterGridView.Columns["certificateTemplateId"];
                    GridColumn colAllowMultipleInputByUser = SurveyCreateMasterGridView.Columns["allowMultipleInputByUser"];
                    GridColumn colAllowMultipleAnswer = SurveyCreateMasterGridView.Columns["allowMultipleAnswer"];
                    GridColumn colAllowMultipleAttempt = SurveyCreateMasterGridView.Columns["allowMultipleAttempt"];
                    GridColumn colAllowCorrectOption = SurveyCreateMasterGridView.Columns["allowCorrectOption"];
                    GridColumn colAllowQuizStop = SurveyCreateMasterGridView.Columns["allowQuizStop"];
                    GridColumn colAllowQuizSkip = SurveyCreateMasterGridView.Columns["allowQuizSkip"];
                    GridColumn colAllowQuestionSuffle = SurveyCreateMasterGridView.Columns["allowQuestionSuffle"];
                    GridColumn colQuizscheduleStartTime = SurveyCreateMasterGridView.Columns["quizscheduleStartTime"];
                    GridColumn colQuizscheduleEndTime = SurveyCreateMasterGridView.Columns["quizscheduleEndTime"];
                    GridColumn colIsRunning = SurveyCreateMasterGridView.Columns["isRunning"];
                    GridColumn colQuizPrice = SurveyCreateMasterGridView.Columns["quizPrice"];
                    GridColumn colIsActive = SurveyCreateMasterGridView.Columns["isActive"];
                    GridColumn colQuestionsCount = SurveyCreateMasterGridView.Columns["questionsCount"];

                    colQuizTopicId.Caption = "ID";
                    colQuizTopicId.BestFit();

                    colQuizTitle.Caption = "TITLE";
                    colQuizTitle.BestFit();

                    colQuizTime.Visible = false;
                    colQuizTotalMarks.Visible = false;
                    colQuizPassMarks.Visible = false;
                    colQuizMarkOptionId.Visible = false;
                    colQuizParticipantOptionId.Visible = false;
                    colCertificateTemplateId.Visible = false;
                    colAllowMultipleInputByUser.Visible = false;
                    colAllowMultipleAnswer.Visible = false;
                    colAllowMultipleAttempt.Visible = false;
                    colAllowCorrectOption.Visible = false;
                    colAllowQuizStop.Visible = false;
                    colAllowQuizSkip.Visible = false;
                    colAllowQuestionSuffle.Visible = false;

                    colQuizscheduleStartTime.Caption = "START TIME";
                    colQuizscheduleStartTime.BestFit();

                    colQuizscheduleEndTime.Caption = "END TIME";
                    colQuizscheduleEndTime.BestFit();

                    colIsRunning.Caption = "IS RUNNING";
                    colIsRunning.BestFit();

                    colQuizPrice.Visible = false;

                    colIsActive.Caption = "IS ACTIVE";
                    colIsActive.BestFit();

                    colQuestionsCount.Caption = "QUESTIONS";
                    colQuestionsCount.BestFit();

                    SurveyCreateMasterGridView.OptionsBehavior.ReadOnly = true;
                    SurveyCreateMasterGridView.OptionsBehavior.Editable = false;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        private void SetConfirmationMessage(int userId, string token)
        {
            if (userId == 0) { XtraMessageBox.Show("You have not selected any users"); }
            else
            {
                if (XtraMessageBox.Show("Are you sure you want to delete the user with ID: " + userId + " ?", "Confirmation", MessageBoxButtons.YesNo) != System.Windows.Forms.DialogResult.No)
                {
                    try
                    {
                        HttpClient deleteHttpClient = new HttpClient();
                        deleteHttpClient.DefaultRequestHeaders.Authorization =
        new AuthenticationHeaderValue("Bearer", token);
                        deleteHttpClient.BaseAddress = new Uri("https://localhost:44338/api/");
                        var result = deleteHttpClient.DeleteAsync($"Quiz/DeleteQuiz/{userId}").Result;
                        if (result.IsSuccessStatusCode)
                        {
                            XtraMessageBox.Show("The quiz with ID " + userId + " has been deleted.");
                        }
                    }
                    catch (Exception e)
                    {
                        throw new Exception(e.Message);
                    }
                }
                else
                {
                    //XtraMessageBox.Show("Quiz won't be deleted");
                }
            }
        }

        // Create - Master Grid View
        private void SurveyCreateMasterGridView_RowClick(object sender, RowClickEventArgs e)
        {
            //int getRowQuestionId = Convert.ToInt16((sender as GridView).GetFocusedRowCellValue("quizTopicId"));
            selectedQuizId = Convert.ToInt32((sender as GridView).GetFocusedRowCellValue("quizTopicId"));
            selectedQuizTitle = (sender as GridView).GetFocusedRowCellValue("quizTitle").ToString();
            selectedQuizTime = (float)(sender as GridView).GetFocusedRowCellValue("quizTime");
            selectedAllowMultipleInputByUser = (bool)(sender as GridView).GetFocusedRowCellValue("allowMultipleInputByUser");
            selectedAllowMultipleAnswer = (bool)(sender as GridView).GetFocusedRowCellValue("allowMultipleAnswer");
            selectedAllowMultipleAttempt = (bool)(sender as GridView).GetFocusedRowCellValue("allowMultipleAttempt");
            selectedAllowCorrectOption = (bool)(sender as GridView).GetFocusedRowCellValue("allowCorrectOption");
            selectedAllowQuizStop = (bool)(sender as GridView).GetFocusedRowCellValue("allowQuizStop");
            selectedAllowQuizSkip = (bool)(sender as GridView).GetFocusedRowCellValue("allowQuizSkip");
            selectedQuizscheduleStartTime = (DateTime)(sender as GridView).GetFocusedRowCellValue("quizscheduleStartTime");
            selectedQuizscheduleEndTime = (DateTime)(sender as GridView).GetFocusedRowCellValue("quizscheduleEndTime");
            selectedIsRunning = (bool)(sender as GridView).GetFocusedRowCellValue("isRunning");
            selectedIsActive = (bool)(sender as GridView).GetFocusedRowCellValue("isActive");
            //string getCategories = (sender as GridView).GetFocusedRowCellValue("categories").ToString();
            //string getRowQuizTitle = (sender as GridView).GetFocusedRowCellValue("quizTitle").ToString();

            //DateTime getRowQuizscheduleStartTime = (DateTime)(sender as GridView).GetFocusedRowCellValue("quizscheduleStartTime");
            //DateTime getRowQuizscheduleEndTime = (DateTime)(sender as GridView).GetFocusedRowCellValue("quizscheduleEndTime");

            try
            {
                IEnumerable<Question> empobj2 = null;
                HttpClient hc = new HttpClient();
                hc.DefaultRequestHeaders.Authorization =
    new AuthenticationHeaderValue("Bearer", globalToken);
                hc.BaseAddress = new Uri("https://localhost:44338/api/");

                var consumeapi = hc.GetAsync("Quiz/GetQuizQuestionList/" + selectedQuizId);
                consumeapi.Wait();

                var readdata = consumeapi.Result;
                if (readdata.IsSuccessStatusCode)
                {
                    var displaydata = readdata.Content.ReadAsAsync<IList<Question>>();
                    displaydata.Wait();
                    empobj2 = displaydata.Result;

                    // Grid Control
                    SurveyCreateDetailGrid.DataSource = empobj2;
                    SurveyCreateDetailGrid.ForceInitialize();

                    // GridView
                    SurveyCreateDetailGridView.PopulateColumns();

                    GridColumn colQuizQuestionId = SurveyCreateDetailGridView.Columns["quizQuestionId"];
                    GridColumn colQuizTopicId = SurveyCreateDetailGridView.Columns["quizTopicId"];
                    GridColumn colQuestionDetail = SurveyCreateDetailGridView.Columns["questionDetail"];
                    GridColumn colSerialNo = SurveyCreateDetailGridView.Columns["serialNo"];
                    GridColumn colPerQuestionMark = SurveyCreateDetailGridView.Columns["perQuestionMark"];
                    GridColumn colQuestionTypeId = SurveyCreateDetailGridView.Columns["questionTypeId"];
                    GridColumn colQuestionLavelId = SurveyCreateDetailGridView.Columns["questionLavelId"];
                    GridColumn colQuestionCategoryId = SurveyCreateDetailGridView.Columns["questionCategoryId"];
                    GridColumn colOptionA = SurveyCreateDetailGridView.Columns["optionA"];
                    GridColumn colOptionB = SurveyCreateDetailGridView.Columns["optionB"];
                    GridColumn colOptionC = SurveyCreateDetailGridView.Columns["optionC"];
                    GridColumn colOptionD = SurveyCreateDetailGridView.Columns["optionD"];
                    GridColumn colOptionE = SurveyCreateDetailGridView.Columns["optionE"];
                    GridColumn colCorrectOption = SurveyCreateDetailGridView.Columns["correctOption"];
                    GridColumn colAnswerExplanation = SurveyCreateDetailGridView.Columns["answerExplanation"];
                    GridColumn colImagePath = SurveyCreateDetailGridView.Columns["imagePath"];
                    GridColumn colVideoPath = SurveyCreateDetailGridView.Columns["videoPath"];
                    GridColumn colIsCodeSnippet = SurveyCreateDetailGridView.Columns["isCodeSnippet"];
                    GridColumn colIsActive = SurveyCreateDetailGridView.Columns["isActive"];
                    GridColumn colAddedBy = SurveyCreateDetailGridView.Columns["addedBy"];
                    GridColumn colQuestionCategoryName = SurveyCreateDetailGridView.Columns["questionCategoryName"];

                    colQuestionDetail.Caption = "QUESTION";
                    colQuestionDetail.BestFit();

                    colOptionA.Caption = "A1";
                    colOptionA.BestFit();

                    colOptionB.Caption = "A2";
                    colOptionB.BestFit();

                    colOptionC.Caption = "A3";
                    colOptionC.BestFit();

                    colOptionD.Caption = "A4";
                    colOptionD.BestFit();

                    colOptionE.Caption = "A5";
                    colOptionE.BestFit(); 

                    colAddedBy.Visible = false;
                    colIsActive.Visible = false;
                    colQuizQuestionId.Visible = false;
                    colQuizTopicId.Visible = false;
                    colSerialNo.Visible = false;
                    colPerQuestionMark.Visible = false;
                    colQuestionTypeId.Visible = false;
                    colQuestionLavelId.Visible = false;
                    colQuestionCategoryId.Visible = false;
                    colCorrectOption.Visible = false;
                    colAnswerExplanation.Visible = false;
                    colImagePath.Visible = false;
                    colVideoPath.Visible = false;
                    colIsCodeSnippet.Visible = false;
                    colQuestionCategoryName.Visible = false;

                    SurveyCreateDetailGridView.OptionsBehavior.ReadOnly = true;
                    SurveyCreateDetailGridView.OptionsBehavior.Editable = false;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            globalToken = loginDetails.Token;
            globalUserId = loginDetails.Id;

            employeeGridControl_source(globalToken, globalUserId);
        }
        private void SurveyQuestionAddBtn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            InitialAdd initialAdd = new InitialAdd();
            initialAdd.quizResponseInitialId = 0;
            initialAdd.userId = selectedUserId_initials;
            initialAdd.email = selectedUserEmail_initials;
            initialAdd.attemptCount = 0;
            initialAdd.isExamined = false;
            initialAdd.quizTopicId = 1;
            initialAdd.quizTitle = "Store Manager Survey";
            initialAdd.quizMark = 0;
            initialAdd.quizPassMarks = 0;
            initialAdd.userObtainedQuizMark = 0;
            initialAdd.quizTime = 0;
            initialAdd.timeTaken = 0;
            initialAdd.startTime = DateTime.Now;
            initialAdd.endTime = DateTime.Now;
            initialAdd.isActive = true;
            initialAdd.isMigrationData = false;
            initialAdd.addedBy = globalUserId;
            initialAdd.dateAdded = DateTime.Now;
            initialAdd.lastUpdatedDate = DateTime.Now;
            initialAdd.lastUpdatedBy = globalUserId;
            initialAdd.positionCode = "";
            initialAdd.positionName = "";
            initialAdd.jobCode = "";
            initialAdd.jobName = "";
            initialAdd.departmentCode = "";
            initialAdd.departmentName = "";
            initialAdd.regionCode = "";
            initialAdd.regionName = "";

            /*
            DefineSurvey defineSurvey = new DefineSurvey();
            defineSurvey.loginDetails = loginDetails;
            defineSurvey.Show();
            */

            AssignSurvey assignSurvey = new AssignSurvey();
            assignSurvey.loginDetails = loginDetails;
            assignSurvey.initialAdd = initialAdd;
            assignSurvey.Show();
        }
        private void SurveyAddBtn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            SurveyCreate surveyCreate = new SurveyCreate();
            surveyCreate.loginDetails = loginDetails;
            surveyCreate.Show();
        }
        private void SurveyEditBtn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (selectedQuizId == 0) { XtraMessageBox.Show("You have not selected any survey"); }
            else
            {
                SurveyItem surveyItem = new SurveyItem();
                surveyItem.QuizTopicId = selectedQuizId;
                surveyItem.QuizTitle = selectedQuizTitle;
                surveyItem.QuizTime = selectedQuizTime;
                surveyItem.AllowMultipleInputByUser = selectedAllowMultipleInputByUser;
                surveyItem.AllowMultipleAnswer = selectedAllowMultipleAnswer;
                surveyItem.AllowMultipleAttempt = selectedAllowMultipleAttempt;
                surveyItem.AllowCorrectOption = selectedAllowCorrectOption;
                surveyItem.AllowQuizStop = selectedAllowQuizStop;
                surveyItem.AllowQuizSkip = selectedAllowQuizSkip;
                surveyItem.IsRunning = selectedIsRunning;
                surveyItem.IsActive = selectedIsActive;
                surveyItem.QuizscheduleStartTime = selectedQuizscheduleStartTime;
                surveyItem.QuizscheduleEndTime = selectedQuizscheduleEndTime;

                SurveyEdit surveyEdit = new SurveyEdit();
                surveyEdit.surveyItem = surveyItem;
                surveyEdit.loginDetails = loginDetails;
                surveyEdit.Show();
            }
        }
        private void ribbonControl_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            RibbonHitInfo hi = ribbonControl.CalcHitInfo(e.Location);
            if (hi.Page == EmployeePage || hi.Page == SurveyCreatePage || hi.Page == SurveyQueryPage || hi.Page == SurveyQuery2Page)
            {
                DXMouseEventArgs.GetMouseArgs(e).Handled = true;
            }
        }
        private void SurveyCreateMasterGridView_DoubleClick(object sender, EventArgs e)
        {
            SurveyItem surveyItem = new SurveyItem();
            surveyItem.QuizTopicId = selectedQuizId;
            surveyItem.QuizTitle = selectedQuizTitle;
            surveyItem.QuizTime = selectedQuizTime;

            surveyItem.AllowMultipleInputByUser = selectedAllowMultipleInputByUser;
            surveyItem.AllowMultipleAnswer = selectedAllowMultipleAnswer;
            surveyItem.AllowMultipleAttempt = selectedAllowMultipleAttempt;
            surveyItem.AllowCorrectOption = selectedAllowCorrectOption;
            surveyItem.AllowQuizStop = selectedAllowQuizStop;
            surveyItem.AllowQuizSkip = selectedAllowQuizSkip;
            surveyItem.IsRunning = selectedIsRunning;
            surveyItem.IsActive = selectedIsActive;
            surveyItem.QuizscheduleStartTime = selectedQuizscheduleStartTime;
            surveyItem.QuizscheduleEndTime = selectedQuizscheduleEndTime;

            SurveyEdit surveyEdit = new SurveyEdit();
            surveyEdit.surveyItem = surveyItem;
            surveyEdit.loginDetails = loginDetails;
            surveyEdit.Show();
        }
        private void EmployeeDeleteBtn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if(selectedUserId != 0)
            {
                if (XtraMessageBox.Show("Are you sure you want to delete the user with ID: " + selectedUserId + " ?", "Confirmation", MessageBoxButtons.YesNo) != System.Windows.Forms.DialogResult.No)
                {
                    try
                    {
                        HttpClient hc = new HttpClient();
                        hc.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", globalToken);
                        hc.BaseAddress = new Uri("https://localhost:44338/api/");
                        var response = hc.DeleteAsync("User/DeleteSingleUser/" + selectedUserId).Result;

                        if (response.IsSuccessStatusCode)
                        {
                            XtraMessageBox.Show("User Deleted Successfully");
                        }
                        else
                        {
                            XtraMessageBox.Show("Fail");
                        }
                    }
                    catch (Exception ex)
                    {
                        XtraMessageBox.Show("Fail: " + ex.Message);
                    }
                }
                else
                {
                    //XtraMessageBox.Show("Quiz won't be deleted");
                }
            }
            else
            {
                XtraMessageBox.Show("Please select any user");
            }
            
        }
        private void SurveyDeleteBtn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            SetConfirmationMessage(selectedQuizId, globalToken);
        }
        private void SurveyRefreshAll_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            surveyCreateMasterGridLoad(globalToken, globalUserId);
        }
        // NavBar
        private void officeNavigationBar_ItemClick(object sender, DevExpress.XtraBars.Navigation.NavigationBarItemEventArgs e)
        {
            if(e.Item == NavigationEmployee)
            {
                navigationFrame.SelectedPage = NavigationEmployee_Page;
                ribbonControl.SelectedPage = EmployeePage;
            }

            if(e.Item == NavigationSurveyCreate)
            {
                navigationFrame.SelectedPage = NavigationSurveyCreate_Page;
                ribbonControl.SelectedPage = SurveyCreatePage;
            }

            if(e.Item == NavigationSurveyQuery)
            {
                navigationFrame.SelectedPage = NavigationSurveyQuery_Page;
                ribbonControl.SelectedPage = SurveyQueryPage;
            }

            if(e.Item == NavigationSurveyQuery2)
            {
                navigationFrame.SelectedPage = NavigationSurveyQuery2_Page;
                ribbonControl.SelectedPage = SurveyQuery2Page;
            }

        }
        private void GridControl_Employee_Load(object sender, EventArgs e)
        {
            
        }
        private void employeeGridControl_source(string token, int userId)
        {
            try
            {
                IEnumerable<SurveySystem.Entities.User> empobj = null;
                HttpClient hc = new HttpClient();
                hc.DefaultRequestHeaders.Authorization =
    new AuthenticationHeaderValue("Bearer", token);
                hc.BaseAddress = new Uri("https://localhost:44338/api/");

                var consumeapi = hc.GetAsync("User/GetUserList/" + userId);
                consumeapi.Wait();

                var readdata = consumeapi.Result;
                if (readdata.IsSuccessStatusCode)
                {
                    var displaydata = readdata.Content.ReadAsAsync<IList<SurveySystem.Entities.User>>();
                    displaydata.Wait();
                    empobj = displaydata.Result;

                    // Grid Control
                    GridControl_Employee.DataSource = empobj;
                    GridControl_Employee.ForceInitialize();

                    // GridView
                    GridControlView_Employee.PopulateColumns();
                    GridColumn colToken = GridControlView_Employee.Columns["token"];
                    GridColumn colUserId = GridControlView_Employee.Columns["userId"];
                    GridColumn colUserRoleId = GridControlView_Employee.Columns["userRoleId"];
                    GridColumn colFullName = GridControlView_Employee.Columns["fullName"];
                    GridColumn colRoleName = GridControlView_Employee.Columns["roleName"];
                    GridColumn colDisplayName = GridControlView_Employee.Columns["displayName"];
                    GridColumn colMobile = GridControlView_Employee.Columns["mobile"];
                    GridColumn colEmail = GridControlView_Employee.Columns["email"];
                    GridColumn colPassword = GridControlView_Employee.Columns["password"];
                    GridColumn colDateOfBirth = GridControlView_Employee.Columns["dateOfBirth"];
                    GridColumn colAddress = GridControlView_Employee.Columns["address"];
                    GridColumn colImagePath = GridControlView_Employee.Columns["imagePath"];
                    GridColumn colAddedBy = GridControlView_Employee.Columns["addedBy"];
                    GridColumn colPositionCode = GridControlView_Employee.Columns["positionCode"];
                    GridColumn colPositionName = GridControlView_Employee.Columns["positionName"];
                    GridColumn colJobCode = GridControlView_Employee.Columns["jobCode"];
                    GridColumn colJobName = GridControlView_Employee.Columns["jobName"];
                    GridColumn colDepartmentCode = GridControlView_Employee.Columns["departmentCode"];
                    GridColumn colDepartmentName = GridControlView_Employee.Columns["departmentName"];
                    GridColumn colRegionCode = GridControlView_Employee.Columns["regionCode"];
                    GridColumn colRegionName = GridControlView_Employee.Columns["regionName"];
                    GridColumn colIsActive = GridControlView_Employee.Columns["isActive"];

                    GridColumn colStripeSessionId = GridControlView_Employee.Columns["stripeSessionId"];
                    GridColumn colBillingPlanId = GridControlView_Employee.Columns["billingPlanId"];
                    GridColumn colPaymentMode = GridControlView_Employee.Columns["paymentMode"];
                    GridColumn colIsMigrationData = GridControlView_Employee.Columns["isMigrationData"];
                    GridColumn colDateAdded = GridControlView_Employee.Columns["dateAdded"];
                    GridColumn colLastUpdatedDate = GridControlView_Employee.Columns["lastUpdatedDate"];
                    GridColumn colLastUpdatedBy = GridControlView_Employee.Columns["lastUpdatedBy"];
                    GridColumn colPlanName = GridControlView_Employee.Columns["planName"];
                    GridColumn colPlanExpiryDate = GridControlView_Employee.Columns["planExpiryDate"];
                    GridColumn colIsPlanExpired = GridControlView_Employee.Columns["isPlanExpired"];
                    GridColumn colTransactionDetail = GridControlView_Employee.Columns["transactionDetail"];
                    GridColumn colPaymentId = GridControlView_Employee.Columns["paymentId"];

                    colToken.Visible = false;
                    colStripeSessionId.Visible = false;
                    colBillingPlanId.Visible = false;
                    colPaymentMode.Visible = false;
                    colIsMigrationData.Visible = false;
                    colDateAdded.Visible = false;
                    colLastUpdatedDate.Visible = false;
                    colLastUpdatedBy.Visible = false;
                    colPlanName.Visible = false;
                    colPlanExpiryDate.Visible = false;
                    colIsPlanExpired.Visible = false;
                    colUserRoleId.Visible = false;
                    colRoleName.Visible = false;
                    colDisplayName.Visible = false;
                    colMobile.Visible = false;
                    colJobName.Visible = false;
                    colPassword.Visible = false;
                    colDateOfBirth.Visible = false;
                    colAddress.Visible = false;
                    colImagePath.Visible = false;
                    colAddedBy.Visible = false;
                    colPositionCode.Visible = false;
                    colJobCode.Visible = false;
                    colRegionCode.Visible = false;
                    colRegionName.Visible = false;
                    colIsActive.Visible = false;
                    colTransactionDetail.Visible = false;
                    colPaymentId.Visible = false;

                    colUserId.Caption = "ID";
                    colUserId.BestFit();

                    colFullName.Caption = "NAME";
                    colFullName.BestFit();

                    colEmail.Caption = "EMAIL";
                    colDepartmentCode.Visible = false;

                    colPositionName.Caption = "POSITION";
                    colPositionName.BestFit();

                    colDepartmentName.Caption = "DEPARTMENT";
                    colDepartmentName.BestFit();

                    GridControlView_Employee.OptionsBehavior.ReadOnly = true;
                    GridControlView_Employee.OptionsBehavior.Editable = false;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        private void surveyQueryMasterGrid_source(string token, int userId)
        {
            try
            {
                IEnumerable<SurveySystem.Entities.User> empobj = null;
                HttpClient hc = new HttpClient();
                hc.DefaultRequestHeaders.Authorization =
    new AuthenticationHeaderValue("Bearer", token);
                hc.BaseAddress = new Uri("https://localhost:44338/api/");

                var consumeapi = hc.GetAsync("User/GetUserList/" + userId);
                consumeapi.Wait();

                var readdata = consumeapi.Result;
                if (readdata.IsSuccessStatusCode)
                {
                    var displaydata = readdata.Content.ReadAsAsync<IList<SurveySystem.Entities.User>>();
                    displaydata.Wait();
                    empobj = displaydata.Result;

                    // Grid Control
                    SurveyQueryMasterGrid.DataSource = empobj;
                    SurveyQueryMasterGrid.ForceInitialize();

                    // GridView
                    SurveyQueryMasterGridView.PopulateColumns();
                    GridColumn colToken = SurveyQueryMasterGridView.Columns["token"];
                    GridColumn colUserId = SurveyQueryMasterGridView.Columns["userId"];
                    GridColumn colUserRoleId = SurveyQueryMasterGridView.Columns["userRoleId"];
                    GridColumn colFullName = SurveyQueryMasterGridView.Columns["fullName"];
                    GridColumn colRoleName = SurveyQueryMasterGridView.Columns["roleName"];
                    GridColumn colDisplayName = SurveyQueryMasterGridView.Columns["displayName"];
                    GridColumn colMobile = SurveyQueryMasterGridView.Columns["mobile"];
                    GridColumn colEmail = SurveyQueryMasterGridView.Columns["email"];
                    GridColumn colPassword = SurveyQueryMasterGridView.Columns["password"];
                    GridColumn colDateOfBirth = SurveyQueryMasterGridView.Columns["dateOfBirth"];
                    GridColumn colAddress = SurveyQueryMasterGridView.Columns["address"];
                    GridColumn colImagePath = SurveyQueryMasterGridView.Columns["imagePath"];
                    GridColumn colAddedBy = SurveyQueryMasterGridView.Columns["addedBy"];
                    GridColumn colPositionCode = SurveyQueryMasterGridView.Columns["positionCode"];
                    GridColumn colPositionName = SurveyQueryMasterGridView.Columns["positionName"];
                    GridColumn colJobCode = SurveyQueryMasterGridView.Columns["jobCode"];
                    GridColumn colJobName = SurveyQueryMasterGridView.Columns["jobName"];
                    GridColumn colDepartmentCode = SurveyQueryMasterGridView.Columns["departmentCode"];
                    GridColumn colDepartmentName = SurveyQueryMasterGridView.Columns["departmentName"];
                    GridColumn colRegionCode = SurveyQueryMasterGridView.Columns["regionCode"];
                    GridColumn colRegionName = SurveyQueryMasterGridView.Columns["regionName"];
                    GridColumn colIsActive = SurveyQueryMasterGridView.Columns["isActive"];

                    GridColumn colStripeSessionId = SurveyQueryMasterGridView.Columns["stripeSessionId"];
                    GridColumn colBillingPlanId = SurveyQueryMasterGridView.Columns["billingPlanId"];
                    GridColumn colPaymentMode = SurveyQueryMasterGridView.Columns["paymentMode"];
                    GridColumn colIsMigrationData = SurveyQueryMasterGridView.Columns["isMigrationData"];
                    GridColumn colDateAdded = SurveyQueryMasterGridView.Columns["dateAdded"];
                    GridColumn colLastUpdatedDate = SurveyQueryMasterGridView.Columns["lastUpdatedDate"];
                    GridColumn colLastUpdatedBy = SurveyQueryMasterGridView.Columns["lastUpdatedBy"];
                    GridColumn colPlanName = SurveyQueryMasterGridView.Columns["planName"];
                    GridColumn colPlanExpiryDate = SurveyQueryMasterGridView.Columns["planExpiryDate"];
                    GridColumn colIsPlanExpired = SurveyQueryMasterGridView.Columns["isPlanExpired"];
                    GridColumn colTransactionDetail = SurveyQueryMasterGridView.Columns["transactionDetail"];
                    GridColumn colPaymentId = SurveyQueryMasterGridView.Columns["paymentId"];

                    colToken.Visible = false;
                    colStripeSessionId.Visible = false;
                    colBillingPlanId.Visible = false;
                    colPaymentMode.Visible = false;
                    colIsMigrationData.Visible = false;
                    colDateAdded.Visible = false;
                    colLastUpdatedDate.Visible = false;
                    colLastUpdatedBy.Visible = false;
                    colPlanName.Visible = false;
                    colPlanExpiryDate.Visible = false;
                    colIsPlanExpired.Visible = false;
                    colUserRoleId.Visible = false;
                    colRoleName.Visible = false;
                    colDisplayName.Visible = false;
                    colMobile.Visible = false;
                    colJobName.Visible = false;
                    colPassword.Visible = false;
                    colDateOfBirth.Visible = false;
                    colAddress.Visible = false;
                    colImagePath.Visible = false;
                    colAddedBy.Visible = false;
                    colPositionCode.Visible = false;
                    colJobCode.Visible = false;
                    colRegionCode.Visible = false;
                    colRegionName.Visible = false;
                    colIsActive.Visible = false;
                    colTransactionDetail.Visible = false;
                    colPaymentId.Visible = false;

                    colUserId.Caption = "ID";
                    colUserId.BestFit();

                    colFullName.Caption = "NAME";
                    colFullName.BestFit();

                    colEmail.Caption = "EMAIL";
                    colDepartmentCode.Visible = false;

                    colPositionName.Caption = "POSITION";
                    colPositionName.BestFit();

                    colDepartmentName.Caption = "DEPARTMENT";
                    colDepartmentName.BestFit();

                    SurveyQueryMasterGridView.OptionsBehavior.ReadOnly = true;
                    SurveyQueryMasterGridView.OptionsBehavior.Editable = false;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        private void GridControlView_Employee_RowClick(object sender, RowClickEventArgs e)
        {
            selectedUserId = Convert.ToInt16((sender as GridView).GetFocusedRowCellValue("userId"));
            selectedUserRoleId = Convert.ToInt16((sender as GridView).GetFocusedRowCellValue("userRoleId"));
            selectedUsername = (sender as GridView).GetFocusedRowCellValue("fullName").ToString();
            selectedMobile = (sender as GridView).GetFocusedRowCellValue("mobile").ToString();
            selectedEmail = (sender as GridView).GetFocusedRowCellValue("email").ToString();
            selectedPosition = (sender as GridView).GetFocusedRowCellValue("positionName").ToString();
            selectedAddress = (sender as GridView).GetFocusedRowCellValue("address").ToString();
            selectedDepartment = (sender as GridView).GetFocusedRowCellValue("departmentName").ToString();
            selectedImagePath_User = (sender as GridView).GetFocusedRowCellValue("imagePath").ToString();
            selectedisActive = (bool)(sender as GridView).GetFocusedRowCellValue("isActive");
            selectedaddedBy = Convert.ToInt16((sender as GridView).GetFocusedRowCellValue("addedBy"));
            selectedpositionCode = (sender as GridView).GetFocusedRowCellValue("positionName").ToString();
            selectedjobCode = (sender as GridView).GetFocusedRowCellValue("jobCode").ToString();
            selectedjobName = (sender as GridView).GetFocusedRowCellValue("jobName").ToString();
            selecteddepartmentCode = (sender as GridView).GetFocusedRowCellValue("departmentCode").ToString();
            selecteddepartmentName = (sender as GridView).GetFocusedRowCellValue("departmentName").ToString();
            selectedregionCode = (sender as GridView).GetFocusedRowCellValue("regionCode").ToString();
            selectedregionName = (sender as GridView).GetFocusedRowCellValue("regionName").ToString();

            Label_Name.Text = selectedUsername;
            Label_Position.Text = selectedPosition;
            Label_Department.Text = selectedDepartment;

            if(!string.IsNullOrEmpty(selectedImagePath_User))
            {
                string fileName = selectedImagePath_User + ".jpg";
                string baseImageAddress = AppDomain.CurrentDomain.BaseDirectory;
                string imageFolder = "Resources\\Images\\";
                string completeImageDirectory = Path.Combine(Path.Combine(baseImageAddress, imageFolder), fileName);

                if(!File.Exists(completeImageDirectory))
                {
                    // do nothing
                }
                else
                {
                    Picture_Employee.Image = OpenImage(completeImageDirectory);
                    Picture_Employee.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Stretch;
                }
            }
            else
            {
                // do nothing
            }
            
        }
        private void SurveyQueryMasterGrid_Load(object sender, EventArgs e)
        {
            surveyQueryMasterGrid_source(globalToken, globalUserId);
        }
        private void surveyQueryDetailGrid_source(string token, int userId)
        {
            try
            {
                IEnumerable<Result> empobj = null;
                HttpClient hc = new HttpClient();
                hc.DefaultRequestHeaders.Authorization =
    new AuthenticationHeaderValue("Bearer", token);
                hc.BaseAddress = new Uri("https://localhost:44338/api/");

                var consumeapi = hc.GetAsync("Report/GetResults/" + userId);
                consumeapi.Wait();

                var readdata = consumeapi.Result;
                if (readdata.IsSuccessStatusCode)
                {
                    var displaydata = readdata.Content.ReadAsAsync<IList<Result>>();
                    displaydata.Wait();
                    empobj = displaydata.Result;

                    // Grid Control
                    SurveyQueryDetailGrid.DataSource = empobj;
                    SurveyQueryDetailGrid.ForceInitialize();

                    // GridView
                    SurveyQueryDetailGridView.PopulateColumns();
                    
                    
                    GridColumn colFullName = SurveyQueryDetailGridView.Columns["fullName"];
                    GridColumn colEmail = SurveyQueryDetailGridView.Columns["email"];
                    GridColumn colAttemptCount = SurveyQueryDetailGridView.Columns["attemptCount"];
                    GridColumn colQuizTopicId = SurveyQueryDetailGridView.Columns["quizTopicId"];
                    GridColumn colQuizResponseInitialId = SurveyQueryDetailGridView.Columns["quizResponseInitialId"];
                    GridColumn colQuizTitle = SurveyQueryDetailGridView.Columns["quizTitle"];
                    GridColumn colQuizMark = SurveyQueryDetailGridView.Columns["quizMark"];
                    GridColumn colQuizTime = SurveyQueryDetailGridView.Columns["quizTime"];
                    GridColumn colUserObtainedQuizMark = SurveyQueryDetailGridView.Columns["userObtainedQuizMark"];
                    GridColumn colTimeTaken = SurveyQueryDetailGridView.Columns["timeTaken"];
                    GridColumn colIsExamined = SurveyQueryDetailGridView.Columns["isExamined"];
                    GridColumn colDateAdded = SurveyQueryDetailGridView.Columns["dateAdded"];
                    GridColumn colCertificateTemplateId = SurveyQueryDetailGridView.Columns["certificateTemplateId"];
                    GridColumn colAllowCorrectOption = SurveyQueryDetailGridView.Columns["allowCorrectOption"];
                    GridColumn colQuizPassMarks = SurveyQueryDetailGridView.Columns["quizPassMarks"];
                    GridColumn colQuizMarkOptionId = SurveyQueryDetailGridView.Columns["quizMarkOptionId"];

                    colFullName.Visible = false;
                    colEmail.Visible = false;
                    colAttemptCount.Visible = false;
                    colQuizTopicId.Visible = false;
                    colQuizResponseInitialId.Visible = false;
                    colQuizMark.Visible = false;
                    colQuizTime.Visible = false;
                    colUserObtainedQuizMark.Visible = false;
                    colTimeTaken.Visible = false;
                    colIsExamined.Visible = false;
                    colDateAdded.Visible = false;
                    colCertificateTemplateId.Visible = false;
                    colAllowCorrectOption.Visible = false;
                    colQuizPassMarks.Visible = false;
                    colQuizMarkOptionId.Visible = false;

                    colQuizTitle.Caption = "SURVEY";
                    colQuizTitle.BestFit();
                    

                    SurveyQueryDetailGridView.OptionsBehavior.ReadOnly = true;
                    SurveyQueryDetailGridView.OptionsBehavior.Editable = false;

                    SurveyQueryDetailGridView.Columns["email"].FilterInfo = new ColumnFilterInfo($"[email] = '{selectedEmail_Result}'");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        private void SurveyQueryMasterGridView_RowClick(object sender, RowClickEventArgs e)
        {
            // Atananlari listelemek icin
            selectedId_Result = Convert.ToInt16((sender as GridView).GetFocusedRowCellValue("quizTopicId"));
            selectedEmail_Result = (sender as GridView).GetFocusedRowCellValue("email").ToString();
            selectedUsername_Result = (sender as GridView).GetFocusedRowCellValue("fullName").ToString();

            // Anket atamak icin
            selectedUserId_initials = Convert.ToInt16((sender as GridView).GetFocusedRowCellValue("userId"));
            selectedUserEmail_initials = (sender as GridView).GetFocusedRowCellValue("email").ToString();

            surveyQueryDetailGrid_source(globalToken, globalUserId);
        }
        private void surveyQuerySubDetailGrid_source(string token, int initialId)
        {
            try
            {
                IEnumerable<Exam> empobj = null;
                HttpClient hc = new HttpClient();
                hc.DefaultRequestHeaders.Authorization =
    new AuthenticationHeaderValue("Bearer", token);
                hc.BaseAddress = new Uri("https://localhost:44338/api/");

                var consumeapi = hc.GetAsync("Report/GetFinishedExamResult/" + initialId);
                consumeapi.Wait();

                var readdata = consumeapi.Result;
                if (readdata.IsSuccessStatusCode)
                {
                    var displaydata = readdata.Content.ReadAsAsync<IList<Exam>>();
                    displaydata.Wait();
                    empobj = displaydata.Result;

                    // Grid Control
                    SurveyQuerySubDetailGrid.DataSource = empobj;
                    SurveyQuerySubDetailGrid.ForceInitialize();

                    // GridView
                    SurveyQuerySubDetailGridView.PopulateColumns();


                    GridColumn colFullName = SurveyQuerySubDetailGridView.Columns["fullName"];
                    GridColumn colEmail = SurveyQuerySubDetailGridView.Columns["email"];
                    GridColumn colMobile = SurveyQuerySubDetailGridView.Columns["mobile"];
                    GridColumn colAddress = SurveyQuerySubDetailGridView.Columns["address"];
                    GridColumn colAttemptCount = SurveyQuerySubDetailGridView.Columns["attemptCount"];
                    GridColumn colQuizTitle = SurveyQuerySubDetailGridView.Columns["quizTitle"];
                    GridColumn colQuizMark = SurveyQuerySubDetailGridView.Columns["quizMark"];
                    GridColumn colQuizTime = SurveyQuerySubDetailGridView.Columns["quizTime"];
                    GridColumn colUserObtainedQuizMark = SurveyQuerySubDetailGridView.Columns["userObtainedQuizMark"];
                    GridColumn colTimeTaken = SurveyQuerySubDetailGridView.Columns["timeTaken"];
                    GridColumn colQuestionDetail = SurveyQuerySubDetailGridView.Columns["questionDetail"];
                    GridColumn colUserAnswer = SurveyQuerySubDetailGridView.Columns["userAnswer"];
                    GridColumn colIsAnswerSkipped = SurveyQuerySubDetailGridView.Columns["isAnswerSkipped"];
                    GridColumn colCorrectAnswer = SurveyQuerySubDetailGridView.Columns["correctAnswer"];
                    GridColumn colAnswerExplanation = SurveyQuerySubDetailGridView.Columns["answerExplanation"];
                    GridColumn colQuestionMark = SurveyQuerySubDetailGridView.Columns["questionMark"];
                    GridColumn colUserObtainedQuestionMark = SurveyQuerySubDetailGridView.Columns["userObtainedQuestionMark"];
                    GridColumn colCertificateTemplateId = SurveyQuerySubDetailGridView.Columns["certificateTemplateId"];
                    GridColumn colAllowCorrectOption = SurveyQuerySubDetailGridView.Columns["allowCorrectOption"];
                    GridColumn colQuizPassMarks = SurveyQuerySubDetailGridView.Columns["quizPassMarks"];
                    GridColumn colQuizMarkOptionId = SurveyQuerySubDetailGridView.Columns["quizMarkOptionId"];

                    colFullName.Visible = false;
                    colEmail.Visible = false;
                    colMobile.Visible = false;
                    colAddress.Visible = false;
                    colAttemptCount.Visible = false;
                    colQuizMark.Visible = false;
                    colQuizTime.Visible = false;
                    colUserObtainedQuizMark.Visible = false;
                    colTimeTaken.Visible = false;
                    colIsAnswerSkipped.Visible = false;
                    colCorrectAnswer.Visible = false;
                    colAnswerExplanation.Visible = false;
                    colQuestionMark.Visible = false;
                    colUserObtainedQuestionMark.Visible = false;
                    colCertificateTemplateId.Visible = false;
                    colAllowCorrectOption.Visible = false;
                    colQuizPassMarks.Visible = false;
                    colQuizMarkOptionId.Visible = false;

                    colQuizTitle.Caption = "SURVEY";
                    colQuizTitle.BestFit();

                    colQuestionDetail.Caption = "QUESTION";
                    colQuestionDetail.BestFit();

                    colUserAnswer.Caption = "ANSWER";
                    colQuizTitle.BestFit();

                    SurveyQuerySubDetailGridView.OptionsBehavior.ReadOnly = true;
                    SurveyQuerySubDetailGridView.OptionsBehavior.Editable = false;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        private void SurveyQueryDetailGridView_RowClick(object sender, RowClickEventArgs e)
        {
            // Define 'quizResponseInitialId' for Exam Results
            selectedQuizResponseInitialId_Result = Convert.ToInt16((sender as GridView).GetFocusedRowCellValue("quizResponseInitialId"));
            surveyQuerySubDetailGrid_source(globalToken, selectedQuizResponseInitialId_Result);
        }
        private void gridControl_SurveyQuery2_source(string token, int userId)
        {
            try
            {
                IEnumerable<SurveySystem.Entities.User> empobj = null;
                HttpClient hc = new HttpClient();
                hc.DefaultRequestHeaders.Authorization =
    new AuthenticationHeaderValue("Bearer", token);
                hc.BaseAddress = new Uri("https://localhost:44338/api/");

                var consumeapi = hc.GetAsync("User/GetUserList/" + userId);
                consumeapi.Wait();

                var readdata = consumeapi.Result;
                if (readdata.IsSuccessStatusCode)
                {
                    var displaydata = readdata.Content.ReadAsAsync<IList<SurveySystem.Entities.User>>();
                    displaydata.Wait();
                    empobj = displaydata.Result;

                    // Grid Control
                    GridControl_SurveyQuery2.DataSource = empobj;
                    GridControl_SurveyQuery2.ForceInitialize();

                    // GridView
                    GridControlView_SurveyQuery2.PopulateColumns();
                    GridColumn colToken = GridControlView_SurveyQuery2.Columns["token"];
                    GridColumn colUserId = GridControlView_SurveyQuery2.Columns["userId"];
                    GridColumn colUserRoleId = GridControlView_SurveyQuery2.Columns["userRoleId"];
                    GridColumn colFullName = GridControlView_SurveyQuery2.Columns["fullName"];
                    GridColumn colRoleName = GridControlView_SurveyQuery2.Columns["roleName"];
                    GridColumn colDisplayName = GridControlView_SurveyQuery2.Columns["displayName"];
                    GridColumn colMobile = GridControlView_SurveyQuery2.Columns["mobile"];
                    GridColumn colEmail = GridControlView_SurveyQuery2.Columns["email"];
                    GridColumn colPassword = GridControlView_SurveyQuery2.Columns["password"];
                    GridColumn colDateOfBirth = GridControlView_SurveyQuery2.Columns["dateOfBirth"];
                    GridColumn colAddress = GridControlView_SurveyQuery2.Columns["address"];
                    GridColumn colImagePath = GridControlView_SurveyQuery2.Columns["imagePath"];
                    GridColumn colAddedBy = GridControlView_SurveyQuery2.Columns["addedBy"];
                    GridColumn colPositionCode = GridControlView_SurveyQuery2.Columns["positionCode"];
                    GridColumn colPositionName = GridControlView_SurveyQuery2.Columns["positionName"];
                    GridColumn colJobCode = GridControlView_SurveyQuery2.Columns["jobCode"];
                    GridColumn colJobName = GridControlView_SurveyQuery2.Columns["jobName"];
                    GridColumn colDepartmentCode = GridControlView_SurveyQuery2.Columns["departmentCode"];
                    GridColumn colDepartmentName = GridControlView_SurveyQuery2.Columns["departmentName"];
                    GridColumn colRegionCode = GridControlView_SurveyQuery2.Columns["regionCode"];
                    GridColumn colRegionName = GridControlView_SurveyQuery2.Columns["regionName"];
                    GridColumn colIsActive = GridControlView_SurveyQuery2.Columns["isActive"];

                    GridColumn colStripeSessionId = GridControlView_SurveyQuery2.Columns["stripeSessionId"];
                    GridColumn colBillingPlanId = GridControlView_SurveyQuery2.Columns["billingPlanId"];
                    GridColumn colPaymentMode = GridControlView_SurveyQuery2.Columns["paymentMode"];
                    GridColumn colIsMigrationData = GridControlView_SurveyQuery2.Columns["isMigrationData"];
                    GridColumn colDateAdded = GridControlView_SurveyQuery2.Columns["dateAdded"];
                    GridColumn colLastUpdatedDate = GridControlView_SurveyQuery2.Columns["lastUpdatedDate"];
                    GridColumn colLastUpdatedBy = GridControlView_SurveyQuery2.Columns["lastUpdatedBy"];
                    GridColumn colPlanName = GridControlView_SurveyQuery2.Columns["planName"];
                    GridColumn colPlanExpiryDate = GridControlView_SurveyQuery2.Columns["planExpiryDate"];
                    GridColumn colIsPlanExpired = GridControlView_SurveyQuery2.Columns["isPlanExpired"];
                    GridColumn colTransactionDetail = GridControlView_SurveyQuery2.Columns["transactionDetail"];
                    GridColumn colPaymentId = GridControlView_SurveyQuery2.Columns["paymentId"];

                    colToken.Visible = false;
                    colStripeSessionId.Visible = false;
                    colBillingPlanId.Visible = false;
                    colPaymentMode.Visible = false;
                    colIsMigrationData.Visible = false;
                    colDateAdded.Visible = false;
                    colLastUpdatedDate.Visible = false;
                    colLastUpdatedBy.Visible = false;
                    colPlanName.Visible = false;
                    colPlanExpiryDate.Visible = false;
                    colIsPlanExpired.Visible = false;
                    colUserRoleId.Visible = false;
                    colRoleName.Visible = false;
                    colDisplayName.Visible = false;
                    colMobile.Visible = false;
                    colJobName.Visible = false;
                    colPassword.Visible = false;
                    colDateOfBirth.Visible = false;
                    colAddress.Visible = false;

                    // IMAGE
                    colImagePath.Visible = true;

                    colAddedBy.Visible = false;
                    colPositionCode.Visible = false;
                    colJobCode.Visible = false;
                    colRegionCode.Visible = false;
                    colRegionName.Visible = false;
                    colIsActive.Visible = false;
                    colTransactionDetail.Visible = false;
                    colPaymentId.Visible = false;

                    colUserId.Caption = "ID";
                    colUserId.BestFit();

                    colFullName.Caption = "NAME";
                    colFullName.BestFit();

                    colEmail.Caption = "EMAIL";
                    colDepartmentCode.Visible = false;

                    colPositionName.Caption = "POSITION";
                    colPositionName.BestFit();

                    colDepartmentName.Caption = "DEPARTMENT";
                    colDepartmentName.BestFit();

                    GridControlView_SurveyQuery2.OptionsBehavior.ReadOnly = true;
                    GridControlView_SurveyQuery2.OptionsBehavior.Editable = false;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public Image OpenImage(string previewFile)
        {
            FileStream fs = new FileStream(previewFile, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
            return Image.FromStream(fs);
        }
        private void GridControl_SurveyQuery2_Load(object sender, EventArgs e)
        {


            gridControl_SurveyQuery2_source(globalToken, globalUserId);
            GridControlView_SurveyQuery2.Columns.Add(new DevExpress.XtraGrid.Columns.GridColumn()
            {
                Caption = "Image",
                FieldName = "Image",
                UnboundDataType = typeof(string),
                Visible = true
            });



        }
        public string getTotalValue(GridView view, int listSourceRowIndex)
        {
            selectedImagePath_Query2 = (string)view.GetListSourceRowCellValue(listSourceRowIndex, "imagePath");
            return selectedImagePath_Query2;
        }

        Dictionary<string, Image> imageCache = new Dictionary<string, Image>();
        private void GridControlView_SurveyQuery2_CustomUnboundColumnData(object sender, CustomColumnDataEventArgs e)
        {
            /*
            string baseImageAddress = AppDomain.CurrentDomain.BaseDirectory;
            string imageFolder = "Resources\\Images\\";
            string completeImageDirectory = Path.Combine(baseImageAddress, imageFolder);
            //string completeImageDirectory = Path.Combine(Path.Combine(baseImageAddress, imageFolder), fileName);
            //string filePath = DevExpress.Utils.FilesHelper.FindingFileName(Application.StartupPath, ImageDir + fileName, false);
            */
        }

        private void EmployeeAddBtn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            EmployeeAdd employeeAdd = new EmployeeAdd();
            employeeAdd.loginDetails = loginDetails;
            employeeAdd.Show();
        }
        private void GridControlView_Employee_DoubleClick(object sender, EventArgs e)
        {
            UserUpdate userUpdate = new UserUpdate();
            userUpdate.userId = selectedUserId;
            userUpdate.userRoleId = selectedUserRoleId;
            userUpdate.fullName = selectedUsername;
            userUpdate.mobile = selectedMobile;
            userUpdate.email = selectedEmail;
            userUpdate.password = "";
            userUpdate.address = selectedAddress;
            userUpdate.dateOfBirth = DateTime.Now;
            userUpdate.imagePath = selectedImagePath_User;
            userUpdate.stripeSessionId = "";
            userUpdate.billingPlanId = 0;
            userUpdate.paymentId = 0;
            userUpdate.paymentMode = "";
            userUpdate.transactionDetail = "";
            userUpdate.isActive = selectedisActive;
            userUpdate.isMigrationData = false;
            userUpdate.addedBy = globalUserId;
            userUpdate.dateAdded = DateTime.Now;
            userUpdate.lastUpdatedDate = DateTime.Now;
            userUpdate.lastUpdatedBy = globalUserId;
            userUpdate.positionCode = selectedPosition;
            userUpdate.positionName = selectedPosition;
            userUpdate.jobCode = selectedjobCode;
            userUpdate.jobName = selectedjobName;
            userUpdate.departmentCode = selecteddepartmentCode;
            userUpdate.departmentName = selecteddepartmentName;
            userUpdate.regionCode = selectedregionCode;
            userUpdate.regionName = selectedregionName;

            EmployeeUpdate employeeUpdate = new EmployeeUpdate();
            employeeUpdate.loginDetails = loginDetails;
            employeeUpdate.userUpdate = userUpdate;
            employeeUpdate.Show();
        }
        private void EmployeeEditBtn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            UserUpdate userUpdate = new UserUpdate();
            userUpdate.userId = selectedUserId;
            userUpdate.userRoleId = selectedUserRoleId;
            userUpdate.fullName = selectedUsername;
            userUpdate.mobile = selectedMobile;
            userUpdate.email = selectedEmail;
            userUpdate.password = "";
            userUpdate.address = selectedAddress;
            userUpdate.dateOfBirth = DateTime.Now;
            userUpdate.imagePath = selectedImagePath_User;
            userUpdate.stripeSessionId = "";
            userUpdate.billingPlanId = 0;
            userUpdate.paymentId = 0;
            userUpdate.paymentMode = "";
            userUpdate.transactionDetail = "";
            userUpdate.isActive = selectedisActive;
            userUpdate.isMigrationData = false;
            userUpdate.addedBy = globalUserId;
            userUpdate.dateAdded = DateTime.Now;
            userUpdate.lastUpdatedDate = DateTime.Now;
            userUpdate.lastUpdatedBy = globalUserId;
            userUpdate.positionCode = selectedPosition;
            userUpdate.positionName = selectedPosition;
            userUpdate.jobCode = selectedjobCode;
            userUpdate.jobName = selectedjobName;
            userUpdate.departmentCode = selecteddepartmentCode;
            userUpdate.departmentName = selecteddepartmentName;
            userUpdate.regionCode = selectedregionCode;
            userUpdate.regionName = selectedregionName;

            EmployeeUpdateFromButton employeeUpdate = new EmployeeUpdateFromButton();
            employeeUpdate.loginDetails = loginDetails;
            employeeUpdate.userUpdate = userUpdate;
            employeeUpdate.Show();
        }
        private void EmployeeRefreshAll_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
           
        }
        private void GridControlView_SurveyQuery2_RowClick(object sender, RowClickEventArgs e)
        {
            selectedUserId = Convert.ToInt16((sender as GridView).GetFocusedRowCellValue("userId"));
            selectedUserRoleId_Query2 = Convert.ToInt16((sender as GridView).GetFocusedRowCellValue("userRoleId"));
            selectedUsername = (sender as GridView).GetFocusedRowCellValue("fullName").ToString();
            selectedMobile_Query2 = (sender as GridView).GetFocusedRowCellValue("mobile").ToString();
            selectedEmail = (sender as GridView).GetFocusedRowCellValue("email").ToString();
            selectedPosition = (sender as GridView).GetFocusedRowCellValue("positionName").ToString();
            selectedAddress_Query2 = (sender as GridView).GetFocusedRowCellValue("address").ToString();
            selectedDepartment = (sender as GridView).GetFocusedRowCellValue("departmentName").ToString();
            selectedImagePath_User = (sender as GridView).GetFocusedRowCellValue("imagePath").ToString();
            selectedisActive_Query2 = (bool)(sender as GridView).GetFocusedRowCellValue("isActive");
            selectedaddedBy_Query2 = Convert.ToInt16((sender as GridView).GetFocusedRowCellValue("addedBy"));
            selectedpositionCode_Query2 = (sender as GridView).GetFocusedRowCellValue("positionName").ToString();
            selectedjobCode_Query2 = (sender as GridView).GetFocusedRowCellValue("jobCode").ToString();
            selectedjobName_Query2 = (sender as GridView).GetFocusedRowCellValue("jobName").ToString();
            selecteddepartmentCode_Query2 = (sender as GridView).GetFocusedRowCellValue("departmentCode").ToString();
            selecteddepartmentName_Query2 = (sender as GridView).GetFocusedRowCellValue("departmentName").ToString();
            selectedregionCode_Query2 = (sender as GridView).GetFocusedRowCellValue("regionCode").ToString();
            selectedregionName_Query2 = (sender as GridView).GetFocusedRowCellValue("regionName").ToString();
        }
        private void GridControlView_SurveyQuery2_DoubleClick(object sender, EventArgs e)
        {
            UserUpdate userUpdate2 = new UserUpdate();
            userUpdate2.userId = selectedUserId;
            userUpdate2.userRoleId = selectedUserRoleId_Query2;
            userUpdate2.fullName = selectedUsername;
            userUpdate2.mobile = selectedMobile_Query2;
            userUpdate2.email = selectedEmail;
            userUpdate2.password = "";
            userUpdate2.address = selectedAddress_Query2;
            userUpdate2.dateOfBirth = DateTime.Now;
            userUpdate2.imagePath = selectedImagePath_User;
            userUpdate2.stripeSessionId = "";
            userUpdate2.billingPlanId = 0;
            userUpdate2.paymentId = 0;
            userUpdate2.paymentMode = "";
            userUpdate2.transactionDetail = "";
            userUpdate2.isActive = selectedisActive_Query2;
            userUpdate2.isMigrationData = false;
            userUpdate2.addedBy = globalUserId;
            userUpdate2.dateAdded = DateTime.Now;
            userUpdate2.lastUpdatedDate = DateTime.Now;
            userUpdate2.lastUpdatedBy = globalUserId;
            userUpdate2.positionCode = selectedPosition;
            userUpdate2.positionName = selectedPosition;
            userUpdate2.jobCode = selectedjobCode_Query2;
            userUpdate2.jobName = selectedjobName_Query2;
            userUpdate2.departmentCode = selecteddepartmentCode_Query2;
            userUpdate2.departmentName = selecteddepartmentName_Query2;
            userUpdate2.regionCode = selectedregionCode_Query2;
            userUpdate2.regionName = selectedregionName_Query2;

            Query2Update query2Update = new Query2Update();
            query2Update.userUpdate = userUpdate2;
            query2Update.Show();
        }

        private void accordionControlElement1_Click(object sender, EventArgs e)
        {

        }
    }
}