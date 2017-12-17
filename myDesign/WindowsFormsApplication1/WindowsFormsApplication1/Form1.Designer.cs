namespace WindowsFormsApplication1
{
    partial class Form1
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

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.bt_run = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.txtPath = new System.Windows.Forms.TextBox();
            this.bt_brower = new System.Windows.Forms.Button();
            this.ofdMsiBrowser = new System.Windows.Forms.OpenFileDialog();
            this.SuspendLayout();
            // 
            // bt_run
            // 
            this.bt_run.Location = new System.Drawing.Point(661, 268);
            this.bt_run.Name = "bt_run";
            this.bt_run.Size = new System.Drawing.Size(75, 23);
            this.bt_run.TabIndex = 0;
            this.bt_run.Text = "run";
            this.bt_run.UseVisualStyleBackColor = true;
            this.bt_run.Click += new System.EventHandler(this.bt_run_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(96, 68);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(82, 15);
            this.label1.TabIndex = 1;
            this.label1.Text = "程序路径：";
            // 
            // txtPath
            // 
            this.txtPath.Location = new System.Drawing.Point(184, 58);
            this.txtPath.Name = "txtPath";
            this.txtPath.Size = new System.Drawing.Size(331, 25);
            this.txtPath.TabIndex = 2;
            // 
            // bt_brower
            // 
            this.bt_brower.Location = new System.Drawing.Point(530, 60);
            this.bt_brower.Name = "bt_brower";
            this.bt_brower.Size = new System.Drawing.Size(75, 23);
            this.bt_brower.TabIndex = 3;
            this.bt_brower.Text = "...";
            this.bt_brower.UseVisualStyleBackColor = true;
            this.bt_brower.Click += new System.EventHandler(this.bt_brower_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(813, 369);
            this.Controls.Add(this.bt_brower);
            this.Controls.Add(this.txtPath);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.bt_run);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button bt_run;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtPath;
        private System.Windows.Forms.Button bt_brower;
 
        private System.Windows.Forms.OpenFileDialog ofdMsiBrowser;
    }
}

