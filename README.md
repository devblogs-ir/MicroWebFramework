# MicroWebFramework

## How to use
In this sample implementatino, an HttpListner is listening to the port 3998 on localhost.
there are 2 sample controllers: User and Order. Each controller has 2 actions called GetAll and GetById which GetAll has not inputs but GetById can take an Id as input.
Here are sample http calls:
```sh
http://localhost:3998/Order/GetAll
http://localhost:3998/Order/GetById/2
http://localhost:3998/User/GetAll
http://localhost:3998/User/GetById/4
```
