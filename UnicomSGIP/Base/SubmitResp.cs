using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnicomSGIP.Base
{
    /// <summary>
    /// Submit响应类
    /// </summary>
    public class SubmitResp : SGIPCommand
    {
        /// <summary>
        /// Submit响应
        /// </summary>
        /// <param name="argCommand"></param>
        public SubmitResp(SGIPCommand argCommand)
        {
            _Header = argCommand._Header;
            Result = argCommand._BodyBytes[0];
        }

        /// <summary>
        /// 结果
        /// </summary>
        public byte Result { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Reserve { get; set; }
    }
}
