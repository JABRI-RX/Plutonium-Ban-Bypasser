using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Management;
 
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Plutonium_Ban_Bypass
{
    public partial class main : Form
    {

        [DllImport("Kernel32.dll")]
        static extern bool SetComputerNameA(string lpComputerName);
        public main()
        {
            InitializeComponent();
        }
         
        private void Form1_Load(object sender, EventArgs e)
        {
            cname_textbox.Text = Environment.MachineName;
            drive_result.Text = "Make Sure you run the program as admin otherwise shit won't work";
            string key = "HKEY_LOCAL_MACHINE\\SOFTWARE\\Microsoft\\Cryptography";
            string r = (string)Registry.GetValue(key, "MachineGuid", (object)"default");
            guidbox.Text = "Your MachineGuid : "+r;
            
        }

        private void cma_btn_Click(object sender, EventArgs e)
        {
            ProcessStartInfo info = new ProcessStartInfo("https://www.youtube.com/watch?v=9E93Q4JRZeI");
            Process.Start(info);
            cma_btn.BackColor = Color.FromArgb(152, 195, 121);
        }

        private void cdi_btn_Click(object sender, EventArgs e)
        {
            Guid gd = Guid.NewGuid();
            Guid gd2 = Guid.NewGuid();
            string drive_id = $"{ gd.ToString().Split('-')[3]}-{gd2.ToString().Split('-')[3]}";
            Process pr = new Process();
            pr.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            pr.StartInfo.FileName = @"exes\Volumeid.exe";
            pr.StartInfo.Arguments = $"C: {drive_id}";
            pr.StartInfo.UseShellExecute = false;
            pr.StartInfo.CreateNoWindow = true;
            pr.StartInfo.RedirectStandardOutput = true;
            pr.StartInfo.RedirectStandardError = true;
            string temp;
            try
            {
                pr.Start();
                temp = "";
                while (!pr.HasExited)
                {
                    temp += pr.StandardOutput.ReadToEnd();
                }
            }
            catch
            {
                drive_result.Text = "Did you move the exes folder ? Because it has been removed.";
                cdi_btn.BackColor = Color.FromArgb(224, 107, 116);
                return;
            }
            
            if (temp.Contains("Access is denied"))
            {
                drive_result.Text = "Access is denied make sure you run the program in Admin";
                cdi_btn.BackColor = Color.FromArgb(224, 107, 116);

            }
            else if(temp.Contains("updated to"))
            {
                drive_result.Text = "You have successfully updated you C Drive ID";
                cdi_btn.BackColor = Color.FromArgb(152, 195, 121);

            }

        }

        private void ccn_btn_Click(object sender, EventArgs e)
        {
            if (cname_textbox.Text.Trim() != "")
            {
                if (SetComputerNameA(cname_textbox.Text))
                {
                    drive_result.Text = "The Name Has Been Changed Sucessfully";
                    ccn_btn.BackColor = Color.FromArgb(152, 195, 121);
                }
                else
                {
                    drive_result.Text = "Access is denied make sure you run the program in Admin";
                    ccn_btn.BackColor = Color.FromArgb(224, 107, 116);

                }
            }
            else
            {
                drive_result.Text = "Remove Any Space From The Name";
                ccn_btn.BackColor = Color.FromArgb(224, 107, 116);
            }
        }

        private void cguid_btn_Click(object sender, EventArgs e)
        {
            
            try
            {
                string key = "HKEY_LOCAL_MACHINE\\SOFTWARE\\Microsoft\\Cryptography";
                Guid newGuid = Guid.NewGuid();
                Registry.SetValue(key,"MachineGuid", newGuid.ToString());
                guidbox.Text = "Your MachineGuid : "+newGuid.ToString();
                MessageBox.Show(newGuid.ToString());
                cguid_btn.BackColor = Color.FromArgb(152, 195, 121);
            }
            catch
            {
                drive_result.Text = "Access is denied make sure you run the program in Admin";
                cguid_btn.BackColor = Color.FromArgb(224, 107, 116);
            }
            
        }
    }
}
