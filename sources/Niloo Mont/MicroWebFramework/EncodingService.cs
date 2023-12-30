using System.Text;

namespace MicroWebFramework;

public static class EncodingService
{
    public static byte[] GetBytes(string input) => 
        Encoding.UTF8.GetBytes(input+"\n");
}
