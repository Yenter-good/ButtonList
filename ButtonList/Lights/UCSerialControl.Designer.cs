namespace ButtonList.Lights
{
    partial class UCSerialControl
    {
        /// <summary> 
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.label4 = new System.Windows.Forms.Label();
            this.rbPositive = new System.Windows.Forms.RadioButton();
            this.rbNegative = new System.Windows.Forms.RadioButton();
            this.SuspendLayout();
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(13, 10);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(41, 12);
            this.label4.TabIndex = 6;
            this.label4.Text = "数据值";
            // 
            // rbPositive
            // 
            this.rbPositive.AutoSize = true;
            this.rbPositive.Checked = true;
            this.rbPositive.Location = new System.Drawing.Point(53, 37);
            this.rbPositive.Name = "rbPositive";
            this.rbPositive.Size = new System.Drawing.Size(47, 16);
            this.rbPositive.TabIndex = 7;
            this.rbPositive.TabStop = true;
            this.rbPositive.Text = "正控";
            this.rbPositive.UseVisualStyleBackColor = true;
            // 
            // rbNegative
            // 
            this.rbNegative.AutoSize = true;
            this.rbNegative.Location = new System.Drawing.Point(149, 37);
            this.rbNegative.Name = "rbNegative";
            this.rbNegative.Size = new System.Drawing.Size(47, 16);
            this.rbNegative.TabIndex = 7;
            this.rbNegative.Text = "负控";
            this.rbNegative.UseVisualStyleBackColor = true;
            // 
            // UCSerialControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.rbNegative);
            this.Controls.Add(this.rbPositive);
            this.Controls.Add(this.label4);
            this.Name = "UCSerialControl";
            this.Size = new System.Drawing.Size(264, 70);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.RadioButton rbPositive;
        private System.Windows.Forms.RadioButton rbNegative;
    }
}
