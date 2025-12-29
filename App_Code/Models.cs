using System;
using System.Collections.Generic;

public class UserInfo
{
    public string Role { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
    public string Email { get; set; }
    public string Telephone { get; set; }
    public string Postcode { get; set; }
}

public class CategoryInfo
{
    public int id { get; set; }
    public string Name { get; set; }
}

public class ProductInfo
{
    public int id { get; set; }
    public string Name { get; set; }
    public string PictureUrl { get; set; }
    public decimal Price { get; set; }
    public string Brand { get; set; }
    public string Size { get; set; }
    public string ForAges { get; set; }
    public int Stock { get; set; }
    public int? CategoryID { get; set; }
    public DateTime CreateDate { get; set; }
    public int Status { get; set; }
}

public class OrderItem
{
    public int id { get; set; }
    public int OrderID { get; set; }
    public int ProductID { get; set; }
    public int Number { get; set; }
    public decimal Price { get; set; }
    public string size { get; set; }
}

public class OrderInfo
{
    public int id { get; set; }
    public decimal TotalMoney { get; set; }
    public int TotalNum { get; set; }
    public DateTime CreateDate { get; set; }
    public string UserName { get; set; }
    public string Addressee { get; set; }
    public string Address { get; set; }
    public string Tel { get; set; }
    public string Status { get; set; }
}

public class CartItem
{
    public int ProductID { get; set; }
    public string Name { get; set; }
    public string PictureUrl { get; set; }
    public decimal Price { get; set; }
    public int Number { get; set; }
    public string Size { get; set; }
}