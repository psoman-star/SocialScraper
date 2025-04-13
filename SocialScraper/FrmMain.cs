using Krypton.Toolkit;
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
    public partial class FrmMain : KryptonForm
    {
        private string _url = "https://www.google.com";
        private SocialInfoScraper _scraper = new SocialInfoScraper();
        private List<SocialModel> _rList = new List<SocialModel>();
        private CancellationTokenSource _tokenSource = new CancellationTokenSource();
        public FrmMain()
        {
            InitializeComponent();
            BindComBox();
        }

        private void BindComBox()
        {
            var list = new List<SocialItem>
            {

            };
            this.comSocial.DataSource = list;
            this.comSocial.DisplayMember = "Name";
            this.comSocial.ValueMember = "Site";
            this.comSocial.SelectedIndex = 0;

        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            var keyword = this.txtKeywrod.Text.Trim();
            if (string.IsNullOrEmpty(keyword))
            {
                KryptonMessageBox.Show("Please enter key words！", "Info",
                    MessageBoxButtons.OK, KryptonMessageBoxIcon.WARNING, showCtrlCopy: false);
                return;
            }
            var maxNum = (int)this.nudMax.Value;
            var delay = (int)this.nudIntevalMin.Value;
            var social = (SocialItem)this.comSocial.SelectedItem;


            if (this.btnStart.Text == "Start")
            {
                this.btnStart.Text = "Stop";
                Action<List<SocialModel>> aciton = (list) => { };
                var token = new CancellationToken();
                token = this._tokenSource.Token;
                Task.Factory.StartNew(() =>
                {
                    this._scraper.ExtractAll(social.Site, aciton, token);


                }, token, TaskCreationOptions.LongRunning, TaskScheduler.Default)
                    .ContinueWith(t =>
                    {

                    });
            }
            else
            {
                this.Invoke(new Action(() =>
                {
                    this.btnStart.Text = "Stopping";
                }));
                this._tokenSource.Cancel();
                this._tokenSource = new CancellationTokenSource();

            }
        }
    }
}
