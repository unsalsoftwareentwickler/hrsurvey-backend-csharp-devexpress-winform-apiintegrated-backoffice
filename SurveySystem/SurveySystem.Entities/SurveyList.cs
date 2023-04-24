using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveySystem.Entities
{
    public class SurveyList
    {
        public int quizTopicId { get; set; }
        public string quizTitle { get; set; }
        public float quizTime { get; set; }
        public float quizTotalMarks { get; set; }
        public float quizPassMarks { get; set; }
        public int quizMarkOptionId { get; set; }
        public int quizParticipantOptionId { get; set; }
        public int? certificateTemplateId { get; set; }
        public bool allowMultipleInputByUser { get; set; }
        public bool allowMultipleAnswer { get; set; }
        public bool allowMultipleAttempt { get; set; }
        public bool allowCorrectOption { get; set; }
        public bool allowQuizStop { get; set; }
        public bool allowQuizSkip { get; set; }
        public bool allowQuestionSuffle { get; set; }
        public DateTime quizscheduleStartTime { get; set; }
        public DateTime quizscheduleEndTime { get; set; }
        public bool isRunning { get; set; }
        public int quizPrice { get; set; }
        public bool isActive { get; set; }
        public int questionsCount { get; set; }
    }

}
