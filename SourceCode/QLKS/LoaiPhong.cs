using BusinessLayer;
using DataTranferObject;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

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
            txtTenPhong.Text = gridLoaiPhong.CurrentRow.Cells[1].Value.ToString();
            cbLoaiPhong.Text = gridLoaiPhong.CurrentRow.Cells[4].Value.ToString();
            cbTang.Text = gridLoaiPhong.CurrentRow.Cells[2].Value.ToString();
            txtMaP.Text = gridLoaiPhong.CurrentRow.Cells[0].Value.ToString();
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
            HienthiGiaPhong();
            LoadLoaiPhong();
            LoadLoaiPhong1();
            LoadLoaiGiaPhongPhong();
        }
        private void LoadLoaiPhong()
        {
            PhongBUS PhongBUS = new PhongBUS();
            cbLoaiPhong.DataSource = PhongBUS.LoadLoaiPhong();
            cbLoaiPhong.DisplayMember = "Ten";
            cbLoaiPhong.ValueMember = "Ma";
        }
        private void gridGia_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            HienthiThongtinGiaPhong();
        }
        private void HienthiThongtinGiaPhong()
        {
            cbloaiPhong1.Text = gridGia.CurrentRow.Cells[5].Value.ToString();
            cbLoaiGia.Text = gridGia.CurrentRow.Cells[6].Value.ToString();
            txtGiaPhong1.Text = gridGia.CurrentRow.Cells[2].Value.ToString();
            txtMagiaP.Text = gridGia.CurrentRow.Cells[3].Value.ToString();

        }
        private void HienthiGiaPhong()
        {
            PhongBUS PhongBUS = new PhongBUS();
            gridGia.DataSource = PhongBUS.LayDanhSachGiaPhong();
            gridGia.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            gridGia.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            gridGia.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            gridGia.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(206, 187, 214);


        }
        private void LoadLoaiPhong1()
        {
            PhongBUS PhongBUS = new PhongBUS();
            cbloaiPhong1.DataSource = PhongBUS.LoadLoaiPhong();
            cbloaiPhong1.DisplayMember = "Ten";
            cbloaiPhong1.ValueMember = "Ma";
        }

        private void LoadLoaiGiaPhongPhong()
        {
            PhongBUS PhongBUS = new PhongBUS();
            cbLoaiGia.DataSource = PhongBUS.LoadLoaiGiaPhongPhong();
            cbLoaiGia.DisplayMember = "Tenloaigia";
            cbLoaiGia.ValueMember = "Maloaigia";
        }
        private void btnUpdatePhong_Click(object sender, EventArgs e)
        {
            string checkForm = cbCheckFarm1.Text.ToString();
            var LoaiGia = cbLoaiGia.SelectedValue.ToString();
            var LoaiPhong = cbloaiPhong1.SelectedValue.ToString();
            var GiaP = txtGiaPhong1.Text.ToString();
            var Map = txtMagiaP.Text.ToString();
            if (string.IsNullOrEmpty(LoaiGia) || string.IsNullOrEmpty(LoaiPhong) || string.IsNullOrEmpty(GiaP))
            {
                MessageBoxDS m = new MessageBoxDS();
                MessageBoxDS.thongbao = "Dữ liệu trống vui lòng nhập đầy đủ";
                MessageBoxDS.maHinh = 1;
                m.ShowDialog();
            }
            GiaPhongDTO PhongDTO = new GiaPhongDTO();
            PhongDTO.Maloaigia = int.Parse(LoaiGia);
            PhongDTO.MaLoaiPhong = int.Parse(LoaiPhong);
            PhongDTO.Gia = float.Parse(GiaP);
            PhongDTO.Ma = int.Parse(Map);
            PhongBUS PhongBUS = new PhongBUS();
            if (checkForm == "Thêm")
            {
                if (PhongBUS.CheckGia(PhongDTO))
                {
                    if (PhongBUS.ThemGiaPhong(PhongDTO))
                    {
                        MessageBoxDS m = new MessageBoxDS();
                        MessageBoxDS.thongbao = "Thêm Gía Phòng thành công";
                        MessageBoxDS.maHinh = 1;
                        m.ShowDialog();
                        HienthiLoaiPhong();
                        HienthiGiaPhong();
                        LoadLoaiPhong();
                    }
                    else
                    {

                        MessageBoxDS m = new MessageBoxDS();
                        MessageBoxDS.thongbao = "Thêm Gía Phòng thất bại";
                        MessageBoxDS.maHinh = 1;
                        m.ShowDialog();
                    }
                }
                else
                {
                    MessageBoxDS m = new MessageBoxDS();
                    MessageBoxDS.thongbao = "Đã tồn tại";
                    MessageBoxDS.maHinh = 1;
                    m.ShowDialog();
                }
            }
            else if (checkForm == "Xóa")
            {
                if (PhongBUS.XoaGiaPhong(PhongDTO))
                {
                    MessageBoxDS m = new MessageBoxDS();
                    MessageBoxDS.thongbao = "Xóa Phòng thành công";
                    MessageBoxDS.maHinh = 1;
                    m.ShowDialog();
                    HienthiLoaiPhong();
                    HienthiGiaPhong();
                    LoadLoaiPhong();
                }
                else
                {

                    MessageBoxDS m = new MessageBoxDS();
                    MessageBoxDS.thongbao = "Xóa Phòng thất bại";
                    MessageBoxDS.maHinh = 1;
                    m.ShowDialog();
                }
            }
            else
            {
                if (PhongBUS.UpdateGiaPhong(PhongDTO))
                {
                    MessageBoxDS m = new MessageBoxDS();
                    MessageBoxDS.thongbao = "Update Phòng thành công";
                    MessageBoxDS.maHinh = 1;
                    m.ShowDialog();
                    HienthiLoaiPhong();
                    HienthiGiaPhong();
                    LoadLoaiPhong();
                }
                else
                {

                    MessageBoxDS m = new MessageBoxDS();
                    MessageBoxDS.thongbao = "Update Phòng thất bại";
                    MessageBoxDS.maHinh = 1;
                    m.ShowDialog();
                }
            }


        }

        private void bntCapNhatTheoGio_Click(object sender, EventArgs e)
        {
            string checkForm = cbbCheckFunction.Text.ToString();
            var tenPhong = txtTenPhong.Text.ToString();
            var LoaiPhong = cbLoaiPhong.SelectedValue.ToString();
            var Tang = cbTang.Text.ToString();
            var Map = txtMaP.Text.ToString();
            if (string.IsNullOrEmpty(tenPhong) || string.IsNullOrEmpty(LoaiPhong) || string.IsNullOrEmpty(Tang))
            {
                MessageBoxDS m = new MessageBoxDS();
                MessageBoxDS.thongbao = "Dữ liệu trống vui lòng nhập đầy đủ";
                MessageBoxDS.maHinh = 1;
                m.ShowDialog();
            }
            PhongDTO PhongDTO = new PhongDTO();
            PhongDTO.Ten = tenPhong;
            PhongDTO.Tang = int.Parse(Tang);
            PhongDTO.MaLoaiPhong = int.Parse(LoaiPhong);
            PhongDTO.Ma = int.Parse(Map);
            if (checkForm == "Thêm")
            {
                PhongBUS PhongBUS = new PhongBUS();
                if (PhongBUS.ThemPhong(PhongDTO))
                {
                    MessageBoxDS m = new MessageBoxDS();
                    MessageBoxDS.thongbao = "Thêm Phòng thành công";
                    MessageBoxDS.maHinh = 1;
                    m.ShowDialog();
                    HienthiLoaiPhong();
                    HienthiGiaPhong();
                    LoadLoaiPhong();
                }
                else
                {

                    MessageBoxDS m = new MessageBoxDS();
                    MessageBoxDS.thongbao = "Thêm Phòng thất bại";
                    MessageBoxDS.maHinh = 1;
                    m.ShowDialog();
                }
            }
            else if (checkForm == "Xóa")
            {
                PhongBUS PhongBUS = new PhongBUS();
                if (PhongBUS.XoaPhong(PhongDTO))
                {
                    MessageBoxDS m = new MessageBoxDS();
                    MessageBoxDS.thongbao = "Xóa Phòng thành công";
                    MessageBoxDS.maHinh = 1;
                    m.ShowDialog();
                    HienthiLoaiPhong();
                    HienthiGiaPhong();
                    LoadLoaiPhong();
                }
                else
                {

                    MessageBoxDS m = new MessageBoxDS();
                    MessageBoxDS.thongbao = "Xóa Phòng thất bại";
                    MessageBoxDS.maHinh = 1;
                    m.ShowDialog();
                }
            }
            else
            {
                PhongBUS PhongBUS = new PhongBUS();
                if (PhongBUS.UpdatePhong(PhongDTO))
                {
                    MessageBoxDS m = new MessageBoxDS();
                    MessageBoxDS.thongbao = "Update Phòng thành công";
                    MessageBoxDS.maHinh = 1;
                    m.ShowDialog();
                    HienthiLoaiPhong();
                    HienthiGiaPhong();
                    LoadLoaiPhong();
                }
                else
                {

                    MessageBoxDS m = new MessageBoxDS();
                    MessageBoxDS.thongbao = "Update Phòng thất bại";
                    MessageBoxDS.maHinh = 1;
                    m.ShowDialog();
                }
            }
        }
    }
}
