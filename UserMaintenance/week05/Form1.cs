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
using System.Windows.Forms.DataVisualization.Charting;
using System.Xml;
using week05.Entities;
using week05.MnbServiceReference;

namespace week05
{
    public partial class Form1 : Form
    {
        BindingList<RateData> Rates = new BindingList<RateData>();
        BindingList<string> Currencies = new BindingList<string>();

        public Form1()
        {
            InitializeComponent();
            dgwRates.DataSource = Rates;
            comboBox1.DataSource= Currencies;
            GetCurrencies();
            RefreshData();
        }
        void GetCurrencies()
        {
            MNBArfolyamServiceSoapClient m = new MNBArfolyamServiceSoapClient();
            GetCurrenciesRequestBody request = new GetCurrenciesRequestBody();
            GetCurrenciesResponseBody response = m.GetCurrencies(request);
            string result = response.GetCurrenciesResult;
            XmlDocument x = new XmlDocument();
            x.LoadXml(result);
            //MessageBox.Show(result);
            XmlElement item = x.DocumentElement;
            int i = 0;
            while (item.ChildNodes[0].ChildNodes[i] != null)
            {
                Currencies.Add(item.ChildNodes[0].ChildNodes[i].InnerText);
                i++;
            }
            m.Close();
        }

        private void RefreshData()
        {
            Rates.Clear();
            ReadXml();
            GetChart();
        }

        private void ReadXml()
        {
            XmlDocument xml = new XmlDocument();
            xml.LoadXml(GetExchangeRates());
            foreach (XmlElement item in xml.DocumentElement)
            {
                if (item.ChildNodes[0] != null)
                {
                    RateData rd = new RateData();
                    Rates.Add(rd);
                    rd.Currency = item.ChildNodes[0].Attributes["curr"].Value;
                    rd.Date = Convert.ToDateTime(item.Attributes["date"].Value);
                    decimal unit = Convert.ToDecimal(item.ChildNodes[0].Attributes["unit"].Value);
                    decimal value = Convert.ToDecimal(item.ChildNodes[0].InnerText);
                    if (unit != 0)
                    {
                        rd.Value = value / unit;
                    }
                    else
                    {
                        rd.Value = value;
                    }
                }
            }
        }
        private void GetChart()
        {
            chartRateData.DataSource = Rates;
            var series = chartRateData.Series[0];
            series.ChartType = SeriesChartType.Line;
            series.XValueMember = "Date";
            series.YValueMembers = "Value";
            series.BorderWidth = 2;
            
            var legend = chartRateData.Legends[0];
            legend.Enabled= false;

            var chartArea = chartRateData.ChartAreas[0];
            chartArea.AxisX.MajorGrid.Enabled = false;
            chartArea.AxisY.MajorGrid.Enabled = false;
            chartArea.AxisY.IsStartedFromZero = false;
        }
        private string GetExchangeRates()
        {
            MNBArfolyamServiceSoapClient mnbService = new MNBArfolyamServiceSoapClient();
            GetExchangeRatesRequestBody request = new GetExchangeRatesRequestBody()
            {
                currencyNames = comboBox1.SelectedItem.ToString(),
                startDate = dateTimePicker1.Value.ToString(),
                endDate = dateTimePicker2.Value.ToString()
            };
            GetExchangeRatesResponseBody response = mnbService.GetExchangeRates(request);
            string result = response.GetExchangeRatesResult;
            //MessageBox.Show(result);
            mnbService.Close();
            return result;

        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            RefreshData();
        }

        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {
            RefreshData();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            RefreshData();
        }
    }

}
