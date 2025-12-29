<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CategoryManage.aspx.cs" Inherits="Admin_CategoryManage" %>
<!DOCTYPE html><html><head><meta charset="utf-8" /></head><body>
<h2>商品类别</h2>
<a href="EditCategory.aspx">添加类别</a>
<br /><br />
<asp:GridView ID="gvCat" runat="server" AutoGenerateColumns="false" OnRowCommand="gvCat_RowCommand">
    <Columns>
        <asp:BoundField DataField="id" HeaderText="ID" />
        <asp:BoundField DataField="Name" HeaderText="名称" />
        <asp:TemplateField HeaderText="操作">
            <ItemTemplate>
                <asp:LinkButton CommandName="EditItem" CommandArgument='<%# Eval("id") %>' runat="server">编辑</asp:LinkButton>
                &nbsp;
                <asp:LinkButton CommandName="DeleteItem" CommandArgument='<%# Eval("id") %>' runat="server" OnClientClick="return confirm('确认删除?')">删除</asp:LinkButton>
            </ItemTemplate>
        </asp:TemplateField>
    </Columns>
</asp:GridView>
</body></html>