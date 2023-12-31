using Domain.Entities;

public static class Helper {
    public static void Print(Product[] data)
    {
        
        foreach (var row in data )
        {
            Console.WriteLine(row.Price + " ,"+ row.Title );
        }
    }
}