using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;



namespace SQL_Con
{
    public partial class video : Form
    {

        public video()
        {
            InitializeComponent();
        }

        private void video_Load(object sender, EventArgs e)
        {
            this.Video1.URL = @"C:\Users\301-06\Desktop\제6회 컬투쇼 UCC 출품작 -운전광 엄마- 편 애니메이션입니다..mp4";
        }

        private void video_Close(object sender, EventArgs e)
        {
            MessageBox.Show("ByeBye");
        }
    }
}
