using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using UnicomSGIP.Base;

namespace UnicomSGIP.Main
{
    public class UnicomSms
    {
        public UnicomSms() { }


        private Socket _Socket;
        private string _Result;


        public string SendSms(Model.SubmitRequest argModel)
        {
            List<Model.SubmitRequest> vList = new List<Model.SubmitRequest>() { argModel };
            return SendSms(vList);
        }


        public string SendSms(List<Model.SubmitRequest> argMessageList)
        {
            _Result = string.Empty;
            try
            {
                ConnectSocket();
                DoSendMsg(argMessageList);
                UnBind();
            }
            catch (System.Exception ex)
            {
                _Result = ex.Message;
            }
            finally
            {
                _Socket.Close();
            }

            return _Result;
        }




        private void ConnectSocket()
        {
            int vConnectNum = 0;
            while (true)
            {
                IPAddress vIp = IPAddress.Parse(Config.Ip);
                IPEndPoint vIpEndPoint = new IPEndPoint(vIp, Config.Port);
                _Socket = new Socket(vIpEndPoint.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
                try
                {
                    _Socket.Connect(vIpEndPoint);
                    if (_Socket.Connected)
                        break;
                    else
                        vConnectNum++;
                }
                catch
                {
                    try
                    {
                        Thread.Sleep(1000 * 1);
                    }
                    catch (System.Exception ex)
                    {
                        throw ex;
                    }
                    vConnectNum++;
                }
                if (vConnectNum >= 3)
                    throw new System.Exception("连接失败!");
            }
            _Socket.NoDelay = true;
            Bind vBindBll = new Bind(Config.LoginName, Config.Password);
            vBindBll.FillBodyBytes();
            vBindBll.Write(_Socket,Config.SourceNode);

            BindResp vRes = (BindResp)vBindBll.Read(_Socket);
            if (vRes.Result != 0)
                throw new System.Exception("SMG拒绝连接!错误码：State Code:" + vRes.Result);
        }
        private void DoSendMsg(List<Model.SubmitRequest> argSendSmss)
        {
            Submit vSubmitBll;
            SubmitResp vSubmitResp;
            foreach (Model.SubmitRequest item in argSendSmss)
            {
                vSubmitBll = new Submit(item);
                vSubmitBll.FillBodyBytes();
                vSubmitBll.Write(_Socket, Config.SourceNode);
                vSubmitResp = (SubmitResp)vSubmitBll.Read(_Socket);
                if (vSubmitResp.Result != 0)
                    _Result = vSubmitResp.Result.ToString();
            }

        }
        private void UnBind()
        {
            UnBind vUnBindBll = new UnBind();
            vUnBindBll.Write(_Socket, Config.SourceNode);
            UnBindResp vResp = (UnBindResp)vUnBindBll.Read(_Socket);
            if (vResp._Header.CommandId.Equals(SGIPCommandDefine.SGIP_UNBIND_RESP))
            {
                try
                {
                    if (_Socket != null)
                        _Socket.Close();
                }
                catch (IOException e)
                {
                    throw e;
                }
            }
        }
    }
}
