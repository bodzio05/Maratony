using System;
using System.Collections.Generic;
using System.Linq;

namespace Maratony.Data
{
    public class MaratonyModelEF
    {
        public static void ClearDb()
        {
            using (var context = new ZawodyDbContext())
            {
                //context.Biegacze.RemoveRange(context.Biegacze);
                //context.Zawody.RemoveRange(context.Zawody);
                context.Database.ExecuteSqlCommand("DELETE FROM dbo.Biegacze");
                context.Database.ExecuteSqlCommand("DELETE FROM dbo.Zawody");
                context.Database.ExecuteSqlCommand("DBCC CHECKIDENT ('dbo.Biegacze',RESEED,0)");
                context.Database.ExecuteSqlCommand("DBCC CHECKIDENT ('dbo.Zawody',RESEED,0)");
                context.SaveChanges();
            }
        }

        public static List<BiegaczEF> PobierzBiegaczy()
        {
            using (var context = new ZawodyDbContext())
            {
                return context.Biegacze.ToList();
            }
        }

        public static List<ZawodyEF> PobierzZawody()
        {
            using (var context = new ZawodyDbContext())
            {
                return context.Zawody.ToList();
            }
        }

        public void DodajBiegacza(long zawodyId, string imie, string nazwisko)
        {
            using (var context = new ZawodyDbContext())
            {
                var biegacz = new BiegaczEF(imie, nazwisko, zawodyId);
                context.Biegacze.Add(biegacz);
                context.SaveChanges();
            }
        }

        public void UsunBiegacza(long biegaczId)
        {
            using (var context = new ZawodyDbContext())
            {
                var biegacz = context.Biegacze.FirstOrDefault(b => b.BiegaczID == biegaczId);

                if (biegacz != null)
                {
                    context.Biegacze.Remove(biegacz);
                    context.SaveChanges();
                }
            }
        }

        public void DodajZawody(string miejsce, DateTime data, double dystans)
        {
            using (var context = new ZawodyDbContext())
            {
                var zawody = new ZawodyEF() { Miejsce = miejsce, Data = data, Dystans = dystans };
                context.Zawody.Add(zawody);
                context.SaveChanges();
            }
        }

        public void UsunZawody(long zawodyId)
        {
            using (var context = new ZawodyDbContext())
            {
                var zawody = context.Zawody.FirstOrDefault(z => z.ZawodyID == zawodyId);

                if (zawody != null)
                {
                    context.Zawody.Remove(zawody);
                    context.SaveChanges();
                }
            }
        }
    }
}
