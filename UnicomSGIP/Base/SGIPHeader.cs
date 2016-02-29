using System;
using System.Net.Sockets;

namespace UnicomSGIP.Base
{
    /// <summary>
    /// SGIPHeader 类
    /// </summary>
    public class SGIPHeader
    {
        private int _SN = 0; // 序列号，循环进位，满了之后再从0开始计位

        /// <summary>
        /// 初始化
        /// </summary>
        public SGIPHeader() { }

        /// <summary>
        /// 初始化Head字节长度
        /// </summary>
        /// <param name="argCommandId"></param>
        public SGIPHeader(uint argCommandId)
        {
            CommandId = argCommandId;
            const int HEAD_LEN = (int)SGIPCommandDefine.LEN_SGIP_HEADER;
            switch (argCommandId)
            {
                case 0x1:
                    TotalMsgLen = HEAD_LEN + (int)SGIPCommandDefine.LEN_SGIP_BIND;
                    break;
                case 0x80000001:
                    TotalMsgLen = HEAD_LEN + (int)SGIPCommandDefine.LEN_SGIP_BIND_RESP;
                    break;
                case 0x2:
                    TotalMsgLen = HEAD_LEN + (int)SGIPCommandDefine.LEN_SGIP_UNBIND;
                    break;
                case 0x80000002:
                    TotalMsgLen = HEAD_LEN + (int)SGIPCommandDefine.LEN_SGIP_UNBIND_RESP;
                    break;
                case 0x3:
                    TotalMsgLen = HEAD_LEN + (int)SGIPCommandDefine.LEN_SGIP_SUBMIT;
                    break;
                case 0x80000003:
                    TotalMsgLen = HEAD_LEN + (int)SGIPCommandDefine.LEN_SGIP_SUBMIT_RESP;
                    break;

                #region 未开发功能,暂时用不到

                case 0x4:
                    TotalMsgLen = HEAD_LEN + (int)SGIPCommandDefine.LEN_SGIP_DELIVER;
                    break;
                case 0x80000004:
                    TotalMsgLen = HEAD_LEN + (int)SGIPCommandDefine.LEN_SGIP_DELIVER_RESP;
                    break;
                case 0x5:
                    TotalMsgLen = HEAD_LEN + (int)SGIPCommandDefine.LEN_SGIP_REPORT;
                    break;
                case 0x80000005:
                    TotalMsgLen = HEAD_LEN + (int)SGIPCommandDefine.LEN_SGIP_REPORT_RESP;
                    break;

                #endregion

                default:
                    TotalMsgLen = -1;
                    break;
            }
        }

        /// <summary>
        /// 命令体的总长度
        /// </summary>
        public int TotalMsgLen { get; set; }

        /// <summary>
        /// 命令码
        /// </summary>
        public uint CommandId { get; set; }

        /// <summary>
        /// 序列号
        /// </summary>
        public string SequenceNumber { get; set; }

        /// <summary>
        /// 联通序列号
        /// </summary>
        public byte[] UnicomSN { get; set; }

