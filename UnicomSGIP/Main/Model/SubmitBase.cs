using System;

namespace UnicomSGIP.Main.Model
{
    /// <summary>
    /// 提交信息基类
    /// 注：联通强制配置，修改请三思哈哈哈
    /// </summary>
    [Serializable]
    public class SubmitBase
    {
        /// <summary>
        /// 强制配置值
        /// </summary>
        public SubmitBase()
        {
            UserCount = 1;
            FeeType = 1;
            FeeValue = "0";
            GivenValue = "0";
            AgentFlag = 0;
            MorelatetoMTFlag = 2;
            Priority = 0;
            ExpireTime = string.Empty;
            ScheduleTime = string.Empty;
            ReportFlag = 1;
            TP_pid = 0;
            TP_udhi = 0;
            MessageType = 0;
        }

        /// <summary>
        /// 接收短消息的手机数量，取值 范围1至100(1字节)
        /// 限制为1,其他发不成功
        /// </summary>
        public byte UserCount { get; set; }
        /// <summary>
        /// 计费类型(6字节)
        /// 强制为1
        /// </summary>
        public byte FeeType { get; set; }
        /// <summary>
        /// 该条短信的收费值，由SP定义，对于包月制收费的用户，该值的为月租费的值
        /// 强制为0
        /// </summary>
        public string FeeValue { get; set; }
        /// <summary>
        /// 赠送话费
        /// </summary>
        public string GivenValue { get; set; }
        /// <summary>
        /// 代收费标标志(0应收 1实收)
        /// </summary>
        public byte AgentFlag { get; set; }
        /// <summary>
        /// 引起MT 消息的原因
        /// 0-MO点播引起的第一条MT消息
        /// 1-MO点播引起的非第一条 MT消息
        /// 2-非MO点播 引起的MT消息
        /// 3-系统反馈引起的MT消息
        /// </summary>
        public byte MorelatetoMTFlag { get; set; }
        /// <summary>
        /// 优先级(0-9),从低到高，默认为0
        /// </summary>
        public byte Priority { get; set; }
        /// <summary>
        /// 短消息寿命终止时间 ，格式为"yymmddhhmmsstnnp"
        /// </summary>
        public string ExpireTime { get; set; }
        /// <summary>
        /// 定时发送时间,如果为空发示立即发送，格式化"yymmddhhmmsstnnp"
        /// </summary>
        public string ScheduleTime { get; set; }
        /// <summary>
        /// 状态报告标记
        /// 0-该条消息只有最后出错时要返回状态报告
        /// 1-该条消息无论最后是否成功都要返回状态报告
        /// 2-该条消息不需要返回状态报告
        /// 3-该条消息仅携带包月计费信息，不下发给用户
        /// 其它保留，缺省为0
        /// </summary>
        public byte ReportFlag { get; set; }
        /// <summary>
        /// GSM协议类型
        /// </summary>
        public byte TP_pid { get; set; }
        /// <summary>
        /// GSM协议类型
        /// </summary>
        public byte TP_udhi { get; set; }
        /// <summary>
        /// 信息类型
        /// 0-短消息信息 其它 :待定
        /// </summary>
        public byte MessageType { get; set; }
    }
}
