<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="frm_UserLogIn.aspx.cs" Inherits="HondaMagnacycleProcurementProject.frm_UserLogIn" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .style1
        {
            width: 85%;
        }
        #Select1
        {
            width: 139px;
        }
        .style5
        {
            width: 183px;
            text-align: left;
        }
        
        .style6
        {
            text-align: left;
        }
        
        .style7
        {
            width: 100%;
        }
        .style8
        {
            text-align: right;
        }
        
    </style>
</head>
<body style="text-align: center">
    <form id="form1" runat="server">
    <asp:MultiView ID="MultiView1" runat="server">
        <asp:View ID="View1" runat="server">
            <div>
            <table align="center" class="style1">
        <tr>
            <td class="style5" rowspan="4">
                <asp:Image ID="HondaImage" runat="server" ImageUrl="~/Resources/Honda Logo.png" 
                    Width="177px" style="text-align: left" BorderStyle="Double"/>
            </td>
            <td class="style6">
                <asp:Label ID="lbl_Label" runat="server" style="text-align: center" 
                    Text="User Log In"></asp:Label>
            </td>
            <td>
                <asp:SqlDataSource ID="HondaAdministratorDataSource" runat="server" 
                    ConnectionString="<%$ ConnectionStrings:MagnaAdministratorConnectionString %>" 
                    SelectCommand="SELECT [UserName], [Password] FROM [SystemUserTBL]">
                </asp:SqlDataSource>
                <asp:SqlDataSource ID="HPIDataSource" runat="server" 
                    ConnectionString="<%$ ConnectionStrings:HPILogInConnectionString %>" 
                    SelectCommand="SELECT * FROM [SystemHPIUsersTBL]">
                </asp:SqlDataSource>
            </td>
        </tr>
        <tr>
            <td class="style6" colspan="2">
                <asp:Label ID="lbl_Username" runat="server" Text="User Name:"></asp:Label>
                <asp:TextBox ID="txt_LogInUserName" runat="server" Width="225px" MaxLength="55"></asp:TextBox>
                <asp:Label ID="lbl_UserValidation" runat="server" Text="Provide User Name" 
                    Visible="False"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="style6" colspan="2">
                <asp:Label ID="lbl_Password" runat="server" Text="Password:"></asp:Label>
                <asp:TextBox ID="txt_LogInPassword" runat="server" TextMode="Password" 
                    Width="233px" MaxLength="30"></asp:TextBox>
                <asp:Label ID="lbl_PasswordValidation" runat="server" Text="Provide Password" 
                    Visible="False"></asp:Label>
            </td>
        </tr>
        <tr>
            <td colspan="2" class="style6">
                <asp:Button ID="btn_LogIn" runat="server" onclick="btn_LogIn_Click" 
                    Text="Log In" />
                <asp:Button ID="btn_ForgotLogInPassword" runat="server" style="text-align: right" 
                    Text="Forgot Password" onclick="btn_ForgotLogInPassword_Click" />
            </td>
        </tr>
    </table>
            </div>
        </asp:View>
        <asp:View ID="View2" runat="server">
            <div>

                <table class="style7">
                    <tr>
                        <td class="style8">
                            Provide Email Address:</td>
                        <td class="style6">
                            <asp:TextBox ID="txt_LogInEmail" runat="server" MaxLength="35" TextMode="Email"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="style8">
                            &nbsp;</td>
                        <td class="style6">
                            <asp:Button ID="btn_LoginCont" runat="server" onclick="btn_LoginCont_Click" 
                                Text="Continue" />
                        </td>
                    </tr>
                </table>

            </div>
        </asp:View>
    </asp:MultiView>




    
    </form>
</body>

</html>