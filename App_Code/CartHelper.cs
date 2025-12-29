using System;
using System.Collections.Generic;
using System.Data;
using MySql.Data.MySqlClient;

public static class CartHelper
{
    // 从 DB 读取用户购物车
    public static List<CartItem> LoadCartFromDb(string username)
    {
        var list = new List<CartItem>();
        if (string.IsNullOrEmpty(username)) return list;

        string sql = @"SELECT uci.ProductID, uci.Number, uci.Size, uci.Price, p.Name, p.PictureUrl
                       FROM userCartItem uci
                       LEFT JOIN productInfo p ON uci.ProductID = p.id
                       WHERE uci.Username = @user";
        var dt = DBHelper.ExecuteDataTable(sql, new MySqlParameter("@user", username));
        foreach (DataRow r in dt.Rows)
        {
            list.Add(new CartItem
            {
                ProductID = Convert.ToInt32(r["ProductID"]),
                Number = r["Number"] == DBNull.Value ? 1 : Convert.ToInt32(r["Number"]),
                Size = r["Size"] == DBNull.Value ? "" : r["Size"].ToString(),
                Price = r["Price"] == DBNull.Value ? 0m : Convert.ToDecimal(r["Price"]),
                Name = r["Name"] == DBNull.Value ? "" : r["Name"].ToString(),
                PictureUrl = r["PictureUrl"] == DBNull.Value ? "" : r["PictureUrl"].ToString()
            });
        }
        return list;
    }

    // 保存整个购物车（先删除用户现有条目，再插入）
    public static void SaveCartToDb(string username, List<CartItem> cart)
    {
        if (string.IsNullOrEmpty(username)) return;
        // 事务性操作可在这里实现；为简洁使用简单方式：删除再插入
        DBHelper.ExecuteNonQuery("DELETE FROM userCartItem WHERE Username = @user", new MySqlParameter("@user", username));
        if (cart == null || cart.Count == 0) return;
        foreach (var item in cart)
        {
            string insert = @"INSERT INTO userCartItem (Username, ProductID, Number, Size, Price)
                              VALUES (@user, @pid, @num, @size, @price)
                              ON DUPLICATE KEY UPDATE Number = @num";
            DBHelper.ExecuteNonQuery(insert,
                new MySqlParameter("@user", username),
                new MySqlParameter("@pid", item.ProductID),
                new MySqlParameter("@num", item.Number),
                new MySqlParameter("@size", item.Size ?? ""),
                new MySqlParameter("@price", item.Price));
        }
    }

    // 添加或更新单条（登录态下，逐条写入以保证实时性）
    public static void AddOrUpdateItem(string username, CartItem item)
    {
        if (string.IsNullOrEmpty(username) || item == null) return;
        // 若存在则累加数量
        string upsert = @"
            INSERT INTO userCartItem (Username, ProductID, Number, Size, Price)
            VALUES (@user, @pid, @num, @size, @price)
            ON DUPLICATE KEY UPDATE Number = Number + VALUES(Number), Price = VALUES(Price)";
        DBHelper.ExecuteNonQuery(upsert,
            new MySqlParameter("@user", username),
            new MySqlParameter("@pid", item.ProductID),
            new MySqlParameter("@num", item.Number),
            new MySqlParameter("@size", item.Size ?? ""),
            new MySqlParameter("@price", item.Price));
    }

    public static void UpdateItemQuantity(string username, int productId, string size, int number)
    {
        if (string.IsNullOrEmpty(username)) return;
        if (number <= 0)
        {
            DBHelper.ExecuteNonQuery("DELETE FROM userCartItem WHERE Username=@user AND ProductID=@pid AND Size=@size",
                new MySqlParameter("@user", username),
                new MySqlParameter("@pid", productId),
                new MySqlParameter("@size", size ?? ""));
        }
        else
        {
            DBHelper.ExecuteNonQuery("UPDATE userCartItem SET Number=@num WHERE Username=@user AND ProductID=@pid AND Size=@size",
                new MySqlParameter("@num", number),
                new MySqlParameter("@user", username),
                new MySqlParameter("@pid", productId),
                new MySqlParameter("@size", size ?? ""));
        }
    }

    public static void ClearCart(string username)
    {
        if (string.IsNullOrEmpty(username)) return;
        DBHelper.ExecuteNonQuery("DELETE FROM userCartItem WHERE Username=@user", new MySqlParameter("@user", username));
    }

    // 合并 Session 购物车（guestCart）与 DB（user），返回合并后的列表（并持久化）
    public static List<CartItem> MergeSessionCartToDb(string username, List<CartItem> sessionCart)
    {
        var dbCart = LoadCartFromDb(username);
        // 合并：dbCart 优先保留并累加数量
        var map = new Dictionary<string, CartItem>(); // key = pid|size
        Func<int, string, string> keyOf = (pid, size) => pid + "|" + (size ?? "");
        foreach (var it in dbCart) map[keyOf(it.ProductID, it.Size)] = new CartItem
        {
            ProductID = it.ProductID,
            Number = it.Number,
            Size = it.Size,
            Price = it.Price,
            Name = it.Name,
            PictureUrl = it.PictureUrl
        };
        if (sessionCart != null)
        {
            foreach (var it in sessionCart)
            {
                var k = keyOf(it.ProductID, it.Size);
                if (map.ContainsKey(k))
                {
                    map[k].Number += it.Number;
                }
                else
                {
                    map[k] = new CartItem { ProductID = it.ProductID, Number = it.Number, Size = it.Size, Price = it.Price, Name = it.Name, PictureUrl = it.PictureUrl };
                }
            }
        }
        var merged = new List<CartItem>(map.Values);
        // 保存回 DB
        SaveCartToDb(username, merged);
        return merged;
    }
}