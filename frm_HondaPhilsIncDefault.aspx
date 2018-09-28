<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="frm_HondaPhilsIncDefault.aspx.cs"
    Inherits="HondaMagnacycleProcurementProject.frm_HondaPhilsIncDefault" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .style1
        {
            width: 1092px;
        }
        .style2
        {
            width: 100%;
        }
        .style3
        {
            text-align: left;
        }
        .style4
        {
            text-align: left;
            width: 435px;
        }
        .style5
        {
            width: 472px;
            text-align: right;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table class="style2">
            <tr>
                <td>
                    <asp:Button runat="server" Text="System Administrators" ID="btn_SysAds" OnClick="btn_SysAds_Click" />
                    <asp:Button runat="server" Text="Activity Log" ID="btn_ActLog" OnClick="btn_ActLog_Click"
                        Visible="False" />
                    <asp:Button ID="btn_Setting" runat="server" OnClick="btn_Setting_Click" Text="Account Setting" />
                </td>
                <td style="text-align: right">
                    <asp:Button runat="server" Text="Sign Out" ID="btn_SOut" OnClick="btn_SOut_Click" />
                </td>
            </tr>
        </table>
    </div>
    <div>
        <asp:MultiView ID="MultiView1" runat="server">
            <asp:View ID="View1" runat="server">
                <div>
                    <table class="style1">
                        <tr>
                            <td colspan="6">
                                <h1>
                                    Honda Philippines Inc. System Administrator</h1>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="6">
                                &nbsp;
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
                                    BackColor="#CCCCCC" BorderColor="#999999" BorderStyle="Solid" BorderWidth="3px"
                                    CellPadding="4" Width="715px" AutoGenerateColumns="False" DataKeyNames="AdminID"
                                    DataSourceID="src_PrimaryAdmins" 
                                    OnSelectedIndexChanged="grid_superAdmins_SelectedIndexChanged" CellSpacing="2" 
                                    ForeColor="Black">
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
                                    <PagerStyle BackColor="#CCCCCC" ForeColor="Black" HorizontalAlign="Left" />
                                    <RowStyle BackColor="White" />
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
                                <asp:Button ID="btn_sAdEdit" runat="server" Text="Edit" OnClick="btn_sAdEdit_Click"
                                    Visible="False" />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="6" style="text-align: left">
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td colspan="6" style="text-align: left; font-size: 25px;">
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td class="style27" style="font-size: 15px;" colspan="6">
                                &nbsp;
                            </td>
                        </tr>
                    </table>
                </div>
            </asp:View>
            <asp:View ID="View2" runat="server">
                <div>
                    Default Account Activity Log<br />
                    <table class="style2">
                        <tr>
                            <td class="style4">
                                Time
                            </td>
                            <td class="style3">
                                Activity Log
                            </td>
                        </tr>
                        <tr>
                            <td class="style4">
                                <asp:SqlDataSource ID="src_Time" runat="server" ConnectionString="<%$ ConnectionStrings:HPILogInConnectionString %>"
                                    SelectCommand="SELECT [LoggedIn], [LoggedOut] FROM [SystemTimeRecordTBL]"></asp:SqlDataSource>
                                <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" AutoGenerateSelectButton="True"
                                    BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px"
                                    CellPadding="3" DataSourceID="src_Time">
                                    <Columns>
                                        <asp:BoundField DataField="LoggedIn" HeaderText="LoggedIn" SortExpression="LoggedIn" />
                                        <asp:BoundField DataField="LoggedOut" HeaderText="LoggedOut" SortExpression="LoggedOut" />
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
                            <td class="style3">
                                <asp:SqlDataSource ID="SqlDataSource2" runat="server"></asp:SqlDataSource>
                                <asp:GridView ID="GridView1" runat="server">
                                </asp:GridView>
                            </td>
                        </tr>
                    </table>
                </div>
            </asp:View>
            <asp:View ID="View3" runat="server">
                <div>
                    <h1>
                        Account Settings</h1>
                    <table class="style2">
                        <tr>
                            <td class="style5">
                                Default Account Recovery Email:
                            </td>
                            <td>
                                <asp:Label ID="lbl_RecEmail" runat="server" Text="Label"></asp:Label>
                                &nbsp;&nbsp;
                                <asp:Button ID="btn_ChangeEmail" runat="server" OnClick="btn_ChangeEmail_Click" Text="Change Recovery Email" />
                            </td>
                        </tr>
                        <tr>
                            <td class="style5">
                                Default Account Password:
                            </td>
                            <td>
                                <asp:TextBox ID="txt_defPass" runat="server" ReadOnly="True" TextMode="Password"></asp:TextBox>
                                <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Change Password" />
                            </td>
                        </tr>
                        <tr>
                            <td class="style5">
                                &nbsp;
                            </td>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td class="style5">
                                <asp:Label ID="lbl_CurrentPass" runat="server" Text="Current Password:" Visible="False"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txt_CurrentPass" runat="server" MaxLength="30" TextMode="Password"
                                    Visible="False"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="style5">
                                <asp:Label ID="lbl_NewPassword" runat="server" Text="New Password:" Visible="False"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txt_newPass" runat="server" MaxLength="30" TextMode="Password" Visible="False"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="style5">
                                <asp:Label ID="lbl_retypePassword" runat="server" Text="Re-type new password:" Visible="False"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txt_retypePass" runat="server" MaxLength="30" TextMode="Password"
                                    Visible="False"></asp:TextBox>
                                <asp:Button ID="btn_Save" runat="server" OnClick="btn_Save_Click" Text="Save" Visible="False" />
                                <asp:Button ID="btn_CancelPass" runat="server" OnClick="btn_CancelPass_Click" Text="Cancel"
                                    Visible="False" />
                            </td>
                        </tr>
                        <tr>
                            <td class="style5">
                                &nbsp;
                            </td>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td class="style5">
                                <asp:Label ID="lbl_RecoveryEmail" runat="server" Text="New Recovery Email:" Visible="False"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txt_newEmail" runat="server" MaxLength="35" TextMode="Email" Visible="False"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="style5">
                                &nbsp;
                            </td>
                            <td>
                                <asp:Button ID="btn_SaveEmail" runat="server" OnClick="btn_SaveEmail_Click" Text="Save"
                                    Visible="False" />
                                <asp:Button ID="btn_CancelEmail" runat="server" Text="Cancel" Visible="False" OnClick="btn_CancelEmail_Click" />
                            </td>
                        </tr>
                    </table>
                </div>
            </asp:View>
        </asp:MultiView>
    </div>
    </form>
</body>
</html>
