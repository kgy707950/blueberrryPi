using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace SQL_Con
{

    public partial class Form1 : Form
    {
        string strCon = "Server=localhost;Database=products;Uid=root;Pwd=Qkfkadml1!;SslMode=none;CharSet=utf8;";

        //DB연결 위한 String(서버,DB이름,ID,PW,등등)
        MySqlConnection con;    //DB 연결 클래스
  //      bool first = true;
  //      bool first2 = true;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //      string strCon = "Server=localhost;Database=products;Uid=root;Pwd=Qkfkadml1!;SslMode=none;";
            //      MySqlConnection con = new MySqlConnection(strCon);
            //con = new MySqlConnection(strCon);
            try
            {

              //  con.Open();
                //MessageBox.Show("MySQL 연결 성공");

            }
            catch
            {
               // con.Close();
               // MessageBox.Show("연결 실패");
                //Application.OpenForms["Form1"].Close();
            }
        }
        public void ConnectCheck()
        {
            try
            {
                con.Open();
                MessageBox.Show("MySQL 연결 성공");         //DB연결되면 성공 메시지
            }
            catch
            {
                con.Close();
                MessageBox.Show("MySQL 연결 실패");     //실패시실패 메시지
                //Application.OpenForms["고객정보등록"].Close();      // 실패시 폼을 닫아준다.
                return;
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            chart1.Visible = false;                         //차트 감추기
            con = new MySqlConnection(strCon);              //DB연결
            ConnectCheck();                                         //DB연결 체크

            listView1.View = View.Details;                      //list뷰 하기 위한 초기화(?)
            MySqlDataAdapter adapt = new MySqlDataAdapter("SELECT * FROM PROD_STAT", con); //상품 상태테이블을
            //SELECT해서 adapt 에 저장
            DataTable dt = new DataTable(); //테이블을 만든다.
            adapt.Fill(dt);                           //만든 테이블에 셀렉트해온값 저장
            listView1.Clear();                      //리스트 초기화
            listView1.Items.Clear();              //리스트 내용 초기화

            listView1.Columns.Add("PROD_ID");   //컬럼 값 설정
            listView1.Columns.Add("상태");
            listView1.Columns.Add("생산시간");
            listView1.Columns.Add("URL");

            for (int i = 0; i < dt.Rows.Count; i++)
            {                                              
                DataRow dr = dt.Rows[i];            //Rows를 저장할 dr 만들어서 row 저장

                ListViewItem listitem = new ListViewItem(dr["PROD_ID"].ToString()); //각 컬럼에 맞는값을 listitem에 저장
//                MessageBox.Show(dr["PROD_ID"].ToString());
                listitem.SubItems.Add(dr["PROD_STA"].ToString());
//               MessageBox.Show(dr["PROD_STA"].ToString());
                listitem.SubItems.Add(dr["PROD_DATE"].ToString());
                //MessageBox.Show(dr["PROD_DATE"].ToString());
                listitem.SubItems.Add(dr["PROD_URL"].ToString());
                // MessageBox.Show(dr["PROD_URL"].ToString());
                listView1.Items.Add(listitem);              //listview1에 저장(출력)
            }
            con.Close();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void listView1_SelectedIndexChanged_1(object sender, EventArgs e)
        {

        }

        private void listView1_MouseClick(object sender, MouseEventArgs e)
        {
            if(e.Button == MouseButtons.Right)
            {
                System.Diagnostics.Process.Start(listView1.SelectedItems[0].SubItems[3].Text.ToString());  
            }
        }

        private void Line_Stat_Click(object sender, EventArgs e)
        {
            chart1.Visible = false;
            con = new MySqlConnection(strCon);              //DB연결
            ConnectCheck();                                         //DB연결 체크
          
            listView1.View = View.Details;                       //list뷰 하기 위한 초기화(?)
            MySqlDataAdapter adapt = new MySqlDataAdapter("SELECT * FROM LINE_STAT", con);//LINE 상태테이블을
            //SELECT해서 adapt 에 저장
            DataTable dt = new DataTable();     //테이블을 만든다.
            adapt.Fill(dt);                              //만든 테이블에 셀렉트해온값 저장
            listView1.Clear();                      //리스트 초기화
            listView1.Items.Clear();              //리스트 내용 초기화

            listView1.Columns.Add("온도");
            listView1.Columns.Add("습도");
            listView1.Columns.Add("체크시간");


            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DataRow dr = dt.Rows[i];                //위에 봐 귀찮아

                //MessageBox.Show("for문 입성"+i);

                ListViewItem listitem = new ListViewItem(dr["TEMPER"].ToString());
                //                MessageBox.Show(dr["PROD_ID"].ToString());
                listitem.SubItems.Add(dr["HUMIDITY"].ToString());
                //               MessageBox.Show(dr["PROD_STA"].ToString());
                listitem.SubItems.Add(dr["CHECK_TIME"].ToString());
                //              MessageBox.Show(dr["PROD_DATE"].ToString());

                listView1.Items.Add(listitem);
            }
            con.Close();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            chart1.Visible = true;

            con = new MySqlConnection(strCon);              //DB연결
            ConnectCheck();                                         //DB연결 체크

            MySqlDataAdapter adapt = new MySqlDataAdapter("SELECT * FROM LINE_STAT", con);
            DataTable dt = new DataTable();
            adapt.Fill(dt);

            chart1.Series.Clear();      // Default 삭제
            Series temper = chart1.Series.Add("온도");
            Series humidity = chart1.Series.Add("습도");
            temper.ChartType = SeriesChartType.Line;
            humidity.ChartType = SeriesChartType.Line;

            for(int i=0;i<dt.Rows.Count;i++)
            {
                DataRow dr = dt.Rows[i];

                temper.Points.AddXY(dr["CHECK_TIME"].ToString(), Convert.ToInt32(dr["TEMPER"].ToString()));
                humidity.Points.AddXY(dr["CHECK_TIME"].ToString(), Convert.ToInt32(dr["HUMIDITY"].ToString()));
            }

        }

        private void chart1_Click(object sender, EventArgs e)
        {

        }
    }
}
