#!/bin/bash
#编写时间：2022.11.12
#更新时间：2025.01.04 22:30
#Edit by ZJHCOFI
#博客Blog：https://zjhcofi.com
#Github：https://github.com/zjhcofi
#功能：规整淘宝的买家订单
#开源协议：BSD 3-Clause “New” or “Revised” License (https://choosealicense.com/licenses/bsd-3-clause/)
#后续更新或漏洞修补通告页面：https://github.com/zjhcofi/get-taobao-order
#=====更新日志=====
#--2022.11.19 23:57--
#第一个版本发布
#--2023.01.19 01:08--
#修改了用于分割的字符串，解决了某些使用场景下出现的订单号错误的bug
#问题提出：小布布布（B站UID：3184592）
#分析解决：cnlnn、ZJHCOFI
#--2024.07.22 14:20--
#解决了预售订单场景下，获取订单状态失败的问题
#问题提出：吃五个苹果（B站UID：1579744）
#分析解决：ZJHCOFI
#--2025.01.03 01:00--
#解决了商品含有多种属性(分类)的场景下，只输出一种属性(分类)的问题
#问题提出：狐狸喵Official（B站UID：290707837）
#分析解决：ZJHCOFI
#--2025.01.04 22:30--
#1、解决了在网页上进行订单筛选后，原始数据规整失败的问题
#2、新增了子订单状态的输出
#问题提出：csjjjj123（github.com/csjjjj123）、狐狸喵Official（B站UID：290707837）
#分析解决：ZJHCOFI
#==================

# 脚本当前路径
path_way=$(readlink -f "$(dirname "$0")")
if [ "$path_way" != "${path_way//[\' ]/}" ]; then 
  echo -e "\n=================\n【错误】\n当前的绝对路径有空格，请将脚本移至无空格的绝对路径下\n=================\n"
  exit
fi

# 【函数】网页源代码预先处理
function Web_page_source_code_handle() {
  
  # 去除在网页进行订单筛选后的原始数据中的换行符
  sed ':t;N;s/<\/font>\r\n//;b t' ${path_way}/$1 >> ${path_way}/zjhcofi_0.tmp
  sed -i ':t;N;s/<\/font>\n//;b t' ${path_way}/zjhcofi_0.tmp
  sed -i 's/<\/font>\r//g' ${path_way}/zjhcofi_0.tmp
  sed -i 's/<font color=\\"red\\">//g' ${path_way}/zjhcofi_0.tmp

  # grep出“notShowSellerInfo”所在的行到临时文件
  grep "notShowSellerInfo" ${path_way}/zjhcofi_0.tmp >> ${path_way}/zjhcofi_1.tmp

  # 将反斜杠“\”、中竖线“|”和换行符清除
  sed -i ':t;N;s/\n//;b t' ${path_way}/zjhcofi_1.tmp
  sed -i 's/\r//' ${path_way}/zjhcofi_1.tmp
  sed -i 's/\\u/zjhcofi/g' ${path_way}/zjhcofi_1.tmp
  sed -i 's/\\//g' ${path_way}/zjhcofi_1.tmp
  sed -i 's/zjhcofi/\\u/g' ${path_way}/zjhcofi_1.tmp
  sed -i 's/|//g' ${path_way}/zjhcofi_1.tmp

  # 将临时文件中的Unicode转为中文
  echo -en "$(<${path_way}/zjhcofi_1.tmp)" >> ${path_way}/zjhcofi_2.tmp
}

# 【函数】使用 batchGroupTips 拆分主订单
function Master_order_split() {
  # 定义变量
  declare -i awk_num=1
  declare -i file_num=10000
  string="zjhcofi"
  # 循环拆分主订单
  while [[ "$string" != "" ]]
  do
    awk_num_str="${awk_num}"
    # 生成主订单临时文件
    awk -v num="${awk_num_str}" -F 'batchGroupTips' '{print $num}' ${path_way}/zjhcofi_2.tmp > ${path_way}/zjhcofi_dd_${file_num}.tmp
    string=$(cat ${path_way}/zjhcofi_dd_${file_num}.tmp)
    awk_num=$(expr ${awk_num} + 1)
    file_num=$(expr ${file_num} + 1)
  done
  # 删除多余的临时文件
  ls ${path_way}/zjhcofi_dd_*tmp | head -n 1 | xargs rm -f
  ls ${path_way}/zjhcofi_dd_*tmp | tail -n 1 | xargs rm -f
}

