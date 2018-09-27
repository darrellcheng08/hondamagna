using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using System.Text.RegularExpressions;

namespace HondaMagnacycleProcurementProject
{
    public partial class frm_HondaPhilsIncDefault : System.Web.UI.Page
    {
        SqlConnection hpiconn = new SqlConnection(ConfigurationManager.ConnectionStrings["HPILogInConnectionString"].ConnectionString);
      
        protected void Page_Load(object sender, EventArgs e)
        {
            //MultiView1.ActiveViewIndex = 0;
            if (!IsPostBack)
            {
                int x = 2018;
                for (int a = x; a >= 1905; a--)
                {
                    ddwn_sAdYear.Items.Add(new ListItem(a.ToString()));
                }
            }


            hpiconn.Open();
            string deafult = "Select DefaultPassword, RecoveryEmail from  SystemDefaultAccountTBL";
            SqlCommand amdns = new SqlCommand(deafult, hpiconn);
            IDataReader ree;
            ree = amdns.ExecuteReader();
            ree.Read();
            string pw = ree.GetString(0).ToString();
            lbl_RecEmail.Text = ree.GetString(1).ToString();
            hpiconn.Close();


            txt_defPass.Attributes.Add("value", pw);
            ViewState["pass"] = pw;

            if (!IsPostBack)
            {
                string time = DateTime.Now.ToString();
                hpiconn.Open();
                string que = "insert into SystemTimeRecordTBL(LoggedIn)values('"+time+"')";
                SqlCommand comm = new SqlCommand(que, hpiconn);
                comm.ExecuteNonQuery();
                hpiconn.Close();
                //insert into MagnacycleEmployersTBL(EmployerIDNumber,FirstName,MiddleName,Surname,Gender,Age,Birthday,Citizenship,Email)values(@EIDNumber,@FName,@MName,@SName,@Gdr,@Ages,@Bday,@Cship,@Emil)";
            }
           // txt_sAdUserName.Text = Request.QueryString["defaultAccount"];
        }

        protected void btn_sAdCancel_Click(object sender, EventArgs e)
        {
            txt_sAdID.ReadOnly = false;
            txt_sAdFname.ReadOnly = false;
            txt_sAdMname.ReadOnly = false;
            txt_sAdSname.ReadOnly = false;
            ddwn_sAdGender.Enabled = true;
            txt_sAdAge.ReadOnly = false;
            ddwn_sAdMonth.Enabled = true;
            ddwn_sAdDay.Enabled = true;
            ddwn_sAdYear.Enabled = true;
            txt_sAdCiti.ReadOnly = false;
            txt_sAdEmail.ReadOnly = false;

            txt_sAdID.ReadOnly = false;
            txt_sAdID.Text = "";
            txt_sAdFname.Text = "";
            txt_sAdMname.Text = "";
            txt_sAdSname.Text = "";
            ddwn_sAdGender.Text = "Male";
            txt_sAdAge.Text = "";
            ddwn_sAdMonth.SelectedIndex = 0;
            ddwn_sAdDay.SelectedIndex = 0;
            ddwn_sAdYear.SelectedIndex = 0;
            txt_sAdCiti.Text = "";
            txt_sAdEmail.Text = "";

            btn_sAdAdd.Text = "Add";
        }

