using System;
using System.Diagnostics.SymbolStore;

namespace SurveySystem.Entities
{
    public class SurveyItem
    {
        private int quizTopicId;
        private string quizTitle;
        private float quizTime;

        // Empty Fields
        private int quizTotalMarks;
        private int quizPassMarks;
        private int quizMarkOptionId;
        private int quizParticipantOptionId;
        private int certificateTemplateId;

        private bool allowMultipleInputByUser;
        private bool allowMultipleAnswer;
        private bool allowMultipleAttempt;
        private bool allowCorrectOption;
        private bool allowQuizStop;
        private bool allowQuizSkip;

        // Empty Fields
        private bool allowQuestionSuffle;

        private bool isRunning;
        private bool isActive;
        private DateTime quizscheduleStartTime;
        private DateTime quizscheduleEndTime;

        // Empty Fields
        private int quizPrice;
        private string categories;
        private bool isMigrationData;
        private int addedBy;
        private DateTime dateAdded;
        private DateTime lastUpdatedDate;
        private int lastUpdatedBy;
        private string positionCode;
        private string positionName;
        private string jobCode;
        private string jobName;
        private string departmentCode;
        private string departmentName;
        private string regionCode;
        private string regionName;

        public int QuizTopicId { get => quizTopicId; set => quizTopicId = value; }
        public string QuizTitle { get => quizTitle; set => quizTitle = value; }
        public float QuizTime { get => quizTime; set => quizTime = value; }
        public bool AllowMultipleInputByUser { get => allowMultipleInputByUser; set => allowMultipleInputByUser = value; }
        public bool AllowMultipleAnswer { get => allowMultipleAnswer; set => allowMultipleAnswer = value; }
        public bool AllowMultipleAttempt { get => allowMultipleAttempt; set => allowMultipleAttempt = value; }
        public bool AllowCorrectOption { get => allowCorrectOption; set => allowCorrectOption = value; }
        public bool AllowQuizStop { get => allowQuizStop; set => allowQuizStop = value; }
        public bool AllowQuizSkip { get => allowQuizSkip; set => allowQuizSkip = value; }
        public bool IsRunning { get => isRunning; set => isRunning = value; }
        public bool IsActive { get => isActive; set => isActive = value; }
        public DateTime QuizscheduleStartTime { get => quizscheduleStartTime; set => quizscheduleStartTime = value; }
        public DateTime QuizscheduleEndTime { get => quizscheduleEndTime; set => quizscheduleEndTime = value; }
        public int QuizTotalMarks { get => quizTotalMarks; set => quizTotalMarks = value; }
        public int QuizPassMarks { get => quizPassMarks; set => quizPassMarks = value; }
        public int QuizMarkOptionId { get => quizMarkOptionId; set => quizMarkOptionId = value; }
        public int QuizParticipantOptionId { get => quizParticipantOptionId; set => quizParticipantOptionId = value; }
        public int CertificateTemplateId { get => certificateTemplateId; set => certificateTemplateId = value; }
        public bool AllowQuestionSuffle { get => allowQuestionSuffle; set => allowQuestionSuffle = value; }
        public int QuizPrice { get => quizPrice; set => quizPrice = value; }
        public string Categories { get => categories; set => categories = value; }
        public bool IsMigrationData { get => isMigrationData; set => isMigrationData = value; }
        public DateTime DateAdded { get => dateAdded; set => dateAdded = value; }
        public DateTime LastUpdatedDate { get => lastUpdatedDate; set => lastUpdatedDate = value; }
        public int LastUpdatedBy { get => lastUpdatedBy; set => lastUpdatedBy = value; }
        public string PositionCode { get => positionCode; set => positionCode = value; }
        public string PositionName { get => positionName; set => positionName = value; }
        public string JobCode { get => jobCode; set => jobCode = value; }
        public string JobName { get => jobName; set => jobName = value; }
        public string DepartmentCode { get => departmentCode; set => departmentCode = value; }
        public string DepartmentName { get => departmentName; set => departmentName = value; }
        public string RegionCode { get => regionCode; set => regionCode = value; }
        public string RegionName { get => regionName; set => regionName = value; }
        public int AddedBy { get => addedBy; set => addedBy = value; }
        
    }
}
