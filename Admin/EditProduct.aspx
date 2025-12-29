<%@ Page Language="C#" AutoEventWireup="true" CodeFile="EditProduct.aspx.cs" Inherits="Admin_EditProduct" %>
<!DOCTYPE html><html><head><meta charset="utf-8" /></head><body>
<h2>添加/编辑商品</h2>
<form id="form1" runat="server">
    名称: <asp:TextBox ID="txtName" runat="server" /><br />
    图片URL: <asp:TextBox ID="txtImg" runat="server" /><br />
    价格: <asp:TextBox ID="txtPrice" runat="server" /><br />
    品牌: <asp:TextBox ID="txtBrand" runat="server" /><br />
    尺码: <asp:TextBox ID="txtSize" runat="server" /><br />
    适用年龄: <asp:TextBox ID="txtForAges" runat="server" /><br />
    库存: <asp:TextBox ID="txtStock" runat="server" /><br />
    类别: <asp:DropDownList ID="ddlCategory" runat="server" /><br />
    <asp:Button ID="btnSave" Text="保存" runat="server" OnClick="btnSave_Click" />
</form>
</body></html>