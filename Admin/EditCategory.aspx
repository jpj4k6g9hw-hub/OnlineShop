<%@ Page Language="C#" AutoEventWireup="true" CodeFile="EditCategory.aspx.cs" Inherits="Admin_EditCategory" %>
<!DOCTYPE html><html><head><meta charset="utf-8" /></head><body>
<h2>添加/编辑类别</h2>
<form id="form1" runat="server">
    名称: <asp:TextBox ID="txtName" runat="server" /><br />
    <asp:Button ID="btnSave" Text="保存" runat="server" OnClick="btnSave_Click" />
</form>
</body></html>