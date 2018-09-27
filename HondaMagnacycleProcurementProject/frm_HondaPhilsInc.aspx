<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="frm_HondaPhilsInc.aspx.cs"
    Inherits="HondaMagnacycleProcurementProject.frm_HondaPhilsInc" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .style1
        {
            width: 100%;
        }
        .style2
        {
            text-align: left;
        }
        .style3
        {
            text-align: left;
        }
        .style5
        {
        }
        .style6
        {
            width: 220px;
            text-align: left;
        }
        .style7
        {
            width: 225px;
        }
        .style8
        {
        }
        .style9
        {
        }
        .style12
        {
        }
        .style15
        {
            width: 228px;
        }
        .style16
        {
            text-align: right;
            height: 23px;
        }
        .style17
        {
            text-align: left;
            height: 23px;
            width: 21px;
        }
        .style18
        {
            height: 23px;
        }
        .style22
        {
            text-align: right;
            width: 388px;
        }
        .style24
        {
            text-align: right;
            width: 268435408px;
        }
        .style25
        {
            text-align: right;
            height: 23px;
            width: 268435408px;
        }
        .style26
        {
            text-align: left;
            width: 21px;
        }
        .style27
        {
            text-align: right;
        }
        .style28
        {
            width: 131px;
        }
        .style29
        {
            text-align: left;
            height: 23px;
        }
        .style34
        {
            width: 78px;
        }
        .style37
        {
            text-align: left;
        }
        .style40
        {
        }
        .style41
        {
            width: 123px;
        }
        .style43
        {
        }
        .style44
        {
            width: 127px;
        }
        .style45
        {
            width: 132px;
        }
        .style46
        {
            width: 238px;
        }
        .style47
        {
            text-align: center;
        }
        .style50
        {
            width: 149px;
        }
        #TextArea1
        {
            height: 190px;
            width: 272px;
        }
        .style51
        {
            width: 507px;
        }
        .style52
        {
            width: 203px;
        }
        .style53
        {
            width: 106px;
        }
        .style55
        {
            width: 178px;
        }
        .style56
        {
            width: 101px;
        }
        .style57
        {
            width: 206px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="True" EnableScriptGlobalization="True"
        EnableCdn="True">
    </asp:ScriptManager>
    <div>
        <table class="style1">
            <tr>
                <td>
                    <asp:Button ID="Button1" runat="server" Text="Orders" OnClick="Button1_Click" 
                        style="height: 26px" />
                    <asp:Button ID="btn_SI" runat="server" Text="Generate Sales Invoice" OnClick="Button31_Click" />
                    <asp:Button ID="btn_Reports" runat="server" Text="Reports" 
                        onclick="btn_Reports_Click" />
                    <asp:Button ID="btn_Inventory" runat="server" Text="Inventory" 
                        onclick="btn_Inventory_Click" />
                    <asp:Button ID="btn_messaging" runat="server" Text="Message" 
                        onclick="btn_messaging_Click" />
                    <asp:Button ID="Button2" runat="server" Text="Maintenance" OnClick="Button2_Click" />
                </td>
                <td style="text-align: right">
                    <asp:Button ID="Button3" runat="server" Text="Sign Out" OnClick="Button3_Click" />
                </td>
            </tr>
        </table>
    </div>
    <div>
        <asp:MultiView ID="MultiView1" runat="server">
            <asp:View ID="View1" runat="server">
                <table class="style1">
                    <tr>
                        <td>
                            <h1>
                                Purchase Orders</h1>
                            <div class="style2">
                                <div class="style2">
                                    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
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
                                    Category:<asp:DropDownList ID="ddwn_Category" runat="server" AutoPostBack="True">
                                        <asp:ListItem>Units</asp:ListItem>
                                        <asp:ListItem>Spare Parts</asp:ListItem>
                                    </asp:DropDownList>
                                    <br />
                                    <asp:Button ID="Button25" runat="server" Text="Refresh List" />
                                    <asp:SqlDataSource ID="src_poUnitReceive" runat="server" ConnectionString="<%$ ConnectionStrings:HPILogInConnectionString %>"
                                        
                                        SelectCommand="SELECT [PurchaseOrderNumber], [DateReceived], [Status], [Remarks] FROM [SystemPOUnitsTBL]">
                                    </asp:SqlDataSource>
                                    <asp:SqlDataSource ID="src_poPartReceive" runat="server" ConnectionString="<%$ ConnectionStrings:HPILogInConnectionString %>"
                                        
                                        SelectCommand="SELECT [PurchaseOrderNumber], [DateReceived], [Status], [Remarks] FROM [SystemPOPartsTBL]">
                                    </asp:SqlDataSource>
                                </div>
                                <div style="text-align: center">
                                    <asp:GridView ID="grid_Sent" runat="server" AutoGenerateSelectButton="True" Width="879px"
                                        BackColor="White" BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px"
                                        CellPadding="3" ForeColor="Black" GridLines="Vertical" OnSelectedIndexChanged="grid_Sent_SelectedIndexChanged">
                                        <AlternatingRowStyle BackColor="#CCCCCC" />
                                        <FooterStyle BackColor="#CCCCCC" />
                                        <HeaderStyle BackColor="Black" Font-Bold="True" ForeColor="White" />
                                        <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
                                        <SelectedRowStyle BackColor="#000099" Font-Bold="True" ForeColor="White" />
                                        <SortedAscendingCellStyle BackColor="#F1F1F1" />
                                        <SortedAscendingHeaderStyle BackColor="#808080" />
                                        <SortedDescendingCellStyle BackColor="#CAC9C9" />
                                        <SortedDescendingHeaderStyle BackColor="#383838" />
                                    </asp:GridView>
                                </div>
                            </div>
                        </td>
                    </tr>
                </table>
            </asp:View>
            <asp:View ID="View2" runat="server">
                <asp:MultiView ID="MultiView2" runat="server">
                    <asp:View ID="View3" runat="server">
                        <div>
                            <table class="style1">
                                <tr>
                                    <td class="style5">
                                        <asp:Button ID="Button5" runat="server" Text="Units" OnClick="Button5_Click" />
                                        <asp:Button ID="Button6" runat="server" Text="Spare Parts" OnClick="Button6_Click" />
                                        <asp:Button ID="Button7" runat="server" Text="Updates" OnClick="Button7_Click" />
                                        <asp:Button ID="Button24" runat="server" Text="User Informations" OnClick="Button24_Click" />
                                    </td>
                                </tr>
                            </table>
                            <asp:MultiView ID="MultiView3" runat="server">
                                <asp:View ID="View4" runat="server">
                                    <div>
                                        <h1>
                                            Unit Models</h1>
                                    </div>
                                    <div>
                                        <asp:SqlDataSource ID="scr_Units" runat="server" ConnectionString="<%$ ConnectionStrings:HPILogInConnectionString %>"
                                            
                                            SelectCommand="SELECT [ModelCode], [Description], [Color], [InitialPrice] FROM [SystemModelsTBL]"></asp:SqlDataSource>
                                        <asp:GridView ID="grid_Units" runat="server" AutoGenerateSelectButton="True" Style="text-align: center"
                                            BackColor="White" BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px"
                                            CellPadding="3" Width="890px" AutoGenerateColumns="False"
                                            DataSourceID="scr_Units" OnSelectedIndexChanged="grid_Units_SelectedIndexChanged"
                                            ForeColor="Black" GridLines="Vertical">
                                            <AlternatingRowStyle BackColor="#CCCCCC" />
                                            <Columns>
                                                <asp:BoundField DataField="ModelCode" HeaderText="ModelCode" 
                                                    SortExpression="ModelCode" />
                                                <asp:BoundField DataField="Description" HeaderText="Description" 
                                                    SortExpression="Description" />
                                                <asp:BoundField DataField="Color" HeaderText="Color" SortExpression="Color" />
                                                <asp:BoundField DataField="InitialPrice" HeaderText="InitialPrice" 
                                                    SortExpression="InitialPrice" />
                                            </Columns>
                                            <FooterStyle BackColor="#CCCCCC" />
                                            <HeaderStyle BackColor="Black" Font-Bold="True" ForeColor="White" />
                                            <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
                                            <SelectedRowStyle BackColor="#000099" Font-Bold="True" ForeColor="White" />
                                            <SortedAscendingCellStyle BackColor="#F1F1F1" />
                                            <SortedAscendingHeaderStyle BackColor="#808080" />
                                            <SortedDescendingCellStyle BackColor="#CAC9C9" />
                                            <SortedDescendingHeaderStyle BackColor="#383838" />
                                        </asp:GridView>
                                    </div>
                                    <div>
                                        <table class="style1">
                                            <tr>
                                                <td class="style37">
                                                    Model Code:
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txt_ModelCode" runat="server" Width="492px"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="style37">
                                                    Description:
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txt_uDescription" runat="server" Width="492px"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="style37" colspan="2">
                                                    <table class="style1">
                                                        <tr>
                                                            <td class="style41">
                                                                Color:
                                                            </td>
                                                            <td>
                                                                <asp:DropDownList ID="ddwn_UnitColor1" runat="server" OnSelectedIndexChanged="ddwn_UnitColor1_SelectedIndexChanged"
                                                                    OnTextChanged="ddwn_UnitColor1_TextChanged" AutoPostBack="True">
                                                                </asp:DropDownList>
                                                            </td>
                                                            <td>
                                                                <asp:DropDownList ID="ddwn_UnitColor2" runat="server" Visible="False">
                                                                </asp:DropDownList>
                                                                <asp:ImageButton ID="btn_uCloseImage2" runat="server" Height="21px" ImageUrl="~/Resources/Close-Image.png"
                                                                    Visible="False" Width="23px" />
                                                            </td>
                                                            <td>
                                                                <asp:DropDownList ID="ddwn_UnitColor3" runat="server" Visible="False">
                                                                </asp:DropDownList>
                                                                <asp:ImageButton ID="btn_uCloseImage3" runat="server" Height="21px" ImageUrl="~/Resources/Close-Image.png"
                                                                    Visible="False" Width="23px" />
                                                            </td>
                                                            <td>
                                                                <asp:DropDownList ID="ddwn_UnitColor4" runat="server" Visible="False">
                                                                </asp:DropDownList>
                                                                <asp:ImageButton ID="btn_uCloseImage4" runat="server" Height="21px" ImageUrl="~/Resources/Close-Image.png"
                                                                    Visible="False" Width="23px" />
                                                            </td>
                                                            <td>
                                                                <asp:DropDownList ID="ddwn_UnitColor5" runat="server" Visible="False">
                                                                </asp:DropDownList>
                                                                <asp:ImageButton ID="btn_uCloseImage5" runat="server" Height="21px" ImageUrl="~/Resources/Close-Image.png"
                                                                    Visible="False" Width="23px" />
                                                            </td>
                                                            <td>
                                                                <asp:DropDownList ID="ddwn_UnitColor6" runat="server" Visible="False">
                                                                </asp:DropDownList>
                                                                <asp:ImageButton ID="btn_uCloseImage6" runat="server" Height="21px" ImageUrl="~/Resources/Close-Image.png"
                                                                    Visible="False" Width="23px" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="style37">
                                                    Initial Price:
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txt_UnitInitPrice" runat="server" Width="492px"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="style3" colspan="2">
                                                    &nbsp;
                                                </td>
                                            </tr>
                                        </table>
                                        <table class="style1">
                                            <tr>
                                                <td class="style43" colspan="2">
                                                    <asp:Button ID="btn_UnitAdd" runat="server" Text="Add" OnClick="btn_UnitAdd_Click"
                                                        Style="height: 26px" />
                                                    <asp:Button ID="btn_uEditSave" runat="server" OnClick="btn_uEditSave_Click" Text="Save"
                                                        Visible="False" />
                                                    <asp:Button ID="btn_uCancelEdit" runat="server" OnClick="btn_uCancelEdit_Click" Text="Cancel"
                                                        Visible="False" />
                                                    <asp:Button ID="btn_UnitEdit" runat="server" Text="Edit" OnClick="btn_UnitEdit_Click" />
                                                    <asp:Button ID="btn_UnitDelete" runat="server" Text="Delete" OnClick="btn_UnitDelete_Click" />
                                                    <asp:Button ID="btn_uClear" runat="server" OnClick="btn_uClear_Click" Text="Clear" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="style44">
                                                    &nbsp;
                                                </td>
                                                <td>
                                                    &nbsp;
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="style44">
                                                    <asp:Button ID="btn_UnitAddColor" runat="server" OnClick="btn_UnitAddColor_Click"
                                                        Text="Add Color" />
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txt_uAddColor" runat="server" MaxLength="25" Visible="False"></asp:TextBox>
                                                    <asp:Button ID="btn_uCancelAddColor" runat="server" Text="Cancel" Visible="False"
                                                        OnClick="btn_uCancelAddColor_Click" />
                                                    <asp:Button ID="btn_uAddColor" runat="server" OnClick="btn_uAddColor_Click" Text="Add"
                                                        Visible="False" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="style44">
                                                    <asp:Button ID="btn_UnitDelColor" runat="server" Text="Delete Color" OnClick="btn_UnitDelColor_Click" />
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="ddwn_uDelColors" runat="server" Visible="False">
                                                    </asp:DropDownList>
                                                    <asp:Button ID="btn_uCancelDelColor" runat="server" Text="Cancel" Visible="False"
                                                        OnClick="btn_uCancelDelColor_Click" />
                                                    <asp:Button ID="btn_uDelColor" runat="server" Text="Delete" Visible="False" OnClick="btn_uDelColor_Click" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="style44">
                                                    &nbsp;
                                                </td>
                                                <td>
                                                    &nbsp;
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="style40" colspan="2">
                                                    &nbsp;
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                </asp:View>
                                <asp:View ID="View5" runat="server">
                                    <div>
                                        <div>
                                            <h1>
                                                Spare Parts</h1>
                                        </div>
                                        <div>
                                            <asp:SqlDataSource ID="src_Parts" runat="server" ConnectionString="<%$ ConnectionStrings:HPILogInConnectionString %>"
                                                
                                                SelectCommand="SELECT [PartNumber], [Description], [Color], [InitialPrice] FROM [SystemPartsTBL]">
                                            </asp:SqlDataSource>
                                            <asp:GridView ID="grid_Parts" runat="server" AutoGenerateSelectButton="True" BackColor="White"
                                                BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" Style="text-align: center"
                                                Width="851px" AutoGenerateColumns="False" DataSourceID="src_Parts" OnSelectedIndexChanged="grid_Parts_SelectedIndexChanged"
                                                ForeColor="Black" GridLines="Vertical">
                                                <AlternatingRowStyle BackColor="#CCCCCC" />
                                                <Columns>
                                                    <asp:BoundField DataField="PartNumber" HeaderText="PartNumber" SortExpression="PartNumber" />
                                                    <asp:BoundField DataField="Description" HeaderText="Description" SortExpression="Description" />
                                                    <asp:BoundField DataField="Color" HeaderText="Color" SortExpression="Color" />
                                                    <asp:BoundField DataField="InitialPrice" HeaderText="InitialPrice" SortExpression="InitialPrice" />
                                                </Columns>
                                                <FooterStyle BackColor="#CCCCCC" />
                                                <HeaderStyle BackColor="Black" Font-Bold="True" ForeColor="White" />
                                                <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
                                                <SelectedRowStyle BackColor="#000099" Font-Bold="True" ForeColor="White" />
                                                <SortedAscendingCellStyle BackColor="#F1F1F1" />
                                                <SortedAscendingHeaderStyle BackColor="#808080" />
                                                <SortedDescendingCellStyle BackColor="#CAC9C9" />
                                                <SortedDescendingHeaderStyle BackColor="#383838" />
                                            </asp:GridView>
                                            <div>
                                                <table class="style1">
                                                    <tr>
                                                        <td class="style6">
                                                            Part No.:
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_PartNumber" runat="server" Width="492px"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="style6">
                                                            Description:
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_Description" runat="server" Width="492px"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="style6">
                                                            Color:
                                                        </td>
                                                        <td>
                                                            <asp:DropDownList ID="ddwn_pColor" runat="server">
                                                            </asp:DropDownList>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="style6">
                                                            Initial Price:
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_Price" runat="server" Width="492px"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="style3" colspan="2">
                                                            &nbsp;
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="style3" colspan="2">
                                                            <asp:Button ID="btn_pAdd" runat="server" Text="Add" OnClick="btn_pAdd_Click" />
                                                            <asp:Button ID="btn_pSave" runat="server" OnClick="btn_pSave_Click" Text="Save" Visible="False" />
                                                            <asp:Button ID="btn_pCancel" runat="server" OnClick="btn_pCancel_Click" Text="Cancel"
                                                                Visible="False" />
                                                            <asp:Button ID="btn_pEdit" runat="server" Text="Edit" OnClick="btn_pEdit_Click" />
                                                            <asp:Button ID="btn_pdelete" runat="server" Text="Delete" OnClick="btn_pdelete_Click" />
                                                            <asp:Button ID="btn_pClear" runat="server" OnClick="btn_pClear_Click" Text="Clear" />
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
                                                                        <asp:Button ID="btn_pAddColor" runat="server" OnClick="btn_pAddColor_Click" Text="Add Color" />
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="txt_AddColor" runat="server" Visible="False"></asp:TextBox>
                                                                        <asp:Button ID="btn_pCancelcolor" runat="server" OnClick="btn_pCancelcolor_Click"
                                                                            Text="Cancel" Visible="False" />
                                                                        <asp:Button ID="btn_pAdcolor" runat="server" OnClick="btn_pAdcolor_Click" Text="Add"
                                                                            Visible="False" />
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <asp:Button ID="btn_pDelColor" runat="server" OnClick="btn_pDelColor_Click" Text="Delete Color" />
                                                                    </td>
                                                                    <td>
                                                                        <asp:DropDownList ID="ddwn_delColor" runat="server" Visible="False">
                                                                        </asp:DropDownList>
                                                                        <asp:Button ID="btn_pCancedel" runat="server" OnClick="btn_pCancedel_Click" Text="Cancel"
                                                                            Visible="False" />
                                                                        <asp:Button ID="btn_pDeldel" runat="server" OnClick="btn_pDeldel_Click" Text="Delete"
                                                                            Visible="False" />
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
                                                        </td>
                                                    </tr>
                                                </table>
                                            </div>
                                        </div>
                                    </div>
                                </asp:View>
                                <asp:View ID="View6" runat="server">
                                    <div>
                                        <h1>
                                            Contact Us</h1>
                                    </div>
                                    <div>
                                        <table class="style1">
                                            <tr>
                                                <td class="style7">
                                                    PLDT Toll Free Number:
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txt_PLDT" runat="server" Width="518px" MaxLength="25" ReadOnly="True"
                                                        OnTextChanged="txt_PLDT_TextChanged"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="style7">
                                                    Landline Number:
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txt_Landline" runat="server" Width="518px" MaxLength="25" ReadOnly="True"
                                                        OnTextChanged="txt_Landline_TextChanged"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="style7">
                                                    Mobile Number:
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txt_Mobile" runat="server" Width="518px" MaxLength="25" ReadOnly="True"
                                                        OnTextChanged="txt_Mobile_TextChanged"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="style7">
                                                    &nbsp;
                                                </td>
                                                <td>
                                                    <asp:Button ID="btn_ContactCancel" runat="server" OnClick="btn_ContactCancel_Click"
                                                        Text="Cancel" Visible="False" />
                                                    <asp:Button ID="btn_ChangeContacts" runat="server" Text="Change Contact Details"
                                                        OnClick="btn_ChangeContacts_Click" />
                                                    <asp:Button ID="btn_SaveContacts" runat="server" Text="Save" OnClick="btn_SaveContacts_Click" />
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                    <div>
                                        <table class="style1">
                                            <tr>
                                                <td>
                                                    &nbsp;
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    &nbsp;
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    &nbsp;
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <h1>
                                                        Charges</h1>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="font-size: 20px">
                                                    Logistic Charges<table class="style1">
                                                        <tr>
                                                            <td class="style15" style="font-size: 15px">
                                                                Per Unit Charge:
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txt_UnitCharge" runat="server" MaxLength="4" ReadOnly="True"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="style15" style="font-size: 15px">
                                                                Per Spare Part Charge:
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txt_PartCharge" runat="server" MaxLength="4" ReadOnly="True"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="style15" style="font-size: 15px">
                                                                Value Added Tax:
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txt_vat" runat="server" MaxLength="2" ReadOnly="True"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="style15">
                                                                &nbsp;
                                                            </td>
                                                            <td>
                                                                <asp:Button ID="btn_ChargesCancel" runat="server" OnClick="btn_ChargesCancel_Click"
                                                                    Text="Cancel" Visible="False" />
                                                                <asp:Button ID="btn_EditCharge" runat="server" OnClick="btn_EditCharge_Click" Text="Edit" />
                                                                <asp:Button ID="btn_SaveCharge" runat="server" OnClick="btn_SaveCharge_Click" Text="Save" />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="style9" colspan="2">
                                                                &nbsp;
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="style9" colspan="2">
                                                                &nbsp;
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="style8" colspan="2">
                                                                &nbsp;
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="style8" colspan="2">
                                                                <h1>
                                                                    Discounts</h1>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                    <table class="style1">
                                                        <tr>
                                                            <td class="style34" style="font-size: 15px">
                                                                Percentage:
                                                            </td>
                                                            <td class="style45" style="font-size: 15px">
                                                                <asp:TextBox ID="txt_UnitPercentage" runat="server" MaxLength="2" ReadOnly="True"></asp:TextBox>
                                                            </td>
                                                            <td class="style46" style="font-size: 15px">
                                                                Per Number of Specific Units:
                                                            </td>
                                                            <td class="style8">
                                                                <asp:TextBox ID="txt_NumberUnits" runat="server" MaxLength="3" ReadOnly="True"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="style34" style="font-size: 15px">
                                                                &nbsp;
                                                            </td>
                                                            <td class="style45" style="font-size: 15px">
                                                                &nbsp;
                                                            </td>
                                                            <td class="style46" style="font-size: 15px">
                                                                &nbsp;
                                                            </td>
                                                            <td class="style8">
                                                                <asp:Button ID="btn_UnitCancel" runat="server" Text="Cancel" Visible="False" OnClick="btn_UnitCancel_Click" />
                                                                <asp:Button ID="btn_UnitsEdit" runat="server" Text="Edit" OnClick="btn_UnitsEdit_Click" />
                                                                <asp:Button ID="btn_UnitSave" runat="server" Text="Save" OnClick="btn_UnitSave_Click" />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="style34" style="font-size: 15px">
                                                                Percentage:
                                                            </td>
                                                            <td class="style45" style="font-size: 15px">
                                                                <asp:TextBox ID="txt_PartPercentage" runat="server" MaxLength="2" ReadOnly="True"></asp:TextBox>
                                                            </td>
                                                            <td class="style46" style="font-size: 15px">
                                                                Per Number of Specific Spare Parts:
                                                            </td>
                                                            <td class="style8">
                                                                <asp:TextBox ID="txt_NumberParts" runat="server" MaxLength="3" ReadOnly="True"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="style34">
                                                                &nbsp;
                                                            </td>
                                                            <td class="style45">
                                                                &nbsp;
                                                            </td>
                                                            <td class="style46">
                                                                &nbsp;
                                                            </td>
                                                            <td class="style8">
                                                                <asp:Button ID="btn_PartsCancel" runat="server" Text="Cancel" Visible="False" OnClick="btn_PartsCancel_Click" />
                                                                <asp:Button ID="btn_PartsEdit" runat="server" Text="Edit" OnClick="btn_PartsEdit_Click" />
                                                                <asp:Button ID="btn_PartsSave" runat="server" Text="Save" OnClick="btn_PartsSave_Click" />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="style12">
                                                                &nbsp;
                                                            </td>
                                                            <td class="style45">
                                                                &nbsp;
                                                            </td>
                                                            <td class="style46">
                                                                &nbsp;
                                                            </td>
                                                            <td class="style12">
                                                                &nbsp;
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="style12">
                                                                &nbsp;
                                                            </td>
                                                            <td class="style45">
                                                                &nbsp;
                                                            </td>
                                                            <td class="style46">
                                                                &nbsp;
                                                            </td>
                                                            <td class="style12">
                                                                &nbsp;
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="style12" colspan="4">
                                                                &nbsp;
                                                            </td>
                                                        </tr
                                                         <tr>
                                                            <tr>
                                                                <td class="style12" colspan="4">
                                                                    <h1>
                                                                        Hauler Details</h1>
                                                                </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="style12" colspan="4">
                                                                <asp:SqlDataSource ID="src_Haulers" runat="server" 
                                                                    ConnectionString="<%$ ConnectionStrings:HPILogInConnectionString %>" 
                                                                    SelectCommand="SELECT [s_number], [Hauler], [Plate] FROM [SystemHaulersTBL]">
                                                                </asp:SqlDataSource>
                                                                <asp:GridView ID="grid_Haulers" runat="server" AutoGenerateColumns="False" 
                                                                    AutoGenerateSelectButton="True" BackColor="White" BorderColor="#999999" 
                                                                    BorderStyle="Solid" BorderWidth="1px" CellPadding="3" 
                                                                    DataSourceID="src_Haulers" ForeColor="Black" GridLines="Vertical" 
                                                                    onselectedindexchanged="grid_Haulers_SelectedIndexChanged" 
                                                                    style="text-align: center" Width="504px">
                                                                    <AlternatingRowStyle BackColor="#CCCCCC" />
                                                                    <Columns>
                                                                        <asp:BoundField DataField="s_number" HeaderText="No." 
                                                                            SortExpression="s_number" />
                                                                        <asp:BoundField DataField="Hauler" HeaderText="Hauler Name" 
                                                                            SortExpression="Hauler" />
                                                                        <asp:BoundField DataField="Plate" HeaderText="Plate Number" 
                                                                            SortExpression="Plate" />
                                                                    </Columns>
                                                                    <FooterStyle BackColor="#CCCCCC" />
                                                                    <HeaderStyle BackColor="Black" Font-Bold="True" ForeColor="White" />
                                                                    <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
                                                                    <SelectedRowStyle BackColor="#000099" Font-Bold="True" ForeColor="White" />
                                                                    <SortedAscendingCellStyle BackColor="#F1F1F1" />
                                                                    <SortedAscendingHeaderStyle BackColor="#808080" />
                                                                    <SortedDescendingCellStyle BackColor="#CAC9C9" />
                                                                    <SortedDescendingHeaderStyle BackColor="#383838" />
                                                                </asp:GridView>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="style12" colspan="4">
                                                                <asp:Button ID="btn_haulerAdd" runat="server" onclick="btn_haulerAdd_Click" 
                                                                    Text="Add" />
                                                                <asp:Button ID="btn_haulerEdit" runat="server" onclick="btn_haulerEdit_Click" 
                                                                    Text="Edit" />
                                                                <asp:Button ID="btn_haulerRemove" runat="server" 
                                                                    onclick="btn_haulerRemove_Click" Text="Remove" />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="style12" colspan="4">
                                                                <asp:Label ID="lbl_haulerName" runat="server" Text="Name: " Visible="False"></asp:Label>
                                                                <asp:TextBox ID="txt_AddHauler" runat="server" MaxLength="50" Visible="False"></asp:TextBox>
                                                                <asp:TextBox ID="txt_EditHauler" runat="server" MaxLength="50" Visible="False"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="style12" colspan="4">
                                                                <asp:Label ID="lbl_haulerPlate" runat="server" Text="Plate No.: " 
                                                                    Visible="False"></asp:Label>
                                                                <asp:TextBox ID="txt_AddPlate" runat="server" MaxLength="10" Visible="False"></asp:TextBox>
                                                                <asp:TextBox ID="txt_EditPlate" runat="server" MaxLength="10" Visible="False"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="style12" colspan="4">
                                                                <asp:Button ID="btn_SaveHauler" runat="server" onclick="btn_SaveHauler_Click" 
                                                                    Text="Save" Visible="False" />
                                                                <asp:Button ID="btn_UpdateHauler" runat="server" 
                                                                    onclick="btn_UpdateHauler_Click" style="height: 26px" Text="Update" 
                                                                    Visible="False" />
                                                                <asp:Button ID="btn_CancelHauler" runat="server" 
                                                                    onclick="btn_CancelHauler_Click" Text="Cancel" Visible="False" />
                                                            </td>
                                                        </tr>
                                                        </tr>
                                                    </table>
                                    </div>
                                </asp:View>
                                <asp:View ID="View7" runat="server">
                                    <asp:Button ID="Button8" runat="server" Text="Magnacycle Admin" OnClick="Button8_Click" />
                                    <asp:Button ID="Button15" runat="server" Text="HPI Admin" OnClick="Button15_Click" />
                                    <asp:MultiView ID="MultiView4" runat="server">
                                        <asp:View ID="View8" runat="server">
                                            <div>
                                                <h1>
                                                    Magnacycle Administrator Log in Informations</h1>
                                            </div>
                                            <div>
                                                <table class="style1">
                                                    <tr>
                                                        <td class="style27">
                                                            Administrator ID:
                                                        </td>
                                                        <td class="style26" colspan="2">
                                                            <asp:TextBox ID="txt_AdminID" runat="server" MaxLength="20"></asp:TextBox>
                                                        </td>
                                                        <td class="style24" colspan="2">
                                                            Age:
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_AdminAge" runat="server" MaxLength="2"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="style27">
                                                            First Name:
                                                        </td>
                                                        <td class="style26" colspan="2">
                                                            <asp:TextBox ID="txt_AdminFname" runat="server" MaxLength="25"></asp:TextBox>
                                                        </td>
                                                        <td class="style24" colspan="2">
                                                            Birthday:
                                                        </td>
                                                        <td>
                                                            <asp:DropDownList ID="ddwn_AdminMonth" runat="server">
                                                                <asp:ListItem Value="01">January</asp:ListItem>
                                                                <asp:ListItem Value="02">February</asp:ListItem>
                                                                <asp:ListItem Value="03">March</asp:ListItem>
                                                                <asp:ListItem Value="04">April</asp:ListItem>
                                                                <asp:ListItem Value="05">May</asp:ListItem>
                                                                <asp:ListItem Value="06">June</asp:ListItem>
                                                                <asp:ListItem Value="07">July</asp:ListItem>
                                                                <asp:ListItem Value="08">August</asp:ListItem>
                                                                <asp:ListItem Value="09">September</asp:ListItem>
                                                                <asp:ListItem Value="10">October</asp:ListItem>
                                                                <asp:ListItem Value="11">November</asp:ListItem>
                                                                <asp:ListItem Value="12">December</asp:ListItem>
                                                            </asp:DropDownList>
                                                            <asp:DropDownList ID="ddwn_AdminDay" runat="server">
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
                                                            <asp:DropDownList ID="ddwn_AdminYear" runat="server">
                                                            </asp:DropDownList>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="style27">
                                                            Middle Name:
                                                        </td>
                                                        <td class="style26" colspan="2">
                                                            <asp:TextBox ID="txt_AdminMname" runat="server" MaxLength="20"></asp:TextBox>
                                                        </td>
                                                        <td class="style24" colspan="2">
                                                            Citizenship:
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_AdminCiti" runat="server" MaxLength="15"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="style16">
                                                            Surname:
                                                        </td>
                                                        <td class="style17" colspan="2">
                                                            <asp:TextBox ID="txt_AdminSurname" runat="server" MaxLength="20"></asp:TextBox>
                                                        </td>
                                                        <td class="style25" colspan="2">
                                                            Email:
                                                        </td>
                                                        <td class="style18">
                                                            <asp:TextBox ID="txt_AdminEmail" runat="server" MaxLength="30" TextMode="Email"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="style27">
                                                            Gender:
                                                        </td>
                                                        <td class="style26" colspan="2">
                                                            <asp:DropDownList ID="ddwn_AdminGender" runat="server">
                                                                <asp:ListItem>Male</asp:ListItem>
                                                                <asp:ListItem>Female</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </td>
                                                        <td class="style24" colspan="2">
                                                            &nbsp;
                                                        </td>
                                                        <td>
                                                            <asp:Button ID="btn_AdminClear" runat="server" OnClick="btn_AdminClear_Click" Text="Clear" />
                                                            <asp:Button ID="btn_AdminAddSave" runat="server" OnClick="btn_AdminAddSave_Click"
                                                                Text="Add" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="style29" colspan="6">
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="style22" colspan="2">
                                                            &nbsp;
                                                        </td>
                                                        <td class="style2" colspan="2">
                                                            <asp:Label ID="lbl_IDExist" runat="server" Visible="False"></asp:Label>
                                                        </td>
                                                        <td class="style2" colspan="2">
                                                            &nbsp;
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="style22" colspan="2">
                                                            &nbsp;
                                                        </td>
                                                        <td class="style2" colspan="2">
                                                            <asp:Button ID="btn_AdminSaveOk" runat="server" OnClick="btn_AdminSaveOk_Click" Text="Ok"
                                                                Visible="False" />
                                                            <asp:Button ID="btn_AdminSaveCancel" runat="server" OnClick="btn_AdminSaveCancel_Click"
                                                                Text="Cancel" Visible="False" />
                                                            <asp:Button ID="btn_AdminOkAdder" runat="server" OnClick="btn_AdminOkAdder_Click"
                                                                Text="Ok" Visible="False" />
                                                            <asp:Button ID="btn_AdminCancelAdder" runat="server" OnClick="btn_AdminCancelAdder_Click"
                                                                Text="Cancel" Visible="False" />
                                                            <asp:Button ID="btn_AdminOK" runat="server" OnClick="btn_AdminOK_Click" Text="Ok"
                                                                Visible="False" />
                                                            <asp:Button ID="btn_IDCancel" runat="server" OnClick="btn_IDCancel_Click" Text="Cancel"
                                                                Visible="False" />
                                                        </td>
                                                        <td class="style2" colspan="2">
                                                            &nbsp;
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="style2" colspan="6">
                                                            &nbsp;
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="style3" colspan="6">
                                                            Administrator/s
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="style2" colspan="6">
                                                            <asp:SqlDataSource ID="src_Admins" runat="server" ConnectionString="<%$ ConnectionStrings:MagnaAdministratorConnectionString %>"
                                                                SelectCommand="SELECT [AdminIDNumber], [FirstName], [MiddleName], [Surname], [Gender], [Age], [Birthday], [Citizenship], [Email], [UserName] FROM [SystemAdminsTBL]">
                                                            </asp:SqlDataSource>
                                                            <asp:GridView ID="grid_Admins" runat="server" AutoGenerateSelectButton="True" BackColor="White"
                                                                BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" Style="text-align: center"
                                                                Width="693px" AutoGenerateColumns="False" DataKeyNames="AdminIDNumber" DataSourceID="src_Admins"
                                                                OnSelectedIndexChanged="grid_Admins_SelectedIndexChanged" ForeColor="Black" GridLines="Vertical">
                                                                <AlternatingRowStyle BackColor="#CCCCCC" />
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
                                                                <FooterStyle BackColor="#CCCCCC" />
                                                                <HeaderStyle BackColor="Black" Font-Bold="True" ForeColor="White" />
                                                                <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
                                                                <SelectedRowStyle BackColor="#000099" Font-Bold="True" ForeColor="White" />
                                                                <SortedAscendingCellStyle BackColor="#F1F1F1" />
                                                                <SortedAscendingHeaderStyle BackColor="#808080" />
                                                                <SortedDescendingCellStyle BackColor="#CAC9C9" />
                                                                <SortedDescendingHeaderStyle BackColor="#383838" />
                                                            </asp:GridView>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="style2" colspan="6">
                                                            <asp:Button ID="btn_AdminRemoveAccess" runat="server" Text="Remove Access" OnClick="btn_AdminRemoveAccess_Click" />
                                                            <asp:Button ID="btn_AdminEdit" runat="server" Text="Edit" OnClick="btn_AdminEdit_Click" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </div>
                                        </asp:View>
                                        <asp:View ID="View9" runat="server">
                                            <div>
                                                <table class="style1">
                                                    <tr>
                                                        <td colspan="6">
                                                            &nbsp;
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="6">
                                                            &nbsp;
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="6">
                                                            <h1>
                                                                Honda Philippines Inc. System Administrator</h1>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="style27">
                                                            Administrator ID:
                                                        </td>
                                                        <td class="style28" colspan="2">
                                                            <asp:TextBox ID="txt_sAdID" runat="server" MaxLength="20"></asp:TextBox>
                                                        </td>
                                                        <td class="style27" colspan="2">
                                                            Age:
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_sAdAge" runat="server" MaxLength="20"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="style27">
                                                            First Name:
                                                        </td>
                                                        <td class="style28" colspan="2">
                                                            <asp:TextBox ID="txt_sAdFname" runat="server" MaxLength="25"></asp:TextBox>
                                                        </td>
                                                        <td class="style27" colspan="2">
                                                            Birthday:
                                                        </td>
                                                        <td>
                                                            <asp:DropDownList ID="ddwn_sAdMonth" runat="server">
                                                                <asp:ListItem Value="01">January</asp:ListItem>
                                                                <asp:ListItem Value="02">February</asp:ListItem>
                                                                <asp:ListItem Value="03">March</asp:ListItem>
                                                                <asp:ListItem Value="04">April</asp:ListItem>
                                                                <asp:ListItem Value="05">May</asp:ListItem>
                                                                <asp:ListItem Value="06">June</asp:ListItem>
                                                                <asp:ListItem Value="07">July</asp:ListItem>
                                                                <asp:ListItem Value="08">August</asp:ListItem>
                                                                <asp:ListItem Value="09">September</asp:ListItem>
                                                                <asp:ListItem Value="10">October</asp:ListItem>
                                                                <asp:ListItem Value="11">November</asp:ListItem>
                                                                <asp:ListItem Value="12">December</asp:ListItem>
                                                            </asp:DropDownList>
                                                            <asp:DropDownList ID="ddwn_sAdDay" runat="server">
                                                                <asp:ListItem>1</asp:ListItem>
                                                                <asp:ListItem>2</asp:ListItem>
                                                                <asp:ListItem>3</asp:ListItem>
                                                                <asp:ListItem>4</asp:ListItem>
                                                                <asp:ListItem>5</asp:ListItem>
                                                                <asp:ListItem>6</asp:ListItem>
                                                                <asp:ListItem>7</asp:ListItem>
                                                                <asp:ListItem>8</asp:ListItem>
                                                                <asp:ListItem>9</asp:ListItem>
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
                                                            <asp:DropDownList ID="ddwn_sAdYear" runat="server">
                                                            </asp:DropDownList>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="style27">
                                                            Middle Name:
                                                        </td>
                                                        <td class="style28" colspan="2">
                                                            <asp:TextBox ID="txt_sAdMname" runat="server" MaxLength="20"></asp:TextBox>
                                                        </td>
                                                        <td class="style27" colspan="2">
                                                            Citizenship:
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_sAdCiti" runat="server" MaxLength="15"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="style27">
                                                            Surname:
                                                        </td>
                                                        <td class="style28" colspan="2">
                                                            <asp:TextBox ID="txt_sAdSname" runat="server" MaxLength="20"></asp:TextBox>
                                                        </td>
                                                        <td class="style27" colspan="2">
                                                            Email:
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_sAdEmail" runat="server" MaxLength="30"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="style27">
                                                            Gender:
                                                        </td>
                                                        <td class="style28" colspan="2">
                                                            <asp:DropDownList ID="ddwn_sAdGender" runat="server">
                                                                <asp:ListItem>Male</asp:ListItem>
                                                                <asp:ListItem>Female</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </td>
                                                        <td class="style27" colspan="2">
                                                            &nbsp;
                                                        </td>
                                                        <td>
                                                            <asp:Button ID="btn_sAdClear" runat="server" Text="Clear" OnClick="btn_sAdCancel_Click" />
                                                            <asp:Button ID="btn_sAdAdd" runat="server" Text="Add" OnClick="btn_sAdAdd_Click" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="6">
                                                            &nbsp;
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="2">
                                                            &nbsp;
                                                        </td>
                                                        <td class="style27" colspan="2">
                                                            <asp:Label ID="lbl_warning" runat="server" Visible="False"></asp:Label>
                                                        </td>
                                                        <td colspan="2">
                                                            &nbsp;
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="2">
                                                            &nbsp;
                                                        </td>
                                                        <td class="style27" colspan="2">
                                                            <asp:Button ID="btn_sAdOk1" runat="server" Text="Ok" Visible="False" OnClick="btn_sAdOk1_Click" />
                                                            <asp:Button ID="btn_sAdCancel1" runat="server" Text="Cancel" Visible="False" OnClick="btn_sAdCancel1_Click" />
                                                            <asp:Button ID="btn_sAdOk2" runat="server" Text="Ok" Visible="False" OnClick="btn_sAdOk2_Click" />
                                                            <asp:Button ID="btn_sAdCancel2" runat="server" Text="Cancel" Visible="False" OnClick="btn_sAdCancel2_Click" />
                                                            <asp:Button ID="btn_sAdOk3" runat="server" Text="Ok" Visible="False" OnClick="btn_sAdOk3_Click" />
                                                            <asp:Button ID="btn_sAdCancel3" runat="server" Text="Cancel" Visible="False" OnClick="btn_sAdCancel3_Click" />
                                                        </td>
                                                        <td colspan="2">
                                                            &nbsp;
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="6">
                                                            &nbsp;
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="6">
                                                            H.P.I. System Administrator/s
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="6" style="text-align: center">
                                                            <asp:SqlDataSource ID="src_PrimaryAdmins" runat="server" ConnectionString="<%$ ConnectionStrings:HPILogInConnectionString %>"
                                                                SelectCommand="SELECT [AdminID], [FirstName], [MiddleName], [Surname], [Gender], [Age], [Birthday], [Citizenship], [Email], [UserName] FROM [SystemHPIUsersTBL]">
                                                            </asp:SqlDataSource>
                                                            <asp:GridView ID="grid_superAdmins" runat="server" AutoGenerateSelectButton="True"
                                                                BackColor="White" BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px"
                                                                CellPadding="3" Width="715px" AutoGenerateColumns="False" DataKeyNames="AdminID"
                                                                DataSourceID="src_PrimaryAdmins" OnSelectedIndexChanged="grid_superAdmins_SelectedIndexChanged"
                                                                ForeColor="Black" GridLines="Vertical">
                                                                <AlternatingRowStyle BackColor="#CCCCCC" />
                                                                <Columns>
                                                                    <asp:BoundField DataField="AdminID" HeaderText="AdminID" ReadOnly="True" SortExpression="AdminID" />
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
                                                                <FooterStyle BackColor="#CCCCCC" />
                                                                <HeaderStyle BackColor="Black" Font-Bold="True" ForeColor="White" />
                                                                <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
                                                                <SelectedRowStyle BackColor="#000099" Font-Bold="True" ForeColor="White" />
                                                                <SortedAscendingCellStyle BackColor="#F1F1F1" />
                                                                <SortedAscendingHeaderStyle BackColor="#808080" />
                                                                <SortedDescendingCellStyle BackColor="#CAC9C9" />
                                                                <SortedDescendingHeaderStyle BackColor="#383838" />
                                                            </asp:GridView>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="6" style="text-align: left">
                                                            <asp:Button ID="btn_sAdRemoveAcc" runat="server" Text="Remove Access" OnClick="btn_sAdRemoveAcc_Click" />
                                                            <asp:Button ID="btn_sAdEdit" runat="server" Text="Edit" OnClick="btn_sAdEdit_Click" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="6" style="text-align: left">
                                                            &nbsp;
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="6" style="text-align: left; font-size: 25px;">
                                                            Logged In
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="style27" style="font-size: 15px;">
                                                            Admin ID:
                                                        </td>
                                                        <td style="font-size: 15px;" colspan="5">
                                                            <asp:TextBox ID="txt_sAdAdminsID" runat="server" ReadOnly="True"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="style27" style="font-size: 15px;">
                                                            Full Name:
                                                        </td>
                                                        <td style="font-size: 15px;" colspan="5">
                                                            <asp:TextBox ID="txt_sAdFullName" runat="server" ReadOnly="True"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="style27" style="font-size: 15px;">
                                                            User Name:
                                                        </td>
                                                        <td style="font-size: 15px;" colspan="5">
                                                            <asp:TextBox ID="txt_sAdUserName" runat="server" ReadOnly="True"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="style27" style="font-size: 15px;">
                                                            Password:
                                                        </td>
                                                        <td style="font-size: 15px;" colspan="5">
                                                            <asp:TextBox ID="txt_sAdAdminPassword" runat="server" ReadOnly="True" TextMode="Password"></asp:TextBox>
                                                            <asp:Button ID="btn_sAdChangePass" runat="server" Text="Change Password" OnClick="btn_sAdChangePass_Click" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="style27" style="font-size: 15px;">
                                                            <asp:Label ID="lbl_superAdminCurrentPass" runat="server" Text="Current Password:"
                                                                Visible="False"></asp:Label>
                                                        </td>
                                                        <td style="font-size: 15px;" colspan="5">
                                                            <asp:TextBox ID="txt_superAdminCurrentPass" runat="server" Visible="False" MaxLength="30"
                                                                TextMode="Password"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="style27" style="font-size: 15px;">
                                                            <asp:Label ID="lbl_superAdminNewPass" runat="server" Text="New Password:" Visible="False"></asp:Label>
                                                        </td>
                                                        <td style="font-size: 15px;" colspan="5">
                                                            <asp:TextBox ID="txt_superAdminNewPassword" runat="server" Visible="False" MaxLength="30"
                                                                TextMode="Password"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="style27" style="font-size: 15px;">
                                                            <asp:Label ID="lbl_superAdminRetypePass" runat="server" Text="Re-type New Password"
                                                                Visible="False"></asp:Label>
                                                        </td>
                                                        <td style="font-size: 15px;" colspan="5">
                                                            <asp:TextBox ID="txt_superAdminConfirm" runat="server" Visible="False" MaxLength="30"
                                                                TextMode="Password"></asp:TextBox>
                                                            <asp:Button ID="btn_superAdminPassCancel" runat="server" Text="Cancel" Visible="False"
                                                                OnClick="btn_superAdminPassCancel_Click" />
                                                            <asp:Button ID="btn_superAdminPassSave" runat="server" Text="Save" Visible="False"
                                                                OnClick="btn_superAdminPassSave_Click" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </div>
                                        </asp:View>
                                    </asp:MultiView>
                                </asp:View>
                            </asp:MultiView>
                        </div>
                    </asp:View>
                </asp:MultiView>
            </asp:View>
            <asp:View ID="View10" runat="server">
                <h1>
                    Generate Sales Invoice</h1>
                <div>
                    <table class="style1">
                        <tr>
                            <td colspan="4">
                                P.O. Number:
                                <asp:TextBox ID="txt_POnum" runat="server" ReadOnly="True"></asp:TextBox>
                                &nbsp;&nbsp;&nbsp;&nbsp;
                                <asp:Label ID="lbl_SI" runat="server" Text="Sales Invoice No.:" Visible="False"></asp:Label>
                                &nbsp;<asp:TextBox ID="txt_SalesInvoice" runat="server" ReadOnly="True" 
                                    Visible="False"></asp:TextBox>
                                &nbsp;&nbsp;&nbsp;&nbsp;
                                <asp:Label ID="lbl_DeliveryDate" runat="server" Text="Delivery Date: " Visible="False"></asp:Label>
                                <asp:TextBox ID="txt_DeliveryDate" runat="server" Visible="False" ReadOnly="True"></asp:TextBox>
                                <asp:ImageButton ID="btn_Cal" runat="server" Height="21px" ImageUrl="~/Resources/calendar.jpg"
                                    OnClick="btn_Cal_Click" Visible="False" Width="23px" />
                                <asp:Calendar ID="cal_Calendar" runat="server" BackColor="White" BorderColor="#3366CC"
                                    BorderWidth="1px" Caption="Delivery Calendar" CaptionAlign="Top" CellPadding="1"
                                    DayNameFormat="Shortest" Font-Names="Verdana" Font-Size="8pt" ForeColor="#003399"
                                    Height="200px" NextMonthText="-&gt;" OnDayRender="cal_Calendar_DayRender" OnSelectionChanged="cal_Calendar_SelectionChanged"
                                    PrevMonthText="&lt;-" Visible="False" Width="220px">
                                    <DayHeaderStyle BackColor="#99CCCC" ForeColor="#336666" Height="1px" />
                                    <NextPrevStyle Font-Size="8pt" ForeColor="#CCCCFF" />
                                    <OtherMonthDayStyle ForeColor="#999999" />
                                    <SelectedDayStyle BackColor="#009999" Font-Bold="True" ForeColor="#CCFF99" />
                                    <SelectorStyle BackColor="#99CCCC" ForeColor="#336666" />
                                    <TitleStyle BackColor="#003399" BorderColor="#3366CC" BorderWidth="1px" Font-Bold="True"
                                        Font-Size="10pt" ForeColor="#CCCCFF" Height="25px" />
                                    <TodayDayStyle BackColor="#99CCCC" ForeColor="White" />
                                    <WeekendDayStyle BackColor="#CCCCFF" />
                                </asp:Calendar>
                                <asp:Button ID="btn_Proceed" runat="server" Text="Proceed" Visible="False" 
                                    onclick="btn_Proceed_Click" style="height: 26px" />
                                <asp:Button ID="btn_Cancel" runat="server" OnClick="btn_Cancel_Click" Text="Cancel"
                                    Visible="False" />
                            </td>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td colspan="4">
                                Date of P.O. :
                                <asp:TextBox ID="txt_DateOfPO" runat="server" ReadOnly="True"></asp:TextBox>
                            </td>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td colspan="4">
                                <asp:UpdatePanel ID="UpdatePanel4" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <asp:Timer ID="Timer4" runat="server" Interval="250" Enabled="False" OnTick="Timer4_Tick">
                                        </asp:Timer>
                                        <asp:GridView ID="grid_PO1" runat="server" Style="text-align: center" Width="565px"
                                            BackColor="White" BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px"
                                            CellPadding="3" ForeColor="Black" GridLines="Vertical">
                                            <AlternatingRowStyle BackColor="#CCCCCC" />
                                            <FooterStyle BackColor="#CCCCCC" />
                                            <HeaderStyle BackColor="Black" Font-Bold="True" ForeColor="White" />
                                            <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
                                            <SelectedRowStyle BackColor="#000099" Font-Bold="True" ForeColor="White" />
                                            <SortedAscendingCellStyle BackColor="#F1F1F1" />
                                            <SortedAscendingHeaderStyle BackColor="#808080" />
                                            <SortedDescendingCellStyle BackColor="#CAC9C9" />
                                            <SortedDescendingHeaderStyle BackColor="#383838" />
                                        </asp:GridView>
                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:AsyncPostBackTrigger ControlID="Timer4" EventName="Tick" />
                                    </Triggers>
                                </asp:UpdatePanel>
                            </td>
                            <td>
                                <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <asp:Timer ID="Timer2" runat="server" OnTick="Timer2_Tick" Interval="250">
                                        </asp:Timer>
                                        <div class="style47">
                                            <asp:GridView ID="grid_update1" runat="server" BackColor="White" BorderColor="#999999"
                                                BorderStyle="Solid" BorderWidth="1px" CellPadding="3" ForeColor="Black" GridLines="Vertical">
                                                <AlternatingRowStyle BackColor="#CCCCCC" />
                                                <FooterStyle BackColor="#CCCCCC" />
                                                <HeaderStyle BackColor="Black" Font-Bold="True" ForeColor="White" />
                                                <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
                                                <SelectedRowStyle BackColor="#000099" Font-Bold="True" ForeColor="White" />
                                                <SortedAscendingCellStyle BackColor="#F1F1F1" />
                                                <SortedAscendingHeaderStyle BackColor="#808080" />
                                                <SortedDescendingCellStyle BackColor="#CAC9C9" />
                                                <SortedDescendingHeaderStyle BackColor="#383838" />
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
                            <td colspan="4">
                                &nbsp;
                            </td>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td colspan="4">
                                <asp:Label ID="lbl_Added" runat="server" Text="Added Items to this P.O. :" Visible="False"></asp:Label>
                            </td>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td colspan="4">
                                <asp:UpdatePanel ID="UpdatePanel5" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <asp:Timer ID="Timer5" runat="server" Interval="250" Enabled="False" OnTick="Timer5_Tick">
                                        </asp:Timer>
                                        <asp:GridView ID="grid_PO2" runat="server" Style="text-align: center" Width="565px"
                                            BackColor="White" BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px"
                                            CellPadding="3" ForeColor="Black" GridLines="Vertical">
                                            <AlternatingRowStyle BackColor="#CCCCCC" />
                                            <FooterStyle BackColor="#CCCCCC" />
                                            <HeaderStyle BackColor="Black" Font-Bold="True" ForeColor="White" />
                                            <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
                                            <SelectedRowStyle BackColor="#000099" Font-Bold="True" ForeColor="White" />
                                            <SortedAscendingCellStyle BackColor="#F1F1F1" />
                                            <SortedAscendingHeaderStyle BackColor="#808080" />
                                            <SortedDescendingCellStyle BackColor="#CAC9C9" />
                                            <SortedDescendingHeaderStyle BackColor="#383838" />
                                        </asp:GridView>
                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:AsyncPostBackTrigger ControlID="Timer5" EventName="Tick" />
                                    </Triggers>
                                </asp:UpdatePanel>
                            </td>
                            <td>
                                <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <asp:Timer ID="Timer3" runat="server" OnTick="Timer3_Tick" Interval="250">
                                        </asp:Timer>
                                        <div class="style47">
                                            <asp:GridView ID="grid_update2" runat="server" BackColor="White" BorderColor="#999999"
                                                BorderStyle="Solid" BorderWidth="1px" CellPadding="3" ForeColor="Black" GridLines="Vertical">
                                                <AlternatingRowStyle BackColor="#CCCCCC" />
                                                <FooterStyle BackColor="#CCCCCC" />
                                                <HeaderStyle BackColor="Black" Font-Bold="True" ForeColor="White" />
                                                <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
                                                <SelectedRowStyle BackColor="#000099" Font-Bold="True" ForeColor="White" />
                                                <SortedAscendingCellStyle BackColor="#F1F1F1" />
                                                <SortedAscendingHeaderStyle BackColor="#808080" />
                                                <SortedDescendingCellStyle BackColor="#CAC9C9" />
                                                <SortedDescendingHeaderStyle BackColor="#383838" />
                                            </asp:GridView>
                                        </div>
                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:AsyncPostBackTrigger ControlID="Timer3" EventName="Tick" />
                                    </Triggers>
                                </asp:UpdatePanel>
                            </td>
                        </tr>
                        <tr>
                            <td class="style50">
                                &nbsp;
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                <asp:Button ID="btn_Attach" runat="server" Text="Attach Updates" 
                                    OnClick="btn_Attach_Click" Visible="False" />
                            </td>
                        </tr>
                        <tr>
                            <td class="style50">
                                Sub-Total:
                            </td>
                            <td>
                                <asp:TextBox ID="txt_Total" runat="server" ReadOnly="True"></asp:TextBox>
                            </td>
                            <td>
                                <asp:Label ID="lbl_ServiceCharge" runat="server" Text="Service Charge:"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txt_ServiceCharge" runat="server" ReadOnly="True"></asp:TextBox>
                            </td>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td class="style50">
                                Discount:
                            </td>
                            <td>
                                <asp:UpdatePanel ID="UpdatePanel6" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <asp:Timer ID="Timer6" runat="server" Interval="1000" OnTick="Timer6_Tick">
                                        </asp:Timer>
                                        <asp:TextBox ID="txt_Discount" runat="server" ReadOnly="True"></asp:TextBox>
                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:AsyncPostBackTrigger ControlID="Timer6" EventName="Tick" />
                                    </Triggers>
                                </asp:UpdatePanel>
                            </td>
                            <td>
                                Total Service Charge:
                            </td>
                            <td>
                                <asp:TextBox ID="txt_TotalCharge" runat="server" ReadOnly="True"></asp:TextBox>
                            </td>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td class="style50">
                                Discounted Amount:
                            </td>
                            <td>
                                <asp:TextBox ID="txt_DiscountedAmount" runat="server"></asp:TextBox>
                            </td>
                            <td>
                                Total:
                            </td>
                            <td>
                                <asp:TextBox ID="txt_GrandTotal" runat="server"></asp:TextBox>
                            </td>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td class="style50">
                                <asp:UpdatePanel ID="UpdatePanel7" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <asp:Timer ID="Timer7" runat="server" Interval="1000" OnTick="Timer7_Tick">
                                        </asp:Timer>
                                        VAT:
                                        <asp:Label ID="lbl_VAT" runat="server" Font-Bold="True" Font-Names="Arial Black"
                                            ForeColor="#CC0000"></asp:Label>
                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:AsyncPostBackTrigger ControlID="Timer7" EventName="Tick" />
                                    </Triggers>
                                </asp:UpdatePanel>
                            </td>
                            <td>
                                <asp:TextBox ID="txt_Vats" runat="server" ReadOnly="True"></asp:TextBox>
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td style="text-align: right">
                                <asp:Button ID="btn_Stock" runat="server" Text="See Stock Report" 
                                    Width="136px" onclick="btn_Stock_Click" />
                                <asp:Button ID="btn_Check" runat="server" OnClick="btn_Check_Click" 
                                    Text="Check for Updates" Width="132px" />
                                <asp:Button ID="btn_Compute" runat="server" Text="Compute" 
                                    OnClick="btn_Compute_Click" Width="65px" />
                                <asp:Button ID="btn_CreateSI" runat="server" Text="Create Sales Invoice" 
                                    OnClick="btn_CreateSI_Click" Visible="False" Width="144px" />
                                <asp:Button ID="btn_CancelProcess" runat="server" 
                                    onclick="btn_CancelProcess_Click" Text="Cancel Process" Width="119px" />
                            </td>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                    </table>
                </div>
            </asp:View>
            <asp:View ID="View17" runat="server">
                <table class="style1">
                    <tr>
                        <td>
                            <div class="style2">
                            <h1>Reports</h1>
                                <div class="style2">
                                    <asp:UpdatePanel ID="UpdatePanel8" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <asp:Timer ID="Timer8" runat="server" Interval="1000" OnTick="Timer8_Tick">
                                            </asp:Timer>
                                            <asp:Panel ID="Panel2" runat="server" Height="29px" Width="250px">
                                                New Reports:
                                                <asp:Label ID="lbl_YesNotif" runat="server" Font-Bold="True" Font-Names="Castellar" 
                                                    ForeColor="#00CC00" Text="0" Visible="False"></asp:Label>
                                                <asp:Label ID="lbl_noNotif" runat="server" Font-Bold="True" Font-Names="Castellar" 
                                                    ForeColor="#FF3300" Visible="False">0</asp:Label>
                                            </asp:Panel>
                                        </ContentTemplate>
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger ControlID="Timer1" EventName="Tick" />
                                        </Triggers>
                                    </asp:UpdatePanel>
                                    Category:<asp:DropDownList ID="ddwn_Categories" runat="server" 
                                        AutoPostBack="True">
                                        <asp:ListItem>Units</asp:ListItem>
                                        <asp:ListItem>Spare Parts</asp:ListItem>
                                    </asp:DropDownList>
                                    <br />
                                    <asp:Button ID="btn_refsh" runat="server" Text="Refresh List" 
                                        onclick="btn_refsh_Click" />
                                </div>
                                <div style="text-align: center">
                                    <asp:GridView ID="grid_Reports" runat="server" AutoGenerateSelectButton="True" 
                                        BackColor="White" BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px" 
                                        CellPadding="3" ForeColor="Black" GridLines="Vertical" 
                                        OnSelectedIndexChanged="grid_Reports_SelectedIndexChanged" Width="879px">
                                        <AlternatingRowStyle BackColor="#CCCCCC" />
                                        <FooterStyle BackColor="#CCCCCC" />
                                        <HeaderStyle BackColor="Black" Font-Bold="True" ForeColor="White" />
                                        <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
                                        <SelectedRowStyle BackColor="#000099" Font-Bold="True" ForeColor="White" />
                                        <SortedAscendingCellStyle BackColor="#F1F1F1" />
                                        <SortedAscendingHeaderStyle BackColor="#808080" />
                                        <SortedDescendingCellStyle BackColor="#CAC9C9" />
                                        <SortedDescendingHeaderStyle BackColor="#383838" />
                                    </asp:GridView>
                                    <div style="text-align: left"> <asp:Button ID="btn_DelReports" runat="server" Text="Delete Report" 
                                        style="text-align: left" />
                                    </div>
                                   
                                </div>
                            </div>
                        </td>
                    </tr>
                </table>
            </asp:View>
            <asp:View ID="View11" runat="server">
                <div>
                    <asp:Button ID="btn_OrderReports" runat="server" Text="Order Report" 
                        onclick="btn_OrderReports_Click" />
                    <asp:Button ID="btn_SIReport" runat="server" Text="Sales Invoice" 
                        onclick="btn_SIReport_Click" />
                    <asp:Button ID="btn_BillingReport" runat="server" Text="Billing Report" 
                        onclick="btn_BillingReport_Click" />
                    <asp:Button ID="btn_DeliveryReport" runat="server" Text="Delivery Report" 
                        onclick="btn_DeliveryReport_Click" />
                </div>
                <asp:MultiView ID="MultiView5" runat="server">
                    
                    
                    
                    <asp:View ID="View12" runat="server">
                        <h1>Order Report</h1>
                        <div>

                            <table class="style1">
                                <tr>
                                    <td>
                                        P.O.Number: 
                                        <asp:TextBox ID="txt_poNumbers" runat="server" ReadOnly="True"></asp:TextBox></td>
                                </tr>
                                <tr>
                                    <td>
                                        Date:
                                        <asp:TextBox ID="txt_DatePO" runat="server" ReadOnly="True"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>

                        </div>
                        <div>

                            <table class="style1">
                                <tr>
                                    <td class="style47">
                                        <asp:GridView ID="grid_orderReport1" runat="server">
                                        </asp:GridView>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="lbl_Ads" runat="server" Text="Added Item/s to this P.O." 
                                            Visible="False"></asp:Label>
                                        <div class="style47">
                                            <asp:GridView ID="grid_orderReport2" runat="server">
                                            </asp:GridView>
                                        </div>
                                    </td>
                                </tr>
                            </table>

                        </div>
                    </asp:View>
                    <asp:View ID="View13" runat="server">
                        <div>
                            <h1>
                                Sales Invoice</h1>
                        </div>
                        <asp:MultiView ID="MultiView6" runat="server">
                            <asp:View ID="View16" runat="server">
                                <asp:GridView ID="grid_invoices" runat="server" AutoGenerateSelectButton="True" 
                                    onselectedindexchanged="grid_invoices_SelectedIndexChanged" 
                                    style="text-align: center" Width="773px">
                                </asp:GridView>
                            </asp:View>
                            <asp:View ID="View18" runat="server">
                            <div>
                            <table class="style1">
                                <tr>
                                    <td>
                                        &nbsp;</td>
                                    <td style="text-align: right">
                                        <asp:Button ID="Button26" runat="server" Text="Back" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        P.O. No.:
                                        <asp:Label ID="lbl_PONumber" runat="server" ForeColor="#0066FF"></asp:Label>
                                    </td>
                                    <td>
                                        Invoice No.:
                                        <asp:Label ID="lbl_SalesInvoice" runat="server" ForeColor="#0066FF"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Date Received:
                                        <asp:Label ID="lbl_DateReceived" runat="server" ForeColor="#0066FF"></asp:Label>
                                    </td>
                                    <td>
                                        Delivery Date: <asp:Label ID="lbl_Delivery" runat="server" ForeColor="#0066FF"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <div>
                            <asp:GridView ID="grid_aSI" runat="server" BackColor="White" 
                                BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" 
                                ForeColor="Black" GridLines="Vertical" style="text-align: center" Width="821px">
                                <AlternatingRowStyle BackColor="#CCCCCC" />
                                <FooterStyle BackColor="#CCCCCC" />
                                <HeaderStyle BackColor="Black" Font-Bold="True" ForeColor="White" />
                                <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
                                <SelectedRowStyle BackColor="#000099" Font-Bold="True" ForeColor="White" />
                                <SortedAscendingCellStyle BackColor="#F1F1F1" />
                                <SortedAscendingHeaderStyle BackColor="#808080" />
                                <SortedDescendingCellStyle BackColor="#CAC9C9" />
                                <SortedDescendingHeaderStyle BackColor="#383838" />
                            </asp:GridView>
                            <br />
                            <asp:Label ID="lbl_AddedItems" runat="server" 
                                Text="Item/s Added To this P.O. :" Visible="False"></asp:Label>
                            <asp:GridView ID="grid_bSI" runat="server" BackColor="White" 
                                BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" 
                                EnableTheming="True" ForeColor="Black" GridLines="Vertical" 
                                style="text-align: center" Width="821px">
                                <AlternatingRowStyle BackColor="#CCCCCC" />
                                <FooterStyle BackColor="#CCCCCC" />
                                <HeaderStyle BackColor="Black" Font-Bold="True" ForeColor="White" />
                                <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
                                <SelectedRowStyle BackColor="#000099" Font-Bold="True" ForeColor="White" />
                                <SortedAscendingCellStyle BackColor="#F1F1F1" />
                                <SortedAscendingHeaderStyle BackColor="#808080" />
                                <SortedDescendingCellStyle BackColor="#CAC9C9" />
                                <SortedDescendingHeaderStyle BackColor="#383838" />
                            </asp:GridView>
                        </div>
                        <div>
                            <table class="style1">
                                <tr>
                                    <td>
                                        Sub-Total:
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txt_sTotal" runat="server" ReadOnly="True"></asp:TextBox>
                                    </td>
                                    <td>
                                        Service Charge:
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txt_SCharge" runat="server" ReadOnly="True"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Discount:
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txt_Disc" runat="server" ReadOnly="True"></asp:TextBox>
                                    </td>
                                    <td>
                                        Total Service Charge:
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txt_TotalsCharge" runat="server" ReadOnly="True"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Discounted Amount:
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txt_DiscAmount" runat="server" ReadOnly="True"></asp:TextBox>
                                    </td>
                                    <td>
                                        Total:
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txt_GTotal" runat="server" ReadOnly="True"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        VAT:
                                        <asp:Label ID="lbl_Vatt" runat="server"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txt_Vatt" runat="server" ReadOnly="True"></asp:TextBox>
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                </tr>
                            </table>
                        </div>
                            </asp:View>
                        </asp:MultiView>
                    </asp:View>
                    <asp:View ID="View14" runat="server">
                        <h1>Billing Report</h1>
                        <asp:MultiView ID="MultiView8" runat="server">
                            <asp:View ID="View22" runat="server">
                                <asp:GridView ID="grid_Billing" runat="server" AutoGenerateSelectButton="True" 
                                    onselectedindexchanged="grid_Billing_SelectedIndexChanged" 
                                    style="text-align: center" Width="689px">
                                </asp:GridView>
                            </asp:View>
                            <asp:View ID="View23" runat="server">
                                <asp:Button ID="btn_Bill" runat="server" onclick="btn_Bill_Click" Text="Bill" 
                                    Width="70px" />
                                <asp:Button ID="btn_Payment" runat="server" onclick="btn_Payment_Click" 
                                    Text="Payment" />
                                <asp:MultiView ID="MultiView9" runat="server">
                                    <asp:View ID="View24" runat="server">
                                        <table class="style1">
                                            <tr>
                                                <td colspan="2">
                                                    P.O. Number:
                                                    <asp:Label ID="lbl_BillPO" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="2">
                                                    Date:
                                                    <asp:Label ID="lbl_DatePO" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="2">
                                                    Invoice Number:
                                                    <asp:Label ID="lbl_InvcNumber" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="2">
                                                    <asp:GridView ID="grid_BillingA" runat="server" AutoGenerateSelectButton="True" 
                                                        style="text-align: center" Width="689px">
                                                    </asp:GridView>
                                                    <table class="style1">
                                                        <tr>
                                                            <td>
                                                                &nbsp;</td>
                                                        </tr>
                                                    </table>
                                                    <asp:Label ID="lbl_Adde" runat="server" Text="Added Item/s to this P.O. :" 
                                                        Visible="False"></asp:Label>
                                                    <asp:GridView ID="grid_BillingB" runat="server" AutoGenerateSelectButton="True" 
                                                        style="text-align: center" Width="689px">
                                                    </asp:GridView>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="style34">
                                                    Sub-Total:</td>
                                                <td>
                                                    <asp:TextBox ID="txt_subtotal" runat="server" ReadOnly="True"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="style34">
                                                    Tax:
                                                    <asp:Label ID="lbl_Taxx" runat="server"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txt_taxs" runat="server" ReadOnly="True"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="style34">
                                                    Shipping:</td>
                                                <td>
                                                    <asp:TextBox ID="txt_shiping" runat="server" ReadOnly="True"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="style34">
                                                    Total:</td>
                                                <td>
                                                    <asp:TextBox ID="txt_tots" runat="server" ReadOnly="True"></asp:TextBox>
                                                </td>
                                            </tr>
                                        </table>
                                    </asp:View>
                                    <asp:View ID="View25" runat="server">
                                        <table class="style1">
                                            <tr>
                                                <td>
                                                    <table class="style1">
                                                        <tr>
                                                            <td class="style57">
                                                                Purchase Order Number:</td>
                                                            <td>
                                                                <asp:Label ID="lbl_POnum" runat="server"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="style57">
                                                                Date Received:</td>
                                                            <td>
                                                                <asp:Label ID="lbl_DateRec" runat="server"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="style57">
                                                                Invoice Number:</td>
                                                            <td>
                                                                <asp:Label ID="lbl_InvNum" runat="server"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="style57">
                                                                Total Bill:</td>
                                                            <td>
                                                                <asp:Label ID="lbl_TotalBill" runat="server"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="style57">
                                                                Amount Paid:</td>
                                                            <td>
                                                                <asp:Label ID="lbl_AmountPaid" runat="server"></asp:Label>
                                                                &nbsp;<asp:Button ID="btn_UpdatePayment" runat="server" 
                                                                    onclick="btn_UpdatePayment_Click" Text="Update" />
                                                                <asp:TextBox ID="txt_Payments" runat="server" Visible="False"></asp:TextBox>
                                                                <asp:Button ID="btn_AddPayment" runat="server" onclick="btn_AddPayment_Click" 
                                                                    Text="Add" Visible="False" Width="41px" />
                                                                <asp:Button ID="Cancel_Payment" runat="server" onclick="Cancel_Payment_Click" 
                                                                    Text="Cancel" Visible="False" />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="style57">
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
                    <asp:View ID="View15" runat="server">
                        <h1>Delivery Report/Acceptance</h1>
                        <asp:MultiView ID="MultiView7" runat="server">
                            <asp:View ID="View20" runat="server">
                                <asp:GridView ID="grid_DeliveryList" runat="server" 
                                    AutoGenerateSelectButton="True" 
                                    onselectedindexchanged="grid_DeliveryList_SelectedIndexChanged" 
                                    style="text-align: center" Width="743px">
                                </asp:GridView>
                            </asp:View>
                            <asp:View ID="View21" runat="server">
                            <div>&nbsp;<table class="style1">
                                <tr>
                                    <td class="style51">
                                        P.O. Number:</td>
                                    <td>
                                        <asp:TextBox ID="txt_delPO" runat="server" ReadOnly="True"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="style51">
                                        Date of Purchase:</td>
                                    <td>
                                        <asp:TextBox ID="txt_poDate" runat="server" ReadOnly="True"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="style51">
                                        Delivery Date:</td>
                                    <td>
                                        <asp:TextBox ID="txt_delDate" runat="server" ReadOnly="True"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="style51">
                                        Delivery Number:</td>
                                    <td>
                                        <asp:TextBox ID="txt_DelNumber" runat="server" ReadOnly="True" 
                                            ontextchanged="txt_DelNumber_TextChanged"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>

                        </div>
                        <div>
                            <asp:GridView ID="grid_Delivery1" runat="server" style="text-align: center" 
                                Width="577px">
                            </asp:GridView>
                            <table class="style1">
                                <tr>
                                    <td>
                                        &nbsp;</td>
                                </tr>
                            </table>
                            <asp:Label ID="lbl_Addeds" runat="server" Text="Added Item/s to this P.O." 
                                Visible="False"></asp:Label>
                            <asp:GridView ID="grid_Delivery2" runat="server" style="text-align: center" 
                                Width="577px">
                            </asp:GridView>
                            <table class="style1">
                                <tr>
                                    <td class="style55">
                                        <asp:Label ID="lbl_Note" runat="server" Font-Bold="True" ForeColor="#CC3300" 
                                            Text="Note: This P.O. includes:" Visible="False"></asp:Label>
                                        <br />
                                        <asp:Label ID="lbl_NoteContent" runat="server" Font-Names="Arial Unicode MS" 
                                            ForeColor="Blue" Visible="False"></asp:Label>
                                    </td>
                                    <td class="style52">
                                        &nbsp;</td>
                                    <td class="style53">
                                        &nbsp;</td>
                                    <td class="style56">
                                        &nbsp;</td>
                                    <td>
                                        &nbsp;</td>
                                </tr>
                                <tr>
                                    <td class="style55">
                                        Hauler:</td>
                                    <td class="style52">
                                        <asp:TextBox ID="txt_hauler" runat="server" ReadOnly="True">(None)</asp:TextBox>
                                    </td>
                                    <td class="style53">
                                        <asp:Label ID="lbl_SelectHauler" runat="server" Text="Select Hauler: " 
                                            Visible="False"></asp:Label>
                                    </td>
                                    <td class="style56">
                                        <asp:DropDownList ID="ddwn_haulers" runat="server" AutoPostBack="True" 
                                            onselectedindexchanged="ddwn_haulers_SelectedIndexChanged" Visible="False">
                                        </asp:DropDownList>
                                    </td>
                                    <td>
                                        &nbsp;</td>
                                </tr>
                                <tr>
                                    <td class="style55">
                                        Plate Number:</td>
                                    <td class="style52">
                                        <asp:TextBox ID="txt_plate" runat="server" ReadOnly="True">(None)</asp:TextBox>
                                    </td>
                                    <td class="style53">
                                        <asp:Label ID="lbl_SelectPlate" runat="server" Text="Select Plate no.: " 
                                            Visible="False"></asp:Label>
                                    </td>
                                    <td class="style56">
                                        <asp:DropDownList ID="ddwn_plates" runat="server" AutoPostBack="True" 
                                            onselectedindexchanged="ddwn_plates_SelectedIndexChanged" Visible="False">
                                        </asp:DropDownList>
                                    </td>
                                    <td rowspan="5">
                                        &nbsp;</td>
                                </tr>
                                <tr>
                                    <td class="style55">
                                        Gate Pass No.:</td>
                                    <td class="style52">
                                        <asp:TextBox ID="txt_GatePass" runat="server" ReadOnly="True">(None)</asp:TextBox>
                                    </td>
                                    <td class="style53">
                                        &nbsp;</td>
                                    <td class="style56">
                                        &nbsp;</td>
                                </tr>
                                <tr>
                                    <td class="style55">
                                        &nbsp;</td>
                                    <td class="style52">
                                        <asp:Button ID="btn_Set" runat="server" onclick="btn_Set_Click" Text="Set" 
                                            Width="63px" />
                                        <asp:Button ID="btn_Confirm" runat="server" onclick="btn_Confirm_Click" 
                                            Text="Confirm" />
                                    </td>
                                    <td class="style53">
                                        <asp:Label ID="lbl_Include" runat="server" Text="Include:" Visible="False"></asp:Label>
                                    </td>
                                    <td class="style56">
                                        &nbsp;</td>
                                </tr>
                                <tr>
                                    <td class="style55">
                                        &nbsp;</td>
                                    <td class="style52">
                                        &nbsp;</td>
                                    <td class="style53">
                                        <asp:CheckBoxList ID="checks_Notes" runat="server" Visible="False" 
                                            Width="138px">
                                            <asp:ListItem Selected="True">Standard Tools</asp:ListItem>
                                            <asp:ListItem Selected="True">Keys</asp:ListItem>
                                            <asp:ListItem Selected="True">Manuals</asp:ListItem>
                                            <asp:ListItem Selected="True">Mirrors</asp:ListItem>
                                            <asp:ListItem Selected="True">Batteries</asp:ListItem>
                                            <asp:ListItem Selected="True">Service Booklets</asp:ListItem>
                                        </asp:CheckBoxList>
                                    </td>
                                    <td class="style56">
                                        &nbsp;</td>
                                </tr>
                                <tr>
                                    <td class="style55">
                                        &nbsp;</td>
                                    <td class="style52">
                                        &nbsp;</td>
                                    <td class="style53">
                                        <asp:Button ID="btn_ok" runat="server" onclick="btn_ok_Click" Text="OK" 
                                            Visible="False" Width="74px" />
                                    </td>
                                    <td class="style56">
                                        &nbsp;</td>
                                </tr>
                                <tr>
                                    <td class="style55">
                                        &nbsp;</td>
                                    <td class="style52">
                                        &nbsp;</td>
                                    <td class="style53">
                                        &nbsp;</td>
                                    <td class="style56">
                                        &nbsp;</td>
                                    <td>
                                        &nbsp;</td>
                                </tr>
                            </table>
                        </div>
                            </asp:View>
                        </asp:MultiView>
                    </asp:View>
                </asp:MultiView>
            </asp:View>
            <asp:View ID="View19" runat="server">
                <h1>Inventory</h1>
                <table class="style1">
                        <tr>
                            <td>
                                Select Category:
                                <asp:DropDownList ID="ddwn_cats" runat="server" AutoPostBack="True">
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
                                    ConnectionString="<%$ ConnectionStrings:HPILogInConnectionString %>" 
                                    SelectCommand="SELECT [ModelCode], [Description], [Color], [Quantity], [Status] FROM [SystemModelsTBL]">
                                </asp:SqlDataSource>
                                <asp:SqlDataSource ID="src_InvParts" runat="server" 
                                    ConnectionString="<%$ ConnectionStrings:HPILogInConnectionString %>" 
                                    SelectCommand="SELECT [PartNumber], [Description], [Color], [Quantity], [Status] FROM [SystemPartsTBL]">
                                </asp:SqlDataSource>
                                <asp:GridView ID="grid_Inv" runat="server" AutoGenerateSelectButton="True" 
                                    BackColor="White" BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px" 
                                    CellPadding="3" ForeColor="Black" GridLines="Vertical" 
                                    onselectedindexchanged="grid_Inv_SelectedIndexChanged" 
                                    style="text-align: center" Width="749px">
                                    <AlternatingRowStyle BackColor="#CCCCCC" />
                                    <FooterStyle BackColor="#CCCCCC" />
                                    <HeaderStyle BackColor="Black" Font-Bold="True" ForeColor="White" />
                                    <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
                                    <SelectedRowStyle BackColor="#000099" Font-Bold="True" ForeColor="White" />
                                    <SortedAscendingCellStyle BackColor="#F1F1F1" />
                                    <SortedAscendingHeaderStyle BackColor="#808080" />
                                    <SortedDescendingCellStyle BackColor="#CAC9C9" />
                                    <SortedDescendingHeaderStyle BackColor="#383838" />
                                </asp:GridView>
                                
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Add Quantity:
                                <asp:TextBox ID="txt_Quans" runat="server" MaxLength="4"></asp:TextBox>
                                <asp:Button ID="btn_SaveQ" runat="server" Text="Add" 
                                    onclick="btn_SaveQ_Click" Width="48px" />
                                <asp:Button ID="btn_ClearQ" runat="server" Text="Clear" 
                                    onclick="btn_ClearQ_Click" />
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
                                    Text="Edit" Visible="False" />
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
            <asp:View ID="View26" runat="server">
                <h1>
                    Stock Report</h1>
                &nbsp;<div style="text-align: right">
                    <asp:Button ID="btn_Backs" runat="server" onclick="btn_Backs_Click" 
                        Text="Back" />
                </div>
                <table class="style1">
                    <tr>
                        <td>
                            P.O. Number:
                            <asp:Label ID="lbl_PONumba" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:GridView ID="grid_StockReport1" runat="server" BackColor="White" 
                                BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" 
                                ForeColor="Black" GridLines="Vertical" style="text-align: center" Width="747px">
                                <AlternatingRowStyle BackColor="#CCCCCC" />
                                <FooterStyle BackColor="#CCCCCC" />
                                <HeaderStyle BackColor="Black" Font-Bold="True" ForeColor="White" />
                                <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
                                <SelectedRowStyle BackColor="#000099" Font-Bold="True" ForeColor="White" />
                                <SortedAscendingCellStyle BackColor="#F1F1F1" />
                                <SortedAscendingHeaderStyle BackColor="#808080" />
                                <SortedDescendingCellStyle BackColor="#CAC9C9" />
                                <SortedDescendingHeaderStyle BackColor="#383838" />
                            </asp:GridView>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lbl_SecStockReport" runat="server" 
                                Text="Secondary P.O. Stock Report" Visible="False"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:GridView ID="grid_StockReport2" runat="server" BackColor="White" 
                                BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" 
                                ForeColor="Black" GridLines="Vertical" style="text-align: center" Width="754px">
                                <AlternatingRowStyle BackColor="#CCCCCC" />
                                <FooterStyle BackColor="#CCCCCC" />
                                <HeaderStyle BackColor="Black" Font-Bold="True" ForeColor="White" />
                                <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
                                <SelectedRowStyle BackColor="#000099" Font-Bold="True" ForeColor="White" />
                                <SortedAscendingCellStyle BackColor="#F1F1F1" />
                                <SortedAscendingHeaderStyle BackColor="#808080" />
                                <SortedDescendingCellStyle BackColor="#CAC9C9" />
                                <SortedDescendingHeaderStyle BackColor="#383838" />
                            </asp:GridView>
                        </td>
                    </tr>
                </table>
            </asp:View>
            <asp:View ID="View27" runat="server">
            <div>&nbsp;<h1>
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
                                    <table class="style1">
                                        <tr>
                                            <td>
                                                <asp:Button ID="btn_Inbox" runat="server" Text="Inbox" 
                                                    onclick="btn_Inbox_Click" />
                                                <asp:Button ID="btn_Sent" runat="server" Text="Sent" />
                                                <asp:Button ID="btn_Drafts" runat="server" Text="Drafts" />
                                                <asp:Button ID="btn_FilesUploaded" runat="server" Text="Files Uploaded" />
                                            </td>
                                        </tr>
                                    </table>
                                    <asp:MultiView ID="MultiView10" runat="server">
                                        <asp:View ID="View28" runat="server">
                                            <br />
                                            Inbox<asp:GridView ID="grid_Inbox" runat="server" AutoGenerateSelectButton="True" 
                                                Style="text-align: center" Width="466px">
                                            </asp:GridView>
                                            <asp:Button ID="Button4" runat="server" Text="Delete" />
                                        </asp:View>
                                        <asp:View ID="View29" runat="server">
                                            <br />
                                            Sent<asp:GridView ID="GridView1" runat="server" AutoGenerateSelectButton="True" 
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
                                            <asp:Button ID="Button9" runat="server" Text="Delete" />
                                        </asp:View>
                                        <asp:View ID="View30" runat="server">
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
                                        <asp:View ID="View31" runat="server">
                                            <asp:GridView ID="GridView6" runat="server" AutoGenerateSelectButton="True" 
                                                Style="text-align: center" Width="466px">
                                            </asp:GridView>
                                            <asp:Button ID="Button27" runat="server" Text="Delete" />
                                        </asp:View>
                                    </asp:MultiView>
                                </div>
                            </td>
                            <td>
                            <asp:Button runat="server" Text="Compose" ID="btn_Compose" 
                                    onclick="btn_Compose_Click" />
                            <asp:Button runat="server" Text="Upload" ID="btn_Upload" 
                                    onclick="btn_Upload_Click" />
                                <asp:MultiView ID="MultiView11" runat="server">
                                    <asp:View ID="View32" runat="server">
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
                                                <asp:Button ID="btn_Reply" runat="server" Text="Reply" />
                                                <asp:Button ID="Button22" runat="server" Text="Update" Visible="False" />
                                                <asp:Button ID="btn_SendMess" runat="server" Text="Send" 
                                                    onclick="btn_SendMess_Click" />
                                            </td>
                                        </tr>
                                    </table>
                                    </div>
                                    </asp:View>
                                    <asp:View ID="View33" runat="server">
                                    <div>
                                        <asp:FileUpload ID="FileUpload1" runat="server" />
                                        <asp:Button ID="btn_Up" runat="server" Text="Upload and Send File" />
                                        </div>
                                    </asp:View>
                                </asp:MultiView>
                            </td>
                        </tr>
                    </table>
            </asp:View>
        </asp:MultiView>
    </div>
    </form>
</body>
</html>
