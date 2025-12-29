using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Data.SqlClient;

public partial class Admin_OrderManage : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack) BindOrders();
        if (Session["CurrentUserRole"] as string != "admin") Response.Redirect("Login.aspx");
    }

    private void BindOrders()
    {
        gvOrders.DataSource = DBHelper.ExecuteDataTable("SELECT id, UserName, TotalMoney, TotalNum, Status FROM orderInfo ORDER BY CreateDate DESC");
        gvOrders.DataBind();
    }

    protected void gvOrders_RowCommand(object sender, System.Web.UI.WebControls.GridViewCommandEventArgs e)
    {
        int id = Convert.ToInt32(e.CommandArgument);
        if (e.CommandName == "Deliver")
        {
            DBHelper.ExecuteNonQuery("UPDATE orderInfo SET Status='已配送' WHERE id=@id", new MySqlParameter("@id", id));
        }
        else if (e.CommandName == "Complete")
        {
            DBHelper.ExecuteNonQuery("UPDATE orderInfo SET Status='已完成' WHERE id=@id", new MySqlParameter("@id", id));
        }
        BindOrders();
    }
}