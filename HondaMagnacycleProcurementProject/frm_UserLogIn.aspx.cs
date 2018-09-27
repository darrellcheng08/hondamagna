using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
//added namespaces
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Net;
using System.Net.Mail;


namespace HondaMagnacycleProcurementProject
{
    public partial class frm_UserLogIn : System.Web.UI.Page
    {
        //string AdminUserName;
        //string AdminPassword;
        //string UserName1;
        //string Password1;
        //string UserNam2;
        //string Password2;
        //string UserName3;
        //string Password3;
        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["MagnaAdministratorConnectionString"].ConnectionString);
        SqlConnection connsa = new SqlConnection(ConfigurationManager.ConnectionStrings["HPILogInConnectionString"].ConnectionString);
        protected void Page_Load(object sender, EventArgs e)
        {
            MultiView1.ActiveViewIndex = 0;

            string query = "Select UserName,Password from SystemUserTBL";
            SqlCommand com = new SqlCommand(query, conn);
            IDataReader r;
            conn.Open();
            r = com.ExecuteReader();

            r.Read();
            ViewState["UserName1"] = r.GetString(0).ToString();//secon row
            ViewState["Password1"] = r.GetString(1).ToString();

            r.Read();
            ViewState["UserName2"] = r.GetString(0).ToString();//third row
            ViewState["Password2"] = r.GetString(1).ToString();

            r.Read();
            ViewState["UserName3"] = r.GetString(0).ToString();//forth row
            ViewState["Password3"] = r.GetString(1).ToString();
            //Response.Write(UserName3+Password2);

            conn.Close();

            string query1 = "Select UserName,Password from SystemAdminsTBL";
            SqlCommand comm = new SqlCommand(query1, conn);
            IDataReader re;
            conn.Open();
            re = comm.ExecuteReader();
            re.Read();
            ViewState["AdminUserName1"] = re.GetString(0).ToString();//first row
            ViewState["AdminPassword1"] = re.GetString(1).ToString();

            re.Read();
            ViewState["AdminUserName2"] = re.GetString(0).ToString();//first row
            ViewState["AdminPassword2"] = re.GetString(1).ToString();

            //ibubukod sa palayo tatlo, dun sa db na SystemAdminTBL
            conn.Close();

            string queryhpi = "Select UserName,Password from SystemHPIUsersTBL";
            SqlCommand comme = new SqlCommand(queryhpi, connsa);
            IDataReader red;
            connsa.Open();
            red = comme.ExecuteReader();
            red.Read();
            ViewState["SuperAdminUserName1"] = red.GetString(0).ToString();//first row
            ViewState["SuperAdminPassword1"] = red.GetString(1).ToString();

            red.Read();
            ViewState["SuperAdminUserName2"] = red.GetString(0).ToString();//first row
            ViewState["SuperAdminPassword2"] = red.GetString(1).ToString();


            connsa.Close();

            connsa.Open();
            string deafult = "Select DefaultUsername, DefaultPassword, RecoveryEmail from  SystemDefaultAccountTBL";
            SqlCommand amdns = new SqlCommand(deafult, connsa);
            IDataReader ree;
            ree = amdns.ExecuteReader();
            ree.Read();
            ViewState["defuser"] = ree.GetString(0).ToString();
            ViewState["defpass"] = ree.GetString(1).ToString();
            ViewState["defrecmail"] = ree.GetString(2).ToString();
            connsa.Close();
        }
        
