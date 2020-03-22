using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Maratony.Data;

namespace Maratony.Data.Test
{
    [TestClass]
    public class MaratonyModelTest
    {
        [TestMethod]
        public void TestNowyMaratonyModel_PusteKolekcje()
        {
            MaratonyModel model = new MaratonyModel();
            Assert.AreEqual(0, model.ListaZawodow.Count, "ListaZawodow powinna byc pusta");
            Assert.AreEqual(0, model.ListaBiegaczy.Count, "ListaBiegaczy powinna byc pusta");
        }

        [TestMethod]
        public void TestDodajZawody_ZawodyDodaneBiegaczeBezZmian()
        {
            MaratonyModel model = new MaratonyModel();
            Zawody zawody = model.DodajZawody("Kraków", new DateTime(2016, 1, 10), 11.6);
            Assert.AreEqual(1, model.ListaZawodow.Count, "ListaZawodow nie powinna byc pusta");
            Assert.AreEqual(0, zawody.Biegacze.Count, "ListaBiegaczy powinna byc pusta");
            Assert.AreSame(zawody, model.ListaZawodow[0]);
        }

        [TestMethod]
        public void TestDodajBiegacza_ZawodyZnalezioneBiegaczDodany()
        {
            MaratonyModel model = new MaratonyModel();
            Zawody zawody1 = model.DodajZawody("Kraków", new DateTime(2016, 1, 10), 11.6);
            Zawody zawody = model.DodajZawody("Warszawa", new DateTime(2016, 1, 23), 6);
            Biegacz biegacz = model.DodajBiegacza(zawody.ID, "Młody", "Bóg");
            Assert.AreEqual(0, zawody1.Biegacze.Count);
            Assert.AreEqual(1, zawody.Biegacze.Count);
            Assert.AreSame(biegacz, zawody.Biegacze[0]);
            Assert.AreSame(zawody, biegacz.Bieg);
            Assert.AreEqual(1, model.ListaBiegaczy.Count);
            Assert.AreSame(biegacz, model.ListaBiegaczy[0]);
        }

    }
}
