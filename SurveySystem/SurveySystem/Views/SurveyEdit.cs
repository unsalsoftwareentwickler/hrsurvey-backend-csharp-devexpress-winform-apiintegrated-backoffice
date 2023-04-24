using DevExpress.XtraBars.Docking2010;
using DevExpress.XtraEditors;
using DevExpress.XtraLayout.Customization;
using SurveySystem.Entities;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Windows.Forms;
using static DevExpress.Data.Helpers.FindSearchRichParser;

namespace SurveySystem.Views
{
    public partial class SurveyEdit : DevExpress.XtraEditors.XtraForm
    {
        string globalToken = "";
        int globalUserId = 0;

        public SurveyEdit()
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
            
            // Tıklanan Quiz bilgileri
            txtQuizId.Text = surveyItem.QuizTopicId.ToString();
            txtQuizTitle.Text = surveyItem.QuizTitle.ToString();
            txtQuizTime.Text = surveyItem.QuizTime.ToString();

            chkMultipleInput.Checked = surveyItem.AllowMultipleInputByUser;
            chkMultipleAnswer.Checked = surveyItem.AllowMultipleAnswer;
            chkMultipleAttempt.Checked = surveyItem.AllowMultipleAttempt;
            chkAllowCorrection.Checked = surveyItem.AllowCorrectOption;

            chkQuizStop.Checked = surveyItem.AllowQuizStop;
            chkQuizSkip.Checked = surveyItem.AllowQuizSkip;
            dateStartTime.DateTime = (DateTime)surveyItem.QuizscheduleStartTime;
            dateEndTime.DateTime = (DateTime)surveyItem.QuizscheduleEndTime;
            chkIsRunning.Checked = surveyItem.IsRunning;
            chkIsActive.Checked = surveyItem.IsActive;
        
        }

