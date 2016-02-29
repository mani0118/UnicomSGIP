using System.Text;
using UnicomSGIP.Exception;

namespace UnicomSGIP.Base
{
    /// <summary>
    /// 短信提交类
    /// </summary>
    public class Submit : SGIPCommand
    {

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="argSubmit"></param>
        public Submit(Main.Model.SubmitRequest argSubmit)
        {
            _SubmitRequestModel = argSubmit;
        }

        #region 数据

        private Main.Model.SubmitRequest _SubmitRequestModel;

        #endregion


        #region Public

        /// <summary>
        /// 填充Body字节
        /// </summary>
        public void FillBodyBytes()
        {
            byte[] vTempChn = null;
            int vMsgLength = 0;
            if (SocketConfig.MessageCoding == 15)
            {
                vTempChn = Encoding.GetEncoding("GBK").GetBytes(_SubmitRequestModel.MessageContent);
                vMsgLength = vTempChn.Length;
            }
            else
            {
                vMsgLength = _SubmitRequestModel.MessageContent.Length;
            }
            _Header = new SGIPHeader((uint)SGIPCommandDefine.SGIP_SUBMIT);
            _Header.TotalMsgLen = 143 + 21 * SocketConfig.UserCount + vMsgLength;
            _BodyBytes = new byte[_Header.TotalMsgLen - (int)SGIPCommandDefine.LEN_SGIP_HEADER];

            #region SpNumber
            byte[] vTempBytes = new byte[21];
            if (SocketConfig.SpNumber.Length > 21)
                UnicomSgipException.LengthExceededError("SpNumber", SocketConfig.SpNumber, 21);
            Encoding.ASCII.GetBytes(SocketConfig.SpNumber, 0, SocketConfig.SpNumber.Length, vTempBytes, 0);
            BytesCopy(vTempBytes, _BodyBytes, 0, 20, 0);
            #endregion
            #region ChargeNumber
            vTempBytes = new byte[21];
            if (SocketConfig.ChargeNumber.Length > 21)
                UnicomSgipException.LengthExceededError("ChargeNumber", SocketConfig.ChargeNumber, 21);
            Encoding.ASCII.GetBytes(SocketConfig.ChargeNumber, 0, SocketConfig.ChargeNumber.Length, vTempBytes, 0);
            BytesCopy(vTempBytes, _BodyBytes, 0, 20, 21);
            #endregion
            #region UserCount
            _BodyBytes[42] = SocketConfig.UserCount;
            #endregion
            #region UserNumbers
            int vCurrentPos = 43;
            for (int i = 0; i < SocketConfig.UserCount; i++)
            {
                vTempBytes = new byte[21];
                if (_SubmitRequestModel.UserNumber.Length > 21)
                    UnicomSgipException.LengthExceededError("UserNumber", _SubmitRequestModel.UserNumber, 21);
                Encoding.ASCII.GetBytes(_SubmitRequestModel.UserNumber, 0, _SubmitRequestModel.UserNumber.Length, vTempBytes, 0);
                BytesCopy(vTempBytes, _BodyBytes, 0, 20, vCurrentPos);
                vCurrentPos += 21;
            }
            #endregion
            #region CorpId
            vTempBytes = new byte[5];
            if (SocketConfig.CorpId.Length > 5)
                UnicomSgipException.LengthExceededError("CorpId", SocketConfig.CorpId, 5);
            Encoding.ASCII.GetBytes(SocketConfig.CorpId, 0, SocketConfig.CorpId.Length, vTempBytes, 0);
            BytesCopy(vTempBytes, _BodyBytes, 0, 4, vCurrentPos);
            vCurrentPos += 5;
            #endregion
            #region ServiceType
            vTempBytes = new byte[10];
            if (SocketConfig.ServiceType.Length > 10)
                UnicomSgipException.LengthExceededError("ServiceType", SocketConfig.ServiceType, 10);
            Encoding.ASCII.GetBytes(SocketConfig.ServiceType, 0, SocketConfig.ServiceType.Length, vTempBytes, 0);
            BytesCopy(vTempBytes, _BodyBytes, 0, 9, vCurrentPos);
            vCurrentPos += 10;
            #endregion
            #region FeeType
            _BodyBytes[vCurrentPos] = SocketConfig.FeeType;
            vCurrentPos++;
            #endregion
            #region FeeValue
            vTempBytes = new byte[6];
            if (SocketConfig.FeeValue.Length > 6)
                UnicomSgipException.LengthExceededError("FeeValue", SocketConfig.FeeValue, 6);
            Encoding.ASCII.GetBytes(SocketConfig.FeeValue, 0, SocketConfig.FeeValue.Length, vTempBytes, 0);
            BytesCopy(vTempBytes, _BodyBytes, 0, 5, vCurrentPos);
            vCurrentPos += 6;
            #endregion
            #region GivenValue
            vTempBytes = new byte[6];
            if (SocketConfig.GivenValue.Length > 6)
                UnicomSgipException.LengthExceededError("GivenValue", SocketConfig.GivenValue, 6);
            Encoding.ASCII.GetBytes(SocketConfig.GivenValue, 0, SocketConfig.GivenValue.Length, vTempBytes, 0);
            BytesCopy(vTempBytes, _BodyBytes, 0, 5, vCurrentPos);
            vCurrentPos += 6;
            #endregion
            #region AgentFlag
            _BodyBytes[vCurrentPos] = SocketConfig.AgentFlag;
            vCurrentPos++;
            #endregion
            #region MorelatetoMTFlag
            _BodyBytes[vCurrentPos] = SocketConfig.MorelatetoMTFlag;
            vCurrentPos++;
            #endregion
            #region Priority
            _BodyBytes[vCurrentPos] = SocketConfig.Priority;
            vCurrentPos++;
            #endregion
            #region ExpireTime
            vTempBytes = new byte[16];
            if (SocketConfig.ExpireTime.Length > 16)
                UnicomSgipException.LengthExceededError("ExpireTime", SocketConfig.ExpireTime, 16);
            Encoding.ASCII.GetBytes(SocketConfig.ExpireTime, 0, SocketConfig.ExpireTime.Length, vTempBytes, 0);
            BytesCopy(vTempBytes, _BodyBytes, 0, 15, vCurrentPos);
            vCurrentPos += 16;
            #endregion
            #region ScheduleTime
            vTempBytes = new byte[16];
            if (SocketConfig.ScheduleTime.Length > 16)
                UnicomSgipException.LengthExceededError("ScheduleTime", SocketConfig.ScheduleTime, 16);
            Encoding.ASCII.GetBytes(SocketConfig.ScheduleTime, 0, SocketConfig.ScheduleTime.Length, vTempBytes, 0);
            BytesCopy(vTempBytes, _BodyBytes, 0, 15, vCurrentPos);
            vCurrentPos += 16;
            #endregion
            #region  ReportFlag
            _BodyBytes[vCurrentPos] = SocketConfig.ReportFlag;
            vCurrentPos++;
            #endregion
            #region TP_pid
            _BodyBytes[vCurrentPos] = SocketConfig.TP_pid;
            vCurrentPos++;
            #endregion
            #region TP_udhi
            _BodyBytes[vCurrentPos] = SocketConfig.TP_udhi;
            vCurrentPos++;
            #endregion
            #region MessageCoding
            _BodyBytes[vCurrentPos] = SocketConfig.MessageCoding;
            vCurrentPos++;
            #endregion
            #region MessageType
            _BodyBytes[vCurrentPos] = SocketConfig.MessageType;
            vCurrentPos++;
            #endregion
            #region MessageLength
            BytesCopy(IntToBytesReverse(vMsgLength), _BodyBytes, 0, 3, vCurrentPos);
            vCurrentPos += 4;
            #endregion
            #region MessageContent
            if (SocketConfig.MessageCoding == 15)
                BytesCopy(vTempChn, _BodyBytes, 0, vMsgLength - 1, vCurrentPos);
            else
                Encoding.ASCII.GetBytes(_SubmitRequestModel.MessageContent, 0, _SubmitRequestModel.MessageContent.Length, _BodyBytes, 0);
            vCurrentPos += vMsgLength;
            #endregion
            #region LinkID
            vTempBytes = new byte[8];
            if (SocketConfig.LinkId.Length > 8)
                UnicomSgipException.LengthExceededError("LinkId", SocketConfig.LinkId, 8);
            Encoding.ASCII.GetBytes(SocketConfig.LinkId, 0, SocketConfig.LinkId.Length, vTempBytes, 0);
            BytesCopy(vTempBytes, _BodyBytes, 0, 7, vCurrentPos);
            #endregion

        }


        #endregion

    }
}
