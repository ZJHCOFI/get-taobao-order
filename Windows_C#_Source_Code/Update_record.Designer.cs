namespace Get_taobao_order
{
    partial class Update_record
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Update_record));
            this.label_title = new System.Windows.Forms.Label();
            this.textBox_update_record = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // label_title
            // 
            this.label_title.AutoSize = true;
            this.label_title.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label_title.Location = new System.Drawing.Point(126, 11);
            this.label_title.Name = "label_title";
            this.label_title.Size = new System.Drawing.Size(106, 22);
            this.label_title.TabIndex = 1;
            this.label_title.Text = "软件更新记录";
            // 
            // textBox_update_record
            // 
            this.textBox_update_record.Location = new System.Drawing.Point(24, 42);
            this.textBox_update_record.Multiline = true;
            this.textBox_update_record.Name = "textBox_update_record";
            this.textBox_update_record.Size = new System.Drawing.Size(309, 157);
            this.textBox_update_record.TabIndex = 2;
            this.textBox_update_record.Text = "2022.11.26 22:18\r\n第一个版本发布。\r\n\r\n2023.01.19 01:08\r\n修改了用于分割的字符串，解决了某些使用场景下出现的订单号错误的bu" +
    "g。\r\n\r\n2024.07.22 14:20\r\n解决了预售订单场景下，获取订单状态失败导致程序报错的问题";
            // 
            // Update_record
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(359, 226);
            this.Controls.Add(this.textBox_update_record);
            this.Controls.Add(this.label_title);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Update_record";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "软件更新记录";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label_title;
        private System.Windows.Forms.TextBox textBox_update_record;
    }
}