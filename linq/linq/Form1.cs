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
                var currentCountry = (from c in countries
                                      where c.Name.Equals(country)
                                      select c).FirstOrDefault();
                if(currentCountry == null)
                {
                    currentCountry = new Country
                    {
                        ID = countries.Count + 1,
                        Name = country
                    };
                    countries.Add(currentCountry);
                }
            }
            sr.Close();

        }

    }

}
