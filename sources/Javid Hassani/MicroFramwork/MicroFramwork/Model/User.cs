namespace MicroFramwork.Model;

public class User(string name, string family)
{
    public string Name { get; set; } = name;
    public string Family { get; set; } = family;
}