# 【函数】使用 skuText 拆分子订单
function Sub_order_split() {
  # 循环
  ls ${path_way}/zjhcofi_dd_*tmp | while read dd_filename
  do
    # 定义变量
    declare -i awk_num=1
    declare -i file_num=10000
    string="zjhcofi"
    # 主订单编号
    dd_num=$(echo ${dd_filename} | awk -F 'zjhcofi_dd_' '{print $2}' | cut -d '.' -f 1)
    # 循环拆分子订单
    while [[ "$string" != "" ]]
    do
      awk_num_str="${awk_num}"
      # 生成子订单临时文件
      awk -v num="${awk_num_str}" -F 'skuText' '{print $num}' ${dd_filename} > ${path_way}/zjhcofi_goods_${dd_num}_${file_num}.tmp
      string=$(cat ${path_way}/zjhcofi_goods_${dd_num}_${file_num}.tmp)
      awk_num=$(expr ${awk_num} + 1)
      file_num=$(expr ${file_num} + 1)
    done
    # 删除多余的临时文件
    ls ${path_way}/zjhcofi_goods_${dd_num}_*.tmp | tail -n 1 | xargs rm -f
  done
}

# 【函数】从子订单中开始获取信息
function Order_info_get() {
  # 定义变量
  declare -i dd_num=10001

  # 获取订单信息
  ls ${path_way}/zjhcofi_dd_*tmp | while read a
  do
    #------主订单-----
    # 主订单文件名
    dd_filename=$(ls ${path_way}/zjhcofi_goods_${dd_num}*tmp | head -n 1)
    # 订单号
    #dd_id=`awk -F ',"inHold":' '{print $1}' ${dd_filename} | awk -F '"id":' '{print $2}'`
    dd_id=$(awk -F '","operations":\\[{"' '{print $1}' ${dd_filename} | awk -F '"id":"' '{print $2}')
    dd_id=$(echo ${dd_id} | awk -F ',' '{print $1}')
    # 订单创建时间
    dd_create_time=$(awk -F 'createTime":"' '{print $2}' ${dd_filename} | awk -F '"' '{print $1}')
    # 订单货币符号
    dd_currency_symbol=$(awk -F '"currencySymbol":"' '{print $2}' ${dd_filename} | awk -F '"' '{print $1}')
    # 订单实付金额
    dd_pay=$(awk -F 'actualFee":"' '{print $2}' ${dd_filename} | awk -F '"' '{print $1}')
    # 订单状态
    dd_state=""
    if [[ $(grep '"}],"text":"' ${dd_filename}) != "" ]]; then
    	dd_state=$(awk -F '"}],"text":"' '{print $2}' ${dd_filename} | awk -F '"' '{print $1}')
    elif [[ $(grep '"linkTitle":"预售"' ${dd_filename}) != "" ]]; then
    	dd_state="预售"
    else
    	dd_state="未知状态"
    fi
    # 订单店铺名称
    dd_shop_name=$(awk -F '"shopName":"' '{print $2}' ${dd_filename} | awk -F '"' '{print $1}')
    # 订单补充项目前缀
    dd_postFees_prefix=$(awk -F '{"prefix":"' '{print $2}' ${dd_filename} | awk -F '"' '{print $1}')
    # 订单补充项目后缀
    dd_postFees_suffix=$(awk -F 'suffix":"' '{print $2}' ${dd_filename} | awk -F '"' '{print $1}')
    # 订单补充项目数值
    dd_postFees_value=$(awk -F ',"value":"' '{print $2}' ${dd_filename} | awk -F '"' '{print $1}')
    dd_postFees_value=$(echo ${dd_postFees_value} | sed "s/${dd_currency_symbol}//g")
    
    #------子订单（商品）------
    ls ${path_way}/zjhcofi_goods_${dd_num}*tmp | while read goods_filename
    do
      goods_filename_bool=$(echo ${goods_filename} | awk -F "zjhcofi_goods_${dd_num}_" '{print $2}' | cut -d '.' -f 1)
      if [[ "$goods_filename_bool" != "10000" ]]; then
        # 商品名
        goods_name=$(awk -F '"title":"' '{print $2}' ${goods_filename} | awk -F '"' '{print $1}')
        # 商品单价
        goods_price=$(awk -F '"realTotal":"' '{print $2}' ${goods_filename} | awk -F '"' '{print $1}')
        # 商品数量
        goods_quantity=$(awk -F '"quantity":"' '{print $2}' ${goods_filename} | awk -F '"' '{print $1}')
        #--商品分类循环--
        goods_type_prefix=""
        goods_type=""
        goods_type_info=$(awk -F '\\[' '{print $2}' ${goods_filename} | awk -F '],' '{print $1}')
        goods_type_num=$(echo $goods_type_info | grep -o 'name' | wc -l)
        if [[ "$goods_type_num" == 0 || "$goods_type_num" == 1 ]]; then
          # 商品分类前缀
          goods_type_prefix=$(echo $goods_type_info | awk -F '"name":"' '{print $2}' | awk -F '"' '{print $1}')
          if [[ "$goods_type_prefix" == "" ]]; then
            goods_type_prefix="(无)"
          fi
          # 商品分类详情
          goods_type=$(echo $goods_type_info | awk -F '"value":"' '{print $2}' | awk -F '"' '{print $1}')
          if [[ "$goods_type" == "" ]]; then
            goods_type="(无)"
          fi
        else
          for (( a=2; a<=$goods_type_num+1; a++ ))
          do
            # 商品分类前缀
            goods_type_prefix+=$(echo $(echo $goods_type_info | awk -F '"name":"' '{print $'$a'}' | awk -F '"' '{print $1}') | sed 's/;/,/g')";"
            # 商品分类详情
            goods_type+=$(echo $(echo $goods_type_info | awk -F '"value":"' '{print $'$a'}' | awk -F '"' '{print $1}') | sed 's/;/,/g')";"
          done
        fi
        # 商品分类去除最后一个";"
        goods_type_prefix=$(echo ${goods_type_prefix/%;})
        goods_type=$(echo ${goods_type/%;})
        #--商品状态循环--
        goods_state=""
        goods_state_info=$(awk -F 'operations' '{print $2}' ${goods_filename} | awk -F '\\[' '{print $2}' | awk -F '],' '{print $1}')
        goods_state_num=$(echo $goods_state_info | grep -o 'text' | wc -l)
        if [[ "$goods_state_num" == 0 ]]; then
          # 商品状态
          goods_state="(无)"
        else
          for (( a=2; a<=$goods_state_num+1; a++ ))
          do
            # 商品状态
            goods_state+=$(echo $(echo $goods_state_info | awk -F '"text":"' '{print $'$a'}' | awk -F '"' '{print $1}') | sed 's/;/,/g')";"
          done
        fi
        # 商品分类去除最后一个";"
        goods_state=$(echo ${goods_state/%;})

        # 格式化数据并输出
        touch ${path_way}/result_taobao.txt
        bool_head=$(grep "补充项目" ${path_way}/result_taobao.txt)
        if [[ "$bool_head" == "" ]]; then
          echo -e "下单时间|主订单状态|子订单状态|商品名|分类前缀(多种分类以;分隔)|分类详情(多种分类以;分隔)|货币类型|单价|数量|补充项目|补充项目数值|实付金额|店铺名|订单号" >> ${path_way}/result_taobao.txt
        fi
        if [[ "$goods_filename_bool" -le 10001 ]]; then
          echo -e "${dd_create_time}|${dd_state}|${goods_state}|${goods_name}|${goods_type_prefix}|${goods_type}|${dd_currency_symbol}|${goods_price}|${goods_quantity}|${dd_postFees_prefix}${dd_postFees_suffix}|${dd_postFees_value}|${dd_pay}|${dd_shop_name}|${dd_id}" >> ${path_way}/result_taobao.txt
        else
          echo -e "${dd_create_time}|${dd_state}|${goods_state}|${goods_name}|${goods_type_prefix}|${goods_type}|${dd_currency_symbol}|${goods_price}|${goods_quantity}||||${dd_shop_name}|${dd_id}" >> ${path_way}/result_taobao.txt
        fi
      fi
    done
    # 循环+1
    dd_num=$(expr ${dd_num} + 1)
  done
}

