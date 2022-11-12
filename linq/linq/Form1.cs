using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace linq
{
    public partial class Form1 : Form
    {
        List<Country> countries = new List<Country>();
        List<Ramen> ramens = new List<Ramen>();
        public Form1()
        {
            InitializeComponent();
            LoadData("Ramen.csv");
        }
        void LoadData(string fileName)
        {
            StreamReader sr = new StreamReader(fileName);
            sr.ReadLine();
            while(!sr.EndOfStream)
            {
                string[] sor = sr.ReadLine().Split(";");
                string country = sor[2];
                Country current = AddCountry(country);
                Ramen r = new Ramen
                {
                    ID = ramens.Count + 1,
                    CountryFK = current.ID,
                    Country = current,
                    Rating = Convert.ToDouble(sor[3]),
                    Name = sor[1],
                    Brand = sor[0],
                };
                ramens.Add(r);
            }
            sr.Close();

            Country AddCountry(string country)
            {
                var currentCountry = (from c in countries
                                      where c.Name.Equals(country)
                                      select c).FirstOrDefault();
                if (currentCountry == null)
                {
                    currentCountry = new Country
                    {
                        ID = countries.Count + 1,
                        Name = country
                    };
                    countries.Add(currentCountry);
                }
                return currentCountry;
            }
        }

    }

}
