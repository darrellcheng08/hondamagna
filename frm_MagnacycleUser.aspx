<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="frm_MagnacycleUser.aspx.cs" Inherits="HondaMagnacycleProcurementProject.MagnacycleUser" %>

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
            height: 397px;
        }
        .style4
        {
            text-align: right;
            }
        .style5
        {
            text-align: right;
            width: 177px;
            height: 23px;
        }
        .style6
        {
            height: 23px;
        }
        .style8
        {
            width: 193px;
        }
    </style>
</head>
<body>

    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="True" EnableScriptGlobalization="True">
    </asp:ScriptManager>
    <div>
        <div>
            <table class="style1">
                <tr>
                    <td>
                        <asp:Button ID="btn_PurchaseOrderInfo" runat="server" 
                            Text="Purchase Order Info" onclick="btn_PurchaseOrderInfo_Click" />
                        <asp:Button ID="btn_Inventory" runat="server" onclick="btn_Inventory_Click" 
                            Text="Inventory" />
                    </td>
                    <td style="text-align: right">
                        <asp:Button ID="btn_LogInSettings" runat="server" Text="Log In settings" 
                            Width="101px" onclick="btn_LogInSettings_Click" />
                        <asp:Button ID="btn_SignOut" runat="server" 
                            Text="Sign Out" Width="70px" onclick="btn_SignOut_Click" />
                    </td>
                </tr>
            </table>
        </div>
        <asp:MultiView ID="MultiView3" runat="server">
            <asp:View ID="View6" runat="server">
                <div>
                    <table class="style1">
                        <tr>
                            <td>
                                <asp:Button ID="btn_UnitsPurchaseOrder" runat="server" 
                                    Text="Units Purchase Order" onclick="btn_UnitsPurchaseOrder_Click" />
                                <asp:Button ID="btn_SparePartsPurchaseOrder" runat="server" Text="Spare Parts Purchase Order" />
                            </td>
                            <td>
                                <asp:Button ID="btn_AvailableUnitsSpareParts" runat="server" Text="Available Units/Spare Parts"
                                    OnClick="btn_AvailableUnitsSpareParts_Click" />
                                <asp:Button ID="btn_PurchaseOrderDetails" runat="server" Text="Purchase Order Details"
                                    OnClick="btn_PurchaseOrderDetails_Click" />
                            </td>
                        </tr>
                        <tr>
                            <td class="style2">
                                <asp:MultiView ID="MultiView1" runat="server" ActiveViewIndex="0">
                                    <asp:View ID="View1" runat="server">
                                        Units Purchase Order<asp:GridView ID="grid_UnitsPurchaseOrder" runat="server" AutoGenerateSelectButton="True"
                                            Style="text-align: center" Width="428px" BackColor="White" BorderColor="#CC9966"
                                            BorderStyle="None" BorderWidth="1px" CellPadding="4" OnSelectedIndexChanged="grid_UnitsPurchaseOrder_SelectedIndexChanged"
                                            OnRowDeleted="grid_UnitsPurchaseOrder_RowDeleted" OnRowDeleting="grid_UnitsPurchaseOrder_RowDeleting"
                                            OnRowCreated="grid_UnitsPurchaseOrder_RowCreated" OnRowEditing="grid_UnitsPurchaseOrder_RowEditing">
                                            <FooterStyle BackColor="#FFFFCC" ForeColor="#330099" />
                                            <HeaderStyle BackColor="#990000" Font-Bold="True" ForeColor="#FFFFCC" />
                                            <PagerStyle BackColor="#FFFFCC" ForeColor="#330099" HorizontalAlign="Center" />
                                            <RowStyle ForeColor="#330099" BackColor="White" />
                                            <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="#663399" />
                                            <SortedAscendingCellStyle BackColor="#FEFCEB" />
                                            <SortedAscendingHeaderStyle BackColor="#AF0101" />
                                            <SortedDescendingCellStyle BackColor="#F6F0C0" />
                                            <SortedDescendingHeaderStyle BackColor="#7E0000" />
                                        </asp:GridView>
                                    </asp:View>
                                    <asp:View ID="View2" runat="server">
                                        Spare Parts Order Units<asp:GridView ID="grid_SparePartsPurchaseOrder" runat="server"
                                            AutoGenerateSelectButton="True" Style="text-align: center" Width="430px">
                                        </asp:GridView>
                                    </asp:View>
                                </asp:MultiView>
                                <div>
                                    <table class="style1">
                                        <tr>
                                            <td>
                                                <asp:Button ID="btn_Remove" runat="server" Text="Remove" OnClick="btn_Remove_Click" />
                                            </td>
                                            <td style="text-align: right">
                                                <asp:Label ID="lbl_POrefNumber" runat="server" Text="P.O. Number:"></asp:Label>
                                                <asp:TextBox ID="txt_POrefNumber" runat="server" ReadOnly="True"></asp:TextBox>
                                                <asp:Label ID="lbl_DateofPO" runat="server" Text="Date of PO:" Visible="False"></asp:Label>
                                                <asp:TextBox ID="txt_DateofPO" runat="server" ReadOnly="True" Visible="False"></asp:TextBox>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                                <div style="text-align: right">
                                    <asp:Button ID="btn_DontSavePO" runat="server" Text="Don't Save PO" OnClick="btn_DontSavePO_Click" />
                                    <asp:Button ID="btn_SavePO" runat="server" Text="Save PO" OnClick="btn_SavePO_Click" />
                                </div>
                            </td>
                            <td class="style2">
                                <asp:MultiView ID="MultiView2" runat="server" ActiveViewIndex="0">
                                    <asp:View ID="View3" runat="server">
                                        <div>
                                            <asp:Label ID="lbl_tab1_Select" runat="server" Text="Select:"></asp:Label>
                                            <asp:DropDownList ID="ddwn_tab1_Select" runat="server" AutoPostBack="True">
                                                <asp:ListItem>Units</asp:ListItem>
                                                <asp:ListItem>Spare Parts</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:TextBox ID="txt_ModelSparePartsName" runat="server" Width="350px">Model or Spare Parts Name</asp:TextBox>
                                            <div>
                                                <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                                                    <ContentTemplate>
                                                        <asp:Timer ID="Timer1" runat="server" Interval="1000" OnTick="Timer1_Tick">
                                                        </asp:Timer>
                                                        <asp:GridView ID="grid_AvialableUnitsSpareParts" runat="server" AutoGenerateSelectButton="True"
                                                            BackColor="White" BorderColor="#CC9966" BorderStyle="None" BorderWidth="1px"
                                                            CellPadding="4" OnSelectedIndexChanged="grid_AvialableUnitsSpareParts_SelectedIndexChanged"
                                                            Style="text-align: center" Width="493px">
                                                            <FooterStyle BackColor="#FFFFCC" ForeColor="#330099" />
                                                            <HeaderStyle BackColor="#990000" Font-Bold="True" ForeColor="#FFFFCC" />
                                                            <PagerStyle BackColor="#FFFFCC" ForeColor="#330099" HorizontalAlign="Center" />
                                                            <RowStyle ForeColor="#330099" BackColor="White" />
                                                            <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="#663399" />
                                                            <SortedAscendingCellStyle BackColor="#FEFCEB" />
                                                            <SortedAscendingHeaderStyle BackColor="#AF0101" />
                                                            <SortedDescendingCellStyle BackColor="#F6F0C0" />
                                                            <SortedDescendingHeaderStyle BackColor="#7E0000" />
                                                        </asp:GridView>
                                                    </ContentTemplate>
                                                    <Triggers>
                                                        <asp:AsyncPostBackTrigger ControlID="Timer1" EventName="Tick" />
                                                    </Triggers>
                                                </asp:UpdatePanel>
                                            </div>
                                            <div style="font-weight: 700">
                                                <asp:Label ID="lbl_tab1_Quantity" runat="server" Font-Bold="False" Text="Quantity:"></asp:Label>
                                                <asp:TextBox ID="txt_tab1_Quantity" runat="server" MaxLength="3" Width="61px"></asp:TextBox>
                                                <asp:Label ID="lbl_Available" runat="server" Font-Bold="False" Text="Available:"
                                                    Visible="False"></asp:Label>
                                                <asp:DropDownList ID="ddwn_Available" runat="server" Visible="False">
                                                </asp:DropDownList>
                                                <asp:Button ID="btn_AddUnitsParts" runat="server" OnClick="btn_AddUnitsParts_Click"
                                                    Text="Add" Width="64px" />
                                                <asp:SqlDataSource ID="src_AvailParts" runat="server" 
                                                    ConnectionString="<%$ ConnectionStrings:HPILogInConnectionString %>" 
                                                    SelectCommand="SELECT [PartNumber], [Description], [Color] FROM [SystemPartsTBL] WHERE ([Quantity] &gt; @Quantity)">
                                                    <SelectParameters>
                                                        <asp:Parameter DefaultValue="0" Name="Quantity" Type="String" />
                                                    </SelectParameters>
                                                </asp:SqlDataSource>
                                                <asp:SqlDataSource ID="src_AvailUnits" runat="server" 
                                                    ConnectionString="<%$ ConnectionStrings:HPILogInConnectionString %>" 
                                                    SelectCommand="SELECT [ModelCode], [Description], [Color] FROM [SystemModelsTBL] WHERE ([Quantity] &gt; @Quantity)">
                                                    <SelectParameters>
                                                        <asp:Parameter DefaultValue="0" Name="Quantity" Type="String" />
                                                    </SelectParameters>
                                                </asp:SqlDataSource>
                                            </div>
                                        </div>
                                    </asp:View>
                                    <asp:View ID="View4" runat="server">
                                        <div>
                                            <asp:Label ID="lbl_tab2_Select" runat="server" Text="Select:"></asp:Label>
                                            <asp:DropDownList ID="ddwn_tab2_Select" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddwn_tab2_Select_SelectedIndexChanged">
                                                <asp:ListItem>Units</asp:ListItem>
                                                <asp:ListItem>Spare Parts</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:Label ID="lbl_tab2_SearchBy" runat="server" Text="Search by:"></asp:Label>
                                            <asp:DropDownList ID="ddwn_SearchBy" runat="server" Height="18px" Width="202px">
                                                <asp:ListItem>PO Reference Number</asp:ListItem>
                                                <asp:ListItem>Date</asp:ListItem>
                                            </asp:DropDownList>
                                            <div style="text-align: right">
                                                <asp:Label ID="lbl_PORefNo" runat="server" Font-Bold="False" Text="P.O. Number:"
                                                    Visible="False"></asp:Label>
                                                <asp:TextBox ID="txt_PONumber" runat="server" ReadOnly="True" Visible="False"></asp:TextBox>
                                                <div style="text-align: left">
                                                    <asp:MultiView ID="MultiView4" runat="server">
                                                        <asp:View ID="View8" runat="server">
                                                            <div style="text-align: right">
                                                                <asp:GridView ID="grid_PurchaseOrderDetails" runat="server" AutoGenerateColumns="False"
                                                                    AutoGenerateSelectButton="True" BackColor="White" BorderColor="#CC9966" BorderStyle="None"
                                                                    BorderWidth="1px" CellPadding="4" DataSourceID="src_SavedUnits" OnSelectedIndexChanged="grid_PurchaseOrderDetails_SelectedIndexChanged"
                                                                    Style="text-align: center" Width="492px">
                                                                    <Columns>
                                                                        <asp:BoundField DataField="PurchaseOrderNumber" HeaderText="PurchaseOrderNumber"
                                                                            SortExpression="PurchaseOrderNumber" />
                                                                        <asp:BoundField DataField="DateCreated" HeaderText="DateCreated" SortExpression="DateCreated" />
                                                                        <asp:BoundField DataField="DateSent" HeaderText="DateSent" SortExpression="DateSent" />
                                                                    </Columns>
                                                                    <FooterStyle BackColor="#FFFFCC" ForeColor="#330099" />
                                                                    <HeaderStyle BackColor="#990000" Font-Bold="True" ForeColor="#FFFFCC" />
                                                                    <PagerStyle BackColor="#FFFFCC" ForeColor="#330099" HorizontalAlign="Center" />
                                                                    <RowStyle BackColor="White" ForeColor="#330099" />
                                                                    <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="#663399" />
                                                                    <SortedAscendingCellStyle BackColor="#FEFCEB" />
                                                                    <SortedAscendingHeaderStyle BackColor="#AF0101" />
                                                                    <SortedDescendingCellStyle BackColor="#F6F0C0" />
                                                                    <SortedDescendingHeaderStyle BackColor="#7E0000" />
                                                                </asp:GridView>
                                                                <asp:Button ID="btn_DeletePO" runat="server" OnClick="btn_DeletePO_Click" Style="text-align: right"
                                                                    Text="Delete P.O." />
                                                            </div>
                                                        </asp:View>
                                                        <asp:View ID="View9" runat="server">
                                                            <div style="text-align: right">
                                                                <asp:Button ID="btn_Back" runat="server" OnClick="btn_Back_Click" Text="Back" />
                                                            </div>
                                                            <asp:GridView ID="grid_PurchaseOrderDetails1" runat="server" AutoGenerateSelectButton="True"
                                                                BackColor="White" BorderColor="#CC9966" BorderStyle="None" BorderWidth="1px"
                                                                CellPadding="4" OnSelectedIndexChanged="grid_PurchaseOrderDetails1_SelectedIndexChanged"
                                                                Style="text-align: center" Width="493px">
                                                                <FooterStyle BackColor="#FFFFCC" ForeColor="#330099" />
                                                                <HeaderStyle BackColor="#990000" Font-Bold="True" ForeColor="#FFFFCC" />
                                                                <PagerStyle BackColor="#FFFFCC" ForeColor="#330099" HorizontalAlign="Center" />
                                                                <RowStyle BackColor="White" ForeColor="#330099" />
                                                                <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="#663399" />
                                                                <SortedAscendingCellStyle BackColor="#FEFCEB" />
                                                                <SortedAscendingHeaderStyle BackColor="#AF0101" />
                                                                <SortedDescendingCellStyle BackColor="#F6F0C0" />
                                                                <SortedDescendingHeaderStyle BackColor="#7E0000" />
                                                            </asp:GridView>
                                                            <asp:Label ID="lbl_tab2_Quantity" runat="server" Font-Bold="False" Text="Quantity:"
                                                                Visible="False"></asp:Label>
                                                            <asp:TextBox ID="txt_tab2_Quantity" runat="server" MaxLength="3" Visible="False"
                                                                Width="61px"></asp:TextBox>
                                                            <asp:Label ID="lbl_tab2_Color" runat="server" Font-Bold="False" Text="Color:" Visible="False"></asp:Label>
                                                            <asp:DropDownList ID="ddwn_Colors" runat="server" Visible="False">
                                                            </asp:DropDownList>
                                                            <asp:Button ID="btn_SaveEdit" runat="server" Text="Save" Visible="False" OnClick="btn_SaveEdit_Click" />
                                                            <asp:Button ID="bnt_CancelEdit" runat="server" Text="Cancel" Visible="False" OnClick="bnt_CancelEdit_Click" />
                                                            <div style="text-align: right">
                                                                <asp:Button ID="btn_Reports" runat="server" OnClick="btn_Reports_Click" 
                                                                    Text="View Reports" />
                                                                <asp:Button ID="btn_recreatePO" runat="server" Text="Recreate" OnClick="btn_recreatePO_Click" />
                                                                <asp:Button ID="btn_ViewAll" runat="server" Text="View All" Visible="False" />
                                                                <asp:Button ID="btn_EditItems" runat="server" Height="26px" Text="Edit Items" OnClick="btn_EditItems_Click" />
                                                                <asp:Button ID="btn_EditPO" runat="server" Text="Edit" OnClick="btn_EditPO_Click" />
                                                                <asp:Button ID="btn_RemovePO" runat="server" Text="Remove" OnClick="btn_RemovePO_Click" />
                                                                <asp:Button ID="btn_SendPO" runat="server" Text="Send" OnClick="btn_SendPO_Click" />
                                                            </div>
                                                        </asp:View>
                                                        <asp:View ID="View10" runat="server">
                                                            <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" AutoGenerateSelectButton="True"
                                                                BackColor="White" BorderColor="#CC9966" BorderStyle="None" BorderWidth="1px"
                                                                CellPadding="4" DataSourceID="src_SavedParts" Style="text-align: center" Width="493px">
                                                                <Columns>
                                                                    <asp:BoundField DataField="PurchaseOrderNumber" HeaderText="PurchaseOrderNumber"
                                                                        SortExpression="PurchaseOrderNumber" />
                                                                    <asp:BoundField DataField="DateCreated" HeaderText="DateCreated" SortExpression="DateCreated" />
                                                                    <asp:BoundField DataField="DateSent" HeaderText="DateSent" SortExpression="DateSent" />
                                                                </Columns>
                                                                <FooterStyle BackColor="#FFFFCC" ForeColor="#330099" />
                                                                <HeaderStyle BackColor="#990000" Font-Bold="True" ForeColor="#FFFFCC" />
                                                                <PagerStyle BackColor="#FFFFCC" ForeColor="#330099" HorizontalAlign="Center" />
                                                                <RowStyle BackColor="White" ForeColor="#330099" />
                                                                <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="#663399" />
                                                                <SortedAscendingCellStyle BackColor="#FEFCEB" />
                                                                <SortedAscendingHeaderStyle BackColor="#AF0101" />
                                                                <SortedDescendingCellStyle BackColor="#F6F0C0" />
                                                                <SortedDescendingHeaderStyle BackColor="#7E0000" />
                                                            </asp:GridView>
                                                        </asp:View>
                                                        <asp:View ID="View11" runat="server">
                                                            <asp:GridView ID="GridView3" runat="server" AutoGenerateSelectButton="True" BackColor="White"
                                                                BorderColor="#CC9966" BorderStyle="None" BorderWidth="1px" CellPadding="4" Style="text-align: center"
                                                                Width="492px">
                                                                <FooterStyle BackColor="#FFFFCC" ForeColor="#330099" />
                                                                <HeaderStyle BackColor="#990000" Font-Bold="True" ForeColor="#FFFFCC" />
                                                                <PagerStyle BackColor="#FFFFCC" ForeColor="#330099" HorizontalAlign="Center" />
                                                                <RowStyle BackColor="White" ForeColor="#330099" />
                                                                <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="#663399" />
                                                                <SortedAscendingCellStyle BackColor="#FEFCEB" />
                                                                <SortedAscendingHeaderStyle BackColor="#AF0101" />
                                                                <SortedDescendingCellStyle BackColor="#F6F0C0" />
                                                                <SortedDescendingHeaderStyle BackColor="#7E0000" />
                                                            </asp:GridView>
                                                        </asp:View>
                                                    </asp:MultiView>
                                                    <asp:SqlDataSource ID="src_SavedUnits" runat="server" ConnectionString="<%$ ConnectionStrings:MagnacycleConnectionString %>"
                                                        SelectCommand="SELECT * FROM [SystemUnitsPONTBL]"></asp:SqlDataSource>
                                                    <asp:SqlDataSource ID="src_SavedParts" runat="server" ConnectionString="<%$ ConnectionStrings:MagnacycleConnectionString %>"
                                                        SelectCommand="SELECT * FROM [SystemPartsPONTBL]"></asp:SqlDataSource>
                                                </div>
                                            </div>
                                        </div>
                                    </asp:View>
                                    <asp:View ID="View5" runat="server">
                                        <asp:Button ID="btn_OrderReps" runat="server" Text="Order Report" 
                                            onclick="btn_OrderReps_Click" />
                                        <asp:Button ID="btn_SIReps" runat="server" Text="Sales Invoice" 
                                            onclick="btn_SIReps_Click" />
                                        <asp:Button ID="btn_DelReps" runat="server" Text="Delivery Report" 
                                            onclick="btn_DelReps_Click" />
                                        <asp:MultiView ID="MultiView5" runat="server">
                                            <asp:View ID="View12" runat="server">
                                            <h1>Order Reports</h1>
                                            P.O. Number: 
                                                <asp:TextBox ID="txt_POnums" runat="server" ReadOnly="True"></asp:TextBox>
                                                Date: 
                                                <asp:TextBox ID="txt_Date" runat="server" ReadOnly="True"></asp:TextBox>
                                                <asp:GridView ID="grid_OrderReport1" runat="server" style="text-align: center">
                                                </asp:GridView>
                                                <table class="style1">
                                                    <tr>
                                                        <td>
                                                            &nbsp;</td>
                                                    </tr>
                                                </table>
                                                <asp:Label ID="lbl_ads" runat="server" Text="Added Item/s to this P.O." 
                                                    Visible="False"></asp:Label>
                                                <asp:GridView ID="grid_OrderReport2" runat="server" style="text-align: center">
                                                </asp:GridView>
                                            </asp:View>
                                            <asp:View ID="View13" runat="server">
                                            <h1>Sales Invoice</h1>
                                                <asp:MultiView ID="MultiView6" runat="server">
                                                    <asp:View ID="View16" runat="server">
                                                        <asp:GridView ID="grid_Userinvoices" runat="server" 
                                                            AutoGenerateSelectButton="True" 
                                                            onselectedindexchanged="grid_Userinvoices_SelectedIndexChanged" 
                                                            style="text-align: center">
                                                        </asp:GridView>
                                                    </asp:View>
                                                    <asp:View ID="View17" runat="server">

                                                        <table class="style1">
                                                            <tr>
                                                                <td>
                                                                    P.O. Number:
                                                                    <asp:Label ID="lbl_POnum" runat="server"></asp:Label>
                                                                </td>
                                                                <td>
                                                                    Invoice Number:
                                                                    <asp:Label ID="lbl_InvNum" runat="server"></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    Date:
                                                                    <asp:Label ID="lbl_Date" runat="server"></asp:Label>
                                                                </td>
                                                                <td>
                                                                    Delivery Date:
                                                                    <asp:Label ID="lbl_DeliveryDate" runat="server"></asp:Label>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                        <table class="style1">
                                                            <tr>
                                                                <td colspan="2">
                                                                    <asp:GridView ID="grid_aSI" runat="server" style="text-align: center">
                                                                    </asp:GridView>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td colspan="2">
                                                                    &nbsp;</td>
                                                            </tr>
                                                            <tr>
                                                                <td colspan="2">
                                                                    <asp:Label ID="lbl_AddedItems" runat="server" Text="Added Item/s to this P.O."></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td colspan="2">
                                                                    <asp:GridView ID="grid_bSI" runat="server" style="text-align: center">
                                                                    </asp:GridView>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    Sub-Total:
                                                                    <asp:Label ID="lbl_ST" runat="server"></asp:Label>
                                                                </td>
                                                                <td>
                                                                    Service Charge:
                                                                    <asp:Label ID="lbl_SC" runat="server"></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    Discount:
                                                                    <asp:Label ID="lbl_D" runat="server"></asp:Label>
                                                                </td>
                                                                <td>
                                                                    Total Service Charge:
                                                                    <asp:Label ID="lbl_TSC" runat="server"></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    Discounted Amount:
                                                                    <asp:Label ID="lbl_DA" runat="server"></asp:Label>
                                                                </td>
                                                                <td>
                                                                    Total:
                                                                    <asp:Label ID="lbl_T" runat="server"></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    VAT:
                                                                    <asp:Label ID="lbl_Percentage" runat="server"></asp:Label>
                                                                    &nbsp;&nbsp;&nbsp;
                                                                    <asp:Label ID="lbl_VAT" runat="server"></asp:Label>
                                                                </td>
                                                                <td>
                                                                    &nbsp;</td>
                                                            </tr>
                                                        </table>

                                                    </asp:View>
                                                </asp:MultiView>
                                            </asp:View>
                                            <asp:View ID="View14" runat="server">
                                            <h1>Delivery Acceptance</h1>
                                                <asp:MultiView ID="MultiView7" runat="server">
                                                    <asp:View ID="View18" runat="server">
                                                        <asp:GridView ID="grid_DeliveryList" runat="server" style="text-align: center" 
                                                            AutoGenerateSelectButton="True" 
                                                            onselectedindexchanged="grid_DeliveryList_SelectedIndexChanged">
                                                        </asp:GridView>
                                                    </asp:View>
                                                    <asp:View ID="View19" runat="server">

                                                        <table class="style1">
                                                            <tr>
                                                                <td>
                                                                    P.O. Number:
                                                                    <asp:Label ID="lbl_delPO" runat="server"></asp:Label>
                                                                </td>
                                                                <td>
                                                                    Delivery Date:
                                                                    <asp:Label ID="lbl_delDDate" runat="server"></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    Date:
                                                                    <asp:Label ID="lbl_delDate" runat="server"></asp:Label>
                                                                </td>
                                                                <td>
                                                                    Delivery Number:
                                                                    <asp:Label ID="lbl_delNum" runat="server"></asp:Label>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                        <table class="style1">
                                                            <tr>
                                                                <td>
                                                                    <asp:GridView ID="grid_Delivery1" runat="server" style="text-align: center">
                                                                    </asp:GridView>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    &nbsp;</td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <asp:Label ID="lbl_Addeds" runat="server" Text="Added Item/s to this P.O." 
                                                                        Visible="False"></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <asp:GridView ID="grid_Delivery2" runat="server" style="text-align: center">
                                                                    </asp:GridView>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <asp:Label ID="lbl_Note" runat="server" Text="Note: This P.O. includes:"></asp:Label>
                                                                    <br />
                                                                    <asp:Label ID="lbl_NoteContent" runat="server"></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    Hauler:
                                                                    <asp:Label ID="lbl_Hauler" runat="server"></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    Plate Number:
                                                                    <asp:Label ID="lbl_PlateNum" runat="server"></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    Gate Pass Number:
                                                                    <asp:Label ID="lbl_GatePassNum" runat="server"></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <asp:Button ID="btn_UpdateInventory" runat="server" Text="Update Inventory" 
                                                                        onclick="btn_UpdateInventory_Click" />
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </asp:View>
                                                    <asp:View ID="View20" runat="server">
                                                        Damaged Items<asp:GridView ID="grid_damages" runat="server" 
                                                            AutoGenerateSelectButton="True" style="text-align: center" 
                                                            onselectedindexchanged="grid_damages_SelectedIndexChanged">
                                                        </asp:GridView>
                                                        <table class="style1">
                                                            <tr>
                                                                <td class="style8">
                                                                    Number of damaged items:</td>
                                                                <td>
                                                                    <asp:TextBox ID="txt_numberDam" runat="server" MaxLength="3"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="style8">
                                                                    &nbsp;</td>
                                                                <td>
                                                                    <asp:Button ID="btn_damagedSave" runat="server" onclick="btn_damagedSave_Click" 
                                                                        Text="Save" />
                                                                    <asp:Button ID="btn_replaced" runat="server" Text="Items Replaced" 
                                                                        onclick="btn_replaced_Click" />
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="style8">
                                                                    &nbsp;</td>
                                                                <td>
                                                                    &nbsp;</td>
                                                            </tr>
                                                            <tr>
                                                                <td class="style8">
                                                                    &nbsp;</td>
                                                                <td>
                                                                    <asp:Button ID="btn_ContUpdate" runat="server" Text="Continue Update" 
                                                                        Width="129px" onclick="btn_ContUpdate_Click" />
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </asp:View>
                                                </asp:MultiView>
                                            </asp:View>
                                        </asp:MultiView>
                                    </asp:View>
                                </asp:MultiView>
                            </td>
                        </tr>
                    </table>
                </div>
            </asp:View>
            <asp:View ID="View7" runat="server">
                <div>
                    <h1>
                        User Information</h1>
                    <div>
                        <asp:SqlDataSource ID="dsrc_UserInfo" runat="server" ConnectionString="<%$ ConnectionStrings:MagnaAdministratorConnectionString %>"
                            SelectCommand="SELECT * FROM [SystemUserTBL]"></asp:SqlDataSource>
                        <asp:SqlDataSource ID="UsersAccessDataSource" runat="server" ConnectionString="<%$ ConnectionStrings:MagnaAdministratorConnectionString %>"
                            SelectCommand="SELECT [EmployeeIDNumber], [FirstName], [Surname], [UserName] FROM [SystemUserTBL]">
                        </asp:SqlDataSource>
                    </div>
                    <table class="style1">
                        <tr>
                            <td class="style4" colspan="2">
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td class="style4" colspan="2">
                                <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" AutoGenerateSelectButton="True"
                                    BackColor="White" BorderColor="#CC9966" BorderStyle="None" BorderWidth="1px"
                                    CellPadding="4" DataKeyNames="EmployeeIDNumber" DataSourceID="UsersAccessDataSource"
                                    Style="text-align: center" Width="783px">
                                    <Columns>
                                        <asp:BoundField DataField="EmployeeIDNumber" HeaderText="EmployeeIDNumber" ReadOnly="True"
                                            SortExpression="EmployeeIDNumber" />
                                        <asp:BoundField DataField="FirstName" HeaderText="FirstName" SortExpression="FirstName" />
                                        <asp:BoundField DataField="Surname" HeaderText="Surname" SortExpression="Surname" />
                                        <asp:BoundField DataField="UserName" HeaderText="UserName" SortExpression="UserName" />
                                    </Columns>
                                    <FooterStyle BackColor="#FFFFCC" ForeColor="#330099" />
                                    <HeaderStyle BackColor="#990000" Font-Bold="True" ForeColor="#FFFFCC" />
                                    <PagerStyle BackColor="#FFFFCC" ForeColor="#330099" HorizontalAlign="Center" />
                                    <RowStyle BackColor="White" ForeColor="#330099" />
                                    <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="#663399" />
                                    <SortedAscendingCellStyle BackColor="#FEFCEB" />
                                    <SortedAscendingHeaderStyle BackColor="#AF0101" />
                                    <SortedDescendingCellStyle BackColor="#F6F0C0" />
                                    <SortedDescendingHeaderStyle BackColor="#7E0000" />
                                </asp:GridView>
                            </td>
                        </tr>
                        <tr>
                            <td class="style4" colspan="2">
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td class="style4" colspan="2">
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td class="style4">
                                User ID:
                            </td>
                            <td>
                                <asp:Label ID="lbl_UserID" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="style4">
                                First Name:
                            </td>
                            <td>
                                <asp:Label ID="lbl_Fname" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="style4">
                                Last Name:
                            </td>
                            <td>
                                <asp:Label ID="lbl_Lname" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="style4">
                                User Name:
                            </td>
                            <td>
                                <asp:Label ID="lbl_Uname" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="style4">
                                Password:
                            </td>
                            <td>
                                <asp:TextBox ID="txt_Password" runat="server" TextMode="Password" ReadOnly="True"></asp:TextBox>
                                <asp:Button ID="btn_ChangePassword" runat="server" Text="Change Password" OnClick="btn_ChangePassword_Click" />
                            </td>
                        </tr>
                        <tr>
                            <td class="style5">
                                <asp:Label ID="lbl_CurrentPass" runat="server" Text="Current Password:" Visible="False"></asp:Label>
                            </td>
                            <td class="style6">
                                <asp:TextBox ID="txt_CurrentPass" runat="server" MaxLength="30" TextMode="Password"
                                    Visible="False"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="style4">
                                <asp:Label ID="lbl_NewPassword" runat="server" Text="New Password:" Visible="False"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txt_newPass" runat="server" TextMode="Password" Visible="False"
                                    MaxLength="30"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="style4">
                                <asp:Label ID="lbl_retypePassword" runat="server" Text="Re-type new password:" Visible="False"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txt_retypePass" runat="server" TextMode="Password" Visible="False"
                                    MaxLength="30"></asp:TextBox>
                                <asp:Button ID="btn_Save" runat="server" OnClick="btn_Save_Click" Text="Save" Visible="False" />
                                <asp:Button ID="btn_CancelPass" runat="server" OnClick="btn_CancelPass_Click" Text="Cancel"
                                    Visible="False" />
                            </td>
                        </tr>
                    </table>
                </div>
            </asp:View>
            <asp:View ID="View15" runat="server">
                <h1>Inventory</h1>
                <table class="style1">
                        <tr>
                            <td>
                                Select Category:
                                <asp:DropDownList ID="ddwn_cats" runat="server" AutoPostBack="True" 
                                    onselectedindexchanged="ddwn_cats_SelectedIndexChanged">
                                    <asp:ListItem>Units</asp:ListItem>
                                    <asp:ListItem>Spare Parts</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Select Status:
                                <asp:DropDownList ID="ddwn_Stats" runat="server" AutoPostBack="True" 
                                    onselectedindexchanged="ddwn_Stats_SelectedIndexChanged">
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
                                <asp:SqlDataSource ID="src_InvUnits" runat="server" 
                                    ConnectionString="<%$ ConnectionStrings:MagnacycleConnectionString %>" 
                                    
                                    SelectCommand="SELECT [ModelCode], [Description], [Color], [Quantity], [Status] FROM [SystemUnitsInventoryTBL]">
                                </asp:SqlDataSource>
                                <asp:SqlDataSource ID="src_InvParts" runat="server" 
                                    ConnectionString="<%$ ConnectionStrings:MagnacycleConnectionString %>" 
                                    
                                    SelectCommand="SELECT [PartNumber], [Description], [Color], [Quantity], [Status] FROM [SystemPartsInventoryTBL]">
                                </asp:SqlDataSource>
                                <asp:GridView ID="grid_Inv" runat="server" 
                                    BackColor="White" BorderColor="#CC9966" BorderStyle="None" BorderWidth="1px" 
                                    CellPadding="4" 
                                    style="text-align: center" Width="749px" AutoGenerateSelectButton="True" 
                                    onselectedindexchanged="grid_Inv_SelectedIndexChanged">
                                    <FooterStyle BackColor="#FFFFCC" ForeColor="#330099" />
                                    <HeaderStyle BackColor="#990000" Font-Bold="True" ForeColor="#FFFFCC" />
                                    <PagerStyle BackColor="#FFFFCC" ForeColor="#330099" HorizontalAlign="Center" />
                                    <RowStyle BackColor="White" ForeColor="#330099" />
                                    <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="#663399" />
                                    <SortedAscendingCellStyle BackColor="#FEFCEB" />
                                    <SortedAscendingHeaderStyle BackColor="#AF0101" />
                                    <SortedDescendingCellStyle BackColor="#F6F0C0" />
                                    <SortedDescendingHeaderStyle BackColor="#7E0000" />
                                </asp:GridView>
                                
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Sales:
                                <asp:TextBox ID="txt_Sales" runat="server"></asp:TextBox>
                                <asp:Button ID="btn_Deduct" runat="server" onclick="btn_Deduct_Click" 
                                    Text="Deduct" />
                                <asp:Button ID="btn_Setting" runat="server" Text="Setting" 
                                    onclick="btn_Setting_Click" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lbl_reOrder" runat="server" Text="Re-order Point: " 
                                    Visible="False"></asp:Label>
                                <asp:TextBox ID="txt_Reorder" runat="server" MaxLength="4" ReadOnly="True" 
                                    Visible="False"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lbl_CretLev" runat="server" Text="Critical Level: " 
                                    Visible="False"></asp:Label>
                                <asp:TextBox ID="txt_Crit" runat="server" MaxLength="4" ReadOnly="True" 
                                    Visible="False"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Button ID="btn_EditCrit" runat="server" onclick="btn_EditCrit_Click" 
                                    Text="Edit" Visible="False" style="width: 37px" />
                                <asp:Button ID="btn_SaveCrit" runat="server" onclick="btn_SaveCrit_Click" 
                                    Text="Save" Visible="False" />
                                <asp:Button ID="btn_CancelCrit" runat="server" onclick="btn_CancelCrit_Click" 
                                    Text="Cancel" Visible="False" />
                                <asp:Button ID="btn_Close" runat="server" onclick="btn_Close_Click" 
                                    Text="Close" Visible="False" />
                            </td>
                        </tr>
                    </table>
            </asp:View>
        </asp:MultiView>
    </div>
    </form>

</body>
</html>

