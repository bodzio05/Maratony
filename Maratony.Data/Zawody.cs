using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace Maratony.Data
{
    [Table("Zawody")]
    public class Zawody
    {
        private static long nextId = 0;
        [Key]
        public long ID { get; set; }
        public string Miejsce { get; set; }
        public System.DateTime Data { get; set; }
        public double Dystans { get; set; }
        public virtual ICollection<Biegacz> Biegacze { get; set; }
    }

    //[Obsolete]
    //public class Zawody
    //{
    //    private static long nextId = 0;

    //    public Zawody()
    //    {
    //        this.ID = System.Threading.Interlocked.Increment(ref nextId);
    //        Biegacze = new List<Biegacz>();
    //    }

    //    public string Miejsce { get; set; }

    //    public System.DateTime Data { get; set; }

    //    public double Dystans { get; set; }

    //    public long ID { get; set; }

    //    public List<Biegacz> Biegacze { get; set; }
    //}
}