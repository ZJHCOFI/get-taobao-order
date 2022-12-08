using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
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
        // 更新时间：2022.11.19 22:33
        // Edit by ZJHCOFI
        // 博客Blog：http://zjhcofi.com
        // Github：http://github.com/zjhcofi
        // 功能：规整淘宝中的买家订单
        // 开源协议：BSD 3-Clause “New” or “Revised” License (https://choosealicense.com/licenses/bsd-3-clause/)
        // 后续更新或漏洞修补通告页面：https://space.bilibili.com/9704701/dynamic

        //文本处理委托-First_deal
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
                int error = 0;
                //定义输出内容
                List<string> dd_id = new List<string>();  //订单号
                List<string> dd_create_time = new List<string>();  //订单创建时间
                List<string> dd_currency_symbol = new List<string>();  //订单货币符号
                List<string> dd_pay = new List<string>();  //订单实付金额
                List<string> dd_state = new List<string>();  //订单状态
                List<string> dd_shop_name = new List<string>();  //订单店铺名称
                List<string> dd_postFees_prefix = new List<string>();  //订单补充项目前缀
                List<string> dd_postFees_suffix = new List<string>();  //订单补充项目后缀
                List<string> dd_postFees_value = new List<string>();  //订单补充项目数值
                List<string> goods_name = new List<string>();  //商品名
                List<string> goods_price = new List<string>();  //商品单价
                List<string> goods_quantity = new List<string>();  //商品数量
                List<string> goods_tpye_prefix = new List<string>();  //商品分类前缀
                List<string> goods_tpye = new List<string>();  //商品分类
                List<string> All_output = new List<string>();  //总输出

                try
                {
                    //鼠标等待
                    this.Cursor = Cursors.WaitCursor;

                    //逐行读取源内容并存进数组中
                    string[] Source_text = new string[textBox_input_text.Lines.Length];
                    for (int i = 0; i < textBox_input_text.Lines.Length; i++)
                    {
                        Source_text[i] = textBox_input_text.Lines[i];
                    }
                    //网页源代码预先处理--步骤1：取出“notShowSellerInfo”字符所在的数组到新数组
                    string[] Deal_text_1 = Array.FindAll(Source_text, FindCallback_text_deal_1);
                    progressBar_deal.Value = 5;
                    //网页源代码预先处理--步骤2：记录步骤1新数组中的内容
                    string Deal_text_2 = "";
                    foreach (string i in Deal_text_1)
                    {
                        //步骤2：
                        Deal_text_2 += i;
                    }
                    //测试输出
                    textBox_test.Text = Deal_text_2;
                    progressBar_deal.Value = 15;
                    //网页源代码预先处理--步骤3：将步骤2中的反斜杠“\”、中竖线“|”和换行符清除
                    string Deal_text_3 = (((Deal_text_2.Replace("\r\n", "")).Replace("\r", "")).Replace("\n", "")).Replace("|", "");
                    Deal_text_3 = ((Deal_text_3.Replace("\\u", "zjhcofi")).Replace("\\", "")).Replace("zjhcofi", "\\u");
                    //测试输出
                    textBox_test.Text = Deal_text_3;
                    //网页源代码预先处理--步骤4：将步骤3中的Unicode转为中文
                    string Deal_text_4 = Regex.Unescape(Deal_text_3);
                    //测试输出
                    textBox_test.Text = Deal_text_4;
                    progressBar_deal.Value = 20;

                    //使用 batchGroupTips 拆分主订单--步骤5：
                    string[] Deal_text_5 = Regex.Split(Deal_text_4, "batchGroupTips");
                    Deal_text_5 = Deal_text_5.Skip(1).ToArray();
                    progressBar_deal.Value = 30;

                    //使用 skuText 拆分子订单--步骤6：
                    int ProgressBar_deal_add = Convert.ToInt32(70/Deal_text_5.Length);
                    foreach (string i in Deal_text_5)
                    {
                        string[] Deal_text_6 = Regex.Split(i, "skuText");
                        //子订单信息获取--步骤7：
                        int a = 1;
                        foreach (string b in Deal_text_6)
                        {
                            //测试输出
                            textBox_test.Text = b;
                            if (a == 1)
                            {
                                //订单号截取
                                string[] dd_id_split_1 = Regex.Split(b, ",\"inHold\":");
                                string[] dd_id_split_2 = Regex.Split(dd_id_split_1[0], "\"id\":");
                                string[] dd_id_split_3 = Regex.Split(dd_id_split_2[1], ",");
                                dd_id.Add(dd_id_split_3[0]);

                                //订单创建时间截取
                                //string[] dd_create_time_split = b.Split(new string[] { "createTime\":\"", "\"" }, StringSplitOptions.RemoveEmptyEntries);
                                string[] dd_create_time_split_1 = Regex.Split(b, "createTime\":\"");
                                string[] dd_create_time_split_2 = Regex.Split(dd_create_time_split_1[1], "\"");
                                dd_create_time.Add(dd_create_time_split_2[0]);

                                //订单货币符号截取
                                //string[] dd_currency_symbol_split = b.Split(new string[] { "\"currencySymbol\":\"", "\"" }, StringSplitOptions.RemoveEmptyEntries);
                                string[] dd_currency_symbol_split_1 = Regex.Split(b, "\"currencySymbol\":\"");
                                string[] dd_currency_symbol_split_2 = Regex.Split(dd_currency_symbol_split_1[1], "\"");
                                dd_currency_symbol.Add(dd_currency_symbol_split_2[0]);

                                //订单实付金额截取
                                //string[] dd_pay_split = b.Split(new string[] { "actualFee\":\"", "\"" }, StringSplitOptions.RemoveEmptyEntries);
                                string[] dd_pay_split_1 = Regex.Split(b, "actualFee\":\"");
                                string[] dd_pay_split_2 = Regex.Split(dd_pay_split_1[1], "\"");
                                dd_pay.Add(dd_pay_split_2[0]);

                                //订单状态截取
                                //string[] dd_state_split = b.Split(new string[] { "\" }],\"text\":\"", "\"" }, StringSplitOptions.RemoveEmptyEntries);
                                string[] dd_state_split_1 = Regex.Split(b, "],\"text\":\"");
                                string[] dd_state_split_2 = Regex.Split(dd_state_split_1[1], "\"");
                                dd_state.Add(dd_state_split_2[0]);

                                //订单店铺名称截取
                                //string[] dd_shop_name_split = b.Split(new string[] { "\"shopName\":\"", "\"" }, StringSplitOptions.RemoveEmptyEntries);
                                string[] dd_shop_name_split_1 = Regex.Split(b, "\"shopName\":\"");
                                if(dd_shop_name_split_1.Length < 2)
                                {
                                    dd_shop_name.Add("");
                                }
                                else
                                {
                                    string[] dd_shop_name_split_2 = Regex.Split(dd_shop_name_split_1[1], "\"");
                                    dd_shop_name.Add(dd_shop_name_split_2[0]);
                                }

                                //订单补充项目前缀截取
                                //string[] dd_postFees_prefix_split = b.Split(new string[] { "{\"prefix\":\"", "\"" }, StringSplitOptions.RemoveEmptyEntries);
                                string[] dd_postFees_prefix_split_1 = Regex.Split(b, "{\"prefix\":\"");
                                if(dd_postFees_prefix_split_1.Length < 2)
                                {
                                    dd_postFees_prefix.Add("");
                                }
                                else
                                {
                                    string[] dd_postFees_prefix_split_2 = Regex.Split(dd_postFees_prefix_split_1[1], "\"");
                                    dd_postFees_prefix.Add(dd_postFees_prefix_split_2[0]);
                                }

                                //订单补充项目后缀截取
                                //string[] dd_postFees_suffix_split = b.Split(new string[] { "suffix\":\"", "\"" }, StringSplitOptions.RemoveEmptyEntries);
                                string[] dd_postFees_suffix_split_1 = Regex.Split(b, "suffix\":\"");
                                if(dd_postFees_suffix_split_1.Length < 2)
                                {
                                    dd_postFees_suffix.Add("");
                                }
                                else
                                {
                                    string[] dd_postFees_suffix_split_2 = Regex.Split(dd_postFees_suffix_split_1[1], "\"");
                                    dd_postFees_suffix.Add(dd_postFees_suffix_split_2[0]);
                                }

                                //订单补充项目数值截取
                                //string[] dd_postFees_value_split = b.Split(new string[] { ",\"value\":\"", "\"" }, StringSplitOptions.RemoveEmptyEntries);
                                string[] dd_postFees_value_split_1 = Regex.Split(b, ",\"value\":\"");
                                if(dd_postFees_value_split_1.Length < 2)
                                {
                                    dd_postFees_value.Add("");
                                }
                                else
                                {
                                    string[] dd_postFees_value_split_2 = Regex.Split(dd_postFees_value_split_1[1], "\"");
                                    dd_postFees_value.Add(dd_postFees_value_split_2[0].Replace(dd_currency_symbol_split_2[0], ""));
                                }

                                a += 1;
                            }
                            else if (a == 2)
                            {
                                //商品名截取
                                //string[] goods_name_split = b.Split(new string[] { "\"title\":\"", "\"" }, StringSplitOptions.RemoveEmptyEntries);
                                string[] goods_name_split_1 = Regex.Split(b, "\"title\":\"");
                                string[] goods_name_split_2 = Regex.Split(goods_name_split_1[1], "\"");
                                goods_name.Add(goods_name_split_2[0]);

                                //商品单价截取
                                //string[] goods_price_split = b.Split(new string[] { "\"realTotal\":\"", "\"" }, StringSplitOptions.RemoveEmptyEntries);
                                string[] goods_price_split_1 = Regex.Split(b, "\"realTotal\":\"");
                                string[] goods_price_split_2 = Regex.Split(goods_price_split_1[1], "\"");
                                goods_price.Add(goods_price_split_2[0]);

                                //商品数量截取
                                //string[] goods_quantity_split = b.Split(new string[] { "\"quantity\":\"", "\"" }, StringSplitOptions.RemoveEmptyEntries);
                                string[] goods_quantity_split_1 = Regex.Split(b, "\"quantity\":\"");
                                string[] goods_quantity_split_2 = Regex.Split(goods_quantity_split_1[1], "\"");
                                goods_quantity.Add(goods_quantity_split_2[0]);

                                //商品分类前缀截取
                                //string[] goods_tpye_prefix_split = b.Split(new string[] { "\"name\":\"", "\"" }, StringSplitOptions.RemoveEmptyEntries);
                                string[] goods_tpye_prefix_split_1 = Regex.Split(b, "\"name\":\"");
                                if (goods_tpye_prefix_split_1.Length < 2)
                                {
                                    goods_tpye_prefix.Add("(无)");
                                }
                                else
                                {
                                    string[] goods_tpye_prefix_split_2 = Regex.Split(goods_tpye_prefix_split_1[1], "\"");
                                    goods_tpye_prefix.Add(goods_tpye_prefix_split_2[0]);
                                }

                                //商品分类截取
                                //string[] goods_tpye_split = b.Split(new string[] { "\"value\":\"", "\"" }, StringSplitOptions.RemoveEmptyEntries);
                                string[] goods_tpye_split_1 = Regex.Split(b, "\"value\":\"");
                                if (goods_tpye_split_1.Length < 2)
                                {
                                    goods_tpye.Add("(无)");
                                }
                                else
                                {
                                    string[] goods_tpye_split_2 = Regex.Split(goods_tpye_split_1[1], "\"");
                                    goods_tpye.Add(goods_tpye_split_2[0]);
                                }
                                
                                All_output.Add(dd_create_time.Last() + "|" + dd_state.Last() + "|" + goods_name.Last() + "|" + goods_tpye_prefix.Last() + "|" + goods_tpye.Last()
                                    + "|" + dd_currency_symbol.Last() + "|" + goods_price.Last() + "|" + goods_quantity.Last() + "|" + dd_postFees_prefix.Last() +
                                    dd_postFees_suffix.Last() + "|" + dd_postFees_value.Last() + "|" + dd_pay.Last() + "|" + dd_shop_name.Last() + "|" + dd_id.Last());
                                //测试输出
                                textBox_test.Text = dd_create_time.Last() + "|" + dd_state.Last() + "|" + goods_name.Last() + "|" + goods_tpye_prefix.Last() + "|" + goods_tpye.Last()
                                    + "|" + dd_currency_symbol.Last() + "|" + goods_price.Last() + "|" + goods_quantity.Last() + "|" + dd_postFees_prefix.Last() +
                                    dd_postFees_suffix.Last() + "|" + dd_postFees_value.Last() + "|" + dd_pay.Last() + "|" + dd_shop_name.Last() + "|" + dd_id.Last();

                                a += 1;
                            }
                            else 
                            {
                                //商品名截取
                                //string[] goods_name_split = b.Split(new string[] { "\"title\":\"", "\"" }, StringSplitOptions.RemoveEmptyEntries);
                                string[] goods_name_split_1 = Regex.Split(b, "\"title\":\"");
                                string[] goods_name_split_2 = Regex.Split(goods_name_split_1[1], "\"");
                                goods_name.Add(goods_name_split_2[0]);

                                //商品单价截取
                                //string[] goods_price_split = b.Split(new string[] { "\"realTotal\":\"", "\"" }, StringSplitOptions.RemoveEmptyEntries);
                                string[] goods_price_split_1 = Regex.Split(b, "\"realTotal\":\"");
                                string[] goods_price_split_2 = Regex.Split(goods_price_split_1[1], "\"");
                                goods_price.Add(goods_price_split_2[0]);

                                //商品数量截取
                                //string[] goods_quantity_split = b.Split(new string[] { "\"quantity\":\"", "\"" }, StringSplitOptions.RemoveEmptyEntries);
                                string[] goods_quantity_split_1 = Regex.Split(b, "\"quantity\":\"");
                                string[] goods_quantity_split_2 = Regex.Split(goods_quantity_split_1[1], "\"");
                                goods_quantity.Add(goods_quantity_split_2[0]);

                                //商品分类前缀截取
                                //string[] goods_tpye_prefix_split = b.Split(new string[] { "\"name\":\"", "\"" }, StringSplitOptions.RemoveEmptyEntries);
                                string[] goods_tpye_prefix_split_1 = Regex.Split(b, "\"name\":\"");
                                if (goods_tpye_prefix_split_1.Length < 2)
                                {
                                    goods_tpye_prefix.Add("(无)");
                                }
                                else
                                {
                                    string[] goods_tpye_prefix_split_2 = Regex.Split(goods_tpye_prefix_split_1[1], "\"");
                                    goods_tpye_prefix.Add(goods_tpye_prefix_split_2[0]);
                                }

                                //商品分类截取
                                //string[] goods_tpye_split = b.Split(new string[] { "\"value\":\"", "\"" }, StringSplitOptions.RemoveEmptyEntries);
                                string[] goods_tpye_split_1 = Regex.Split(b, "\"value\":\"");
                                if (goods_tpye_split_1.Length < 2)
                                {
                                    goods_tpye.Add("(无)");
                                }
                                else
                                {
                                    string[] goods_tpye_split_2 = Regex.Split(goods_tpye_split_1[1], "\"");
                                    goods_tpye.Add(goods_tpye_split_2[0]);
                                }

                                All_output.Add(dd_create_time.Last() + "|" + dd_state.Last() + "|" + goods_name.Last() + "|" + goods_tpye_prefix.Last() + "|" + goods_tpye.Last()
                                    + "|" + dd_currency_symbol.Last() + "|" + goods_price.Last() + "|" + goods_quantity.Last() + "|" + "|" + "|" + "|" + dd_shop_name.Last() + "|" + dd_id.Last());
                                //测试输出
                                textBox_test.Text = dd_create_time.Last() + "|" + dd_state.Last() + "|" + goods_name.Last() + "|" + goods_tpye_prefix.Last() + "|" + goods_tpye.Last()
                                    + "|" + dd_currency_symbol.Last() + "|" + goods_price.Last() + "|" + goods_quantity.Last() + "|" + "|" + "|" + "|" + dd_shop_name.Last() + "|" + dd_id.Last();

                                a += 1;
                            }
                        }
                        //清空商品信息
                        dd_id.Clear();
                        dd_create_time.Clear();
                        dd_currency_symbol.Clear();
                        dd_pay.Clear();
                        dd_state.Clear();
                        dd_shop_name.Clear();
                        dd_postFees_prefix.Clear();
                        dd_postFees_suffix.Clear();
                        dd_postFees_value.Clear();
                        goods_name.Clear();
                        goods_price.Clear();
                        goods_quantity.Clear();
                        goods_tpye_prefix.Clear();
                        goods_tpye.Clear();
                        //进度条
                        if (progressBar_deal.Value < 100)
                        {
                            progressBar_deal.Value += ProgressBar_deal_add;
                        }
                    }

                    //输出结果
                    textBox_output.Text = "下单时间|订单状态|商品名|分类前缀|分类|货币类型|单价|数量|补充项目|补充项目数值|实付金额|店铺名|订单号" + Environment.NewLine;
                    for (int i = 0; i < All_output.Count; i++)
                    {
                        textBox_output.Text += All_output[i] + Environment.NewLine;
                    }
                    textBox_output.Enabled = true;

                }
                catch (Exception ex)
                {
                    error += 1;
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
                    dd_id.Clear();
                    dd_create_time.Clear();
                    dd_currency_symbol.Clear();
                    dd_pay.Clear();
                    dd_state.Clear();
                    dd_shop_name.Clear();
                    dd_postFees_prefix.Clear();
                    dd_postFees_suffix.Clear();
                    dd_postFees_value.Clear();
                    goods_name.Clear();
                    goods_price.Clear();
                    goods_quantity.Clear();
                    goods_tpye_prefix.Clear();
                    goods_tpye.Clear();
                    All_output.Clear();

                    if (error == 0)
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

        //点击“清空信息”按钮
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
    }
}
