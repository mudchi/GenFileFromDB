namespace GenOutDMS
{
    partial class formMain
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
            this.Label1 = new System.Windows.Forms.Label();
            this.txtCountDown = new System.Windows.Forms.TextBox();
            this.TimeAuto = new System.Windows.Forms.Timer(this.components);
            this.dtpTransactionDate = new System.Windows.Forms.DateTimePicker();
            this.StatusStrip1 = new System.Windows.Forms.StatusStrip();
            this.tssValue = new System.Windows.Forms.ToolStripStatusLabel();
            this.TssText = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblProgramGroup = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.lblRunby = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.KHead = new Templates.TFHeader.KExcellence();
            this.StatusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // Label1
            // 
            this.Label1.AccessibleDescription = "v";
            this.Label1.AutoSize = true;
            this.Label1.Location = new System.Drawing.Point(216, 130);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(98, 13);
            this.Label1.TabIndex = 35;
            this.Label1.Text = "Transaction Date  :";
            // 
            // txtCountDown
            // 
            this.txtCountDown.BackColor = System.Drawing.SystemColors.Control;
            this.txtCountDown.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtCountDown.Location = new System.Drawing.Point(61, 85);
            this.txtCountDown.Name = "txtCountDown";
            this.txtCountDown.ReadOnly = true;
            this.txtCountDown.Size = new System.Drawing.Size(595, 13);
            this.txtCountDown.TabIndex = 33;
            this.txtCountDown.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // TimeAuto
            // 
            this.TimeAuto.Enabled = true;
            this.TimeAuto.Interval = 1000;
            this.TimeAuto.Tick += new System.EventHandler(this.TimeAuto_Tick);
            // 
            // dtpTransactionDate
            // 
            this.dtpTransactionDate.AccessibleDescription = "v";
            this.dtpTransactionDate.Enabled = false;
            this.dtpTransactionDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpTransactionDate.Location = new System.Drawing.Point(342, 127);
            this.dtpTransactionDate.Name = "dtpTransactionDate";
            this.dtpTransactionDate.Size = new System.Drawing.Size(148, 20);
            this.dtpTransactionDate.TabIndex = 34;
            // 
            // StatusStrip1
            // 
            this.StatusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tssValue,
            this.TssText});
            this.StatusStrip1.Location = new System.Drawing.Point(0, 202);
            this.StatusStrip1.Name = "StatusStrip1";
            this.StatusStrip1.Size = new System.Drawing.Size(703, 22);
            this.StatusStrip1.TabIndex = 32;
            this.StatusStrip1.Text = "StatusStrip1";
            // 
            // tssValue
            // 
            this.tssValue.Name = "tssValue";
            this.tssValue.Size = new System.Drawing.Size(0, 17);
            // 
            // TssText
            // 
            this.TssText.Name = "TssText";
            this.TssText.Size = new System.Drawing.Size(0, 17);
            // 
            // lblProgramGroup
            // 
            this.lblProgramGroup.AccessibleDescription = "v";
            this.lblProgramGroup.AutoSize = true;
            this.lblProgramGroup.Location = new System.Drawing.Point(340, 172);
            this.lblProgramGroup.Name = "lblProgramGroup";
            this.lblProgramGroup.Size = new System.Drawing.Size(10, 13);
            this.lblProgramGroup.TabIndex = 39;
            this.lblProgramGroup.Text = "-";
            // 
            // label4
            // 
            this.label4.AccessibleDescription = "v";
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(230, 172);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(84, 13);
            this.label4.TabIndex = 38;
            this.label4.Text = "Program Group :";
            // 
            // lblRunby
            // 
            this.lblRunby.AccessibleDescription = "v";
            this.lblRunby.AutoSize = true;
            this.lblRunby.Location = new System.Drawing.Point(340, 151);
            this.lblRunby.Name = "lblRunby";
            this.lblRunby.Size = new System.Drawing.Size(10, 13);
            this.lblRunby.TabIndex = 37;
            this.lblRunby.Text = "-";
            // 
            // label2
            // 
            this.label2.AccessibleDescription = "v";
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(267, 151);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(47, 13);
            this.label2.TabIndex = 36;
            this.label2.Text = "Run by :";
            // 
            // KHead
            // 
            this.KHead.BackColor = System.Drawing.Color.White;
            this.KHead.Dock = System.Windows.Forms.DockStyle.Top;
            this.KHead.Location = new System.Drawing.Point(0, 0);
            this.KHead.Margin = new System.Windows.Forms.Padding(4);
            this.KHead.Name = "KHead";
            this.KHead.Purpose = "N/A";
            this.KHead.PurposeFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.KHead.PurposeForeColor = System.Drawing.Color.SteelBlue;
            this.KHead.Size = new System.Drawing.Size(703, 71);
            this.KHead.TabIndex = 31;
            this.KHead.Title = "TITLE";
            this.KHead.TitleFont = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.KHead.TitleForeColor = System.Drawing.Color.SteelBlue;
            // 
            // formMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(703, 224);
            this.Controls.Add(this.Label1);
            this.Controls.Add(this.txtCountDown);
            this.Controls.Add(this.dtpTransactionDate);
            this.Controls.Add(this.StatusStrip1);
            this.Controls.Add(this.lblProgramGroup);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.lblRunby);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.KHead);
            this.Name = "formMain";
            this.Text = "Form1";
            this.StatusStrip1.ResumeLayout(false);
            this.StatusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal System.Windows.Forms.Label Label1;
        internal System.Windows.Forms.TextBox txtCountDown;
        internal System.Windows.Forms.Timer TimeAuto;
        internal System.Windows.Forms.DateTimePicker dtpTransactionDate;
        internal System.Windows.Forms.StatusStrip StatusStrip1;
        internal System.Windows.Forms.ToolStripStatusLabel tssValue;
        internal System.Windows.Forms.ToolStripStatusLabel TssText;
        internal System.Windows.Forms.Label lblProgramGroup;
        internal System.Windows.Forms.Label label4;
        internal System.Windows.Forms.Label lblRunby;
        internal System.Windows.Forms.Label label2;
        internal Templates.TFHeader.KExcellence KHead;
    }
}

