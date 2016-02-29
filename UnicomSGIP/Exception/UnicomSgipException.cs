namespace UnicomSGIP.Exception
{
    public class UnicomSgipException
    {
        public static void LengthExceededError(string argSource,string argSourceValue, int argLength)
        {          
            throw new System.Exception(string.Concat(argSource," longer than", argLength, " bytes:", argSourceValue));
        }

        public static void CustomerException(string argStr)
        {
            throw new System.Exception(argStr);
        }
    }
}
