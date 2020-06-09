using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Maratony.Data
{
    [Table("Biegacze")]
    public class BiegaczEF
    {
        #region Properties
        [Key]
        public long BiegaczID { get; set; }
        public string Imie { get; set; }
        public string Nazwisko { get; set; }
        public System.TimeSpan Czas { get; set; }

        [ForeignKey("Zawody")]
        public long ZawodyID { get; set; }
        public virtual ZawodyEF Zawody { get; set; }
        #endregion

        #region Constructor
        public BiegaczEF()
        {
            this.Czas = GenerujCzas();
        }

        public BiegaczEF(string imie, string nazwisko, long zawodyId):base()
        {
            this.Imie = imie;
            this.Nazwisko = nazwisko;
            this.ZawodyID = zawodyId;
        }
        #endregion



        #region Private_Methods
        private TimeSpan GenerujCzas()
        {
            var rnd = new Random();
            int godzina = rnd.Next(3, 6);
            int minuty = rnd.Next(0, 59);
            int sekundy = rnd.Next(0, 59);
            return new TimeSpan(godzina, minuty, sekundy);
        }
        #endregion
    }
}