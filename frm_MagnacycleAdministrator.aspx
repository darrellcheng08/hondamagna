<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="frm_MagnacycleAdministrator.aspx.cs"
    Inherits="HondaMagnacycleProcurementProject.frm_MagnacycleAdministrator" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <style type="text/css">
        .style1
        {
            width: 100%;
        }
        .style2
        {
            text-align: right;
        }
        .style4
        {
            text-align: left;
            margin-left: 80px;
        }
        .style13
        {
            text-align: left;
        }
        .style16
        {
            text-align: right;
            width: 172px;
        }
        .style17
        {
            width: 465px;
        }
        .style19
        {
            text-align: right;
            margin-left: 80px;
        }
        .style20
        {
            text-align: left;
        }
        .style21
        {
            text-align: left;
            height: 30px;
        }
        .style28
        {
            text-align: center;
            height: 23px;
        }
        .style29
        {
            text-align: right;
            width: 444px;
        }
        .style31
        {
            text-align: right;
            width: 444px;
            height: 30px;
        }
        .style36
        {
            width: 317px;
        }
        .style37
        {
            text-align: left;
        }
        .style40
        {
            width: 739px;
            text-align: right;
        }
        .style41
        {
            width: 720px;
            text-align: right;
            margin-left: 40px;
        }
        .style48
        {
            width: 664px;
            text-align: right;
        }
        .style51
        {
            width: 589px;
            text-align: right;
        }
        .style52
        {
            width: 589px;
        }
        .style53
        {
            width: 447px;
            text-align: left;
        }
        .style54
        {
            width: 447px;
            text-align: left;
        }
        .style58
        {
            text-align: left;
            width: 2424px;
        }
        .style59
        {
            width: 447px;
            text-align: left;
            height: 26px;
        }
        .style60
        {
            width: 589px;
            text-align: right;
            height: 26px;
        }
        .style61
        {
            width: 739px;
            text-align: right;
            height: 26px;
        }
        .style62
        {
        }
        .style63
        {
            width: 190px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="True" EnableScriptGlobalization="True" EnableCdn="True">
    </asp:ScriptManager>
    <div>
        <table class="style1">
            <tr>
                <td>
                    <asp:Button ID="btn_PurchaseList" runat="server" Text="Purchase List" OnClick="btn_PurchaseList_Click" />
                    <asp:Button ID="btn_OrderInformations" runat="server" Text="Order Information" OnClick="btn_OrderInformations_Click" />
                    <asp:Button ID="btn_Messaging" runat="server" Text="Messages" OnClick="btn_Messaging_Click" />
                    <asp:Button ID="btn_UsersMaintenance" runat="server" Text="Users Maintenance" OnClick="btn_UsersMaintenance_Click" />
                    <asp:Button ID="btn_Inventory" runat="server" Text="Inventory" Width="113px" OnClick="btn_Inventory_Click" />
                </td>
                <td style="text-align: right">
                    <asp:Button ID="btn_SignOut" runat="server" OnClick="btn_SignOut_Click" Text="Sign Out" />
                </td>
            </tr>
        </table>
    </div>
    <div>
        <asp:MultiView ID="MultiView1" runat="server">
            <asp:View ID="View1" runat="server">
                <div>
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                            <asp:Timer ID="Timer1" runat="server" Interval="1000" OnTick="Timer1_Tick">
                            </asp:Timer>
                            <asp:Panel ID="Panel1" runat="server" Height="29px" Width="250px">
                                New Orders:
                                <asp:Label ID="lbl_NumberNewPO" runat="server" Font-Bold="True" Font-Names="Castellar"
                                    ForeColor="#00CC00" Text="0" Visible="False"></asp:Label>
                                <asp:Label ID="lbl_notifN" runat="server" Font-Bold="True" Font-Names="Castellar"
                                    ForeColor="#FF3300" Visible="False">0</asp:Label>
                            </asp:Panel>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="Timer1" EventName="Tick" />
                        </Triggers>
                    </asp:UpdatePanel>
                    <asp:SqlDataSource ID="src_SentUnitsPO" runat="server" ConnectionString="<%$ ConnectionStrings:MagnaAdministratorConnectionString %>"
                        SelectCommand="SELECT [PurchaseOrderNumber], [DateReceived], [DateModified], [DateSent], [Status] FROM [SystemUnitsOrderListsTBL]">
                    </asp:SqlDataSource>
                    <asp:SqlDataSource ID="src_SentPartsPO" runat="server" ConnectionString="<%$ ConnectionStrings:MagnaAdministratorConnectionString %>"
                        SelectCommand="SELECT [PurchaseOrderNumber], [DateReceived], [DateModified], [DateSent], [Status] FROM [SystemPartsOrderListsTBL]">
                    </asp:SqlDataSource>
                    <div>
                        <table class="style1">
                            <tr>
                                <td class="style36">
                                    <asp:Label ID="Label1" runat="server" Text="Select:"></asp:Label>
                                    <asp:DropDownList ID="ddwn_tab1_Select" runat="server" AutoPostBack="True">
                                        <asp:ListItem>Units</asp:ListItem>
                                        <asp:ListItem>Spare Parts</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                <td class="style2">
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td class="style36">
                                    &nbsp;
                                </td>
                                <td class="style2">
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td class="style36">
                                    <asp:Button ID="btn_refresh" runat="server" OnClick="btn_refresh_Click" Text="Refresh List" />
                                </td>
                                <td class="style2">
                                    &nbsp;
                                </td>
                            </tr>
                        </table>
                    </div>
                    <div>
                        <asp:GridView ID="grid_SentPO" runat="server" Width="918px" AutoGenerateSelectButton="True"
                            Style="text-align: center" BackColor="White" BorderColor="#DEDFDE" BorderStyle="None"
                            BorderWidth="1px" CellPadding="4" OnSelectedIndexChanged="grid_SentPO_SelectedIndexChanged"
                            ForeColor="Black" GridLines="Vertical">
                            <AlternatingRowStyle BackColor="White" />
                            <FooterStyle BackColor="#CCCC99" />
                            <HeaderStyle BackColor="#6B696B" Font-Bold="True" ForeColor="White" />
                            <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Right" />
                            <RowStyle BackColor="#F7F7DE" />
                            <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
                            <SortedAscendingCellStyle BackColor="#FBFBF2" />
                            <SortedAscendingHeaderStyle BackColor="#848384" />
                            <SortedDescendingCellStyle BackColor="#EAEAD3" />
                            <SortedDescendingHeaderStyle BackColor="#575357" />
                        </asp:GridView>
                    </div>
                </div>
            </asp:View>
            <asp:View ID="View2" runat="server">
                <asp:MultiView ID="MultiView4" runat="server">
                    <asp:View ID="View7" runat="server">
                        <div style="text-align: right">
                            <table class="style1">
                                <tr>
                                    <td class="style40" colspan="3">
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td class="style59">
                                        <asp:Label ID="lbl_tab1_POReferenceNo" runat="server" Text="P.O. Number:"></asp:Label>
                                    </td>
                                    <td class="style60">
                                        <asp:TextBox ID="txt_tab1_POReferenceNo" runat="server" ReadOnly="True" Style="margin-left: 0px"></asp:TextBox>
                                    </td>
                                    <td class="style61">
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td class="style53">
                                        <asp:Label ID="lbl_DateOfPO" runat="server" Text="Date of PO:"></asp:Label>
                                    </td>
                                    <td class="style51">
                                        <asp:TextBox ID="txt_DateOfPO" runat="server" ReadOnly="True"></asp:TextBox>
                                    </td>
                                    <td class="style40">
                                        <asp:Button ID="btn_Back" runat="server" OnClick="btn_Back_Click" Text="Back" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="style53">
                                        &nbsp;</td>
                                    <td class="style51">
                                        &nbsp;
                                    </td>
                                    <td class="style40">
                                        &nbsp;
                                    </td>
                                </tr>
                            </table>
                            <table class="style1">
                                <tr>
                                    <td class="style37" colspan="4">




                                    
                                         <asp:GridView ID="grid_PurchaseList" runat="server" AutoGenerateSelectButton="True"
                                            BackColor="White" BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px"
                                            CellPadding="4" ForeColor="Black" GridLines="Vertical" OnSelectedIndexChanged="grid_PurchaseList_SelectedIndexChanged"
                                            Style="text-align: center" Width="749px">
                                            <AlternatingRowStyle BackColor="White" />
                                            <FooterStyle BackColor="#CCCC99" />
                                            <HeaderStyle BackColor="#6B696B" Font-Bold="True" ForeColor="White" />
                                            <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Right" />
                                            <RowStyle BackColor="#F7F7DE" />
                                            <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
                                            <SortedAscendingCellStyle BackColor="#FBFBF2" />
                                            <SortedAscendingHeaderStyle BackColor="#848384" />
                                            <SortedDescendingCellStyle BackColor="#EAEAD3" />
                                            <SortedDescendingHeaderStyle BackColor="#575357" />
                                        </asp:GridView>






                                       






                                    </td>
                                    <td>
                                        <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional" 
                                            RenderMode="Inline" >
                                            <ContentTemplate>
                                                <asp:Timer ID="Timer2" runat="server" Interval="250" OnTick="Timer2_Tick">
                                                </asp:Timer>
                                                <div class="style47">
                                                <asp:GridView ID="grid_Updates" runat="server" BackColor="White" BorderColor="#DEDFDE"
                                                    BorderStyle="None" BorderWidth="1px" CellPadding="4" ForeColor="Black" GridLines="Vertical"
                                                    Style="text-align: center">
                                                    <AlternatingRowStyle BackColor="White" />
                                                    <FooterStyle BackColor="#CCCC99" />
                                                    <HeaderStyle BackColor="#6B696B" Font-Bold="True" ForeColor="White" />
                                                    <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Right" />
                                                    <RowStyle BackColor="#F7F7DE" />
                                                    <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
                                                    <SortedAscendingCellStyle BackColor="#FBFBF2" />
                                                    <SortedAscendingHeaderStyle BackColor="#848384" />
                                                    <SortedDescendingCellStyle BackColor="#EAEAD3" />
                                                    <SortedDescendingHeaderStyle BackColor="#575357" />
                                                </asp:GridView>
                                                </div>
                                            </ContentTemplate>
                                            <Triggers>
                                                <asp:AsyncPostBackTrigger ControlID="Timer2" EventName="Tick" />
                                            </Triggers>
                                        </asp:UpdatePanel>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="style37" colspan="4">
                                        &nbsp;
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td class="style37" colspan="4">
                                        <asp:Label ID="lbl_Added" runat="server" Text="Added Items to this P.O. :" Visible="False"></asp:Label>
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td class="style37" colspan="4">





                                   
                                        <asp:GridView ID="grid_Addeds" runat="server" AutoGenerateSelectButton="True" CellPadding="4"
                                            ForeColor="Black" GridLines="Vertical" Style="text-align: center" Width="750px"
                                            BackColor="White" BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px"
                                            OnSelectedIndexChanged="grid_Addeds_SelectedIndexChanged">
                                            <AlternatingRowStyle BackColor="White" />
                                            <FooterStyle BackColor="#CCCC99" />
                                            <HeaderStyle BackColor="#6B696B" Font-Bold="True" ForeColor="White" />
                                            <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Right" />
                                            <RowStyle BackColor="#F7F7DE" />
                                            <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
                                            <SortedAscendingCellStyle BackColor="#FBFBF2" />
                                            <SortedAscendingHeaderStyle BackColor="#848384" />
                                            <SortedDescendingCellStyle BackColor="#EAEAD3" />
                                            <SortedDescendingHeaderStyle BackColor="#575357" />
                                        </asp:GridView>




                                        







                                    </td>
                                    <td>
                                        <asp:UpdatePanel ID="UpdatePanel4" runat="server" UpdateMode="Conditional" 
                                            RenderMode="Inline">
                                            <ContentTemplate>
                                                <asp:Timer ID="Timer4" runat="server" Interval="250" OnTick="Timer4_Tick">
                                                </asp:Timer>
                                                <div class="style47">
                                                <asp:GridView ID="grid_Updates1" runat="server" CellPadding="4" ForeColor="Black"
                                                    GridLines="Vertical" Style="text-align: center" BackColor="White"
                                                    BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px">
                                                    <AlternatingRowStyle BackColor="White" />
                                                    <FooterStyle BackColor="#CCCC99" />
                                                    <HeaderStyle BackColor="#6B696B" Font-Bold="True" ForeColor="White" />
                                                    <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Right" />
                                                    <RowStyle BackColor="#F7F7DE" />
                                                    <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
                                                    <SortedAscendingCellStyle BackColor="#FBFBF2" />
                                                    <SortedAscendingHeaderStyle BackColor="#848384" />
                                                    <SortedDescendingCellStyle BackColor="#EAEAD3" />
                                                    <SortedDescendingHeaderStyle BackColor="#575357" />
                                                </asp:GridView>
                                                <div>
                                            </ContentTemplate>
                                            <Triggers>
                                                <asp:AsyncPostBackTrigger ControlID="Timer4" EventName="Tick" />
                                            </Triggers>
                                        </asp:UpdatePanel>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="style37" colspan="4">
                                        <asp:Label ID="lbl_Quan" runat="server" Text="Quantity:" Visible="False"></asp:Label>
                                        <asp:TextBox ID="txt_quants" runat="server" MaxLength="3" Visible="False" Width="67px"></asp:TextBox>
                                        <asp:Label ID="lbl_Color" runat="server" Text="Color:" Visible="False"></asp:Label>
                                        <asp:DropDownList ID="ddwn_Colors" runat="server" Visible="False">
                                        </asp:DropDownList>
                                        <asp:Button ID="btn_SaveEdit" runat="server" OnClick="btn_SaveEdit_Click" Text="Save"
                                            Visible="False" />
                                        <asp:Button ID="btn_CancelEdit" runat="server" OnClick="btn_CancelEdit_Click" Text="Cancel"
                                            Visible="False" />
                                        <asp:Label ID="lbl_Quan2" runat="server" Text="Quantity:" Visible="False"></asp:Label>
                                        <asp:TextBox ID="txt_quants2" runat="server" MaxLength="3" Visible="False" Width="67px"></asp:TextBox>
                                        <asp:Label ID="lbl_Color2" runat="server" Text="Color:" Visible="False"></asp:Label>
                                        <asp:DropDownList ID="ddwn_Colors2" runat="server" Visible="False">
                                        </asp:DropDownList>
                                        <asp:Button ID="btn_SaveEdit2" runat="server" OnClick="btn_SaveEdit2_Click" Text="Save"
                                            Visible="False" />
                                        <asp:Button ID="btn_CancelEdit2" runat="server" OnClick="btn_CancelEdit2_Click" Text="Cancel"
                                            Visible="False" />
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td class="style37" colspan="4">
                                        <asp:Button ID="btn_AddItem" runat="server" Height="25px" OnClick="btn_EditItems_Click"
                                            Style="margin-right: 0px" Text="Add Items" />
                                        <asp:Button ID="btn_EditItem" runat="server" OnClick="btn_EditPO_Click" Text="Edit"
                                            Visible="False" />
                                        <asp:Button ID="btn_RemoveItem" runat="server" OnClick="btn_RemovePO_Click" Text="Remove"
                                            Visible="False" />
                                        <asp:Button ID="btn_EditItem2" runat="server" OnClick="btn_EditItem2_Click" Text="Edit"
                                            Visible="False" />
                                        <asp:Button ID="btn_RemoveItem2" runat="server" OnClick="btn_RemoveItem2_Click" Text="Remove"
                                            Visible="False" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="style37" colspan="4">
                                        <asp:Button ID="btn_SecPO" runat="server" OnClick="btn_SecPO_Click" Text="Add Secondary P.O." />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="style37" colspan="2">
                                        &nbsp;
                                    </td>
                                    <td class="style41" colspan="2">
                                        <asp:Button ID="btn_CheckForUpdates" runat="server" OnClick="btn_CheckForUpdates_Click"
                                            Text="Check for Updates" Width="133px" />
                                        <asp:Button ID="btn_Compute" runat="server" OnClick="btn_Compute_Click" Text="Compute" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="style54">
                                        <asp:Label ID="lbl_Total" runat="server" Text="Sub-Total:"></asp:Label>
                                    </td>
                                    <td class="style52">
                                        <asp:TextBox ID="txt_Total" runat="server" ReadOnly="True" Style="text-align: right"></asp:TextBox>
                                    </td>
                                    <td class="style58">
                                        <asp:Label ID="lbl_ServiceCharge" runat="server" Text="Service Charge Per:"></asp:Label>
                                    </td>
                                    <td class="style48">
                                        <asp:TextBox ID="txt_ServiceCharge" runat="server" OnTextChanged="txt_ServiceCharge_TextChanged"
                                            ReadOnly="True" Style="text-align: right"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="style54">
                                        <asp:Label ID="lbl_Discount" runat="server" Text="Discount:"></asp:Label>
                                    </td>
                                    <td class="style52">



                                    <asp:UpdatePanel ID="UpdatePanel7" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <asp:Timer ID="Timer7" runat="server" Interval="1000" OnTick="Timer7_Tick">
                                        </asp:Timer>
<asp:TextBox ID="txt_Discount" runat="server" ReadOnly="True" Style="text-align: right"></asp:TextBox>
                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:AsyncPostBackTrigger ControlID="Timer7" EventName="Tick" />
                                    </Triggers>
                                </asp:UpdatePanel>



                                        
                                    </td>
                                    <td class="style58">
                                        <asp:Label ID="lbl_TotalCharge" runat="server" Text="Total Service Charge:"></asp:Label>
                                    </td>
                                    <td class="style48">
                                        <asp:TextBox ID="txt_TotalCharge" runat="server" ReadOnly="True" Style="text-align: right"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="style54">
                                        Discounted Amount:
                                    </td>
                                    <td class="style52">
                                        <asp:TextBox ID="txt_DiscountedAmount" runat="server" ReadOnly="True"></asp:TextBox>
                                    </td>
                                    <td class="style58">
                                        <asp:Label ID="lbl_GrandTotal" runat="server" Text="Total:"></asp:Label>
                                    </td>
                                    <td class="style48">
                                        <asp:TextBox ID="txt_GrandTotal" runat="server" OnTextChanged="txt_GrandTotal_TextChanged"
                                            Style="text-align: right" Width="128px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="style54">
                                        <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Conditional">
                                            <ContentTemplate>
                                                <asp:Timer ID="Timer3" runat="server" Interval="1000" OnTick="Timer3_Tick">
                                                </asp:Timer>
                                                VAT:&nbsp;
                                                <asp:Label ID="lbl_vat" runat="server" Font-Bold="True" Font-Names="Arial Black"
                                                    ForeColor="Red"></asp:Label>
                                            </ContentTemplate>
                                            <Triggers>
                                                <asp:AsyncPostBackTrigger ControlID="Timer3" EventName="Tick" />
                                            </Triggers>
                                        </asp:UpdatePanel>
                                    </td>
                                    <td class="style51">
                                        <asp:TextBox ID="txt_Vat" runat="server" ReadOnly="True" Style="text-align: right"></asp:TextBox>
                                    </td>
                                    <td class="style58">
                                        &nbsp;
                                    </td>
                                    <td class="style48">
                                        <asp:Button ID="btn_Stock" runat="server" OnClick="btn_Stock_Click" Text="Stock Report" />
                                        <asp:Button ID="btn_SendToHPI" runat="server" OnClick="btn_SendToHPI_Click" Text="Send to HPI" />
                                        <asp:Button ID="btn_CancelOrder" runat="server" OnClick="btn_CancelOrder_Click" Text="Cancel Order" />
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </asp:View>
                    <asp:View ID="View11" runat="server">
                        <table class="style1">
                            <tr>
                                <td style="text-align: right">
                                    <asp:Button ID="btn_BCK" runat="server" OnClick="btn_BCK_Click" Text="Back" />
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                            </tr>
                        </table>
                        Selected New Item/s:<table class="style1">
                            <tr>
                                <td>
                                    <asp:GridView ID="grid_AddedItems" runat="server" AutoGenerateSelectButton="True"
                                        BackColor="White" BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px"
                                        CellPadding="4" ForeColor="Black" GridLines="Vertical" Style="text-align: center"
                                        Width="954px" OnSelectedIndexChanged="grid_AddedItems_SelectedIndexChanged" OnRowDeleting="grid_AddedItems_RowDeleting">
                                        <AlternatingRowStyle BackColor="White" />
                                        <FooterStyle BackColor="#CCCC99" />
                                        <HeaderStyle BackColor="#6B696B" Font-Bold="True" ForeColor="White" />
                                        <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Right" />
                                        <RowStyle BackColor="#F7F7DE" />
                                        <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
                                        <SortedAscendingCellStyle BackColor="#FBFBF2" />
                                        <SortedAscendingHeaderStyle BackColor="#848384" />
                                        <SortedDescendingCellStyle BackColor="#EAEAD3" />
                                        <SortedDescendingHeaderStyle BackColor="#575357" />
                                    </asp:GridView>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lbl_Quan1" runat="server" Text="Quantity:" Visible="False"></asp:Label>
                                    <asp:TextBox ID="txt_quants1" runat="server" MaxLength="3" Visible="False" Width="67px"></asp:TextBox>
                                    <asp:Label ID="lbl_Color1" runat="server" Text="Color:" Visible="False"></asp:Label>
                                    <asp:DropDownList ID="ddwn_Colors1" runat="server" Visible="False">
                                    </asp:DropDownList>
                                    <asp:Button ID="btn_SaveEdit1" runat="server" Text="Save" Visible="False" OnClick="btn_SaveEdit1_Click" />
                                    <asp:Button ID="btn_CancelEdit1" runat="server" Text="Cancel" Visible="False" OnClick="btn_CancelEdit1_Click" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Button ID="btn_Edits" runat="server" OnClick="btn_Edits_Click" Text="Edit" />
                                    <asp:Button ID="btn_Remove" runat="server" Text="Remove" OnClick="btn_Remove_Click" />
                                    <asp:Button ID="btn_AddTo" runat="server" Text="Add to ..." OnClick="btn_AddTo_Click" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Available Units/Spare Parts:
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:TextBox ID="txt_ModelSparePartsName" runat="server" Width="350px">Model or Spare Parts Name</asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:SqlDataSource ID="src_AvailUnits" runat="server" ConnectionString="<%$ ConnectionStrings:HPILogInConnectionString %>"
                                        SelectCommand="SELECT [ModelCode], [Description], [Color] FROM [SystemModelsTBL]">
                                    </asp:SqlDataSource>
                                    <asp:SqlDataSource ID="src_AvailParts" runat="server" ConnectionString="<%$ ConnectionStrings:HPILogInConnectionString %>"
                                        SelectCommand="SELECT [PartNumber], [Description], [Color] FROM [SystemPartsTBL]">
                                    </asp:SqlDataSource>
                                    <asp:UpdatePanel ID="UpdatePanel5" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <asp:Timer ID="Timer5" runat="server" Interval="1000" OnTick="Timer5_Tick">
                                            </asp:Timer>
                                            <asp:GridView ID="grid_Availables" runat="server" AutoGenerateSelectButton="True"
                                                BackColor="White" BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px"
                                                CellPadding="4" ForeColor="Black" GridLines="Vertical" OnSelectedIndexChanged="grid_Availables_SelectedIndexChanged"
                                                Style="text-align: center" Width="954px">
                                                <AlternatingRowStyle BackColor="White" />
                                                <FooterStyle BackColor="#CCCC99" />
                                                <HeaderStyle BackColor="#6B696B" Font-Bold="True" ForeColor="White" />
                                                <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Right" />
                                                <RowStyle BackColor="#F7F7DE" />
                                                <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
                                                <SortedAscendingCellStyle BackColor="#FBFBF2" />
                                                <SortedAscendingHeaderStyle BackColor="#848384" />
                                                <SortedDescendingCellStyle BackColor="#EAEAD3" />
                                                <SortedDescendingHeaderStyle BackColor="#575357" />
                                            </asp:GridView>
                                        </ContentTemplate>
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger ControlID="Timer5" EventName="Tick" />
                                        </Triggers>
                                    </asp:UpdatePanel>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lbl_tab1_Quantity" runat="server" Font-Bold="False" Text="Quantity:"></asp:Label>
                                    <asp:TextBox ID="txt_tab1_Quantity" runat="server" MaxLength="3" Width="61px"></asp:TextBox>
                                    <asp:Button ID="btn_AddUnitsParts" runat="server" OnClick="btn_AddUnitsParts_Click"
                                        Text="Add" Width="64px" />
                                </td>
                            </tr>
                        </table>
                    </asp:View>
                </asp:MultiView>
            </asp:View>
            <asp:View ID="View3" runat="server">
                <div>
                    <asp:Button ID="btn_FrontEndUsers" runat="server" Text="Front End Users" OnClick="btn_FrontEndUsers_Click" />
                    <asp:Button ID="btn_Admins" runat="server" Text="Administrator" OnClick="btn_Admins_Click" />
                </div>
                <div>
                    <asp:MultiView ID="MultiView2" runat="server">
                        <asp:View ID="View4" runat="server">
                            <div>
                                <h1>
                                    Magnacycle User Informations</h1>
                            </div>
                            <div>
                                <table class="style1">
                                    <tr>
                                        <td class="style2">
                                            Employee ID Number:
                                        </td>
                                        <td class="style4">
                                            <asp:TextBox ID="txt_EmployerIDNumber" runat="server" MaxLength="20"></asp:TextBox>
                                        </td>
                                        <td class="style2">
                                            Age:
                                        </td>
                                        <td class="style4">
                                            <asp:TextBox ID="txt_Age" runat="server" MaxLength="2"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="style2">
                                            First Name:
                                        </td>
                                        <td class="style4">
                                            <asp:TextBox ID="txt_FName" runat="server" MaxLength="25"></asp:TextBox>
                                        </td>
                                        <td class="style2">
                                            Birthday:
                                        </td>
                                        <td class="style4">
                                            <asp:DropDownList ID="ddwn_Month" runat="server">
                                                <asp:ListItem Value="01">January</asp:ListItem>
                                                <asp:ListItem Value="02">February</asp:ListItem>
                                                <asp:ListItem Value="03">March</asp:ListItem>
                                                <asp:ListItem Value="04">April</asp:ListItem>
                                                <asp:ListItem Value="05">May</asp:ListItem>
                                                <asp:ListItem Value="06">June</asp:ListItem>
                                                <asp:ListItem Value="07">July</asp:ListItem>
                                                <asp:ListItem Value="08">August</asp:ListItem>
                                                <asp:ListItem Value="09">Septemer</asp:ListItem>
                                                <asp:ListItem Value="10">October</asp:ListItem>
                                                <asp:ListItem Value="11">November</asp:ListItem>
                                                <asp:ListItem Value="12">December</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:DropDownList ID="ddwn_Day" runat="server">
                                                <asp:ListItem Value="01">1</asp:ListItem>
                                                <asp:ListItem Value="02">2</asp:ListItem>
                                                <asp:ListItem Value="03">3</asp:ListItem>
                                                <asp:ListItem Value="04">4</asp:ListItem>
                                                <asp:ListItem Value="05">5</asp:ListItem>
                                                <asp:ListItem Value="06">6</asp:ListItem>
                                                <asp:ListItem Value="07">7</asp:ListItem>
                                                <asp:ListItem Value="08">8</asp:ListItem>
                                                <asp:ListItem Value="09">9</asp:ListItem>
                                                <asp:ListItem>10</asp:ListItem>
                                                <asp:ListItem>11</asp:ListItem>
                                                <asp:ListItem>12</asp:ListItem>
                                                <asp:ListItem>13</asp:ListItem>
                                                <asp:ListItem>14</asp:ListItem>
                                                <asp:ListItem>15</asp:ListItem>
                                                <asp:ListItem>16</asp:ListItem>
                                                <asp:ListItem>17</asp:ListItem>
                                                <asp:ListItem>18</asp:ListItem>
                                                <asp:ListItem>19</asp:ListItem>
                                                <asp:ListItem>20</asp:ListItem>
                                                <asp:ListItem>21</asp:ListItem>
                                                <asp:ListItem>22</asp:ListItem>
                                                <asp:ListItem>23</asp:ListItem>
                                                <asp:ListItem>24</asp:ListItem>
                                                <asp:ListItem>25</asp:ListItem>
                                                <asp:ListItem>26</asp:ListItem>
                                                <asp:ListItem>27</asp:ListItem>
                                                <asp:ListItem>28</asp:ListItem>
                                                <asp:ListItem>29</asp:ListItem>
                                                <asp:ListItem>30</asp:ListItem>
                                                <asp:ListItem>31</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:DropDownList ID="ddwn_Year" runat="server">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="style2">
                                            Middel Name:
                                        </td>
                                        <td class="style4">
                                            <asp:TextBox ID="txt_Mname" runat="server" MaxLength="20"></asp:TextBox>
                                        </td>
                                        <td class="style2">
                                            Citizenship:
                                        </td>
                                        <td class="style4">
                                            <asp:TextBox ID="txt_Citizenship" runat="server" MaxLength="15"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="style2">
                                            Surname:
                                        </td>
                                        <td class="style4">
                                            <asp:TextBox ID="txt_Surname" runat="server" MaxLength="20"></asp:TextBox>
                                        </td>
                                        <td class="style2">
                                            Email:
                                        </td>
                                        <td class="style4">
                                            <asp:TextBox ID="txt_Email" runat="server" MaxLength="30" TextMode="Email"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="style2">
                                            Gender:
                                        </td>
                                        <td class="style4">
                                            <asp:DropDownList ID="ddwn_Gender" runat="server">
                                                <asp:ListItem>Male</asp:ListItem>
                                                <asp:ListItem>Female</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                        <td class="style2">
                                            &nbsp;
                                        </td>
                                        <td class="style4">
                                            <asp:Button ID="btn_AddSave" runat="server" OnClick="btn_AddSave_Click" Text="Add"
                                                Width="64px" />
                                            <asp:Button ID="btn_Clear" runat="server" Text="Clear" Width="64px" OnClick="btn_Clear_Click" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="style2">
                                            &nbsp;
                                        </td>
                                        <td class="style4">
                                            &nbsp;
                                        </td>
                                        <td class="style2">
                                            &nbsp;
                                        </td>
                                        <td class="style4">
                                            &nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="style2">
                                            &nbsp;
                                        </td>
                                        <td class="style19" colspan="2">
                                            <asp:Label ID="lbl_Confirm" runat="server" Visible="False"></asp:Label>
                                        </td>
                                        <td class="style4">
                                            &nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="style2">
                                            &nbsp;
                                        </td>
                                        <td class="style19" colspan="2">
                                            <asp:Button ID="btn_OkSaver" runat="server" OnClick="btn_OkSaver_Click" Text="Ok"
                                                Visible="False" />
                                            <asp:Button ID="btn_CancelSaver" runat="server" OnClick="btn_CancelSaver_Click" Text="Cancel"
                                                Visible="False" Height="26px" />
                                            <asp:Button ID="btn_okD" runat="server" OnClick="btn_okD_Click" Text="Ok" Visible="False" />
                                            <asp:Button ID="btn_cancelD" runat="server" OnClick="btn_cancelD_Click" Text="Cancel"
                                                Visible="False" />
                                            <asp:Button ID="btn_Ok" runat="server" Text="Ok" Visible="False" OnClick="btn_Ok_Click" />
                                            <asp:Button ID="btn_Cancel" runat="server" Text="Cancel" Visible="False" OnClick="btn_Cancel_Click" />
                                        </td>
                                        <td class="style4">
                                            &nbsp;
                                        </td>
                                    </tr>
                                </table>
                            </div>
                            <div style="text-align: left">
                                <div style="text-align: left">
                                    Honda Magnacycle Employees<asp:SqlDataSource ID="EmployeesDataSource" runat="server"
                                        ConnectionString="<%$ ConnectionStrings:MagnaAdministratorConnectionString %>"
                                        SelectCommand="SELECT [EmployeeIDNumber], [FirstName], [MiddleName], [Surname], [Gender], [Age], [Birthday], [Citizenship], [Email] FROM [SystemMagnacycleEmployeesTBL]">
                                    </asp:SqlDataSource>
                                    <br />
                                </div>
                                <div>
                                    <asp:GridView ID="grid_Employers" runat="server" AutoGenerateSelectButton="True"
                                        Style="text-align: center" AutoGenerateColumns="False" BackColor="White" BorderColor="#CCCCCC"
                                        BorderStyle="None" BorderWidth="1px" CellPadding="3" DataSourceID="EmployeesDataSource"
                                        Width="874px" OnSelectedIndexChanged="grid_AuthorizedUsers_SelectedIndexChanged"
                                        DataKeyNames="EmployeeIDNumber">
                                        <Columns>
                                            <asp:BoundField DataField="EmployeeIDNumber" HeaderText="EmployeeIDNumber" SortExpression="EmployeeIDNumber"
                                                ReadOnly="True" />
                                            <asp:BoundField DataField="FirstName" HeaderText="FirstName" SortExpression="FirstName" />
                                            <asp:BoundField DataField="MiddleName" HeaderText="MiddleName" SortExpression="MiddleName" />
                                            <asp:BoundField DataField="Surname" HeaderText="Surname" SortExpression="Surname" />
                                            <asp:BoundField DataField="Gender" HeaderText="Gender" SortExpression="Gender" />
                                            <asp:BoundField DataField="Age" HeaderText="Age" SortExpression="Age" />
                                            <asp:BoundField DataField="Birthday" HeaderText="Birthday" SortExpression="Birthday" />
                                            <asp:BoundField DataField="Citizenship" HeaderText="Citizenship" SortExpression="Citizenship" />
                                            <asp:BoundField DataField="Email" HeaderText="Email" SortExpression="Email" />
                                        </Columns>
                                        <FooterStyle BackColor="White" ForeColor="#000066" />
                                        <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" />
                                        <PagerStyle ForeColor="#000066" HorizontalAlign="Left" BackColor="White" />
                                        <RowStyle ForeColor="#000066" />
                                        <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                                        <SortedAscendingCellStyle BackColor="#F1F1F1" />
                                        <SortedAscendingHeaderStyle BackColor="#007DBB" />
                                        <SortedDescendingCellStyle BackColor="#CAC9C9" />
                                        <SortedDescendingHeaderStyle BackColor="#00547E" />
                                    </asp:GridView>
                                    <div>
                                        <asp:Button ID="btn_AddAsUser" runat="server" Text="Add As User" OnClick="btn_AddAsUser_Click" />
                                        <asp:Button ID="btn_RemoveEmployer" runat="server" OnClick="btn_RemoveEmployer_Click"
                                            Text="Remove Employer" />
                                        <asp:Button ID="btn_Edit" runat="server" OnClick="btn_Edit_Click" Text="Edit" />
                                        <div>
                                            <table class="style1">
                                                <tr>
                                                    <td>
                                                        &nbsp;
                                                    </td>
                                                    <td>
                                                        &nbsp;
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        &nbsp;
                                                    </td>
                                                    <td>
                                                        &nbsp;
                                                    </td>
                                                </tr>
                                            </table>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div>
                                <h1>
                                    Magnacycle Current System User/s</h1>
                                <asp:SqlDataSource ID="AutorizedUsersDataSource" runat="server" ConnectionString="<%$ ConnectionStrings:MagnaAdministratorConnectionString %>"
                                    SelectCommand="SELECT [EmployeeIDNumber], [FirstName], [Surname], [UserName] FROM [SystemUserTBL]">
                                </asp:SqlDataSource>
                            </div>
                            <div>
                                <div>
                                    <table class="style1">
                                        <tr>
                                            <td style="text-align: center">
                                                <asp:GridView ID="grid_AuthorizedUsers" runat="server" AutoGenerateColumns="False"
                                                    AutoGenerateSelectButton="True" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None"
                                                    BorderWidth="1px" CellPadding="3" Height="117px" Style="text-align: center" Width="874px"
                                                    OnSelectedIndexChanged="grid_AuthorizedUsers_SelectedIndexChanged" DataKeyNames="EmployeeIDNumber"
                                                    DataSourceID="AutorizedUsersDataSource">
                                                    <Columns>
                                                        <asp:BoundField DataField="EmployeeIDNumber" HeaderText="EmployeeIDNumber" ReadOnly="True"
                                                            SortExpression="EmployeeIDNumber" />
                                                        <asp:BoundField DataField="FirstName" HeaderText="FirstName" SortExpression="FirstName" />
                                                        <asp:BoundField DataField="Surname" HeaderText="Surname" SortExpression="Surname" />
                                                        <asp:BoundField DataField="UserName" HeaderText="UserName" SortExpression="UserName" />
                                                    </Columns>
                                                    <FooterStyle BackColor="White" ForeColor="#000066" />
                                                    <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" />
                                                    <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                                                    <RowStyle ForeColor="#000066" />
                                                    <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                                                    <SortedAscendingCellStyle BackColor="#F1F1F1" />
                                                    <SortedAscendingHeaderStyle BackColor="#007DBB" />
                                                    <SortedDescendingCellStyle BackColor="#CAC9C9" />
                                                    <SortedDescendingHeaderStyle BackColor="#00547E" />
                                                </asp:GridView>
                                                <div style="text-align: left">
                                                    <asp:Button ID="btn_RemoveAccess" runat="server" OnClick="btn_RemoveAccess_Click"
                                                        Style="text-align: left" Text="Remove Access" />
                                                </div>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </div>
                        </asp:View>
                        <asp:View ID="View5" runat="server">
                            <div>
                                <div>
                                    <asp:MultiView ID="MultiView3" runat="server">
                                        <asp:View ID="View6" runat="server">
                                            <div>
                                                <h1>
                                                    Secondary Administrator User Informations</h1>
                                                <div>
                                                </div>
                                            </div>
                                            <div>
                                                <table class="style1">
                                                    <tr>
                                                        <td class="style13">
                                                            &nbsp;
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="style20">
                                                            Administrator/s
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="style13">
                                                            <asp:SqlDataSource ID="AdminsDataSource" runat="server" ConnectionString="<%$ ConnectionStrings:MagnaAdministratorConnectionString %>"
                                                                SelectCommand="SELECT [AdminIDNumber], [FirstName], [MiddleName], [Surname], [Gender], [Age], [Birthday], [Citizenship], [Email], [UserName] FROM [SystemAdminsTBL]">
                                                            </asp:SqlDataSource>
                                                            <asp:GridView ID="grid_Admins" runat="server" AutoGenerateColumns="False" AutoGenerateSelectButton="True"
                                                                BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px"
                                                                CellPadding="3" DataKeyNames="AdminIDNumber" DataSourceID="AdminsDataSource"
                                                                Style="text-align: center" Width="870px">
                                                                <Columns>
                                                                    <asp:BoundField DataField="AdminIDNumber" HeaderText="AdminIDNumber" ReadOnly="True"
                                                                        SortExpression="AdminIDNumber" />
                                                                    <asp:BoundField DataField="FirstName" HeaderText="FirstName" SortExpression="FirstName" />
                                                                    <asp:BoundField DataField="MiddleName" HeaderText="MiddleName" SortExpression="MiddleName" />
                                                                    <asp:BoundField DataField="Surname" HeaderText="Surname" SortExpression="Surname" />
                                                                    <asp:BoundField DataField="Gender" HeaderText="Gender" SortExpression="Gender" />
                                                                    <asp:BoundField DataField="Age" HeaderText="Age" SortExpression="Age" />
                                                                    <asp:BoundField DataField="Birthday" HeaderText="Birthday" SortExpression="Birthday" />
                                                                    <asp:BoundField DataField="Citizenship" HeaderText="Citizenship" SortExpression="Citizenship" />
                                                                    <asp:BoundField DataField="Email" HeaderText="Email" SortExpression="Email" />
                                                                    <asp:BoundField DataField="UserName" HeaderText="UserName" SortExpression="UserName" />
                                                                </Columns>
                                                                <FooterStyle BackColor="White" ForeColor="#000066" />
                                                                <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" />
                                                                <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                                                                <RowStyle ForeColor="#000066" />
                                                                <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                                                                <SortedAscendingCellStyle BackColor="#F1F1F1" />
                                                                <SortedAscendingHeaderStyle BackColor="#007DBB" />
                                                                <SortedDescendingCellStyle BackColor="#CAC9C9" />
                                                                <SortedDescendingHeaderStyle BackColor="#00547E" />
                                                            </asp:GridView>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="style21">
                                                            &nbsp;
                                                        </td>
                                                    </tr>
                                                </table>
                                                <table class="style1">
                                                    <tr>
                                                        <td class="style13" colspan="2">
                                                            &nbsp;
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="style28" colspan="2">
                                                            <h1>
                                                                Logged In</h1>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="style29">
                                                            Admin ID:
                                                        </td>
                                                        <td class="style20">
                                                            <asp:TextBox ID="txt_AdminsID" runat="server" ReadOnly="True"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="style29">
                                                            Full Name:
                                                        </td>
                                                        <td class="style20">
                                                            <asp:TextBox ID="txt_AdminFullName" runat="server" ReadOnly="True"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="style29">
                                                            User Name:
                                                        </td>
                                                        <td class="style20">
                                                            <asp:TextBox ID="txt_AdminUserName" runat="server" ReadOnly="True"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="style29">
                                                            Password:
                                                        </td>
                                                        <td class="style20">
                                                            <asp:TextBox ID="txt_AdminPassword" runat="server" TextMode="Password" ReadOnly="True"></asp:TextBox>
                                                            <asp:Button ID="btn_AdminChangePass" runat="server" Text="Change Password" OnClick="btn_AdminChangePass_Click" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="style31">
                                                            <asp:Label ID="lbl_AdminCurrentPass" runat="server" Text="Current Password:" Visible="False"></asp:Label>
                                                        </td>
                                                        <td class="style21">
                                                            <asp:TextBox ID="txt_AdminCurrentPass" runat="server" MaxLength="30" TextMode="Password"
                                                                Visible="False"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="style29">
                                                            <asp:Label ID="lbl_AdminNewPass" runat="server" Text="New Password:" Visible="False"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_AdminNewPassword" runat="server" MaxLength="30" TextMode="Password"
                                                                Visible="False"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="style29">
                                                            <asp:Label ID="lbl_AdminRetypePass" runat="server" Text="Re-type New Password" Visible="False"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_AdminConfirm" runat="server" MaxLength="30" TextMode="Password"
                                                                Visible="False"></asp:TextBox>
                                                            <asp:Button ID="btn_AdminPassSave" runat="server" OnClick="btn_AdminPassSave_Click"
                                                                Text="Save" Visible="False" />
                                                            <asp:Button ID="btn_AdminPassCancel" runat="server" Text="Cancel" Visible="False"
                                                                OnClick="btn_AdminPassCancel_Click" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </div>
                                        </asp:View>
                                    </asp:MultiView>
                                </div>
                            </div>
                        </asp:View>
                    </asp:MultiView>
                </div>
            </asp:View>
            <asp:View ID="View8" runat="server">
                <div>
                    <h1>
                        Messages</h1>
                    <p>
                        Honda Philippines Inc. Contact Information (Batangas)</p>
                    <div>
                        <table class="style1">
                            <tr>
                                <td class="style16">
                                    PLDT Toll Free Number:&nbsp;&nbsp;
                                </td>
                                <td class="style4">
                                    <asp:Label ID="lbl_PLDT" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="style16">
                                    Landline Number:&nbsp;&nbsp;
                                </td>
                                <td class="style4">
                                    <asp:Label ID="lbl_Landline" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="style16">
                                    Mobile Number:&nbsp;&nbsp;
                                </td>
                                <td class="style4">
                                    <asp:Label ID="lbl_Mobile" runat="server"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>

                <table class="style1">
                        <tr>
                            <td class="style17">
                                <div>
                                    <p>
                                        Messages</p>
                                        
                                        
                                        <table class="style1">
                                            <tr>
                                                <td>
                                                    <asp:UpdatePanel ID="UpdatePanel9" runat="server" UpdateMode="Conditional">
                                                        <ContentTemplate>
                                                            <asp:Timer ID="Timer9" runat="server" Interval="1000" OnTick="Timer9_Tick">
                                                            </asp:Timer>
                                                            <asp:Panel ID="Panel3" runat="server" Height="29px" Width="250px">
                                                                New Message:&nbsp;
                                                                <asp:Label ID="lbl_NumberNewPO0" runat="server" Font-Bold="True" 
                                                                    Font-Names="Castellar" ForeColor="#00CC00" Text="0" Visible="False"></asp:Label>
                                                                <asp:Label ID="lbl_notifN0" runat="server" Font-Bold="True" 
                                                                    Font-Names="Castellar" ForeColor="#FF3300" Visible="False">0</asp:Label>
                                                            </asp:Panel>
                                                        </ContentTemplate>
                                                        <Triggers>
                                                            <asp:AsyncPostBackTrigger ControlID="Timer9" EventName="Tick" />
                                                        </Triggers>
                                                    </asp:UpdatePanel>
                                                </td>
                                            </tr>
                                        </table>
                                    
                                </div>
                                <div>
                                    <asp:Button ID="btn_Inbox" runat="server" Text="Inbox" 
                                        onclick="btn_Inbox_Click" />
                                    <asp:Button ID="btn_Sent" runat="server" Text="Sent" onclick="btn_Sent_Click" />
                                    <asp:Button ID="btn_Drafts" runat="server" Text="Drafts" 
                                        onclick="btn_Drafts_Click" />
                                    <asp:Button ID="btn_Uploads" runat="server" onclick="btn_Uploads_Click" 
                                        Text="Files Uploaded" />
                                    <asp:MultiView ID="MultiView6" runat="server">
                                        <asp:View ID="View15" runat="server">
                                            <br />
                                            Inbox<asp:GridView ID="grid_Inbox" runat="server" AutoGenerateSelectButton="True" 
                                                Style="text-align: center" Width="466px" BackColor="White" 
                                                BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="4" 
                                                ForeColor="Black" GridLines="Horizontal" 
                                                onselectedindexchanged="grid_Inbox_SelectedIndexChanged">
                                                <FooterStyle BackColor="#CCCC99" ForeColor="Black" />
                                                <HeaderStyle BackColor="#333333" Font-Bold="True" ForeColor="White" />
                                                <PagerStyle BackColor="White" ForeColor="Black" HorizontalAlign="Right" />
                                                <SelectedRowStyle BackColor="#CC3333" Font-Bold="True" ForeColor="White" />
                                                <SortedAscendingCellStyle BackColor="#F7F7F7" />
                                                <SortedAscendingHeaderStyle BackColor="#4B4B4B" />
                                                <SortedDescendingCellStyle BackColor="#E5E5E5" />
                                                <SortedDescendingHeaderStyle BackColor="#242121" />
                                            </asp:GridView>
                                            <asp:Button ID="btn_DeleteInbox" runat="server" Text="Delete" 
                                                onclick="btn_DeleteInbox_Click" />
                                            <asp:Button ID="btn_DeleteAllInbox" runat="server" 
                                                onclick="btn_DeleteAllInbox_Click" Text="Delete All" />
                                        </asp:View>
                                        <asp:View ID="View16" runat="server">
                                            <br />
                                            Sent<asp:GridView ID="grid_Sent" runat="server" AutoGenerateSelectButton="True" 
                                                Style="text-align: center" Width="466px" BackColor="White" 
                                                BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="4" 
                                                ForeColor="Black" GridLines="Horizontal" 
                                                onselectedindexchanged="grid_Sent_SelectedIndexChanged">
                                                <FooterStyle BackColor="#CCCC99" ForeColor="Black" />
                                                <HeaderStyle BackColor="#333333" Font-Bold="True" ForeColor="White" />
                                                <PagerStyle BackColor="White" ForeColor="Black" HorizontalAlign="Right" />
                                                <SelectedRowStyle BackColor="#CC3333" Font-Bold="True" ForeColor="White" />
                                                <SortedAscendingCellStyle BackColor="#F7F7F7" />
                                                <SortedAscendingHeaderStyle BackColor="#4B4B4B" />
                                                <SortedDescendingCellStyle BackColor="#E5E5E5" />
                                                <SortedDescendingHeaderStyle BackColor="#242121" />
                                            </asp:GridView>
                                            <asp:Button ID="btn_DeleteSent" runat="server" Text="Delete" 
                                                onclick="btn_DeleteSent_Click" />
                                            <asp:Button ID="btn_deleteAllSent" runat="server" 
                                                onclick="btn_deleteAllSent_Click" Text="Delete All" />
                                        </asp:View>
                                        <asp:View ID="View17" runat="server">
                                            <br />
                                            Drafts<asp:GridView ID="grid_Drafts" runat="server" AutoGenerateSelectButton="True" 
                                                Style="text-align: center" Width="466px" BackColor="White" 
                                                BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="4" 
                                                ForeColor="Black" GridLines="Horizontal" 
                                                onselectedindexchanged="grid_Drafts_SelectedIndexChanged">
                                                <FooterStyle BackColor="#CCCC99" ForeColor="Black" />
                                                <HeaderStyle BackColor="#333333" Font-Bold="True" ForeColor="White" />
                                                <PagerStyle BackColor="White" ForeColor="Black" HorizontalAlign="Right" />
                                                <SelectedRowStyle BackColor="#CC3333" Font-Bold="True" ForeColor="White" />
                                                <SortedAscendingCellStyle BackColor="#F7F7F7" />
                                                <SortedAscendingHeaderStyle BackColor="#4B4B4B" />
                                                <SortedDescendingCellStyle BackColor="#E5E5E5" />
                                                <SortedDescendingHeaderStyle BackColor="#242121" />
                                            </asp:GridView>
                                            <asp:Button ID="btn_DelDraft" runat="server" Text="Delete" 
                                                onclick="btn_DelDraft_Click" />
                                            <asp:Button ID="btn_DeleteAll" runat="server" onclick="btn_DeleteAll_Click" 
                                                Text="Delete All" />
                                        </asp:View>
                                        <asp:View ID="View19" runat="server">
                                            <asp:GridView ID="grid_Files" runat="server" 
                                                Style="text-align: center" Width="466px" AutoGenerateColumns="False" 
                                                onrowcommand="grid_Files_RowCommand" 
                                                onrowdeleting="grid_Files_RowDeleting" 
                                                onselectedindexchanged="grid_Files_SelectedIndexChanged">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="File">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="LinkButton1" runat="server" 
                                                                CommandArgument='<%# Eval("File") %>' CommandName="Download" 
                                                                Text='<%# Eval("File") %>'></asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="Size" HeaderText="Size(Bytes)" />
                                                    <asp:BoundField DataField="Type" HeaderText="File Type" />
                                                    <asp:TemplateField HeaderText="Delete">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="LinkButton2" runat="server" 
                                                                CommandArgument='<%# Eval("File") %>' CommandName="Delete" 
                                                                oncommand="LinkButton2_Command" Text="Delete File"></asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                        </asp:View>
                                    </asp:MultiView>
                                </div>
                            </td>
                            <td>
                            <asp:Button runat="server" Text="Compose" ID="btn_Compose" 
                                    onclick="btn_Compose_Click" />
                            <asp:Button runat="server" Text="Upload" ID="btn_Upload" 
                                    onclick="btn_Upload_Click" />
                                <asp:MultiView ID="MultiView7" runat="server">
                                    <asp:View ID="View9" runat="server">
                                    <div>
                                        From: Honda Magnacycle Sales Corp.&nbsp;Head Office<br /> To: Honda Philippines Inc.
                                    <table class="style1">
                                        <tr>
                                            <td>
                                                <asp:TextBox ID="txt_MessageContent" runat="server" Height="114px" 
                                                    TextMode="MultiLine" Width="462px"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="text-align: right">
                                                <asp:Button ID="btn_SaveDraft" runat="server" Text="Save as Draft" 
                                                    onclick="btn_SaveDraft_Click" />
                                                <asp:Button ID="btn_Reply" runat="server" Text="Reply" 
                                                    onclick="btn_Reply_Click" />
                                                <asp:Button ID="Button22" runat="server" Text="Update" Visible="False" />
                                                <asp:Button ID="btn_SendMess" runat="server" Text="Send" 
                                                    onclick="btn_SendMess_Click" />
                                            </td>
                                        </tr>
                                    </table>
                                    </div>
                                    </asp:View>
                                    <asp:View ID="View18" runat="server">
                                    <div>
                                        <asp:FileUpload ID="FileUpload1" runat="server" />
                                        <asp:Button ID="btn_Up" runat="server" Text="Upload and Send File" 
                                            onclick="btn_Up_Click" />
                                        </div>
                                    </asp:View>
                                </asp:MultiView>
                            </td>
                        </tr>
                    </table>
            </asp:View>
            <asp:View ID="View10" runat="server">
                <asp:Button ID="btn_Magnacycle" runat="server" Text="Magnacycle" OnClick="btn_Magnacycle_Click" />
                <asp:Button ID="btn_Branch" runat="server" Text="(Branch)" OnClick="btn_Branch_Click" />
                <asp:MultiView ID="MultiView5" runat="server">
                    <asp:View ID="View12" runat="server">
                        <h1>
                            Magnacycle Inventory</h1>
                        <table class="style1">
                            <tr>
                                <td>
                                    Select Category:
                                    <asp:DropDownList ID="ddwn_cats" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddwn_cats_SelectedIndexChanged">
                                        <asp:ListItem>Units</asp:ListItem>
                                        <asp:ListItem>Spare Parts</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Select Status:
                                    <asp:DropDownList ID="ddwn_Stats" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddwn_Stats_SelectedIndexChanged">
                                        <asp:ListItem>(None)</asp:ListItem>
                                        <asp:ListItem>Safety Stocks</asp:ListItem>
                                        <asp:ListItem>Re-order Points</asp:ListItem>
                                        <asp:ListItem>Critical Levels</asp:ListItem>
                                        <asp:ListItem>Out of Stock</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                        </table>
                        <table class="style1">
                            <tr>
                                <td>
                                    <asp:SqlDataSource ID="src_InvUnits" runat="server" ConnectionString="<%$ ConnectionStrings:MagnacycleConnectionString %>"
                                        SelectCommand="SELECT [ModelCode], [Description], [Color], [Quantity], [Status] FROM [SystemUnitsInventoryTBL]">
                                    </asp:SqlDataSource>
                                    <asp:SqlDataSource ID="src_InvParts" runat="server" ConnectionString="<%$ ConnectionStrings:MagnacycleConnectionString %>"
                                        SelectCommand="SELECT [PartNumber], [Description], [Color], [Quantity], [Status] FROM [SystemPartsInventoryTBL]">
                                    </asp:SqlDataSource>
                                    <asp:UpdatePanel ID="UpdatePanel6" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <asp:Timer ID="Timer6" runat="server" Interval="1000" OnTick="Timer6_Tick">
                                            </asp:Timer>
                                            <asp:GridView ID="grid_Inv" runat="server" BackColor="White" BorderColor="#DEDFDE"
                                                BorderStyle="None" BorderWidth="1px" CellPadding="4" ForeColor="Black" GridLines="Vertical"
                                                Style="text-align: center" Width="749px">
                                                <AlternatingRowStyle BackColor="White" />
                                                <FooterStyle BackColor="#CCCC99" />
                                                <HeaderStyle BackColor="#6B696B" Font-Bold="True" ForeColor="White" />
                                                <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Right" />
                                                <RowStyle BackColor="#F7F7DE" />
                                                <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
                                                <SortedAscendingCellStyle BackColor="#FBFBF2" />
                                                <SortedAscendingHeaderStyle BackColor="#848384" />
                                                <SortedDescendingCellStyle BackColor="#EAEAD3" />
                                                <SortedDescendingHeaderStyle BackColor="#575357" />
                                            </asp:GridView>
                                        </ContentTemplate>
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger ControlID="Timer6" EventName="Tick" />
                                        </Triggers>
                                    </asp:UpdatePanel>
                                </td>
                            </tr>
                        </table>
                    </asp:View>
                    <asp:View ID="View13" runat="server">
                        <h1>
                            Branch Inventory</h1>
                        <table class="style1">
                            <tr>
                                <td>
                                    Select Category:
                                    <asp:DropDownList ID="ddwn_Catss" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged">
                                        <asp:ListItem>Units</asp:ListItem>
                                        <asp:ListItem>Spare Parts</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Select Status:
                                    <asp:DropDownList ID="ddwn_Statss" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddwn_Statss_SelectedIndexChanged">
                                        <asp:ListItem>(None)</asp:ListItem>
                                        <asp:ListItem>Safety Stocks</asp:ListItem>
                                        <asp:ListItem>Re-order Points</asp:ListItem>
                                        <asp:ListItem>Critical Levels</asp:ListItem>
                                        <asp:ListItem>Out of Stock</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                        </table>
                        <table class="style1">
                            <tr>
                                <td>
                                    <asp:SqlDataSource ID="src_InvBunits" runat="server" ConnectionString="<%$ ConnectionStrings:MagnaAdministratorConnectionString %>"
                                        SelectCommand="SELECT [ModelCode], [Description], [Color], [Quantity], [Status] FROM [BranchUnitsInventoryTBL]">
                                    </asp:SqlDataSource>
                                    <asp:SqlDataSource ID="src_InvBparts" runat="server" ConnectionString="<%$ ConnectionStrings:MagnaAdministratorConnectionString %>"
                                        SelectCommand="SELECT [PartNumber], [Description], [Color], [Quantity], [Status] FROM [BranchPartsInventoryTBL]">
                                    </asp:SqlDataSource>
                                    <asp:GridView ID="grid_BInv" runat="server" BackColor="White" BorderColor="#DEDFDE"
                                        BorderStyle="None" BorderWidth="1px" CellPadding="4" ForeColor="Black" GridLines="Vertical"
                                        Style="text-align: center" Width="749px">
                                        <AlternatingRowStyle BackColor="White" />
                                        <FooterStyle BackColor="#CCCC99" />
                                        <HeaderStyle BackColor="#6B696B" Font-Bold="True" ForeColor="White" />
                                        <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Right" />
                                        <RowStyle BackColor="#F7F7DE" />
                                        <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
                                        <SortedAscendingCellStyle BackColor="#FBFBF2" />
                                        <SortedAscendingHeaderStyle BackColor="#848384" />
                                        <SortedDescendingCellStyle BackColor="#EAEAD3" />
                                        <SortedDescendingHeaderStyle BackColor="#575357" />
                                    </asp:GridView>
                                </td>
                            </tr>
                        </table>
                    </asp:View>
                </asp:MultiView>
            </asp:View>
            <asp:View ID="View14" runat="server">
                <h1>
                    Stock Report</h1>
                <div style="text-align: right">
                    <asp:Button ID="btn_Backs" runat="server" Text="Back" OnClick="btn_Backs_Click" />
                </div>
                <table class="style1">
                    <tr>
                        <td>
                            P.O. Number:
                            <asp:Label ID="lbl_PONum" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:GridView ID="grid_StockReport1" runat="server" BackColor="White" BorderColor="#DEDFDE"
                                BorderStyle="None" BorderWidth="1px" CellPadding="4" ForeColor="Black" GridLines="Vertical"
                                Style="text-align: center" Width="747px">
                                <AlternatingRowStyle BackColor="White" />
                                <FooterStyle BackColor="#CCCC99" />
                                <HeaderStyle BackColor="#6B696B" Font-Bold="True" ForeColor="White" />
                                <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Right" />
                                <RowStyle BackColor="#F7F7DE" />
                                <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
                                <SortedAscendingCellStyle BackColor="#FBFBF2" />
                                <SortedAscendingHeaderStyle BackColor="#848384" />
                                <SortedDescendingCellStyle BackColor="#EAEAD3" />
                                <SortedDescendingHeaderStyle BackColor="#575357" />
                            </asp:GridView>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lbl_SecStockReport" runat="server" Text="Secondary P.O. Stock Report"
                                Visible="False"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:GridView ID="grid_StockReport2" runat="server" BackColor="White" BorderColor="#DEDFDE"
                                BorderStyle="None" BorderWidth="1px" CellPadding="4" ForeColor="Black" GridLines="Vertical"
                                Style="text-align: center" Width="754px">
                                <AlternatingRowStyle BackColor="White" />
                                <FooterStyle BackColor="#CCCC99" />
                                <HeaderStyle BackColor="#6B696B" Font-Bold="True" ForeColor="White" />
                                <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Right" />
                                <RowStyle BackColor="#F7F7DE" />
                                <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
                                <SortedAscendingCellStyle BackColor="#FBFBF2" />
                                <SortedAscendingHeaderStyle BackColor="#848384" />
                                <SortedDescendingCellStyle BackColor="#EAEAD3" />
                                <SortedDescendingHeaderStyle BackColor="#575357" />
                            </asp:GridView>
                        </td>
                    </tr>
                </table>
            </asp:View>
            <asp:View ID="View20" runat="server">
                <asp:Button ID="Button23" runat="server" Text="Order Report" />
                <asp:Button ID="Button24" runat="server" Text="Sales Invoice" />
                <asp:Button ID="Button25" runat="server" Text="Billing Report" />
                <asp:Button ID="Button26" runat="server" Text="Delivery Report" />
                <asp:MultiView ID="MultiView8" runat="server">
                    <asp:View ID="View21" runat="server">
                        <h1>Order Report</h1>
                        <table class="style1">
                            <tr>
                                <td>
                                    P.O. Number:
                                    <asp:Label ID="Label4" runat="server" Text="Label"></asp:Label>
                                    &nbsp;&nbsp;&nbsp; Date:
                                    <asp:Label ID="Label5" runat="server" Text="Label"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:GridView ID="GridView1" runat="server" style="text-align: center" 
                                        Width="727px">
                                    </asp:GridView>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    &nbsp;</td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="Label6" runat="server" Text="Label"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:GridView ID="GridView9" runat="server" style="text-align: center" 
                                        Width="727px">
                                    </asp:GridView>
                                </td>
                            </tr>
                            </table>
                    </asp:View>
                    <asp:View ID="View22" runat="server">
                        <h1>Sales Invoice</h1>
                            <asp:MultiView ID="MultiView9" runat="server">
                            <asp:View ID="View25" runat="server">
                                <asp:GridView ID="GridView14" runat="server" AutoGenerateSelectButton="True" 
                                    style="text-align: center" Width="727px">
                                </asp:GridView>
                            </asp:View>
                            <asp:View ID="View26" runat="server">
                                <table class="style1">
                                    <tr>
                                        <td>
                                            P.O. Number:
                                            <asp:Label ID="Label7" runat="server" Text="Label"></asp:Label>
                                        </td>
                                        <td>
                                            Invoice Number:
                                            <asp:Label ID="Label9" runat="server" Text="Label"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            Date:
                                            <asp:Label ID="Label8" runat="server" Text="Label"></asp:Label>
                                        </td>
                                        <td>
                                            Delivery Date:
                                            <asp:Label ID="Label10" runat="server" Text="Label"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2">
                                            <asp:GridView ID="GridView10" runat="server" style="text-align: center" 
                                                Width="727px">
                                            </asp:GridView>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2">
                                            &nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td colspan="2">
                                            <asp:Label ID="Label11" runat="server" Text="Label"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2">
                                            <asp:GridView ID="GridView11" runat="server" style="text-align: center" 
                                                Width="727px">
                                            </asp:GridView>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            Sub-Total:
                                            <asp:Label ID="Label24" runat="server" Text="Label"></asp:Label>
                                        </td>
                                        <td>
                                            Service Charge:
                                            <asp:Label ID="Label27" runat="server" Text="Label"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            Discount:
                                            <asp:Label ID="Label25" runat="server" Text="Label"></asp:Label>
                                        </td>
                                        <td>
                                            Total Service Charge:
                                            <asp:Label ID="Label28" runat="server" Text="Label"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            Discounted Amount:
                                            <asp:Label ID="Label26" runat="server" Text="Label"></asp:Label>
                                        </td>
                                        <td>
                                            Total:
                                            <asp:Label ID="Label29" runat="server" Text="Label"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            VAT:
                                            <asp:Label ID="Label12" runat="server" Text="Label"></asp:Label>
                                            &nbsp;&nbsp;&nbsp;
                                            <asp:Label ID="Label13" runat="server" Text="Label"></asp:Label>
                                        </td>
                                        <td>
                                            &nbsp;</td>
                                    </tr>
                                </table>
                            </asp:View>
                        </asp:MultiView>
                    </asp:View>
                    <asp:View ID="View23" runat="server">
                        <h1>Billing Report</h1>
                            <asp:MultiView ID="MultiView10" runat="server">
                            <asp:View ID="View27" runat="server">
                                <asp:GridView ID="GridView15" runat="server" AutoGenerateSelectButton="True" 
                                    style="text-align: center" Width="727px">
                                </asp:GridView>
                            </asp:View>
                            <asp:View ID="View28" runat="server">
                                <asp:Button ID="Button27" runat="server" Text="Button" />
                                <asp:Button ID="Button28" runat="server" Text="Button" />
                                <asp:MultiView ID="MultiView11" runat="server">
                                    <asp:View ID="View29" runat="server">
                                    <table class="style1">
                                            <tr>
                                                <td>
                                                    P.O. Number: </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    Date: </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    Invoice Number: </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:GridView ID="GridView16" runat="server" style="text-align: center" 
                                                        Width="727px">
                                                    </asp:GridView>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    &nbsp;</td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="Label30" runat="server" Text="Label"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:GridView ID="GridView17" runat="server" style="text-align: center" 
                                                        Width="727px">
                                                    </asp:GridView>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="style62">
                                                    Sub-Total:
                                                    <asp:Label ID="Label31" runat="server" Text="Label"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="style62">
                                                    Tax:
                                                    <asp:Label ID="Label32" runat="server" Text="Label"></asp:Label>
                                                    &nbsp;&nbsp;&nbsp;&nbsp;
                                                    <asp:Label ID="Label33" runat="server" Text="Label"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="style62">
                                                    Shipping:
                                                    <asp:Label ID="Label34" runat="server" Text="Label"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="style62">
                                                    Total:
                                                    <asp:Label ID="Label35" runat="server" Text="Label"></asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                    </asp:View>
                                    <asp:View ID="View30" runat="server">
                                        <table class="style1">
                                            <tr>
                                                <td>
                                                    <table class="style1">
                                                        <tr>
                                                            <td class="style63">
                                                                Purchase Order Number:</td>
                                                            <td>
                                                                <asp:Label ID="Label36" runat="server"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="style63">
                                                                Date Received:</td>
                                                            <td>
                                                                <asp:Label ID="lbl_DateRec" runat="server"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="style63">
                                                                Invoice Number:</td>
                                                            <td>
                                                                <asp:Label ID="lbl_InvNum" runat="server"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="style63">
                                                                Total Bill:</td>
                                                            <td>
                                                                <asp:Label ID="lbl_TotalBill" runat="server"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="style63">
                                                                Amount Paid:</td>
                                                            <td>
                                                                <asp:Label ID="lbl_AmountPaid" runat="server"></asp:Label>
                                                                &nbsp;</td>
                                                        </tr>
                                                        <tr>
                                                            <td class="style63">
                                                                Remarks:</td>
                                                            <td>
                                                                <asp:Label ID="lbl_Remarks" runat="server"></asp:Label>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                        </table>
                                    </asp:View>
                                </asp:MultiView>
                            </asp:View>
                        </asp:MultiView>
                    </asp:View>
                    <asp:View ID="View24" runat="server">
                        <h1>Delivery Report</h1>
                            <asp:MultiView ID="MultiView12" runat="server">
                            <asp:View ID="View31" runat="server">
                                <asp:GridView ID="GridView6" runat="server" AutoGenerateSelectButton="True" 
                                    style="text-align: center" Width="727px">
                                </asp:GridView>
                            </asp:View>
                            <asp:View ID="View32" runat="server">
                                <table class="style1">
                                    <tr>
                                        <td>
                                            P.O. Number:
                                            <asp:Label ID="Label20" runat="server" Text="Label"></asp:Label>
                                        </td>
                                        <td>
                                            Delivery Date:
                                            <asp:Label ID="Label22" runat="server" Text="Label"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            Date:
                                            <asp:Label ID="Label21" runat="server" Text="Label"></asp:Label>
                                        </td>
                                        <td>
                                            Deivery Number:
                                            <asp:Label ID="Label23" runat="server" Text="Label"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2">
                                            <asp:GridView ID="GridView12" runat="server" style="text-align: center" 
                                                Width="727px">
                                            </asp:GridView>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2">
                                            &nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td colspan="2">
                                            <asp:Label ID="Label14" runat="server" Text="Label"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2">
                                            <asp:GridView ID="GridView13" runat="server" style="text-align: center" 
                                                Width="727px">
                                            </asp:GridView>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2">
                                            <asp:Label ID="Label15" runat="server" Text="Label"></asp:Label>
                                            <br />
                                            <asp:Label ID="Label16" runat="server" Text="Label"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2">
                                            Hauler:
                                            <asp:Label ID="Label17" runat="server" Text="Label"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2">
                                            Plate Number:
                                            <asp:Label ID="Label18" runat="server" Text="Label"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2">
                                            Gate Pass Number:
                                            <asp:Label ID="Label19" runat="server" Text="Label"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </asp:View>
                        </asp:MultiView>
                    </asp:View>
                </asp:MultiView>
            </asp:View>
        </asp:MultiView>
    </div>
    </form>
</body>
</html>
