using SepiMicroFrameWork.Configuration;
using System.Net;

var listener = new HttpListener();

var perfixesBase = "http://localhost:9001/";
var perfixesUsers = "http://localhost:9001/UserController/GetAllUser/";
var perfixesUser = "http://localhost:9001/UserController/GetUserByPhoneNumber/09393025280/";
var perfixesProducts = "http://localhost:9001/UserController/GetAllProducts/";
var perfixesProduct = "http://localhost:9001/UserController/GetProductByCode/R-0004/";
listener.Prefixes.Add(perfixesBase);
listener.Prefixes.Add(perfixesUsers);
listener.Prefixes.Add(perfixesUser);
listener.Prefixes.Add(perfixesProducts);
listener.Prefixes.Add(perfixesProduct);

Console.WriteLine($"Start Listening to {perfixesBase} ...");
Console.WriteLine($"Start Listening to {perfixesUsers} ...");
Console.WriteLine($"Start Listening to {perfixesUser} ...");
Console.WriteLine($"Start Listening to {perfixesProducts} ...");
Console.WriteLine($"Start Listening to {perfixesProduct} ...");
listener.Start();

var httpContext = listener.GetContext();

var app = new PiplineBuilder()
                .AddPipe<ExeptionHandler>()
                .AddPipe<Authentication>()
                .AddPipe<EndPointHandler>()
                .Builder();
app(httpContext);

httpContext.Response.Close();
Console.ReadKey();