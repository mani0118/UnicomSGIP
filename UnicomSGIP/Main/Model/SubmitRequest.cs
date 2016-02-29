using System;

namespace UnicomSGIP.Main.Model
{
    /// <summary>
    /// 短信提交类
    /// </summary>
    [Serializable]
    public class SubmitRequest
    {
        /// <summary>
        /// 初始化
        /// </summary>
        public SubmitRequest() { }

        /// <summary>
        /// 接收该短消息的手机号，手机号加86国别标志(21字节)
        /// </summary>
        public string UserNumber { get; set; }

        /// <summary>
        /// 短消息的内容
        /// </summary>
        public string MessageContent { get; set; }


    }
}
