using System;

public partial class Admin_Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        var role = Session["CurrentUserRole"] as string;
        if (role != "admin") Response.Redirect("Login.aspx");
    }
}