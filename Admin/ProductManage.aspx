<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ProductManage.aspx.cs" Inherits="Admin_ProductManage" %>
<!DOCTYPE html><html><head><meta charset="utf-8" /></head><body>
<h2>商品管理</h2>
<a href="EditProduct.aspx">添加新商品</a>
<br /><br />
<asp:GridView ID="gvProducts" runat="server" AutoGenerateColumns="false" OnRowCommand="gvProducts_RowCommand">
    <Columns>
        <asp:BoundField DataField="id" HeaderText="ID" />
        <asp:BoundField DataField="Name" HeaderText="名称" />
        <asp:BoundField DataField="Price" HeaderText="价格" DataFormatString="{0:C2}" />
        <asp:BoundField DataField="Stock" HeaderText="库存" />
        <asp:TemplateField HeaderText="操作">
            <ItemTemplate>
                <asp:LinkButton ID="lnkEdit" CommandName="EditItem" CommandArgument='<%# Eval("id") %>' runat="server">编辑</asp:LinkButton>
                &nbsp;
                <asp:LinkButton ID="lnkDel" CommandName="DeleteItem" CommandArgument='<%# Eval("id") %>' runat="server" OnClientClick="return confirm('确定删除吗?')">删除</asp:LinkButton>
            </ItemTemplate>
        </asp:TemplateField>
    </Columns>
</asp:GridView>
</body></html>