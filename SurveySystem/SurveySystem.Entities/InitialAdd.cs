using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveySystem.Entities
{
    public class InitialAdd
    {
        public int quizResponseInitialId { get; set; }
        public int userId { get; set; }
        public string email { get; set; }
        public int attemptCount { get; set; }
        public bool isExamined { get; set; }
        public int quizTopicId { get; set; }
        public string quizTitle { get; set; }
        public int quizMark { get; set; }
        public int quizPassMarks { get; set; }
        public int userObtainedQuizMark { get; set; }
        public int quizTime { get; set; }
        public int timeTaken { get; set; }
        public DateTime startTime { get; set; }
        public DateTime endTime { get; set; }
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
