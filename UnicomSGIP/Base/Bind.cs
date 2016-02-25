using System;
using System.Text;

namespace UnicomSGIP.Base
{
    /// <summary>
    ///Bind类
    /// </summary>
    public class Bind : SGIPCommand
    {
        /// <summary>
        /// 初始化默认值
        /// </summary>
        /// <param name="argLoginName"></param>
        /// <param name="argPassword"></param>
        public Bind(string argLoginName, string argPassword)
        {
            _LoginName = argLoginName;
            _Password = argPassword;
            _Reserve = string.Empty;
            _LoginType = 1;

            _Header = new SGIPHeader((uint)SGIPCommandDefine.SGIP_BIND);
            _BodyBytes = new byte[(int)SGIPCommandDefine.LEN_SGIP_BIND];
        }

        /// <summary>
        /// 初始化Read值
        /// </summary>
        /// <param name="argCommand"></param>
        public Bind(SGIPCommand argCommand)
        {
            _Header = argCommand._Header;
            _BodyBytes = argCommand._BodyBytes;

            _LoginType = argCommand._BodyBytes[0];
            byte[] vLoginNameBytes = new byte[16];
            Buffer.BlockCopy(argCommand._BodyBytes, 1, vLoginNameBytes, 0, 16);
            _LoginName = Encoding.ASCII.GetString(vLoginNameBytes).Trim();

            byte[] vLoginPwdBytes = new byte[16];
            Buffer.BlockCopy(argCommand._BodyBytes, 17, vLoginPwdBytes, 0, 16);
            _Password = Encoding.ASCII.GetString(vLoginPwdBytes).Trim();

            byte[] vReserveByte = new byte[8];
            Buffer.BlockCopy(argCommand._BodyBytes, 33, vReserveByte, 0, 8);
            _Reserve = Encoding.ASCII.GetString(vReserveByte).Trim();
        }

        #region 数据

        private byte _LoginType;
        private string _LoginName;
        private string _Password;
        private string _Reserve;

        #endregion

        /// <summary>
        /// 填充Body字节
        /// </summary>
        public void FillBodyBytes()
        {
            FillLoginType();
            FillLoginName();
            FillPassword();
            FillReserve();
        }

        #region private

        private void FillReserve()
        {
            byte[] vReserveByte = new byte[8];
            Buffer.BlockCopy(Encoding.ASCII.GetBytes(_Reserve), 0, vReserveByte, 0, _Reserve.Length);
            Buffer.BlockCopy(vReserveByte, 0, _BodyBytes, 33, 8);
        }

        private void FillLoginType()
        {
            _BodyBytes[0] = _LoginType;
        }

        private void FillLoginName()
        {
            byte[] vLoginNameByte = new byte[16];
            Buffer.BlockCopy(Encoding.ASCII.GetBytes(_LoginName), 0, vLoginNameByte, 0, _LoginName.Length);
            Buffer.BlockCopy(vLoginNameByte, 0, _BodyBytes, 1, 16);
        }

        private void FillPassword()
        {
            byte[] vLoginPwdByte = new byte[16];
            Buffer.BlockCopy(Encoding.ASCII.GetBytes(_Password), 0, vLoginPwdByte, 0, _Password.Length);
            Buffer.BlockCopy(vLoginPwdByte, 0, _BodyBytes, 17, 16);
        }

        #endregion


    }
}
