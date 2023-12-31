﻿using MicroWebFramework.Common;
using MicroWebFramework.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace MicroWebFramework.Controllers;
public class SetOrderController
{
    private readonly HttpContext _httpContext;

    public SetOrderController(HttpContext httpContext)
    {
        _httpContext = httpContext;
    }
    List<Order> orders = new List<Order>()
    {
        new Order() { ID = 1, Name = "Order1" },
        new Order() { ID = 2, Name = "Order2" },
    };

    public void GetAllOrders()
    {
        string ordersList = JsonSerializer.Serialize(orders, new JsonSerializerOptions
        {
            WriteIndented = true
        });
        _httpContext.Response.OutputStream.Write(Encoding.UTF8.GetBytes(ordersList));
    }


    public void GetOrderById(int id)
    {
        if (!orders.Any(p => p.ID == id))
        {
            _httpContext.Response.OutputStream.Write(Encoding.UTF8.GetBytes($"No order was found with id: {id}!"));
            return;
        }
        _httpContext.Response.OutputStream.Write(Encoding.UTF8.GetBytes(orders.SingleOrDefault(p => p.ID == id).Name));
        return;
    }
}

