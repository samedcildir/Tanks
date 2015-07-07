namespace Tanks
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.canvas = new System.Windows.Forms.PictureBox();
            this.tankExecutableListBox = new System.Windows.Forms.ListBox();
            this.selectTankButton = new System.Windows.Forms.Button();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.startButton = new System.Windows.Forms.Button();
            this.drawTimer = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.canvas)).BeginInit();
            this.SuspendLayout();
            // 
            // canvas
            // 
            this.canvas.Dock = System.Windows.Forms.DockStyle.Fill;
            this.canvas.Location = new System.Drawing.Point(0, 0);
            this.canvas.Name = "canvas";
            this.canvas.Size = new System.Drawing.Size(552, 421);
            this.canvas.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.canvas.TabIndex = 0;
            this.canvas.TabStop = false;
            // 
            // tankExecutableListBox
            // 
            this.tankExecutableListBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tankExecutableListBox.FormattingEnabled = true;
            this.tankExecutableListBox.Location = new System.Drawing.Point(12, 12);
            this.tankExecutableListBox.Name = "tankExecutableListBox";
            this.tankExecutableListBox.Size = new System.Drawing.Size(528, 342);
            this.tankExecutableListBox.TabIndex = 1;
            // 
            // selectTankButton
            // 
            this.selectTankButton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.selectTankButton.Location = new System.Drawing.Point(12, 361);
            this.selectTankButton.Name = "selectTankButton";
            this.selectTankButton.Size = new System.Drawing.Size(442, 49);
            this.selectTankButton.TabIndex = 2;
            this.selectTankButton.Text = "Select Tank Executable";
            this.selectTankButton.UseVisualStyleBackColor = true;
            this.selectTankButton.Click += new System.EventHandler(this.selectTankButton_Click);
            // 
            // openFileDialog
            // 
            this.openFileDialog.FileName = "openFileDialog1";
            this.openFileDialog.Filter = "Executable files (*.exe)|*.exe|All files (*.*)|*.*";
            this.openFileDialog.FileOk += new System.ComponentModel.CancelEventHandler(this.openFileDialog_FileOk);
            // 
            // startButton
            // 
            this.startButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.startButton.Location = new System.Drawing.Point(460, 361);
            this.startButton.Name = "startButton";
            this.startButton.Size = new System.Drawing.Size(80, 49);
            this.startButton.TabIndex = 3;
            this.startButton.Text = "START";
            this.startButton.UseVisualStyleBackColor = true;
            this.startButton.Click += new System.EventHandler(this.startButton_Click);
            // 
            // drawTimer
            // 
            this.drawTimer.Tick += new System.EventHandler(this.drawTimer_Tick);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(552, 421);
            this.Controls.Add(this.startButton);
            this.Controls.Add(this.selectTankButton);
            this.Controls.Add(this.tankExecutableListBox);
            this.Controls.Add(this.canvas);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.canvas)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox canvas;
        private System.Windows.Forms.ListBox tankExecutableListBox;
        private System.Windows.Forms.Button selectTankButton;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.Button startButton;
        private System.Windows.Forms.Timer drawTimer;
    }
}

