using ClinicServiceClientNamespace;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClinicDesctop
{

    /// <summary>
    /// НЕОБХОДИМО ПОДКЛЮЧИТЬ СБОРКУ: System.Runtime.Serialization
    /// 
    /// 
    /// 
    /// ДОМАШНЕЕ ЗАДАНИЕ:
    /// 
    /// Разработать приложение ClinicDesctop, подключить ClinicService к вашему клиентскому приложению.
    /// </summary>
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            Method();
        }

        public void Method()
        {

        }

        private void buttonLoadClients_Click(object sender, EventArgs e)
        {
            ClinicServiceClient clinicServiceClient =
                new ClinicServiceClient("http://localhost:5198/", new System.Net.Http.HttpClient());

            ICollection<Client> clients = clinicServiceClient.ClientGetAllAsync().Result;
            listViewClients.Items.Clear();
            foreach (Client client in clients)
            {
                ListViewItem item = new ListViewItem();
                item.Text = client.ClientId.ToString();
                item.SubItems.Add(new ListViewItem.ListViewSubItem()
                {
                    Text = client.SurName
                });
                item.SubItems.Add(new ListViewItem.ListViewSubItem()
                {
                    Text = client.FirstName
                });
                item.SubItems.Add(new ListViewItem.ListViewSubItem()
                {
                    Text = client.Patronymic
                });

                listViewClients.Items.Add(item);
            }
        }
    }
}
