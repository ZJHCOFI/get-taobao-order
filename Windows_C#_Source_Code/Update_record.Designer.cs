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
            this.richTextBox_update_record = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // label_title
            // 
            this.label_title.AutoSize = true;
            this.label_title.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label_title.Location = new System.Drawing.Point(192, 11);
            this.label_title.Name = "label_title";
            this.label_title.Size = new System.Drawing.Size(106, 22);
            this.label_title.TabIndex = 1;
            this.label_title.Text = "软件更新记录";
            // 
            // richTextBox_update_record
            // 
            this.richTextBox_update_record.Location = new System.Drawing.Point(24, 42);
            this.richTextBox_update_record.Name = "richTextBox_update_record";
            this.richTextBox_update_record.Size = new System.Drawing.Size(437, 201);
            this.richTextBox_update_record.TabIndex = 3;
            this.richTextBox_update_record.Text = resources.GetString("richTextBox_update_record.Text");
            this.richTextBox_update_record.LinkClicked += new System.Windows.Forms.LinkClickedEventHandler(this.richTextBox_update_record_LinkClicked);
            // 
            // Update_record
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(489, 266);
            this.Controls.Add(this.richTextBox_update_record);
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
        private System.Windows.Forms.RichTextBox richTextBox_update_record;
    }
}