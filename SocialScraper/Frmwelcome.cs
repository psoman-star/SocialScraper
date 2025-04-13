using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SocialScraper
{
    public partial class Frmwelcome : Form
    {
        public Frmwelcome()
        {
            InitializeComponent();
        }

        private void Frmwelcome_Load(object sender, EventArgs e)
        {
            this.progressBar1.Step = 5;
            Task.Factory.StartNew(() =>
            {
                for (int i = 0; i < 20; i++)
                {
                    SpinWait.SpinUntil(() => false, 100);
                    this.Invoke(new Action(() => this.progressBar1.PerformStep()));
                }
                this.Invoke(new Action(() =>
                {
                    this.Hide();
                    new FrmMain().ShowDialog();
                }));

            });
        }
    }
}
