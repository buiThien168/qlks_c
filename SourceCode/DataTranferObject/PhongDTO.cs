using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataTranferObject
{
    public class PhongDTO
    {
        public int Ma { get; set; }
        public string Ten { get; set; }
        public int Tang { get; set; }
        public int MaLoaiPhong { get; set; }
        public string LP { get; set; }
    }

    public class GiaPhongDTO
    {
        public int Maloaigia { get; set; }
        public int MaLoaiPhong { get; set; }
        public float Gia { get; set; }
        public int Ma { get; set; }
        public string Ten { get; set; }
        public string tenPhong { get; set; }
        public string Tenloaigia { get; set; }

    }
}
