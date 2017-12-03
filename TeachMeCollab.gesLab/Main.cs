using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.CSharp;
using CefSharp;
using CefSharp.WinForms;
using System.Threading;

namespace TeachMeCollab.gesLab
{
    public partial class Main : MetroFramework.Forms.MetroForm
    {
        public ChromiumWebBrowser chromeBrowser { get; private set; }

        public Main()
        {
            InitializeComponent();
            InitializeChromium();
        }

        private void InitializeChromium()
        {
            CefSettings settings = new CefSettings();
            // Initialize cef with the provided settings
            Cef.Initialize(settings);
            // Create a browser component
            chromeBrowser = new ChromiumWebBrowser("http://"+ServerAdress.adress+"/gentelella/production");
            // Add it to the form and fill it to the form window.
            this.Controls.Add(chromeBrowser);
            chromeBrowser.Dock = DockStyle.Fill;

            chromeBrowser.KeyDown += new KeyEventHandler(KeyDown);
            chromeBrowser.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Main_MouseMove);
           
        }

        private void Main_Load(object sender, EventArgs e)
        {
           // MenuHide();

        }
        private void Main_FormClosing(object sender, FormClosingEventArgs e)
        {
            Cef.Shutdown();
        }

        private void KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.A )
                new Server().ShowDialog();

        }

        private void closeToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void setServerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            chromeBrowser.GetBrowser().PrintToPdfAsync("/", new PdfPrintSettings());

            //new Server().ShowDialog();
        }

        private void closeToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void MenuShow()
        {
            while (menuStrip1.Top < 0)
            {

                menuStrip1.Top++;
                Thread.Sleep(10);
                menuStrip1.Update();
            }
        }

        private void MenuHide()
        {
            while (menuStrip1.Top != -25)
            {
                menuStrip1.Top--;
                Thread.Sleep(10);
            }
        }

        private void Main_MouseMove(object sender, MouseEventArgs e)
        {

            if (e.Y < 40)
            {
                this.BeginInvoke((ThreadStart)delegate ()
                {
                    MenuShow();

                });
            }
            else if (e.Y > 100)
            {

                this.BeginInvoke((ThreadStart)delegate ()
                {
                    MenuHide();

                });

            }
        }
    }
}
