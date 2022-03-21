using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DemoQLTKQN
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        //form hien thi
        private void Form1_Load(object sender, EventArgs e)
        {
            HienThi();
        }

        //ket noi sql
        string chuoiketnoi = "Data Source=HID\\SQLEXPRESS;Initial Catalog=QLTKNET;Integrated Security=True";
        SqlConnection conn = null;

        // ham hien thi
        public void HienThi()
        {
            conn = new SqlConnection(chuoiketnoi);
            conn.Open();
            string sql = "Select * from TTTK";
            SqlDataAdapter da = new SqlDataAdapter(sql, conn);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
        }

        //Them TK
        private void buttonThem_Click(object sender, EventArgs e)
        {
            try
            {
                string them = "Insert into TTTK values(@ID, @TK, @MK, @TN)";
                SqlCommand cmd = new SqlCommand(them, conn);
                cmd.Parameters.AddWithValue("ID", textBoxMaTK.Text);
                cmd.Parameters.AddWithValue("TK", textBoxTenTK.Text);
                cmd.Parameters.AddWithValue("MK", textBoxMK.Text);
                cmd.Parameters.AddWithValue("TN", textBoxTien.Text);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Them thanh cong!");
                HienThi();
            }
            catch(Exception)
            {
                MessageBox.Show("ID da ton tai");
            }
        }

        //Sua TK
        private void buttonSua_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Ban co muon sua khong?", "Edit?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == System.Windows.Forms.DialogResult.Yes)
            {
                string sua = "Update TTTK set TK = @TK, MK = @MK, TN = @TN where ID = @ID";
                SqlCommand cmd = new SqlCommand(sua, conn);
                cmd.Parameters.AddWithValue("ID", textBoxMaTK.Text);
                cmd.Parameters.AddWithValue("TK", textBoxTenTK.Text);
                cmd.Parameters.AddWithValue("MK", textBoxMK.Text);
                cmd.Parameters.AddWithValue("TN", textBoxTien.Text);
                cmd.ExecuteNonQuery();
                HienThi();
            }
        }

        //Xoa TK
        private void buttonXoa_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Ban co muon xoa khong?", "Delete?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == System.Windows.Forms.DialogResult.Yes)
            {
                string xoa = "Delete from TTTK where ID = @ID";
                SqlCommand cmd = new SqlCommand(xoa, conn);
                cmd.Parameters.AddWithValue("ID", textBoxMaTK.Text);
                cmd.Parameters.AddWithValue("TK", textBoxTenTK.Text);
                cmd.Parameters.AddWithValue("MK", textBoxMK.Text);
                cmd.Parameters.AddWithValue("TN", textBoxTien.Text);
                cmd.ExecuteNonQuery();
                HienThi();
            }
        }

        //Tinh Tien Food
        private void buttonTinhTien_Click(object sender, EventArgs e)
        {
            int[] gia = { 20000, 10000, 5000, 10000 };
            int tong = 0;
            tong += (Convert.ToInt32(numericUpDown1.Value) * gia[0]);
            tong += (Convert.ToInt32(numericUpDown2.Value) * gia[1]);
            tong += (Convert.ToInt32(numericUpDown3.Value) * gia[2]);
            tong += (Convert.ToInt32(numericUpDown4.Value) * gia[3]);
            textBox2.Text = tong.ToString();
            
            string tinhtien = "Update TTTK set [TN] = [TN] - " + textBox2.Text + " where ID = " + textBox1.Text;
            SqlCommand cmd = new SqlCommand(tinhtien, conn);
            cmd.ExecuteNonQuery();
            MessageBox.Show("Thanh toán thành công");
        }

        //Phim Thoat
        private void buttonThoat_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Ban co muon thoat khong?", "Thoat?", MessageBoxButtons.YesNo);
            if (result == System.Windows.Forms.DialogResult.Yes)
            {
                Close();
            }
        }

        //Hien thi dong da click
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            textBoxMaTK.Text = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
            textBoxTenTK.Text = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
            textBoxMK.Text = dataGridView1.SelectedRows[0].Cells[2].Value.ToString();
            textBoxTien.Text = dataGridView1.SelectedRows[0].Cells[3].Value.ToString();
        }

        //Tim TK theo ID
        private void buttonTim_Click(object sender, EventArgs e)
        {
            try
            {
                string tim = "Select * from TTTK where ID = @ID";
                SqlCommand cmd = new SqlCommand(tim, conn);
                cmd.Parameters.AddWithValue("ID", textBoxTim.Text);
                cmd.Parameters.AddWithValue("TK", textBoxTenTK.Text);
                cmd.Parameters.AddWithValue("MK", textBoxMK.Text);
                cmd.Parameters.AddWithValue("TN", textBoxTien.Text);
                cmd.ExecuteNonQuery();
                SqlDataReader dr = cmd.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Load(dr);
                dataGridView1.DataSource = dt;
            }
            catch (Exception)
            {
                MessageBox.Show("Vui long nhap lai ID", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            textBoxTim.Text = null;
            textBoxTenTK.Text = null;
            textBoxMaTK.Text = null;
            textBoxMK.Text = null;
            textBoxTien.Text = null;
            HienThi();
        }
    }
}
