using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnicomSGIP.Base
{
    /// <summary>
    /// 用于通知server端，拆掉已建立的链接
    /// </summary>
    public class UnBind : SGIPCommand
    {
        /// <summary>
        /// 解绑
        /// </summary>
        public UnBind()
        {
            _Header = new SGIPHeader((uint)SGIPCommandDefine.SGIP_UNBIND);
        }
        /// <summary>
        /// 解绑响应
        /// </summary>
        /// <param name="argCommand"></param>
        public UnBind(SGIPCommand argCommand)
        {
            _Header = argCommand._Header;
            _BodyBytes = argCommand._BodyBytes;
        }

    }
}
