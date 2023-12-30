using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace SepiMicroFrameWork.Configuration
{
    public static class ResponseConfig
    {
        public static byte[] SayWelcome()
        {
            var message = "Welcome to my Micro FrameWork for WebApplication";
            var buffer = Encoding.UTF8.GetBytes(message);
            return buffer;
        }

        public static byte[] ErrorMessage(string error, string perfix)
        {
            var stringBuilder = new StringBuilder();
            stringBuilder.AppendLine("You have Error!");
            stringBuilder.AppendLine(error);
            stringBuilder.AppendLine("#################");
            stringBuilder.AppendLine("For Use Better select Some Of below URL");
            stringBuilder.AppendLine($"Users: {perfix}/UserController/GetAllUser");
            stringBuilder.AppendLine($"Users By PhoneNumber: {perfix}/UserController/GetUserByPhoneNumber/yourphonenumber");
            stringBuilder.AppendLine($"Products: {perfix}/ProductController/GetAllProducts");
            stringBuilder.AppendLine($"Products By Code: {perfix}/ProductController/GetProductByCode/productcode");
            var buffer = Encoding.UTF8.GetBytes(stringBuilder.ToString());
            return buffer;
        }
    }
}