        protected void btn_sAdAdd_Click(object sender, EventArgs e)
        {
            if (btn_sAdAdd.Text == "Add")
            {
                if (grid_superAdmins.Rows.Count == 2)
                {
                    Response.Write("Max");
                }
                else
                {
                    int counters = grid_superAdmins.Rows.Count;
                    for (int c = 0; c <= counters - 1; c++)
                    {
                        ViewState["idGetts"] = grid_superAdmins.Rows[c].Cells[1].Text;
                        ViewState["fnameGetts"] = grid_superAdmins.Rows[c].Cells[2].Text;
                        ViewState["mnameGetts"] = grid_superAdmins.Rows[c].Cells[3].Text;
                        ViewState["snameGetts"] = grid_superAdmins.Rows[c].Cells[4].Text;
                        ViewState["genderGetts"] = grid_superAdmins.Rows[c].Cells[5].Text;
                        ViewState["ageGetts"] = grid_superAdmins.Rows[c].Cells[6].Text;
                        ViewState["bdayGetts"] = grid_superAdmins.Rows[c].Cells[7].Text;
                        ViewState["citizenshipGetts"] = grid_superAdmins.Rows[c].Cells[8].Text;
                        //ViewState["emailGetter"] = grid_Employers.Rows[c].Cells[9].Text;


                        if (ViewState["idGetts"].ToString() == txt_sAdID.Text)
                        {
                            lbl_warning.Text = "This employer ID already exist." + "<br />" + "Generate new ID for the new employer you want to add?";
                            lbl_warning.Visible = true;
                            btn_sAdOk3.Visible = true;
                            btn_sAdCancel3.Visible = true;
                            break;
                        }

                        //loop ulit
                    }
                    if (lbl_warning.Visible == true)
                    {

                    }
                    else if (lbl_warning.Visible == false)
                    {
                        ViewState["choiceeee"] = null;
                        for (int c = 0; c <= counters - 1; c++)
                        {
                            ViewState["idGettahhs"] = grid_superAdmins.Rows[c].Cells[1].Text;
                            ViewState["fnameGetts"] = grid_superAdmins.Rows[c].Cells[2].Text;
                            ViewState["mnameGetts"] = grid_superAdmins.Rows[c].Cells[3].Text;
                            ViewState["snameGetts"] = grid_superAdmins.Rows[c].Cells[4].Text;
                            ViewState["genderGetts"] = grid_superAdmins.Rows[c].Cells[5].Text;
                            ViewState["ageGetts"] = grid_superAdmins.Rows[c].Cells[6].Text;
                            ViewState["bdayGetts"] = grid_superAdmins.Rows[c].Cells[7].Text;
                            ViewState["citizenshipGetts"] = grid_superAdmins.Rows[c].Cells[8].Text;

                            ViewState["bdayTesterrrs"] = ddwn_sAdMonth.SelectedItem.Text + " " + ddwn_sAdDay.SelectedItem.Text + ", " + ddwn_sAdYear.SelectedItem.Text;

                            if (ViewState["fnameGetts"].ToString() == txt_sAdFname.Text && ViewState["mnameGetts"].ToString() == txt_sAdMname.Text && ViewState["snameGetts"].ToString() == txt_sAdSname.Text && ViewState["genderGetts"].ToString() == ddwn_sAdGender.Text && ViewState["ageGetts"].ToString() == txt_sAdAge.Text && ViewState["bdayGetts"].ToString() == ViewState["bdayTesterrrs"].ToString() && ViewState["citizenshipGetts"].ToString() == txt_sAdCiti.Text)
                            {
                                lbl_warning.Text = "This employer has same details with " + ViewState["idGettahhs"].ToString() + "<br />" + "Do you want to add this person anyway?";
                                lbl_warning.Visible = true;
                                btn_sAdOk2.Visible = true;
                                btn_sAdCancel2.Visible = true;
                                break;
                            }
                        }
                        if (lbl_warning.Visible == true)
                        {

                        }
                        else if (lbl_warning.Visible == false)
                        {
                            sAdAdder();
                            txt_sAdID.ReadOnly = false;
                            txt_sAdFname.ReadOnly = false;
                            txt_sAdMname.ReadOnly = false;
                            txt_sAdSname.ReadOnly = false;
                            ddwn_sAdGender.Enabled = true;
                            txt_sAdAge.ReadOnly = false;
                            ddwn_sAdMonth.Enabled = true;
                            ddwn_sAdDay.Enabled = true;
                            ddwn_sAdYear.Enabled = true;
                            txt_sAdCiti.ReadOnly = false;
                            txt_sAdEmail.ReadOnly = false;

                            txt_sAdID.ReadOnly = false;
                            txt_sAdID.Text = "";
                            txt_sAdFname.Text = "";
                            txt_sAdMname.Text = "";
                            txt_sAdSname.Text = "";
                            ddwn_sAdGender.Text = "Male";
                            txt_sAdAge.Text = "";
                            ddwn_sAdMonth.SelectedIndex = 0;
                            ddwn_sAdDay.SelectedIndex = 0;
                            ddwn_sAdYear.SelectedIndex = 0;
                            txt_sAdCiti.Text = "";
                            txt_sAdEmail.Text = "";
                            btn_sAdAdd.Text = "Add";
                        }
                    }
                }



            }
            else if (btn_sAdAdd.Text == "Save")
            {
                ViewState["monthsss"] = ddwn_sAdMonth.SelectedItem.Value;
                ViewState["daysss"] = ddwn_sAdDay.SelectedItem.Value;
                int counters = grid_superAdmins.Rows.Count;
                for (int w = 0; w <= counters - 1; w++)
                {
                    ViewState["idDDs"] = grid_superAdmins.Rows[w].Cells[1].Text;

                    if (ViewState["idDDs"].ToString() == ViewState["editRefasss"].ToString())
                    {
                        ViewState["fGettersss"] = grid_superAdmins.Rows[w].Cells[2].Text;///////////////////////////////////////////////
                        ViewState["mGettersss"] = grid_superAdmins.Rows[w].Cells[3].Text;
                        ViewState["sGettersss"] = grid_superAdmins.Rows[w].Cells[4].Text;
                        ViewState["gGettersss"] = grid_superAdmins.Rows[w].Cells[5].Text;
                        ViewState["aGettersss"] = grid_superAdmins.Rows[w].Cells[6].Text;
                        ViewState["bGettersss"] = grid_superAdmins.Rows[w].Cells[7].Text;
                        ViewState["cGettersss"] = grid_superAdmins.Rows[w].Cells[8].Text;
                        //ViewState["emailGetter"] = grid_Employers.Rows[c].Cells[9].Text;

                        ViewState["bDayTesterssss"] = ddwn_sAdMonth.SelectedItem.Text + " " + ddwn_sAdDay.SelectedItem.Text + ", " + ddwn_sAdYear.SelectedItem.Text;

                        if (txt_sAdID.Text == ViewState["editRefasss"].ToString())
                        {
                            for (int c = 0; c <= counters - 1; c++)
                            {
                                ViewState["idGetteraaa"] = grid_superAdmins.Rows[c].Cells[1].Text;
                                ViewState["fGettersss"] = grid_superAdmins.Rows[c].Cells[2].Text;
                                ViewState["mGettersss"] = grid_superAdmins.Rows[c].Cells[3].Text;
                                ViewState["sGettersss"] = grid_superAdmins.Rows[c].Cells[4].Text;
                                ViewState["gGettersss"] = grid_superAdmins.Rows[c].Cells[5].Text;
                                ViewState["aGettersss"] = grid_superAdmins.Rows[c].Cells[6].Text;
                                ViewState["bGettersss"] = grid_superAdmins.Rows[c].Cells[7].Text;
                                ViewState["cGettersss"] = grid_superAdmins.Rows[c].Cells[8].Text;

                                //ViewState["bdayTester"] = ddwn_Month.SelectedItem.Text + " " + ddwn_Day.SelectedItem.Text + ", " + ddwn_Year.SelectedItem.Text;

                                if (ViewState["fGettersss"].ToString() == txt_sAdFname.Text && ViewState["mGettersss"].ToString() == txt_sAdMname.Text && ViewState["sGettersss"].ToString() == txt_sAdSname.Text && ViewState["gGettersss"].ToString() == ddwn_sAdGender.Text && ViewState["aGettersss"].ToString() == txt_sAdAge.Text && ViewState["bGettersss"].ToString() == ViewState["bDayTesterssss"].ToString() && ViewState["cGettersss"].ToString() == txt_sAdCiti.Text)
                                {
                                    lbl_warning.Text = "This employer has same details with " + ViewState["idGetteraaa"].ToString() + "<br />" + "Do you want to update this person anyway?";
                                    lbl_warning.Visible = true;
                                    btn_sAdOk1.Visible = true;
                                    btn_sAdCancel1.Visible = true;
                                    break;
                                }
                            }
                            if (lbl_warning.Visible == true)
                            {

                            }
                            else if (lbl_warning.Visible == false)
                            {
                                superAdminUpdater();
                                txt_sAdID.ReadOnly = false;
                                txt_sAdFname.ReadOnly = false;
                                txt_sAdMname.ReadOnly = false;
                                txt_sAdSname.ReadOnly = false;
                                ddwn_sAdGender.Enabled = true;
                                txt_sAdAge.ReadOnly = false;
                                ddwn_sAdMonth.Enabled = true;
                                ddwn_sAdDay.Enabled = true;
                                ddwn_sAdYear.Enabled = true;
                                txt_sAdCiti.ReadOnly = false;
                                txt_sAdEmail.ReadOnly = false;

                                txt_sAdID.ReadOnly = false;
                                txt_sAdID.Text = "";
                                txt_sAdFname.Text = "";
                                txt_sAdMname.Text = "";
                                txt_sAdSname.Text = "";
                                ddwn_sAdGender.Text = "Male";
                                txt_sAdAge.Text = "";
                                ddwn_sAdMonth.SelectedIndex = 0;
                                ddwn_sAdDay.SelectedIndex = 0;
                                ddwn_sAdYear.SelectedIndex = 0;
                                txt_sAdCiti.Text = "";
                                txt_sAdEmail.Text = "";
                                btn_sAdAdd.Text = "Add";
                            }
                        }
                        else
                        {
                            //for checking of errors
                            Response.Write("txt_EmployerIDNumber.Text != ViewState['editRef'].ToString()");
                            Response.Write(txt_sAdID.Text + " " + ViewState["editRefsss"].ToString());
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
        private void sAdAdder()
        {
            hpiconn.Open();
            //try
            //{
            string inserts = "insert into SystemHPIUsersTBL(AdminID,FirstName,MiddleName,Surname,Gender,Age,Birthday,Citizenship,Email,UserName,Password)values(@adIDNumber,@FName,@MName,@SName,@Gdr,@Ages,@Bday,@Cship,@Emil,@User,@Pass)";
            SqlCommand comm = new SqlCommand(inserts, hpiconn);

            //string recieved = ViewState["AdminIDGetters"].ToString();
            //string querys = "Select AdminIDNumber,FirstName from SystemAdminsTBL where AdminIDNumber = " + recieved;
            //SqlCommand comma = new SqlCommand(querys, conn);
            //IDataReader rd;
            //conn.Open();
            //rd = comma.ExecuteReader();
            //rd.Read();
            //string adminidnumber = rd.GetString(0);//first row
            string sadminfirstname = txt_sAdFname.Text;
            string sfanames = sadminfirstname;
            sfanames = Regex.Replace(sfanames, @"\s", "");
            string sfinalName = sfanames;

            string sids = txt_sAdID.Text;
            //conn.Close();

            string sretrieved = "";
            string svals = sids;
            char sval1 = svals[svals.Length - 1];
            char sval2 = svals[svals.Length - 2];
            char sval3 = svals[svals.Length - 3];
            //if (val1 >= '0' && val1 <= '9')
            //{
            sretrieved = sval3.ToString() + sval2.ToString() + sval1.ToString();
            //}

            string sgeneratedUN = sfinalName + sretrieved;


            ViewState["admonthhh"] = ddwn_sAdMonth.SelectedItem.Value;
            ViewState["addayyy"] = ddwn_sAdDay.SelectedItem.Value;

            string sfaname = sadminfirstname;
            //faname = Regex.Replace(faname, @"\s", "");
            //string generatedUname = faname + ViewState["admonth"].ToString() + ViewState["adday"].ToString() + "User";

            // string uuname = "";
            int ss = grid_superAdmins.Rows.Count;
            bool bolls = false;
            string suname = "";
            for (int p = 0; p <= ss - 1; p++)
            {
                string sun = grid_superAdmins.Rows[p].Cells[4].Text;
                if (sgeneratedUN == sun)
                {
                    Random srnd = new Random();
                    int id = srnd.Next(100, 999);

                    string finame = sadminfirstname;
                    string sfaname1 = Regex.Replace(sfaname, @"\s", "");


                    string iids = sids;

                    string retrieveds = "";
                    string valss = iids;
                    char val1s = valss[valss.Length - 1];
                    char val2s = valss[valss.Length - 2];
                    char val3s = valss[valss.Length - 3];
                    //if (val1 >= '0' && val1 <= '9')
                    //{
                    retrieveds = val3s.ToString() + val2s.ToString() + val1s.ToString();
                    //}

                    suname = sfaname1 + retrieveds;

                    //uname = finame + ViewState["admonth"].ToString() + ViewState["adday"].ToString() + "User" + id.ToString();

                    bolls = true;
                    break;
                }
            }
            if (bolls == true)
                ViewState["uunameee"] = suname;
            else if (bolls == false)
                ViewState["uunameee"] = sgeneratedUN;

            if (ViewState["choiceeee"] == "ok")
            {
                string str = ViewState["eidddd"].ToString();

                string retrievedsae = "";
                string valssa = str;
                char val1sae = valssa[valssa.Length - 1];
                char val2sae = valssa[valssa.Length - 2];
                char val3sae = valssa[valssa.Length - 3];
                //if (val1 >= '0' && val1 <= '9')
                //{
                retrievedsae = val3sae.ToString() + val2sae.ToString() + val1sae.ToString();
                //}

                string finame = sadminfirstname;
                string faname1 = Regex.Replace(sfaname, @"\s", "");

                ViewState["uunameee"] = faname1 + retrievedsae;

                comm.Parameters.AddWithValue("@adIDNumber", ViewState["eidddd"].ToString());
                ViewState["choiceeee"] = null;
                //comm.Parameters.AddWithValue("@UName", txt_UserName.Text);
                //@adIDNumber,@FName,@MName,@SName,@Gdr,@Ages,@Bday,@Cship,@Emil,@User,@Pass
                comm.Parameters.AddWithValue("@FName", txt_sAdFname.Text);
                comm.Parameters.AddWithValue("@MName", txt_sAdMname.Text);
                comm.Parameters.AddWithValue("@SName", txt_sAdSname.Text);
                comm.Parameters.AddWithValue("@Gdr", ddwn_sAdGender.Text);
                comm.Parameters.AddWithValue("@Ages", txt_sAdAge.Text);
                comm.Parameters.AddWithValue("@Bday", ddwn_sAdMonth.SelectedItem.Text + " " + ddwn_sAdDay.SelectedItem.Text + ", " + ddwn_sAdYear.SelectedItem.Text);
                comm.Parameters.AddWithValue("@Cship", txt_sAdCiti.Text);
                comm.Parameters.AddWithValue("@Emil", txt_sAdEmail.Text);
                comm.Parameters.AddWithValue("@User", ViewState["uunameee"].ToString());//////////////////////////////
                comm.Parameters.AddWithValue("@Pass", "defaultPassword789");//////////////////////////////
                comm.ExecuteNonQuery();
                ViewState["monthsss"] = ddwn_sAdMonth.SelectedItem.Value;
                ViewState["daysss"] = ddwn_sAdDay.SelectedItem.Value;
                Response.Write("Addedchoice");
                grid_superAdmins.DataBind();
            }
            else if (ViewState["choiceeee"] == null || ViewState["choice1sss"] == "ok")
            {
                Response.Write(txt_sAdID.Text + txt_sAdFname.Text + txt_sAdMname.Text + txt_sAdSname.Text);
                comm.Parameters.AddWithValue("@adIDNumber", txt_sAdID.Text);
                //comm.Parameters.AddWithValue("@UName", txt_UserName.Text);
                comm.Parameters.AddWithValue("@FName", txt_sAdFname.Text);
                comm.Parameters.AddWithValue("@MName", txt_sAdMname.Text);
                comm.Parameters.AddWithValue("@SName", txt_sAdSname.Text);
                comm.Parameters.AddWithValue("@Gdr", ddwn_sAdGender.Text);
                comm.Parameters.AddWithValue("@Ages", txt_sAdAge.Text);
                comm.Parameters.AddWithValue("@Bday", ddwn_sAdMonth.SelectedItem.Text + " " + ddwn_sAdDay.SelectedItem.Text + ", " + ddwn_sAdYear.SelectedItem.Text);
                comm.Parameters.AddWithValue("@Cship", txt_sAdCiti.Text);
                comm.Parameters.AddWithValue("@Emil", txt_sAdEmail.Text);
                comm.Parameters.AddWithValue("@User", sgeneratedUN);//////////////////////////////
                comm.Parameters.AddWithValue("@Pass", "defaultPassword789");//////////////////////////////
                comm.ExecuteNonQuery();
                ViewState["monthsss"] = ddwn_sAdMonth.SelectedItem.Value;
                ViewState["daysss"] = ddwn_sAdDay.SelectedItem.Value;
                Response.Write("Addedchoice");
                grid_superAdmins.DataBind();
            }
            //}
            //catch (Exception ex)
            //{
            //    Response.Write(ex.Message);
            //}
            hpiconn.Close();
        }
        private void superAdminUpdater()
        {
            string ffname = txt_sAdFname.Text;
            string mmname = txt_sAdMname.Text;
            string ssname = txt_sAdSname.Text;
            string ggender = ddwn_sAdGender.Text;
            string aage = txt_sAdAge.Text;

            string bdaysm = ddwn_sAdMonth.SelectedItem.Text;
            string bdaysd = ddwn_sAdDay.SelectedItem.Text;
            string bdaysy = ddwn_sAdYear.SelectedItem.Text;

            string Compbday = bdaysm + " " + bdaysd + ", " + bdaysy;

            string bdaysmV = ddwn_sAdMonth.SelectedItem.Value.ToString();
            string bdaysdV = ddwn_sAdDay.SelectedItem.Value.ToString();

            string cciti = txt_sAdCiti.Text;
            string eemil = txt_sAdEmail.Text;
            //generate uname
            string genfname = ffname;
            genfname = Regex.Replace(genfname, @"\s", "");

            string idi = txt_sAdID.Text;

            string retrieved = "";
            string valse = idi;
            char val1e = valse[valse.Length - 1];
            char val2e = valse[valse.Length - 2];
            char val3e = valse[valse.Length - 3];

            retrieved = val3e.ToString() + val2e.ToString() + val1e.ToString();


            string orgigeneratedUname = genfname + retrieved;

            //check for current users
            int counter = grid_superAdmins.Rows.Count;
            if (counter == 0)
            {
                Response.Write("Counter = 0");//for checking
            }
            else
            {
                for (int a = 0; a <= counter - 1; a++)
                {
                    ViewState["testerrr"] = grid_superAdmins.Rows[a].Cells[1].Text;//Response.Write(ViewState["tester"].ToString() + ViewState["editRef"].ToString());
                    if (ViewState["testerrr"].ToString() == ViewState["editRefasss"].ToString())
                    {
                        hpiconn.Open();
                        string q2 = "UPDATE SystemHPIUsersTBL SET FirstName = '" + ffname + "',MiddleName = '" + mmname + "',Surname = '" + ssname + "', Gender = '" + ggender + "', Age = '" + aage + "', Birthday = '" + Compbday + "' ,Citizenship = '" + cciti + "', Email = '" + eemil + "' where AdminID = " + ViewState["editRefasss"].ToString();
                        SqlCommand finalcommss = new SqlCommand(q2, hpiconn);
                        //conn.Open();
                        finalcommss.ExecuteNonQuery();
                        hpiconn.Close();

                        hpiconn.Open();
                        string q3 = "Select UserName from SystemHPIUsersTBl where AdminID = " + ViewState["testerrr"];
                        SqlCommand comms = new SqlCommand(q3, hpiconn);
                        IDataReader r;
                        r = comms.ExecuteReader();
                        r.Read();
                        //string employeridno = r.GetString(0);//first row
                        string retrievedUname = r.GetString(0);
                        hpiconn.Close();

                        //string retrieved = "";
                        //string vals = retrievedUname;
                        //char val1 = vals[vals.Length - 1];
                        //char val2 = vals[vals.Length - 2];
                        //char val3 = vals[vals.Length - 3];
                        //if (val1 >= '0' && val1 <= '9')
                        //{
                        //    retrieved = val3.ToString() + val2.ToString() + val1.ToString();
                        //}


                        hpiconn.Open();
                        string qry = "UPDATE SystemHPIUsersTBL SET UserName = 'Removed' where AdminID = " + ViewState["testerrr"];
                        SqlCommand mand = new SqlCommand(qry, hpiconn);
                        //conn.Open();
                        mand.ExecuteNonQuery();
                        hpiconn.Close();

                        grid_superAdmins.DataBind();

                        int d = grid_superAdmins.Rows.Count;
                        string datas = "";
                        string newUname = "";
                        bool bol = false;
                        for (int x = 0; x <= d - 1; x++)
                        {
                            datas = grid_superAdmins.Rows[x].Cells[10].Text;
                            if (datas == orgigeneratedUname)
                            {
                                bol = true;
                                Random rnd = new Random();
                                int id = rnd.Next(100, 999);
                                string faname = ffname;
                                faname = Regex.Replace(faname, @"\s", "");
                                newUname = faname + bdaysmV + bdaysdV + "User" + id.ToString();

                                //update of username
                                hpiconn.Open();
                                string q22 = "UPDATE SystemHPIUsersTBL SET UserName = '" + newUname + "' where AdminID = " + ViewState["testerrr"];
                                SqlCommand mande = new SqlCommand(q22, hpiconn);
                                mande.ExecuteNonQuery();
                                hpiconn.Close();

                                grid_superAdmins.DataBind();

                                break;
                            }
                        }
                        if (bol == false)
                        {
                            //  newUname = orgigeneratedUname;

                            //update of username
                            hpiconn.Open();
                            string q22 = "UPDATE SystemHPIUsersTBL SET UserName = '" + orgigeneratedUname + "' where AdminID = " + ViewState["testerrr"];
                            SqlCommand mande = new SqlCommand(q22, hpiconn);
                            mande.ExecuteNonQuery();
                            hpiconn.Close();

                            grid_superAdmins.DataBind();
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

        protected void btn_sAdOk1_Click(object sender, EventArgs e)
        {
            superAdminUpdater();

            txt_sAdID.ReadOnly = false;
            txt_sAdFname.ReadOnly = false;
            txt_sAdMname.ReadOnly = false;
            txt_sAdSname.ReadOnly = false;
            ddwn_sAdGender.Enabled = true;
            txt_sAdAge.ReadOnly = false;
            ddwn_sAdMonth.Enabled = true;
            ddwn_sAdDay.Enabled = true;
            ddwn_sAdYear.Enabled = true;
            txt_sAdCiti.ReadOnly = false;
            txt_sAdEmail.ReadOnly = false;

            txt_sAdID.ReadOnly = false;
            txt_sAdID.Text = "";
            txt_sAdFname.Text = "";
            txt_sAdMname.Text = "";
            txt_sAdSname.Text = "";
            ddwn_sAdGender.Text = "Male";
            txt_sAdAge.Text = "";
            ddwn_sAdMonth.SelectedIndex = 0;
            ddwn_sAdDay.SelectedIndex = 0;
            ddwn_sAdYear.SelectedIndex = 0;
            txt_sAdCiti.Text = "";
            txt_sAdEmail.Text = "";
            btn_sAdAdd.Text = "Add";

            lbl_warning.Visible = false;
            btn_sAdOk1.Visible = false;
            btn_sAdCancel1.Visible = false;
        }

        protected void btn_sAdCancel1_Click(object sender, EventArgs e)
        {
            lbl_warning.Visible = false;
            btn_sAdOk1.Visible = false;
            btn_sAdCancel1.Visible = false;
        }

        protected void btn_sAdOk2_Click(object sender, EventArgs e)
        {
            ViewState["choice1sss"] = "ok";
            sAdAdder();

            txt_sAdID.ReadOnly = false;
            txt_sAdFname.ReadOnly = false;
            txt_sAdMname.ReadOnly = false;
            txt_sAdSname.ReadOnly = false;
            ddwn_sAdGender.Enabled = true;
            txt_sAdAge.ReadOnly = false;
            ddwn_sAdMonth.Enabled = true;
            ddwn_sAdDay.Enabled = true;
            ddwn_sAdYear.Enabled = true;
            txt_sAdCiti.ReadOnly = false;
            txt_sAdEmail.ReadOnly = false;

            txt_sAdID.ReadOnly = false;
            txt_sAdID.Text = "";
            txt_sAdFname.Text = "";
            txt_sAdMname.Text = "";
            txt_sAdSname.Text = "";
            ddwn_sAdGender.Text = "Male";
            txt_sAdAge.Text = "";
            ddwn_sAdMonth.SelectedIndex = 0;
            ddwn_sAdDay.SelectedIndex = 0;
            ddwn_sAdYear.SelectedIndex = 0;
            txt_sAdCiti.Text = "";
            txt_sAdEmail.Text = "";
            btn_sAdAdd.Text = "Add";

            ViewState["choice1sss"] = null;
            lbl_warning.Visible = false;
            btn_sAdOk2.Visible = false;
            btn_sAdCancel2.Visible = false;
        }

        protected void btn_sAdCancel2_Click(object sender, EventArgs e)
        {
            ViewState["choice1sss"] = null;
            lbl_warning.Visible = false;
            btn_sAdOk2.Visible = false;
            btn_sAdCancel2.Visible = false;
        }

        protected void btn_sAdOk3_Click(object sender, EventArgs e)
        {
            ViewState["choiceeee"] = "ok";
            Random rnd = new Random();
            ViewState["eidddd"] = rnd.Next(100000, 999999);

            sAdAdder();

            txt_sAdID.ReadOnly = false;
            txt_sAdFname.ReadOnly = false;
            txt_sAdMname.ReadOnly = false;
            txt_sAdSname.ReadOnly = false;
            ddwn_sAdGender.Enabled = true;
            txt_sAdAge.ReadOnly = false;
            ddwn_sAdMonth.Enabled = true;
            ddwn_sAdDay.Enabled = true;
            ddwn_sAdYear.Enabled = true;
            txt_sAdCiti.ReadOnly = false;
            txt_sAdEmail.ReadOnly = false;

            txt_sAdID.ReadOnly = false;
            txt_sAdID.Text = "";
            txt_sAdFname.Text = "";
            txt_sAdMname.Text = "";
            txt_sAdSname.Text = "";
            ddwn_sAdGender.Text = "Male";
            txt_sAdAge.Text = "";
            ddwn_sAdMonth.SelectedIndex = 0;
            ddwn_sAdDay.SelectedIndex = 0;
            ddwn_sAdYear.SelectedIndex = 0;
            txt_sAdCiti.Text = "";
            txt_sAdEmail.Text = "";
            btn_sAdAdd.Text = "Add";

            ViewState["choiceeee"] = null;

            lbl_warning.Visible = false;
            btn_sAdOk3.Visible = false;
            btn_sAdCancel3.Visible = false;
        }

        protected void btn_sAdCancel3_Click(object sender, EventArgs e)
        {
            ViewState["choiceeee"] = null;
            lbl_warning.Visible = false;
            btn_sAdOk3.Visible = false;
            btn_sAdCancel3.Visible = false;
        }

        protected void grid_superAdmins_SelectedIndexChanged(object sender, EventArgs e)
        {
            ViewState["sAdminIDGettersss"] = grid_superAdmins.SelectedRow.Cells[1].Text;
            //conn.Open();
            string que = "Select * from SystemHPIUsersTBL where AdminID = " + ViewState["sAdminIDGettersss"];
            SqlCommand commssy = new SqlCommand(que, hpiconn);
            IDataReader rders;
            hpiconn.Open();
            rders = commssy.ExecuteReader();
            rders.Read();
            ViewState["iiids1"] = rders.GetString(0).ToString();
            ViewState["fffnames1"] = rders.GetString(1).ToString();
            ViewState["mmmnames1"] = rders.GetString(2).ToString();
            ViewState["sssnames1"] = rders.GetString(3).ToString();
            ViewState["gggenders1"] = rders.GetString(4).ToString();
            ViewState["aaages1"] = rders.GetInt32(5);
            ViewState["bbbdays1"] = rders.GetString(6).ToString();
            ViewState["cccitis1"] = rders.GetString(7).ToString();
            ViewState["eeemils1"] = rders.GetString(8).ToString();
            hpiconn.Close();

            txt_sAdID.Text = ViewState["iiids1"].ToString();
            txt_sAdFname.Text = ViewState["fffnames1"].ToString();
            txt_sAdMname.Text = ViewState["mmmnames1"].ToString();
            txt_sAdSname.Text = ViewState["sssnames1"].ToString();
            ddwn_sAdGender.Text = ViewState["gggenders1"].ToString();
            txt_sAdAge.Text = ViewState["aaages1"].ToString();

            //separeate and load bday
            string bday1 = ViewState["bbbdays1"].ToString();
            string[] names1 = bday1.Split(' '); // "1" means stop splitting after one space
            string month1 = names1[0];
            string day1 = names1[1];
            string year1 = names1[2];
            string de1 = day1.Replace(",", "");
            ddwn_sAdMonth.SelectedIndex = ddwn_sAdMonth.Items.IndexOf(ddwn_sAdMonth.Items.FindByText(month1));
            ddwn_sAdDay.SelectedIndex = ddwn_sAdDay.Items.IndexOf(ddwn_sAdDay.Items.FindByText(de1));
            ddwn_sAdYear.SelectedIndex = ddwn_sAdYear.Items.IndexOf(ddwn_sAdYear.Items.FindByText(year1));

            txt_sAdCiti.Text = ViewState["cccitis1"].ToString();
            txt_sAdEmail.Text = ViewState["eeemils1"].ToString();

            txt_sAdID.ReadOnly = true;
            txt_sAdFname.ReadOnly = true;
            txt_sAdMname.ReadOnly = true;
            txt_sAdSname.ReadOnly = true;
            ddwn_sAdGender.Enabled = false;
            txt_sAdAge.ReadOnly = true;
            ddwn_sAdMonth.Enabled = false;
            ddwn_sAdDay.Enabled = false;
            ddwn_sAdYear.Enabled = false;
            txt_sAdCiti.ReadOnly = true;
            txt_sAdEmail.ReadOnly = true;


            //btn_GenerateUserName.Text = UserIDGetter;
            //Response.Write(ViewState["EmployerIDGetter"]);
            if (btn_sAdAdd.Text == "Save")
            {
                txt_sAdID.Text = "";
                txt_sAdFname.Text = "";
                txt_sAdMname.Text = "";
                txt_sAdSname.Text = "";
                ddwn_sAdGender.Text = "Male";
                txt_sAdAge.Text = "";
                ddwn_sAdMonth.SelectedIndex = 0;
                ddwn_sAdDay.SelectedIndex = 0;
                ddwn_sAdYear.SelectedIndex = 0;
                txt_sAdCiti.Text = "";
                txt_sAdEmail.Text = "";
                btn_sAdAdd.Text = "Add";
                txt_sAdID.ReadOnly = false;
            }
        }

        protected void btn_sAdRemoveAcc_Click(object sender, EventArgs e)
        {
            ViewState["AdminIDUserRemoverrr"] = grid_superAdmins.SelectedRow.Cells[1].Text;
            SqlCommand commm = new SqlCommand("delete from SystemHPIUsersTBL where AdminID = " + ViewState["AdminIDUserRemoverrr"], hpiconn);
            // commm.Parameters.AddWithValue("@reff", ViewState["AdminIDUserRemover"]);
            hpiconn.Open();
            commm.ExecuteNonQuery();
            hpiconn.Close();

            grid_superAdmins.DataBind();
        }

        protected void btn_sAdEdit_Click(object sender, EventArgs e)
        {

        }







        protected void btn_sAdChangePass_Click(object sender, EventArgs e)
        {

        }

        protected void btn_superAdminPassCancel_Click(object sender, EventArgs e)
        {

        }

        protected void btn_superAdminPassSave_Click(object sender, EventArgs e)
        {

        }

        protected void btn_SysAds_Click(object sender, EventArgs e)
        {
            MultiView1.ActiveViewIndex = 0;
        }

        protected void btn_ActLog_Click(object sender, EventArgs e)
        {
            MultiView1.ActiveViewIndex = 1;
        }

        protected void btn_SOut_Click(object sender, EventArgs e)
        {
            string nulls = "SELECT Number FROM SystemTimeRecordTBL WHERE LoggedOut IS NULL";
            hpiconn.Open();
            SqlCommand commd = new SqlCommand(nulls, hpiconn);
            IDataReader rad;
            rad = commd.ExecuteReader();
            rad.Read();
            int timeNo  = rad.GetInt32(0);
            hpiconn.Close();


            string time = DateTime.Now.ToString();
            hpiconn.Open();
            string que = "Update SystemTimeRecordTBL Set LoggedOut = '" + time + "' where Number = '"+timeNo+"'";
            SqlCommand comm = new SqlCommand(que, hpiconn);
            comm.ExecuteNonQuery();
            hpiconn.Close();
            Response.Redirect("frm_UserLogIn.aspx");
        }

        protected void btn_ChangeEmail_Click(object sender, EventArgs e)
        {
            lbl_RecoveryEmail.Visible = true;
            txt_newEmail.Visible = true;
            btn_SaveEmail.Visible = true;
            btn_CancelEmail.Visible = true;
        }

        protected void btn_SaveEmail_Click(object sender, EventArgs e)
        {
            if (txt_newEmail.Text == "" || txt_newEmail.Text == null)
            {

            }
            else
            {
                hpiconn.Open();
                string q1 = "UPDATE SystemDefaultAccountTBL SET RecoveryEmail = '" + txt_newEmail.Text + "'";
                SqlCommand comm = new SqlCommand(q1, hpiconn);

                comm.ExecuteNonQuery();

                hpiconn.Close();


                hpiconn.Open();
                string deafult = "Select  RecoveryEmail from  SystemDefaultAccountTBL";
                SqlCommand amdns = new SqlCommand(deafult, hpiconn);
                IDataReader ree;
                ree = amdns.ExecuteReader();
                ree.Read();
                lbl_RecEmail.Text = ree.GetString(0).ToString();
                hpiconn.Close();


                txt_newEmail.Text = "";
                lbl_RecoveryEmail.Visible = !true;
                txt_newEmail.Visible = !true;
                btn_SaveEmail.Visible = !true;
                btn_CancelEmail.Visible = !true;
            }
        }

        protected void btn_Save_Click(object sender, EventArgs e)
        {
            if (txt_CurrentPass.Text == null || txt_newPass.Text == null || txt_retypePass.Text == null || txt_CurrentPass.Text == "" || txt_newPass.Text == "" || txt_retypePass.Text == "")
            {
                Response.Write("Complete the sentence!");
            }
            else
            {
                if (txt_CurrentPass.Text == ViewState["pass"].ToString())
                {
                    if (txt_newPass.Text == txt_retypePass.Text)
                    {
                        try
                        {
                            string q1 = "UPDATE SystemDefaultAccountTBL SET DefaultPassword = '" + txt_newPass.Text + "'";
                            SqlCommand comm = new SqlCommand(q1, hpiconn);
                            hpiconn.Open();
                            comm.ExecuteNonQuery();
                            hpiconn.Close();

                            hpiconn.Open();
                            string qry3 = "Select DefaultPassword from SystemDefaultAccountTBL";
                            SqlCommand commas = new SqlCommand(qry3, hpiconn);
                            IDataReader rder;
                            rder = commas.ExecuteReader();
                            rder.Read();
                            string pssword = rder.GetString(0);
                            hpiconn.Close();

                            txt_defPass.Attributes.Add("value", pssword);
                        }
                        catch (Exception ex)
                        {
                            Response.Write(ex.Message);
                        }
                        lbl_CurrentPass.Visible = false;
                        lbl_NewPassword.Visible = false;
                        lbl_retypePassword.Visible = false;
                        txt_CurrentPass.Visible = false;
                        txt_newPass.Visible = false;
                        txt_retypePass.Visible = false;
                        btn_Save.Visible = false;
                        btn_CancelPass.Visible = false;

                    }
                    else if (txt_newPass.Text != txt_retypePass.Text)
                    {
                        Response.Write("Comfirmation not valid");
                    }
                }
                else if (txt_CurrentPass.Text != txt_defPass.Text)
                {
                    Response.Write("Current Password mismatch!");
                    // Response.Write(txt_Password.Text +" "+txt_CurrentPass.Text);
                }

            }
        }

        protected void btn_CancelPass_Click(object sender, EventArgs e)
        {
            lbl_CurrentPass.Visible = false;
            lbl_NewPassword.Visible = false;
            lbl_retypePassword.Visible = false;
            txt_CurrentPass.Visible = false;
            txt_newPass.Visible = false;
            txt_retypePass.Visible = false;

            btn_Save.Visible = false;
            btn_CancelPass.Visible = false;
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            lbl_CurrentPass.Visible = true;
            lbl_NewPassword.Visible = true;
            lbl_retypePassword.Visible = true;
            txt_CurrentPass.Visible = true;
            txt_newPass.Visible = true;
            txt_retypePass.Visible = true;

            btn_Save.Visible = true;
            btn_CancelPass.Visible = true;
        }

        protected void btn_Setting_Click(object sender, EventArgs e)
        {
            MultiView1.ActiveViewIndex = 2;
        }

        protected void btn_CancelEmail_Click(object sender, EventArgs e)
        {
            txt_newEmail.Text = "";
            lbl_RecoveryEmail.Visible = !true;
            txt_newEmail.Visible = !true;
            btn_SaveEmail.Visible = !true;
            btn_CancelEmail.Visible = !true;
        }
    }
}