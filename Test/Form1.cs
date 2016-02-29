using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Test
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            UnicomSGIP.Main.Model.SubmitRequest vSubmitRequest = new UnicomSGIP.Main.Model.SubmitRequest();
            vSubmitRequest.UserNumber = "";
            vSubmitRequest.MessageContent = "TTTTTTTTTTT";

            UnicomSGIP.Main.UnicomSms vUnicomSms = new UnicomSGIP.Main.UnicomSms();
            string Result = vUnicomSms.SendSms(vSubmitRequest);
        }
    }
}
