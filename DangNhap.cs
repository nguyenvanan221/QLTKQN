using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace DemoQLTKQN
{
    public partial class DangNhap : Form
    {
        string chuoiketnoi = @"Data Source=ADMIN\SQLEXPRESS;Initial Catalog=QLTKNET;Integrated Security=True";
        SqlConnection conn = null;
        string tk1 = "admin";
        string tk2 = "admin";
        public DangNhap()
        {
            InitializeComponent();
        }

        private void btn_DN_Click(object sender, EventArgs e)
        {

            try
            {
                conn = new SqlConnection(chuoiketnoi);
                Form1 frm = new Form1();
                conn.Open();
                //string tk = txt_TK.Text;
                //string mk = txt_MK.Text;
                //string sql = "select TK,MK from TTTK where TK = '" + tk + "' and MK = '" + mk + "'";
                //SqlCommand cmd = new SqlCommand(sql, conn);
                //SqlDataReader dta = cmd.ExecuteReader();
                //if (dta.Read() == true)
                if(txb_TK.Text == tk1 && txb_MK.Text == tk2)
                {
                    MessageBox.Show("Đăng nhập thành công!","Thông báo");

                    frm.Show();
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("Tên đăng nhập hoặc mật khẩu sai!","Thông báo");
                }
               
                conn.Close();
            }
            catch (Exception)
            {
                MessageBox.Show("Lỗi kết nối");
            }

            

        }

        private void btn_Thoat_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