# 【函数】删除临时文件并判断是否成功
function Order_info_check() {
  # 删除临时文件
  ls ${path_way}/zjhcofi*tmp | xargs rm -f
  # 判断生成是否成功
  if [ ! -f "${path_way}/result_taobao.txt" ];then
    echo -e "\n=================\n【错误】\n结果文件生成失败！\n=================\n"
  else
    bool_head=$(grep "补充项目" ${path_way}/result_taobao.txt)
    if [[ "$bool_head" != "" ]]; then
      echo -e "\n=================\n【恭喜】\n订单信息获取成功！\n请输入：\ncat result_taobao.txt\n进行查看\n=================\n"
    fi
  fi
}

# 【函数】主程序
function main() {
  Web_page_source_code_handle $1
  Master_order_split
  Sub_order_split
  Order_info_get
  Order_info_check
}

# 检测输入的内容并判断，按情况执行主程序
if [[ $1 == "" ]]; then
  echo -e "\n=================\n\n【脚本使用流程】\n\n1、在脚本的当前目录下创建一个txt文档，如：touch taobao.txt \n\n\n2、打开淘宝的“已买到的宝贝”页面，当页面是第一页时，可以直接将网页源代码复制到 taobao.txt 中\n\n如果需要获取订单页面第二页及之后页数的内容，需要打开浏览器调试(F12)，切换至“网络”功能\n\n《点击一下“⊘”清除，然后再点击页面中的页码，点击名称为“async”开头的文件，点击“响应”，复制里面的内容到 taobao.txt 》\n\n继续获取其他页码的内容，重复《》中的步骤即可\n\n【注意】请直接将网页的内容复制到 taobao.txt 中，不要在Windows系统弄个文档再传到Linux系统，会乱码！\n\n\n3、执行脚本时带上该文档的文件名，如：sh get_taobao_order.sh taobao.txt\n\n=================\n"
  exit
elif [ ! -f "${path_way}/$1" ];then
  echo -e "\n=================\n【错误】\n当前目录下无 $1 文件\n=================\n"
  exit
else
  # 执行主程序
  main $1
fi