        protected void btn_LogIn_Click(object sender, EventArgs e)
        {
            
            if (txt_LogInUserName.Text == "")
            {
                lbl_UserValidation.Text = "Provide User Name";
                lbl_UserValidation.Visible = true;
            }
            else if (txt_LogInUserName.Text != "")
            {
                lbl_UserValidation.Visible = false;

                if (txt_LogInUserName.Text == ViewState["defuser"].ToString())
                {
                    //ViewState["reff"] = "1";
                    if (txt_LogInPassword.Text == "")
                    {
                        lbl_PasswordValidation.Text = "Provide Password";
                        lbl_PasswordValidation.Visible = true;
                    }
                    else if (txt_LogInPassword.Text != "")
                    {
                        lbl_PasswordValidation.Visible = false;
                        if (txt_LogInPassword.Text == ViewState["defpass"].ToString())
                        {
                            if (ViewState["defrecmail"].ToString() == "default")
                            {
                                MultiView1.ActiveViewIndex = 1;
                            }
                            else
                            {
                                Response.Redirect("~/frm_HondaPhilsIncDefault.aspx");
                                txt_LogInUserName.Text = "";
                            }
                        }
                        else if (txt_LogInPassword.Text != ViewState["defpass"].ToString())
                        {
                            lbl_PasswordValidation.Text = "Invalid Password";
                            lbl_PasswordValidation.Visible = true;
                            //Response.Write(ViewState["AdminPassword"].ToString()/* + ViewState["AdminUserName"].ToString()*/);
                        }
                    }
                }
                else if (txt_LogInUserName.Text == ViewState["SuperAdminUserName1"].ToString())
                {
                    //ViewState["reff"] = "1";
                    if (txt_LogInPassword.Text == "")
                    {
                        lbl_PasswordValidation.Text = "Provide Password";
                        lbl_PasswordValidation.Visible = true;
                    }
                    else if (txt_LogInPassword.Text != "")
                    {
                        lbl_PasswordValidation.Visible = false;
                        if (txt_LogInPassword.Text == ViewState["SuperAdminPassword1"].ToString())
                        {
                            Response.Redirect("~/frm_HondaPhilsInc.aspx?superAdmin=" + txt_LogInUserName.Text);
                            txt_LogInUserName.Text = "";
                        }
                        else if (txt_LogInPassword.Text != ViewState["SuperAdminPassword1"].ToString())
                        {
                            lbl_PasswordValidation.Text = "Invalid Password";
                            lbl_PasswordValidation.Visible = true;
                            //Response.Write(ViewState["AdminPassword"].ToString()/* + ViewState["AdminUserName"].ToString()*/);
                        }
                    }
                }
                else if (txt_LogInUserName.Text == ViewState["SuperAdminUserName2"].ToString())
                {
                    //ViewState["reff"] = "1";
                    if (txt_LogInPassword.Text == "")
                    {
                        lbl_PasswordValidation.Text = "Provide Password";
                        lbl_PasswordValidation.Visible = true;
                    }
                    else if (txt_LogInPassword.Text != "")
                    {
                        lbl_PasswordValidation.Visible = false;
                        if (txt_LogInPassword.Text == ViewState["SuperAdminPassword2"].ToString())
                        {
                            Response.Redirect("~/frm_HondaPhilsInc.aspx?superAdmin=" + txt_LogInUserName.Text);
                            txt_LogInUserName.Text = "";
                        }
                        else if (txt_LogInPassword.Text != ViewState["SuperAdminPassword2"].ToString())
                        {
                            lbl_PasswordValidation.Text = "Invalid Password";
                            lbl_PasswordValidation.Visible = true;
                            //Response.Write(ViewState["AdminPassword"].ToString()/* + ViewState["AdminUserName"].ToString()*/);
                        }
                    }
                }
                else if (txt_LogInUserName.Text == ViewState["AdminUserName1"].ToString())
                {
                    ViewState["reff"] = "1"; 
                    if (txt_LogInPassword.Text == "")
                    {
                        lbl_PasswordValidation.Text = "Provide Password";
                        lbl_PasswordValidation.Visible = true;
                    }
                    else if (txt_LogInPassword.Text != "")
                    {
                        lbl_PasswordValidation.Visible = false;
                        if (txt_LogInPassword.Text == ViewState["AdminPassword1"].ToString())
                        {
                            Response.Redirect("~/frm_MagnacycleAdministrator.aspx?Admin=" + txt_LogInUserName.Text);
                            txt_LogInUserName.Text = "";
                        }
                        else if (txt_LogInPassword.Text != ViewState["AdminPassword1"].ToString())
                        {
                            lbl_PasswordValidation.Text = "Invalid Password";
                            lbl_PasswordValidation.Visible = true;
                            //Response.Write(ViewState["AdminPassword"].ToString()/* + ViewState["AdminUserName"].ToString()*/);
                        }
                    }
                }
                else if (txt_LogInUserName.Text == ViewState["AdminUserName2"].ToString())
                {
                    ViewState["reff"] = "1"; 
                    if (txt_LogInPassword.Text == "")
                    {
                        lbl_PasswordValidation.Text = "Provide Password";
                        lbl_PasswordValidation.Visible = true;
                    }
                    else if (txt_LogInPassword.Text != "")
                    {
                        lbl_PasswordValidation.Visible = false;
                        if (txt_LogInPassword.Text == ViewState["AdminPassword2"].ToString())
                        {
                            Response.Redirect("~/frm_MagnacycleAdministrator.aspx?Admin=" + txt_LogInUserName.Text);
                            txt_LogInUserName.Text = "";
                        }
                        else if (txt_LogInPassword.Text != ViewState["AdminPassword2"].ToString())
                        {
                            lbl_PasswordValidation.Text = "Invalid Password";
                            lbl_PasswordValidation.Visible = true;
                            //Response.Write(ViewState["AdminPassword"].ToString()/* + ViewState["AdminUserName"].ToString()*/);
                        }
                    }
                }
                else if (txt_LogInUserName.Text == ViewState["UserName1"].ToString())
                {
                    ViewState["reff"] = "2"; 
                    if (txt_LogInPassword.Text == "")
                    {
                        lbl_PasswordValidation.Text = "Provide Password";
                        lbl_PasswordValidation.Visible = true;
                    }
                    else if (txt_LogInPassword.Text != "")
                    {
                        lbl_PasswordValidation.Visible = false;
                        if (txt_LogInPassword.Text == ViewState["Password1"].ToString())
                        {
                            Response.Redirect("~/frm_MagnacycleUser.aspx?User=" + txt_LogInUserName.Text);
                            txt_LogInUserName.Text = "";
                        }
                        else if (txt_LogInPassword.Text != ViewState["Password1"].ToString())
                        {
                            lbl_PasswordValidation.Text = "Invalid Password";
                            lbl_PasswordValidation.Visible = true;
                        }
                    }
                }
                else if (txt_LogInUserName.Text == ViewState["UserName2"].ToString())
                {
                    ViewState["reff"] = "2"; 
                    if (txt_LogInPassword.Text == "")
                    {
                        lbl_PasswordValidation.Text = "Provide Password";
                        lbl_PasswordValidation.Visible = true;
                    }
                    else if (txt_LogInPassword.Text != "")
                    {
                        lbl_PasswordValidation.Visible = false;
                        if (txt_LogInPassword.Text == ViewState["Password2"].ToString())
                        {
                            Response.Redirect("~/frm_MagnacycleUser.aspx?User=" + txt_LogInUserName.Text);
                            txt_LogInUserName.Text = "";
                        }
                        else if (txt_LogInPassword.Text != ViewState["Password2"].ToString())
                        {
                            lbl_PasswordValidation.Text = "Invalid Password";
                            lbl_PasswordValidation.Visible = true;
                        }
                    }
                }
                else if (txt_LogInUserName.Text == ViewState["UserName3"].ToString())
                {
                    ViewState["reff"] = "2"; 
                    if (txt_LogInPassword.Text == "")
                    {
                        lbl_PasswordValidation.Text = "Provide Password";
                        lbl_PasswordValidation.Visible = true;
                    }
                    else if (txt_LogInPassword.Text != "")
                    {
                        lbl_PasswordValidation.Visible = false;
                        if (txt_LogInPassword.Text == ViewState["Password3"].ToString())
                        {
                            Response.Redirect("~/frm_MagnacycleUser.aspx?User=" + txt_LogInUserName.Text);
                            txt_LogInUserName.Text = "";
                        }
                        else if (txt_LogInPassword.Text != ViewState["Password3"].ToString())
                        {
                            lbl_PasswordValidation.Text = "Invalid Password";
                            lbl_PasswordValidation.Visible = true;
                        }
                    }
                }
                else
                {
                    lbl_UserValidation.Text = "Invalid User Name.";
                    lbl_UserValidation.Visible = true;

                    Response.Write(ViewState["SuperAdminUserName1"] + "jjj " + ViewState["SuperAdminPassword1"] + " " + ViewState["SuperAdminUserName2"] + " " + ViewState["SuperAdminPassword2"]);
                }
                
            }
            
        }

