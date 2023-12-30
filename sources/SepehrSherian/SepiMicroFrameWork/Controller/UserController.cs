using SepiMicroFrameWork.Configuration;
using System.Net;
using System.Text;

namespace SepiMicroFrameWork.Controller;

public class UserController(HttpListenerContext Context)
{
    private IEnumerable<User> Users => new[]
    {
                            new User() {FirstName="sepehr", LastName="Sherian", PhoneNumber="09393025280" },
                            new User(){FirstName ="Ali", LastName="Sharifi", PhoneNumber="09125647898" },
                            new User(){FirstName ="narges", LastName="Alavi", PhoneNumber="09192036589" },
    };

    public IEnumerable<User> GetAllUser()
    {
        var stringBuilder = new StringBuilder();
        var users = Users.ToList();
        foreach (var user in users)
        {
            stringBuilder.AppendLine($"ID :{user.Id}");
            stringBuilder.AppendLine($"First Name :{user.FirstName}");
            stringBuilder.AppendLine($"Last Name :{user.LastName}");
            stringBuilder.AppendLine($"Phone Number :{user.PhoneNumber}");
            stringBuilder.AppendLine($"##################################");
        }
        var buffer = Encoding.UTF8.GetBytes(stringBuilder.ToString());
        Context.Response.OutputStream.Write(buffer, 0, buffer.Length);
        return users;
    }

    public User GetUserByPhoneNumber(string phoneNumber)
    {
        var stringBuilder = new StringBuilder();
        var thisUser = Users.FirstOrDefault(c => c.PhoneNumber == phoneNumber, new User());
        stringBuilder.AppendLine($"ID :{thisUser.Id}");
        stringBuilder.AppendLine($"First Name :{thisUser.FirstName}");
        stringBuilder.AppendLine($"Last Name :{thisUser.LastName}");
        stringBuilder.AppendLine($"Phone Number :{thisUser.PhoneNumber}");
        stringBuilder.AppendLine($"##################################");
        var buffer = Encoding.UTF8.GetBytes(stringBuilder.ToString());
        Context.Response.OutputStream.Write(buffer, 0, buffer.Length);
        return Users.FirstOrDefault(c => c.PhoneNumber == phoneNumber, new User());
    }
}
