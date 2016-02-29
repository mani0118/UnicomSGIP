using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnicomSGIP.Base
{
    /// <summary>
    /// 配置读取类
    /// </summary>
    public class SocketConfig
    {
        /// <summary>
        /// 联通网关地址
        /// </summary>
        public static readonly string SocketIp = "";

        /// <summary>
        /// 端口号
        /// </summary>
        public static readonly int Port = 0;

        /// <summary>
        /// 网关登陆账号（下行）
        /// </summary>
        public static readonly string LoginName = "";

        /// <summary>
        /// 登陆密码
        /// </summary>
        public static readonly string Password = "";

        /// <summary>
        /// 付费号码,手机号码前加"86"国别标志,当且仅当群发且对用户收费是为空;
        /// 如果为空，则该条短信消息产生的费用由UserNumber代表的用户支付;
        /// 如果为全字符串"000000000000000000000",表示该条消息产生的费用由SP支付 (21字节)
        /// </summary>
        public static readonly string ChargeNumber = "";

        /// <summary>
        /// 设备节点号
        /// </summary>
        public static readonly long SourceNode = long.Parse("");

        /// <summary>
        /// SP接入号码(21字节)
        /// </summary>
        public static readonly string SpNumber = "";

        /// <summary>
        ///  企业代码(5字节)
        /// </summary>
        public static readonly string CorpId = "";

        /// <summary>
        /// 业务代码(10字节)
        /// </summary>
        public static readonly string ServiceType = "";

        /// <summary>
        /// 不知道用来干嘛的 - -! (8字节)
        /// </summary>
        public static readonly string LinkId = "";

        /// <summary>
        /// 短消息的编码格式
        /// 0:纯ASCII字符串
        /// 3:写卡操作
        /// 4:二进制编码
        /// 8:UCS2编码
        /// 15:GBK编码
        /// </summary>
        public static readonly byte MessageCoding = 15;


        #region 联通规定强制值

        /// <summary>
        /// 登陆类型
        /// </summary>
        public static readonly byte LoginType = 1;

        /// <summary>
        /// 接收短消息的手机数量，取值 范围1至100(1字节)
        /// 限制为1,其他发不成功
        /// </summary>
        public static readonly byte UserCount = 1;

        /// <summary>
        /// 计费类型(6字节)
        /// 强制为1
        /// </summary>
        public static readonly byte FeeType = 1;

        /// <summary>
        /// 该条短信的收费值，由SP定义，对于包月制收费的用户，该值的为月租费的值
        /// 强制为0
        /// </summary>
        public static readonly string FeeValue = "0";

        /// <summary>
        /// 赠送话费
        /// </summary>
        public static readonly string GivenValue = "0";

        /// <summary>
        /// 代收费标标志(0应收 1实收)
        /// </summary>
        public static readonly byte AgentFlag = 0;

        /// <summary>
        /// 引起MT 消息的原因
        /// 0-MO点播引起的第一条MT消息
        /// 1-MO点播引起的非第一条 MT消息
        /// 2-非MO点播 引起的MT消息
        /// 3-系统反馈引起的MT消息
        /// </summary>
        public static readonly byte MorelatetoMTFlag = 2;

        /// <summary>
        /// 优先级(0-9),从低到高，默认为0
        /// </summary>
        public static readonly byte Priority = 0;

        /// <summary>
        /// 短消息寿命终止时间 ，格式为"yymmddhhmmsstnnp"
        /// </summary>
        public static readonly string ExpireTime = string.Empty;

        /// <summary>
        /// 定时发送时间,如果为空发示立即发送，格式化"yymmddhhmmsstnnp"
        /// </summary>
        public static readonly string ScheduleTime = string.Empty;

        /// <summary>
        /// 状态报告标记
        /// 0-该条消息只有最后出错时要返回状态报告
        /// 1-该条消息无论最后是否成功都要返回状态报告
        /// 2-该条消息不需要返回状态报告
        /// 3-该条消息仅携带包月计费信息，不下发给用户
        /// 其它保留，缺省为0
        /// </summary>
        public static readonly byte ReportFlag = 1;

        /// <summary>
        /// GSM协议类型
        /// </summary>
        public static readonly byte TP_pid = 0;

        /// <summary>
        /// GSM协议类型
        /// </summary>
        public static readonly byte TP_udhi = 0;

        /// <summary>
        /// 信息类型
        /// 0-短消息信息 其它 :待定
        /// </summary>
        public static readonly byte MessageType = 0;

        #endregion
    }
}
