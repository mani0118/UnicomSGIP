using System;
using System.Text;

namespace UnicomSGIP.Base
{
    /// <summary>
    /// bind命令返回体
    /// </summary>
    public class BindResp : SGIPCommand
    {
        public BindResp(byte[] argUnicomSN)
        {
            _Header = new SGIPHeader((uint)SGIPCommandDefine.SGIP_BIND_RESP);
            _Header.UnicomSN = argUnicomSN;
            _BodyBytes = new byte[(int)SGIPCommandDefine.LEN_SGIP_BIND_RESP];
            byte[] vReserveByte = new byte[8];
            Buffer.BlockCopy(Encoding.ASCII.GetBytes(Reserve), 0, vReserveByte, 0, Reserve.Length);
            Buffer.BlockCopy(vReserveByte, 0, _BodyBytes, 1, 8);
        }

        /// <summary>
        /// bind返回
        /// </summary>
        /// <param name="argCommand"></param>
        public BindResp(SGIPCommand argCommand)
        {
            _Header = argCommand._Header;
            _BodyBytes = argCommand._BodyBytes;

            Result = argCommand._BodyBytes[0];
            byte[] vReserveBytes = new byte[8];
            Buffer.BlockCopy(argCommand._BodyBytes, 1, vReserveBytes, 0, 8);
            Reserve = Encoding.ASCII.GetString(vReserveBytes);
        }

        /// <summary>
        /// 绑定结果
        /// </summary>
        public int Result { get; set; }
        /// <summary>
        /// 返回值
        /// </summary>
        public string Reserve { get; set; }

    }
}
