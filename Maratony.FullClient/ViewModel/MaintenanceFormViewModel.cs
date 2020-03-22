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
    // Wersja jest delikatnie rozszerzona: 
    //  - dodałem przycisk Usuń, który usuwa wybranych w DataGridzie zawodników oraz zwalnia ich numer ID.
    //  - dodałem nowe TextBoxy do wpisywania Imienia i Nazwiska biegacza. Jeśli pole jest puste to wpisuje się "Imię" lub "Nazwisko".
    //  - dodawanie nowego biegacza nada mu pierwszy wolny numer ID.
    public class MaintenanceFormViewModel : ViewModelBase
    {
        #region Properties       
        private Zawody wybraneZawody;
        public Zawody WybraneZawody
        {
            get
            {
                return this.wybraneZawody;
            }
            set
            {
                this.wybraneZawody = value;
                this.OnPropertyChanged();
                this.OdswiezBiegaczy();  // dodane ponizej
            }
        }

        private IEnumerable<Biegacz> biegacze;
        public IEnumerable<Biegacz> Biegacze
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

        private IEnumerable<Zawody> zawody;
        public IEnumerable<Zawody> Zawody
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

        private List<Biegacz> wybraniBiegacze = new List<Biegacz>();
        public List<Biegacz> WybraniBiegacze
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
        private MaratonyModel model = new MaratonyModel();
        #endregion

        #region Commands
        public ICommand SaveCommand
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
            Task.Run(() => Init());
        }

        public MaintenanceFormViewModel(bool init)
        {
            wybraniBiegacze = new List<Biegacz>();
            if (init)
                Init();
        }
        #endregion

        #region Private methods
        private void DodajBiegacza()
        {
            model.DodajBiegacza(this.WybraneZawody.ID, 
                (imieTextBox == null || imieTextBox == "") ? "Imie" : imieTextBox, 
                (nazwiskoTextBox == null || nazwiskoTextBox == "") ? "Nazwisko" : nazwiskoTextBox);
            this.OdswiezBiegaczy();
        }

        private void OdswiezBiegaczy()
        {
            this.Biegacze = null;
            this.Biegacze = this.WybraneZawody?.Biegacze;
        }

        private bool CzyMoznaDodacBiegacza()
        {
            return (this.WybraneZawody != null);
        }

        private void OdswiezZawody()
        {
            this.Zawody = model.ListaZawodow;
        }

        private void WyczyscWybraneZawody()
        {
            this.WybraneZawody = null;
        }

        private void UsunWybranychBiegaczy()
        {
            foreach (var biegacz in WybraniBiegacze)
            {
                model.UsunBiegacza(biegacz.ID,WybraneZawody.ID);
            }

            OdswiezBiegaczy();
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
            var collection = items.Cast<Biegacz>().ToList();

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
            this.PrzykladoweDane();
            this.OdswiezZawody();
            this.SaveCommand = new RelayCommand(
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
        }

        internal void PrzykladoweDane()
        {
            model.DodajZawody("Kraków", new DateTime(2016, 1, 10), 11.6);
            model.DodajZawody("Warszawa", new DateTime(2016, 1, 23), 6);
            model.DodajBiegacza(model.ListaZawodow[0].ID, "Młody", "Bóg");
            model.DodajBiegacza(model.ListaZawodow[1].ID, "Jan", "Kowalski");
            model.DodajBiegacza(model.ListaZawodow[1].ID, "Adam", "Nowak");
        }
        #endregion
    }
}
