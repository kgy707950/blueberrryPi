using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.DirectX;
//using System.Diagnostics;                   //프로세스 가동 여부
namespace smartfactory
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void RealTimeVideo_Enter(object sender, EventArgs e)
        {
            
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            //OpenFileDialog opn = new OpenFileDialog();
            this.Media.URL = @"D:\c#\friend.mp4";

        }
    }
}
