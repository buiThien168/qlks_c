using DataTranferObject;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccesLayer
{
    public class PhongDAO
    {
        DataProvider dataProvider = new DataProvider();
        public PhongDTO[] LayDanhSachPhong( )
        {
            PhongDTO[] phongs = null;
            string query = "select * from Phong t1 left join Loaiphong t2 on t1.Maloaiphong=t2.Ma";
            DataTable tb = new DataTable();
            tb = dataProvider.ExecuteQuery_DataTble(query);
            phongs = new PhongDTO[tb.Rows.Count];

            int n = tb.Rows.Count;

            for (int i = 0; i < n; i++)
            {
                phongs[i] = ChuyenRowThanhObject(tb.Rows[i]);
            }
            return phongs;
        }

        public GiaPhongDTO[] LayDanhSachGiaPhong()
        {
            GiaPhongDTO[] Giaphongs = null;
            string query = "select t3.Ten as tenPhong,t2.Tenloaigia,t1.* from Giaphong t1 left join LoaiGia t2 ON t1.Maloaigia=t2.Maloaigia left join Loaiphong t3 ON T1.Maloaiphong=t3.Ma\r\n";
            DataTable tb = new DataTable();
            tb = dataProvider.ExecuteQuery_DataTble(query);
            Giaphongs = new GiaPhongDTO[tb.Rows.Count];

            int n = tb.Rows.Count;

            for (int i = 0; i < n; i++)
            {
                Giaphongs[i] = ChuyenRowThanhObjectGiaPhong(tb.Rows[i]);
            }
            return Giaphongs;
        }
        

        public PhongDTO ChuyenRowThanhObject(DataRow row)
        {
            PhongDTO phong = new PhongDTO();
            phong.Ma = int.Parse(row[0].ToString());
            phong.Ten = row[1].ToString();
            phong.Tang = int.Parse(row[2].ToString());
            phong.MaLoaiPhong = int.Parse(row[3].ToString());
            phong.LP = row[5].ToString();

            return phong;
        }

        public GiaPhongDTO ChuyenRowThanhObjectGiaPhong(DataRow row)
        {
            GiaPhongDTO phong = new GiaPhongDTO();
            phong.tenPhong = row[0]?.ToString();
            phong.Tenloaigia = row[1]?.ToString();
            phong.Ma = int.Parse(row[2]?.ToString());
            phong.Maloaigia = int.Parse(row[3]?.ToString());
            phong.MaLoaiPhong = int.Parse(row[4]?.ToString());
            phong.Gia = float.Parse(row[5]?.ToString());
            return phong;
    }

        public int LaySoLuongPhongLonNhatTrongCacTang()
        {
            string query = "select p.Tang , COUNT(p.Ma) as Maxnumber " +
                "from Phong as p " +
                "group by p.Tang having COUNT(p.Ma) >= all ( " +
                "select COUNT(ph.Ma) as countmax " +
                "from Phong as ph " +
                "group by ph.Tang" +
                ")";
            DataTable tb = new DataTable();
            tb = dataProvider.ExecuteQuery_DataTble(query);
            return int.Parse(tb.Rows[0][1].ToString());
        }

		public DataTable LayDanhSachPhongTrong(string time1, string time2, int maLoaiPhong)
		{
			DataTable dataTable = new DataTable();
			string query = "Select * From Phong where Ma not in " +
				"(Select Maphong From Phieuthuephong where(TrangThai = 1 or TrangThai = 2) " +
				"and Thoigiannhanphong >= '"+ time1 + "' " +
				"and Thoigiantraphong <= '"+ time2 + "' )";
			dataTable = dataProvider.ExecuteQuery_DataTble(query);
			return dataTable;
		}

		public PhongDTO LayPhongTheoMaSo(int maPhong)
		{
			PhongDTO phongDTO = new PhongDTO();
			string query = "Select * from Phong where Ma = "+maPhong+"";
			DataTable tb = new DataTable();
			tb = dataProvider.ExecuteQuery_DataTble(query);
			if(tb.Rows.Count != 0)
			{
				phongDTO.Ma = maPhong;
				phongDTO.Ten = tb.Rows[0]["Ten"].ToString();
				phongDTO.Tang = int.Parse(tb.Rows[0]["Tang"].ToString());
				phongDTO.MaLoaiPhong = int.Parse(tb.Rows[0]["Maloaiphong"].ToString());
			}
			return phongDTO;
		}

		public float LayGiaPhong(int maLoaiGia, int maLoaiPhong)
		{
			float giaPhong = 0;
			string query = "Select Gia from Giaphong where Maloaigia = "+maLoaiGia+" and Maloaiphong = "+maLoaiPhong+"";
			DataTable tb = new DataTable();
			tb = dataProvider.ExecuteQuery_DataTble(query);
			if (tb.Rows.Count != 0)
			{
				giaPhong = float.Parse(tb.Rows[0]["Gia"].ToString());
			}
			return giaPhong;
		}

		public string LayLoaiPhong(int maLoaiPhong)
		{
			string query = "Select Ten from Loaiphong where Ma =  " + maLoaiPhong + "";
			string loaiPhong = "";
			DataTable tb = new DataTable();
			tb = dataProvider.ExecuteQuery_DataTble(query);
			if (tb.Rows.Count != 0)
			{
				loaiPhong = tb.Rows[0]["Ten"].ToString();
			}
			return loaiPhong;
		}
		
		public string LayLoaiDangKy(int maLoaiDangKy)
		{
			string query = "Select Tenloaigia from LoaiGia where Maloaigia =  " + maLoaiDangKy + "";
			string loaiDangKy = "";
			DataTable tb = new DataTable();
			tb = dataProvider.ExecuteQuery_DataTble(query);
			if (tb.Rows.Count != 0)
			{
				loaiDangKy = tb.Rows[0]["Tenloaigia"].ToString();
			}
			return loaiDangKy;
		}

        public DataTable LoadLoaiPhong()
        {
            string query = "select Ma,Ten from Loaiphong";
            return dataProvider.ExecuteQuery_DataTble(query);
        }
        public DataTable LoadLoaiGiaPhongPhong()
        {
            string query = "SELECT Maloaigia,Tenloaigia FROM LoaiGia";
            return dataProvider.ExecuteQuery_DataTble(query);
        }

        public bool CapnhatPhong(PhongDTO nv)
        {
            string query = "insert into Phong(Ten,Tang,Maloaiphong) Values (N'"+nv.Ten+ "',"+nv.Tang+ ","+nv.MaLoaiPhong+")";
            try
            {
                dataProvider.ExecuteUpdateQuery(query);
                return true;
            }
            catch
            {
                return false;
            }
        }
        public bool ThemGiaPhong(GiaPhongDTO nv)
        {
            string query = "insert into Giaphong(Maloaigia,Maloaiphong,Gia) Values (" + nv.Maloaigia+ ","+nv.MaLoaiPhong+ ","+nv.Gia+")";
            try
            {
                dataProvider.ExecuteUpdateQuery(query);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool UpdatePhong(PhongDTO nv)
        {
            string query = "update Phong set Ten = N'" + nv.Ten + "',Tang = " + nv.Tang + " , Maloaiphong=" + nv.MaLoaiPhong + " WHERE Ma="+nv.Ma+"";
            try
            {
                dataProvider.ExecuteUpdateQuery(query);
                return true;
            }
            catch
            {
                return false;
            }
        } 
        public bool UpdateGiaPhong(GiaPhongDTO nv)
        {
            string query = "update Giaphong set Maloaigia = " + nv.Maloaigia + ",Maloaiphong = " + nv.MaLoaiPhong + " , Gia=" + nv.Gia + " WHERE Ma=" + nv.Ma+"";
            try
            {
                dataProvider.ExecuteUpdateQuery(query);
                return true;
            }
            catch
            {
                return false;
            }
        }
        public bool XoaPhong(PhongDTO nv)
        {
            string query = "DELETE FROM Phong where Ma = "+nv.Ma+"";
            try
            {
                dataProvider.ExecuteUpdateQuery(query);
                return true;
            }
            catch
            {
                return false;
            }
        } 
        public bool XoaGiaPhong(GiaPhongDTO nv)
        {
            string query = "DELETE FROM Giaphong where Ma = " + nv.Ma+"";
            try
            {
                dataProvider.ExecuteUpdateQuery(query);
                return true;
            }
            catch
            {
                return false;
            }
        }
        public bool CheckGia(GiaPhongDTO nv)
        {
            string query = "select * from Giaphong WHERE Maloaiphong="+nv.MaLoaiPhong+ " AND Maloaigia=" + nv.Maloaigia + "";
            DataTable tb = new DataTable();
            tb = dataProvider.ExecuteQuery_DataTble(query);
            if (tb.Rows.Count > 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

    }
}
