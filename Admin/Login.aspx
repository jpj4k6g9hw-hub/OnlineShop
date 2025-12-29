<%@ Page Title="" Language="C#" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="Admin_Login" %>
<!DOCTYPE html><html><head><meta charset="utf-8" /></head><body>
<h2>管理员登录</h2>
<form id="form1" runat="server">
    用户名: <asp:TextBox ID="txtUser" runat="server" /><br />
    密码: <asp:TextBox ID="txtPass" TextMode="Password" runat="server" /><br />
    <asp:Button ID="btnLogin" Text="登录" OnClick="btnLogin_Click" runat="server" />
</form>
</body></html>