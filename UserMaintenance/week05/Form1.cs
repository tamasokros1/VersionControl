﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using week05.Entities;
using week05.MnbServiceReference;

namespace week05
{
    public partial class Form1 : Form
    {
        BindingList<RateData> Rates = new BindingList<RateData>();
        
        public Form1()
        {
            InitializeComponent();
            GetExchangeRates();
            dgwRates.DataSource = Rates;
        }
        void GetExchangeRates()
        {
            var mnbService = new MNBArfolyamServiceSoapClient();
            var request = new GetExchangeRatesRequestBody()
            {
                currencyNames = "EUR",
                startDate = "2020-01-01",
                endDate = "2020-06-30"
            };
            var response = mnbService.GetExchangeRates(request);
            var result = response.GetExchangeRatesResult;

            /*
           string exchangeResult = result;
           File.WriteAllText(@"C:\Users\Tamás\source\repos\VersionControl\UserMaintenance\week05\result.xml", result);
           string readResult = File.ReadAllText(@"C:\Users\Tamás\source\repos\VersionControl\UserMaintenance\week05\result.xml");
           Console.WriteLine(readResult);
           */


        }

    }

}
