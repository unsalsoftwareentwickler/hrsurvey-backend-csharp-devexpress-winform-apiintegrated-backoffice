using System;

namespace SurveySystem.Entities
{
    public class LoginDetail
    {
        private string token;
        private int id;
        private string email;
        private string password;
        private string fullname;
        private string mobile;
        private string address;
        private string imagepath;
        private string addedby;
        private DateTime birthdate;
        private string position;
        private string job;
        private string department;
        private string region;

        public string Token
        {
            get { return token; }
            set { token = value; }
        }
        public int Id
        {
            get { return id; }
            set { id = value; }
        }
        public string Email
        {
            get { return email; }
            set { email = value; }
        }
        public string Password
        {
            get { return password; }
            set
            {
                password = value;
            }
        }
        public string FullName
        {
            get { return fullname; }
            set
            {
                fullname = value;
            }
        }
        public string Mobile
        {
            get { return mobile; }
            set
            {
                mobile = value;
            }
        }
        public string Address
        {
            get { return address; }
            set
            {
                address = value;
            }
        }
        public string ImagePath
        {
            get { return imagepath; }
            set
            {
                imagepath = value;
            }
        }
        public string AddedBy
        {
            get { return addedby; }
            set
            {
                addedby = value;
            }
        }
        public DateTime BirthDate
        {
            get { return birthdate; }
            set
            {
                birthdate = value;
            }
        }
        public string Position
        {
            get { return position; }
            set
            {
                position = value;
            }
        }
        public string Job
        {
            get { return job; }
            set
            {
                job = value;
            }
        }
        public string Department
        {
            get { return department; }
            set
            {
                department = value;
            }
        }
        public string Region
        {
            get { return region; }
            set
            {
                region = value;
            }
        }
    }
}
