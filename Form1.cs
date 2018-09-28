/*
 * 제품명 : 루베리파이 생산라인
 * 조이름 : 블루베리파이-
 * 제작자 :  -김강영-  -이제우-  -박다정-  -노기종-
 * 버전 :     V_0.000001
 * 날짜 :    2018-09-28
 */

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

namespace smartfactory
{
    public partial class 스마트생산라인 : Form
    {

        string strCon = "Server=localhost;Database=products;Uid=root;Pwd=Qkfkadml1!;SslMode=none;CharSet=utf8;";
        //DB연결 위한 String(서버,DB이름,ID,PW,등등)
        MySqlConnection con;    //DB 연결 클래스
        public 스마트생산라인()
        {
            InitializeComponent();
        }
        public void ConnectCheck()          //DB 연결확인 함수
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

        private void Form1_Load(object sender, EventArgs e)  //Form1 켜질때
        {
            listView1.Items.Clear();              //생산량 리스트 내용 초기화
        }
        private void label1_Click(object sender, EventArgs e){}//블루베리 생산라인 제목
        private void label2_Click(object sender, EventArgs e){}//작업자 제목
        private void label3_Click(object sender, EventArgs e){}// 날짜 제목
        
        private void button1_Click(object sender, EventArgs e)//button1(실시간화면)
        {
            //this.Hide();//form1 감추기
            Form2 test = new Form2(); //새창만들고
            test.Show(); //test창열기
        }
        private void button2_Click(object sender, EventArgs e)//생산량 확인 (리스트트뷰)
        {
            this.button5.Visible = true;
            this.listView2.Visible = false;                         //상품전체리스트뷰 안보임
            this.listView1.Show();                                  //생산량 확인 리스트뷰보임
            chart1.Visible = false;                                 //온습도 그래프 안보임
            con = new MySqlConnection(strCon);              //DB연결
            ConnectCheck();
            string temp2 = Date.Text.ToString();              //DateCheck에서 값 읽어와서 저장하기

            MySqlDataAdapter adapt = new MySqlDataAdapter("SELECT * FROM PROD_STAT WHERE PROD_DATE LIKE '" + temp2 + "%'", con);
            //날자에 맞게 검색하여 들고오기(MySQL)

            //MySqlDataAdapter adapt = new MySqlDataAdapter("SELECT * FROM PROD_STAT", con);//LINE 상태테이블을
            //SELECT해서 adapt 에 저장
            DataTable dt = new DataTable();     //테이블을 만든다.
            adapt.Fill(dt);                              //만든 테이블에 셀렉트해온값 저장

            if (dt.Rows.Count == 0)                 //데이터가 없을떄 처리되는 구간
            {
                MessageBox.Show("데이터가 없습니다.");
                ListViewItem ivi2 = new ListViewItem("--");      //상품명
                ivi2.SubItems.Add("--");                               //총 생산량
                ivi2.SubItems.Add("--");                               //불량률
                listView1.Items.Add(ivi2);
                return;
            }
            DataRow dr = dt.Rows[0];    //나도 모르겠아요 //상품명 읽어오기 위해서 첫번째 ROW를 dr에 저장.

            MySqlDataAdapter adapt2 = new MySqlDataAdapter("SELECT * FROM PROD_STAT WHERE PROD_STA = 'N'", con);//DB연결코드
            DataTable temp = new DataTable();//DB객체 생성
            adapt2.Fill(temp);//DB채움

            int defact = temp.Rows.Count;   //불량품 갯수

            int num = dt.Rows.Count;            //총 물품 갯수
            double defact_rate = (double)defact / (double)num;// * 100.0;   //불량률

            ListView View = new ListView();
            ListViewItem ivi = new ListViewItem(dr["PROD_NAME"].ToString());      //상품명 //
            ivi.SubItems.Add(num.ToString());                                                //총 생산량
            ivi.SubItems.Add(defact_rate.ToString("P2"));                                  //불량률

            if(radioButton1.Checked)                            //작업자 라디오 체크 
            {
                ivi.SubItems.Add(radioButton1.Text.ToString());//김강영text_를 넣어줌
            }
            if (radioButton2.Checked) //이제우
            {
                ivi.SubItems.Add(radioButton2.Text.ToString());
            }
            if (radioButton3.Checked)//노기종
            {
                ivi.SubItems.Add(radioButton3.Text.ToString());
            }
            if (radioButton4.Checked)//박다정
            {
                ivi.SubItems.Add(radioButton4.Text.ToString());
            }
            //ivi.SubItems.Add("박다정");
            listView1.Items.Add(ivi);
            //view1.Visible =true;
        }
        private void button3_Click(object sender, EventArgs e)//온습도 확인(그래프)
        {
            //this.listView1.Hide();
            this.button5.Visible = false;
            this.listView2.Visible = false;
            chart1.Visible = true;
            con = new MySqlConnection(strCon);              //DB연결
            ConnectCheck();                                         //DB연결 체크

            string temp = Date.Text.ToString();
            //MySqlDataAdapter adapt = new MySqlDataAdapter("SELECT * FROM LINE_STAT", con);
            MySqlDataAdapter adapt = new MySqlDataAdapter("SELECT * FROM LINE_STAT WHERE CHECK_TIME LIKE '" + temp + "%'", con);
            DataTable dt = new DataTable();
            adapt.Fill(dt);

            chart1.Series.Clear();      // Default 삭제
            Series temper = chart1.Series.Add("온도");
            Series humidity = chart1.Series.Add("습도");
            temper.ChartType = SeriesChartType.Line;
            humidity.ChartType = SeriesChartType.Line;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DataRow dr = dt.Rows[i];

                temper.Points.AddXY(dr["CHECK_TIME"].ToString(), Convert.ToInt32(dr["TEMPER"].ToString()));
                humidity.Points.AddXY(dr["CHECK_TIME"].ToString(), Convert.ToInt32(dr["HUMIDITY"].ToString()));
            }
        }
        private void button4_Click(object sender, EventArgs e)//완성품 확인(DB연동, 사진부름)
        {
            this.button5.Visible = false;
            chart1.Visible = false ;
            this.listView2.Visible = true;
            con = new MySqlConnection(strCon);              //DB연결
            ConnectCheck();                                         //DB연결 체크

            MySqlDataAdapter adapt = new MySqlDataAdapter("SELECT * FROM PROD_STAT", con); //상품 상태테이블을
            //SELECT해서 adapt 에 저장
            DataTable dt = new DataTable();      //테이블을 만든다.
            adapt.Fill(dt);                                //만든 테이블에 셀렉트해온값 저장
            //  listView1.Items.Clear();                //생산량 리스트 내용 초기화
            listView2.Items.Clear();                   //완성품 리스트 내용 초기화
         
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DataRow dr = dt.Rows[i];            //Rows를 저장할 dr 만들어서 row 저장

                ListViewItem listitem = new ListViewItem(dr["PROD_ID"].ToString()); //각 컬럼에 맞는값을 listitem에 저장
                                                                                    // MessageBox.Show(dr["PROD_ID"].ToString());
                listitem.SubItems.Add(dr["PROD_STA"].ToString());
                                                                                    //MessageBox.Show(dr["PROD_STA"].ToString());
                listitem.SubItems.Add(dr["PROD_DATE"].ToString());
                                                                                    //MessageBox.Show(dr["PROD_DATE"].ToString());
                listitem.SubItems.Add(dr["PROD_URL"].ToString());
                                                                                    // MessageBox.Show(dr["PROD_URL"].ToString());
                listView2.Items.Add(listitem);              //listview1에 저장(출력)
            }
            con.Close();
        }
        private void ListView1(object sender, EventArgs e){}//생산량(뷰리스트)
        private void axWindowsMediaPlayer1_Enter(object sender, EventArgs e){}//플레이어
        private void button5_Click(object sender, EventArgs e)      //생산량 지움 함수
        {
            listView1.Items.Clear();              //생산량 리스트 내용 초기화
            //listView2.Items.Clear();               //완성품 리스트 내용 초기화
        }

        private void chart1_Click(object sender, EventArgs e){ }//그래프함수
        private void listView2_MouseClick(object sender, MouseEventArgs e)  //전체 물품에서 마우스 클릭에 대한 이벤트 발생
        {
            if(e.Button == MouseButtons.Right)          //우클릭 시 발생하는 이벤트
            {
                System.Diagnostics.Process.Start(listView2.SelectedItems[0].SubItems[3].Text.ToString());       //파일 열수 있는....?
            }
            /*
             * ListView 는 lstDocView 이것이다.
                txtSectorCd.Text = lstDocView.SelectedItems[0].SubItems[0].Text;   // <- 이것이 첫번째 헤더
                txtDocNo.Text = lstDocView.SelectedItems[0].SubItems[1].Text;     // <- 이것이 두번째 헤더
                txtDocTitle.Text = lstDocView.SelectedItems[0].SubItems[2].Text;  // <- 이것이 세번째 헤더
                txtDocCode.Text = lstDocView.SelectedItems[0].SubItems[3].Text;    // <- 이것이 네번째 헤더
                txtDocWriteDate.Text = lstDocView.SelectedItems[0].SubItems[4].Text;   // <- 이것이 다섯번째 헤더
             */
        }
        private void listView2_SelectedIndexChanged(object sender, EventArgs e)//완성품(뷰리스트)
        {
        }
    }
}
