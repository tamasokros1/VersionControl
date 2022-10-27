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
using UserMaintenance.Entities;

namespace UserMaintenance
{
    public partial class Form1 : Form
    {
        BindingList<User> users = new BindingList<User>();
        public Form1()
        {
            InitializeComponent();
            lblFullName.Text = Resource1.FullName;
            btnAdd.Text = Resource1.Add;
            btnSave.Text = Resource1.Save;
            btnDelete.Text = Resource1.Delete;
            lbUsers.DataSource = users;
            lbUsers.ValueMember = "Id";
            lbUsers.DisplayMember = "FullName";

        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            User u = new User()
            {
                FullName = txtFullName.Text,
            };
            users.Add(u);
        }

        private void btnWrite_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "Text File|*.txt";
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                StreamWriter sw = new StreamWriter(sfd.FileName);
                for (int i = 0; i < users.Count; i++)
                {
                    sw.Write(string.Format("{0}, {1} \r\n", users[i].FullName, users[i].Id));
                    
                }
                sw.Close();
            }
            sfd.Dispose();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            users.Remove((User)lbUsers.SelectedItem);
        }
    }
}
