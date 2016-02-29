using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnicomSGIP.Base
{
    /// <summary>
    /// 
    /// </summary>
    public static class Help
    {
        static Help()
        {
            ResultCode = new Dictionary<byte, string>();
            ResultCode.Add(0, "成功");
            ResultCode.Add(1, "非法登录(如登录名、口令出错、登录名与口令不符等)");
            ResultCode.Add(2, "重复登录(如在同一TCP/IP连接中连续两次以上请求登录)");
            ResultCode.Add(3, "连接过多(指单个节点要求同时建立的连接数过多)");
            ResultCode.Add(4, "登录类型错误(指bind命令中的logintype字段出错)");
            ResultCode.Add(5, "参数格式错误(指命令中参数值与参数类型不符或与协议规定的范围不符)");
            ResultCode.Add(6, "非法手机号码(协议中所有手机号码字段出现非8613号码或手机号码前未加“86”时都应报错)");
            ResultCode.Add(7, "消息ID错误");
            ResultCode.Add(8, "信息长度错误");
            ResultCode.Add(9, "非法序列号(包括序列号重复、序列号格式错误等)");
            ResultCode.Add(10, "非法操作GNS");
            ResultCode.Add(11, "节点忙(指本节点存储队列满或其他原因，暂时不能提供服务的情况)");
            ResultCode.Add(21, "目的地址不可达(指路由表存在路由且消息路由正确但被路由的节点暂时不能提供服务的情况)");
            ResultCode.Add(22, "路由错(指路由表存在路由但消息路由出错的情况，如转错SMG等)");
            ResultCode.Add(23, "路由不存在(指消息路由的节点在路由表中不存在)");
            ResultCode.Add(24, "计费号码无效");
            ResultCode.Add(25, "用户不能通信(如不在服务区、未开机等情况)");
            ResultCode.Add(26, "手机内存不足");
            ResultCode.Add(27, "手机不支持短消息");
            ResultCode.Add(28, "手机接收短消息出现错误");
            ResultCode.Add(29, "未知用户");
            ResultCode.Add(30, "不提供此功能");
            ResultCode.Add(31, "非法设备");
            ResultCode.Add(32, "系统错误");
            ResultCode.Add(33, "短信中心队列满");
            ResultCode.Add(99, "未知错误");
        }


        public static Dictionary<byte, string> ResultCode { get; set; }

    }
}
