using System;
using System.Data;
using System.Xml.Linq;

public partial class Admin_EditCategory : System.Web.UI.Page
{
    protected int id = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (int.TryParse(Request.QueryString["id"], out id) && id > 0)
            {
                var dt = DBHelper.ExecuteDataTable("SELECT * FROM categoryInfo WHERE id=@id", new MySql.Data.MySqlClient.MySqlParameter("@id", id));
                if (dt.Rows.Count > 0) txtName.Text = dt.Rows[0]["Name"].ToString();
            }
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (int.TryParse(Request.QueryString["id"], out id) && id > 0)
        {
            DBHelper.ExecuteNonQuery("UPDATE categoryInfo SET Name=@n WHERE id=@id", new MySql.Data.MySqlClient.MySqlParameter("@n", txtName.Text.Trim()), new MySql.Data.MySqlClient.MySqlParameter("@id", id));
        }
        else
        {
            DBHelper.ExecuteNonQuery("INSERT INTO categoryInfo (Name) VALUES (@n)", new MySql.Data.MySqlClient.MySqlParameter("@n", txtName.Text.Trim()));
        }
        Response.Redirect("CategoryManage.aspx");
    }
}