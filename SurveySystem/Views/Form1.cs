using DevExpress.Utils;
using DevExpress.XtraBars.Ribbon.ViewInfo;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Grid;
using SurveySystem.Entities;
using SurveySystem.Views;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Windows.Forms;

namespace SurveySystem
{
    public partial class Form1 : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        string globalToken = "";
        int globalUserId = 0;

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

        public LoginDetail loginDetails { get; set; }

        public Form1()
        {
            InitializeComponent();
        }
        void navBarControl_ActiveGroupChanged(object sender, DevExpress.XtraNavBar.NavBarGroupEventArgs e)
        {
            navigationFrame.SelectedPageIndex = navBarControl.Groups.IndexOf(e.Group);
        }
        void barButtonNavigation_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            int barItemIndex = barSubItemNavigation.ItemLinks.IndexOf(e.Link);
            navBarControl.ActiveGroup = navBarControl.Groups[barItemIndex];
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

            navBarControl.ActiveGroupChanged += NavBarControl_ActiveGroupChanged;
            
            ribbonControl.SelectedPage = EmployeePage;
            navigationFrame.SelectedPage = EmployeesNavigationPage;


        }

        private void NavBarControl_ActiveGroupChanged(object sender, DevExpress.XtraNavBar.NavBarGroupEventArgs e)
        {
            if(navigationFrame.SelectedPageIndex == 0)
            {
                ribbonControl.SelectedPage = EmployeePage;
            }
            if(navigationFrame.SelectedPageIndex == 1)
            {
                ribbonControl.SelectedPage = SurveyPage;
            }

            if(navigationFrame.SelectedPageIndex == 2)
            {
                ribbonControl.SelectedPage = QuestionPage;
            }
        }

        private void SurveyQuestionAddBtn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

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
            if (hi.Page == EmployeePage || hi.Page == SurveyPage || hi.Page == QuestionPage)
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

        }
        private void SurveyDeleteBtn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            SetConfirmationMessage(selectedQuizId, globalToken);
        }
        private void SurveyRefreshAll_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            surveyCreateMasterGridLoad(globalToken, globalUserId);
        }
    }
}