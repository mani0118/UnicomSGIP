namespace UnicomSGIP.Base
{
    /// <summary>
    /// Bind命令的响应类
    /// </summary>
    public class UnBindResp : SGIPCommand
    {
        public UnBindResp(byte[] argUnicomSN)
        {
            _Header = new SGIPHeader((uint)SGIPCommandDefine.SGIP_UNBIND_RESP);
            _Header.UnicomSN = argUnicomSN;
        }

        public UnBindResp(SGIPCommand argCommand)
        {
            _Header = argCommand._Header;
        }
    }
}
