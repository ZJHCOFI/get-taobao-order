
namespace Get_taobao_order
{
    partial class Main
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
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Main));
            this.label_title = new System.Windows.Forms.Label();
            this.groupBox_input = new System.Windows.Forms.GroupBox();
            this.panel_input = new System.Windows.Forms.Panel();
            this.label_tip = new System.Windows.Forms.Label();
            this.radioButton_text = new System.Windows.Forms.RadioButton();
            this.textBox_input_text = new System.Windows.Forms.TextBox();
            this.textBox_input_file = new System.Windows.Forms.TextBox();
            this.radioButton_file = new System.Windows.Forms.RadioButton();
            this.groupBox_output = new System.Windows.Forms.GroupBox();
            this.textBox_output = new System.Windows.Forms.TextBox();
            this.button_start = new System.Windows.Forms.Button();
            this.panel_info = new System.Windows.Forms.Panel();
            this.linkLabel_course = new System.Windows.Forms.LinkLabel();
            this.label3 = new System.Windows.Forms.Label();
            this.linkLabel_author = new System.Windows.Forms.LinkLabel();
            this.label2 = new System.Windows.Forms.Label();
            this.linkLabel_license = new System.Windows.Forms.LinkLabel();
            this.label1 = new System.Windows.Forms.Label();
            this.button_clear = new System.Windows.Forms.Button();
            this.textBox_test = new System.Windows.Forms.TextBox();
            this.progressBar_deal = new System.Windows.Forms.ProgressBar();
            this.label4 = new System.Windows.Forms.Label();
            this.label_test = new System.Windows.Forms.Label();
            this.groupBox_input.SuspendLayout();
            this.panel_input.SuspendLayout();
            this.groupBox_output.SuspendLayout();
            this.panel_info.SuspendLayout();
            this.SuspendLayout();
            // 
            // label_title
            // 
            this.label_title.AutoSize = true;
            this.label_title.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label_title.Location = new System.Drawing.Point(200, 20);
            this.label_title.Name = "label_title";
            this.label_title.Size = new System.Drawing.Size(170, 22);
            this.label_title.TabIndex = 0;
            this.label_title.Text = "淘宝买家订单规整工具";
            // 
            // groupBox_input
            // 
            this.groupBox_input.Controls.Add(this.panel_input);
            this.groupBox_input.Location = new System.Drawing.Point(24, 50);
            this.groupBox_input.Name = "groupBox_input";
            this.groupBox_input.Size = new System.Drawing.Size(556, 198);
            this.groupBox_input.TabIndex = 0;
            this.groupBox_input.TabStop = false;
            this.groupBox_input.Text = "输入区";
            // 
            // panel_input
            // 
            this.panel_input.AllowDrop = true;
            this.panel_input.Controls.Add(this.label_tip);
            this.panel_input.Controls.Add(this.radioButton_text);
            this.panel_input.Controls.Add(this.textBox_input_text);
            this.panel_input.Controls.Add(this.textBox_input_file);
            this.panel_input.Controls.Add(this.radioButton_file);
            this.panel_input.Location = new System.Drawing.Point(7, 16);
            this.panel_input.Name = "panel_input";
            this.panel_input.Size = new System.Drawing.Size(542, 176);
            this.panel_input.TabIndex = 0;
            this.panel_input.DragDrop += new System.Windows.Forms.DragEventHandler(this.panel_input_DragDrop);
            this.panel_input.DragEnter += new System.Windows.Forms.DragEventHandler(this.panel_input_DragEnter);
            // 
            // label_tip
            // 
            this.label_tip.ForeColor = System.Drawing.Color.Red;
            this.label_tip.Location = new System.Drawing.Point(9, 81);
            this.label_tip.Name = "label_tip";
            this.label_tip.Size = new System.Drawing.Size(74, 85);
            this.label_tip.TabIndex = 4;
            this.label_tip.Text = "建议使用手动复制输入，文件导入很容易乱码";
            // 
            // radioButton_text
            // 
            this.radioButton_text.Location = new System.Drawing.Point(9, 36);
            this.radioButton_text.Name = "radioButton_text";
            this.radioButton_text.Size = new System.Drawing.Size(74, 42);
            this.radioButton_text.TabIndex = 1;
            this.radioButton_text.TabStop = true;
            this.radioButton_text.Text = "手动复制输入";
            this.radioButton_text.UseVisualStyleBackColor = true;
            this.radioButton_text.CheckedChanged += new System.EventHandler(this.radioButton_text_CheckedChanged);
            // 
            // textBox_input_text
            // 
            this.textBox_input_text.Enabled = false;
            this.textBox_input_text.Location = new System.Drawing.Point(89, 35);
            this.textBox_input_text.MaxLength = 16777216;
            this.textBox_input_text.Multiline = true;
            this.textBox_input_text.Name = "textBox_input_text";
            this.textBox_input_text.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBox_input_text.Size = new System.Drawing.Size(446, 135);
            this.textBox_input_text.TabIndex = 3;
            this.textBox_input_text.Text = "（如需手动复制输入，请先点选）";
            // 
            // textBox_input_file
            // 
            this.textBox_input_file.BackColor = System.Drawing.SystemColors.Window;
            this.textBox_input_file.Enabled = false;
            this.textBox_input_file.Location = new System.Drawing.Point(89, 8);
            this.textBox_input_file.MaxLength = 65536;
            this.textBox_input_file.Name = "textBox_input_file";
            this.textBox_input_file.Size = new System.Drawing.Size(446, 23);
            this.textBox_input_file.TabIndex = 2;
            this.textBox_input_file.Text = "（如需选择文件，请先点选）";
            // 
            // radioButton_file
            // 
            this.radioButton_file.AutoSize = true;
            this.radioButton_file.Location = new System.Drawing.Point(9, 9);
            this.radioButton_file.Name = "radioButton_file";
            this.radioButton_file.Size = new System.Drawing.Size(74, 21);
            this.radioButton_file.TabIndex = 0;
            this.radioButton_file.TabStop = true;
            this.radioButton_file.Text = "选择文件";
            this.radioButton_file.UseVisualStyleBackColor = true;
            this.radioButton_file.CheckedChanged += new System.EventHandler(this.radioButton_file_CheckedChanged);
            // 
            // groupBox_output
            // 
            this.groupBox_output.Controls.Add(this.textBox_output);
            this.groupBox_output.Location = new System.Drawing.Point(24, 281);
            this.groupBox_output.Name = "groupBox_output";
            this.groupBox_output.Size = new System.Drawing.Size(556, 179);
            this.groupBox_output.TabIndex = 1;
            this.groupBox_output.TabStop = false;
            this.groupBox_output.Text = "输出区";
            // 
            // textBox_output
            // 
            this.textBox_output.Enabled = false;
            this.textBox_output.Location = new System.Drawing.Point(10, 23);
            this.textBox_output.MaxLength = 16777216;
            this.textBox_output.Multiline = true;
            this.textBox_output.Name = "textBox_output";
            this.textBox_output.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBox_output.Size = new System.Drawing.Size(540, 150);
            this.textBox_output.TabIndex = 0;
            this.textBox_output.Text = "（请点击“开始规整”）";
            // 
            // button_start
            // 
            this.button_start.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button_start.Location = new System.Drawing.Point(168, 473);
            this.button_start.Name = "button_start";
            this.button_start.Size = new System.Drawing.Size(108, 35);
            this.button_start.TabIndex = 2;
            this.button_start.Text = "开始规整";
            this.button_start.UseVisualStyleBackColor = true;
            this.button_start.Click += new System.EventHandler(this.button_start_Click);
            // 
            // panel_info
            // 
            this.panel_info.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.panel_info.Controls.Add(this.linkLabel_course);
            this.panel_info.Controls.Add(this.label3);
            this.panel_info.Controls.Add(this.linkLabel_author);
            this.panel_info.Controls.Add(this.label2);
            this.panel_info.Controls.Add(this.linkLabel_license);
            this.panel_info.Controls.Add(this.label1);
            this.panel_info.Location = new System.Drawing.Point(0, 524);
            this.panel_info.Name = "panel_info";
            this.panel_info.Size = new System.Drawing.Size(605, 29);
            this.panel_info.TabIndex = 31;
            // 
            // linkLabel_course
            // 
            this.linkLabel_course.AutoSize = true;
            this.linkLabel_course.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.linkLabel_course.Location = new System.Drawing.Point(71, 2);
            this.linkLabel_course.Name = "linkLabel_course";
            this.linkLabel_course.Size = new System.Drawing.Size(65, 20);
            this.linkLabel_course.TabIndex = 4;
            this.linkLabel_course.TabStop = true;
            this.linkLabel_course.Text = "点击此处";
            this.linkLabel_course.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel_course_LinkClicked);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.Location = new System.Drawing.Point(3, 2);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(79, 20);
            this.label3.TabIndex = 5;
            this.label3.Text = "操作教程：";
            // 
            // linkLabel_author
            // 
            this.linkLabel_author.AutoSize = true;
            this.linkLabel_author.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.linkLabel_author.Location = new System.Drawing.Point(531, 2);
            this.linkLabel_author.Name = "linkLabel_author";
            this.linkLabel_author.Size = new System.Drawing.Size(66, 20);
            this.linkLabel_author.TabIndex = 6;
            this.linkLabel_author.TabStop = true;
            this.linkLabel_author.Text = "ZJHCOFI";
            this.linkLabel_author.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel_author_LinkClicked);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(444, 2);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(92, 20);
            this.label2.TabIndex = 2;
            this.label2.Text = "Copyright ©";
            // 
            // linkLabel_license
            // 
            this.linkLabel_license.AutoSize = true;
            this.linkLabel_license.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.linkLabel_license.Location = new System.Drawing.Point(209, 2);
            this.linkLabel_license.Name = "linkLabel_license";
            this.linkLabel_license.Size = new System.Drawing.Size(230, 20);
            this.linkLabel_license.TabIndex = 5;
            this.linkLabel_license.TabStop = true;
            this.linkLabel_license.Text = "BSD 3-Clause \"New\" or \"Revised\"";
            this.linkLabel_license.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel_license_LinkClicked);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(139, 2);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(79, 20);
            this.label1.TabIndex = 1;
            this.label1.Text = "开源协议：";
            // 
            // button_clear
            // 
            this.button_clear.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button_clear.Location = new System.Drawing.Point(305, 473);
            this.button_clear.Name = "button_clear";
            this.button_clear.Size = new System.Drawing.Size(108, 35);
            this.button_clear.TabIndex = 3;
            this.button_clear.Text = "清空信息";
            this.button_clear.UseVisualStyleBackColor = true;
            this.button_clear.Click += new System.EventHandler(this.button_clear_Click);
            // 
            // textBox_test
            // 
            this.textBox_test.Location = new System.Drawing.Point(695, 50);
            this.textBox_test.MaxLength = 16777216;
            this.textBox_test.Multiline = true;
            this.textBox_test.Name = "textBox_test";
            this.textBox_test.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBox_test.Size = new System.Drawing.Size(401, 438);
            this.textBox_test.TabIndex = 1;
            // 
            // progressBar_deal
            // 
            this.progressBar_deal.Location = new System.Drawing.Point(85, 255);
            this.progressBar_deal.Name = "progressBar_deal";
            this.progressBar_deal.Size = new System.Drawing.Size(489, 20);
            this.progressBar_deal.TabIndex = 7;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label4.Location = new System.Drawing.Point(25, 255);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(56, 17);
            this.label4.TabIndex = 9;
            this.label4.Text = "进度条：";
            // 
            // label_test
            // 
            this.label_test.AutoSize = true;
            this.label_test.Font = new System.Drawing.Font("微软雅黑", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label_test.ForeColor = System.Drawing.Color.Black;
            this.label_test.Location = new System.Drawing.Point(824, 501);
            this.label_test.Name = "label_test";
            this.label_test.Size = new System.Drawing.Size(157, 28);
            this.label_test.TabIndex = 32;
            this.label_test.Text = "🤪测试专用框⬆️";
            this.label_test.Visible = false;
            // 
            // Main
            // 
            this.AcceptButton = this.button_start;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(604, 551);
            this.Controls.Add(this.label_test);
            this.Controls.Add(this.progressBar_deal);
            this.Controls.Add(this.textBox_test);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.button_clear);
            this.Controls.Add(this.panel_info);
            this.Controls.Add(this.button_start);
            this.Controls.Add(this.groupBox_output);
            this.Controls.Add(this.groupBox_input);
            this.Controls.Add(this.label_title);
            this.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.Name = "Main";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "淘宝买家订单规整工具(更新时间：2022.11.26 22:18)";
            this.groupBox_input.ResumeLayout(false);
            this.panel_input.ResumeLayout(false);
            this.panel_input.PerformLayout();
            this.groupBox_output.ResumeLayout(false);
            this.groupBox_output.PerformLayout();
            this.panel_info.ResumeLayout(false);
            this.panel_info.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label_title;
        private System.Windows.Forms.GroupBox groupBox_input;
        private System.Windows.Forms.TextBox textBox_input_text;
        private System.Windows.Forms.TextBox textBox_input_file;
        private System.Windows.Forms.GroupBox groupBox_output;
        private System.Windows.Forms.TextBox textBox_output;
        private System.Windows.Forms.Button button_start;
        private System.Windows.Forms.Panel panel_info;
        private System.Windows.Forms.LinkLabel linkLabel_author;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.LinkLabel linkLabel_license;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RadioButton radioButton_text;
        private System.Windows.Forms.RadioButton radioButton_file;
        private System.Windows.Forms.LinkLabel linkLabel_course;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button button_clear;
        private System.Windows.Forms.Panel panel_input;
        private System.Windows.Forms.Label label_tip;
        private System.Windows.Forms.TextBox textBox_test;
        private System.Windows.Forms.ProgressBar progressBar_deal;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label_test;
    }
}

