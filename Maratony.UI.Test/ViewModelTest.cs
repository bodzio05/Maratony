using System;
using System.Linq;
using Maratony.UI.ViewModel;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Maratony.UI.Test
{
    [TestClass]
    public class ViewModelTest
    {
        [TestMethod]
        public void TestNowyViewModel_OdswiezZawody()
        {
            var vm = new MaintenanceFormViewModel(true); // konstruktor dla UT
            Assert.IsTrue(vm.Zawody.Count() > 0);
        }

        [TestMethod]
        public void TestNowyViewModel_OdswiezBiegaczy()
        {
            var vm = new MaintenanceFormViewModel(true); // konstruktor dla UT
            var wywolaneOdswiezBiegaczy = false;

            vm.PropertyChanged += (s, e) =>
            {
                if (e.PropertyName == "Biegacze")
                {
                    wywolaneOdswiezBiegaczy = true;
                }
            };

            vm.WybraneZawody = vm.Zawody.Skip(1).First();
            Assert.IsTrue(wywolaneOdswiezBiegaczy);
            Assert.IsTrue(vm.Biegacze.Count() > 0);
        }

    }
}
