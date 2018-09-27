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
using System.Text.RegularExpressions;
using System.Web.Services;
using System.Collections.Specialized;
using System.IO;//namespace for Regex
//june 30 2018

namespace HondaMagnacycleProcurementProject
{
    public partial class frm_MagnacycleAdministrator : System.Web.UI.Page
    {
        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["MagnaAdministratorConnectionString"].ConnectionString);
        SqlConnection hpiconn = new SqlConnection(ConfigurationManager.ConnectionStrings["HPILogInConnectionString"].ConnectionString);
        SqlConnection magnaconn = new SqlConnection(ConfigurationManager.ConnectionStrings["MagnacycleConnectionString"].ConnectionString);
        SqlConnection messconn = new SqlConnection(ConfigurationManager.ConnectionStrings["MessagingConnectionString"].ConnectionString);
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int x = 2018;
                for (int a = x; a >= 1905; a--)
                {
                    ddwn_Year.Items.Add(new ListItem(a.ToString()));
                }
                
            }

            txt_AdminUserName.Text = Request.QueryString["Admin"];

            if (ddwn_tab1_Select.SelectedItem.Text == "Units")
            {
                grid_SentPO.DataSourceID = "src_SentUnitsPO";
                grid_SentPO.DataBind();
            }
            else if (ddwn_tab1_Select.SelectedItem.Text == "Spare Parts")
            {
                grid_SentPO.DataSourceID = "src_SentPartsPO";
                grid_SentPO.DataBind();
            }

            if (ddwn_cats.SelectedItem.Text == "Units")
            {
                grid_Inv.DataSourceID = "src_InvUnits";
                grid_Inv.DataBind();
            }
            else if (ddwn_cats.SelectedItem.Text == "Spare Parts")
            {
                grid_Inv.DataSourceID = "src_InvParts";
                grid_Inv.DataBind();
            }
            if (ddwn_Catss.SelectedItem.Text == "Units")
            {
                grid_BInv.DataSourceID = "src_InvBunits";
                grid_BInv.DataBind();
            }
            else if (ddwn_Catss.SelectedItem.Text == "Spare Parts")
            {
                grid_BInv.DataSourceID = "src_InvBparts";
                grid_BInv.DataBind();
            }
            
        }

        protected void btn_UsersMaintenance_Click(object sender, EventArgs e)
        {
            MultiView1.ActiveViewIndex = 2;
            MultiView2.ActiveViewIndex = 0;
           // grid_CurrentUsersRowCounter();
        }

        private void grid_CurrentUsersRowCounter()
        {
            ////string count = grid_CurrentUsers.Rows.Count.ToString();
            //int users;
            //if (count == "0")
            //{
            //    users = 3;
            //    Enabler();
            //}
            //else if (count == "1")
            //{
            //    users = 2;
            //    Enabler();
            //}
            //else if (count == "2")
            //{
            //    users = 1;
            //    Enabler();
            //}
            //else if (count == "3")
            //{
            //    users = 0;
            //    if (btn_AddSave.Text == "Add")
            //    {
            //        txt_EmployerIDNumber.Enabled = false;
            //        txt_FName.Enabled = false;
            //        txt_Mname.Enabled = false;
            //        txt_Surname.Enabled = false;
            //        ddwn_Gender.Enabled = false;
            //        txt_Age.Enabled = false;
            //        ddwn_Month.Enabled = false;
            //        ddwn_Day.Enabled = false;
            //        ddwn_Year.Enabled = false;
            //        txt_Citizenship.Enabled = false;
            //        txt_Email.Enabled = false;
            //        txt_UserName.Enabled = false;
            //        btn_GenerateUserName.Enabled = false;
            //        btn_AddSave.Enabled = false;
            //    }
            //}
        }

        private void Enabler()
        {
            if (btn_AddSave.Text == "Add")
            {
                txt_EmployerIDNumber.Enabled = true;
                txt_FName.Enabled = true;
                txt_Mname.Enabled = true;
                txt_Surname.Enabled = true;
                ddwn_Gender.Enabled = true;
                txt_Age.Enabled = true;
                ddwn_Month.Enabled = true;
                ddwn_Day.Enabled = true;
                ddwn_Year.Enabled = true;
                txt_Citizenship.Enabled = true;
                txt_Email.Enabled = true;
              
                btn_AddSave.Enabled = true;
            }
        }

        protected void btn_FrontEndUsers_Click(object sender, EventArgs e)
        {
            MultiView2.ActiveViewIndex = 0;
            grid_CurrentUsersRowCounter();
        }

        protected void btn_GenerateUserName_Click(object sender, EventArgs e)
        {
            //string fname = txt_FName.Text;
            //fname = Regex.Replace(fname, @"\s", "");
            //txt_UserName.Text = fname + ddwn_Month.SelectedItem.Value.ToString() + ddwn_Day.SelectedItem.Value.ToString() + "User";
        }

        protected void btn_AddSave_Click(object sender, EventArgs e)
        {
            if (btn_AddSave.Text == "Add")
            {

                int counters = grid_Employers.Rows.Count;
                for (int c = 0; c <= counters -1; c++)
                {
                    ViewState["idGetter"] = grid_Employers.Rows[c].Cells[1].Text;
                    ViewState["fnameGetter"] = grid_Employers.Rows[c].Cells[2].Text;
                    ViewState["mnameGetter"] = grid_Employers.Rows[c].Cells[3].Text;
                    ViewState["snameGetter"] = grid_Employers.Rows[c].Cells[4].Text;
                    ViewState["genderGetter"] = grid_Employers.Rows[c].Cells[5].Text;
                    ViewState["ageGetter"] = grid_Employers.Rows[c].Cells[6].Text;
                    ViewState["bdayGetter"] = grid_Employers.Rows[c].Cells[7].Text;
                    ViewState["citizenshipGetter"] = grid_Employers.Rows[c].Cells[8].Text;
                    //ViewState["emailGetter"] = grid_Employers.Rows[c].Cells[9].Text;

                    
                    if (ViewState["idGetter"].ToString() == txt_EmployerIDNumber.Text)
                    {
                        lbl_Confirm.Text = "This employer ID already exist." + "<br />" + "Generate new ID for the new employer you want to add?";
                        lbl_Confirm.Visible = true;
                        btn_Ok.Visible = true;
                        btn_Cancel.Visible = true;
                        break;
                    }
                    
                    //loop ulit
                }
                if (lbl_Confirm.Visible == true)
                {

                }
                else if (lbl_Confirm.Visible == false)
                {
                    ViewState["choice"] = null;
                    for (int c = 0; c <= counters - 1; c++)
                    {
                        ViewState["idGetta"] = grid_Employers.Rows[c].Cells[1].Text;
                        ViewState["fnameGetter"] = grid_Employers.Rows[c].Cells[2].Text;
                        ViewState["mnameGetter"] = grid_Employers.Rows[c].Cells[3].Text;
                        ViewState["snameGetter"] = grid_Employers.Rows[c].Cells[4].Text;
                        ViewState["genderGetter"] = grid_Employers.Rows[c].Cells[5].Text;
                        ViewState["ageGetter"] = grid_Employers.Rows[c].Cells[6].Text;
                        ViewState["bdayGetter"] = grid_Employers.Rows[c].Cells[7].Text;
                        ViewState["citizenshipGetter"] = grid_Employers.Rows[c].Cells[8].Text;

                        ViewState["bdayTester"] = ddwn_Month.SelectedItem.Text + " " + ddwn_Day.SelectedItem.Text + ", " + ddwn_Year.SelectedItem.Text;

                        if (ViewState["fnameGetter"].ToString() == txt_FName.Text && ViewState["mnameGetter"].ToString() == txt_Mname.Text && ViewState["snameGetter"].ToString() == txt_Surname.Text && ViewState["genderGetter"].ToString() == ddwn_Gender.Text && ViewState["ageGetter"].ToString() == txt_Age.Text && ViewState["bdayGetter"].ToString() == ViewState["bdayTester"].ToString() && ViewState["citizenshipGetter"].ToString() == txt_Citizenship.Text)
                        {
                            lbl_Confirm.Text = "This employer has same details with " + ViewState["idGetta"].ToString() + "<br />" + "Do you want to add this person anyway?";
                            lbl_Confirm.Visible = true;
                            btn_okD.Visible = true;
                            btn_cancelD.Visible = true;
                            break;
                        }
                    }
                    if (lbl_Confirm.Visible == true)
                    {

                    }
                    else if (lbl_Confirm.Visible == false)
                    {
                        employerAdder();
                        txt_EmployerIDNumber.ReadOnly = false;
                        txt_FName.ReadOnly = false;
                        txt_Mname.ReadOnly = false;
                        txt_Surname.ReadOnly = false;
                        ddwn_Gender.Enabled = true;
                        txt_Age.ReadOnly = false;
                        ddwn_Month.Enabled = true;
                        ddwn_Day.Enabled = true;
                        ddwn_Year.Enabled = true;
                        txt_Citizenship.ReadOnly = false;
                        txt_Email.ReadOnly = false;

                        txt_EmployerIDNumber.ReadOnly = false;
                        txt_EmployerIDNumber.Text = "";
                        txt_FName.Text = "";
                        txt_Mname.Text = "";
                        txt_Surname.Text = "";
                        ddwn_Gender.Text = "Male";
                        txt_Age.Text = "";
                        ddwn_Month.SelectedIndex = 0;
                        ddwn_Day.SelectedIndex = 0;
                        ddwn_Year.SelectedIndex = 0;
                        txt_Citizenship.Text = "";
                        txt_Email.Text = "";
                        btn_AddSave.Text = "Add";
                    }
                }
            }
            else if (btn_AddSave.Text == "Save")
            {
                ViewState["month"] = ddwn_Month.SelectedItem.Value;
                ViewState["day"] = ddwn_Day.SelectedItem.Value;
                int counters = grid_Employers.Rows.Count;
                for (int w = 0; w <= counters - 1; w++)
                {
                    ViewState["id"] = grid_Employers.Rows[w].Cells[1].Text;

                    if (ViewState["id"].ToString() == ViewState["editRef"].ToString())
                    {
                        ViewState["fGetter"] = grid_Employers.Rows[w].Cells[2].Text;
                        ViewState["mGetter"] = grid_Employers.Rows[w].Cells[3].Text;
                        ViewState["sGetter"] = grid_Employers.Rows[w].Cells[4].Text;
                        ViewState["gGetter"] = grid_Employers.Rows[w].Cells[5].Text;
                        ViewState["aGetter"] = grid_Employers.Rows[w].Cells[6].Text;
                        ViewState["bGetter"] = grid_Employers.Rows[w].Cells[7].Text;
                        ViewState["cGetter"] = grid_Employers.Rows[w].Cells[8].Text;
                        //ViewState["emailGetter"] = grid_Employers.Rows[c].Cells[9].Text;

                        ViewState["bDayTester"] = ddwn_Month.SelectedItem.Text + " " + ddwn_Day.SelectedItem.Text + ", " + ddwn_Year.SelectedItem.Text;

                        if (txt_EmployerIDNumber.Text == ViewState["editRef"].ToString())
                        {
                            for (int c = 0; c <= counters - 1; c++)
                            {
                                ViewState["idGettera"] = grid_Employers.Rows[c].Cells[1].Text;
                                ViewState["fGetter"] = grid_Employers.Rows[c].Cells[2].Text;
                                ViewState["mGetter"] = grid_Employers.Rows[c].Cells[3].Text;
                                ViewState["sGetter"] = grid_Employers.Rows[c].Cells[4].Text;
                                ViewState["gGetter"] = grid_Employers.Rows[c].Cells[5].Text;
                                ViewState["aGetter"] = grid_Employers.Rows[c].Cells[6].Text;
                                ViewState["bGetter"] = grid_Employers.Rows[c].Cells[7].Text;
                                ViewState["cGetter"] = grid_Employers.Rows[c].Cells[8].Text;

                                //ViewState["bdayTester"] = ddwn_Month.SelectedItem.Text + " " + ddwn_Day.SelectedItem.Text + ", " + ddwn_Year.SelectedItem.Text;

                                if (ViewState["fGetter"].ToString() == txt_FName.Text && ViewState["mGetter"].ToString() == txt_Mname.Text && ViewState["sGetter"].ToString() == txt_Surname.Text && ViewState["gGetter"].ToString() == ddwn_Gender.Text && ViewState["aGetter"].ToString() == txt_Age.Text && ViewState["bGetter"].ToString() == ViewState["bDayTester"].ToString() && ViewState["cGetter"].ToString() == txt_Citizenship.Text)
                                {
                                    lbl_Confirm.Text = "This employer has same details with " + ViewState["idGettera"].ToString() + "<br />" + "Do you want to update this person anyway?";
                                    lbl_Confirm.Visible = true;
                                    btn_OkSaver.Visible = true;
                                    btn_CancelSaver.Visible = true;
                                    break;
                                }
                            }
                            if (lbl_Confirm.Visible == true)
                            {

                            }
                            else if (lbl_Confirm.Visible == false)
                            {
                                systemUserUpdater();
                                txt_EmployerIDNumber.ReadOnly = false;
                                txt_FName.ReadOnly = false;
                                txt_Mname.ReadOnly = false;
                                txt_Surname.ReadOnly = false;
                                ddwn_Gender.Enabled = true;
                                txt_Age.ReadOnly = false;
                                ddwn_Month.Enabled = true;
                                ddwn_Day.Enabled = true;
                                ddwn_Year.Enabled = true;
                                txt_Citizenship.ReadOnly = false;
                                txt_Email.ReadOnly = false;

                                txt_EmployerIDNumber.ReadOnly = false;
                                txt_EmployerIDNumber.Text = "";
                                txt_FName.Text = "";
                                txt_Mname.Text = "";
                                txt_Surname.Text = "";
                                ddwn_Gender.Text = "Male";
                                txt_Age.Text = "";
                                ddwn_Month.SelectedIndex = 0;
                                ddwn_Day.SelectedIndex = 0;
                                ddwn_Year.SelectedIndex = 0;
                                txt_Citizenship.Text = "";
                                txt_Email.Text = "";
                                btn_AddSave.Text = "Add";
                            }
                        }
                        else
                        {
                            //for checking of errors
                            Response.Write("txt_EmployerIDNumber.Text != ViewState['editRef'].ToString()");
                            Response.Write(txt_EmployerIDNumber.Text + " " + ViewState["editRef"].ToString());
                            break;
                        }
                    }
                    else
                    {
                        //loop ulit
                    }
                }
                //unable to change user id 
            }
        }

        private void systemUserUpdater()
        {
            string q1 = "UPDATE SystemMagnacycleEmployeesTBL SET FirstName = '" + txt_FName.Text + "',MiddleName = '" + txt_Mname.Text + "',Surname = '" + txt_Surname.Text + "', Gender = '" + ddwn_Gender.Text + "', Age = '" + txt_Age.Text + "', Birthday = '" + ddwn_Month.SelectedItem.Text + " " + ddwn_Day.SelectedItem.Text + ", " + ddwn_Year.SelectedItem.Text + "' ,Citizenship = '" + txt_Citizenship.Text + "', Email = '" + txt_Email.Text + "' where EmployeeIDNumber = " + ViewState["editRef"].ToString();
            SqlCommand comm = new SqlCommand(q1, conn);
            conn.Open();
            comm.ExecuteNonQuery();
            conn.Close();
            grid_Employers.DataBind();

            string ffname = txt_FName.Text;
            string ssname = txt_Surname.Text;
            string bdaysm = ddwn_Month.SelectedItem.Value.ToString();
            string bdaysd = ddwn_Day.SelectedItem.Value.ToString();

            string genfname = ffname;
            genfname = Regex.Replace(genfname, @"\s", "");
            string orgigeneratedUname = genfname + bdaysm + bdaysd + "User";

            //check for current users
            int counter = grid_AuthorizedUsers.Rows.Count;
            if (counter == 0)
            {
                Response.Write("Counter = 0");//for checking
            }
            else
            {
                for (int a = 0; a <= counter - 1; a++)
                {
                    ViewState["tester"] = grid_AuthorizedUsers.Rows[a].Cells[1].Text;//Response.Write(ViewState["tester"].ToString() + ViewState["editRef"].ToString());
                    if (ViewState["tester"].ToString() == ViewState["editRef"].ToString())
                    {
                        conn.Open();
                        string q2 = "UPDATE SystemUserTBL SET FirstName = '" + ffname + "',Surname = '" + ssname + "' where  EmployeeIDNUmber = " + ViewState["tester"];
                        SqlCommand finalcommss = new SqlCommand(q2, conn);
                        //conn.Open();
                        finalcommss.ExecuteNonQuery();
                        conn.Close();

                        conn.Open();
                        string q3 = "Select UserName from SystemUserTBl where EmployeeIDNumber = " + ViewState["tester"];
                        SqlCommand comms = new SqlCommand(q3, conn);
                        IDataReader r;
                        r = comms.ExecuteReader();
                        r.Read();
                        //string employeridno = r.GetString(0);//first row
                        string retrievedUname = r.GetString(0);
                        conn.Close();

  //////////////////////////////////////                      //string retrieved = "";
 //////////////////////////////////////                       //string vals = retrievedUname;
  //////////////////////////////////////////                      //char val1 = vals[vals.Length - 1];
  ///////////////////////////////////////////////                      //char val2 = vals[vals.Length - 2];
  /////////////////////////////////////////                      //char val3 = vals[vals.Length - 3];
  ///////////////////////////////////////////////                      //if (val1 >= '0' && val1 <= '9')
 //////////////////////////////////////////////////////                       //{
  //////////////////////////////////////////////                      //    retrieved = val3.ToString() + val2.ToString() + val1.ToString();
 ////////////////////////////////////////////////                       //}
                        

                        conn.Open();
                        string qry = "UPDATE SystemUserTBL SET UserName = 'Removed' where EmployeeIDNumber = " + ViewState["tester"];
                        SqlCommand mand = new SqlCommand(qry, conn);
                        //conn.Open();
                        mand.ExecuteNonQuery();
                        conn.Close();

                        grid_AuthorizedUsers.DataBind();

                        int d = grid_AuthorizedUsers.Rows.Count;
                        string datas = "";
                        string newUname = "";
                        bool bol = false;
                        for (int x = 0; x <= d - 1; x++)
                        {
                            datas = grid_AuthorizedUsers.Rows[x].Cells[4].Text;
                            if (datas == orgigeneratedUname)
                            {
                                bol = true;
                                Random rnd = new Random();
                                int id = rnd.Next(100, 999);
                                string faname = ffname;
                                faname = Regex.Replace(faname, @"\s", "");
                                newUname = faname + bdaysm + bdaysd + "User" + id.ToString();

                                //update of username
                                conn.Open();
                                string q22 = "UPDATE SystemUserTBL SET UserName = '" + newUname + "' where EmployeeIDNumber = " + ViewState["tester"];
                                SqlCommand mande = new SqlCommand(q22, conn);
                                mande.ExecuteNonQuery();
                                conn.Close();

                                grid_AuthorizedUsers.DataBind();

                                break;
                            }
                        }
                        if (bol == false)
                        {
                          //  newUname = orgigeneratedUname;

                            //update of username
                            conn.Open();
                            string q22 = "UPDATE SystemUserTBL SET UserName = '" + orgigeneratedUname + "' where EmployeeIDNumber = " + ViewState["tester"];
                            SqlCommand mande = new SqlCommand(q22, conn);
                            mande.ExecuteNonQuery();
                            conn.Close();

                            grid_AuthorizedUsers.DataBind();
                        }
                        break;
                    }
                    else
                    {
                        //loop ulit
                    }
                }
            }
        }
        
        public void employerAdder()
        {
            conn.Open();
            try
            {
                string insert = "insert into SystemMagnacycleEmployeesTBL(EmployeeIDNumber,FirstName,MiddleName,Surname,Gender,Age,Birthday,Citizenship,Email)values(@EIDNumber,@FName,@MName,@SName,@Gdr,@Ages,@Bday,@Cship,@Emil)";
                SqlCommand comm = new SqlCommand(insert, conn);
                if (ViewState["choice"] == "ok")
                {
                    comm.Parameters.AddWithValue("@EIDNumber", ViewState["eid"].ToString());
                    ViewState["choice"] = null;
                    //comm.Parameters.AddWithValue("@UName", txt_UserName.Text);
                    comm.Parameters.AddWithValue("@FName", txt_FName.Text);
                    comm.Parameters.AddWithValue("@MName", txt_Mname.Text);
                    comm.Parameters.AddWithValue("@SName", txt_Surname.Text);
                    comm.Parameters.AddWithValue("@Gdr", ddwn_Gender.Text);
                    comm.Parameters.AddWithValue("@Ages", txt_Age.Text);
                    comm.Parameters.AddWithValue("@Bday", ddwn_Month.SelectedItem.Text + " " + ddwn_Day.SelectedItem.Text + ", " + ddwn_Year.SelectedItem.Text);
                    comm.Parameters.AddWithValue("@Cship", txt_Citizenship.Text);
                    comm.Parameters.AddWithValue("@Emil", txt_Email.Text);
                    comm.ExecuteNonQuery();
                    ViewState["month"] = ddwn_Month.SelectedItem.Value;
                    ViewState["day"] = ddwn_Day.SelectedItem.Value;
                    Response.Write("Addedchoice");
                    grid_Employers.DataBind();
                }
                else if (ViewState["choice"] == null || ViewState["choice1"] == "ok")
                {
                    comm.Parameters.AddWithValue("@EIDNumber", txt_EmployerIDNumber.Text);
                    //comm.Parameters.AddWithValue("@UName", txt_UserName.Text);
                    comm.Parameters.AddWithValue("@FName", txt_FName.Text);
                    comm.Parameters.AddWithValue("@MName", txt_Mname.Text);
                    comm.Parameters.AddWithValue("@SName", txt_Surname.Text);
                    comm.Parameters.AddWithValue("@Gdr", ddwn_Gender.Text);
                    comm.Parameters.AddWithValue("@Ages", txt_Age.Text);
                    comm.Parameters.AddWithValue("@Bday", ddwn_Month.SelectedItem.Text + " " + ddwn_Day.SelectedItem.Text + ", " + ddwn_Year.SelectedItem.Text);
                    comm.Parameters.AddWithValue("@Cship", txt_Citizenship.Text);
                    comm.Parameters.AddWithValue("@Emil", txt_Email.Text);
                    comm.ExecuteNonQuery();
                    ViewState["month"] = ddwn_Month.SelectedItem.Value;
                    ViewState["day"] = ddwn_Day.SelectedItem.Value;
                    Response.Write("Addedchoice");
                    grid_Employers.DataBind();
                }
                //if (ViewState["choice1"] == "ok")
                //{
                //    string inserts = "insert into MagnacycleEmployersTBL(EmployerIDNumber,FirstName,MiddleName,Surname,Gender,Age,Birthday,Citizenship,Email)values(@EIDNumba,@FName,@MName,@SName,@Gdr,@Ages,@Bday,@Cship,@Emil)";
                //    SqlCommand comma = new SqlCommand(inserts, conn);
                //    comma.Parameters.AddWithValue("@EIDNumba", txt_EmployerIDNumber.Text);
                //    //comm.Parameters.AddWithValue("@UName", txt_UserName.Text);
                //    comma.Parameters.AddWithValue("@FName", txt_FName.Text);
                //    comma.Parameters.AddWithValue("@MName", txt_Mname.Text);
                //    comma.Parameters.AddWithValue("@SName", txt_Surname.Text);
                //    comma.Parameters.AddWithValue("@Gdr", ddwn_Gender.Text);
                //    comma.Parameters.AddWithValue("@Ages", txt_Age.Text);
                //    comma.Parameters.AddWithValue("@Bday", ddwn_Month.SelectedItem.Text + " " + ddwn_Day.SelectedItem.Text + ", " + ddwn_Year.SelectedItem.Text);
                //    comma.Parameters.AddWithValue("@Cship", txt_Citizenship.Text);
                //    comma.Parameters.AddWithValue("@Emil", txt_Email.Text);
                //    comma.ExecuteNonQuery();
                //    ViewState["month"] = ddwn_Month.SelectedItem.Value;
                //    ViewState["day"] = ddwn_Day.SelectedItem.Value;
                //    Response.Write("Addedchoice1");
                //    grid_Employers.DataBind();
                //    ViewState["choice1"] = null;
                //}
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
            }
            conn.Close();
        }
        protected void grid_AuthorizedUsers_SelectedIndexChanged(object sender, EventArgs e)
        {
            ViewState["EmployerIDGetter"] = grid_Employers.SelectedRow.Cells[1].Text;
            //conn.Open();
            string que = "Select * from SystemMagnacycleEmployeesTBL where EmployeeIDNumber = " + ViewState["EmployerIDGetter"];
            SqlCommand commssy = new SqlCommand(que, conn);
            IDataReader rders;
            conn.Open();
            rders = commssy.ExecuteReader();
            rders.Read();
            ViewState["id1"] = rders.GetString(0).ToString();
            ViewState["fname1"] = rders.GetString(1).ToString();
            ViewState["mname1"] = rders.GetString(2).ToString();
            ViewState["sname1"] = rders.GetString(3).ToString();
            ViewState["gender1"] = rders.GetString(4).ToString();
            ViewState["age1"] = rders.GetInt32(5);
            ViewState["bday1"] = rders.GetString(6).ToString();
            ViewState["citi1"] = rders.GetString(7).ToString();
            ViewState["emil1"] = rders.GetString(8).ToString();
            conn.Close();

            txt_EmployerIDNumber.Text = ViewState["id1"].ToString();
            txt_FName.Text = ViewState["fname1"].ToString();
            txt_Mname.Text = ViewState["mname1"].ToString();
            txt_Surname.Text = ViewState["sname1"].ToString();
            ddwn_Gender.Text = ViewState["gender1"].ToString();
            txt_Age.Text = ViewState["age1"].ToString();

            //separeate and load bday
            string bday1 = ViewState["bday1"].ToString();
            string[] names1 = bday1.Split(' '); // "1" means stop splitting after one space
            string month1 = names1[0];
            string day1 = names1[1];
            string year1 = names1[2];
            string de1 = day1.Replace(",", "");
            ddwn_Month.SelectedIndex = ddwn_Month.Items.IndexOf(ddwn_Month.Items.FindByText(month1));
            ddwn_Day.SelectedIndex = ddwn_Day.Items.IndexOf(ddwn_Day.Items.FindByText(de1));
            ddwn_Year.SelectedIndex = ddwn_Year.Items.IndexOf(ddwn_Year.Items.FindByText(year1));

            txt_Citizenship.Text = ViewState["citi1"].ToString();
            txt_Email.Text = ViewState["emil1"].ToString();

            txt_EmployerIDNumber.ReadOnly = true;
            txt_FName.ReadOnly = true;
            txt_Mname.ReadOnly = true;
            txt_Surname.ReadOnly = true;
            ddwn_Gender.Enabled = false;
            txt_Age.ReadOnly = true;
            ddwn_Month.Enabled = false;
            ddwn_Day.Enabled = false;
            ddwn_Year.Enabled = false;
            txt_Citizenship.ReadOnly = true;
            txt_Email.ReadOnly = true;


            //btn_GenerateUserName.Text = UserIDGetter;
            //Response.Write(ViewState["EmployerIDGetter"]);
            if (btn_AddSave.Text == "Save")
            {
                txt_EmployerIDNumber.Text = "";
                txt_FName.Text = "";
                txt_Mname.Text = "";
                txt_Surname.Text = "";
                ddwn_Gender.Text = "Male";
                txt_Age.Text = "";
                ddwn_Month.SelectedIndex = 0;
                ddwn_Day.SelectedIndex = 0;
                ddwn_Year.SelectedIndex = 0;
                txt_Citizenship.Text = "";
                txt_Email.Text = "";
                btn_AddSave.Text = "Add";
                txt_EmployerIDNumber.ReadOnly = false;
            }
            
        }
        protected void btn_RemoveEmployer_Click(object sender, EventArgs e)
        {
            //Response.Write(ViewState["EmployerIDGetter"]);
            //string query = "delete from SystemUsersTBL where EmployerIDNumber = @ref"+ EmployerIDGetter;
            SqlCommand comm = new SqlCommand("delete from SystemMagnacycleEmployeesTBL where EmployeeIDNumber = @ref", conn);
            comm.Parameters.AddWithValue("@ref", ViewState["EmployerIDGetter"]);
            conn.Open();
            comm.ExecuteNonQuery();
            conn.Close();
            

            grid_Employers.DataBind();
          //  grid_CurrentUsersRowCounter();
       try
            {
                SqlCommand commm = new SqlCommand("delete from SystemUserTBL where EmployerIDNumber = "+ViewState["EmployerIDGetter"], conn);
                //commm.Parameters.AddWithValue("@reff", ViewState["EmployerIDGetter"]);
                conn.Open();
                commm.ExecuteNonQuery();
                conn.Close();

                grid_AuthorizedUsers.DataBind();
           
                txt_EmployerIDNumber.ReadOnly = false;
                txt_EmployerIDNumber.Text = "";
                txt_FName.Text = "";
                txt_Mname.Text = "";
                txt_Surname.Text = "";
                ddwn_Gender.Text = "Male";
                txt_Age.Text = "";
                ddwn_Month.SelectedIndex = 0;
                ddwn_Day.SelectedIndex = 0;
                ddwn_Year.SelectedIndex = 0;
                txt_Citizenship.Text = "";
                txt_Email.Text = "";
                btn_AddSave.Text = "Add";

                txt_EmployerIDNumber.ReadOnly = false;
                txt_FName.ReadOnly = false;
                txt_Mname.ReadOnly = false;
                txt_Surname.ReadOnly = false;
                ddwn_Gender.Enabled = true;
                txt_Age.ReadOnly = false;
                ddwn_Month.Enabled = true;
                ddwn_Day.Enabled = true;
                ddwn_Year.Enabled = true;
                txt_Citizenship.ReadOnly = false;
                txt_Email.ReadOnly = false;
            }
       catch (Exception ex)
       {
           Response.Write("wala sya sa authorized");  // bakit di nag eerror
       } 
        }
        protected void btn_AddAsUser_Click(object sender, EventArgs e)
        {
            int cnt = grid_AuthorizedUsers.Rows.Count;
            if (cnt == 3)
            {
                Response.Write("Max");
            }
            else 
            {
                string recieve = ViewState["EmployerIDGetter"].ToString();
                string query = "Select EmployeeIDNumber,FirstName,Surname from SystemMagnacycleEmployeesTBL where EmployeeIDNumber = " + recieve;
                SqlCommand com = new SqlCommand(query, conn);
                IDataReader r;
                conn.Open();
                r = com.ExecuteReader();
                r.Read();
                string employeridnumber = r.GetString(0);//first row
                string firstname = r.GetString(1);
                string surname = r.GetString(2);
                conn.Close();



                //Generate Username

                ViewState["month"] = ddwn_Month.SelectedItem.Value;
                ViewState["day"] = ddwn_Day.SelectedItem.Value;

                string faname = firstname;
                faname = Regex.Replace(faname, @"\s", "");
                string generatedUname = faname + ViewState["month"].ToString() + ViewState["day"].ToString() + "User";

                int s = grid_AuthorizedUsers.Rows.Count;
                bool boll = false;
                string uname = "";
                for (int p = 0; p <= s - 1; p++)
                {
                    string un = grid_AuthorizedUsers.Rows[p].Cells[4].Text;
                    if (generatedUname == un)
                    {
                        Random rnd = new Random();
                        int id = rnd.Next(100, 999);
                        string finame = firstname;
                        faname = Regex.Replace(faname, @"\s", "");
                        uname = finame + ViewState["month"].ToString() + ViewState["day"].ToString() + "User" + id.ToString();

                        try
                        {
                            string insert = "insert into SystemUserTBL(EmployeeIDNumber,FirstName,Surname,UserName,Password)values(@EIDNumber,@FName,@SName,@Uname,@Pass)";
                            SqlCommand comm = new SqlCommand(insert, conn);
                            conn.Open();
                            comm.Parameters.AddWithValue("@EIDNumber", employeridnumber);
                            comm.Parameters.AddWithValue("@FName", firstname);
                            comm.Parameters.AddWithValue("@SName", surname);
                            comm.Parameters.AddWithValue("@Uname", uname);
                            comm.Parameters.AddWithValue("@Pass", "defaultPassword123");
                            comm.ExecuteNonQuery();
                            conn.Close();
                            Response.Write("ex.Message");



                            grid_AuthorizedUsers.DataBind();

                            txt_EmployerIDNumber.ReadOnly = false;
                            txt_EmployerIDNumber.Text = "";
                            txt_FName.Text = "";
                            txt_Mname.Text = "";
                            txt_Surname.Text = "";
                            ddwn_Gender.Text = "Male";
                            txt_Age.Text = "";
                            ddwn_Month.SelectedIndex = 0;
                            ddwn_Day.SelectedIndex = 0;
                            ddwn_Year.SelectedIndex = 0;
                            txt_Citizenship.Text = "";
                            txt_Email.Text = "";
                            btn_AddSave.Text = "Add";

                            txt_EmployerIDNumber.ReadOnly = false;
                            txt_FName.ReadOnly = false;
                            txt_Mname.ReadOnly = false;
                            txt_Surname.ReadOnly = false;
                            ddwn_Gender.Enabled = true;
                            txt_Age.ReadOnly = false;
                            ddwn_Month.Enabled = true;
                            ddwn_Day.Enabled = true;
                            ddwn_Year.Enabled = true;
                            txt_Citizenship.ReadOnly = false;
                            txt_Email.ReadOnly = false;

                        }
                        catch (Exception ex)
                        {
                            Response.Write(ex.Message);
                        }

                        boll = true;
                        break;
                    }

                }
                if (boll == false)
                {
                    try
                    {
                        string insert = "insert into SystemUserTBL(EmployeeIDNumber,FirstName,Surname,UserName,Password)values(@EIDNumber,@FName,@SName,@Uname,@Pass)";
                        SqlCommand comm = new SqlCommand(insert, conn);
                        conn.Open();
                        comm.Parameters.AddWithValue("@EIDNumber", employeridnumber);
                        comm.Parameters.AddWithValue("@FName", firstname);
                        comm.Parameters.AddWithValue("@SName", surname);
                        comm.Parameters.AddWithValue("@Uname", generatedUname);
                        comm.Parameters.AddWithValue("@Pass", "defaultPassword123");
                        comm.ExecuteNonQuery();
                        conn.Close();
                        Response.Write("ex.Message");



                        grid_AuthorizedUsers.DataBind();

                        txt_EmployerIDNumber.ReadOnly = false;
                        txt_EmployerIDNumber.Text = "";
                        txt_FName.Text = "";
                        txt_Mname.Text = "";
                        txt_Surname.Text = "";
                        ddwn_Gender.Text = "Male";
                        txt_Age.Text = "";
                        ddwn_Month.SelectedIndex = 0;
                        ddwn_Day.SelectedIndex = 0;
                        ddwn_Year.SelectedIndex = 0;
                        txt_Citizenship.Text = "";
                        txt_Email.Text = "";
                        btn_AddSave.Text = "Add";

                        txt_EmployerIDNumber.ReadOnly = false;
                        txt_FName.ReadOnly = false;
                        txt_Mname.ReadOnly = false;
                        txt_Surname.ReadOnly = false;
                        ddwn_Gender.Enabled = true;
                        txt_Age.ReadOnly = false;
                        ddwn_Month.Enabled = true;
                        ddwn_Day.Enabled = true;
                        ddwn_Year.Enabled = true;
                        txt_Citizenship.ReadOnly = false;
                        txt_Email.ReadOnly = false;

                        Response.Write(generatedUname + " " + uname);
                    }
                    catch (Exception ex)
                    {
                        Response.Write(ex.Message);
                    }
                }
            }
        }
        protected void btn_RemoveAccess_Click(object sender, EventArgs e)
        {
            ViewState["EmployerIDUserRemover"] = grid_AuthorizedUsers.SelectedRow.Cells[1].Text;
            SqlCommand commm = new SqlCommand("delete from SystemUserTBL where EmployeeIDNumber = " + ViewState["EmployerIDUserRemover"], conn);
            commm.Parameters.AddWithValue("@reff", ViewState["EmployerIDUserRemover"]);
            conn.Open();
            commm.ExecuteNonQuery();
            conn.Close();

            grid_AuthorizedUsers.DataBind();
        }
        protected void btn_Edit_Click(object sender, EventArgs e)
        {
            ViewState["editRef"] = grid_Employers.SelectedRow.Cells[1].Text;
            string query = "Select * from SystemMagnacycleEmployeesTBL where EmployeeIDNumber = " + ViewState["editRef"];
            //SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["MagnaAdministratorConnectionString"].ConnectionString);
            SqlCommand com = new SqlCommand(query, conn);
            IDataReader r;
            conn.Open();
            r = com.ExecuteReader();
            r.Read();
            ViewState["id"] = r.GetString(0).ToString();
            ViewState["fname"] = r.GetString(1).ToString();
            ViewState["mname"] = r.GetString(2).ToString();
            ViewState["sname"] = r.GetString(3).ToString();
            ViewState["gender"] = r.GetString(4).ToString();
            ViewState["age"] = r.GetInt32(5);
            ViewState["bday"] = r.GetString(6).ToString();
            ViewState["citi"] = r.GetString(7).ToString();
            ViewState["emil"] = r.GetString(8).ToString();
            conn.Close();

            txt_EmployerIDNumber.Text = ViewState["id"].ToString();
            txt_FName.Text = ViewState["fname"].ToString();
            txt_Mname.Text = ViewState["mname"].ToString();
            txt_Surname.Text = ViewState["sname"].ToString();
            ddwn_Gender.Text = ViewState["gender"].ToString();
            txt_Age.Text = ViewState["age"].ToString();
            
            //separeate and load bday
            string bday = ViewState["bday"].ToString();
            string[] names = bday.Split(' '); // "1" means stop splitting after one space
            string month = names[0];
            string day = names[1];
            string year = names[2];
            string de = day.Replace(",","");
            ddwn_Month.SelectedIndex=ddwn_Month.Items.IndexOf(ddwn_Month.Items.FindByText(month));
            ddwn_Day.SelectedIndex = ddwn_Day.Items.IndexOf(ddwn_Day.Items.FindByText(de));
            ddwn_Year.SelectedIndex = ddwn_Year.Items.IndexOf(ddwn_Year.Items.FindByText(year));

            txt_Citizenship.Text = ViewState["citi"].ToString();
            txt_Email.Text = ViewState["emil"].ToString();

            txt_EmployerIDNumber.ReadOnly = false;
            txt_FName.ReadOnly = false;
            txt_Mname.ReadOnly = false;
            txt_Surname.ReadOnly = false;
            ddwn_Gender.Enabled = true;
            txt_Age.ReadOnly = false;
            ddwn_Month.Enabled = true;
            ddwn_Day.Enabled = true;
            ddwn_Year.Enabled = true;
            txt_Citizenship.ReadOnly = false;
            txt_Email.ReadOnly = false;

            btn_AddSave.Text = "Save";
            txt_EmployerIDNumber.ReadOnly = true;
        }

        protected void btn_Admins_Click(object sender, EventArgs e)
        {
            MultiView2.ActiveViewIndex = 1;
            MultiView3.ActiveViewIndex = 0;

            string rref = txt_AdminUserName.Text;

            conn.Open();
            string pp = "Select Password from SystemAdminsTBL where UserName = '"+ rref+"'";
            SqlCommand commsi = new SqlCommand(pp, conn);
            IDataReader rs;
            rs = commsi.ExecuteReader();
            rs.Read();
            string pword = rs.GetString(0);
            conn.Close();


            conn.Open();
            string q3 = "Select * from SystemAdminsTBl where UserName = '" + rref + "'";
            SqlCommand comms = new SqlCommand(q3, conn);
            IDataReader r;
            r = comms.ExecuteReader();
            r.Read();
            ViewState["idno"] = r.GetString(0);//first row
            string fname = r.GetString(1);
            string sname = r.GetString(3);
            conn.Close();

            txt_AdminsID.Text = ViewState["idno"].ToString();
            txt_AdminFullName.Text = sname + ", " + fname;
            txt_AdminPassword.Attributes.Add("value", pword);

            ViewState["pass"] = pword;

        }

        protected void btn_Administrator_Click(object sender, EventArgs e)
        {
            MultiView3.ActiveViewIndex = 0;
        }

        protected void btn_SystemUsers_Click(object sender, EventArgs e)
        {
            MultiView3.ActiveViewIndex = 1;
        }
        protected void btn_Clear_Click(object sender, EventArgs e)
        {
            txt_EmployerIDNumber.ReadOnly = false;
            txt_FName.ReadOnly = false;
            txt_Mname.ReadOnly = false;
            txt_Surname.ReadOnly = false;
            ddwn_Gender.Enabled = true;
            txt_Age.ReadOnly = false;
            ddwn_Month.Enabled = true;
            ddwn_Day.Enabled = true;
            ddwn_Year.Enabled = true;
            txt_Citizenship.ReadOnly = false;
            txt_Email.ReadOnly = false;

            txt_EmployerIDNumber.ReadOnly = false;
            txt_EmployerIDNumber.Text = "";
            txt_FName.Text = "";
            txt_Mname.Text = "";
            txt_Surname.Text = "";
            ddwn_Gender.Text = "Male";
            txt_Age.Text = "";
            ddwn_Month.SelectedIndex = 0;
            ddwn_Day.SelectedIndex = 0;
            ddwn_Year.SelectedIndex = 0;
            txt_Citizenship.Text = "";
            txt_Email.Text = "";
            btn_AddSave.Text = "Add";
        }

        protected void btn_Ok_Click(object sender, EventArgs e)
        {
            ViewState["choice"] = "ok";
            Random rnd = new Random();
            ViewState["eid"] = rnd.Next(100000, 999999);
            
            employerAdder();

            txt_EmployerIDNumber.ReadOnly = false;
            txt_FName.ReadOnly = false;
            txt_Mname.ReadOnly = false;
            txt_Surname.ReadOnly = false;
            ddwn_Gender.Enabled = true;
            txt_Age.ReadOnly = false;
            ddwn_Month.Enabled = true;
            ddwn_Day.Enabled = true;
            ddwn_Year.Enabled = true;
            txt_Citizenship.ReadOnly = false;
            txt_Email.ReadOnly = false;

            txt_EmployerIDNumber.ReadOnly = false;
            txt_EmployerIDNumber.Text = "";
            txt_FName.Text = "";
            txt_Mname.Text = "";
            txt_Surname.Text = "";
            ddwn_Gender.Text = "Male";
            txt_Age.Text = "";
            ddwn_Month.SelectedIndex = 0;
            ddwn_Day.SelectedIndex = 0;
            ddwn_Year.SelectedIndex = 0;
            txt_Citizenship.Text = "";
            txt_Email.Text = "";
            btn_AddSave.Text = "Add";

            ViewState["choice"] = null;

            lbl_Confirm.Visible = false;
            btn_Ok.Visible = false;
            btn_Cancel.Visible = false;
        }

        protected void btn_Cancel_Click(object sender, EventArgs e)
        {
            ViewState["choice"] = null;
            lbl_Confirm.Visible = false;
            btn_Ok.Visible = false;
            btn_Cancel.Visible = false;

        }

        protected void btn_okD_Click(object sender, EventArgs e)
        {
            ViewState["choice1"] = "ok";
            employerAdder();

            txt_EmployerIDNumber.ReadOnly = false;
            txt_FName.ReadOnly = false;
            txt_Mname.ReadOnly = false;
            txt_Surname.ReadOnly = false;
            ddwn_Gender.Enabled = true;
            txt_Age.ReadOnly = false;
            ddwn_Month.Enabled = true;
            ddwn_Day.Enabled = true;
            ddwn_Year.Enabled = true;
            txt_Citizenship.ReadOnly = false;
            txt_Email.ReadOnly = false;

            txt_EmployerIDNumber.ReadOnly = false;
            txt_EmployerIDNumber.Text = "";
            txt_FName.Text = "";
            txt_Mname.Text = "";
            txt_Surname.Text = "";
            ddwn_Gender.Text = "Male";
            txt_Age.Text = "";
            ddwn_Month.SelectedIndex = 0;
            ddwn_Day.SelectedIndex = 0;
            ddwn_Year.SelectedIndex = 0;
            txt_Citizenship.Text = "";
            txt_Email.Text = "";
            btn_AddSave.Text = "Add";

            ViewState["choice1"] = null;
            lbl_Confirm.Visible = false;
            btn_okD.Visible = false;
            btn_cancelD.Visible = false;
        }

        protected void btn_cancelD_Click(object sender, EventArgs e)
        {
            ViewState["choice1"] = null;
            lbl_Confirm.Visible = false;
            btn_okD.Visible = false;
            btn_cancelD.Visible = false;
        }

        protected void btn_OkSaver_Click(object sender, EventArgs e)
        {
            systemUserUpdater();

            txt_EmployerIDNumber.ReadOnly = false;
            txt_FName.ReadOnly = false;
            txt_Mname.ReadOnly = false;
            txt_Surname.ReadOnly = false;
            ddwn_Gender.Enabled = true;
            txt_Age.ReadOnly = false;
            ddwn_Month.Enabled = true;
            ddwn_Day.Enabled = true;
            ddwn_Year.Enabled = true;
            txt_Citizenship.ReadOnly = false;
            txt_Email.ReadOnly = false;

            txt_EmployerIDNumber.ReadOnly = false;
            txt_EmployerIDNumber.Text = "";
            txt_FName.Text = "";
            txt_Mname.Text = "";
            txt_Surname.Text = "";
            ddwn_Gender.Text = "Male";
            txt_Age.Text = "";
            ddwn_Month.SelectedIndex = 0;
            ddwn_Day.SelectedIndex = 0;
            ddwn_Year.SelectedIndex = 0;
            txt_Citizenship.Text = "";
            txt_Email.Text = "";
            btn_AddSave.Text = "Add";

            lbl_Confirm.Visible = false;
            btn_OkSaver.Visible = false;
            btn_CancelSaver.Visible = false;
        }

        protected void btn_CancelSaver_Click(object sender, EventArgs e)
        {
            lbl_Confirm.Visible = false;
            btn_OkSaver.Visible = false;
            btn_CancelSaver.Visible = false;
        }

       

        


        



       

        protected void btn_AdminChangePass_Click(object sender, EventArgs e)
        {
            lbl_AdminCurrentPass.Visible = true;
            lbl_AdminNewPass.Visible = true;
            lbl_AdminRetypePass.Visible = true;
            txt_AdminCurrentPass.Visible = true;
            txt_AdminNewPassword.Visible = true;
            txt_AdminConfirm.Visible = true;
          
            btn_AdminPassSave.Visible = true;
            btn_AdminPassCancel.Visible = true;
        }


        protected void btn_AdminPassCancel_Click(object sender, EventArgs e)
        {
            lbl_AdminCurrentPass.Visible = false;
            lbl_AdminNewPass.Visible = false;
            lbl_AdminRetypePass.Visible = false;
            txt_AdminCurrentPass.Visible = false;
            txt_AdminNewPassword.Visible = false;
            txt_AdminConfirm.Visible = false;
          
            btn_AdminPassSave.Visible = false;
            btn_AdminPassCancel.Visible = false;
        }

        protected void btn_AdminPassSave_Click(object sender, EventArgs e)
        {
            if (txt_AdminCurrentPass.Text == null || txt_AdminNewPassword.Text == null || txt_AdminConfirm.Text == null)
            {
                Response.Write("Complete the sentence!");
            }
            else
            {
                string rref = txt_AdminUserName.Text;
                conn.Open();
                string pp = "Select Password from SystemAdminsTBL where UserName = '" + rref + "'";
                SqlCommand commsi = new SqlCommand(pp, conn);
                IDataReader rs;
                rs = commsi.ExecuteReader();
                rs.Read();
                string pword = rs.GetString(0);
                conn.Close();

                txt_AdminPassword.Attributes.Add("value", pword);

                ViewState["pass"] = pword;

                if (txt_AdminCurrentPass.Text == ViewState["pass"].ToString())
                {
                    if (txt_AdminNewPassword.Text == txt_AdminConfirm.Text)
                    {
                        try
                        {
                            string q1 = "UPDATE SystemAdminsTBL SET Password = '" + txt_AdminNewPassword.Text + "' where AdminIDNumber = '" + ViewState["idno"].ToString() + "'";
                            SqlCommand comm = new SqlCommand(q1, conn);
                            conn.Open();
                            comm.ExecuteNonQuery();
                            conn.Close();

                            conn.Open();
                            string qry3 = "Select Password from SystemAdminsTBl where AdminIDNumber = '" + ViewState["idno"].ToString() + "'";
                            SqlCommand commas = new SqlCommand(qry3, conn);
                            IDataReader rder;
                            rder = commas.ExecuteReader();
                            rder.Read();
                            string pssword = rder.GetString(0);
                            conn.Close();

                            txt_AdminPassword.Attributes.Add("value", pssword);
                        }
                        catch (Exception ex)
                        {
                            Response.Write(ex.Message);
                        }

                        lbl_AdminCurrentPass.Visible = false;
                        lbl_AdminNewPass.Visible = false;
                        lbl_AdminRetypePass.Visible = false;
                        txt_AdminCurrentPass.Visible = false;
                        txt_AdminNewPassword.Visible = false;
                        txt_AdminConfirm.Visible = false;
                        btn_AdminPassSave.Visible = false;
                        btn_AdminPassCancel.Visible = false;
                        ViewState["pass"] = "";
                      
                    }
                    else if (txt_AdminNewPassword.Text != txt_AdminConfirm.Text)
                    {
                        Response.Write("Comfirmation not valid");
                    }
                }
                else if (txt_AdminCurrentPass.Text != txt_AdminPassword.Text)
                {
                    Response.Write("Current Password mismatch!");
                    // Response.Write(txt_Password.Text +" "+txt_CurrentPass.Text);
                }

            }
        }

        protected void btn_SignOut_Click(object sender, EventArgs e)
        {
            Response.Redirect("frm_UserLogIn.aspx");
        }

        //protected void Timer1_Tick(object sender, EventArgs e)
        //{
        //    //Label1.Text = DateTime.Now.ToLongTimeString();
        //    if (ddwn_tab1_Select.SelectedItem.Text == "Units")
        //    {
        //        grid_SentPO.DataSourceID = "src_SentUnitsPO";
        //        grid_SentPO.DataBind();
        //    }
        //    else if (ddwn_tab1_Select.SelectedItem.Text == "Spare Parts")
        //    {
        //        grid_SentPO.DataSourceID = "src_SentPartsPO";
        //        grid_SentPO.DataBind();
        //    }
        //    //kapag na pindot na ang select sa grid_sentPO, at nag view na ng items, di dapat sya mag dedatabind

        //    string dr = "UPDATE SystemUnitsOrderListsTBL SET DateReceived = '"+DateTime.Now.ToLongDateString()+"' WHERE (DateReceived is null);";
        //    SqlCommand det = new SqlCommand(dr,conn);
        //    conn.Open();
        //    det.ExecuteNonQuery();
        //    conn.Close();

        //}

        protected void btn_OrderInformations_Click(object sender, EventArgs e)
        {
            MultiView1.ActiveViewIndex = 1;
            MultiView4.ActiveViewIndex = 0;

            

            if (ddwn_tab1_Select.Text == "Units")
            {
                hpiconn.Open();
                string sc = "select UnitsCharge from systemchargestbl where chargenumber = 1";
                SqlCommand come = new SqlCommand(sc, hpiconn);
                IDataReader rr;
                rr = come.ExecuteReader();
                rr.Read();
                txt_ServiceCharge.Text = rr.GetString(0).ToString();
                hpiconn.Close();
                lbl_ServiceCharge.Text = "Service Charge Per Unit:";
            }
            else if (ddwn_tab1_Select.Text == "Spare Parts")
            {
                hpiconn.Open();
                string sc = "select partsCharge from systemchargestbl where chargenumber = 1";
                SqlCommand come = new SqlCommand(sc, hpiconn);
                IDataReader rr;
                rr = come.ExecuteReader();
                rr.Read();
                txt_ServiceCharge.Text = rr.GetString(0).ToString();
                hpiconn.Close();
                lbl_ServiceCharge.Text = "Service Charge Per Part:";
            }

            
        }

        protected void btn_EditItems_Click(object sender, EventArgs e)
        {
            if (txt_tab1_POReferenceNo.Text == "" || txt_tab1_POReferenceNo.Text == null)
            {

            }
            else
            {
                string refnum = txt_tab1_POReferenceNo.Text;
                char refID = refnum[0];
                if (refID.ToString() == "0")//meaning units
                {
                    grid_Availables.DataSourceID = "src_AvailUnits";
                    grid_Availables.DataBind();
                    MultiView4.ActiveViewIndex = 1;
                }
                else if(refID.ToString() == "1")
                {
                    grid_Availables.DataSourceID = "src_AvailParts";
                    grid_Availables.DataBind();
                    MultiView4.ActiveViewIndex = 1;
                }
                
            }
            btn_AddTo.Text = "Add to " + txt_tab1_POReferenceNo.Text;
        }

        protected void btn_EditPO_Click(object sender, EventArgs e)
        {
            lbl_Quan.Visible = true;
            txt_quants.Visible = true;
            lbl_Color.Visible = true;
            ddwn_Colors.Visible = true;
            btn_SaveEdit.Visible = true;
            btn_CancelEdit.Visible = true;
        }

        protected void btn_RemovePO_Click(object sender, EventArgs e)
        {
            //string remove = ViewState["idssa"].ToString();
            int remove = grid_PurchaseList.SelectedIndex;
            int firemove = remove + 1;
            string co = "delete from a" + txt_tab1_POReferenceNo.Text + "_Units where s_number = '" + firemove.ToString() + "'";
            SqlCommand commm = new SqlCommand(co, conn);
            //commm.Parameters.AddWithValue("@reff", ViewState["EmployerIDUserRemover"]);
            //Response.Write(co);
            conn.Open();
            commm.ExecuteNonQuery();
            conn.Close();
            /////////////////////////////////
            //reset s_number
            conn.Open();
            string remover = "UPDATE a" + txt_tab1_POReferenceNo.Text + "_Units SET s_number = NULL";
            SqlCommand ms = new SqlCommand(remover, conn);
            ms.ExecuteNonQuery();

            string snumber = "With cte As(Select id, s_number,  Row_Number() Over (Order By s_number) As rn From a" + txt_tab1_POReferenceNo.Text + "_Units)Update cte Set s_number = rn";
            SqlCommand ssqu = new SqlCommand(snumber, conn);

            ssqu.ExecuteNonQuery();
            conn.Close();
            //reset s_number

            //grid_PurchaseOrderDetails1.DataBind();
            string PON = txt_tab1_POReferenceNo.Text;
            lbl_tab1_POReferenceNo.Visible = true;
            txt_tab1_POReferenceNo.Visible = true;
            txt_tab1_POReferenceNo.Text = PON;


            //string que = "slect ModelCode,Description,Color,Quantity from a" + PON + "_Units";
            //conn.Open();
            //SqlCommand scom = new SqlCommand(que, conn);
            //scom.ExecuteNonQuery();
            //conn.Close();
            conn.Open();
            string ass = "select ModelCode,Description,Color,Quantity from a" + PON + "_Units";
            //Response.Write(ass);
            SqlDataAdapter adapter = new SqlDataAdapter(ass, conn);

            DataSet set = new DataSet();
            adapter.Fill(set);

            //grid_PurchaseOrderDetails1.DataSource = null;

            grid_PurchaseList.DataSource = set.Tables[0];
            grid_PurchaseList.DataBind();
            conn.Close();

            //string del = "drop table a"+remove+"_Units";
            //SqlCommand c = new SqlCommand(del,conn);
            //conn.Open();
            //c.ExecuteNonQuery();
            //conn.Close();
        }

        protected void btn_ModifyItem_Click(object sender, EventArgs e)
        {

        }

        //protected void btn_UpdateOrder_Click(object sender, EventArgs e)
        //{
        //    btn_AddItem.Visible = true;
        //    btn_EditItem.Visible = true;
        //    btn_RemoveItem.Visible = true;
        //    btn_ModifyItem.Visible = true;
        //    btn_UpdateOrder.Visible = !true;
        //    //MultiView1.ActiveViewIndex = 5;
        //}

        protected void btn_CancelOrder_Click(object sender, EventArgs e)
        {
            //string a = DateTime.Now.AddDays(double 7);
        }

        protected void btn_SendToHPI_Click(object sender, EventArgs e)
        {
            //string a = "9,213.45";
            //a = a.Replace(",", string.Empty);
            //double b = Convert.ToDouble(a);
            //int c = Convert.ToInt32(b);
            //Response.Write("double= "+b+" int="+c);

            string po = grid_SentPO.SelectedRow.Cells[1].Text;
            
            DateTime d1 = DateTime.Now;
            string dd1 = d1.ToLongDateString();

            DateTime d2 = d1.AddDays(1);
            string dd2 = d2.ToLongDateString();

            DateTime d3 = d2.AddDays(1);
            string dd3 = d3.ToLongDateString();

            DateTime d4 = d3.AddDays(1);
            string dd4 = d4.ToLongDateString();

            DateTime d5 = d4.AddDays(1);
            string dd5 = d5.ToLongDateString();

            DateTime d6 = d5.AddDays(1);
            string dd6 = d6.ToLongDateString();

            DateTime d7 = d6.AddDays(1);
            string dd7 = d7.ToLongDateString();

            //Response.Write(dd1+" "+dd2+" "+dd3+" "+dd4+" "+dd5+" "+dd6+" "+dd7);

            conn.Open();
            string u = "update systemUnitsorderlistsTBL set datesent = '" + dd1 + "', day1 = '" + dd1 + "', day2 = '" + dd2 + "', day3 = '" + dd3 + "', day4 = '" + dd4 + "', day5 = '" + dd5 + "', day6 = '" + dd6 + "', day7 = '" + dd7 + "' where  purchaseorderNumber = '" + po + "'";
            SqlCommand q = new SqlCommand(u, conn);
            q.ExecuteNonQuery();
            conn.Close();
            grid_SentPO.DataBind();

            //string aaa = "jerome";
            //string bbb = dd1;


            //string dsent = grid_SentPO.SelectedRow.Cells[3].Text;
            string qq = "insert into SystemPOUnitsTBL (PurchaseOrderNumber,DateReceived,Status)values('" + po + "','"+dd1+"','UNSEEN')";
            SqlCommand mm = new SqlCommand(qq, hpiconn);
            hpiconn.Open();
            mm.ExecuteNonQuery();
            hpiconn.Close();

            //reset s_number
            ///////////////////////////////////////////////////
            hpiconn.Open();

            string remover = "UPDATE SystemPOUnitsTBL SET s_number = NULL";
            SqlCommand ms = new SqlCommand(remover, hpiconn);
            ms.ExecuteNonQuery();

            string snumber = "With cte As(Select id, s_number,  Row_Number() Over (Order By s_number) As rn From SystemPOUnitsTBL)Update cte Set s_number = rn";
            SqlCommand ssqu = new SqlCommand(snumber, hpiconn);

            ssqu.ExecuteNonQuery();
            hpiconn.Close();
            //reset s_number


            //// sending po to hpi
            string re = ViewState["pon"].ToString();
            string a = "create table [a" + re + "_Units] ([id] [int] identity (1,1) not null,[s_number] [varchar] (50), [ModelCode] [varchar] (100), [Description] [varchar] (100), [Color] [varchar] (100), [Quantity] [varchar] (100), [BackOrders] [varchar] (100), [Available] [varchar] (100), [Percentage] [varchar] (100))";

            SqlCommand cc = new SqlCommand(a, hpiconn);
            hpiconn.Open();
            cc.ExecuteNonQuery();
            hpiconn.Close();

            int cx = grid_PurchaseList.Rows.Count;
            hpiconn.Open();
            for (int ax = 0; ax <= cx - 1; ax++)
            {
                string MCodes = grid_PurchaseList.Rows[ax].Cells[1].Text;
                string Decss = grid_PurchaseList.Rows[ax].Cells[2].Text;
                string Colors = grid_PurchaseList.Rows[ax].Cells[3].Text;
                string Qq = grid_PurchaseList.Rows[ax].Cells[4].Text;
                int j = ax + 1;
                string ins = "insert into a" + re + "_Units (s_number,ModelCode,Description,Color,Quantity)values('" + j.ToString() + "','" + MCodes + "','" + Decss + "','" + Colors + "','" + Qq + "')";
                SqlCommand mss = new SqlCommand(ins, hpiconn);
                mss.ExecuteNonQuery();
            }

            //string notif = "update SystemNotifierTBL set notifier = 'YES'";
            //SqlCommand mn = new SqlCommand(notif,conn);
            //mn.ExecuteNonQuery();
            hpiconn.Close();


            if (grid_Addeds.Rows.Count == 0)// meaning walang laman ang grid_Addeds at walang b
            {

            }
            else 
            {
                string re1 = ViewState["pon"].ToString();
                string a1 = "create table [b" + re1 + "_Units] ([id] [int] identity (1,1) not null,[s_number] [varchar] (50), [ModelCode] [varchar] (100), [Description] [varchar] (100), [Color] [varchar] (100), [Quantity] [varchar] (100), [BackOrders] [varchar] (100), [Available] [varchar] (100), [Percentage] [varchar] (100))";

                SqlCommand cc1 = new SqlCommand(a1, hpiconn);
                hpiconn.Open();
                cc1.ExecuteNonQuery();
                hpiconn.Close();

                int cx1 = grid_Addeds.Rows.Count;
                hpiconn.Open();
                for (int ax = 0; ax <= cx1 - 1; ax++)
                {
                    string MCodes = grid_Addeds.Rows[ax].Cells[1].Text;
                    string Decss = grid_Addeds.Rows[ax].Cells[2].Text;
                    string Colors = grid_Addeds.Rows[ax].Cells[3].Text;
                    string Qq = grid_Addeds.Rows[ax].Cells[4].Text;
                    int j = ax + 1;
                    string ins = "insert into b" + re + "_Units (s_number,ModelCode,Description,Color,Quantity)values('" + j.ToString() + "','" + MCodes + "','" + Decss + "','" + Colors + "','" + Qq + "')";
                    SqlCommand mss = new SqlCommand(ins, hpiconn);
                    mss.ExecuteNonQuery();
                }

                //string notif = "update SystemNotifierTBL set notifier = 'YES'";
                //SqlCommand mn = new SqlCommand(notif,conn);
                //mn.ExecuteNonQuery();
                hpiconn.Close();
            }

            //try
            //{
            //    int cxa = grid_Addeds.Rows.Count;
            //    hpiconn.Open();
            //    for (int ax = 0; ax <= cxa - 1; ax++)
            //    {
            //        string MCodes = grid_Addeds.Rows[ax].Cells[1].Text;
            //        string Decss = grid_Addeds.Rows[ax].Cells[2].Text;
            //        string Colors = grid_Addeds.Rows[ax].Cells[3].Text;
            //        string Qq = grid_Addeds.Rows[ax].Cells[4].Text;
            //        int j = ax + 1;
            //        string ins = "insert into b" + re + "_Units (s_number,ModelCode,Description,Color,Quantity)values('" + j.ToString() + "','" + MCodes + "','" + Decss + "','" + Colors + "','" + Qq + "')";
            //        SqlCommand mss = new SqlCommand(ins, hpiconn);
            //        mss.ExecuteNonQuery();
            //    }

            //    //string notif = "update SystemNotifierTBL set notifier = 'YES'";
            //    //SqlCommand mn = new SqlCommand(notif,conn);
            //    //mn.ExecuteNonQuery();
            //    hpiconn.Close();
            //}
            //catch (Exception ex)
            //{
            //    Response.Write("walang b");
            //}

            //create stockreport table
         // string
            //create stockreport table
        }

        protected void grid_SentPO_SelectedIndexChanged(object sender, EventArgs e)
        {
            string po = grid_SentPO.SelectedRow.Cells[1].Text;
            string datepo = grid_SentPO.SelectedRow.Cells[2].Text;
            
            if (po != txt_tab1_POReferenceNo.Text)
            {
                txt_Total.Text = "";
                txt_TotalCharge.Text = "";
                txt_Discount.Text = "";
                txt_DiscountedAmount.Text = "";
                txt_GrandTotal.Text = "";
                txt_Vat.Text = "";
                //ddwn_TermsOfPayment.SelectedIndex = 0;
            }
            txt_tab1_POReferenceNo.Text = po;
            txt_DateOfPO.Text = datepo;
            
            conn.Open();
            string ass = "select ModelCode,Description,Color,Quantity from a" + po + "_Units";
            //Response.Write(ass);
            SqlDataAdapter adapter = new SqlDataAdapter(ass, conn);

            DataSet set = new DataSet();
            adapter.Fill(set);

            //grid_PurchaseOrderDetails1.DataSource = null;

            grid_PurchaseList.DataSource = set.Tables[0];
            grid_PurchaseList.DataBind();
            conn.Close();


            try
            {
                conn.Open();
                string ass1 = "select ModelCode,Description,Color,Quantity from b" + po + "_Units";
                //Response.Write(ass);
                SqlDataAdapter adapter1 = new SqlDataAdapter(ass1, conn);

                DataSet set1 = new DataSet();
                adapter1.Fill(set1);

                //grid_PurchaseOrderDetails1.DataSource = null;

                grid_Addeds.DataSource = set1.Tables[0];
                grid_Addeds.DataBind();
                conn.Close();
                lbl_Added.Visible = true;
            }
            catch(Exception ex)
            {
                conn.Close();
                Response.Write(ex.Message);
                lbl_Added.Visible = !true;
                grid_Addeds.DataSource = null;
                grid_Addeds.DataBind();
            }


            MultiView1.ActiveViewIndex = 1;
            MultiView4.ActiveViewIndex = 0;

            
            conn.Open();
            string u = "update systemUnitsorderlistsTBL set Status = 'Viewed' where  purchaseorderNumber = '"+po+"'";
            SqlCommand q = new SqlCommand(u,conn);
            q.ExecuteNonQuery();
            conn.Close();
            grid_Updates.Visible = false;
            grid_Updates1.Visible = false;

            if (ddwn_tab1_Select.Text == "Units")
            {
                hpiconn.Open();
                string sc = "select UnitsCharge from systemchargestbl where chargenumber = 1";
                SqlCommand come = new SqlCommand(sc, hpiconn);
                IDataReader rr;
                rr = come.ExecuteReader();
                rr.Read();
                txt_ServiceCharge.Text = rr.GetString(0).ToString();
                hpiconn.Close();
                lbl_ServiceCharge.Text = "Service Charge Per Unit:";
            }
            else if (ddwn_tab1_Select.Text == "Spare Parts")
            {
                hpiconn.Open();
                string sc = "select partsCharge from systemchargestbl where chargenumber = 1";
                SqlCommand come = new SqlCommand(sc, hpiconn);
                IDataReader rr;
                rr = come.ExecuteReader();
                rr.Read();
                txt_ServiceCharge.Text = rr.GetString(0).ToString();
                hpiconn.Close();
                lbl_ServiceCharge.Text = "Service Charge Per Part:";
            }
            ViewState["pon"] = grid_SentPO.SelectedRow.Cells[1].Text;
          //  StockReport(!true);
        }

        protected void btn_Back_Click(object sender, EventArgs e)
        {
            MultiView1.ActiveViewIndex = 0;
        }

        protected void btn_refresh_Click(object sender, EventArgs e)
        {
            //conn.Open();
            //string notif = "update SystemNotifierTBL set notifier = 'NO'";
            //SqlCommand mn = new SqlCommand(notif,conn);
            //mn.ExecuteNonQuery();
            //conn.Close();
            grid_SentPO.DataBind();
        }
        string sel = "select Status from SystemUnitsOrderListsTBL where status = 'unseen'";
        string sel2 = "select Status from SystemPartsOrderListsTBL where status = 'unseen'";
        protected void Timer1_Tick(object sender, EventArgs e)
        {
            
            SqlCommand com = new SqlCommand(sel, conn);
            IDataReader r;
            conn.Open();
            r = com.ExecuteReader();
            int a = 0;
            while (r.Read())
            {
                
                string not = r.GetString(0).ToString();//secon row
                if (not == "UNSEEN")
                a++;
            }
            conn.Close();

            SqlCommand coma = new SqlCommand(sel2, conn);
            IDataReader ra;
            conn.Open();
            ra = coma.ExecuteReader();
            int aa = 0;
            while (ra.Read())
            {

                string not = ra.GetString(0).ToString();//secon row
                if (not == "UNSEEN")
                    aa++;
            }
            conn.Close();

            int ax = a + aa;

            //string nots = r.GetString(0).ToString();//secon row
            if (a != 0 || aa != 0)
            {
                lbl_notifN.Visible = !true;
                //lbl_notifY.Visible = true;
                lbl_NumberNewPO.Visible = true;
                //a++;
                lbl_NumberNewPO.Text = ax.ToString();
                //if (a != 0)
                //{
                //    lbl_NumberNewPO.Text = a.ToString();
                //}
                //else if (aa != 0)
                //{
                //    lbl_NumberNewPO.Text = aa.ToString();
                //}
            }
            else if (a == 0 || aa == 0)
            {
                //lbl_notifY.Visible = !true;
                lbl_notifN.Visible = true;
                lbl_NumberNewPO.Visible = false;
            }
            
            ////////

            
            
        }

        protected void btn_PurchaseList_Click(object sender, EventArgs e)
        {
            MultiView1.ActiveViewIndex = 0;
        }

        protected void btn_CheckForUpdates_Click(object sender, EventArgs e)
        {
            //string data = "";
            ////1creates column
            //DataTable dt = new DataTable();
            //dt.Columns.AddRange(new DataColumn[1] { new DataColumn("Quantity") });
            //ViewState["Orders"] = dt;
            ////2creates column
            ////1binds the column to the gridview
            //grid_Updates.DataSource = (DataTable)ViewState["Orders"];
            //grid_Updates.DataBind();
            ////2binds the column to the gridview
            ////1declares a datatable
            //DataTable dts = (DataTable)ViewState["Orders"];
            ////2decalres a datatable

            
            //int cont = grid_PurchaseList.Rows.Count;
            //for (int x = 0; x <= cont - 1; x++)
            //{
            //    hpiconn.Open();
            //    string mc = grid_PurchaseList.Rows[x].Cells[1].Text;
            //    string col = grid_PurchaseList.Rows[x].Cells[3].Text;
            //    string se = "select quantity from SystemModelsTBL where modelcode = '" + mc + "' and color = '" + col + "'";
            //    SqlCommand com = new SqlCommand(se, hpiconn);
            //    IDataReader r;
            //    r = com.ExecuteReader();
            //    while (r.Read())
            //    {
            //        data = r.GetString(0).ToString();
            //        dts.Rows.Add(data);
            //    }
            //    hpiconn.Close();
            //}


            //ViewState["Orders"] = dts;
            //grid_Updates.DataSource = (DataTable)ViewState["Orders"];
            //grid_Updates.DataBind();



            grid_Updates.Visible = true;
            grid_Updates1.Visible = true;
        }

        protected void Timer2_Tick(object sender, EventArgs e)
        {
            string data1 = "";
            string data2 = "";
            string data3 = "";
            string data4 = "";
            //1creates column
            DataTable dt = new DataTable();
            dt.Columns.AddRange(new DataColumn[4] { new DataColumn("Mocel Code"), new DataColumn("Color"), new DataColumn("Stocks"), new DataColumn("Unit Price") });
            ViewState["Ordersas"] = dt;
            //2creates column
            //1binds the column to the gridview
            grid_Updates.DataSource = (DataTable)ViewState["Ordersas"];
            grid_Updates.DataBind();
            //2binds the column to the gridview
            //1declares a datatable
            DataTable dts = (DataTable)ViewState["Ordersas"];
            //2decalres a datatable


            int cont = grid_PurchaseList.Rows.Count;
            for (int x = 0; x <= cont - 1; x++)
            {
                hpiconn.Open();
                string mc = grid_PurchaseList.Rows[x].Cells[1].Text;
                string col = grid_PurchaseList.Rows[x].Cells[3].Text;
                string se = "select modelcode,color,quantity,initialprice from SystemModelsTBL where modelcode = '" + mc + "' and color = '" + col + "'";
                SqlCommand come = new SqlCommand(se, hpiconn);
                IDataReader rr;
                rr = come.ExecuteReader();
                while (rr.Read())
                {
                    data1 = rr.GetString(0).ToString();
                    data2 = rr.GetString(1).ToString();
                    data3 = rr.GetString(2).ToString();
                    data4 = rr.GetString(3).ToString();
                    dts.Rows.Add(data1,data2,data3,data4);
                }
                hpiconn.Close();
            }


            ViewState["Ordersas"] = dts;
            grid_Updates.DataSource = (DataTable)ViewState["Ordersas"];
            grid_Updates.DataBind();
        }

        protected void btn_Compute_Click(object sender, EventArgs e)
        {
            //string s = txt_UnitCharge.Text;
            //string a = txt_PartCharge.Text;
            //string y = txt_vat.Text;

            ////string x = Convert.ToDecimal(s).ToString("#,##0.00");
            //string x1 = Convert.ToDecimal(a).ToString("#,##0.00");

            //1sub-total
            double st = 0;
            int cont = grid_PurchaseList.Rows.Count;
            for (int xx = 0; xx <= cont - 1; xx++)
            {
                hpiconn.Open();
                string mc = grid_PurchaseList.Rows[xx].Cells[1].Text;
                string col = grid_PurchaseList.Rows[xx].Cells[3].Text;
                int qua = Convert.ToInt32(grid_PurchaseList.Rows[xx].Cells[4].Text);
                string se = "select price from SystemModelsTBL where modelcode = '" + mc + "' and color = '" + col + "'";
                SqlCommand come = new SqlCommand(se, hpiconn);
                IDataReader rr;
                rr = come.ExecuteReader();
                while (rr.Read())
                {
                    string data1 = rr.GetString(0).ToString();
                    double j = Convert.ToDouble(data1);
                    double sp = qua * j;
                    st = st + sp;
                }
                hpiconn.Close();
            }
            string xxx = Convert.ToDecimal(st).ToString("#,##0.00");
            txt_Total.Text = xxx.ToString();
            //2sub-total

            //1vat
            double er = st;
            string vat = lbl_vat.Text;
            string sam = vat.Replace("%", string.Empty);
            string vat2 = "0." + sam;
            double vat3 = Convert.ToDouble(vat2);
            double o = er*vat3;
            //double final = st - o;
            string m = Convert.ToDecimal(o).ToString("#,##0.00");
            //double final = st - m;
            txt_Vat.Text = m.ToString();
            //2vat
           

            //1totalcharge
            double totnum = 0;
            int x = grid_PurchaseList.Rows.Count;
            for (int a = 0; a <= x - 1; a++)
            {
                double num = Convert.ToInt32(grid_PurchaseList.Rows[a].Cells[4].Text);
                totnum = totnum + num;
            }
            double fin = totnum * Convert.ToDouble(txt_ServiceCharge.Text);
            string q = Convert.ToDecimal(fin).ToString("#,##0.00");
            txt_TotalCharge.Text = q.ToString();
            //2totalcharge
        }

        protected void Timer3_Tick(object sender, EventArgs e)
        {
            string vat = "select vat from systemchargesTBL where chargenumber = 1";
            hpiconn.Open();
            SqlCommand cam = new SqlCommand(vat, hpiconn);
            SqlDataReader red;
            red = cam.ExecuteReader();
            red.Read();
            lbl_vat.Text = red.GetString(0) + "%";
            hpiconn.Close();
        }

        protected void txt_ServiceCharge_TextChanged(object sender, EventArgs e)
        {

        }

        protected void txt_GrandTotal_TextChanged(object sender, EventArgs e)
        {

        }

        //protected void btn_UsersMaintenance0_Click(object sender, EventArgs e)
        //{
        //    MultiView1.ActiveViewIndex = 4;
        //}

        protected void btn_CancelEdit_Click(object sender, EventArgs e)
        {
            lbl_Quan.Visible = !true;
            txt_quants.Visible = !true;
            lbl_Color.Visible = !true;
            ddwn_Colors.Visible = !true;
            btn_SaveEdit.Visible = !true;
            btn_CancelEdit.Visible = !true;
        }

        protected void btn_SaveEdit_Click(object sender, EventArgs e)
        {
            string change;
            string qs = "";
            //ViewState["mcc"] = grid_PurchaseOrderDetails1.SelectedRow.Cells[1].Text;
            ViewState["mcc"] = grid_PurchaseList.SelectedIndex;
            int index = Convert.ToInt32(ViewState["mcc"]);//index ng grid
            int finalIndex = index + 1;//index ng db(s_number)
            ViewState["colorr"] = grid_PurchaseList.SelectedRow.Cells[3].Text;
            if (txt_quants.Text == "" || txt_quants.Text == null || ddwn_Colors.Text == "" || ddwn_Colors.Text == null)
            {
                change = "update a" + txt_tab1_POReferenceNo.Text + "_Units set Color = '" + ddwn_Colors.Text + "' where s_number = '" + finalIndex.ToString() + "'";// and Color = '"+ViewState["colorr"].ToString()+"'";
            }
            else
            {
                change = "update a" + txt_tab1_POReferenceNo.Text + "_Units set Color = '" + ddwn_Colors.Text + "', Quantity = '" + txt_quants.Text + "' where s_number = '" + finalIndex.ToString() + "'";// and Color = '" + ViewState["colorr"].ToString()+"'";

            }
            conn.Open();
            SqlCommand sql = new SqlCommand(change, conn);
            sql.ExecuteNonQuery();
            conn.Close();

            string po = grid_SentPO.SelectedRow.Cells[1].Text;
            conn.Open();
            string u = "update systemUnitsorderlistsTBL set DateModified = '" + DateTime.Now.ToLongDateString() + "' where  purchaseorderNumber = '" + po + "'";
            SqlCommand q = new SqlCommand(u, conn);
            q.ExecuteNonQuery();
            conn.Close();


            //databind
            string PON = txt_tab1_POReferenceNo.Text;
            lbl_tab1_POReferenceNo.Visible = true;
            txt_tab1_POReferenceNo.Visible = true;
            txt_tab1_POReferenceNo.Text = PON;


            //string que = "slect ModelCode,Description,Color,Quantity from a" + PON + "_Units";
            //conn.Open();
            //SqlCommand scom = new SqlCommand(que, conn);
            //scom.ExecuteNonQuery();
            //conn.Close();
            conn.Open();
            string ass = "select ModelCode,Description,Color,Quantity from a" + PON + "_Units";
            //Response.Write(ass);
            SqlDataAdapter adapter = new SqlDataAdapter(ass, conn);

            DataSet set = new DataSet();
            adapter.Fill(set);

            //grid_PurchaseOrderDetails1.DataSource = null;

            grid_PurchaseList.DataSource = set.Tables[0];
            grid_PurchaseList.DataBind();
            conn.Close();
            //databind
            try
            {
                //reading/gettign of data
                string query = "WITH unitsCTE AS (SELECT *, Row_number() OVER (Partition BY ModelCode, Color ORDER BY s_number) AS rows FROM a" + PON + "_Units) SELECT ModelCode,Color,Quantity FROM unitsCTE WHERE rows > 1";
                SqlCommand com = new SqlCommand(query, conn);
                IDataReader r;
                conn.Open();
                r = com.ExecuteReader();

                r.Read();
                ViewState["code"] = r.GetString(0).ToString();//secon row
                //ViewState["desc"] = r.GetString(1).ToString();
                ViewState["colser"] = r.GetString(1).ToString();//secon row
                ViewState["qquan"] = r.GetString(2).ToString();
                conn.Close();
                //reading/gettign of data
                // Response.Write(ViewState["code"].ToString() + " " + ViewState["colser"].ToString() + " " + ViewState["qquan"].ToString());
                int old = Convert.ToInt32(ViewState["qquan"]);//Convert.ToInt32(txt_tab2_Quantity.Text);

                //deleting of row1
                qs = "WITH unitsCTE AS (SELECT *, Row_number() OVER (Partition BY ModelCode, Color ORDER BY s_number) AS rows FROM a" + PON + "_Units) delete from unitsCTE WHERE rows > 1";
                SqlCommand mn = new SqlCommand(qs, conn);
                conn.Open();
                mn.ExecuteNonQuery();
                conn.Close();
                //deleteing of row


                string sel = "select Quantity from a" + PON + "_Units where ModelCode = '" + ViewState["code"].ToString() + "' and Color = '" + ViewState["colser"].ToString() + "' order by s_number";
                SqlCommand cmd = new SqlCommand(sel, conn);
                conn.Open();
                SqlDataReader rdr;
                rdr = cmd.ExecuteReader();
                rdr.Read();
                int dest = Convert.ToInt32(rdr.GetString(0));
                conn.Close();

                int finals = old + dest;

                string ups = "update a" + PON + "_Units set Quantity = '" + finals.ToString() + "' Where ModelCode ='" + ViewState["code"].ToString() + "' and Color = '" + ViewState["colser"].ToString() + "'";
                SqlCommand ss = new SqlCommand(ups, conn);
                conn.Open();
                ss.ExecuteNonQuery();
                conn.Close();

                //databind
                string PONs = txt_tab1_POReferenceNo.Text;
                lbl_tab1_POReferenceNo.Visible = true;
                txt_tab1_POReferenceNo.Visible = true;
                txt_tab1_POReferenceNo.Text = PONs;


                //string que = "slect ModelCode,Description,Color,Quantity from a" + PON + "_Units";
                //conn.Open();
                //SqlCommand scom = new SqlCommand(que, conn);
                //scom.ExecuteNonQuery();
                //conn.Close();
                conn.Open();
                string asss = "select ModelCode,Description,Color,Quantity from a" + PONs + "_Units";
                //Response.Write(ass);
                SqlDataAdapter adapters = new SqlDataAdapter(asss, conn);

                DataSet sets = new DataSet();
                adapters.Fill(sets);

                //grid_PurchaseOrderDetails1.DataSource = null;

                grid_PurchaseList.DataSource = sets.Tables[0];
                grid_PurchaseList.DataBind();
                conn.Close();
                //databind
                //////////////////////////////////////////////////
                //reset s_number
                conn.Open();
                string remover = "UPDATE a" + txt_tab1_POReferenceNo.Text + "_Units SET s_number = NULL";
                SqlCommand ms = new SqlCommand(remover, conn);
                ms.ExecuteNonQuery();

                string snumber = "With cte As(Select id, s_number,  Row_Number() Over (Order By s_number) As rn From a" + txt_tab1_POReferenceNo.Text + "_Units)Update cte Set s_number = rn";
                SqlCommand ssqu = new SqlCommand(snumber, conn);

                ssqu.ExecuteNonQuery();
                conn.Close();
                //reset s_number

            }
            catch (Exception ex)
            {
                Response.Write("walang kaparehas");
            }
        }

        protected void grid_PurchaseList_SelectedIndexChanged(object sender, EventArgs e)
        {
            ddwn_Colors.Items.Clear();
            ViewState["coda"] = grid_PurchaseList.SelectedRow.Cells[1].Text;
            ViewState["idssa"] = grid_PurchaseList.SelectedRow.Cells[1].Text;
            ViewState["mcID"] = grid_PurchaseList.SelectedIndex;
            // ViewState["colorr"] = grid_PurchaseOrderDetails1.SelectedRow.Cells[3].Text;

            ////if (txt_PONumber.Visible == true)
            ////{
            ////    string wow = grid_PurchaseOrderDetails.SelectedRow.Cells[1].Text;
            string query = "select color from SystemModelsTBL where ModelCode='" + ViewState["coda"].ToString() + "'";
            hpiconn.Open();
            SqlCommand cmds = new SqlCommand(query, hpiconn);
            SqlDataReader dr;
            dr = cmds.ExecuteReader();

            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    ddwn_Colors.Items.Add(dr[0].ToString());
                }
            }
            hpiconn.Close();
            ////    Response.Write("jerome ko");
            ////}
            ////else
            ////{

            btn_EditItem2.Visible = !true;
            btn_RemoveItem2.Visible = !true;

            lbl_Quan.Visible = !true;
            txt_quants.Visible = !true;
            lbl_Color.Visible = !true;
            ddwn_Colors.Visible = !true;
            btn_SaveEdit.Visible = !true;
            btn_CancelEdit.Visible = !true;

            lbl_Quan2.Visible = !true;
            txt_quants2.Visible = !true;
            lbl_Color2.Visible = !true;
            ddwn_Colors2.Visible = !true;
            btn_SaveEdit2.Visible = !true;
            btn_CancelEdit2.Visible = !true;

            btn_EditItem.Visible = true;
            btn_RemoveItem.Visible = true;

            //btn_AddItem.Visible = !true;
            //btn_AddItem1.Visible = true;
            ViewState["adders"] = "first";

            grid_Addeds.SelectedIndex = -1;
        }

        protected void btn_AddUnitsParts_Click(object sender, EventArgs e)
        {
            int done = 0;
            if (txt_tab1_Quantity.Text == "" || txt_tab1_Quantity.Text == null)
            {
                Response.Write("jj");
            }
            else
            {
                if (ddwn_tab1_Select.Text == "Units")
                {

                    if (grid_AddedItems.Rows.Count == 0)
                    {
                        //string num3 = "";
                        ////Random rnd = new Random();
                        ////int num1 = rnd.Next(10000, 99999);
                        ////int num2 = rnd.Next(1000, 9999);
                        //if (ddwn_tab1_Select.Text == "Units")
                        //{
                        //    num3 = "0";
                        //}
                        //else if (ddwn_tab1_Select.Text == "Spare Parts")
                        //{
                        //    num3 = "1";
                        //}

                        //txt_tab1_POReferenceNo.Text = num3.ToString();

                        DataTable dt = new DataTable();
                        dt.Columns.AddRange(new DataColumn[4] { new DataColumn("Model Code"), new DataColumn("Description"), new DataColumn("Color"), new DataColumn("Quantity") });
                        ViewState["Orders"] = dt;

                        grid_AddedItems.DataSource = (DataTable)ViewState["Orders"];
                        grid_AddedItems.DataBind();

                        DataTable dts = (DataTable)ViewState["Orders"];
                        dts.Rows.Add(ViewState["MCode1"].ToString(), ViewState["Decs1"].ToString(), ViewState["Color1"].ToString(), txt_tab1_Quantity.Text);
                        ViewState["Orders"] = dts;

                        grid_AddedItems.DataSource = (DataTable)ViewState["Orders"];
                        grid_AddedItems.DataBind();


                    }
                    else
                    {
                        

                        string modeltest = ViewState["MCode1"].ToString();
                        string colortest = ViewState["Color1"].ToString();

                        int rows = grid_AddedItems.Rows.Count;
                        for (int cv = 0; cv <= rows - 1; cv++)
                        {
                            ViewState["ModelCode"] = grid_AddedItems.Rows[cv].Cells[1].Text;
                            ViewState["Colors"] = grid_AddedItems.Rows[cv].Cells[3].Text;

                            if (ViewState["ModelCode"].ToString() == modeltest)
                            {
                                if (ViewState["Colors"].ToString() == colortest)
                                {
                                    int nums = Convert.ToInt32(grid_AddedItems.Rows[cv].Cells[4].Text);
                                    int nums1 = Convert.ToInt32(txt_tab1_Quantity.Text);

                                    int nums2 = nums1 + nums;
                                    string final = nums2.ToString();

                                    //int inds = -1;

                                    for (int i = 0; i < grid_AddedItems.Rows.Count; i++)
                                    {
                                        foreach (DataControlFieldCell df in grid_AddedItems.Rows[i].Cells)
                                        {
                                            if (df.Text == modeltest)
                                            {
                                                foreach (DataControlFieldCell df2 in grid_AddedItems.Rows[i].Cells)
                                                {
                                                    if (df2.Text == colortest)
                                                    {
                                                        DataTable dts = (DataTable)ViewState["Orders"];
                                                        DataRow dr;
                                                        dr = dts.Rows[i];
                                                        dr["Quantity"] = final;
                                                        grid_AddedItems.DataSource = (DataTable)ViewState["Orders"];
                                                        grid_AddedItems.DataBind();
                                                        // Response.Write(i.ToString()); 
                                                        break;
                                                    }
                                                }

                                            }
                                        }

                                    }



                                    done = 1;
                                    break;
                                }
                            }
                        }
                        if (done == 0)
                        {
                            DataTable dts = (DataTable)ViewState["Orders"];
                            dts.Rows.Add(ViewState["MCode1"].ToString(), ViewState["Decs1"].ToString(), ViewState["Color1"].ToString(), txt_tab1_Quantity.Text);
                            ViewState["Orders"] = dts;

                            grid_AddedItems.DataSource = (DataTable)ViewState["Orders"];
                            grid_AddedItems.DataBind();

                        }
                        
                    }
                    
                }
            }
        }

        protected void btn_BCK_Click(object sender, EventArgs e)
        {
            MultiView1.ActiveViewIndex = 1;
            MultiView4.ActiveViewIndex = 0;
            
        }

        protected void grid_Availables_SelectedIndexChanged(object sender, EventArgs e)
        {
            ViewState["MCode1"] = grid_Availables.SelectedRow.Cells[1].Text;
            ViewState["Decs1"] = grid_Availables.SelectedRow.Cells[2].Text;
            ViewState["Color1"] = grid_Availables.SelectedRow.Cells[3].Text;
        }

        protected void btn_Edits_Click(object sender, EventArgs e)
        {
            lbl_Quan1.Visible = true;
            txt_quants1.Visible = true;
            lbl_Color1.Visible = true;
            ddwn_Colors1.Visible = true;
            btn_SaveEdit1.Visible = true;
            btn_CancelEdit1.Visible = true;
        }

        protected void btn_CancelEdit1_Click(object sender, EventArgs e)
        {
            lbl_Quan1.Visible = !true;
            txt_quants1.Visible = !true;
            lbl_Color1.Visible = !true;
            ddwn_Colors1.Visible = !true;
            btn_SaveEdit1.Visible = !true;
            btn_CancelEdit1.Visible = !true;
        }

        protected void btn_SaveEdit1_Click(object sender, EventArgs e)
        {
            int aw = grid_AddedItems.SelectedRow.RowIndex;
            string mc = grid_AddedItems.SelectedRow.Cells[1].Text;
            string cols = ddwn_Colors1.Text;
            int nums = Convert.ToInt32(grid_AddedItems.SelectedRow.Cells[4].Text);
           // int qa = Convert.ToInt32(txt_quants1.Text);
            int dones = 0;
            int axf = 0;

            int rowsa = grid_AddedItems.Rows.Count;
            for (int b = 0; b <= rowsa - 1; b++ )
            {
                if (b != aw)
                {
                    string mcget = grid_AddedItems.Rows[b].Cells[1].Text;
                    if (mc == mcget)
                    {
                        string colsget = grid_AddedItems.Rows[b].Cells[3].Text;
                        if (cols == colsget)
                        {
                            int ax = Convert.ToInt32(grid_AddedItems.Rows[b].Cells[4].Text);
                            if (txt_quants1.Text != "")// may laman
                            {
                                axf = ax + Convert.ToInt32(txt_quants1.Text);
                                string final = ddwn_Colors1.Text;
                                string quanfinal = axf.ToString();
                                DataTable dts = (DataTable)ViewState["Orders"];
                                DataRow dr;
                                dr = dts.Rows[b];
                                dr["Color"] = final;
                                dr["Quantity"] = quanfinal;
                                grid_AddedItems.DataSource = (DataTable)ViewState["Orders"];
                                grid_AddedItems.DataBind();
                            }
                            else
                            {
                                axf = ax + nums;
                                string final = ddwn_Colors1.Text;
                                string quanfinal = axf.ToString();
                                DataTable dts = (DataTable)ViewState["Orders"];
                                DataRow dr;
                                dr = dts.Rows[b];
                                dr["Color"] = final;
                                dr["Quantity"] = quanfinal;
                                grid_AddedItems.DataSource = (DataTable)ViewState["Orders"];
                                grid_AddedItems.DataBind();
                            }
                            grid_AddedItems.DeleteRow(grid_AddedItems.SelectedIndex);
                            dones = 1;
                            break;
                        }
                    }
                }
            }
            if (dones == 0)// meaning, walang nabago
            {
                 if (txt_quants1.Text == "" || txt_quants1.Text == null)
                {
                    string modeltest = grid_AddedItems.SelectedRow.Cells[1].Text;
                    string colortest = grid_AddedItems.SelectedRow.Cells[3].Text;

                    int rows = grid_AddedItems.Rows.Count;
                    for (int cv = 0; cv <= rows - 1; cv++)
                    {
                        ViewState["ModelCode"] = grid_AddedItems.Rows[cv].Cells[1].Text;
                        ViewState["Colors"] = grid_AddedItems.Rows[cv].Cells[3].Text;

                        if (ViewState["ModelCode"].ToString() == modeltest)
                        {
                            if (ViewState["Colors"].ToString() == colortest)
                            {
                                string final = ddwn_Colors1.Text;
                                //int inds = -1;

                                for (int i = 0; i < grid_AddedItems.Rows.Count; i++)
                                {
                                    foreach (DataControlFieldCell df in grid_AddedItems.Rows[i].Cells)
                                    {
                                        if (df.Text == modeltest)
                                        {
                                            foreach (DataControlFieldCell df2 in grid_AddedItems.Rows[i].Cells)
                                            {
                                                if (df2.Text == colortest)
                                                {
                                                    DataTable dts = (DataTable)ViewState["Orders"];
                                                    DataRow dr;
                                                    dr = dts.Rows[i];
                                                    dr["Color"] = final;
                                                    grid_AddedItems.DataSource = (DataTable)ViewState["Orders"];
                                                    grid_AddedItems.DataBind();
                                                    break;
                                                }
                                            }
                                        }
                                    }
                                }
                                break;
                            }
                        }
                    }
                }
                else if (txt_quants1.Text != "" || txt_quants1.Text != null)
                {
                    string modeltest = grid_AddedItems.SelectedRow.Cells[1].Text;
                    string colortest = grid_AddedItems.SelectedRow.Cells[3].Text;

                    int rows = grid_AddedItems.Rows.Count;
                    for (int cv = 0; cv <= rows - 1; cv++)
                    {
                        ViewState["ModelCode"] = grid_AddedItems.Rows[cv].Cells[1].Text;
                        ViewState["Colors"] = grid_AddedItems.Rows[cv].Cells[3].Text;

                        if (ViewState["ModelCode"].ToString() == modeltest)
                        {
                            if (ViewState["Colors"].ToString() == colortest)
                            {
                                string colfinal = ddwn_Colors1.Text;
                                string quanfinal = txt_quants1.Text;
                                //string quanfinal = axf.ToString();

                                //int inds = -1;

                                for (int i = 0; i < grid_AddedItems.Rows.Count; i++)
                                {
                                    foreach (DataControlFieldCell df in grid_AddedItems.Rows[i].Cells)
                                    {
                                        if (df.Text == modeltest)
                                        {
                                            foreach (DataControlFieldCell df2 in grid_AddedItems.Rows[i].Cells)
                                            {
                                                if (df2.Text == colortest)
                                                {
                                                    DataTable dts = (DataTable)ViewState["Orders"];
                                                    DataRow dr;
                                                    dr = dts.Rows[i];
                                                    dr["Color"] = colfinal;
                                                    dr["Quantity"] = quanfinal;
                                                    grid_AddedItems.DataSource = (DataTable)ViewState["Orders"];
                                                    grid_AddedItems.DataBind();
                                                    break;
                                                }
                                            }
                                        }
                                    }
                                }
                                break;
                            }
                        }
                    }
                }
            }
        }

        protected void grid_AddedItems_SelectedIndexChanged(object sender, EventArgs e)
        {
            ddwn_Colors1.Items.Clear();
            string code = grid_AddedItems.SelectedRow.Cells[1].Text;
            ViewState["idssa"] = grid_AddedItems.SelectedRow.Cells[1].Text;
            ViewState["mcID"] = grid_AddedItems.SelectedIndex;

            string query = "select color from SystemModelsTBL where ModelCode='" + code + "'";
            hpiconn.Open();
            SqlCommand cmds = new SqlCommand(query, hpiconn);
            SqlDataReader dr;
            dr = cmds.ExecuteReader();

            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    ddwn_Colors1.Items.Add(dr[0].ToString());
                }
            }
            hpiconn.Close();
        }

        protected void btn_Remove_Click(object sender, EventArgs e)
        {
            grid_AddedItems.DeleteRow(grid_AddedItems.SelectedIndex);
        }

        protected void grid_AddedItems_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int index = Convert.ToInt32(e.RowIndex);
            DataTable dt = ViewState["Orders"] as DataTable;
            dt.Rows[index].Delete();
            ViewState["Orders"] = dt;
            grid_AddedItems.DataSource = ViewState["Orders"] as DataTable;
            grid_AddedItems.DataBind();
        }

        protected void btn_AddTo_Click(object sender, EventArgs e)
        {
            string vars = "";
            int rs = 0;
            if (ViewState["adders"].ToString() == "first")
            {
                vars = "a";
                rs = grid_PurchaseList.Rows.Count;
            }
            else if (ViewState["adders"].ToString() == "second")
            {
                vars = "b";
                rs = grid_Addeds.Rows.Count;
            }


            string qs = "";
            string po = grid_SentPO.SelectedRow.Cells[1].Text;
            conn.Open();
            string u = "update systemUnitsorderlistsTBL set DateModified = '" + DateTime.Now.ToLongDateString() + "' where  purchaseorderNumber = '" + po + "'";
            SqlCommand q = new SqlCommand(u, conn);
            q.ExecuteNonQuery();
            conn.Close();

            try
            {
                conn.Open();
                string a = "create table [" + vars + txt_tab1_POReferenceNo.Text + "_Units] ([id] [int] identity (1,1) not null,[s_number] [varchar] (50), [ModelCode] [varchar] (100), [Description] [varchar] (100), [Color] [varchar] (100), [Quantity] [varchar] (100), [BackOrders] [varchar] (100), [Available] [varchar] (100), [Percentage] [varchar] (100))";

                SqlCommand cc = new SqlCommand(a, conn);

                cc.ExecuteNonQuery();
                conn.Close();

                int counts = grid_AddedItems.Rows.Count;
                conn.Open();
                for (int c = 0; c <= counts - 1; c++)
                {
                    ViewState["MCode"] = grid_AddedItems.Rows[c].Cells[1].Text;
                    ViewState["Decs"] = grid_AddedItems.Rows[c].Cells[2].Text;
                    ViewState["Color"] = grid_AddedItems.Rows[c].Cells[3].Text;
                    ViewState["Q"] = grid_AddedItems.Rows[c].Cells[4].Text;
                    int xs = c + 1;
                    string query1 = "insert into " + vars + txt_tab1_POReferenceNo.Text + "_Units" + " (s_number,ModelCode,Description,Color,Quantity)values('" + xs.ToString() + "','" + ViewState["MCode"].ToString() + "','" + ViewState["Decs"].ToString() + "','" + ViewState["Color"].ToString() + "','" + ViewState["Q"].ToString() + "')";
                    SqlCommand commm = new SqlCommand(query1, conn);
                    //Response.Write(query1);
                    commm.ExecuteNonQuery();
                }
                conn.Close();

                conn.Open();
                string asss = "select ModelCode,Description,Color,Quantity from " + vars + txt_tab1_POReferenceNo.Text + "_Units";
                //Response.Write(ass);
                SqlDataAdapter adapters = new SqlDataAdapter(asss, conn);

                DataSet sets = new DataSet();
                adapters.Fill(sets);

                //grid_PurchaseOrderDetails1.DataSource = null;
                Bind(vars, sets, vars);
                //grid_Addeds.DataSource = sets.Tables[0];
                //grid_Addeds.DataBind();
                conn.Close();
            }
            catch(Exception ex)
            {
                conn.Close();


                int counts = grid_AddedItems.Rows.Count;
                conn.Open();
                for (int c = 0; c <= counts - 1; c++)
                {
                    ViewState["MCode"] = grid_AddedItems.Rows[c].Cells[1].Text;
                    ViewState["Decs"] = grid_AddedItems.Rows[c].Cells[2].Text;
                    ViewState["Color"] = grid_AddedItems.Rows[c].Cells[3].Text;
                    ViewState["Q"] = grid_AddedItems.Rows[c].Cells[4].Text;
                    int xs = c + 1;
                    string query1 = "insert into " + vars + txt_tab1_POReferenceNo.Text + "_Units" + " (s_number,ModelCode,Description,Color,Quantity)values('" + xs.ToString() + "','" + ViewState["MCode"].ToString() + "','" + ViewState["Decs"].ToString() + "','" + ViewState["Color"].ToString() + "','" + ViewState["Q"].ToString() + "')";
                    SqlCommand commm = new SqlCommand(query1, conn);
                    //Response.Write(query1);
                    commm.ExecuteNonQuery();
                }
                conn.Close();

                //reset s_number
                conn.Open();
                string remover = "UPDATE " + vars + txt_tab1_POReferenceNo.Text + "_Units SET s_number = NULL";
                SqlCommand ms = new SqlCommand(remover, conn);
                ms.ExecuteNonQuery();

                string snumber = "With cte As(Select id, s_number,  Row_Number() Over (Order By s_number) As rn From " + vars + txt_tab1_POReferenceNo.Text + "_Units)Update cte Set s_number = rn";
                SqlCommand ssqu = new SqlCommand(snumber, conn);

                ssqu.ExecuteNonQuery();
                conn.Close();
                //reset s_number
                conn.Open();
                string asss = "select ModelCode,Description,Color,Quantity from " + vars + txt_tab1_POReferenceNo.Text + "_Units";
                //Response.Write(ass);
                SqlDataAdapter adapters = new SqlDataAdapter(asss, conn);

                DataSet sets = new DataSet();
                adapters.Fill(sets);

                //grid_PurchaseOrderDetails1.DataSource = null;
                Bind(vars, sets, vars);
                //grid_Addeds.DataSource = sets.Tables[0];
                //grid_Addeds.DataBind();
                conn.Close();
            }




            try
            {


                for (int l = 0; l <= rs - 1; l++)
                {
                    conn.Open();
                    //reading/gettign of data
                    string query = "WITH unitsCTE AS (SELECT *, Row_number() OVER (Partition BY ModelCode, Color ORDER BY s_number) AS rows FROM " + vars + txt_tab1_POReferenceNo.Text + "_Units) SELECT ModelCode,Color,Quantity FROM unitsCTE WHERE rows > 1";
                    SqlCommand com = new SqlCommand(query, conn);
                    IDataReader r;

                    r = com.ExecuteReader();

                    r.Read();
                    ViewState["code"] = r.GetString(0).ToString();//secon row
                    //ViewState["desc"] = r.GetString(1).ToString();
                    ViewState["colser"] = r.GetString(1).ToString();//secon row
                    ViewState["qquan"] = r.GetString(2).ToString();
                    conn.Close();
                    //reading/gettign of data

                    int old = Convert.ToInt32(ViewState["qquan"]);//Convert.ToInt32(txt_tab2_Quantity.Text);



                    //deleting of row1
                    qs = "WITH unitsCTE AS (SELECT *, Row_number() OVER (Partition BY ModelCode, Color ORDER BY s_number) AS rows FROM " + vars + txt_tab1_POReferenceNo.Text + "_Units) delete from unitsCTE where modelcode = '" + ViewState["code"].ToString() + "' and rows > 1 ";//delete from unitsCTE WHERE rows > 1"
                    SqlCommand mn = new SqlCommand(qs, conn);
                    conn.Open();
                    mn.ExecuteNonQuery();
                    conn.Close();
                    //deleteing of row


                    string sel = "select Quantity from " + vars + txt_tab1_POReferenceNo.Text + "_Units where ModelCode = '" + ViewState["code"].ToString() + "' and Color = '" + ViewState["colser"].ToString() + "' order by s_number";
                    SqlCommand cmd = new SqlCommand(sel, conn);
                    conn.Open();
                    SqlDataReader rdr;
                    rdr = cmd.ExecuteReader();
                    rdr.Read();
                    int dest = Convert.ToInt32(rdr.GetString(0));
                    conn.Close();

                    int finals = old + dest;

                    string ups = "update " + vars + txt_tab1_POReferenceNo.Text + "_Units set Quantity = '" + finals.ToString() + "' Where ModelCode ='" + ViewState["code"].ToString() + "' and Color = '" + ViewState["colser"].ToString() + "'";
                    SqlCommand ss = new SqlCommand(ups, conn);
                    conn.Open();
                    ss.ExecuteNonQuery();
                    conn.Close();


                    conn.Open();
                    string asss = "select ModelCode,Description,Color,Quantity from " + vars + txt_tab1_POReferenceNo.Text + "_Units";
                    //Response.Write(ass);
                    SqlDataAdapter adapters = new SqlDataAdapter(asss, conn);

                    DataSet sets = new DataSet();
                    adapters.Fill(sets);
                    Bind(vars, sets, vars);
                    //grid_PurchaseOrderDetails1.DataSource = null;
                    //grid_Addeds.DataSource = sets.Tables[0];
                    //grid_Addeds.DataBind();

                    conn.Close();
                    //databind
                    //if (ViewState["adders"].ToString() == "first")
                    
                    //////////////////////////////////////////////////
                    //reset s_number
                    conn.Open();
                    string removers = "UPDATE " + vars + txt_tab1_POReferenceNo.Text + "_Units SET s_number = NULL";
                    SqlCommand mss = new SqlCommand(removers, conn);
                    mss.ExecuteNonQuery();

                    string snumbers = "With cte As(Select id, s_number,  Row_Number() Over (Order By s_number) As rn From " + vars + txt_tab1_POReferenceNo.Text + "_Units)Update cte Set s_number = rn";
                    SqlCommand ssqus = new SqlCommand(snumbers, conn);

                    ssqus.ExecuteNonQuery();
                    conn.Close();
                    //reset s_number


                }
            }
            catch (Exception ex)
            {
                conn.Close();
                // Response.Write(ex.Message);

                conn.Open();
                string asss = "select ModelCode,Description,Color,Quantity from " + vars + txt_tab1_POReferenceNo.Text + "_Units";
                //Response.Write(ass);
                SqlDataAdapter adapters = new SqlDataAdapter(asss, conn);

                DataSet sets = new DataSet();
                adapters.Fill(sets);

                //grid_PurchaseOrderDetails1.DataSource = null;
                Bind(vars, sets,vars);
                //grid_Addeds.DataSource = sets.Tables[0];
                //grid_Addeds.DataBind();
                conn.Close();
                //databind

            }


            grid_AddedItems.DataSource = null;
            grid_AddedItems.DataBind();

            MultiView1.ActiveViewIndex = 1;
            MultiView4.ActiveViewIndex = 0;

        }
        
        private void Bind(string varr, DataSet a, string vvv)
        {
            if (varr == "a")
            {
                grid_PurchaseList.DataSource = a.Tables[0];
                grid_PurchaseList.DataBind();
            }
            else if (varr == "b")
            {
                grid_Addeds.DataSource = a.Tables[0];
                grid_Addeds.DataBind();
               // if (vvv == "a")
                        lbl_Added.Visible = true;
                 //   else
                    //    lbl_Added.Visible = !false;
            }
        }

        protected void Timer4_Tick(object sender, EventArgs e)
        {
            string data1 = "";
            string data2 = "";
            string data3 = "";
            string data4 = "";
            //1creates column
            DataTable dt = new DataTable();
            dt.Columns.AddRange(new DataColumn[4] { new DataColumn("Model Code"), new DataColumn("Color"), new DataColumn("Stocks"), new DataColumn("Unit Price") });
            ViewState["Ordera"] = dt;
            //2creates column
            //1binds the column to the gridview
            grid_Updates1.DataSource = (DataTable)ViewState["Ordera"];
            grid_Updates1.DataBind();
            //2binds the column to the gridview
            //1declares a datatable
            DataTable dts = (DataTable)ViewState["Ordera"];
            //2decalres a datatable


            int cont = grid_Addeds.Rows.Count;
            for (int x = 0; x <= cont - 1; x++)
            {
                hpiconn.Open();
                string mc = grid_Addeds.Rows[x].Cells[1].Text;
                string col = grid_Addeds.Rows[x].Cells[3].Text;
                string se = "select ModelCode, color,quantity,initialprice from SystemModelsTBL where modelcode = '" + mc + "' and color = '" + col + "'";
                SqlCommand come = new SqlCommand(se, hpiconn);
                IDataReader rr;
                rr = come.ExecuteReader();
                while (rr.Read())
                {
                    data1 = rr.GetString(0).ToString();
                    data2 = rr.GetString(1).ToString();
                    data3 = rr.GetString(2).ToString();
                    data4 = rr.GetString(3).ToString();
                    dts.Rows.Add(data1, data2,data3,data4);
                }
                hpiconn.Close();
            }


            ViewState["Ordera"] = dts;
            grid_Updates1.DataSource = (DataTable)ViewState["Ordera"];
            grid_Updates1.DataBind();
        }

        protected void grid_Addeds_SelectedIndexChanged(object sender, EventArgs e)
        {
            ddwn_Colors2.Items.Clear();
            ViewState["coda"] = grid_Addeds.SelectedRow.Cells[1].Text;
            ViewState["idssa"] = grid_Addeds.SelectedRow.Cells[1].Text;
            ViewState["mcID"] = grid_Addeds.SelectedIndex;
            // ViewState["colorr"] = grid_PurchaseOrderDetails1.SelectedRow.Cells[3].Text;

            ////if (txt_PONumber.Visible == true)
            ////{
            ////    string wow = grid_PurchaseOrderDetails.SelectedRow.Cells[1].Text;
            string query = "select color from SystemModelsTBL where ModelCode='" + ViewState["coda"].ToString() + "'";
            hpiconn.Open();
            SqlCommand cmds = new SqlCommand(query, hpiconn);
            SqlDataReader dr;
            dr = cmds.ExecuteReader();

            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    ddwn_Colors2.Items.Add(dr[0].ToString());
                }
            }
            hpiconn.Close();
            ////    Response.Write("jerome ko");
            ////}
            ////else
            ////{


            btn_EditItem.Visible = !true;
            btn_RemoveItem.Visible = !true;

            lbl_Quan.Visible = !true;
            txt_quants.Visible = !true;
            lbl_Color.Visible = !true;
            ddwn_Colors.Visible = !true;
            btn_SaveEdit.Visible = !true;
            btn_CancelEdit.Visible = !true;

            lbl_Quan2.Visible = !true;
            txt_quants2.Visible = !true;
            lbl_Color2.Visible = !true;
            ddwn_Colors2.Visible = !true;
            btn_SaveEdit2.Visible = !true;
            btn_CancelEdit2.Visible = !true;
            
            btn_EditItem2.Visible = true;
            btn_RemoveItem2.Visible = true;

            //btn_AddItem.Visible = true;
            //btn_AddItem1.Visible = !true;
            ViewState["adders"] = "second";

            grid_PurchaseList.SelectedIndex = -1;
        }

        protected void btn_EditItem2_Click(object sender, EventArgs e)
        {
            lbl_Quan2.Visible = true;
            txt_quants2.Visible = true;
            lbl_Color2.Visible = true;
            ddwn_Colors2.Visible = true;
            btn_SaveEdit2.Visible = true;
            btn_CancelEdit2.Visible = true;
        }

        protected void btn_RemoveItem2_Click(object sender, EventArgs e)
        {
            conn.Open();
            int remove = grid_Addeds.SelectedIndex;
            int firemove = remove + 1;
            string co = "delete from b" + txt_tab1_POReferenceNo.Text + "_Units where s_number = '" + firemove.ToString() + "'";
            SqlCommand commm = new SqlCommand(co, conn);

            commm.ExecuteNonQuery();
            conn.Close();
            /////////////////////////////////
            //reset s_number
            conn.Open();
            string remover = "UPDATE b" + txt_tab1_POReferenceNo.Text + "_Units SET s_number = NULL";
            SqlCommand ms = new SqlCommand(remover, conn);
            ms.ExecuteNonQuery();

            string snumber = "With cte As(Select id, s_number,  Row_Number() Over (Order By s_number) As rn From b" + txt_tab1_POReferenceNo.Text + "_Units)Update cte Set s_number = rn";
            SqlCommand ssqu = new SqlCommand(snumber, conn);

            ssqu.ExecuteNonQuery();
            conn.Close();
            //reset s_number

            string PON = txt_tab1_POReferenceNo.Text;
            lbl_tab1_POReferenceNo.Visible = true;
            txt_tab1_POReferenceNo.Visible = true;
            txt_tab1_POReferenceNo.Text = PON;

            conn.Open();
            string ass = "select ModelCode,Description,Color,Quantity from b" + PON + "_Units";
            //Response.Write(ass);
            SqlDataAdapter adapter = new SqlDataAdapter(ass, conn);

            DataSet set = new DataSet();
            adapter.Fill(set);

            grid_Addeds.DataSource = set.Tables[0];
            grid_Addeds.DataBind();
            conn.Close();

        }

        protected void btn_SaveEdit2_Click(object sender, EventArgs e)
        {
            string change;
            string qs = "";
            //ViewState["mcc"] = grid_PurchaseOrderDetails1.SelectedRow.Cells[1].Text;
            ViewState["mcc"] = grid_Addeds.SelectedIndex;
            int index = Convert.ToInt32(ViewState["mcc"]);//index ng grid
            int finalIndex = index + 1;//index ng db(s_number)
            ViewState["colorr"] = grid_Addeds.SelectedRow.Cells[3].Text;
            if (txt_quants2.Text == "" || txt_quants2.Text == null || ddwn_Colors2.Text == "" || ddwn_Colors2.Text == null)
            {
                change = "update b" + txt_tab1_POReferenceNo.Text + "_Units set Color = '" + ddwn_Colors2.Text + "' where s_number = '" + finalIndex.ToString() + "'";// and Color = '"+ViewState["colorr"].ToString()+"'";
            }
            else
            {
                change = "update b" + txt_tab1_POReferenceNo.Text + "_Units set Color = '" + ddwn_Colors2.Text + "', Quantity = '" + txt_quants2.Text + "' where s_number = '" + finalIndex.ToString() + "'";// and Color = '" + ViewState["colorr"].ToString()+"'";

            }
            conn.Open();
            SqlCommand sql = new SqlCommand(change, conn);
            sql.ExecuteNonQuery();
            conn.Close();

            string po = grid_SentPO.SelectedRow.Cells[1].Text;
            conn.Open();
            string u = "update systemUnitsorderlistsTBL set DateModified = '" + DateTime.Now.ToLongDateString() + "' where  purchaseorderNumber = '" + po + "'";
            SqlCommand q = new SqlCommand(u, conn);
            q.ExecuteNonQuery();
            conn.Close();


            //databind
            string PON = txt_tab1_POReferenceNo.Text;
            lbl_tab1_POReferenceNo.Visible = true;
            txt_tab1_POReferenceNo.Visible = true;
            txt_tab1_POReferenceNo.Text = PON;


            //string que = "slect ModelCode,Description,Color,Quantity from a" + PON + "_Units";
            //conn.Open();
            //SqlCommand scom = new SqlCommand(que, conn);
            //scom.ExecuteNonQuery();
            //conn.Close();
            conn.Open();
            string ass = "select ModelCode,Description,Color,Quantity from b" + PON + "_Units";
            //Response.Write(ass);
            SqlDataAdapter adapter = new SqlDataAdapter(ass, conn);

            DataSet set = new DataSet();
            adapter.Fill(set);

            //grid_PurchaseOrderDetails1.DataSource = null;

            grid_Addeds.DataSource = set.Tables[0];
            grid_Addeds.DataBind();
            conn.Close();
            //databind
            try
            {
                //reading/gettign of data
                string query = "WITH unitsCTE AS (SELECT *, Row_number() OVER (Partition BY ModelCode, Color ORDER BY s_number) AS rows FROM b" + PON + "_Units) SELECT ModelCode,Color,Quantity FROM unitsCTE WHERE rows > 1";
                SqlCommand com = new SqlCommand(query, conn);
                IDataReader r;
                conn.Open();
                r = com.ExecuteReader();

                r.Read();
                ViewState["code"] = r.GetString(0).ToString();//secon row
                //ViewState["desc"] = r.GetString(1).ToString();
                ViewState["colser"] = r.GetString(1).ToString();//secon row
                ViewState["qquan"] = r.GetString(2).ToString();
                conn.Close();
                //reading/gettign of data
                // Response.Write(ViewState["code"].ToString() + " " + ViewState["colser"].ToString() + " " + ViewState["qquan"].ToString());
                int old = Convert.ToInt32(ViewState["qquan"]);//Convert.ToInt32(txt_tab2_Quantity.Text);

                //deleting of row1
                qs = "WITH unitsCTE AS (SELECT *, Row_number() OVER (Partition BY ModelCode, Color ORDER BY s_number) AS rows FROM b" + PON + "_Units) delete from unitsCTE WHERE rows > 1";
                SqlCommand mn = new SqlCommand(qs, conn);
                conn.Open();
                mn.ExecuteNonQuery();
                conn.Close();
                //deleteing of row


                string sel = "select Quantity from b" + PON + "_Units where ModelCode = '" + ViewState["code"].ToString() + "' and Color = '" + ViewState["colser"].ToString() + "' order by s_number";
                SqlCommand cmd = new SqlCommand(sel, conn);
                conn.Open();
                SqlDataReader rdr;
                rdr = cmd.ExecuteReader();
                rdr.Read();
                int dest = Convert.ToInt32(rdr.GetString(0));
                conn.Close();

                int finals = old + dest;

                string ups = "update b" + PON + "_Units set Quantity = '" + finals.ToString() + "' Where ModelCode ='" + ViewState["code"].ToString() + "' and Color = '" + ViewState["colser"].ToString() + "'";
                SqlCommand ss = new SqlCommand(ups, conn);
                conn.Open();
                ss.ExecuteNonQuery();
                conn.Close();

                //databind
                string PONs = txt_tab1_POReferenceNo.Text;
                lbl_tab1_POReferenceNo.Visible = true;
                txt_tab1_POReferenceNo.Visible = true;
                txt_tab1_POReferenceNo.Text = PONs;


                //string que = "slect ModelCode,Description,Color,Quantity from a" + PON + "_Units";
                //conn.Open();
                //SqlCommand scom = new SqlCommand(que, conn);
                //scom.ExecuteNonQuery();
                //conn.Close();
                conn.Open();
                string asss = "select ModelCode,Description,Color,Quantity from b" + PONs + "_Units";
                //Response.Write(ass);
                SqlDataAdapter adapters = new SqlDataAdapter(asss, conn);

                DataSet sets = new DataSet();
                adapters.Fill(sets);

                //grid_PurchaseOrderDetails1.DataSource = null;

                grid_Addeds.DataSource = sets.Tables[0];
                grid_Addeds.DataBind();
                conn.Close();
                //databind
                //////////////////////////////////////////////////
                //reset s_number
                conn.Open();
                string remover = "UPDATE b" + txt_tab1_POReferenceNo.Text + "_Units SET s_number = NULL";
                SqlCommand ms = new SqlCommand(remover, conn);
                ms.ExecuteNonQuery();

                string snumber = "With cte As(Select id, s_number,  Row_Number() Over (Order By s_number) As rn From b" + txt_tab1_POReferenceNo.Text + "_Units)Update cte Set s_number = rn";
                SqlCommand ssqu = new SqlCommand(snumber, conn);

                ssqu.ExecuteNonQuery();
                conn.Close();
                //reset s_number

            }
            catch (Exception ex)
            {
                Response.Write("walang kaparehas");
            }
        }

        protected void btn_CancelEdit2_Click(object sender, EventArgs e)
        {
            lbl_Quan2.Visible = !true;
            txt_quants2.Visible = !true;
            lbl_Color2.Visible = !true;
            ddwn_Colors2.Visible = !true;
            btn_SaveEdit2.Visible = !true;
            btn_CancelEdit2.Visible = !true;
        }

        protected void btn_Dels_Click(object sender, EventArgs e)
        {
            Response.Write("delete if this order already proccessed completely");
        }

        protected void Timer5_Tick(object sender, EventArgs e)
        {
            string refnum = txt_tab1_POReferenceNo.Text;
            char refID = refnum[0];
            if (refID.ToString() == "0")//meaning units
            {
                grid_Availables.DataSourceID = "src_AvailUnits";
                grid_Availables.DataBind();
                MultiView4.ActiveViewIndex = 1;
            }
            else if (refID.ToString() == "1")
            {
                grid_Availables.DataSourceID = "src_AvailParts";
                grid_Availables.DataBind();
                MultiView4.ActiveViewIndex = 1;
            }
        }

        protected void btn_SecPO_Click(object sender, EventArgs e)
        {
            ViewState["adders"] = "second";

            if (txt_tab1_POReferenceNo.Text == "" || txt_tab1_POReferenceNo.Text == null)
            {

            }
            else
            {
                string refnum = txt_tab1_POReferenceNo.Text;
                char refID = refnum[0];
                if (refID.ToString() == "0")//meaning units
                {
                    grid_Availables.DataSourceID = "src_AvailUnits";
                    grid_Availables.DataBind();
                    MultiView4.ActiveViewIndex = 1;
                }
                else if (refID.ToString() == "1")
                {
                    grid_Availables.DataSourceID = "src_AvailParts";
                    grid_Availables.DataBind();
                    MultiView4.ActiveViewIndex = 1;
                }

            }
            btn_AddTo.Text = "Add to " + txt_tab1_POReferenceNo.Text;
          //  ViewState["adders"] = "second";
        }

        protected void ddwn_cats_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void ddwn_Stats_SelectedIndexChanged(object sender, EventArgs e)
        {
            stable();
        }

        //protected void grid_Inv_SelectedIndexChanged(object sender, EventArgs e)
        //{

        //}

        protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void btn_Inventory_Click(object sender, EventArgs e)
        {
            //MultiView1.ActiveViewIndex = 5;
            //MultiView5.ActiveViewIndex = 0;
            MultiView1.ActiveViewIndex = 4;
            MultiView5.ActiveViewIndex = 0;
        }

        protected void btn_Magnacycle_Click(object sender, EventArgs e)
        {
            MultiView5.ActiveViewIndex = 0;
        }

        protected void btn_Branch_Click(object sender, EventArgs e)
        {
            MultiView5.ActiveViewIndex = 1;
        }

        private void stable()
        {

            magnaconn.Open();
            if (ddwn_Stats.SelectedItem.Text == "(None)")
            {
                if (ddwn_cats.SelectedItem.Text == "Units")
                {
                    grid_Inv.DataSourceID = "src_InvUnits";
                    grid_Inv.DataBind();
                }
                else if (ddwn_cats.SelectedItem.Text == "Spare Parts")
                {
                    grid_Inv.DataSourceID = "src_InvParts";
                    grid_Inv.DataBind();
                }
            }
            else if (ddwn_Stats.SelectedItem.Text == "Safety Stocks")
            {
                grid_Inv.DataSourceID = null;
                string ss = "select modelcode ,description, color,quantity,status from systemunitsinventorytbl where status = 'Safety'";
                SqlDataAdapter mz = new SqlDataAdapter(ss, magnaconn);
                DataSet set = new DataSet();
                mz.Fill(set);
                grid_Inv.DataSource = set.Tables[0];
                grid_Inv.DataBind();
            }
            else if (ddwn_Stats.SelectedItem.Text == "Re-order Points")
            {
                grid_Inv.DataSourceID = null;
                string ss = "select modelcode ,description, color,quantity,status from systemunitsinventorytbl where status = 'Re-order'";
                SqlDataAdapter mz = new SqlDataAdapter(ss, magnaconn);
                DataSet set = new DataSet();
                mz.Fill(set);
                grid_Inv.DataSource = set.Tables[0];
                grid_Inv.DataBind();
            }
            else if (ddwn_Stats.SelectedItem.Text == "Critical Levels")
            {
                grid_Inv.DataSourceID = null;
                string ss = "select modelcode ,description, color,quantity,status from systemunitsinventorytbl where status = 'Critical'";
                SqlDataAdapter mz = new SqlDataAdapter(ss, magnaconn);
                DataSet set = new DataSet();
                mz.Fill(set);
                grid_Inv.DataSource = set.Tables[0];
                grid_Inv.DataBind();
            }
            else if (ddwn_Stats.SelectedItem.Text == "Out of Stock")
            {
                grid_Inv.DataSourceID = null;
                string ss = "select modelcode ,description, color,quantity,status from systemunitsinventorytbl where status = 'Out'";
                SqlDataAdapter mz = new SqlDataAdapter(ss, magnaconn);
                DataSet set = new DataSet();
                mz.Fill(set);
                grid_Inv.DataSource = set.Tables[0];
                grid_Inv.DataBind();
            }
            magnaconn.Close();
        }

        protected void ddwn_Statss_SelectedIndexChanged(object sender, EventArgs e)
        {
            stableb();
        }
        private void stableb()
        {
            conn.Open();
            if (ddwn_Statss.SelectedItem.Text == "(None)")
            {
                if (ddwn_Catss.SelectedItem.Text == "Units")
                {
                    grid_BInv.DataSourceID = "src_InvBunits";
                    grid_BInv.DataBind();
                }
                else if (ddwn_Catss.SelectedItem.Text == "Spare Parts")
                {
                    grid_BInv.DataSourceID = "src_InvBparts";
                    grid_BInv.DataBind();
                }
            }
            else if (ddwn_Statss.SelectedItem.Text == "Safety Stocks")
            {
                grid_BInv.DataSourceID = null;
                string ss = "select modelcode ,description, color,quantity,status from branchunitsinventorytbl where status = 'Safety'";
                SqlDataAdapter mz = new SqlDataAdapter(ss, conn);
                DataSet set = new DataSet();
                mz.Fill(set);
                grid_BInv.DataSource = set.Tables[0];
                grid_BInv.DataBind();
            }
            else if (ddwn_Statss.SelectedItem.Text == "Re-order Points")
            {
                grid_BInv.DataSourceID = null;
                string ss = "select modelcode ,description, color,quantity,status from branchunitsinventorytbl where status = 'Re-order'";
                SqlDataAdapter mz = new SqlDataAdapter(ss, conn);
                DataSet set = new DataSet();
                mz.Fill(set);
                grid_BInv.DataSource = set.Tables[0];
                grid_BInv.DataBind();
            }
            else if (ddwn_Statss.SelectedItem.Text == "Critical Levels")
            {
                grid_BInv.DataSourceID = null;
                string ss = "select modelcode ,description, color,quantity,status from branchunitsinventorytbl where status = 'Critical'";
                SqlDataAdapter mz = new SqlDataAdapter(ss, conn);
                DataSet set = new DataSet();
                mz.Fill(set);
                grid_BInv.DataSource = set.Tables[0];
                grid_BInv.DataBind();
            }
            else if (ddwn_Statss.SelectedItem.Text == "Out of Stock")
            {
                grid_BInv.DataSourceID = null;
                string ss = "select modelcode ,description, color,quantity,status from branchunitsinventorytbl where status = 'Out'";
                SqlDataAdapter mz = new SqlDataAdapter(ss, conn);
                DataSet set = new DataSet();
                mz.Fill(set);
                grid_BInv.DataSource = set.Tables[0];
                grid_BInv.DataBind();
            }
            conn.Close();
        }

        protected void Timer6_Tick(object sender, EventArgs e)
        {
            stable();
        }

        protected void btn_Stock_Click(object sender, EventArgs e)
        {
            try
            {
                lbl_SecStockReport.Visible = !true;
                // if (k)
                MultiView1.ActiveViewIndex = 5;
                lbl_PONum.Text = txt_tab1_POReferenceNo.Text;

                //1creates column
                DataTable dt = new DataTable();
                dt.Columns.AddRange(new DataColumn[4] { new DataColumn("Model Code"), new DataColumn("Description"), new DataColumn("Color"), new DataColumn("Status") });
                ViewState["orders1"] = dt;
                //2creates column
                DataTable dts = (DataTable)ViewState["orders1"];

                int stock = grid_PurchaseList.Rows.Count;
                for (int count = 0; count <= stock - 1; count++)
                {
                    string data1 = grid_PurchaseList.Rows[count].Cells[1].Text;
                    string data2 = grid_PurchaseList.Rows[count].Cells[3].Text;

                    string get = "select modelcode,description,color,status from systemunitsinventorytbl where modelcode = '" + data1 + "' and color = '" + data2 + "'";
                    SqlCommand mk = new SqlCommand(get, magnaconn);
                    SqlDataReader ii;
                    magnaconn.Open();
                    ii = mk.ExecuteReader();
                    ii.Read();
                    string mc = ii.GetString(0).ToString();
                    string de = ii.GetString(1).ToString();
                    string co = ii.GetString(2).ToString();
                    string st = ii.GetString(3).ToString();
                    dts.Rows.Add(mc, de, co, st);
                    magnaconn.Close();
                }
                ViewState["orders1"] = dts;
                grid_StockReport1.DataSource = (DataTable)ViewState["orders1"];
                grid_StockReport1.DataBind();
            }
            catch
            {
                //1creates column
                DataTable dt = new DataTable();
                dt.Columns.AddRange(new DataColumn[4] { new DataColumn("Model Code"), new DataColumn("Description"), new DataColumn("Color"), new DataColumn("Status") });
                ViewState["orders1"] = dt;
                //2creates column
                DataTable dts = (DataTable)ViewState["orders1"];


                int stock = grid_PurchaseList.Rows.Count;
                for (int count = 0; count <= stock - 1; count++)
                {
                    string data1 = grid_PurchaseList.Rows[count].Cells[1].Text;
                    string data2 = grid_PurchaseList.Rows[count].Cells[2].Text;
                    string data3 = grid_PurchaseList.Rows[count].Cells[3].Text;
                    string data4 = "(None)";

                    
                    dts.Rows.Add(data1, data2, data3, data4);
                
                }


                ViewState["orders1"] = dts;
                grid_StockReport1.DataSource = (DataTable)ViewState["orders1"];
                grid_StockReport1.DataBind();

              //  grid_PurchaseList
            }

            try
            {
                //1creates column
                DataTable dt1 = new DataTable();
                dt1.Columns.AddRange(new DataColumn[4] { new DataColumn("Model Code"), new DataColumn("Description"), new DataColumn("Color"), new DataColumn("Status") });
                ViewState["orders2"] = dt1;
                //2creates column
                DataTable dts1 = (DataTable)ViewState["orders2"];

                int stock1 = grid_Addeds.Rows.Count;
                for (int count = 0; count <= stock1 - 1; count++)
                {
                    string data1 = grid_Addeds.Rows[count].Cells[1].Text;
                    string data2 = grid_Addeds.Rows[count].Cells[3].Text;

                    string get = "select modelcode,description,color,status from branchunitsinventorytbl where modelcode = '" + data1 + "' and color = '" + data2 + "'";
                    SqlCommand mk = new SqlCommand(get, conn);
                    SqlDataReader ii;
                    conn.Open();
                    ii = mk.ExecuteReader();
                    ii.Read();
                    string mc = ii.GetString(0).ToString();
                    string de = ii.GetString(1).ToString();
                    string co = ii.GetString(2).ToString();
                    string st = ii.GetString(3).ToString();
                    dts1.Rows.Add(mc, de, co, st);
                    conn.Close();
                }
                ViewState["orders2"] = dts1;
                grid_StockReport2.DataSource = (DataTable)ViewState["orders2"];
                grid_StockReport2.DataBind();

                lbl_SecStockReport.Visible = true;
            }
            catch
            {

            }
        }

      

        protected void btn_Backs_Click(object sender, EventArgs e)
        {
            MultiView1.ActiveViewIndex = 1;
            MultiView4.ActiveViewIndex = 0;
        }

        protected void btn_Messaging_Click(object sender, EventArgs e)
        {
            try
            {
                string contacts = "select pldt,landline,mobile from systemcontactstbl";
                SqlCommand jj = new SqlCommand(contacts,hpiconn);
                SqlDataReader kk;
                hpiconn.Open();
                kk = jj.ExecuteReader();
                kk.Read();
                string pldt = kk.GetString(0).ToString();
                string landline = kk.GetString(1).ToString();
                string mobile = kk.GetString(2).ToString();
                hpiconn.Close();

                lbl_PLDT.Text = pldt;
                lbl_Landline.Text = landline;
                lbl_Mobile.Text = mobile;
            }
            catch 
            {
                if (hpiconn.State == ConnectionState.Open)
                    hpiconn.Close();
            }
            MultiView1.ActiveViewIndex = 3;
            MultiView6.ActiveViewIndex = 0;
            MultiView7.ActiveViewIndex = 0;


            messconn.Open();
            string ss = "select dateReceived as 'Date Received',Time from systemInboxtbl";
            SqlDataAdapter mz = new SqlDataAdapter(ss, messconn);
            DataSet set = new DataSet();
            mz.Fill(set);
            grid_Inbox.DataSource = set.Tables[0];
            grid_Inbox.DataBind();
            messconn.Close();

            txt_MessageContent.Text = "";
        }

        protected void btn_Inbox_Click(object sender, EventArgs e)
        {
            messconn.Open();
            string ss = "select dateReceived as 'Date Received',Time from systemInboxtbl";
            SqlDataAdapter mz = new SqlDataAdapter(ss, messconn);
            DataSet set = new DataSet();
            mz.Fill(set);
            grid_Inbox.DataSource = set.Tables[0];
            grid_Inbox.DataBind();
            messconn.Close();

            txt_MessageContent.Text = "";

            MultiView6.ActiveViewIndex = 0;
        }

        protected void btn_Sent_Click(object sender, EventArgs e)
        {
            messconn.Open();
            string ss = "select datesent as 'Date Sent',Time from systemsenttbl";
            SqlDataAdapter mz = new SqlDataAdapter(ss, messconn);
            DataSet set = new DataSet();
            mz.Fill(set);
            grid_Sent.DataSource = set.Tables[0];
            grid_Sent.DataBind();
            messconn.Close();

            txt_MessageContent.Text = "";
            MultiView6.ActiveViewIndex = 1;
        }

        protected void btn_Drafts_Click(object sender, EventArgs e)
        {
            messconn.Open();
            string ss = "select date as 'Date Saved' ,Time from systemdraftstbl";
            SqlDataAdapter mz = new SqlDataAdapter(ss, messconn);
            DataSet set = new DataSet();
            mz.Fill(set);
            grid_Drafts.DataSource = set.Tables[0];
            grid_Drafts.DataBind();
            messconn.Close();
            MultiView6.ActiveViewIndex = 2;
        }

        protected void btn_Uploads_Click(object sender, EventArgs e)
        {
            MultiView6.ActiveViewIndex = 3;
        }

        protected void btn_Compose_Click(object sender, EventArgs e)
        {
            MultiView7.ActiveViewIndex = 0;
        }

        protected void btn_Upload_Click(object sender, EventArgs e)
        {
            MultiView7.ActiveViewIndex = 1;
        }

        protected void btn_SaveDraft_Click(object sender, EventArgs e)
        {
            string save = "insert into systemdraftstbl (date,time,message)values('" + DateTime.Now.ToLongDateString() + "','" + DateTime.Now.ToLongTimeString() + "','" + txt_MessageContent.Text+ "')";
            SqlCommand draf = new SqlCommand(save,messconn);
            messconn.Open();
            draf.ExecuteNonQuery();
            messconn.Close();

            //reset s_number
            messconn.Open();
            string removers = "UPDATE systemdraftstbl SET s_number = NULL";
            SqlCommand mss = new SqlCommand(removers, messconn);
            mss.ExecuteNonQuery();

            string snumbers = "With cte As(Select id, s_number,  Row_Number() Over (Order By s_number) As rn From systemdraftstbl)Update cte Set s_number = rn";
            SqlCommand ssqus = new SqlCommand(snumbers, messconn);

            ssqus.ExecuteNonQuery();
            messconn.Close();
            //reset s_number

            messconn.Open();
            string ss = "select Date as 'Date Saved',Time from systemdraftstbl";
            SqlDataAdapter mz = new SqlDataAdapter(ss, messconn);
            DataSet set = new DataSet();
            mz.Fill(set);
            grid_Drafts.DataSource = set.Tables[0];
            grid_Drafts.DataBind();
            messconn.Close();

            txt_MessageContent.Text = "";

            MultiView6.ActiveViewIndex = 2;
        }

        protected void grid_Drafts_SelectedIndexChanged(object sender, EventArgs e)
        {
            string date = grid_Drafts.SelectedRow.Cells[1].Text;
            string time = grid_Drafts.SelectedRow.Cells[2].Text;

            string contacts = "select message from systemdraftstbl where date = '"+date+"' and time = '"+time+"'";
            SqlCommand jj = new SqlCommand(contacts, messconn);
            SqlDataReader kk;
            messconn.Open();
            kk = jj.ExecuteReader();
            kk.Read();
            txt_MessageContent.Text = kk.GetString(0).ToString();
            messconn.Close();
        }

        protected void btn_DelDraft_Click(object sender, EventArgs e)
        {
            string date = grid_Drafts.SelectedRow.Cells[1].Text;
            string time = grid_Drafts.SelectedRow.Cells[2].Text;

            string delete = "delete from systemdraftstbl where date = '" + date + "' and time = '" + time + "'";
            SqlCommand del = new SqlCommand(delete, messconn);
            messconn.Open();
            del.ExecuteNonQuery();
            messconn.Close();

            messconn.Open();
            string ss = "select Date as 'Date Saved',Time from systemdraftstbl";
            SqlDataAdapter mz = new SqlDataAdapter(ss, messconn);
            DataSet set = new DataSet();
            mz.Fill(set);
            grid_Drafts.DataSource = set.Tables[0];
            grid_Drafts.DataBind();
            messconn.Close();
            txt_MessageContent.Text = "";
        }

        protected void btn_DeleteAll_Click(object sender, EventArgs e)
        {
            string delete = "truncate table systemdraftstbl";
            SqlCommand del = new SqlCommand(delete, messconn);
            messconn.Open();
            del.ExecuteNonQuery();
            messconn.Close();

            messconn.Open();
            string ss = "select Date as 'Date Saved',Time from systemdraftstbl";
            SqlDataAdapter mz = new SqlDataAdapter(ss, messconn);
            DataSet set = new DataSet();
            mz.Fill(set);
            grid_Drafts.DataSource = set.Tables[0];
            grid_Drafts.DataBind();
            messconn.Close();
            txt_MessageContent.Text = "";
        }

        protected void btn_SendMess_Click(object sender, EventArgs e)
        {
            string sent = "insert into systemsenttbl (datesent,time,message)values('" + DateTime.Now.ToLongDateString() + "','" + DateTime.Now.ToLongTimeString() + "','" + txt_MessageContent.Text + "')";
            SqlCommand send = new SqlCommand(sent, messconn);
            messconn.Open();
            send.ExecuteNonQuery();
            messconn.Close();

            //reset s_number
            messconn.Open();
            string removers = "UPDATE systemsenttbl SET s_number = NULL";
            SqlCommand mss = new SqlCommand(removers, messconn);
            mss.ExecuteNonQuery();

            string snumbers = "With cte As(Select id, s_number,  Row_Number() Over (Order By s_number) As rn From systemsenttbl)Update cte Set s_number = rn";
            SqlCommand ssqus = new SqlCommand(snumbers, messconn);

            ssqus.ExecuteNonQuery();
            messconn.Close();
            //reset s_number

            messconn.Open();
            string ss = "select datesent as 'Date Sent',Time from systemsenttbl";
            SqlDataAdapter mz = new SqlDataAdapter(ss, messconn);
            DataSet set = new DataSet();
            mz.Fill(set);
            grid_Sent.DataSource = set.Tables[0];
            grid_Sent.DataBind();
            messconn.Close();

           

            string sents = "insert into systeminboxtbl (datereceived,time,message,status)values('" + DateTime.Now.ToLongDateString() + "','" + DateTime.Now.ToLongTimeString() + "','" + txt_MessageContent.Text + "','New')";
            SqlCommand sends = new SqlCommand(sents, hpiconn);
            hpiconn.Open();
            sends.ExecuteNonQuery();
            hpiconn.Close();

            //reset s_number
            hpiconn.Open();
            string remover = "UPDATE systeminboxtbl SET s_number = NULL";
            SqlCommand ms = new SqlCommand(remover, hpiconn);
            ms.ExecuteNonQuery();

            string snumber = "With cte As(Select id, s_number,  Row_Number() Over (Order By s_number) As rn From systeminboxtbl)Update cte Set s_number = rn";
            SqlCommand ssqu = new SqlCommand(snumber, hpiconn);

            ssqu.ExecuteNonQuery();
            hpiconn.Close();
            //reset s_number
            
            txt_MessageContent.Text = "";
            MultiView6.ActiveViewIndex = 1;
        }

        protected void grid_Sent_SelectedIndexChanged(object sender, EventArgs e)
        {
            string date = grid_Sent.SelectedRow.Cells[1].Text;
            string time = grid_Sent.SelectedRow.Cells[2].Text;

            string contacts = "select message from systemsenttbl where datesent = '" + date + "' and time = '" + time + "'";
            SqlCommand jj = new SqlCommand(contacts, messconn);
            SqlDataReader kk;
            messconn.Open();
            kk = jj.ExecuteReader();
            kk.Read();
            txt_MessageContent.Text = kk.GetString(0).ToString();
            messconn.Close();

           
        }

        protected void grid_Inbox_SelectedIndexChanged(object sender, EventArgs e)
        {
            string date = grid_Inbox.SelectedRow.Cells[1].Text;
            string time = grid_Inbox.SelectedRow.Cells[2].Text;

            string contacts = "select message from systeminboxtbl where datereceived = '" + date + "' and time = '" + time + "'";
            SqlCommand jj = new SqlCommand(contacts, messconn);
            SqlDataReader kk;
            messconn.Open();
            kk = jj.ExecuteReader();
            kk.Read();
            txt_MessageContent.Text = kk.GetString(0).ToString();
            messconn.Close();

        }

        protected void btn_DeleteAllInbox_Click(object sender, EventArgs e)
        {
            string delete = "truncate table systeminboxtbl";
            SqlCommand del = new SqlCommand(delete, messconn);
            messconn.Open();
            del.ExecuteNonQuery();
            messconn.Close();

            messconn.Open();
            string ss = "select Datereceived as 'Date Received',Time from systeminboxtbl";
            SqlDataAdapter mz = new SqlDataAdapter(ss, messconn);
            DataSet set = new DataSet();
            mz.Fill(set);
            grid_Inbox.DataSource = set.Tables[0];
            grid_Inbox.DataBind();
            messconn.Close();
            txt_MessageContent.Text = "";
        }

        protected void btn_DeleteInbox_Click(object sender, EventArgs e)
        {
            string date = grid_Inbox.SelectedRow.Cells[1].Text;
            string time = grid_Inbox.SelectedRow.Cells[2].Text;

            string delete = "delete from systeminboxtbl where Datereceived = '" + date + "' and time = '" + time + "'";
            SqlCommand del = new SqlCommand(delete, messconn);
            messconn.Open();
            del.ExecuteNonQuery();
            messconn.Close();

            messconn.Open();
            string ss = "select Datereceived as 'Date Received',Time from systeminboxtbl";
            SqlDataAdapter mz = new SqlDataAdapter(ss, messconn);
            DataSet set = new DataSet();
            mz.Fill(set);
            grid_Inbox.DataSource = set.Tables[0];
            grid_Inbox.DataBind();
            messconn.Close();
            txt_MessageContent.Text = "";
        }

        protected void Button25_Click(object sender, EventArgs e)
        {

        }

        protected void btn_DeleteSent_Click(object sender, EventArgs e)
        {
            string date = grid_Sent.SelectedRow.Cells[1].Text;
            string time = grid_Sent.SelectedRow.Cells[2].Text;

            string delete = "delete from systemsenttbl where Datesent = '" + date + "' and time = '" + time + "'";
            SqlCommand del = new SqlCommand(delete, messconn);
            messconn.Open();
            del.ExecuteNonQuery();
            messconn.Close();

            messconn.Open();
            string ss = "select Datesent as 'Date Sent',Time from systemsenttbl";
            SqlDataAdapter mz = new SqlDataAdapter(ss, messconn);
            DataSet set = new DataSet();
            mz.Fill(set);
            grid_Sent.DataSource = set.Tables[0];
            grid_Sent.DataBind();
            messconn.Close();
            txt_MessageContent.Text = "";
        }

        protected void btn_deleteAllSent_Click(object sender, EventArgs e)
        {
            string delete = "truncate table systemsenttbl";
            SqlCommand del = new SqlCommand(delete, messconn);
            messconn.Open();
            del.ExecuteNonQuery();
            messconn.Close();

            messconn.Open();
            string ss = "select Datesent as 'Date Sent',Time from systemsenttbl";
            SqlDataAdapter mz = new SqlDataAdapter(ss, messconn);
            DataSet set = new DataSet();
            mz.Fill(set);
            grid_Sent.DataSource = set.Tables[0];
            grid_Sent.DataBind();
            messconn.Close();
            txt_MessageContent.Text = "";
        }

        protected void btn_Up_Click(object sender, EventArgs e)
        {
            if (FileUpload1.HasFile)
            {
                FileUpload1.PostedFile.SaveAs(Server.MapPath("~/FileUploaded/") + FileUpload1.FileName);

            }

            foreach (string strFile in Directory.GetFiles(Server.MapPath("~/FileUploaded/")))
            {
                FileInfo f1 = new FileInfo(strFile);


            }
        }




        

        
    }
}