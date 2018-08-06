using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;

namespace BatteryAlarm
{
    public partial class Form1 : Form
    {
        //PowerLineStatus - Offline
        //BatteryChargeStatus - Low
        //BatteryFullLifetime - -1
        //BatteryLifePercent - 0.32
        //BatteryLifeRemaining - -1

        //PowerLineStatus - Online
        //BatteryChargeStatus - Low, Charging
        //BatteryFullLifetime - -1
        //BatteryLifePercent - 0.31
        //BatteryLifeRemaining - -1

        private z.UI.UserControls.PopUp pop;

        public Form1()
        {
            InitializeComponent();
            
            this.pop = new z.UI.UserControls.PopUp(Application.ProductName, BatteryAlarm.Properties.Resources.battery1);
            //this.pop.add("About", new EventHandler(About_Click));
            this.pop.add("Exit", new EventHandler(Exit_Click));
            this.pop.eMenuHover += pop_eMenuHover;
            
        }

        void pop_eMenuHover(object Sender, MouseEventArgs e)
        {
           
        }

        private void Exit_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.GetCurrentProcess().Kill();
        }

        private void About_Click(object sender, EventArgs e)
        {
            
        }

        bool AlreadyShow = false;

        private void tmrChecker_Tick(object sender, EventArgs e)
        {
            try
            {
                var j = (from k in this.Prop()
                         where k.Name == "PowerLineStatus"
                         select k).SingleOrDefault();

                if (((PowerLineStatus)j.GetValue(SystemInformation.PowerStatus, null)) == PowerLineStatus.Online)
                {
                    var ch = (from k in this.Prop()
                             where k.Name == "BatteryLifePercent"
                             select k).SingleOrDefault();

                    if (Convert.ToDecimal(ch.GetValue(SystemInformation.PowerStatus, null)) >= 1)
                    {
                        if (AlreadyShow == false)
                        {
                            this.Show();
                            AlreadyShow = true;
                        }
                    }
                    else
                    {
                        this.Hide();
                        AlreadyShow = false;
                    }
                }
                else //offline
                {
                    this.Hide();
                    AlreadyShow = false;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Application.ProductName);
                this.Exit_Click(this, e);
            }
        }
		
        private PropertyInfo[] Prop()
        {
            Type t = typeof(System.Windows.Forms.PowerStatus);
            return t.GetProperties();  
        }

        enum PowerLineStatus
        {
            Offline,
            Online
        }

    }
}

