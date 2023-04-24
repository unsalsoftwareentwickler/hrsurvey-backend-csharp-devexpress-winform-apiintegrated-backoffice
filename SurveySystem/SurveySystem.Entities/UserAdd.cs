using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveySystem.Entities
{
    public class UserAdd
    {
        public int userRoleId { get; set; }
        public string fullName { get; set; }
        public string mobile { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public string address { get; set; }
        public DateTime dateOfBirth { get; set; }
        public string imagePath { get; set; }
        public string stripeSessionId { get; set; }
        public int billingPlanId { get; set; }
        public int paymentId { get; set; }
        public string paymentMode { get; set; }
        public string transactionDetail { get; set; }
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
