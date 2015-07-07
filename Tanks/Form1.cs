using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tanks
{
    public partial class Form1 : Form
    {
        Bitmap bmp;
        Graphics g;
        GameManager gm;
        List<string> executableNames;

        const int arenaWidth = 1000;
        const int arenaHeight = 1000;
        const int drawTimerInterval = 100;

        public Form1()
        {
            InitializeComponent();

            bmp = new Bitmap(arenaWidth, arenaHeight);
            g = Graphics.FromImage(bmp);

            executableNames = new List<string>();
        }

        private void selectTankButton_Click(object sender, EventArgs e)
        {
            openFileDialog.ShowDialog();
        }

        private void openFileDialog_FileOk(object sender, CancelEventArgs e)
        {
            if (!executableNames.Contains(openFileDialog.FileName))
                executableNames.Add(openFileDialog.FileName);
        }

        private void startButton_Click(object sender, EventArgs e)
        {
            startButton.Visible = false;
            tankExecutableListBox.Visible = false;
            selectTankButton.Visible = false;

            drawTimer.Interval = drawTimerInterval;
            drawTimer.Enabled = true;

            gm = new GameManager(executableNames);
        }

        private void drawTimer_Tick(object sender, EventArgs e)
        {
            foreach (Tank t in gm.tanks)
                t.Draw(g);

            canvas.Image = bmp;
        }
    }
}
