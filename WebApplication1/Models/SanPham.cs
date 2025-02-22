﻿using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models
{
    public class SanPham
    {
        [Key]
        public int IdSP { get; set; }
        public string NameSP { get; set; }
        public string TrangThai { get; set; }

        public List<GioHangCT> GioHangCT { get; set; }

        public List<SanPhamCT> SanPhamCT { get; set;}
    }
}
