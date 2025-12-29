using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Data.SqlClient;

public partial class Admin_ProductManage : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack) BindProducts();
        if (Session["CurrentUserRole"] as string != "admin") Response.Redirect("Login.aspx");
    }

    private void BindProducts()
    {
        gvProducts.DataSource = DBHelper.ExecuteDataTable("SELECT id, Name, Price, Stock FROM productInfo");
        gvProducts.DataBind();
    }

    protected void gvProducts_RowCommand(object sender, System.Web.UI.WebControls.GridViewCommandEventArgs e)
    {
        int id = Convert.ToInt32(e.CommandArgument);
        if (e.CommandName == "DeleteItem")
        {
            DBHelper.ExecuteNonQuery("DELETE FROM productInfo WHERE id=@id", new MySqlParameter("@id", id));
            BindProducts();
        }
        else if (e.CommandName == "EditItem")
        {
            Response.Redirect("EditProduct.aspx?id=" + id);
        }
    }
}