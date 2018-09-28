using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace smartfactory
{
    public partial class 스마트생산라인 : Form
    {
        public 스마트생산라인()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
           
        }
        private void label1_Click(object sender, EventArgs e)
        {

        }
        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }
        private void button1_Click(object sender, EventArgs e)//button1(실시간화면)
        {
            //this.Hide();//감추기
            Form2 test = new Form2(); //새창만들고
            test.Show(); //test창열기
        }
        private void button2_Click(object sender, EventArgs e)
        {
            ListView View = new ListView();
            ListViewItem ivi = new ListViewItem("라면");
            ivi.SubItems.Add("1000");
            ivi.SubItems.Add("1000");
            ivi.SubItems.Add("박다정");
            listView1.Items.Add(ivi);

            //view1.Visible =true;
        }
        private void button3_Click(object sender, EventArgs e)
        {

        }
        private void button4_Click(object sender, EventArgs e)
        {

        }
        private void ListView1(object sender, EventArgs e)
        {
        }
        private void bindingNavigator1_RefreshItems(object sender, EventArgs e)
        {

        }

        private void axWindowsMediaPlayer1_Enter(object sender, EventArgs e)
        {

        }
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }
        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {

        }
        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {

        }
        private void checkBox4_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {

        }
    }
}
