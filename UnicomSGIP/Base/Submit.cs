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
            if (_SubmitRequestModel.MessageCoding == 15)
            {
                vTempChn = Encoding.GetEncoding("GBK").GetBytes(_SubmitRequestModel.MessageContent);
                _SubmitRequestModel.MessageLength = vTempChn.Length;
            }
            else
            {
                _SubmitRequestModel.MessageLength = _SubmitRequestModel.MessageContent.Length;
            }
            _Header = new SGIPHeader((uint)SGIPCommandDefine.SGIP_SUBMIT);
            _Header.TotalMsgLen = 143 + 21 * _SubmitRequestModel.UserCount + _SubmitRequestModel.MessageLength;
            _BodyBytes = new byte[_Header.TotalMsgLen - (int)SGIPCommandDefine.LEN_SGIP_HEADER];

            #region SpNumber
            byte[] vTempBytes = new byte[21];
            if (_SubmitRequestModel.SpNumber.Length > 21)
                UnicomSgipException.LengthExceededError("SpNumber", _SubmitRequestModel.SpNumber, 21);
            Encoding.ASCII.GetBytes(_SubmitRequestModel.SpNumber, 0, _SubmitRequestModel.SpNumber.Length, vTempBytes, 0);
            BytesCopy(vTempBytes, _BodyBytes, 0, 20, 0);
            #endregion
            #region ChargeNumber
            vTempBytes = new byte[21];
            if (_SubmitRequestModel.ChargeNumber.Length > 21)
                UnicomSgipException.LengthExceededError("ChargeNumber", _SubmitRequestModel.ChargeNumber, 21);
            Encoding.ASCII.GetBytes(_SubmitRequestModel.ChargeNumber, 0, _SubmitRequestModel.ChargeNumber.Length, vTempBytes, 0);
            BytesCopy(vTempBytes, _BodyBytes, 0, 20, 21);
            #endregion
            #region UserCount
            _BodyBytes[42] = _SubmitRequestModel.UserCount;
            #endregion
            #region UserNumbers
            int vCurrentPos = 43;
            for (int i = 0; i < _SubmitRequestModel.UserCount; i++)
            {
                vTempBytes = new byte[21];
                string vMobile = _SubmitRequestModel.UserNumber[i];
                if (vMobile.Length > 21)
                    UnicomSgipException.LengthExceededError("UserNumber", vMobile, 21);
                Encoding.ASCII.GetBytes(vMobile, 0, vMobile.Length, vTempBytes, 0);
                BytesCopy(vTempBytes, _BodyBytes, 0, 20, vCurrentPos);
                vCurrentPos += 21;
            }
            #endregion
            #region CorpId
            vTempBytes = new byte[5];
            if (_SubmitRequestModel.CorpId.Length > 5)
                UnicomSgipException.LengthExceededError("CorpId", _SubmitRequestModel.CorpId, 5);
            Encoding.ASCII.GetBytes(_SubmitRequestModel.CorpId, 0, _SubmitRequestModel.CorpId.Length, vTempBytes, 0);
            BytesCopy(vTempBytes, _BodyBytes, 0, 4, vCurrentPos);
            vCurrentPos += 5;
            #endregion
            # region ServiceType
            vTempBytes = new byte[10];
            if (_SubmitRequestModel.ServiceType.Length > 10)
                UnicomSgipException.LengthExceededError("ServiceType", _SubmitRequestModel.ServiceType, 10);
            Encoding.ASCII.GetBytes(_SubmitRequestModel.ServiceType, 0, _SubmitRequestModel.ServiceType.Length, vTempBytes, 0);
            BytesCopy(vTempBytes, _BodyBytes, 0, 9, vCurrentPos);
            vCurrentPos += 10;
            #endregion
            #region FeeType
            _BodyBytes[vCurrentPos] = _SubmitRequestModel.FeeType;
            vCurrentPos++;
            #endregion
            #region FeeValue
            vTempBytes = new byte[6];
            if (_SubmitRequestModel.FeeValue.Length > 6)
                UnicomSgipException.LengthExceededError("FeeValue", _SubmitRequestModel.FeeValue, 6);
            Encoding.ASCII.GetBytes(_SubmitRequestModel.FeeValue, 0, _SubmitRequestModel.FeeValue.Length, vTempBytes, 0);
            BytesCopy(vTempBytes, _BodyBytes, 0, 5, vCurrentPos);
            vCurrentPos += 6;
            #endregion
            #region GivenValue
            vTempBytes = new byte[6];
            if (_SubmitRequestModel.GivenValue.Length > 6)
                UnicomSgipException.LengthExceededError("GivenValue", _SubmitRequestModel.GivenValue, 6);
            Encoding.ASCII.GetBytes(_SubmitRequestModel.GivenValue, 0, _SubmitRequestModel.GivenValue.Length, vTempBytes, 0);
            BytesCopy(vTempBytes, _BodyBytes, 0, 5, vCurrentPos);
            vCurrentPos += 6;
            #endregion
            #region AgentFlag
            _BodyBytes[vCurrentPos] = _SubmitRequestModel.AgentFlag;
            vCurrentPos++;
            #endregion
            #region MorelatetoMTFlag
            _BodyBytes[vCurrentPos] = _SubmitRequestModel.MorelatetoMTFlag;
            vCurrentPos++;
            #endregion
            #region Priority
            _BodyBytes[vCurrentPos] = _SubmitRequestModel.Priority;
            vCurrentPos++;
            #endregion
            #region ExpireTime
            vTempBytes = new byte[16];
            if (_SubmitRequestModel.ExpireTime.Length > 16)
                UnicomSgipException.LengthExceededError("ExpireTime", _SubmitRequestModel.ExpireTime, 16);
            Encoding.ASCII.GetBytes(_SubmitRequestModel.ExpireTime, 0, _SubmitRequestModel.ExpireTime.Length, vTempBytes, 0);
            BytesCopy(vTempBytes, _BodyBytes, 0, 15, vCurrentPos);
            vCurrentPos += 16;
            #endregion
            #region ScheduleTime
            vTempBytes = new byte[16];
            if (_SubmitRequestModel.ScheduleTime.Length > 16)
                UnicomSgipException.LengthExceededError("ScheduleTime", _SubmitRequestModel.ScheduleTime, 16);
            Encoding.ASCII.GetBytes(_SubmitRequestModel.ScheduleTime, 0, _SubmitRequestModel.ScheduleTime.Length, vTempBytes, 0);
            BytesCopy(vTempBytes, _BodyBytes, 0, 15, vCurrentPos);
            vCurrentPos += 16;
            #endregion
            #region  ReportFlag
            _BodyBytes[vCurrentPos] = _SubmitRequestModel.ReportFlag;
            vCurrentPos++;
            #endregion
            #region TP_pid
            _BodyBytes[vCurrentPos] = _SubmitRequestModel.TP_pid;
            vCurrentPos++;
            #endregion
            #region TP_udhi
            _BodyBytes[vCurrentPos] = _SubmitRequestModel.TP_udhi;
            vCurrentPos++;
            #endregion
            #region MessageCoding
            _BodyBytes[vCurrentPos] = _SubmitRequestModel.MessageCoding;
            vCurrentPos++;
            #endregion
            #region MessageType
            _BodyBytes[vCurrentPos] = _SubmitRequestModel.MessageType;
            vCurrentPos++;
            #endregion
            #region MessageLength
            BytesCopy(IntToBytesReverse(_SubmitRequestModel.MessageLength), _BodyBytes, 0, 3, vCurrentPos);
            vCurrentPos += 4;
            #endregion
            #region MessageContent
            if (_SubmitRequestModel.MessageCoding == 15)
                BytesCopy(vTempChn, _BodyBytes, 0, _SubmitRequestModel.MessageLength - 1, vCurrentPos);
            else
                Encoding.ASCII.GetBytes(_SubmitRequestModel.MessageContent, 0, _SubmitRequestModel.MessageContent.Length, _BodyBytes, 0);
            vCurrentPos += _SubmitRequestModel.MessageLength;
            #endregion
            #region LinkID
            vTempBytes = new byte[8];
            if (_SubmitRequestModel.LinkID.Length > 8)
                UnicomSgipException.LengthExceededError("LinkID", _SubmitRequestModel.LinkID, 8);
            Encoding.ASCII.GetBytes(_SubmitRequestModel.LinkID, 0, _SubmitRequestModel.LinkID.Length, vTempBytes, 0);
            BytesCopy(vTempBytes, _BodyBytes, 0, 7, vCurrentPos);
            #endregion

        }


        #endregion

    }
}
