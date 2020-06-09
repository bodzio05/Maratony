using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Maratony.Data
{
    [Table("Zawody")]
    public class ZawodyEF
    {
        [Key]
        public long ZawodyID { get; set; }
        public string Miejsce { get; set; }
        public DateTime Data { get; set; }
        public double Dystans { get; set; }
        public virtual ICollection<BiegaczEF> Biegacze { get; set; }
    }
}