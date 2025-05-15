using BusinessLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PresentationLayer
{
	public partial class LoaiPhong : Form
	{
		public LoaiPhong()
		{
			InitializeComponent();
		}

  

        private void gridLoaiPhong_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            HienthiThongtinLoaiPhong();
        }
        private void HienthiThongtinLoaiPhong()
        {
            txtGiaLoaiGio.Text = gridLoaiPhong.CurrentRow.Cells[1].Value.ToString();
        }
        private void HienthiLoaiPhong()
        {
            PhongBUS PhongBUS = new PhongBUS();
            gridLoaiPhong.DataSource = PhongBUS.LayDanhSachPhong();
            gridLoaiPhong.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            gridLoaiPhong.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            gridLoaiPhong.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            gridLoaiPhong.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(206, 187, 214);
        }

        private void LoaiPhong_Load(object sender, EventArgs e)
        {
            HienthiLoaiPhong();
        }
    }
}
