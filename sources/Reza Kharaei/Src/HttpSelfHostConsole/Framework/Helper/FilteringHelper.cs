using System;

namespace HttpSelfHostConsole.Framework.Helper
{
    public class FilteringHelper
    {
        public bool IsValid(string IP)
        {
            if (string.IsNullOrEmpty(IP))
                throw new Exception("Your IP Address is not accessible");

            if (IP.StartsWith("98."))
                return false;

            return true;
        }
    }
}
