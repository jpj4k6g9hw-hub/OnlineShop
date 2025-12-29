using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Data.SqlClient;

public partial class Admin_UserManage : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack) BindUsers();
        if (Session["CurrentUserRole"] as string != "admin") Response.Redirect("Login.aspx");
    }

    private void BindUsers(string kw = "")
    {
        if (string.IsNullOrEmpty(kw))
            gvUsers.DataSource = DBHelper.ExecuteDataTable("SELECT Username, Email, Telephone FROM userInfo WHERE Role='member'");
        else
            gvUsers.DataSource = DBHelper.ExecuteDataTable("SELECT Username, Email, Telephone FROM userInfo WHERE Role='member' AND (Username LIKE @k OR Telephone LIKE @k)", new MySqlParameter("@k", "%" + kw + "%"));
        gvUsers.DataBind();
    }

    protected void btnQuery_Click(object sender, EventArgs e)
    {
        BindUsers(txtQuery.Text.Trim());
    }

    protected void gvUsers_RowCommand(object sender, System.Web.UI.WebControls.GridViewCommandEventArgs e)
    {
        if (e.CommandName == "DeleteUser")
        {
            string username = e.CommandArgument.ToString();
            DBHelper.ExecuteNonQuery("DELETE FROM userInfo WHERE Username=@u", new MySqlParameter("@u", username));
            BindUsers();
        }
    }
}