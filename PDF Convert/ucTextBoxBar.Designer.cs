namespace PDF_Convert
{
    partial class ucTextBoxBar
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
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.txtOutPath = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // txtOutPath
            // 
            this.txtOutPath.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.txtOutPath.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtOutPath.Font = new System.Drawing.Font("黑体", 12F);
            this.txtOutPath.ForeColor = System.Drawing.Color.Gray;
            this.txtOutPath.Location = new System.Drawing.Point(17, 4);
            this.txtOutPath.Name = "txtOutPath";
            this.txtOutPath.ReadOnly = true;
            this.txtOutPath.Size = new System.Drawing.Size(343, 19);
            this.txtOutPath.TabIndex = 0;
            // 
            // ucTextBoxBar
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::PDF_Convert.Properties.Resources.txtBox;
            this.Controls.Add(this.txtOutPath);
            this.DoubleBuffered = true;
            this.Name = "ucTextBoxBar";
            this.Size = new System.Drawing.Size(363, 31);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtOutPath;

    }
}
