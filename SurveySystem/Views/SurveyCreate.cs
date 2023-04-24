using DevExpress.Drawing;
using DevExpress.XtraBars.Docking2010;
using DevExpress.XtraEditors;
using DevExpress.XtraLayout;
using DevExpress.XtraLayout.Helpers;
using DevExpress.XtraSpellChecker.Parser;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static DevExpress.Data.Helpers.FindSearchRichParser;
using static DevExpress.Skins.SolidColorHelper;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;
using SurveySystem.Entities;
using DevExpress.Printing.Core.PdfExport.Metafile;

namespace SurveySystem.Views
{
    public partial class SurveyCreate : DevExpress.XtraEditors.XtraForm
    {
        string globalToken = "";
        int globalUserId = 0;

        public LoginDetail loginDetails { get; set; }

        public SurveyCreate()
        {
            InitializeComponent();
        }

        private void windowsUIButtonPanelMain_ButtonClick(object sender, ButtonEventArgs e)
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
                #region Save Survey - Variables
                string token = globalToken;
                string quizTitle = txtQuizTitle.Text;
                string quizTime = txtQuizTime.Text;
                bool multipleInput = chkMultipleInput.Checked;
                bool multipleAnswer = chkMultipleAnswer.Checked;
                bool multipleAttempt = chkMultipleAttempt.Checked;
                bool correctOption = chkAllowCorrection.Checked;
                bool quizStop = chkQuizStop.Checked;
                bool quizSkip = chkQuizSkip.Checked;
                bool isRunning = chkIsRunning.Checked;
                bool isActive = chkIsActive.Checked;
                int addedBy = globalUserId;
                int lastUpdatedBy = globalUserId;
                string category = cmbxCategory.Text;
                DateTime quizStart = quizStartDate;
                DateTime quizEnd = quizEndDate;
                DateTime dateAdded = now;
                #endregion

                SaveSurvey(globalToken, quizTitle, quizTime, multipleInput, multipleAnswer, multipleAttempt,
                    correctOption, quizStop, quizSkip, quizStart, quizEnd, isRunning, category, isActive, addedBy,
                    lastUpdatedBy, dateAdded);
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
                #region Save Survey - Variables
                string token = globalToken;
                string quizTitle = txtQuizTitle.Text;
                string quizTime = txtQuizTime.Text;
                bool multipleInput = chkMultipleInput.Checked;
                bool multipleAnswer = chkMultipleAnswer.Checked;
                bool multipleAttempt = chkMultipleAttempt.Checked;
                bool correctOption = chkAllowCorrection.Checked;
                bool quizStop = chkQuizStop.Checked;
                bool quizSkip = chkQuizSkip.Checked;
                bool isRunning = chkIsRunning.Checked;
                bool isActive = chkIsActive.Checked;
                int addedBy = globalUserId;
                int lastUpdatedBy = globalUserId;
                string category = cmbxCategory.Text;
                DateTime quizStart = quizStartDate;
                DateTime quizEnd = quizEndDate;
                DateTime dateAdded = now;
                #endregion

                SaveSurvey(token, quizTitle, quizTime, multipleInput, multipleAnswer, multipleAttempt,
                    correctOption, quizStop, quizSkip, quizStart, quizEnd, isRunning, category, isActive, addedBy,
                    lastUpdatedBy, dateAdded);
                this.Dispose();
            }
            if (btn.Tag != null && btn.Tag.Equals("ResetChanges"))
            {
                if (XtraMessageBox.Show("Your changes will be deleted. Are you sure?", "Confirmation", MessageBoxButtons.YesNo) != System.Windows.Forms.DialogResult.No)
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

        private void SaveSurvey(string token,string quizTitle, string quizTime, bool multipleInput, bool multipleAnswer,
            bool multipleAttempt, bool correctOption, bool quizStop, bool quizSkip, DateTime quizStart, DateTime quizEnd,
            bool isRunning, string category, bool isActive, int addedBy,int lastUpdatedBy, DateTime dateAdded)
        {
            try
            {
                HttpClient hc = new HttpClient();
                hc.DefaultRequestHeaders.Authorization =
    new AuthenticationHeaderValue("Bearer", token);
                hc.BaseAddress = new Uri("https://localhost:44338/api/");
                SurveyAdd survey = new SurveyAdd()
                {
                    quizTitle = quizTitle,
                    quizTime = Convert.ToInt16(quizTime),
                    quizTotalMarks = 0,
                    quizPassMarks = 0,
                    quizMarkOptionId = 0,
                    quizParticipantOptionId = 0,
                    certificateTemplateId = 0,
                    allowMultipleInputByUser = multipleInput,
                    allowMultipleAnswer = multipleAnswer,
                    allowMultipleAttempt = multipleAttempt,
                    allowCorrectOption = correctOption,
                    allowQuizStop = quizStop,
                    allowQuizSkip = quizSkip,
                    allowQuestionSuffle = false,
                    quizscheduleStartTime = quizStart,
                    quizscheduleEndTime = quizEnd,
                    quizPrice = 0,
                    isRunning = isRunning,
                    isActive = isActive,
                    isMigrationData = false,
                    addedBy = addedBy,
                    dateAdded = dateAdded,
                    lastUpdatedDate = dateAdded,
                    lastUpdatedBy = lastUpdatedBy,
                    positionCode = "DEFAULT",
                    positionName = "DEFAULT",
                    jobCode = "DEFAULT",
                    jobName = "DEFAULT",
                    departmentCode = "DEFAULT",
                    departmentName = "DEFAULT",
                    regionCode = "DEFAULT",
                    regionName = "DEFAULT"
                };
                var serializedUser = Newtonsoft.Json.JsonConvert.SerializeObject(survey);
                StringContent stringContent = new StringContent(serializedUser, Encoding.UTF8, "application/json");
                var result = hc.PostAsync("Quiz/CreateQuizTopic", stringContent).Result;
                if (result.IsSuccessStatusCode)
                {MessageBox.Show("Survey created successfully");}
                else
                {MessageBox.Show(result.ReasonPhrase);}
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void SurveyCreate_Load(object sender, EventArgs e)
        {
            globalToken = loginDetails.Token;
            globalUserId = loginDetails.Id;

        }
    }
}