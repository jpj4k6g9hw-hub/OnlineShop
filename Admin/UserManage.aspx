<%@ Page Language="C#" AutoEventWireup="true" CodeFile="UserManage.aspx.cs" Inherits="Admin_UserManage" %>
<!DOCTYPE html><html><head><meta charset="utf-8" /></head><body>
<h2>会员管理</h2>
用户名/手机号: <asp:TextBox ID="txtQuery" runat="server" />
<asp:Button ID="btnQuery" Text="查询" runat="server" OnClick="btnQuery_Click" />
<br /><br />
<asp:GridView ID="gvUsers" runat="server" AutoGenerateColumns="false" OnRowCommand="gvUsers_RowCommand">
    <Columns>
        <asp:BoundField DataField="Username" HeaderText="用户名" />
        <asp:BoundField DataField="Email" HeaderText="邮箱" />
        <asp:BoundField DataField="Telephone" HeaderText="电话" />
        <asp:TemplateField HeaderText="操作">
            <ItemTemplate>
                <asp:LinkButton CommandName="DeleteUser" CommandArgument='<%# Eval("Username") %>' runat="server" OnClientClick="return confirm('确定删除该用户?')">删除</asp:LinkButton>
            </ItemTemplate>
        </asp:TemplateField>
    </Columns>
</asp:GridView>
</body></html>