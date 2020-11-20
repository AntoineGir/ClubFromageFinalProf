using System;
using System.Collections.Generic;
using System.Text;

namespace ModelLayer.Business
{
    class FamilleFromage
    {
        private int id;
        private string nom;
        private List<Fromage> unFromage = new List<Fromage>();

        public int Id { get => id; set => id = value; }
        public string Nom { get => nom; set => nom = value; }
        public List<Fromage> UnFromage { get => unFromage; set => unFromage = value; }

    }
}
