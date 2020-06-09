using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Maratony.Data;
using System.ComponentModel;
using System.Windows.Input;
using System.Windows.Controls;

namespace Maratony.UI.ViewModel
{
    public class MaintenanceFormViewModel : ViewModelBase
    {
        #region Properties       
        private ZawodyEF wybraneZawody;
        public ZawodyEF WybraneZawody
        {
            get
            {
                return this.wybraneZawody;
            }
            set
            {
                this.wybraneZawody = value;
                this.OnPropertyChanged();
                this.OdswiezAktywnychBiegaczy();
                this.OnPropertyChanged(nameof(BiegaczeWBiegu));
            }
        }

        private List<BiegaczEF> biegaczeWBiegu;
        public List<BiegaczEF> BiegaczeWBiegu
        {
            get 
            { 
                return biegaczeWBiegu; 
            }
            set 
            { 
                biegaczeWBiegu = value;
                OnPropertyChanged();
            }
        }

        private List<BiegaczEF> biegacze;
        public List<BiegaczEF> Biegacze
        {
            get
            {
                return this.biegacze;
            }
            set
            {
                this.biegacze = value;
                OnPropertyChanged();
            }
        }

        private List<ZawodyEF> zawody;
        public List<ZawodyEF> Zawody
        {
            get
            {
                return this.zawody;
            }
            set
            {
                this.zawody = value;
                this.OnPropertyChanged();
            }
        }

        private string imieTextBox;
        public string ImieTextBox 
        { 
            get
            {
                return this.imieTextBox;
            }
            set
            {
                this.imieTextBox = value;
                this.OnPropertyChanged();
            }
        }

        private string nazwiskoTextBox;
        public string NazwiskoTextBox
        {
            get
            {
                return this.nazwiskoTextBox;
            }
            set
            {
                this.nazwiskoTextBox = value;
                this.OnPropertyChanged();
            }
        }

        private List<BiegaczEF> wybraniBiegacze = new List<BiegaczEF>();
        public List<BiegaczEF> WybraniBiegacze
        {
            get
            {
                return this.wybraniBiegacze;
            }
            set
            {
                this.wybraniBiegacze = value;
                this.OnPropertyChanged();
            }
        }
        #endregion

        #region Fields
        private MaratonyModelEF model = new MaratonyModelEF();
        #endregion

        #region Commands
        public ICommand AddCommand
        {
            get; private set;
        }

        public ICommand ClearCommand
        {
            get; private set;
        }

        public ICommand DeleteCommand
        {
            get; private set;
        }

        public ICommand DataGridSelectionChangedCommand
        {
            get; private set;
        }       
        #endregion

        #region Constructors
        public MaintenanceFormViewModel()
        {
            Init();
        }

        #endregion

        #region Private methods
        private void DodajBiegacza()
        {
            model.DodajBiegacza(this.WybraneZawody.ZawodyID,
                (imieTextBox == null || imieTextBox == "") ? "Imie" : imieTextBox,
                (nazwiskoTextBox == null || nazwiskoTextBox == "") ? "Nazwisko" : nazwiskoTextBox);
            this.OdswiezBiegaczy();
            this.OdswiezAktywnychBiegaczy();
        }

        private void OdswiezBiegaczy()
        {
            this.Biegacze = null;
            this.Biegacze = MaratonyModelEF.PobierzBiegaczy();
        }

        private void OdswiezAktywnychBiegaczy()
        {
            this.BiegaczeWBiegu = null;
            if (WybraneZawody != null)
            {
                this.BiegaczeWBiegu = Biegacze.Where(b => b.ZawodyID == WybraneZawody.ZawodyID).ToList();
            }
        }

        private bool CzyMoznaDodacBiegacza()
        {
            return (this.WybraneZawody != null);
        }

        private void OdswiezZawody()
        {
            this.Zawody = null;
            this.Zawody = MaratonyModelEF.PobierzZawody();
        }

        private void WyczyscWybraneZawody()
        {
            this.WybraneZawody = null;
        }

        private void UsunWybranychBiegaczy()
        {
            foreach (var biegacz in WybraniBiegacze)
            {
                model.UsunBiegacza(biegacz.BiegaczID);
            }

            OdswiezBiegaczy();
            OdswiezAktywnychBiegaczy();
        }

        private bool CzyMoznaUsunacBiegaczy()
        {
            if (this.wybraniBiegacze != null)
            {
                //return true;
                return (this.wybraniBiegacze.Count != 0);
            }

            return false;
        }

        private bool CzyMoznaWyczyscicZawody()
        {
            return (this.WybraneZawody != null);
        }

        private void AktualizujZaznaczonychBiegaczy(object param)
        {
            System.Collections.IList items = (System.Collections.IList)param;
            var collection = items.Cast<BiegaczEF>().ToList();

            this.WybraniBiegacze = collection;
        }

        private bool CzyMoznaAktualizowacBiegaczy()
        {
            return (this.WybraneZawody != null);
        }
        #endregion

        #region Internal Methods
        internal void Init()
        {
            MaratonyModelEF.ClearDb();

            this.PrzykladoweDane();
            this.OdswiezZawody();
            this.OdswiezBiegaczy();
            this.AddCommand = new RelayCommand(
                action => this.DodajBiegacza(),
                enable => this.CzyMoznaDodacBiegacza());
            this.ClearCommand = new RelayCommand(
                action => this.WyczyscWybraneZawody(),
                enable => this.CzyMoznaWyczyscicZawody());
            this.DeleteCommand = new RelayCommand(
                action => this.UsunWybranychBiegaczy(),
                enable => this.CzyMoznaUsunacBiegaczy());
            this.DataGridSelectionChangedCommand = new RelayCommand(
                (parameter) => this.AktualizujZaznaczonychBiegaczy((parameter)),
                enable => this.CzyMoznaAktualizowacBiegaczy());

            WybraneZawody = Zawody.FirstOrDefault();
        }

        internal void PrzykladoweDane()
        {
            model.DodajZawody("Kraków", new DateTime(2016, 1, 10), 11.6);
            model.DodajZawody("Warszawa", new DateTime(2016, 1, 23), 6);
            model.DodajBiegacza(1,"Młody", "Bóg");
            model.DodajBiegacza(1, "Jan", "Kowalski");
            model.DodajBiegacza(2, "Adam", "Nowak");
        }
        #endregion
    }
}
