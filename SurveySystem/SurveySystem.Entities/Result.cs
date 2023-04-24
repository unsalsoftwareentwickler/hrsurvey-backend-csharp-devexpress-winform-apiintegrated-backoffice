using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveySystem.Entities
{
    public class Result
    {
        public string fullName { get; set; }
        public string email { get; set; }
        public int attemptCount { get; set; }
        public int quizTopicId { get; set; }
        public int quizResponseInitialId { get; set; }
        public string quizTitle { get; set; }
        public float quizMark { get; set; }
        public int quizTime { get; set; }
        public float userObtainedQuizMark { get; set; }
        public object timeTaken { get; set; }
        public bool isExamined { get; set; }
        public DateTime dateAdded { get; set; }
        public int certificateTemplateId { get; set; }
        public bool allowCorrectOption { get; set; }
        public float quizPassMarks { get; set; }
        public int quizMarkOptionId { get; set; }
    }

}
