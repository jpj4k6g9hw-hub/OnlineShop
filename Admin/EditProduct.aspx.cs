using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Xml.Linq;

public partial class Admin_EditProduct : System.Web.UI.Page
{
    protected int id = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindCategories();
            if (int.TryParse(Request.QueryString["id"], out id) && id > 0)
            {
                LoadProduct(id);
            }
        }
    }

    private void BindCategories()
    {
        ddlCategory.DataSource = DBHelper.ExecuteDataTable("SELECT id, Name FROM categoryInfo");
        ddlCategory.DataTextField = "Name";
        ddlCategory.DataValueField = "id";
        ddlCategory.DataBind();
    }

    private void LoadProduct(int id)
    {
        var dt = DBHelper.ExecuteDataTable("SELECT * FROM productInfo WHERE id=@id", new MySqlParameter("@id", id));
        if (dt.Rows.Count == 0) return;
        var r = dt.Rows[0];
        txtName.Text = r["Name"].ToString();
        txtImg.Text = r["PictureUrl"].ToString();
        txtPrice.Text = r["Price"].ToString();
        txtBrand.Text = r["Brand"].ToString();
        txtSize.Text = r["Size"].ToString();
        txtForAges.Text = r["ForAges"].ToString();
        txtStock.Text = r["Stock"].ToString();
        ddlCategory.SelectedValue = r["CategoryID"].ToString();
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        int cid = int.Parse(ddlCategory.SelectedValue);
        decimal price = decimal.Parse(txtPrice.Text.Trim());
        int stock = int.Parse(txtStock.Text.Trim());
        if (int.TryParse(Request.QueryString["id"], out id) && id > 0)
        {
            DBHelper.ExecuteNonQuery("UPDATE productInfo SET Name=@n, PictureUrl=@p, Price=@price, Brand=@b, Size=@s, ForAges=@f, Stock=@st, CategoryID=@cid WHERE id=@id",
                new MySqlParameter("@n", txtName.Text.Trim()),
                new MySqlParameter("@p", txtImg.Text.Trim()),
                new MySqlParameter("@price", price),
                new MySqlParameter("@b", txtBrand.Text.Trim()),
                new MySqlParameter("@s", txtSize.Text.Trim()),
                new MySqlParameter("@f", txtForAges.Text.Trim()),
                new MySqlParameter("@st", stock),
                new MySqlParameter("@cid", cid),
                new MySqlParameter("@id", id));
        }
        else
        {
            DBHelper.ExecuteNonQuery("INSERT INTO productInfo (Name, PictureUrl, Price, Brand, Size, ForAges, Stock, CategoryID) VALUES (@n,@p,@price,@b,@s,@f,@st,@cid)",
                new MySqlParameter("@n", txtName.Text.Trim()),
                new MySqlParameter("@p", txtImg.Text.Trim()),
                new MySqlParameter("@price", price),
                new MySqlParameter("@b", txtBrand.Text.Trim()),
                new MySqlParameter("@s", txtSize.Text.Trim()),
                new MySqlParameter("@f", txtForAges.Text.Trim()),
                new MySqlParameter("@st", stock),
                new MySqlParameter("@cid", cid));
        }
        Response.Redirect("ProductManage.aspx");
    }
}