        private void UpdateQuiz(string token, int quizId, string quizTitle, string quizTime, bool multipleInput, bool multipleAnswer,
            bool multipleAttempt, bool correctOption, bool quizStop, bool quizSkip, DateTime quizStart, DateTime quizEnd,
            bool isRunning, string category, bool isActive, int addedBy, int lastUpdatedBy, DateTime dateAdded)
        {
            DateTime nowDate = DateTime.Now;
            string sqlFormattedDate = nowDate.ToString("yyyy-MM-dd HH:mm:ss.fff");
            DateTime now = DateTime.ParseExact(sqlFormattedDate, "yyyy-MM-dd HH:mm:ss.fff", System.Globalization.CultureInfo.InvariantCulture);
            try
            {
                HttpClient hc = new HttpClient();
                hc.DefaultRequestHeaders.Authorization =
    new AuthenticationHeaderValue("Bearer", token);
                hc.BaseAddress = new Uri("https://localhost:44338/api/");

                SurveyItem survey2 = new SurveyItem()
                {
                    QuizTopicId = quizId,
                    QuizTitle = quizTitle,
                    QuizTime = Convert.ToInt16(quizTime),
                    QuizTotalMarks = 0,
                    QuizPassMarks = 0,
                    QuizMarkOptionId = 0,
                    QuizParticipantOptionId = 0,
                    CertificateTemplateId = 0,
                    AllowMultipleInputByUser = multipleInput,
                    AllowMultipleAnswer = multipleAnswer,
                    AllowMultipleAttempt = multipleAttempt,
                    AllowCorrectOption = correctOption,
                    AllowQuizStop = quizStop,
                    AllowQuizSkip = quizSkip,
                    AllowQuestionSuffle = false,
                    QuizscheduleStartTime = now,
                    QuizscheduleEndTime = now,
                    QuizPrice = 0,
                    IsRunning = isRunning,
                    Categories = category,
                    IsActive = isActive,
                    IsMigrationData = false,
                    AddedBy = addedBy,
                    DateAdded = now,
                    LastUpdatedDate = now,
                    LastUpdatedBy = lastUpdatedBy,
                    PositionCode = "DEFAULT",
                    PositionName = "DEFAULT",
                    JobCode = "DEFAULT",
                    JobName = "DEFAULT",
                    DepartmentCode = "DEFAULT",
                    DepartmentName = "DEFAULT",
                    RegionCode = "DEFAULT",
                    RegionName = "DEFAULT"
                };

                var serializedSurvey = Newtonsoft.Json.JsonConvert.SerializeObject(survey2);

                //MessageBox.Show(serializedSurvey);

                StringContent stringContent = new StringContent(serializedSurvey, Encoding.UTF8, "application/json");
                var result = hc.PutAsync("Quiz/UpdateQuizTopic", stringContent).Result;
                if (result.IsSuccessStatusCode)
                {
                    MessageBox.Show("Survey updated successfully");
                }
                else
                {
                    MessageBox.Show(result.ReasonPhrase);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }


        private void windowsUIButtonPanelMain_ButtonClick(object sender, DevExpress.XtraBars.Docking2010.ButtonEventArgs e)
        {
            WindowsUIButton btn = e.Button as WindowsUIButton;
            if (btn.Tag != null && btn.Tag.Equals("Save"))
            {
                #region Date Expressions
                DateTime nowDate = DateTime.Now;
                string sqlFormattedDate = nowDate.ToString("yyyy-MM-dd HH:mm:ss.fff");

                DateTime now = DateTime.ParseExact(sqlFormattedDate, "yyyy-MM-dd HH:mm:ss.fff", System.Globalization.CultureInfo.InvariantCulture);
                DateTime quizStartDate = DateTime.ParseExact(sqlFormattedDate, "yyyy-MM-dd HH:mm:ss.fff", System.Globalization.CultureInfo.InvariantCulture);
                DateTime quizEndDate = DateTime.ParseExact(sqlFormattedDate, "yyyy-MM-dd HH:mm:ss.fff", System.Globalization.CultureInfo.InvariantCulture);
                #endregion

                /*
                MessageBox.Show("Token: " + globalToken + "\nQuiz Id: " + Convert.ToInt16(txtQuizId.Text) + "\nQuiz Title: " + txtQuizTitle.Text + "\nQuizTime: " + txtQuizTime.Text + "\nMultiple Input: " + chkMultipleInput.Checked
                    + "\nMultiple Answer: " + chkMultipleAnswer.Checked + "\nMultiple Attempt: " + chkMultipleAttempt.Checked + "\nCorrect Option: "
                    + chkAllowCorrection.Checked + "\nQuiz Stop: " + chkQuizStop.Checked + "\nQuiz Skip: " + chkQuizSkip.Checked + "\nQuiz Start: " + quizStartDate
                    + "\nQuiz End: " + quizEndDate + "\nIs Running: " + chkIsRunning.Checked + "\nCategory: " + cmbxCategory.Text + "\nIs Active: " +
                    chkIsActive.Checked + "\nAdded By: " + globalUserId + "\nLast Updated By: " + globalUserId + "\nDate Added: " + now
                    );
                */
                UpdateQuiz(globalToken, Convert.ToInt16(txtQuizId.Text), txtQuizTitle.Text, txtQuizTime.Text, chkMultipleInput.Checked,
                    chkMultipleAnswer.Checked, chkMultipleAttempt.Checked, chkAllowCorrection.Checked, chkQuizStop.Checked,
                    chkQuizSkip.Checked, quizStartDate, quizEndDate, chkIsRunning.Checked, cmbxCategory.Text,
                    chkIsActive.Checked, globalUserId, globalUserId, now);


            }
            if (btn.Tag != null && btn.Tag.Equals("SaveClose"))
            {
                #region Date Expressions
                DateTime nowDate = DateTime.Now;
                string sqlFormattedDate = nowDate.ToString("yyyy-MM-dd HH:mm:ss.fff");

                DateTime now = DateTime.ParseExact(sqlFormattedDate, "yyyy-MM-dd HH:mm:ss.fff", System.Globalization.CultureInfo.InvariantCulture);
                DateTime quizStartDate = DateTime.ParseExact(sqlFormattedDate, "yyyy-MM-dd HH:mm:ss.fff", System.Globalization.CultureInfo.InvariantCulture);
                DateTime quizEndDate = DateTime.ParseExact(sqlFormattedDate, "yyyy-MM-dd HH:mm:ss.fff", System.Globalization.CultureInfo.InvariantCulture);
                #endregion

                UpdateQuiz(globalToken, Convert.ToInt16(txtQuizId.Text), txtQuizTitle.Text, txtQuizTime.Text, chkMultipleInput.Checked, 
                    chkMultipleAnswer.Checked,chkMultipleAttempt.Checked, chkAllowCorrection.Checked, chkQuizStop.Checked, 
                    chkQuizSkip.Checked, dateStartTime.DateTime, dateEndTime.DateTime, chkIsRunning.Checked, cmbxCategory.Text, 
                    chkIsActive.Checked, globalUserId, globalUserId, now);
                this.Dispose();
            }
            if (btn.Tag != null && btn.Tag.Equals("ResetChanges"))
            {
                if (XtraMessageBox.Show("Your changes will be reset. Are you sure?", "Confirmation", MessageBoxButtons.YesNo) != System.Windows.Forms.DialogResult.No)
                {
                    txtQuizTitle.Text = "";
                    txtQuizTime.Text = "";
                    chkMultipleInput.Checked = false;
                    chkMultipleAnswer.Checked = false;
                    chkMultipleAttempt.Checked = false;
                    chkAllowCorrection.Checked = false;
                    chkQuizStop.Checked = false;
                    chkQuizSkip.Checked = false;
                    chkIsRunning.Checked = false;
                    chkIsActive.Checked = false;
                    cmbxCategory.Text = "";
                }
                else
                {
                    //XtraMessageBox.Show("User won't be deleted");
                }
            }
        }

        private void SurveyEdit_FormClosed(object sender, FormClosedEventArgs e)
        {
            
        }
    }
}