using System;
using System.Data;

public partial class Admin_CategoryManage : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack) BindCategories();
        if (Session["CurrentUserRole"] as string != "admin") Response.Redirect("Login.aspx");
    }
    private void BindCategories()
    {
        gvCat.DataSource = DBHelper.ExecuteDataTable("SELECT id, Name FROM categoryInfo");
        gvCat.DataBind();
    }

    protected void gvCat_RowCommand(object sender, System.Web.UI.WebControls.GridViewCommandEventArgs e)
    {
        int id = Convert.ToInt32(e.CommandArgument);
        if (e.CommandName == "DeleteItem")
        {
            DBHelper.ExecuteNonQuery("DELETE FROM categoryInfo WHERE id=@id", new MySql.Data.MySqlClient.MySqlParameter("@id", id));
            BindCategories();
        }
        else if (e.CommandName == "EditItem")
        {
            Response.Redirect("EditCategory.aspx?id=" + id);
        }
    }
}