        protected void btn_ForgotLogInPassword_Click(object sender, EventArgs e)
        {
            Response.Write(ViewState["reff"].ToString());
           // string userRef = ViewState["reff"];
            if (txt_LogInUserName.Text == "" || txt_LogInUserName.Text == null)
            {
                Response.Write("Enter UserName first");
            }
            else
            {
                Response.Write(ViewState["reff"].ToString());
                string unme = txt_LogInUserName.Text;
                if (ViewState["reff"].ToString() == "1")// admin
                {
                    string que = "select Email, UserName, Password from SystemAdminsTBL where UserName = '" + unme + "'";
                    SqlCommand coms = new SqlCommand(que, conn);
                    conn.Open();
                    SqlDataReader rdr = coms.ExecuteReader();
                    conn.Close();
                    if (rdr.Read())
                    {
                        string mail = rdr["Email"].ToString();
                        string uname = rdr["UserName"].ToString();
                        string pword = rdr["Password"].ToString();
                        //                                      from               to
                        MailMessage mm = new MailMessage("Slapvoi@gmail.com", mail);
                        mm.Subject = "Magnacycle Password Recovery";
                        mm.Body = string.Format("Hello : <h1>{0}</h1> is your User Name. <br/> Your password is <h1>{1}</h1>", uname, pword);
                        mm.IsBodyHtml = true;
                        SmtpClient smtp = new SmtpClient();
                        smtp.Host = "smtp.gmail.com";
                        smtp.EnableSsl = true;
                        // from sender
                        NetworkCredential nc = new NetworkCredential();
                        nc.UserName = "Slapvoi@gmail.com";
                        nc.Password = "btololabemorej";
                        // from sender
                        smtp.UseDefaultCredentials = false;
                        smtp.Credentials = nc;
                        smtp.Port = 587;
                        smtp.Send(mm);
                        Response.Write("send");
                    }
                    else
                    {
                        Response.Write("not send");
                        Response.Write(rdr.Read());
                    }
                }
                else if (ViewState["reff"] == "2")//user
                {
                    //string que = "select UserName, Password from SystemUserTBL where UserName = '" + unme + "'";
                    //SqlCommand coms = new SqlCommand(que, conn);
                    //conn.Open();
                    //SqlDataReader rdr = coms.ExecuteReader();
                    //conn.Close();

                    string query = "select EmployeeIDNumber, UserName, Password from SystemUserTBL where UserName = '" + unme + "'";
                    SqlCommand com = new SqlCommand(query, conn);
                    IDataReader r;
                    conn.Open();
                    r = com.ExecuteReader();

                    r.Read();
                    string id = r.GetString(0).ToString();
                    string UserName1 = r.GetString(0).ToString();//secon row
                    string Password1 = r.GetString(1).ToString();
                    conn.Close();

                    string query1 = "Select Email from SystemMagnacycleEmployeesTBL where EmployeeIDNumber = " + id;
                    SqlCommand comas = new SqlCommand(query1, conn);
                    IDataReader rs;
                    conn.Open();
                    rs = comas.ExecuteReader();

                    rs.Read();
                    string mel = rs.GetString(0).ToString();

                    conn.Close();

                    //if (rdr.Read())
                    //{
                    //    string mail = rdr["Email"].ToString();
                    //    string uname = rdr["UserName"].ToString();
                    //    string pword = rdr["Password"].ToString();
                        //                                      from               to
                        MailMessage mm = new MailMessage("Slapvoi@gmail.com", mel);
                        mm.Subject = "Magnacycle Password Recovery";
                        mm.Body = string.Format("Hello : <h1>{0}</h1> is your User Name. <br/> Your password is <h1>{1}</h1>", UserName1, Password1);
                        mm.IsBodyHtml = true;
                        SmtpClient smtp = new SmtpClient();
                        smtp.Host = "smtp.gmail.com";
                        smtp.EnableSsl = true;
                        // from sender
                        NetworkCredential nc = new NetworkCredential();
                        nc.UserName = "Slapvoi@gmail.com";
                        nc.Password = "btololabemorej";
                        // from sender
                        smtp.UseDefaultCredentials = false;
                        smtp.Credentials = nc;
                        smtp.Port = 587;
                        smtp.Send(mm);
                        Response.Write("send");
                    //}
                    //else
                    //{
                    //    Response.Write("not send");
                    //    Response.Write(rdr.Read());
                    //}
                }
            }






            ViewState["reff"] = null; 
        }

        protected void btn_LoginCont_Click(object sender, EventArgs e)
        {
            if (txt_LogInEmail.Text == "" || txt_LogInEmail.Text == null)
            {

            }
            else
            {
 connsa.Open();
            string q1 = "UPDATE SystemDefaultAccountTBL SET RecoveryEmail = '" + txt_LogInEmail.Text + "'";
            SqlCommand comm = new SqlCommand(q1, connsa);
            
            comm.ExecuteNonQuery();
            
            connsa.Close();
            Response.Redirect("~/frm_HondaPhilsIncDefault.aspx");
            }
            
        }
    }
}