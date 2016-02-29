using System;
using System.Net.Sockets;

namespace UnicomSGIP.Base
{
    /// <summary>
    /// SGIP基类
    /// </summary>
    public class SGIPCommand
    {
        /// <summary>
        /// Head字节
        /// </summary>
        public SGIPHeader _Header = new SGIPHeader();
        /// <summary>
        /// Body字节
        /// </summary>
        public byte[] _BodyBytes = new byte[0];

        #region Public

        /// <summary>
        /// 发送绑定请求
        /// </summary>
        /// <param name="argSocket"></param>
        public void Write(Socket argSocket)
        {
            byte[] vCommandBytes = GetCommandBodybytes();

            try
            {
                argSocket.Send(vCommandBytes);
            }
            catch (System.Exception ex)
            {
                throw ex;
            }

        }

        /// <summary>
        /// 读取字节流
        /// </summary>
        /// <param name="argSocket"></param>
        /// <returns></returns>
        public SGIPCommand Read(Socket argSocket)
        {
            try
            {
                _Header.ReadHead(argSocket);
                ReadDataIntoBody(argSocket);
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
            switch (_Header.CommandId)
            {
                case 0x1: 
                    return new Bind(this);// 绑定命令
                case 0x80000001: 
                    return new BindResp(this);// 绑定响应
                case 0x2: 
                    return new UnBind(this);// 注销绑定命令
                case 0x80000002: 
                    return new UnBindResp(this);// 注销绑定响应
                case 0x80000003:
                    return new SubmitResp(this); // Submit响应
            }
            return null;
        }

        /// <summary>
        /// 将int转换为byte数组并反转
        /// </summary>
        /// <param name="argInt"></param>
        /// <returns></returns>
        public static byte[] IntToBytesReverse(int argInt)
        {
            byte[] vResultBytes = new byte[4];
            vResultBytes[3] = (byte)(0xFF & argInt);
            vResultBytes[2] = (byte)((0xFF00 & argInt) >> 8);
            vResultBytes[1] = (byte)((0xFF0000 & argInt) >> 16);
            vResultBytes[0] = (byte)((0xFF000000 & argInt) >> 24);
            return vResultBytes;
        }
        /// <summary>
        /// 讲long转换为byte数组并反转
        /// </summary>
        /// <param name="argLong"></param>
        /// <returns></returns>
        public static byte[] LongToBytesReverse(long argLong)
        {
            byte[] vResultBytes = new byte[4];
            vResultBytes[3] = (byte)(int)(0xFF & argLong);
            vResultBytes[2] = (byte)(int)((0xFF00 & argLong) >> 8);
            vResultBytes[1] = (byte)(int)((0xFF0000 & argLong) >> 16);
            vResultBytes[0] = (byte)(int)((0xFF000000 & argLong) >> 24);
            return vResultBytes;
        }
        /// <summary>
        /// 将字节数组转换为int类型
        /// </summary>
        /// <param name="argBytes"></param>
        /// <returns></returns>
        public static int Bytes4ToInt(byte[] argBytes)
        {
            return ((0xFF & argBytes[0]) << 24 | (0xFF & argBytes[1]) << 16
                    | (0xFF & argBytes[2]) << 8 | 0xFF & argBytes[3]);
        }
        /// <summary>
        /// 将字节数组转换为long类型
        /// </summary>
        /// <param name="mybytes"></param>
        /// <returns></returns>
        public static long Bytes4ToLong(byte[] mybytes)
        {
            return ((0xFF & mybytes[0]) << 24 | (0xFF & mybytes[1]) << 16
                    | (0xFF & mybytes[2]) << 8 | 0xFF & mybytes[3]);
        }
        /// <summary>
        /// 数组拷贝
        /// </summary>
        /// <param name="argSource">源数组</param>
        /// <param name="argDest">目标数组</param>
        /// <param name="argSourcebegin">源开始位置</param>
        /// <param name="argSourceEnd">源结束位置</param>
        /// <param name="argDestBegin">目标数组开始位置</param>
        public static void BytesCopy(byte[] argSource,
                                     byte[] argDest,
                                     int argSourcebegin,
                                     int argSourceEnd,
                                     int argDestBegin)
        {
            int j = 0;
            for (int i = argSourcebegin; i <= argSourceEnd; ++i)
            {
                argDest[(argDestBegin + j)] = argSource[i];
                ++j;
            }
        }


        #endregion

        #region Private

        /// <summary>
        /// 获得命令体总字节(头+信息体)
        /// </summary>
        /// <param name="argSrcCode"></param>
        /// <returns></returns>
        private byte[] GetCommandBodybytes()
        {
            byte[] vCommandBytes = new byte[(int)SGIPCommandDefine.LEN_SGIP_HEADER + _BodyBytes.Length];
            // 拷贝head字节
            Buffer.BlockCopy(_Header.GetCommandHeadBytes(), 0, vCommandBytes, 0,(int)SGIPCommandDefine.LEN_SGIP_HEADER);
            // 拷贝body字节
            Buffer.BlockCopy(_BodyBytes, 0, vCommandBytes, (int)SGIPCommandDefine.LEN_SGIP_HEADER, _Header.TotalMsgLen - (int)SGIPCommandDefine.LEN_SGIP_HEADER);
            return vCommandBytes;
        }
        /// <summary>
        /// 读取Body字节
        /// </summary>
        /// <param name="argSocket"></param>
        private void ReadDataIntoBody(Socket argSocket)
        {
            _BodyBytes = new byte[_Header.TotalMsgLen - (int)SGIPCommandDefine.LEN_SGIP_HEADER];
            if (_BodyBytes.Length == 0)
                return;
            try
            {
                argSocket.Receive(_BodyBytes);
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }

        #endregion
    }
}
