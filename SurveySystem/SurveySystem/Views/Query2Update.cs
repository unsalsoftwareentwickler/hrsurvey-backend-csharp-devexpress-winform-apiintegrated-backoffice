using DevExpress.XtraBars.Docking2010;
using DevExpress.XtraEditors;
using SurveySystem.Entities;
using System;
using System.Drawing;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Windows.Forms;

namespace SurveySystem.Views
{
    public partial class Query2Update : DevExpress.XtraEditors.XtraForm
    {
        public UserUpdate userUpdate { get; set; }

        public Query2Update()
        {
            InitializeComponent();
        }

        public Image OpenImage(string previewFile)
        {
            FileStream fs = new FileStream(previewFile, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
            return Image.FromStream(fs);
        }

        private void windowsUIButtonPanelMain_ButtonClick(object sender, DevExpress.XtraBars.Docking2010.ButtonEventArgs e)
        {
            
        }

        private void SurveyEdit_FormClosed(object sender, FormClosedEventArgs e)
        {
            
        }

        private void Query2Update_Load_1(object sender, EventArgs e)
        {
            txtUserId.Text = userUpdate.userId.ToString();
            txtFullName.Text = userUpdate.fullName;
            txtEmail.Text = userUpdate.email;
            txtMobile.Text = userUpdate.mobile;
            txtAddress.Text = userUpdate.address;
            cmbxPosition.Text = userUpdate.positionName;
            cmbxDepartment.Text = userUpdate.departmentName;
            cmbxRegion.Text = userUpdate.regionName;

            if (!string.IsNullOrEmpty(userUpdate.imagePath))
            {
                string fileName = userUpdate.imagePath + ".jpg";
                string baseImageAddress = AppDomain.CurrentDomain.BaseDirectory;
                string imageFolder = "Resources\\Images\\";
                string completeImageDirectory = Path.Combine(Path.Combine(baseImageAddress, imageFolder), fileName);

                if (!File.Exists(completeImageDirectory))
                {
                    // do nothing
                }
                else
                {
                    Picture_Employee.Image = OpenImage(completeImageDirectory);
                    Picture_Employee.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Stretch;
                }
            }
            else
            {
                // do nothing
            }
        }
    }
}