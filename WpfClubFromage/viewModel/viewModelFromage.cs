using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using ModelLayer.Business;
using ModelLayer.Data;
using WpfClubFromage.viewModel;
using System.Windows;

namespace WpfClubFromage.viewModel
{
    class viewModelFromage : viewModelBase
    {
        //déclaration des attributs ...à compléter
        private DaoPays vmDaoPays;
        private DaoFromage vmDaoFromage;
        private ICommand updateCommand;
        private ICommand newCommand;
        private ICommand deleteCommand;
        private ObservableCollection<Pays> listPays;
        private ObservableCollection<Fromage> listFromages;
        private Fromage selectedFromage = new Fromage();
        private Fromage activeFromage = new Fromage();


        //déclaration des listes...à compléter avec les fromages
        public ObservableCollection<Pays> ListPays { get => listPays; set => listPays = value; }
        public ObservableCollection<Fromage> ListFromages { get => listFromages; set => listFromages = value; }
        //déclaration des propriétés avec OnPropertyChanged("nom_propriété_bindée")
        //par exemple...
        public string Name
        {
            get => activeFromage.Name;

            set
            {
                if (activeFromage.Name != value)
                {
                    activeFromage.Name = value;
                    //création d'un évènement si la propriété Name (bindée dans le XAML) change
                    OnPropertyChanged("Name");
                }
            }
        }


        public Fromage SelectedFromage
        {
            get => selectedFromage;

            set
            {
                if (selectedFromage != value)
                {
                    selectedFromage = value;
                    //création d'un évènement si la propriété Name (bindée dans le XAML) change
                    OnPropertyChanged("SelectedFromage");

                    if (selectedFromage != null)
                    {
                        ActiveFromage = selectedFromage;
                    }
                }
            }
        }

        public Fromage ActiveFromage
        {
            get => activeFromage;

            set
            {
                if (activeFromage != value)
                {
                    activeFromage = value;
                    //création d'un évènement si la propriété Name (bindée dans le XAML) change
                    OnPropertyChanged("Name");
                    OnPropertyChanged("Origin");
                    OnPropertyChanged("Creation");
                }
            }
        }


        public DateTime Creation
        {
            get => activeFromage.Creation;

            set
            {
                if (activeFromage.Creation != value)
                {
                    activeFromage.Creation = value;
                    //création d'un évènement si la propriété Name (bindée dans le XAML) change
                    OnPropertyChanged("Creation");
                }
            }
        }


        public Pays Origin
        {
            get => activeFromage.Origin;

            set
            {
                if (activeFromage.Origin != value)
                {
                    activeFromage.Origin = value;
                    OnPropertyChanged("Origin");


                }
            }
        }

        //déclaration du contructeur de viewModelFromage
        public viewModelFromage(DaoPays thedaopays, DaoFromage thedaofromages)
        {
            vmDaoPays = thedaopays;
            vmDaoFromage = thedaofromages;

            listPays = new ObservableCollection<Pays>(thedaopays.SelectAll());
            listFromages = new ObservableCollection<Fromage>(thedaofromages.SelectAll());


            /*foreach (Fromage itemFr in listFromages)
            {
                foreach (Pays itemPa in listPays)
                {
                    if (itemFr.Origin.Id == itemPa.Id)
                    {
                        itemFr.Origin = itemPa;
                        
                    }
                }
            }
            */
            foreach (Fromage itemFr in listFromages)
            {
                int i = 0;
                while (itemFr.Origin.Id != listPays[i].Id)
                {
                    i++;
                }
                itemFr.Origin = listPays[i];
            }
        }

        //Méthode appelée au click du bouton UpdateCommand
        public ICommand UpdateCommand
        {
            get
            {
                if (this.updateCommand == null)
                {
                    this.updateCommand = new RelayCommand(() => UpdateFromage(), () => true);
                }
                return this.updateCommand;

            }

        }

        public void UpdateFromage()
        {
            //code du bouton - à coder
            Fromage backup = new Fromage();
            this.vmDaoFromage.Update(this.ActiveFromage);
            int emplacement = listFromages.IndexOf(SelectedFromage);
            backup = ActiveFromage;
            listFromages.Insert(emplacement, ActiveFromage);
            listFromages.RemoveAt(emplacement + 1);
            SelectedFromage = backup;
        }

        public ICommand NewCommand
        {
            get
            {
                if (this.newCommand == null)
                {
                    this.newCommand = new RelayCommand(() => NewFromage(), () => true);
                }
                return this.newCommand;
            }
        }

        public void NewFromage()
        {
            //code du bouton - à coder
            
            int total = listFromages.Count;
            total = SelectedFromage.Id;
            this.vmDaoFromage.Insert(this.ActiveFromage);

            listFromages.Insert(total, ActiveFromage);
        }


        public ICommand DeleteCommand
        {
            get
            {
                if (this.deleteCommand == null)
                {
                    this.deleteCommand = new RelayCommand(() => DeleteFromage(), () => true);
                }
                return this.deleteCommand;
            }
        }

        public void DeleteFromage()
        {
            this.vmDaoFromage.Delete(this.ActiveFromage);
            int emplacement = listFromages.IndexOf(SelectedFromage);
            listFromages.RemoveAt(emplacement);
        }
    }
}
