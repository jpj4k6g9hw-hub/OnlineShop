using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Data.SqlClient;

public partial class Admin_Login : System.Web.UI.Page
{
    protected void btnLogin_Click(object sender, EventArgs e)
    {
        string user = Request.Form[txtUser.UniqueID];
        string pass = Request.Form[txtPass.UniqueID];
        var dt = DBHelper.ExecuteDataTable("SELECT Role FROM userInfo WHERE Username=@u AND Password=@p", new MySqlParameter("@u", user), new MySqlParameter("@p", pass));
        if (dt.Rows.Count == 0) { Response.Write("<script>alert('用户不存在');</script>"); return; }
        if (dt.Rows[0]["Role"].ToString() != "admin") { Response.Write("<script>alert('非管理员账户');</script>"); return; }
        Session["CurrentUser"] = user;
        Session["CurrentUserRole"] = "admin";
        Response.Redirect("Default.aspx");
    }
}