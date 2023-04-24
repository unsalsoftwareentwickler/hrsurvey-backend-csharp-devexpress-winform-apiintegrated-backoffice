using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveySystem.Entities
{
    public class Exam
    {
        public string fullName { get; set; }
        public string email { get; set; }
        public string mobile { get; set; }
        public string address { get; set; }
        public int attemptCount { get; set; }
        public string quizTitle { get; set; }
        public float quizMark { get; set; }
        public int quizTime { get; set; }
        public float userObtainedQuizMark { get; set; }
        public object timeTaken { get; set; }
        public string questionDetail { get; set; }
        public string userAnswer { get; set; }
        public bool isAnswerSkipped { get; set; }
        public string correctAnswer { get; set; }
        public object answerExplanation { get; set; }
        public float questionMark { get; set; }
        public float userObtainedQuestionMark { get; set; }
        public int certificateTemplateId { get; set; }
        public bool allowCorrectOption { get; set; }
        public float quizPassMarks { get; set; }
        public int quizMarkOptionId { get; set; }
    }

}
