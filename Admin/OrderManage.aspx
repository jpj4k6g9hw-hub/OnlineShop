<%@ Page Language="C#" AutoEventWireup="true" CodeFile="OrderManage.aspx.cs" Inherits="Admin_OrderManage" %>
<!DOCTYPE html><html><head><meta charset="utf-8" /></head><body>
<h2>订单管理</h2>
<asp:GridView ID="gvOrders" runat="server" AutoGenerateColumns="false" OnRowCommand="gvOrders_RowCommand">
    <Columns>
        <asp:BoundField DataField="id" HeaderText="订单ID" />
        <asp:BoundField DataField="UserName" HeaderText="用户" />
        <asp:BoundField DataField="TotalMoney" HeaderText="总金额" DataFormatString="{0:C2}" />
        <asp:BoundField DataField="TotalNum" HeaderText="总数量" />
        <asp:BoundField DataField="Status" HeaderText="状态" />
        <asp:TemplateField HeaderText="操作">
            <ItemTemplate>
                <asp:LinkButton CommandName="Deliver" CommandArgument='<%# Eval("id") %>' runat="server">标为已配送</asp:LinkButton>
                &nbsp;
                <asp:LinkButton CommandName="Complete" CommandArgument='<%# Eval("id") %>' runat="server">标为已完成</asp:LinkButton>
            </ItemTemplate>
        </asp:TemplateField>
    </Columns>
</asp:GridView>
</body></html>