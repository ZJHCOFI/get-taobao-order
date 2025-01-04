using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace Get_taobao_order
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
        }

        // 编写时间：2022.11.19
        // 更新时间：2025.01.04 22:30
        // Edit by ZJHCOFI
        // 博客Blog：https://zjhcofi.com
        // Github：https://github.com/zjhcofi
        // 功能：规整淘宝的买家订单
        // 开源协议：BSD 3-Clause “New” or “Revised” License (https://choosealicense.com/licenses/bsd-3-clause/)
        // 后续更新或漏洞修补通告页面：https://github.com/zjhcofi/get-taobao-order
        //=====更新日志=====
        //2022.11.19 23:57
        // 第一个版本发布
        //2023.01.19 01:08
        // 修改了用于分割的字符串，解决了某些使用场景下出现的订单号错误的bug
        // 问题提出：小布布布（B站UID：3184592）
        // 分析解决：cnlnn、ZJHCOFI
        //2024.07.22 14:20
        // 解决了预售订单场景下，获取订单状态失败导致程序报错的问题
        // 问题提出：吃五个苹果（B站UID：1579744）
        // 分析解决：ZJHCOFI
        //2025.01.03 01:00
        // 解决了商品含有多种属性(分类)的场景下，只输出一种属性(分类)的问题
        // 问题提出：狐狸喵Official（B站UID：290707837）
        // 分析解决：ZJHCOFI
        //2025.01.04 22:30
        // 1、解决了在网页上进行订单筛选后，原始数据规整失败的问题
        // 2、新增了子订单状态的输出
        // 问题提出：csjjjj123（github.com/csjjjj123）、狐狸喵Official（B站UID：290707837）
        // 分析解决：ZJHCOFI
        //==================

        //=======文本处理委托-First_deal=======
        private static bool FindCallback_text_deal_1(string val)
        {
            if (val.IndexOf("notShowSellerInfo") >= 0)
            {
                return true;
            }
            return false;
        }

        //=======点击开源协议=======
        private void linkLabel_license_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://choosealicense.com/licenses/bsd-3-clause/");
        }

        //=======点击作者名字=======
        private void linkLabel_author_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (label_test.Visible == false)
            {   
                MessageBox.Show("感谢您点击作者，触发彩蛋！", "Easter egg");
                this.Width = this.Width + 520;
                label_test.Visible = true;
                System.Diagnostics.Process.Start("https://www.zjhcofi.com");
            }
            else
            {
                this.Width = this.Width - 520;
                label_test.Visible = false;
            }
        }

        //=======点击“软件更新日志”=======
        private void linkLabel_update_record_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Update_record Update_record = new Update_record();
            Update_record.ShowDialog();
        }

        //=======点击操作教程=======
        private void linkLabel_course_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://www.zjhcofi.com/2022/12/09/get-taobao-order/");
        }

        //=======如果“选择文件”选项改变=======
        private void radioButton_file_CheckedChanged(object sender, EventArgs e)
        {
            //如果“选择文件”选项被选中
            if (radioButton_file.Checked == true)
            {
                textBox_input_file.Text = "【请将txt文档拖至黄色区域，仅支持读取一个txt文档】";
                panel_input.BackColor = Color.LightYellow;
            }
            //否则
            else
            {
                textBox_input_file.Text = "（如需选择文件，请先点选）";
                panel_input.BackColor = SystemColors.Control;
            }
        }

        //=======如果“手动输入”选项改变=======
        private void radioButton_text_CheckedChanged(object sender, EventArgs e)
        {
            //如果“手动输入”选项被选中
            if(radioButton_text.Checked == true)
            {
                textBox_input_text.Enabled = true;
                textBox_input_text.Clear();
            }
            //否则
            else
            {
                textBox_input_text.Enabled = false;
                textBox_input_text.Text = "（如需手动复制输入，请先点选）";
            }
        }

        //=======当有文件拖入控件=======
        private void panel_input_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effect = DragDropEffects.All;
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
        }

        private void panel_input_DragEnter(object sender, DragEventArgs e)
        {
            string file = ((System.Array)e.Data.GetData(DataFormats.FileDrop)).GetValue(0).ToString();
            //如果是文件并且存在
            if (File.Exists(file))
            {
                //检查文件类型
                string Extension = Path.GetExtension(file).ToLower();
                //如果是txt
                if (Extension == ".txt")
                {
                    textBox_input_file.Text = file.ToString();
                    string text = File.ReadAllText(@file);
                    //如果字符串长度超过16777216
                    if (text.Length >= 16777216)
                    {
                        MessageBox.Show("您txt里的内容太多，请删减一些内容再拖入吧", "提示");
                    }
                    else
                    {
                        textBox_input_text.Text = text;
                    }
                }
                //如果不是txt
                else
                {
                    MessageBox.Show("文件类型错误，请拖入txt文档", "提示");
                }
            }
            //如果是文件夹并且存在
            else if (Directory.Exists(file))
            {
                MessageBox.Show("拖入文件夹不能识别，请拖入txt文档", "提示");
            }
            //如果都不是
            else
            {
                MessageBox.Show("拖入错误", "提示");
            }
        }

        //=======点击开始规整=======
        private void button_start_Click(object sender, EventArgs e)
        {
            if (textBox_input_text.Text == "" || textBox_input_text.Text == "（如需手动复制输入，请先点选）")
            {
                MessageBox.Show("请输入内容或拖入文件", "提示");
            }
            else
            {
                //清空输出区
                textBox_output.Text = "";
                button_start.Text = "正在规整";
                button_start.Enabled = false;
                //定义错误次数
                int Int_error = 0;
                //定义输出内容
                List<string> List_dd_id = new List<string>();  //订单号
                List<string> List_dd_create_time = new List<string>();  //订单创建时间
                List<string> List_dd_currency_symbol = new List<string>();  //订单货币符号
                List<string> List_dd_pay = new List<string>();  //订单实付金额
                List<string> List_dd_state = new List<string>();  //订单状态
                List<string> List_dd_shop_name = new List<string>();  //订单店铺名称
                List<string> List_dd_postFees_prefix = new List<string>();  //订单补充项目前缀
                List<string> List_dd_postFees_suffix = new List<string>();  //订单补充项目后缀
                List<string> List_dd_postFees_value = new List<string>();  //订单补充项目数值
                List<string> List_goods_name = new List<string>();  //商品名
                List<string> List_goods_price = new List<string>();  //商品单价
                List<string> List_goods_quantity = new List<string>();  //商品数量
                List<string> List_all_output = new List<string>();  //总输出

                try
                {
                    //鼠标等待
                    this.Cursor = Cursors.WaitCursor;

                    //去除在网页进行订单筛选后的原始数据中的换行符
                    textBox_test.Text = (((textBox_input_text.Text.Replace("</font>\r\n", "").Replace("</font>\n", "")).Replace("</font>\r", "")).Replace("<font color=\\\"red\\\">", ""));

                    //逐行读取源内容并存进数组中
                    string[] StrArray_source_text = new string[textBox_test.Lines.Length];
                    for (int Int_i = 0; Int_i < textBox_test.Lines.Length; Int_i++)
                    {
                        StrArray_source_text[Int_i] = textBox_test.Lines[Int_i];
                    }
                    //网页源代码预先处理--步骤1：取出“notShowSellerInfo”字符所在的数组到新数组
                    string[] StrArray_deal_text_1 = Array.FindAll(StrArray_source_text, FindCallback_text_deal_1);
                    progressBar_deal.Value = 5;
                    //网页源代码预先处理--步骤2：记录步骤1新数组中的内容
                    string Str_deal_text_2 = "";
                    foreach (string Str_i in StrArray_deal_text_1)
                    {
                        //步骤2：
                        Str_deal_text_2 += Str_i;
                    }
                    //测试输出
                    textBox_test.Text = Str_deal_text_2;
                    progressBar_deal.Value = 15;
                    //网页源代码预先处理--步骤3：将步骤2中的反斜杠“\”、中竖线“|”和换行符清除
                    string Str_deal_text_3 = (((Str_deal_text_2.Replace("\r\n", "")).Replace("\r", "")).Replace("\n", "")).Replace("|", "");
                    Str_deal_text_3 = ((Str_deal_text_3.Replace("\\u", "zjhcofi")).Replace("\\", "")).Replace("zjhcofi", "\\u");
                    //测试输出
                    textBox_test.Text = Str_deal_text_3;
                    //网页源代码预先处理--步骤4：将步骤3中的Unicode转为中文
                    string Str_deal_text_4 = Regex.Unescape(Str_deal_text_3);
                    //测试输出
                    textBox_test.Text = Str_deal_text_4;
                    progressBar_deal.Value = 20;

                    //使用 batchGroupTips 拆分主订单--步骤5：
                    string[] StrArray_deal_text_5 = Regex.Split(Str_deal_text_4, "batchGroupTips");
                    StrArray_deal_text_5 = StrArray_deal_text_5.Skip(1).ToArray();
                    progressBar_deal.Value = 30;

                    //使用 skuText 拆分子订单--步骤6：
                    int Int_ProgressBar_deal_add = Convert.ToInt32(70/StrArray_deal_text_5.Length);
                    foreach (string Str_i in StrArray_deal_text_5)
                    {
                        string[] StrArray_deal_text_6 = Regex.Split(Str_i, "skuText");
                        //子订单信息获取--步骤7：
                        int Int_a = 1;
                        foreach (string Str_b in StrArray_deal_text_6)
                        {
                            //测试输出
                            textBox_test.Text = Str_b;
                            if (Int_a == 1)
                            {
                                //订单号截取
                                //string[] dd_id_split_1 = Regex.Split(Str_b, ",\"inHold\":");
                                //string[] dd_id_split_2 = Regex.Split(dd_id_split_1[0], "\"id\":");
                                string[] StrArray_dd_id_split_1 = Regex.Split(Str_b, "\",\"operations\":\\[{\"");
                                string[] StrArray_dd_id_split_2 = Regex.Split(StrArray_dd_id_split_1[0], "\"id\":\"");
                                string[] StrArray_dd_id_split_3 = Regex.Split(StrArray_dd_id_split_2[1], ",");
                                List_dd_id.Add(StrArray_dd_id_split_3[0]);

                                //订单创建时间截取
                                //string[] dd_create_time_split = Str_b.Split(new string[] { "createTime\":\"", "\"" }, StringSplitOptions.RemoveEmptyEntries);
                                string[] StrArray_dd_create_time_split_1 = Regex.Split(Str_b, "createTime\":\"");
                                string[] StrArray_dd_create_time_split_2 = Regex.Split(StrArray_dd_create_time_split_1[1], "\"");
                                List_dd_create_time.Add(StrArray_dd_create_time_split_2[0]);

                                //订单货币符号截取
                                //string[] dd_currency_symbol_split = Str_b.Split(new string[] { "\"currencySymbol\":\"", "\"" }, StringSplitOptions.RemoveEmptyEntries);
                                string[] StrArray_dd_currency_symbol_split_1 = Regex.Split(Str_b, "\"currencySymbol\":\"");
                                string[] StrArray_dd_currency_symbol_split_2 = Regex.Split(StrArray_dd_currency_symbol_split_1[1], "\"");
                                List_dd_currency_symbol.Add(StrArray_dd_currency_symbol_split_2[0]);

                                //订单实付金额截取
                                //string[] dd_pay_split = Str_b.Split(new string[] { "actualFee\":\"", "\"" }, StringSplitOptions.RemoveEmptyEntries);
                                string[] StrArray_dd_pay_split_1 = Regex.Split(Str_b, "actualFee\":\"");
                                string[] StrArray_dd_pay_split_2 = Regex.Split(StrArray_dd_pay_split_1[1], "\"");
                                List_dd_pay.Add(StrArray_dd_pay_split_2[0]);

                                //订单状态截取
                                //string[] dd_state_split = Str_b.Split(new string[] { "\" }],\"text\":\"", "\"" }, StringSplitOptions.RemoveEmptyEntries);
                                if (Str_b.Contains("],\"text\":\"") == true)
                                {
                                    string[] StrArray_dd_state_split_1 = Regex.Split(Str_b, "],\"text\":\"");
                                    string[] StrArray_dd_state_split_2 = Regex.Split(StrArray_dd_state_split_1[1], "\"");
                                    List_dd_state.Add(StrArray_dd_state_split_2[0]);
                                }
                                else if (Str_b.Contains("\"linkTitle\":\"预售\"") == true)
                                {
                                    List_dd_state.Add("预售");
                                }
                                else
                                {
                                    List_dd_state.Add("未知状态");
                                }

                                //订单店铺名称截取
                                //string[] dd_shop_name_split = Str_b.Split(new string[] { "\"shopName\":\"", "\"" }, StringSplitOptions.RemoveEmptyEntries);
                                string[] StrArray_dd_shop_name_split_1 = Regex.Split(Str_b, "\"shopName\":\"");
                                if(StrArray_dd_shop_name_split_1.Length < 2)
                                {
                                    List_dd_shop_name.Add("");
                                }
                                else
                                {
                                    string[] StrArray_dd_shop_name_split_2 = Regex.Split(StrArray_dd_shop_name_split_1[1], "\"");
                                    List_dd_shop_name.Add(StrArray_dd_shop_name_split_2[0]);
                                }

                                //订单补充项目前缀截取
                                //string[] dd_postFees_prefix_split = Str_b.Split(new string[] { "{\"prefix\":\"", "\"" }, StringSplitOptions.RemoveEmptyEntries);
                                string[] StrArray_dd_postFees_prefix_split_1 = Regex.Split(Str_b, "{\"prefix\":\"");
                                if(StrArray_dd_postFees_prefix_split_1.Length < 2)
                                {
                                    List_dd_postFees_prefix.Add("");
                                }
                                else
                                {
                                    string[] StrArray_dd_postFees_prefix_split_2 = Regex.Split(StrArray_dd_postFees_prefix_split_1[1], "\"");
                                    List_dd_postFees_prefix.Add(StrArray_dd_postFees_prefix_split_2[0]);
                                }

                                //订单补充项目后缀截取
                                //string[] dd_postFees_suffix_split = Str_b.Split(new string[] { "suffix\":\"", "\"" }, StringSplitOptions.RemoveEmptyEntries);
                                string[] StrArray_dd_postFees_suffix_split_1 = Regex.Split(Str_b, "suffix\":\"");
                                if(StrArray_dd_postFees_suffix_split_1.Length < 2)
                                {
                                    List_dd_postFees_suffix.Add("");
                                }
                                else
                                {
                                    string[] StrArray_dd_postFees_suffix_split_2 = Regex.Split(StrArray_dd_postFees_suffix_split_1[1], "\"");
                                    List_dd_postFees_suffix.Add(StrArray_dd_postFees_suffix_split_2[0]);
                                }

                                //订单补充项目数值截取
                                //string[] dd_postFees_value_split = Str_b.Split(new string[] { ",\"value\":\"", "\"" }, StringSplitOptions.RemoveEmptyEntries);
                                string[] StrArray_dd_postFees_value_split_1 = Regex.Split(Str_b, ",\"value\":\"");
                                if(StrArray_dd_postFees_value_split_1.Length < 2)
                                {
                                    List_dd_postFees_value.Add("");
                                }
                                else
                                {
                                    string[] StrArray_dd_postFees_value_split_2 = Regex.Split(StrArray_dd_postFees_value_split_1[1], "\"");
                                    List_dd_postFees_value.Add(StrArray_dd_postFees_value_split_2[0].Replace(StrArray_dd_currency_symbol_split_2[0], ""));
                                }

                                Int_a += 1;
                            }
                            else if (Int_a == 2)
                            {
                                //定义输出内容
                                string Str_goods_tpye_prefix = "";  //商品属性(分类)前缀
                                string Str_goods_tpye = "";  //商品属性(分类)详情
                                string Str_goods_state = "";  //商品状态详情
                                List<string> List_goods_tpye_prefix = new List<string>();  //商品属性(分类)前缀(中转用)
                                List<string> List_goods_tpye = new List<string>();  //商品属性(分类)详情(中转用)
                                List<string> List_goods_state = new List<string>();  //商品状态详情(中转用)

                                //商品名截取
                                //string[] goods_name_split = Str_b.Split(new string[] { "\"title\":\"", "\"" }, StringSplitOptions.RemoveEmptyEntries);
                                string[] StrArray_goods_name_split_1 = Regex.Split(Str_b, "\"title\":\"");
                                string[] StrArray_goods_name_split_2 = Regex.Split(StrArray_goods_name_split_1[1], "\"");
                                List_goods_name.Add(StrArray_goods_name_split_2[0]);

                                //商品单价截取
                                //string[] goods_price_split = Str_b.Split(new string[] { "\"realTotal\":\"", "\"" }, StringSplitOptions.RemoveEmptyEntries);
                                string[] StrArray_goods_price_split_1 = Regex.Split(Str_b, "\"realTotal\":\"");
                                string[] StrArray_goods_price_split_2 = Regex.Split(StrArray_goods_price_split_1[1], "\"");
                                List_goods_price.Add(StrArray_goods_price_split_2[0]);

                                //商品数量截取
                                //string[] goods_quantity_split = Str_b.Split(new string[] { "\"quantity\":\"", "\"" }, StringSplitOptions.RemoveEmptyEntries);
                                string[] StrArray_goods_quantity_split_1 = Regex.Split(Str_b, "\"quantity\":\"");
                                string[] StrArray_goods_quantity_split_2 = Regex.Split(StrArray_goods_quantity_split_1[1], "\"");
                                List_goods_quantity.Add(StrArray_goods_quantity_split_2[0]);

                                //商品属性(分类)总信息截取
                                string[] StrArray_goods_tpye_info_split_1 = Regex.Split(Str_b, "\\[");
                                string[] StrArray_goods_tpye_info_split_2 = Regex.Split(StrArray_goods_tpye_info_split_1[1], "\\]");
                                string Str_goods_tpye_info = string.Join("", StrArray_goods_tpye_info_split_2[0]);

                                //商品属性(分类)前缀截取
                                //string[] goods_tpye_prefix_split = Str_b.Split(new string[] { "\"name\":\"", "\"" }, StringSplitOptions.RemoveEmptyEntries);
                                string[] StrArray_goods_tpye_prefix_split_1 = Regex.Split(Str_goods_tpye_info, "\"name\":\"");
                                if (StrArray_goods_tpye_prefix_split_1.Length < 2)
                                {
                                    Str_goods_tpye_prefix = "(无)";
                                }
                                else
                                {
                                    //获取所有分类到List
                                    for (int Int_goods_tpye_num = 1; Int_goods_tpye_num < StrArray_goods_tpye_prefix_split_1.Length; Int_goods_tpye_num++)
                                    {
                                        string[] StrArray_goods_tpye_prefix_split_2 = Regex.Split(StrArray_goods_tpye_prefix_split_1[Int_goods_tpye_num], "\"");
                                        List_goods_tpye_prefix.Add(StrArray_goods_tpye_prefix_split_2[0].Replace(";",","));
                                    }
                                    //将List转换成字符串并输出
                                    Str_goods_tpye_prefix = string.Join(";", List_goods_tpye_prefix);
                                }

                                //商品属性(分类)详情截取
                                //string[] goods_tpye_split = Str_b.Split(new string[] { "\"value\":\"", "\"" }, StringSplitOptions.RemoveEmptyEntries);
                                string[] StrArray_goods_tpye_split_1 = Regex.Split(Str_goods_tpye_info, "\"value\":\"");
                                if (StrArray_goods_tpye_split_1.Length < 2)
                                {
                                    Str_goods_tpye = "(无)";
                                }
                                else
                                {
                                    //获取所有分类到List
                                    for (int Int_goods_tpye_num = 1; Int_goods_tpye_num < StrArray_goods_tpye_split_1.Length; Int_goods_tpye_num++)
                                    {
                                        string[] StrArray_goods_tpye_split_2 = Regex.Split(StrArray_goods_tpye_split_1[Int_goods_tpye_num], "\"");
                                        List_goods_tpye.Add(StrArray_goods_tpye_split_2[0].Replace(";", ","));
                                    }
                                    //将List转换成字符串并输出
                                    Str_goods_tpye = string.Join(";", List_goods_tpye);
                                }

                                //商品状态总信息截取
                                string[] StrArray_goods_state_info_split_1 = Regex.Split(Str_b, "operations"); 
                                if (StrArray_goods_state_info_split_1.Length < 2)
                                {
                                    Str_goods_state = "(无)";
                                }
                                else
                                {
                                    string[] StrArray_goods_state_info_split_2 = Regex.Split(StrArray_goods_state_info_split_1[1], "\\[");
                                    string[] StrArray_goods_state_info_split_3 = Regex.Split(StrArray_goods_state_info_split_2[1], "\\]");
                                    string Str_goods_state_info = string.Join("", StrArray_goods_state_info_split_3[0]);

                                    //商品状态详情截取
                                    string[] StrArray_goods_state_split_1 = Regex.Split(Str_goods_state_info, "\"text\":\"");
                                    if (StrArray_goods_state_split_1.Length < 2)
                                    {
                                        Str_goods_state = "(无)";
                                    }
                                    else
                                    {
                                        //获取所有分类到List
                                        for (int Int_goods_state_num = 1; Int_goods_state_num < StrArray_goods_state_split_1.Length; Int_goods_state_num++)
                                        {
                                            string[] StrArray_goods_state_split_2 = Regex.Split(StrArray_goods_state_split_1[Int_goods_state_num], "\"");
                                            List_goods_state.Add(StrArray_goods_state_split_2[0].Replace(";", ","));
                                        }
                                        //将List转换成字符串并输出
                                        Str_goods_state = string.Join(";", List_goods_state);
                                    }
                                }


                                //输出
                                List_all_output.Add(List_dd_create_time.Last() + "|" + List_dd_state.Last() + "|" + Str_goods_state + "|" + List_goods_name.Last() + "|" + Str_goods_tpye_prefix.ToString() + "|" + Str_goods_tpye.ToString()
                                    + "|" + List_dd_currency_symbol.Last() + "|" + List_goods_price.Last() + "|" + List_goods_quantity.Last() + "|" + List_dd_postFees_prefix.Last() +
                                    List_dd_postFees_suffix.Last() + "|" + List_dd_postFees_value.Last() + "|" + List_dd_pay.Last() + "|" + List_dd_shop_name.Last() + "|" + List_dd_id.Last());
                                textBox_test.Text = List_dd_create_time.Last() + "|" + List_dd_state.Last() + "|" + Str_goods_state + "|" + List_goods_name.Last() + "|" + Str_goods_tpye_prefix.ToString() + "|" + Str_goods_tpye.ToString()
                                    + "|" + List_dd_currency_symbol.Last() + "|" + List_goods_price.Last() + "|" + List_goods_quantity.Last() + "|" + List_dd_postFees_prefix.Last() +
                                    List_dd_postFees_suffix.Last() + "|" + List_dd_postFees_value.Last() + "|" + List_dd_pay.Last() + "|" + List_dd_shop_name.Last() + "|" + List_dd_id.Last();

                                Int_a += 1;
                            }
                            else 
                            {
                                //定义输出内容
                                string Str_goods_tpye_prefix = "";  //商品属性(分类)前缀
                                string Str_goods_tpye = "";  //商品属性(分类)详情
                                string Str_goods_state = "";  //商品状态详情
                                List<string> List_goods_tpye_prefix = new List<string>();  //商品属性(分类)前缀(中转用)
                                List<string> List_goods_tpye = new List<string>();  //商品属性(分类)详情(中转用)
                                List<string> List_goods_state = new List<string>();  //商品状态详情(中转用)

                                //商品名截取
                                //string[] goods_name_split = Str_b.Split(new string[] { "\"title\":\"", "\"" }, StringSplitOptions.RemoveEmptyEntries);
                                string[] StrArray_goods_name_split_1 = Regex.Split(Str_b, "\"title\":\"");
                                string[] StrArray_goods_name_split_2 = Regex.Split(StrArray_goods_name_split_1[1], "\"");
                                List_goods_name.Add(StrArray_goods_name_split_2[0]);

                                //商品单价截取
                                //string[] goods_price_split = Str_b.Split(new string[] { "\"realTotal\":\"", "\"" }, StringSplitOptions.RemoveEmptyEntries);
                                string[] StrArray_goods_price_split_1 = Regex.Split(Str_b, "\"realTotal\":\"");
                                string[] StrArray_goods_price_split_2 = Regex.Split(StrArray_goods_price_split_1[1], "\"");
                                List_goods_price.Add(StrArray_goods_price_split_2[0]);

                                //商品数量截取
                                //string[] goods_quantity_split = Str_b.Split(new string[] { "\"quantity\":\"", "\"" }, StringSplitOptions.RemoveEmptyEntries);
                                string[] StrArray_goods_quantity_split_1 = Regex.Split(Str_b, "\"quantity\":\"");
                                string[] StrArray_goods_quantity_split_2 = Regex.Split(StrArray_goods_quantity_split_1[1], "\"");
                                List_goods_quantity.Add(StrArray_goods_quantity_split_2[0]);

                                //商品属性(分类)总信息截取
                                string[] StrArray_goods_tpye_info_split_1 = Regex.Split(Str_b, "\\[");
                                string[] StrArray_goods_tpye_info_split_2 = Regex.Split(StrArray_goods_tpye_info_split_1[1], "\\]");
                                string Str_goods_tpye_info = string.Join("", StrArray_goods_tpye_info_split_2[0]);

                                //商品属性(分类)前缀截取
                                //string[] goods_tpye_prefix_split = Str_b.Split(new string[] { "\"name\":\"", "\"" }, StringSplitOptions.RemoveEmptyEntries);
                                string[] StrArray_goods_tpye_prefix_split_1 = Regex.Split(Str_goods_tpye_info, "\"name\":\"");
                                if (StrArray_goods_tpye_prefix_split_1.Length < 2)
                                {
                                    Str_goods_tpye_prefix = "(无)";
                                }
                                else
                                {
                                    //获取所有分类到List
                                    for (int Int_goods_tpye_num = 1; Int_goods_tpye_num < StrArray_goods_tpye_prefix_split_1.Length; Int_goods_tpye_num++)
                                    {
                                        string[] StrArray_goods_tpye_prefix_split_2 = Regex.Split(StrArray_goods_tpye_prefix_split_1[Int_goods_tpye_num], "\"");
                                        List_goods_tpye_prefix.Add(StrArray_goods_tpye_prefix_split_2[0].Replace(";", ","));
                                    }
                                    //将List转换成字符串并输出
                                    Str_goods_tpye_prefix = string.Join(";", List_goods_tpye_prefix);
                                }

                                //商品属性(分类)详情截取
                                //string[] goods_tpye_split = Str_b.Split(new string[] { "\"value\":\"", "\"" }, StringSplitOptions.RemoveEmptyEntries);
                                string[] StrArray_goods_tpye_split_1 = Regex.Split(Str_goods_tpye_info, "\"value\":\"");
                                if (StrArray_goods_tpye_split_1.Length < 2)
                                {
                                    Str_goods_tpye = "(无)";
                                }
                                else
                                {
                                    //获取所有分类到List
                                    for (int Int_goods_tpye_num = 1; Int_goods_tpye_num < StrArray_goods_tpye_split_1.Length; Int_goods_tpye_num++)
                                    {
                                        string[] StrArray_goods_tpye_split_2 = Regex.Split(StrArray_goods_tpye_split_1[Int_goods_tpye_num], "\"");
                                        List_goods_tpye.Add(StrArray_goods_tpye_split_2[0].Replace(";", ","));
                                    }
                                    //将List转换成字符串并输出
                                    Str_goods_tpye = string.Join(";", List_goods_tpye);
                                }

                                //商品状态总信息截取
                                string[] StrArray_goods_state_info_split_1 = Regex.Split(Str_b, "operations");
                                if (StrArray_goods_state_info_split_1.Length < 2)
                                {
                                    Str_goods_state = "(无)";
                                }
                                else
                                {
                                    string[] StrArray_goods_state_info_split_2 = Regex.Split(StrArray_goods_state_info_split_1[1], "\\[");
                                    string[] StrArray_goods_state_info_split_3 = Regex.Split(StrArray_goods_state_info_split_2[1], "\\]");
                                    string Str_goods_state_info = string.Join("", StrArray_goods_state_info_split_3[0]);

                                    //商品状态详情截取
                                    string[] StrArray_goods_state_split_1 = Regex.Split(Str_goods_state_info, "\"text\":\"");
                                    if (StrArray_goods_state_split_1.Length < 2)
                                    {
                                        Str_goods_state = "(无)";
                                    }
                                    else
                                    {
                                        //获取所有分类到List
                                        for (int Int_goods_state_num = 1; Int_goods_state_num < StrArray_goods_state_split_1.Length; Int_goods_state_num++)
                                        {
                                            string[] StrArray_goods_state_split_2 = Regex.Split(StrArray_goods_state_split_1[Int_goods_state_num], "\"");
                                            List_goods_state.Add(StrArray_goods_state_split_2[0].Replace(";", ","));
                                        }
                                        //将List转换成字符串并输出
                                        Str_goods_state = string.Join(";", List_goods_state);
                                    }
                                }


                                //输出
                                List_all_output.Add(List_dd_create_time.Last() + "|" + List_dd_state.Last() + "|" + Str_goods_state + "|" + List_goods_name.Last() + "|" + Str_goods_tpye_prefix.ToString() + "|" + Str_goods_tpye.ToString()
                                    + "|" + List_dd_currency_symbol.Last() + "|" + List_goods_price.Last() + "|" + List_goods_quantity.Last() + "|" + "|" + "|" + "|" + List_dd_shop_name.Last() + "|" + List_dd_id.Last());
                                textBox_test.Text = List_dd_create_time.Last() + "|" + List_dd_state.Last() + "|" + Str_goods_state + "|" + List_goods_name.Last() + "|" + Str_goods_tpye_prefix.ToString() + "|" + Str_goods_tpye.ToString()
                                    + "|" + List_dd_currency_symbol.Last() + "|" + List_goods_price.Last() + "|" + List_goods_quantity.Last() + "|" + "|" + "|" + "|" + List_dd_shop_name.Last() + "|" + List_dd_id.Last();

                                Int_a += 1;
                            }
                        }
                        //清空商品信息
                        List_dd_id.Clear();
                        List_dd_create_time.Clear();
                        List_dd_currency_symbol.Clear();
                        List_dd_pay.Clear();
                        List_dd_state.Clear();
                        List_dd_shop_name.Clear();
                        List_dd_postFees_prefix.Clear();
                        List_dd_postFees_suffix.Clear();
                        List_dd_postFees_value.Clear();
                        List_goods_name.Clear();
                        List_goods_price.Clear();
                        List_goods_quantity.Clear();
                        //进度条
                        if (progressBar_deal.Value < 100)
                        {
                            progressBar_deal.Value += Int_ProgressBar_deal_add;
                        }
                    }

                    //输出结果
                    textBox_output.Text = "下单时间|主订单状态|子订单状态|商品名|分类前缀(多种分类以;分隔)|分类详情(多种分类以;分隔)|货币类型|单价|数量|补充项目|补充项目数值|实付金额|店铺名|订单号" + Environment.NewLine;
                    for (int Int_i = 0; Int_i < List_all_output.Count; Int_i++)
                    {
                        textBox_output.Text += List_all_output[Int_i] + Environment.NewLine;
                    }
                    textBox_output.Enabled = true;

                }
                catch (Exception ex)
                {
                    Int_error += 1;
                    MessageBox.Show("规整订单内容出错：" + ex); 
                }
                finally
                {
                    //按钮归零
                    button_start.Text = "开始规整";
                    button_start.Enabled = true;
                    //鼠标正常状态
                    this.Cursor = Cursors.Default;
                    //进度条归零
                    progressBar_deal.Value = 0;
                    //清空信息
                    List_dd_id.Clear();
                    List_dd_create_time.Clear();
                    List_dd_currency_symbol.Clear();
                    List_dd_pay.Clear();
                    List_dd_state.Clear();
                    List_dd_shop_name.Clear();
                    List_dd_postFees_prefix.Clear();
                    List_dd_postFees_suffix.Clear();
                    List_dd_postFees_value.Clear();
                    List_goods_name.Clear();
                    List_goods_price.Clear();
                    List_goods_quantity.Clear();
                    List_all_output.Clear();

                    if (Int_error == 0)
                    {
                        
                        MessageBox.Show("【完成】规整订单内容成功！", "恭喜发财！",MessageBoxButtons.OK,MessageBoxIcon.Information);
                    }
                    else
                    {
                        textBox_output.Text = "（请点击“开始规整”）";
                        textBox_output.Enabled = false;
                        MessageBox.Show("规整订单内容失败！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        //=======点击“清空信息”按钮=======
        private void button_clear_Click(object sender, EventArgs e)
        {
            radioButton_file.Checked = true;
            textBox_output.Enabled = false;
            textBox_input_file.Text = "【请将txt文档拖至黄色区域，仅支持读取一个txt文档】";
            textBox_input_text.Text = "（如需手动复制输入，请先点选）";
            textBox_output.Text = "（请点击“开始规整”）";
            textBox_test.Text = "";
            progressBar_deal.Value = 0;
        }

        //=======主界面加载=======
        private void Main_Load(object sender, EventArgs e)
        {
            radioButton_text.Checked = true;
        }
    }
}