        /// <summary>
        /// 获取头字节
        /// </summary>
        /// <param name="argSrcNode"></param>
        /// <returns></returns>
        public byte[] GetCommandHeadBytes()
        {
            byte[] vSrcNode = SGIPCommand.LongToBytesReverse(SocketConfig.SourceNode);
            byte[] vDateCmd = SGIPCommand.IntToBytesReverse(GetDateCmd());// 命令产生日期
            byte[] vSeqNum = SGIPCommand.IntToBytesReverse(GetSeqNumber());// 序列号
            byte[] vMsgLen = SGIPCommand.IntToBytesReverse(TotalMsgLen); // 命令体总长度
            byte[] vCommandId = BitConverter.GetBytes(CommandId);
            Array.Reverse(vCommandId);
            SequenceNumber = SocketConfig.SourceNode.ToString() + SGIPCommand.Bytes4ToInt(vDateCmd) + SGIPCommand.Bytes4ToInt(vSeqNum);
            byte[] vCommandHeadbytes = new byte[(int)SGIPCommandDefine.LEN_SGIP_HEADER];
            byte[] vCmdseq = new byte[12]; // 总序列号(MessageLength + Commandid + sequenceNumber)
            Buffer.BlockCopy(vMsgLen, 0, vCommandHeadbytes, 0, 4);
            Buffer.BlockCopy(vCommandId, 0, vCommandHeadbytes, 4, 4);
            if (UnicomSN != null)
            {
                Buffer.BlockCopy(UnicomSN, 0, vCmdseq, 0, 12);
                byte[] vTempBytes = new byte[4];
                Buffer.BlockCopy(UnicomSN, 0, vTempBytes, 0, 4);
                byte[] vSrcNodeByte = new byte[5];
                Buffer.BlockCopy(vTempBytes, 0, vSrcNodeByte, 1, 4);
                uint vSrc = BitConverter.ToUInt32(vSrcNodeByte, 0);
                Buffer.BlockCopy(UnicomSN, 4, vTempBytes, 0, 4);
                string vDate = SGIPCommand.Bytes4ToInt(vTempBytes) + "";
                Buffer.BlockCopy(UnicomSN, 8, vTempBytes, 0, 4);
                string vNum = SGIPCommand.Bytes4ToInt(vTempBytes) + "";
                SequenceNumber = vSrc + vDate + vNum;
            }
            else
            {
                Buffer.BlockCopy(vSrcNode, 0, vCmdseq, 0, 4);
                Buffer.BlockCopy(vDateCmd, 0, vCmdseq, 4, 4);
                Buffer.BlockCopy(vSeqNum, 0, vCmdseq, 8, 4);

            }
            Buffer.BlockCopy(vCmdseq, 0, vCommandHeadbytes, 8, 12);
            return vCommandHeadbytes;
        }


        /// <summary>
        /// 读取头信息的总长度　命令id 和序列号
        /// </summary>
        /// <param name="argSocket"></param>
        public void ReadHead(Socket argSocket)
        {
            byte[] vTempBytes = new byte[4];
            argSocket.Receive(vTempBytes);
            TotalMsgLen = SGIPCommand.Bytes4ToInt(vTempBytes); // 长度

            byte[] vCommandIdByte = new byte[4];
            argSocket.Receive(vCommandIdByte);
            CommandId = (uint)SGIPCommand.Bytes4ToInt(vCommandIdByte); // 命令id
            UnicomSN = new byte[12];
            argSocket.Receive(vTempBytes);
            byte[] vSrcnodeByte = new byte[5];
            Buffer.BlockCopy(vTempBytes, 0, vSrcnodeByte, 1, 4);
            long vSrcNode = SGIPCommand.Bytes4ToLong(vSrcnodeByte);
            Buffer.BlockCopy(vTempBytes, 0, UnicomSN, 0, 4);
            argSocket.Receive(vTempBytes);
            string vDate = SGIPCommand.Bytes4ToInt(vTempBytes) + "";
            Buffer.BlockCopy(vTempBytes, 0, UnicomSN, 4, 4);
            argSocket.Receive(vTempBytes);
            string vSeqNum = SGIPCommand.Bytes4ToInt(vTempBytes) + "";
            SequenceNumber = vSrcNode + vDate + vSeqNum;
            Buffer.BlockCopy(vTempBytes, 0, UnicomSN, 8, 4);
        }


        /// <summary>
        /// 命令产生时间
        /// </summary>
        /// <returns></returns>
        private int GetDateCmd()
        {
            DateTime vDtNow = DateTime.Now;
            string s = vDtNow.Month.ToString().PadLeft(2, '0');
            s += vDtNow.Day.ToString().PadLeft(2, '0');
            s += vDtNow.Hour.ToString().PadLeft(2, '0');
            s += vDtNow.Minute.ToString().PadLeft(2, '0');
            s += vDtNow.Second.ToString().PadLeft(2, '0');
            return int.Parse(s);
        }

        /// <summary>
        /// 获得序列号
        /// </summary>
        /// <returns></returns>
        private int GetSeqNumber()
        {
            if (_SN == int.MaxValue)
                _SN = 0;
            return ++_SN;
        }
    }

}
