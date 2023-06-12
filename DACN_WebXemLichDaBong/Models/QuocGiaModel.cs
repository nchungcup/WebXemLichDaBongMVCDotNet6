using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DACN_WebXemLichDaBong.Models
{
    [Table("QuocGia")]
    public class QuocGiaModel
    {
        [Key]
        public int QuocGiaId { get; set; }
        public string TenQuocGia { get; set; }
    }
}