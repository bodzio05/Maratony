using System;
using System.Collections.Generic;

namespace Maratony.Data
{
    public class Biegacz
    {
        #region Properties
        public long ID { get; set; }
        public string Imie { get; set; }
        public string Nazwisko { get; set; }
        public System.TimeSpan Czas { get; set; }
        public Zawody Bieg { get; set; }
        #endregion

        #region Fields
        private static List<long> IDList = new List<long>();
        private static Random rnd = new Random();
        private static object lockObject = new object();
        #endregion

        #region Constructor
        public Biegacz()
        {
            this.ID = GenerujID();
            this.Czas = GenerujCzas();
        }
        #endregion

        #region Public_Methods
        public void ZwolnijID()
        {
            IDList.Remove(this.ID);
        }
        #endregion

        #region Private_Methods
        private long GenerujID()
        {
            long newId = 0;

            lock (lockObject)
            {
                for (int i = 0; i < 100; i++)
                {
                    if (!IDList.Contains(i))
                    {
                        newId = i;
                        IDList.Add(newId);
                        break;
                    }
                }
            }

            return newId;
        }

        private TimeSpan GenerujCzas()
        {
            int godzina = rnd.Next(3, 6);
            int minuty = rnd.Next(0, 59);
            int sekundy = rnd.Next(0, 59);
            return new TimeSpan(godzina, minuty, sekundy);
        }
        #endregion
    }
}