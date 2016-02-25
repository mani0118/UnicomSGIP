using System;

namespace UnicomSGIP.Main.Model
{
    /// <summary>
    /// 短信提交类,可以配置在Web.config中
    /// </summary>
    [Serializable]
    public class SubmitRequest : SubmitBase
    {
        /// <summary>
        /// 初始化
        /// </summary>
        public SubmitRequest() { }
        /// <summary>
        /// SP接入号码(21字节)
        /// </summary>
        public string SpNumber { get; set; }
        /// <summary>
        /// 付费号码,手机号码前加"86"国别标志,当且仅当群发且对用户收费是为空;
        /// 如果为空，则该条短信消息产生的费用由UserNumber代表的用户支付;
        /// 如果为全字符串"000000000000000000000",表示该条消息产生的费用由SP支付 (21字节)
        /// </summary>
        public string ChargeNumber { get; set; }
        /// <summary>
        /// 接收该短消息的手机号，该字段重复UserCount指定的次数，手机号加86国别标志(21字节)
        /// </summary>
        public string[] UserNumber { get; set; }
        /// <summary>
        ///  企业代码(5字节)
        /// </summary>
        public string CorpId { get; set; }
        /// <summary>
        /// 业务代码(10字节)
        /// </summary>
        public string ServiceType { get; set; }
        /// <summary>
        /// 短消息的编码格式
        /// 0:纯ASCII字符串
        /// 3:写卡操作
        /// 4:二进制编码
        /// 8:UCS2编码
        /// 15:GBK编码
        /// </summary>
        public byte MessageCoding { get; set; }
        /// <summary>
        /// 短消息的长度
        /// </summary>
        public int MessageLength { get; set; }
        /// <summary>
        /// 短消息的内容
        /// </summary>
        public string MessageContent { get; set; }
        /// <summary>
        /// 源节点
        /// </summary>
        public string SourceNode { get; set; }
        /// <summary>
        /// LinkID
        /// </summary>
        public string LinkID { get; set; }
    }
}
