using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveySystem.Entities
{
    public class Question
    {
            public int quizQuestionId { get; set; }
            public int quizTopicId { get; set; }
            public string questionDetail { get; set; }
            public int serialNo { get; set; }
            public float perQuestionMark { get; set; }
            public int questionTypeId { get; set; }
            public int questionLavelId { get; set; }
            public int questionCategoryId { get; set; }
            public string optionA { get; set; }
            public string optionB { get; set; }
            public string optionC { get; set; }
            public string optionD { get; set; }
            public string optionE { get; set; }
            public string correctOption { get; set; }
            public object answerExplanation { get; set; }
            public object imagePath { get; set; }
            public object videoPath { get; set; }
            public bool isCodeSnippet { get; set; }
            public bool isActive { get; set; }
            public int addedBy { get; set; }
            public string questionCategoryName { get; set; }
    }
}
