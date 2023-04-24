using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveySystem.Entities
{
    public class SurveyAdd
    {
            public string quizTitle { get; set; }
            public int quizTime { get; set; }
            public int quizTotalMarks { get; set; }
            public int quizPassMarks { get; set; }
            public int quizMarkOptionId { get; set; }
            public int quizParticipantOptionId { get; set; }
            public int certificateTemplateId { get; set; }
            public bool allowMultipleInputByUser { get; set; }
            public bool allowMultipleAnswer { get; set; }
            public bool allowMultipleAttempt { get; set; }
            public bool allowCorrectOption { get; set; }
            public bool allowQuizStop { get; set; }
            public bool allowQuizSkip { get; set; }
            public bool allowQuestionSuffle { get; set; }
            public DateTime quizscheduleStartTime { get; set; }
            public DateTime quizscheduleEndTime { get; set; }
            public int quizPrice { get; set; }
            public bool isRunning { get; set; }
            public string categories { get; set; }
            public bool isActive { get; set; }
            public bool isMigrationData { get; set; }
            public int addedBy { get; set; }
            public DateTime dateAdded { get; set; }
            public DateTime lastUpdatedDate { get; set; }
            public int lastUpdatedBy { get; set; }
            public string positionCode { get; set; }
            public string positionName { get; set; }
            public string jobCode { get; set; }
            public string jobName { get; set; }
            public string departmentCode { get; set; }
            public string departmentName { get; set; }
            public string regionCode { get; set; }
            public string regionName { get; set; }
    }
}
