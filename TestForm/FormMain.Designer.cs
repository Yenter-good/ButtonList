namespace TestForm
{
    partial class FormMain
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
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.ucLightList1 = new ButtonList.Lights.UCLightList();
            this.ucLightList2 = new ButtonList.Lights.UCLightList();
            this.ucTable1 = new ButtonList.Tables.UCTable();
            this.button1 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // button2
            // 
            this.button2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button2.Location = new System.Drawing.Point(1465, 138);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 2;
            this.button2.Text = "保存";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button3
            // 
            this.button3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button3.Location = new System.Drawing.Point(1465, 178);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 23);
            this.button3.TabIndex = 4;
            this.button3.Text = "打开";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // ucLightList1
            // 
            this.ucLightList1.ButtonSize = 100;
            this.ucLightList1.Location = new System.Drawing.Point(12, 12);
            this.ucLightList1.Manager = null;
            this.ucLightList1.Name = "ucLightList1";
            this.ucLightList1.Size = new System.Drawing.Size(457, 534);
            this.ucLightList1.TabIndex = 3;
            // 
            // ucLightList2
            // 
            this.ucLightList2.ButtonSize = 100;
            this.ucLightList2.Location = new System.Drawing.Point(947, 12);
            this.ucLightList2.Manager = null;
            this.ucLightList2.Name = "ucLightList2";
            this.ucLightList2.Size = new System.Drawing.Size(457, 534);
            this.ucLightList2.TabIndex = 4;
            // 
            // ucTable1
            // 
            this.ucTable1.Location = new System.Drawing.Point(491, 12);
            this.ucTable1.Manager = null;
            this.ucTable1.Name = "ucTable1";
            this.ucTable1.Size = new System.Drawing.Size(440, 534);
            this.ucTable1.TabIndex = 5;
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.Location = new System.Drawing.Point(1465, 374);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 6;
            this.button1.Text = "增加";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button4
            // 
            this.button4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button4.Location = new System.Drawing.Point(1465, 470);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(75, 23);
            this.button4.TabIndex = 7;
            this.button4.Text = "获取";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1552, 572);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.ucTable1);
            this.Controls.Add(this.ucLightList2);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.ucLightList1);
            this.Controls.Add(this.button2);
            this.Name = "FormMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FormSignalSetting";
            this.Shown += new System.EventHandler(this.FormMain_Shown);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button button2;
        private ButtonList.Lights.UCLightList ucLightList1;
        private System.Windows.Forms.Button button3;
        private ButtonList.Lights.UCLightList ucLightList2;
        private ButtonList.Tables.UCTable ucTable1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button4;
    }
}