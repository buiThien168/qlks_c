using DataAccesLayer;
using DataTranferObject;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public class PhongBUS
    {
        PhongDAO phongDAO = new PhongDAO();

        public PhongDTO[] LayDanhSachPhong()
        {
            return phongDAO.LayDanhSachPhong();
        }
        public GiaPhongDTO[] LayDanhSachGiaPhong()
        {
            return phongDAO.LayDanhSachGiaPhong();
        }

        public int LaySoLuongPhongLonNhatTrongCacTang()
        {
            return phongDAO.LaySoLuongPhongLonNhatTrongCacTang();
        }

		public DataTable LayDanhSachPhongTrong(string time1, string time2, int maLoaiPhong)
		{
			return phongDAO.LayDanhSachPhongTrong(time1, time2, maLoaiPhong);
		}

		public PhongDTO LayPhongTheoMaSo(int maPhong)
		{
			return phongDAO.LayPhongTheoMaSo(maPhong);
		}

		public float LayGiaPhong(int maLoaiGia,int maLoaiPhong)
		{
			return phongDAO.LayGiaPhong(maLoaiGia,maLoaiPhong);
		}

		public string LayLoaiPhong(int maLoaiPhong)
		{
			return phongDAO.LayLoaiPhong(maLoaiPhong);
		}

		public string LayLoaiDangKy(int maLoaiDangKy)
		{
			return phongDAO.LayLoaiDangKy(maLoaiDangKy);
		}

		public DataTable LoadLoaiPhong()
		{
			return phongDAO.LoadLoaiPhong();

        }
        public DataTable LoadLoaiGiaPhongPhong()
		{
			return phongDAO.LoadLoaiGiaPhongPhong();

        }
        public bool ThemPhong(PhongDTO phong)
        {
            return phongDAO.CapnhatPhong(phong);
        } 
        public bool ThemGiaPhong(GiaPhongDTO phong)
        {
            return phongDAO.ThemGiaPhong(phong);
        }
        public bool UpdatePhong(PhongDTO phong)
        {
            return phongDAO.UpdatePhong(phong);
        } 
        public bool UpdateGiaPhong(GiaPhongDTO phong)
        {
            return phongDAO.UpdateGiaPhong(phong);
        }
        public bool XoaPhong(PhongDTO phong)
        {
            return phongDAO.XoaPhong(phong);
        }
        public bool XoaGiaPhong(GiaPhongDTO phong)
        {
            return phongDAO.XoaGiaPhong(phong);
        }
        public bool CheckGia(GiaPhongDTO phong)
        {
            return phongDAO.CheckGia(phong);
        }
    }
}
