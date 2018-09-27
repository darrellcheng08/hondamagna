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
    public partial class frm_HondaPhilsInc : System.Web.UI.Page
    {
        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["MagnaAdministratorConnectionString"].ConnectionString);
        SqlConnection hpiconn = new SqlConnection(ConfigurationManager.ConnectionStrings["HPILogInConnectionString"].ConnectionString);
        SqlConnection recsconn = new SqlConnection(ConfigurationManager.ConnectionStrings["HPIPORecordConnectionString"].ConnectionString);
        SqlConnection magnaconn = new SqlConnection(ConfigurationManager.ConnectionStrings["MagnacycleConnectionString"].ConnectionString);
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int x = Convert.ToInt32(DateTime.Now.Year.ToString());
                for (int a = x; a >= 1905; a--)
                {
                    ddwn_AdminYear.Items.Add(new ListItem(a.ToString()));
                    ddwn_sAdYear.Items.Add(new ListItem(a.ToString()));
                }
             
            }
            txt_sAdUserName.Text = Request.QueryString["superAdmin"];
            if (ddwn_Category.SelectedItem.Text == "Units")
            {
                grid_Sent.DataSourceID = "src_poUnitReceive";
                grid_Sent.DataBind();
            }
            else if (ddwn_Category.SelectedItem.Text == "Spare Parts")
            {
                grid_Sent.DataSourceID = "src_poPartReceive";
                grid_Sent.DataBind();
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

            if (ddwn_Categories.SelectedItem.Text == "Units")
            {
                recsconn.Open();
                string ass2 = "select Purchaseorder as 'P.O. Number',DateReceived as 'Date Received',Status from systemPurchaseorderstbl where category = 'unit'";
                //Response.Write(ass);
                SqlDataAdapter adapter2 = new SqlDataAdapter(ass2, recsconn);

                DataSet set2 = new DataSet();
                adapter2.Fill(set2);

                //grid_PurchaseOrderDetails1.DataSource = null;

                grid_Reports.DataSource = set2.Tables[0];
                grid_Reports.DataBind();
                recsconn.Close();
            }
            else if (ddwn_Categories.SelectedItem.Text == "Spare Parts")
            {
                recsconn.Open();
                string ass2 = "select Purchaseorder as 'P.O. Number',DateReceived as 'Date Received',Status from systemPurchaseorderstbl where category = 'part'";
                //Response.Write(ass);
                SqlDataAdapter adapter2 = new SqlDataAdapter(ass2, recsconn);

                DataSet set2 = new DataSet();
                adapter2.Fill(set2);

                //grid_PurchaseOrderDetails1.DataSource = null;

                grid_Reports.DataSource = set2.Tables[0];
                grid_Reports.DataBind();
                recsconn.Close();
            }
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            MultiView1.ActiveViewIndex = 1;
            MultiView2.ActiveViewIndex = 0;
        }

        protected void Button5_Click(object sender, EventArgs e)
        {
            MultiView3.ActiveViewIndex = 0;

            ddwn_UnitColor1.Items.Clear();
            string query="select Color from SystemModleColorsTBL";

            SqlCommand cmd = new SqlCommand(query, hpiconn);
            hpiconn.Open();
            SqlDataReader dr;
            dr = cmd.ExecuteReader();

            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    ddwn_UnitColor1.Items.Add(dr[0].ToString());
                    //ddwn_UnitColor2.Items.Add(dr[0].ToString());
                }
            }
            hpiconn.Close();
            //ddwn_UnitColor1.Items
        }

        protected void Button6_Click(object sender, EventArgs e)
        {
            MultiView3.ActiveViewIndex = 1;

            ddwn_pColor.Items.Clear();

            string query = "select Color from SystemPartColorsTBL";

            SqlCommand cmd = new SqlCommand(query, hpiconn);
            hpiconn.Open();
            SqlDataReader dr;
            dr = cmd.ExecuteReader();

            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    ddwn_pColor.Items.Add(dr[0].ToString());
                    //ddwn_UnitColor2.Items.Add(dr[0].ToString());
                }
            }
            hpiconn.Close();
            //ddwn_UnitColor1.Items
        }

        protected void Button7_Click(object sender, EventArgs e)
        {
            MultiView3.ActiveViewIndex = 2;
            try
            {
                hpiconn.Open();
                string contactgetter = "Select PLDT, Landline, Mobile from SystemContactsTBL";
                SqlCommand comand = new SqlCommand(contactgetter, hpiconn);
                IDataReader rad;
                rad = comand.ExecuteReader();
                rad.Read();
                txt_PLDT.Text = rad.GetString(0);
                txt_Landline.Text = rad.GetString(1);
                txt_Mobile.Text = rad.GetString(2);
                hpiconn.Close();
            }
            catch
            {
                if (hpiconn.State == ConnectionState.Open)
                    hpiconn.Close();
            }

            hpiconn.Open();
            string chargegetter = "Select UnitsCharge, PartsCharge from SystemChargesTBL";
            SqlCommand comands = new SqlCommand(chargegetter, hpiconn);
            IDataReader rads;
            rads = comands.ExecuteReader();
            rads.Read();
            txt_UnitCharge.Text = rads.GetString(0);
            txt_PartCharge.Text = rads.GetString(1);
            hpiconn.Close();

            hpiconn.Open();
            string discountgetter = "Select Percentage, DiscountQuantity from SystemDiscountsTBL";
            SqlCommand comander = new SqlCommand(discountgetter, hpiconn);
            IDataReader rader;
            rader = comander.ExecuteReader();

            rader.Read();
            txt_PartPercentage.Text = rader.GetString(0)+"%";
            txt_NumberParts.Text = rader.GetString(1);

            rader.Read();
            txt_UnitPercentage.Text = rader.GetString(0) + "%";
            txt_NumberUnits.Text = rader.GetString(1);
            hpiconn.Close();

            hpiconn.Open();
            string vatgetter = "Select vat from SystemChargesTBL";
            SqlCommand comanders = new SqlCommand(vatgetter, hpiconn);
            IDataReader raders;
            raders = comanders.ExecuteReader();

            raders.Read();
            txt_vat.Text = raders.GetString(0) + "%";
            hpiconn.Close();
        }

        protected void Button24_Click(object sender, EventArgs e)
        {
            MultiView3.ActiveViewIndex = 3;
            //must be modulized
            // ang code na ito ay para sa save ng password ng HPI ,, dapat to ilipat sa pag momodulized ng design

            string rref = txt_sAdUserName.Text;

            hpiconn.Open();
            string pp = "Select Password from SystemHPIUsersTBL where UserName = '" + rref + "'";
            SqlCommand commsi = new SqlCommand(pp, hpiconn);
            IDataReader rs;
            rs = commsi.ExecuteReader();
            rs.Read();
            string pword = rs.GetString(0);
            hpiconn.Close();


            hpiconn.Open();
            string q3 = "Select * from SystemHPIUsersTBl where UserName = '" + rref + "'";
            SqlCommand comms = new SqlCommand(q3, hpiconn);
            IDataReader r;
            r = comms.ExecuteReader();
            r.Read();
            ViewState["idno"] = r.GetString(0);//first row
            string fname = r.GetString(1);
            string sname = r.GetString(3);
            hpiconn.Close();

            txt_sAdAdminsID.Text = ViewState["idno"].ToString();
            txt_sAdFullName.Text = sname + ", " + fname;
            txt_sAdAdminPassword.Attributes.Add("value", pword);

            ViewState["pass"] = pword;
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            MultiView1.ActiveViewIndex = 0;
        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            Response.Redirect("frm_UserLogIn.aspx");
        }

        protected void btn_AdminClear_Click(object sender, EventArgs e)
        {
            txt_AdminID.ReadOnly = false;
            txt_AdminFname.ReadOnly = false;
            txt_AdminMname.ReadOnly = false;
            txt_AdminSurname.ReadOnly = false;
            ddwn_AdminGender.Enabled = true;
            txt_AdminAge.ReadOnly = false;
            ddwn_AdminMonth.Enabled = true;
            ddwn_AdminDay.Enabled = true;
            ddwn_AdminYear.Enabled = true;
            txt_AdminCiti.ReadOnly = false;
            txt_AdminEmail.ReadOnly = false;

            txt_AdminID.ReadOnly = false;
            txt_AdminID.Text = "";
            txt_AdminFname.Text = "";
            txt_AdminMname.Text = "";
            txt_AdminSurname.Text = "";
            ddwn_AdminGender.Text = "Male";
            txt_AdminAge.Text = "";
            ddwn_AdminMonth.SelectedIndex = 0;
            ddwn_AdminDay.SelectedIndex = 0;
            ddwn_AdminYear.SelectedIndex = 0;
            txt_AdminCiti.Text = "";
            txt_AdminEmail.Text = "";

            btn_AdminAddSave.Text = "Add";
        }

        protected void btn_AdminAddSave_Click(object sender, EventArgs e)
        {
            if (btn_AdminAddSave.Text == "Add")
            {
                if (grid_Admins.Rows.Count == 2)
                {
                    Response.Write("Max");
                }
                else
                {
                    int counters = grid_Admins.Rows.Count;
                    for (int c = 0; c <= counters - 1; c++)
                    {
                        ViewState["idGet"] = grid_Admins.Rows[c].Cells[1].Text;
                        ViewState["fnameGet"] = grid_Admins.Rows[c].Cells[2].Text;
                        ViewState["mnameGet"] = grid_Admins.Rows[c].Cells[3].Text;
                        ViewState["snameGet"] = grid_Admins.Rows[c].Cells[4].Text;
                        ViewState["genderGet"] = grid_Admins.Rows[c].Cells[5].Text;
                        ViewState["ageGet"] = grid_Admins.Rows[c].Cells[6].Text;
                        ViewState["bdayGet"] = grid_Admins.Rows[c].Cells[7].Text;
                        ViewState["citizenshipGet"] = grid_Admins.Rows[c].Cells[8].Text;
                        //ViewState["emailGetter"] = grid_Employers.Rows[c].Cells[9].Text;


                        if (ViewState["idGet"].ToString() == txt_AdminID.Text)
                        {
                            lbl_IDExist.Text = "This employer ID already exist." + "<br />" + "Generate new ID for the new employer you want to add?";
                            lbl_IDExist.Visible = true;
                            btn_AdminOK.Visible = true;
                            btn_IDCancel.Visible = true;
                            break;
                        }

                        //loop ulit
                    }
                    if (lbl_IDExist.Visible == true)
                    {

                    }
                    else if (lbl_IDExist.Visible == false)
                    {
                        ViewState["choicee"] = null;
                        for (int c = 0; c <= counters - 1; c++)
                        {
                            ViewState["idGettah"] = grid_Admins.Rows[c].Cells[1].Text;
                            ViewState["fnameGet"] = grid_Admins.Rows[c].Cells[2].Text;
                            ViewState["mnameGet"] = grid_Admins.Rows[c].Cells[3].Text;
                            ViewState["snameGet"] = grid_Admins.Rows[c].Cells[4].Text;
                            ViewState["genderGet"] = grid_Admins.Rows[c].Cells[5].Text;
                            ViewState["ageGet"] = grid_Admins.Rows[c].Cells[6].Text;
                            ViewState["bdayGet"] = grid_Admins.Rows[c].Cells[7].Text;
                            ViewState["citizenshipGet"] = grid_Admins.Rows[c].Cells[8].Text;

                            ViewState["bdayTesterr"] = ddwn_AdminMonth.SelectedItem.Text + " " + ddwn_AdminDay.SelectedItem.Text + ", " + ddwn_AdminYear.SelectedItem.Text;

                            if (ViewState["fnameGet"].ToString() == txt_AdminFname.Text && ViewState["mnameGet"].ToString() == txt_AdminMname.Text && ViewState["snameGet"].ToString() == txt_AdminSurname.Text && ViewState["genderGet"].ToString() == ddwn_AdminGender.Text && ViewState["ageGet"].ToString() == txt_AdminAge.Text && ViewState["bdayGet"].ToString() == ViewState["bdayTesterr"].ToString() && ViewState["citizenshipGet"].ToString() == txt_AdminCiti.Text)
                            {
                                lbl_IDExist.Text = "This employer has same details with " + ViewState["idGettah"].ToString() + "<br />" + "Do you want to add this person anyway?";
                                lbl_IDExist.Visible = true;
                                btn_AdminOkAdder.Visible = true;
                                btn_AdminCancelAdder.Visible = true;
                                break;
                            }
                        }
                        if (lbl_IDExist.Visible == true)
                        {

                        }
                        else if (lbl_IDExist.Visible == false)
                        {
                            AdminAdder();
                            txt_AdminID.ReadOnly = false;
                            txt_AdminFname.ReadOnly = false;
                            txt_AdminMname.ReadOnly = false;
                            txt_AdminSurname.ReadOnly = false;
                            ddwn_AdminGender.Enabled = true;
                            txt_AdminAge.ReadOnly = false;
                            ddwn_AdminMonth.Enabled = true;
                            ddwn_AdminDay.Enabled = true;
                            ddwn_AdminYear.Enabled = true;
                            txt_AdminCiti.ReadOnly = false;
                            txt_AdminEmail.ReadOnly = false;

                            txt_AdminID.ReadOnly = false;
                            txt_AdminID.Text = "";
                            txt_AdminFname.Text = "";
                            txt_AdminMname.Text = "";
                            txt_AdminSurname.Text = "";
                            ddwn_AdminGender.Text = "Male";
                            txt_AdminAge.Text = "";
                            ddwn_AdminMonth.SelectedIndex = 0;
                            ddwn_AdminDay.SelectedIndex = 0;
                            ddwn_AdminYear.SelectedIndex = 0;
                            txt_AdminCiti.Text = "";
                            txt_AdminEmail.Text = "";
                            btn_AdminAddSave.Text = "Add";
                        }
                    }
                }



            }
            else if (btn_AdminAddSave.Text == "Save")
            {
                ViewState["months"] = ddwn_AdminMonth.SelectedItem.Value;
                ViewState["days"] = ddwn_AdminDay.SelectedItem.Value;
                int counters = grid_Admins.Rows.Count;
                for (int w = 0; w <= counters - 1; w++)
                {
                    ViewState["idD"] = grid_Admins.Rows[w].Cells[1].Text;

                    if (ViewState["idD"].ToString() == ViewState["editRefas"].ToString())
                    {
                        ViewState["fGetters"] = grid_Admins.Rows[w].Cells[2].Text;///////////////////////////////////////////////
                        ViewState["mGetters"] = grid_Admins.Rows[w].Cells[3].Text;
                        ViewState["sGetters"] = grid_Admins.Rows[w].Cells[4].Text;
                        ViewState["gGetters"] = grid_Admins.Rows[w].Cells[5].Text;
                        ViewState["aGetters"] = grid_Admins.Rows[w].Cells[6].Text;
                        ViewState["bGetters"] = grid_Admins.Rows[w].Cells[7].Text;
                        ViewState["cGetters"] = grid_Admins.Rows[w].Cells[8].Text;
                        //ViewState["emailGetter"] = grid_Employers.Rows[c].Cells[9].Text;

                        ViewState["bDayTesterss"] = ddwn_AdminMonth.SelectedItem.Text + " " + ddwn_AdminDay.SelectedItem.Text + ", " + ddwn_AdminYear.SelectedItem.Text;

                        if (txt_AdminID.Text == ViewState["editRefas"].ToString())
                        {
                            for (int c = 0; c <= counters - 1; c++)
                            {
                                ViewState["idGettera"] = grid_Admins.Rows[c].Cells[1].Text;
                                ViewState["fGetters"] = grid_Admins.Rows[c].Cells[2].Text;
                                ViewState["mGetters"] = grid_Admins.Rows[c].Cells[3].Text;
                                ViewState["sGetters"] = grid_Admins.Rows[c].Cells[4].Text;
                                ViewState["gGetters"] = grid_Admins.Rows[c].Cells[5].Text;
                                ViewState["aGetters"] = grid_Admins.Rows[c].Cells[6].Text;
                                ViewState["bGetters"] = grid_Admins.Rows[c].Cells[7].Text;
                                ViewState["cGetters"] = grid_Admins.Rows[c].Cells[8].Text;

                                //ViewState["bdayTester"] = ddwn_Month.SelectedItem.Text + " " + ddwn_Day.SelectedItem.Text + ", " + ddwn_Year.SelectedItem.Text;

                                if (ViewState["fGetters"].ToString() == txt_AdminFname.Text && ViewState["mGetters"].ToString() == txt_AdminMname.Text && ViewState["sGetters"].ToString() == txt_AdminSurname.Text && ViewState["gGetters"].ToString() == ddwn_AdminGender.Text && ViewState["aGetters"].ToString() == txt_AdminAge.Text && ViewState["bGetters"].ToString() == ViewState["bDayTesterss"].ToString() && ViewState["cGetters"].ToString() == txt_AdminCiti.Text)
                                {
                                    lbl_IDExist.Text = "This employer has same details with " + ViewState["idGettera"].ToString() + "<br />" + "Do you want to update this person anyway?";
                                    lbl_IDExist.Visible = true;
                                    btn_AdminSaveOk.Visible = true;
                                    btn_AdminSaveCancel.Visible = true;
                                    break;
                                }
                            }
                            if (lbl_IDExist.Visible == true)
                            {

                            }
                            else if (lbl_IDExist.Visible == false)
                            {
                                systemAdminUpdater();
                                txt_AdminID.ReadOnly = false;
                                txt_AdminFname.ReadOnly = false;
                                txt_AdminMname.ReadOnly = false;
                                txt_AdminSurname.ReadOnly = false;
                                ddwn_AdminGender.Enabled = true;
                                txt_AdminAge.ReadOnly = false;
                                ddwn_AdminMonth.Enabled = true;
                                ddwn_AdminDay.Enabled = true;
                                ddwn_AdminYear.Enabled = true;
                                txt_AdminCiti.ReadOnly = false;
                                txt_AdminEmail.ReadOnly = false;

                                txt_AdminID.ReadOnly = false;
                                txt_AdminID.Text = "";
                                txt_AdminFname.Text = "";
                                txt_AdminMname.Text = "";
                                txt_AdminSurname.Text = "";
                                ddwn_AdminGender.Text = "Male";
                                txt_AdminAge.Text = "";
                                ddwn_AdminMonth.SelectedIndex = 0;
                                ddwn_AdminDay.SelectedIndex = 0;
                                ddwn_AdminYear.SelectedIndex = 0;
                                txt_AdminCiti.Text = "";
                                txt_AdminEmail.Text = "";
                                btn_AdminAddSave.Text = "Add";
                            }
                        }
                        else
                        {
                            //for checking of errors
                            Response.Write("txt_EmployerIDNumber.Text != ViewState['editRef'].ToString()");
                            Response.Write(txt_AdminID.Text + " " + ViewState["editRefs"].ToString());
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

        protected void btn_AdminSaveOk_Click(object sender, EventArgs e)
        {
            systemAdminUpdater();

            txt_AdminID.ReadOnly = false;
            txt_AdminFname.ReadOnly = false;
            txt_AdminMname.ReadOnly = false;
            txt_AdminSurname.ReadOnly = false;
            ddwn_AdminGender.Enabled = true;
            txt_AdminAge.ReadOnly = false;
            ddwn_AdminMonth.Enabled = true;
            ddwn_AdminDay.Enabled = true;
            ddwn_AdminYear.Enabled = true;
            txt_AdminCiti.ReadOnly = false;
            txt_AdminEmail.ReadOnly = false;

            txt_AdminID.ReadOnly = false;
            txt_AdminID.Text = "";
            txt_AdminFname.Text = "";
            txt_AdminMname.Text = "";
            txt_AdminSurname.Text = "";
            ddwn_AdminGender.Text = "Male";
            txt_AdminAge.Text = "";
            ddwn_AdminMonth.SelectedIndex = 0;
            ddwn_AdminDay.SelectedIndex = 0;
            ddwn_AdminYear.SelectedIndex = 0;
            txt_AdminCiti.Text = "";
            txt_AdminEmail.Text = "";
            btn_AdminAddSave.Text = "Add";

            lbl_IDExist.Visible = false;
            btn_AdminSaveOk.Visible = false;
            btn_AdminSaveCancel.Visible = false;
        }

        protected void btn_AdminSaveCancel_Click(object sender, EventArgs e)
        {
            lbl_IDExist.Visible = false;
            btn_AdminSaveOk.Visible = false;
            btn_AdminSaveCancel.Visible = false;
        }

        protected void btn_AdminOkAdder_Click(object sender, EventArgs e)
        {
            ViewState["choice1s"] = "ok";
            AdminAdder();

            txt_AdminID.ReadOnly = false;
            txt_AdminFname.ReadOnly = false;
            txt_AdminMname.ReadOnly = false;
            txt_AdminSurname.ReadOnly = false;
            ddwn_AdminGender.Enabled = true;
            txt_AdminAge.ReadOnly = false;
            ddwn_AdminMonth.Enabled = true;
            ddwn_AdminDay.Enabled = true;
            ddwn_AdminYear.Enabled = true;
            txt_AdminCiti.ReadOnly = false;
            txt_AdminEmail.ReadOnly = false;

            txt_AdminID.ReadOnly = false;
            txt_AdminID.Text = "";
            txt_AdminFname.Text = "";
            txt_AdminMname.Text = "";
            txt_AdminSurname.Text = "";
            ddwn_AdminGender.Text = "Male";
            txt_AdminAge.Text = "";
            ddwn_AdminMonth.SelectedIndex = 0;
            ddwn_AdminDay.SelectedIndex = 0;
            ddwn_AdminYear.SelectedIndex = 0;
            txt_AdminCiti.Text = "";
            txt_AdminEmail.Text = "";
            btn_AdminAddSave.Text = "Add";

            ViewState["choice1s"] = null;
            lbl_IDExist.Visible = false;
            btn_AdminOkAdder.Visible = false;
            btn_AdminCancelAdder.Visible = false;
        }

        protected void btn_AdminCancelAdder_Click(object sender, EventArgs e)
        {
            ViewState["choice1s"] = null;
            lbl_IDExist.Visible = false;
            btn_AdminOkAdder.Visible = false;
            btn_AdminCancelAdder.Visible = false;
        }

        protected void btn_AdminOK_Click(object sender, EventArgs e)
        {
            ViewState["choicee"] = "ok";
            Random rnd = new Random();
            ViewState["eidd"] = rnd.Next(100000, 999999);

            AdminAdder();

            txt_AdminID.ReadOnly = false;
            txt_AdminFname.ReadOnly = false;
            txt_AdminMname.ReadOnly = false;
            txt_AdminSurname.ReadOnly = false;
            ddwn_AdminGender.Enabled = true;
            txt_AdminAge.ReadOnly = false;
            ddwn_AdminMonth.Enabled = true;
            ddwn_AdminDay.Enabled = true;
            ddwn_AdminYear.Enabled = true;
            txt_AdminCiti.ReadOnly = false;
            txt_AdminEmail.ReadOnly = false;

            txt_AdminID.ReadOnly = false;
            txt_AdminID.Text = "";
            txt_AdminFname.Text = "";
            txt_AdminMname.Text = "";
            txt_AdminSurname.Text = "";
            ddwn_AdminGender.Text = "Male";
            txt_AdminAge.Text = "";
            ddwn_AdminMonth.SelectedIndex = 0;
            ddwn_AdminDay.SelectedIndex = 0;
            ddwn_AdminYear.SelectedIndex = 0;
            txt_AdminCiti.Text = "";
            txt_AdminEmail.Text = "";
            btn_AdminAddSave.Text = "Add";

            ViewState["choicee"] = null;

            lbl_IDExist.Visible = false;
            btn_AdminOK.Visible = false;
            btn_IDCancel.Visible = false;
        }

        protected void btn_IDCancel_Click(object sender, EventArgs e)
        {
            ViewState["choicee"] = null;
            lbl_IDExist.Visible = false;
            btn_AdminOK.Visible = false;
            btn_IDCancel.Visible = false;
        }

        protected void grid_Admins_SelectedIndexChanged(object sender, EventArgs e)
        {
            ViewState["AdminIDGetters"] = grid_Admins.SelectedRow.Cells[1].Text;
            //conn.Open();
            string que = "Select * from SystemAdminsTBL where AdminIDNumber = " + ViewState["AdminIDGetters"];
            SqlCommand commssy = new SqlCommand(que, conn);
            IDataReader rders;
            conn.Open();
            rders = commssy.ExecuteReader();
            rders.Read();
            ViewState["ids1"] = rders.GetString(0).ToString();
            ViewState["fnames1"] = rders.GetString(1).ToString();
            ViewState["mnames1"] = rders.GetString(2).ToString();
            ViewState["snames1"] = rders.GetString(3).ToString();
            ViewState["genders1"] = rders.GetString(4).ToString();
            ViewState["ages1"] = rders.GetInt32(5);
            ViewState["bdays1"] = rders.GetString(6).ToString();
            ViewState["citis1"] = rders.GetString(7).ToString();
            ViewState["emils1"] = rders.GetString(8).ToString();
            conn.Close();

            txt_AdminID.Text = ViewState["ids1"].ToString();
            txt_AdminFname.Text = ViewState["fnames1"].ToString();
            txt_AdminMname.Text = ViewState["mnames1"].ToString();
            txt_AdminSurname.Text = ViewState["snames1"].ToString();
            ddwn_AdminGender.Text = ViewState["genders1"].ToString();
            txt_AdminAge.Text = ViewState["ages1"].ToString();

            //separeate and load bday
            string bday1 = ViewState["bdays1"].ToString();
            string[] names1 = bday1.Split(' '); // "1" means stop splitting after one space
            string month1 = names1[0];
            string day1 = names1[1];
            string year1 = names1[2];
            string de1 = day1.Replace(",", "");
            ddwn_AdminMonth.SelectedIndex = ddwn_AdminMonth.Items.IndexOf(ddwn_AdminMonth.Items.FindByText(month1));
            ddwn_AdminDay.SelectedIndex = ddwn_AdminDay.Items.IndexOf(ddwn_AdminDay.Items.FindByText(de1));
            ddwn_AdminYear.SelectedIndex = ddwn_AdminYear.Items.IndexOf(ddwn_AdminYear.Items.FindByText(year1));

            txt_AdminCiti.Text = ViewState["citis1"].ToString();
            txt_AdminEmail.Text = ViewState["emils1"].ToString();

            txt_AdminID.ReadOnly = true;
            txt_AdminFname.ReadOnly = true;
            txt_AdminMname.ReadOnly = true;
            txt_AdminSurname.ReadOnly = true;
            ddwn_AdminGender.Enabled = false;
            txt_AdminAge.ReadOnly = true;
            ddwn_AdminMonth.Enabled = false;
            ddwn_AdminDay.Enabled = false;
            ddwn_AdminYear.Enabled = false;
            txt_AdminCiti.ReadOnly = true;
            txt_AdminEmail.ReadOnly = true;


            //btn_GenerateUserName.Text = UserIDGetter;
            //Response.Write(ViewState["EmployerIDGetter"]);
            if (btn_AdminAddSave.Text == "Save")
            {
                txt_AdminID.Text = "";
                txt_AdminFname.Text = "";
                txt_AdminMname.Text = "";
                txt_AdminSurname.Text = "";
                ddwn_AdminGender.Text = "Male";
                txt_AdminAge.Text = "";
                ddwn_AdminMonth.SelectedIndex = 0;
                ddwn_AdminDay.SelectedIndex = 0;
                ddwn_AdminYear.SelectedIndex = 0;
                txt_AdminCiti.Text = "";
                txt_AdminEmail.Text = "";
                btn_AdminAddSave.Text = "Add";
                txt_AdminID.ReadOnly = false;
            }
            
        }

        protected void btn_AdminRemoveAccess_Click(object sender, EventArgs e)
        {
            ViewState["AdminIDUserRemover"] = grid_Admins.SelectedRow.Cells[1].Text;
            SqlCommand commm = new SqlCommand("delete from SystemAdminsTBL where AdminIDNumber = " + ViewState["AdminIDUserRemover"], conn);
            // commm.Parameters.AddWithValue("@reff", ViewState["AdminIDUserRemover"]);
            conn.Open();
            commm.ExecuteNonQuery();
            conn.Close();

            grid_Admins.DataBind();
        }

        protected void btn_AdminEdit_Click(object sender, EventArgs e)
        {
            ViewState["editRefas"] = grid_Admins.SelectedRow.Cells[1].Text;
            string query = "Select * from SystemAdminsTBL where AdminIDNumber = " + ViewState["editRefas"];
            //SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["MagnaAdministratorConnectionString"].ConnectionString);
            SqlCommand com = new SqlCommand(query, conn);
            IDataReader r;
            conn.Open();
            r = com.ExecuteReader();
            r.Read();
            ViewState["idsa"] = r.GetString(0).ToString();
            ViewState["fnamesa"] = r.GetString(1).ToString();
            ViewState["mnamesa"] = r.GetString(2).ToString();
            ViewState["snamesa"] = r.GetString(3).ToString();
            ViewState["gendersa"] = r.GetString(4).ToString();
            ViewState["agesa"] = r.GetInt32(5);
            ViewState["bdaysa"] = r.GetString(6).ToString();
            ViewState["citisa"] = r.GetString(7).ToString();
            ViewState["emilsa"] = r.GetString(8).ToString();
            conn.Close();

            txt_AdminID.Text = ViewState["idsa"].ToString();
            txt_AdminFname.Text = ViewState["fnamesa"].ToString();
            txt_AdminMname.Text = ViewState["mnamesa"].ToString();
            txt_AdminSurname.Text = ViewState["snamesa"].ToString();
            ddwn_AdminGender.Text = ViewState["gendersa"].ToString();
            txt_AdminAge.Text = ViewState["agesa"].ToString();

            //separeate and load bday
            string bday = ViewState["bdaysa"].ToString();
            string[] names = bday.Split(' '); // "1" means stop splitting after one space
            string month = names[0];
            string day = names[1];
            string year = names[2];
            string de = day.Replace(",", "");
            ddwn_AdminMonth.SelectedIndex = ddwn_AdminMonth.Items.IndexOf(ddwn_AdminMonth.Items.FindByText(month));
            ddwn_AdminDay.SelectedIndex = ddwn_AdminDay.Items.IndexOf(ddwn_AdminDay.Items.FindByText(de));
            ddwn_AdminYear.SelectedIndex = ddwn_AdminYear.Items.IndexOf(ddwn_AdminYear.Items.FindByText(year));

            txt_AdminCiti.Text = ViewState["citisa"].ToString();
            txt_AdminEmail.Text = ViewState["emilsa"].ToString();

            txt_AdminID.ReadOnly = false;
            txt_AdminFname.ReadOnly = false;
            txt_AdminMname.ReadOnly = false;
            txt_AdminSurname.ReadOnly = false;
            ddwn_AdminGender.Enabled = true;
            txt_AdminAge.ReadOnly = false;
            ddwn_AdminMonth.Enabled = true;
            ddwn_AdminDay.Enabled = true;
            ddwn_AdminYear.Enabled = true;
            txt_AdminCiti.ReadOnly = false;
            txt_AdminEmail.ReadOnly = false;

            btn_AdminAddSave.Text = "Save";
            txt_AdminID.ReadOnly = true;
        } 
        private void AdminAdder()
        {
            conn.Open();
            //try
            //{
            string inserts = "insert into SystemAdminsTBL(AdminIDNumber,FirstName,MiddleName,Surname,Gender,Age,Birthday,Citizenship,Email,UserName,Password)values(@adIDNumber,@FName,@MName,@SName,@Gdr,@Ages,@Bday,@Cship,@Emil,@User,@Pass)";
            SqlCommand comm = new SqlCommand(inserts, conn);

            //string recieved = ViewState["AdminIDGetters"].ToString();
            //string querys = "Select AdminIDNumber,FirstName from SystemAdminsTBL where AdminIDNumber = " + recieved;
            //SqlCommand comma = new SqlCommand(querys, conn);
            //IDataReader rd;
            //conn.Open();
            //rd = comma.ExecuteReader();
            //rd.Read();
            //string adminidnumber = rd.GetString(0);//first row
            string adminfirstname = txt_AdminFname.Text;
            string fanames = adminfirstname;
            fanames = Regex.Replace(fanames, @"\s", "");
            string finalName = fanames;

            string ids = txt_AdminID.Text;
            //conn.Close();

            string retrieved = "";
            string vals = ids;
            char val1 = vals[vals.Length - 1];
            char val2 = vals[vals.Length - 2];
            char val3 = vals[vals.Length - 3];
            //if (val1 >= '0' && val1 <= '9')
            //{
            retrieved = val3.ToString() + val2.ToString() + val1.ToString();
            //}

            string generatedUN = finalName + retrieved;

            ViewState["admonth"] = ddwn_AdminMonth.SelectedItem.Value;
            ViewState["adday"] = ddwn_AdminDay.SelectedItem.Value;

            string faname = adminfirstname;
            //faname = Regex.Replace(faname, @"\s", "");
            //string generatedUname = faname + ViewState["admonth"].ToString() + ViewState["adday"].ToString() + "User";

            // string uuname = "";
            int s = grid_Admins.Rows.Count;
            bool boll = false;
            string uname = "";
            for (int p = 0; p <= s - 1; p++)
            {
                string un = grid_Admins.Rows[p].Cells[4].Text;
                if (generatedUN == un)
                {
                    Random rnd = new Random();
                    int id = rnd.Next(100, 999);

                    string finame = adminfirstname;
                    string faname1 = Regex.Replace(faname, @"\s", "");


                    string iids = ids;

                    string retrieveds = "";
                    string valss = iids;
                    char val1s = valss[valss.Length - 1];
                    char val2s = valss[valss.Length - 2];
                    char val3s = valss[valss.Length - 3];
                    //if (val1 >= '0' && val1 <= '9')
                    //{
                    retrieveds = val3s.ToString() + val2s.ToString() + val1s.ToString();
                    //}

                    uname = faname1 + retrieveds;

                    //uname = finame + ViewState["admonth"].ToString() + ViewState["adday"].ToString() + "User" + id.ToString();

                    boll = true;
                    break;
                }
            }
            if (boll == true)
                ViewState["uuname"] = uname;
            else if (boll == false)
                ViewState["uuname"] = generatedUN;

            if (ViewState["choicee"] == "ok")
            {
                string str = ViewState["eidd"].ToString();

                string retrievedsae = "";
                string valssa = str;
                char val1sae = valssa[valssa.Length - 1];
                char val2sae = valssa[valssa.Length - 2];
                char val3sae = valssa[valssa.Length - 3];
                //if (val1 >= '0' && val1 <= '9')
                //{
                retrievedsae = val3sae.ToString() + val2sae.ToString() + val1sae.ToString();
                //}

                string finame = adminfirstname;
                string faname1 = Regex.Replace(faname, @"\s", "");

                ViewState["uuname"] = faname1 + retrievedsae;

                comm.Parameters.AddWithValue("@adIDNumber", ViewState["eidd"].ToString());
                ViewState["choicee"] = null;
                //comm.Parameters.AddWithValue("@UName", txt_UserName.Text);
                //@adIDNumber,@FName,@MName,@SName,@Gdr,@Ages,@Bday,@Cship,@Emil,@User,@Pass
                comm.Parameters.AddWithValue("@FName", txt_AdminFname.Text);
                comm.Parameters.AddWithValue("@MName", txt_AdminMname.Text);
                comm.Parameters.AddWithValue("@SName", txt_AdminSurname.Text);
                comm.Parameters.AddWithValue("@Gdr", ddwn_AdminGender.Text);
                comm.Parameters.AddWithValue("@Ages", txt_AdminAge.Text);
                comm.Parameters.AddWithValue("@Bday", ddwn_AdminMonth.SelectedItem.Text + " " + ddwn_AdminDay.SelectedItem.Text + ", " + ddwn_AdminYear.SelectedItem.Text);
                comm.Parameters.AddWithValue("@Cship", txt_AdminCiti.Text);
                comm.Parameters.AddWithValue("@Emil", txt_AdminEmail.Text);
                comm.Parameters.AddWithValue("@User", ViewState["uuname"].ToString());//////////////////////////////
                comm.Parameters.AddWithValue("@Pass", "defaultPassword456");//////////////////////////////
                comm.ExecuteNonQuery();
                ViewState["months"] = ddwn_AdminMonth.SelectedItem.Value;
                ViewState["days"] = ddwn_AdminDay.SelectedItem.Value;
                Response.Write("Addedchoice");
                grid_Admins.DataBind();
            }
            else if (ViewState["choicee"] == null || ViewState["choice1s"] == "ok")
            {
                Response.Write(txt_AdminID.Text + txt_AdminFname.Text + txt_AdminMname.Text + txt_AdminSurname.Text);
                comm.Parameters.AddWithValue("@adIDNumber", txt_AdminID.Text);
                //comm.Parameters.AddWithValue("@UName", txt_UserName.Text);
                comm.Parameters.AddWithValue("@FName", txt_AdminFname.Text);
                comm.Parameters.AddWithValue("@MName", txt_AdminMname.Text);
                comm.Parameters.AddWithValue("@SName", txt_AdminSurname.Text);
                comm.Parameters.AddWithValue("@Gdr", ddwn_AdminGender.Text);
                comm.Parameters.AddWithValue("@Ages", txt_AdminAge.Text);
                comm.Parameters.AddWithValue("@Bday", ddwn_AdminMonth.SelectedItem.Text + " " + ddwn_AdminDay.SelectedItem.Text + ", " + ddwn_AdminYear.SelectedItem.Text);
                comm.Parameters.AddWithValue("@Cship", txt_AdminCiti.Text);
                comm.Parameters.AddWithValue("@Emil", txt_AdminEmail.Text);
                comm.Parameters.AddWithValue("@User", generatedUN);//////////////////////////////
                comm.Parameters.AddWithValue("@Pass", "defaultPassword456");//////////////////////////////
                comm.ExecuteNonQuery();
                ViewState["months"] = ddwn_AdminMonth.SelectedItem.Value;
                ViewState["days"] = ddwn_AdminDay.SelectedItem.Value;
                Response.Write("Addedchoice");
                grid_Admins.DataBind();
            }
            //}
            //catch (Exception ex)
            //{
            //    Response.Write(ex.Message);
            //}
            conn.Close();
        }
        private void systemAdminUpdater()
        {
            string ffname = txt_AdminFname.Text;
            string mmname = txt_AdminMname.Text;
            string ssname = txt_AdminSurname.Text;
            string ggender = ddwn_AdminGender.Text;
            string aage = txt_AdminAge.Text;

            string bdaysm = ddwn_AdminMonth.SelectedItem.Text;
            string bdaysd = ddwn_AdminDay.SelectedItem.Text;
            string bdaysy = ddwn_AdminYear.SelectedItem.Text;

            string Compbday = bdaysm + " " + bdaysd + ", " + bdaysy;

            string bdaysmV = ddwn_AdminMonth.SelectedItem.Value.ToString();
            string bdaysdV = ddwn_AdminDay.SelectedItem.Value.ToString();

            string cciti = txt_AdminCiti.Text;
            string eemil = txt_AdminEmail.Text;
            //generate uname
            string genfname = ffname;
            genfname = Regex.Replace(genfname, @"\s", "");

            string idi = txt_AdminID.Text;

            string retrieved = "";
            string valse = idi;
            char val1e = valse[valse.Length - 1];
            char val2e = valse[valse.Length - 2];
            char val3e = valse[valse.Length - 3];

            retrieved = val3e.ToString() + val2e.ToString() + val1e.ToString();


            string orgigeneratedUname = genfname + retrieved;

            //check for current users
            int counter = grid_Admins.Rows.Count;
            if (counter == 0)
            {
                Response.Write("Counter = 0");//for checking
            }
            else
            {
                for (int a = 0; a <= counter - 1; a++)
                {
                    ViewState["tester"] = grid_Admins.Rows[a].Cells[1].Text;//Response.Write(ViewState["tester"].ToString() + ViewState["editRef"].ToString());
                    if (ViewState["tester"].ToString() == ViewState["editRefas"].ToString())
                    {
                        conn.Open();
                        string q2 = "UPDATE SystemAdminsTBL SET FirstName = '" + ffname + "',MiddleName = '" + mmname + "',Surname = '" + ssname + "', Gender = '" + ggender + "', Age = '" + aage + "', Birthday = '" + Compbday + "' ,Citizenship = '" + cciti + "', Email = '" + eemil + "' where AdminIDNumber = " + ViewState["editRefas"].ToString();
                        SqlCommand finalcommss = new SqlCommand(q2, conn);
                        //conn.Open();
                        finalcommss.ExecuteNonQuery();
                        conn.Close();

                        conn.Open();
                        string q3 = "Select UserName from SystemAdminsTBl where AdminIDNumber = " + ViewState["tester"];
                        SqlCommand comms = new SqlCommand(q3, conn);
                        IDataReader r;
                        r = comms.ExecuteReader();
                        r.Read();
                        //string employeridno = r.GetString(0);//first row
                        string retrievedUname = r.GetString(0);
                        conn.Close();

                        //string retrieved = "";
                        //string vals = retrievedUname;
                        //char val1 = vals[vals.Length - 1];
                        //char val2 = vals[vals.Length - 2];
                        //char val3 = vals[vals.Length - 3];
                        //if (val1 >= '0' && val1 <= '9')
                        //{
                        //    retrieved = val3.ToString() + val2.ToString() + val1.ToString();
                        //}


                        conn.Open();
                        string qry = "UPDATE SystemAdminsTBL SET UserName = 'Removed' where AdminIDNumber = " + ViewState["tester"];
                        SqlCommand mand = new SqlCommand(qry, conn);
                        //conn.Open();
                        mand.ExecuteNonQuery();
                        conn.Close();

                        grid_Admins.DataBind();

                        int d = grid_Admins.Rows.Count;
                        string datas = "";
                        string newUname = "";
                        bool bol = false;
                        for (int x = 0; x <= d - 1; x++)
                        {
                            datas = grid_Admins.Rows[x].Cells[10].Text;
                            if (datas == orgigeneratedUname)
                            {
                                bol = true;
                                Random rnd = new Random();
                                int id = rnd.Next(100, 999);
                                string faname = ffname;
                                faname = Regex.Replace(faname, @"\s", "");
                                newUname = faname + bdaysmV + bdaysdV + "User" + id.ToString();

                                //update of username
                                conn.Open();
                                string q22 = "UPDATE SystemAdminsTBL SET UserName = '" + newUname + "' where AdminIDNumber = " + ViewState["tester"];
                                SqlCommand mande = new SqlCommand(q22, conn);
                                mande.ExecuteNonQuery();
                                conn.Close();

                                grid_Admins.DataBind();

                                break;
                            }
                        }
                        if (bol == false)
                        {
                            //  newUname = orgigeneratedUname;

                            //update of username
                            conn.Open();
                            string q22 = "UPDATE SystemAdminsTBL SET UserName = '" + orgigeneratedUname + "' where AdminIDNumber = " + ViewState["tester"];
                            SqlCommand mande = new SqlCommand(q22, conn);
                            mande.ExecuteNonQuery();
                            conn.Close();

                            grid_Admins.DataBind();
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
            ViewState["AdminIDGettersss"] = grid_superAdmins.SelectedRow.Cells[1].Text;
            //conn.Open();
            string que = "Select * from SystemHPIUsersTBL where AdminID = " + ViewState["AdminIDGettersss"];
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

            ViewState["editRefasss"] = grid_superAdmins.SelectedRow.Cells[1].Text;
            string query = "Select * from SystemHPIUsersTBL where AdminID = " + ViewState["editRefasss"];
            //SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["MagnaAdministratorConnectionString"].ConnectionString);
            SqlCommand com = new SqlCommand(query, hpiconn);
            IDataReader r;
            hpiconn.Open();
            r = com.ExecuteReader();
            r.Read();
            ViewState["iiidsa"] = r.GetString(0).ToString();
            ViewState["fffnamesa"] = r.GetString(1).ToString();
            ViewState["mmmnamesa"] = r.GetString(2).ToString();
            ViewState["sssnamesa"] = r.GetString(3).ToString();
            ViewState["gggendersa"] = r.GetString(4).ToString();
            ViewState["aaagesa"] = r.GetInt32(5);
            ViewState["bbbdaysa"] = r.GetString(6).ToString();
            ViewState["cccitisa"] = r.GetString(7).ToString();
            ViewState["eeemilsa"] = r.GetString(8).ToString();
            hpiconn.Close();

            txt_sAdID.Text = ViewState["iiidsa"].ToString();
            txt_sAdFname.Text = ViewState["fffnamesa"].ToString();
            txt_sAdMname.Text = ViewState["mmmnamesa"].ToString();
            txt_sAdSname.Text = ViewState["sssnamesa"].ToString();
            ddwn_sAdGender.Text = ViewState["gggendersa"].ToString();
            txt_sAdAge.Text = ViewState["aaagesa"].ToString();

            //separeate and load bday
            string bday = ViewState["bbbdaysa"].ToString();
            string[] names = bday.Split(' '); // "1" means stop splitting after one space
            string month = names[0];
            string day = names[1];
            string year = names[2];
            string de = day.Replace(",", "");
            ddwn_sAdMonth.SelectedIndex = ddwn_sAdMonth.Items.IndexOf(ddwn_sAdMonth.Items.FindByText(month));
            ddwn_sAdDay.SelectedIndex = ddwn_sAdDay.Items.IndexOf(ddwn_sAdDay.Items.FindByText(de));
            ddwn_sAdYear.SelectedIndex = ddwn_sAdYear.Items.IndexOf(ddwn_sAdYear.Items.FindByText(year));

            txt_sAdCiti.Text = ViewState["cccitisa"].ToString();
            txt_sAdEmail.Text = ViewState["eeemilsa"].ToString();

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

            btn_sAdAdd.Text = "Save";
            txt_sAdID.ReadOnly = true;
        }

        protected void btn_sAdChangePass_Click(object sender, EventArgs e)
        {
            lbl_superAdminCurrentPass.Visible = true;
            lbl_superAdminNewPass.Visible = true;
            lbl_superAdminRetypePass.Visible = true;
            txt_superAdminCurrentPass.Visible = true;
            txt_superAdminNewPassword.Visible = true;
            txt_superAdminConfirm.Visible = true;

            btn_superAdminPassSave.Visible = true;
            btn_superAdminPassCancel.Visible = true;
        }

        protected void btn_superAdminPassCancel_Click(object sender, EventArgs e)
        {
            lbl_superAdminCurrentPass.Visible = false;
            lbl_superAdminNewPass.Visible = false;
            lbl_superAdminRetypePass.Visible = false;
            txt_superAdminCurrentPass.Visible = false;
            txt_superAdminNewPassword.Visible = false;
            txt_superAdminConfirm.Visible = false;

            btn_superAdminPassSave.Visible = false;
            btn_superAdminPassCancel.Visible = false;
        }

        protected void btn_superAdminPassSave_Click(object sender, EventArgs e)
        {
            if (txt_superAdminCurrentPass.Text == null || txt_superAdminNewPassword.Text == null || txt_superAdminConfirm.Text == null)
            {
                Response.Write("Complete the sentence!");
            }
            else
            {
                string rref = txt_sAdUserName.Text;
                hpiconn.Open();
                string pp = "Select Password from SystemHPIUsersTBL where UserName = '" + rref + "'";
                SqlCommand commsi = new SqlCommand(pp, hpiconn);
                IDataReader rs;
                rs = commsi.ExecuteReader();
                rs.Read();
                string pword = rs.GetString(0);
                hpiconn.Close();

                txt_sAdAdminPassword.Attributes.Add("value", pword);

                ViewState["pass"] = pword;

                if (txt_superAdminCurrentPass.Text == ViewState["pass"].ToString())
                {
                    if (txt_superAdminNewPassword.Text == txt_superAdminConfirm.Text)
                    {
                        try
                        {
                            string q1 = "UPDATE SystemHPIUsersTBL SET Password = '" + txt_superAdminNewPassword.Text + "' where AdminID = '" + ViewState["idno"].ToString() + "'";
                            SqlCommand comm = new SqlCommand(q1, hpiconn);
                            hpiconn.Open();
                            comm.ExecuteNonQuery();
                            hpiconn.Close();

                            hpiconn.Open();
                            string qry3 = "Select Password from SystemHPIUsersTBl where AdminID = '" + ViewState["idno"].ToString() + "'";
                            SqlCommand commas = new SqlCommand(qry3, hpiconn);
                            IDataReader rder;
                            rder = commas.ExecuteReader();
                            rder.Read();
                            string pssword = rder.GetString(0);
                            hpiconn.Close();

                            txt_sAdAdminPassword.Attributes.Add("value", pssword);
                        }
                        catch (Exception ex)
                        {
                            Response.Write(ex.Message);
                        }

                        lbl_superAdminCurrentPass.Visible = false;
                        lbl_superAdminNewPass.Visible = false;
                        lbl_superAdminRetypePass.Visible = false;
                        txt_superAdminCurrentPass.Visible = false;
                        txt_superAdminNewPassword.Visible = false;
                        txt_superAdminConfirm.Visible = false;
                        btn_superAdminPassSave.Visible = false;
                        btn_superAdminPassCancel.Visible = false;
                        ViewState["pass"] = "";

                    }
                    else if (txt_superAdminNewPassword.Text != txt_superAdminConfirm.Text)
                    {
                        Response.Write("Comfirmation not valid");
                    }
                }
                else if (txt_superAdminCurrentPass.Text != txt_sAdAdminPassword.Text)
                {
                    Response.Write("Current Password mismatch!");
                    // Response.Write(txt_Password.Text +" "+txt_CurrentPass.Text);
                }

            }
        }

        protected void btn_ChangeContacts_Click(object sender, EventArgs e)
        {
            ViewState["pldt"] = txt_PLDT.Text;
            ViewState["landline"] = txt_Landline.Text;
            ViewState["mobile"] = txt_Mobile.Text;

            txt_PLDT.ReadOnly = false;
            txt_Landline.ReadOnly = false;
            txt_Mobile.ReadOnly = false;
            btn_ChangeContacts.Visible = false;
            btn_ContactCancel.Visible = true;
        }

        protected void btn_ContactCancel_Click(object sender, EventArgs e)
        {
            txt_PLDT.Text = ViewState["pldt"].ToString();
            txt_Landline.Text = ViewState["landline"].ToString();
            txt_Mobile.Text = ViewState["mobile"].ToString();

            ViewState["pldt"] = null;
            ViewState["landline"] = null;
            ViewState["mobile"] = null;

            txt_PLDT.ReadOnly = true;
            txt_Landline.ReadOnly = true;
            txt_Mobile.ReadOnly = true;
            btn_ChangeContacts.Visible = !false;
            btn_ContactCancel.Visible = !true;
        }


        protected void btn_SaveContacts_Click(object sender, EventArgs e)
        {
            string numgetter = "SELECT COUNT(*) as Numbers FROM SystemContactsTBL";
            SqlCommand c1 = new SqlCommand(numgetter, hpiconn);
            SqlDataReader re1;
            hpiconn.Open();
            re1 = c1.ExecuteReader();
            re1.Read();
            int number = Convert.ToInt32(re1.GetInt32(0));
            hpiconn.Close();
            if (number != 1)
            {
                hpiconn.Open();
                string qq = "insert into SystemContactsTBL (pldt,landline,mobile)values('" + txt_PLDT.Text + "','" + txt_Landline.Text + "','" + txt_Mobile.Text + "')";
                SqlCommand mande = new SqlCommand(qq, hpiconn);
                mande.ExecuteNonQuery();
                hpiconn.Close();
            }
            else
            {
                hpiconn.Open();
                string qq = "Update SystemContactsTBL set PLDT = '" + txt_PLDT.Text + "', Landline = '" + txt_Landline.Text + "', Mobile = '" + txt_Mobile.Text + "'";
                SqlCommand mande = new SqlCommand(qq, hpiconn);
                mande.ExecuteNonQuery();
                hpiconn.Close();
            }

            txt_PLDT.ReadOnly = true;
            txt_Landline.ReadOnly = true;
            txt_Mobile.ReadOnly = true;

            btn_ChangeContacts.Visible = !false;
            btn_ContactCancel.Visible = !true;
        }
        
        protected void txt_PLDT_TextChanged(object sender, EventArgs e)
        {
           // btn_SaveContacts.Text = "Cancel";
        }

        protected void txt_Landline_TextChanged(object sender, EventArgs e)
        {
           // btn_SaveContacts.Text = "Cancel";
        }

        protected void txt_Mobile_TextChanged(object sender, EventArgs e)
        {
           // btn_SaveContacts.Text = "Cancel";
        }







        protected void btn_EditCharge_Click(object sender, EventArgs e)
        {
            ViewState["unit"] = txt_UnitCharge.Text;
            ViewState["part"] = txt_PartCharge.Text;
            //
            ViewState["vat"] = txt_vat.Text;

            txt_UnitCharge.ReadOnly = false;
            txt_PartCharge.ReadOnly = false;
            //
            txt_vat.ReadOnly = false;

            btn_EditCharge.Visible = false;
            btn_ChargesCancel.Visible = true;
            //
            string vat = txt_vat.Text;
            string vat2 = Regex.Replace(vat, "[^a-zA-Z0-9_.]+", "");
            txt_vat.Text = vat2;
        }


        protected void btn_ChargesCancel_Click(object sender, EventArgs e)
        {
            txt_UnitCharge.Text = ViewState["unit"].ToString();
            txt_PartCharge.Text = ViewState["part"].ToString();
            //
            txt_vat.Text = ViewState["vat"].ToString();

            ViewState["unit"] = null;
            ViewState["part"] = null;
            //
            ViewState["vat"] = null;

            txt_UnitCharge.ReadOnly = true;
            txt_PartCharge.ReadOnly = true;
            //
            txt_vat.ReadOnly = true;
            
            btn_EditCharge.Visible = !false;
            btn_ChargesCancel.Visible = !true;
        }

        protected void btn_SaveCharge_Click(object sender, EventArgs e)
        {
            string s = txt_UnitCharge.Text;
            string a = txt_PartCharge.Text;
            string y = txt_vat.Text;

            string x = Convert.ToDecimal(s).ToString("#,##0.00");
            string x1 = Convert.ToDecimal(a).ToString("#,##0.00");

            hpiconn.Open();
            string qqq = "Update SystemChargesTBL set UnitsCharge = '" + x + "', PartsCharge = '" + x1 + "', vat = '" + y + "'";
            SqlCommand cmande = new SqlCommand(qqq, hpiconn);
            cmande.ExecuteNonQuery();
            hpiconn.Close();

            txt_UnitCharge.ReadOnly = true;
            txt_PartCharge.ReadOnly = true;
            txt_vat.ReadOnly = true;
            
            btn_EditCharge.Visible = !false;
            btn_ChargesCancel.Visible = !true;

            hpiconn.Open();
            string chargegetter = "Select UnitsCharge, PartsCharge ,vat from SystemChargesTBL";
            SqlCommand comands = new SqlCommand(chargegetter, hpiconn);
            IDataReader rads;
            rads = comands.ExecuteReader();
            rads.Read();
            txt_UnitCharge.Text = rads.GetString(0);
            txt_PartCharge.Text = rads.GetString(1);
            txt_vat.Text = rads.GetString(2) + "%";
            hpiconn.Close();
        }



        protected void btn_UnitCancel_Click(object sender, EventArgs e)
        {
            txt_UnitPercentage.Text = ViewState["uperc"].ToString();
            txt_NumberUnits.Text = ViewState["unums"].ToString();

            ViewState["uperc"] = null;
            ViewState["unums"] = null;

            txt_UnitPercentage.ReadOnly = true;
            txt_NumberUnits.ReadOnly = true;

            btn_UnitsEdit.Visible = !false;
            btn_UnitCancel.Visible = !true;
        }
        protected void btn_UnitsEdit_Click(object sender, EventArgs e)
        {
            ViewState["uperc"] = txt_UnitPercentage.Text;
            ViewState["unums"] = txt_NumberUnits.Text;

            txt_UnitPercentage.ReadOnly = false;
            txt_NumberUnits.ReadOnly = false;

            btn_UnitsEdit.Visible = false;
            btn_UnitCancel.Visible = true;

            string pers = txt_UnitPercentage.Text;
            string pers2 = Regex.Replace(pers, "[^a-zA-Z0-9_.]+", "");
            txt_UnitPercentage.Text = pers2;
        }
        protected void btn_UnitSave_Click(object sender, EventArgs e)
        {
            if (txt_UnitPercentage.ReadOnly != true)
            {
hpiconn.Open();
            string qqqs = "Update SystemDiscountsTBL set Percentage = '" + txt_UnitPercentage.Text + "', DiscountQuantity = '" + txt_NumberUnits.Text + "' where Category = 'Units'";
            SqlCommand cmandes = new SqlCommand(qqqs, hpiconn);
            cmandes.ExecuteNonQuery();
            hpiconn.Close();

            txt_UnitPercentage.ReadOnly = true;
            txt_NumberUnits.ReadOnly = true;

            btn_UnitsEdit.Visible = !false;
            btn_UnitCancel.Visible = !true;

            hpiconn.Open();
            string discountgetter = "Select Percentage, DiscountQuantity from SystemDiscountsTBL";
            SqlCommand comander = new SqlCommand(discountgetter, hpiconn);
            IDataReader rader;
            rader = comander.ExecuteReader();
            rader.Read();
            txt_PartPercentage.Text = rader.GetString(0) + "%";
            txt_NumberParts.Text = rader.GetString(1);

            rader.Read();
            txt_UnitPercentage.Text = rader.GetString(0) + "%";
            txt_NumberUnits.Text = rader.GetString(1);
            hpiconn.Close();
            }
            
        }
        protected void btn_PartsCancel_Click(object sender, EventArgs e)
        {
            txt_PartPercentage.Text = ViewState["pperc"].ToString();
            txt_NumberParts.Text = ViewState["pnums"].ToString();

            ViewState["pperc"] = null;
            ViewState["pnums"] = null;

            txt_PartPercentage.ReadOnly = true;
            txt_NumberParts.ReadOnly = true;

            btn_PartsEdit.Visible = !false;
            btn_PartsCancel.Visible = !true;
        }
        protected void btn_PartsEdit_Click(object sender, EventArgs e)
        {
            ViewState["pperc"] = txt_PartPercentage.Text;
            ViewState["pnums"] = txt_NumberParts.Text;

            txt_PartPercentage.ReadOnly = false;
            txt_NumberParts.ReadOnly = false;

            btn_PartsEdit.Visible = false;
            btn_PartsCancel.Visible = true;

            string pers = txt_PartPercentage.Text;
            string pers2 = Regex.Replace(pers, "[^a-zA-Z0-9_.]+", "");
            txt_PartPercentage.Text = pers2;
        }

        protected void btn_PartsSave_Click(object sender, EventArgs e)
        {
            if (txt_PartPercentage.ReadOnly != true)
            {
                hpiconn.Open();
                string qqq = "Update SystemDiscountsTBL set Percentage = '" + txt_PartPercentage.Text + "', DiscountQuantity = '" + txt_NumberParts.Text + "' where Category = 'Parts'";
                SqlCommand cmande = new SqlCommand(qqq, hpiconn);
                cmande.ExecuteNonQuery();
                hpiconn.Close();

                txt_PartPercentage.ReadOnly = true;
                txt_NumberParts.ReadOnly = true;

                btn_PartsEdit.Visible = !false;
                btn_PartsCancel.Visible = !true;

                hpiconn.Open();
                string discountgetter = "Select Percentage, DiscountQuantity from SystemDiscountsTBL";
                SqlCommand comander = new SqlCommand(discountgetter, hpiconn);
                IDataReader rader;
                rader = comander.ExecuteReader();
                rader.Read();
                txt_PartPercentage.Text = rader.GetString(0) + "%";
                txt_NumberParts.Text = rader.GetString(1);

                rader.Read();
                txt_UnitPercentage.Text = rader.GetString(0) + "%";
                txt_NumberUnits.Text = rader.GetString(1);
                hpiconn.Close();
            }
            
        }

        protected void Button8_Click(object sender, EventArgs e)
        {
            MultiView4.ActiveViewIndex = 0;
        }

        protected void Button15_Click(object sender, EventArgs e)
        {
            MultiView4.ActiveViewIndex = 1;
        }


        protected void btn_uAddColor_Click(object sender, EventArgs e)
        {
            hpiconn.Open();
            string add = "insert into SystemModleColorsTBL(Color)values('" + txt_uAddColor.Text+ "')";
            SqlCommand m = new SqlCommand(add,hpiconn);
            m.ExecuteNonQuery();
            hpiconn.Close();

            ddwn_UnitColor1.Items.Clear();

            string query = "select Color from SystemModleColorsTBL";

            SqlCommand cmd = new SqlCommand(query, hpiconn);
            hpiconn.Open();
            SqlDataReader dr;
            dr = cmd.ExecuteReader();

            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    ddwn_UnitColor1.Items.Add(dr[0].ToString());
                    ddwn_uDelColors.Items.Add(dr[0].ToString());
                }
            }
            hpiconn.Close();
            txt_uAddColor.Visible = false;
            btn_uCancelAddColor.Visible = false;
            btn_uAddColor.Visible = false;
        }

        protected void btn_UnitAddColor_Click(object sender, EventArgs e)
        {
           // ddwn_UnitColor1.Items.Add(new ListItem("txt_box1.Text"));
            txt_uAddColor.Visible = true;
                btn_uCancelAddColor.Visible = true;
                btn_uAddColor.Visible = true;
        }

        protected void ddwn_UnitColor1_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        protected void ddwn_UnitColor1_TextChanged(object sender, EventArgs e)
        {

        }

        protected void btn_UnitAdd_Click(object sender, EventArgs e)
        {
            
            int done = 0;
            string idtest1 = txt_ModelCode.Text;
            string colortest2 = ddwn_UnitColor1.Text;
          //  int quan = Convert.ToInt32(txt_UnitQuan1.Text);

            //int rows = grid_Units.Rows.Count;
            //for (int cv = 0; cv <= rows - 1; cv++)
            //{
            //    ViewState["ModelCode"] = grid_Units.Rows[cv].Cells[1].Text;
            //    ViewState["Colors"] = grid_Units.Rows[cv].Cells[3].Text;
            //    if (ViewState["ModelCode"].ToString() == idtest1)
            //    {
            //        if (ViewState["Colors"].ToString() == colortest2)
            //        {

            //            int num = Convert.ToInt32(grid_Units.Rows[cv].Cells[4].Text);
            //            int added = num + quan;
            //            string av = added.ToString();

            //            string ass = txt_UnitInitPrice.Text;

            //            string x = Convert.ToDecimal(ass).ToString("#,##0.00");

            //            //hpiconn.Open();
            //            //string d = "Select ";
            //            //hpiconn.Close();

            //            string save = "UPDATE SystemModelsTBL SET Quantity = '" + av + "', Price = '"+x+"'  WHERE ModelCode = '" + ViewState["ModelCode"].ToString() + "' AND Color ='" + ViewState["Colors"].ToString() + "'";
            //            hpiconn.Open();
            //            SqlCommand up = new SqlCommand(save, hpiconn);
            //            up.ExecuteNonQuery();
            //            hpiconn.Close();

            //            grid_Units.DataBind();

            //            done = 1;

            //            break;
            //        }
            //    }
            //    else
            //    {

            //    }
            //}

            if (done == 0)
            {
                string MCode = txt_ModelCode.Text;
                string Mdesc = txt_uDescription.Text;
                string Color = ddwn_UnitColor1.Text;
                //string Quan = txt_UnitQuan1.Text;
                string InitPrice = txt_UnitInitPrice.Text;

                //int num = 123456789;
                string x = Convert.ToDecimal(InitPrice).ToString("#,##0.00");
                //Response.Write(x);


                hpiconn.Open();
                string a = "insert into SystemModelsTBL (ModelCode,Description,Color,Quantity,initialPrice,status)values('" + MCode + "','" + Mdesc + "','" + Color + "','0','" + x + "','Out')";
                SqlCommand c = new SqlCommand(a, hpiconn);
                c.ExecuteNonQuery();
                hpiconn.Close();
                grid_Units.DataBind();

            }
            done = 0;
        }

        protected void btn_uCancelAddColor_Click(object sender, EventArgs e)
        {
            txt_uAddColor.Visible = false;
            btn_uCancelAddColor.Visible = false;
            btn_uAddColor.Visible = false;
        }

        protected void btn_UnitDelColor_Click(object sender, EventArgs e)
        {

            ddwn_uDelColors.Items.Clear();
            string query = "select Color from SystemModleColorsTBL";

            SqlCommand cmd = new SqlCommand(query, hpiconn);
            hpiconn.Open();
            SqlDataReader dr;
            dr = cmd.ExecuteReader();

            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    ddwn_uDelColors.Items.Add(dr[0].ToString());
                    //ddwn_UnitColor2.Items.Add(dr[0].ToString());
                }
            }
            hpiconn.Close();

            ddwn_uDelColors.Visible = true;
            btn_uCancelDelColor.Visible = true;
            btn_uDelColor.Visible = true;
        }

        protected void btn_uDelColor_Click(object sender, EventArgs e)
        {
            string text = ddwn_uDelColors.Text;
            hpiconn.Open();
            string del = "DELETE FROM SystemModleColorsTBL WHERE Color = '"+text+"'";
            SqlCommand d = new SqlCommand(del, hpiconn);
            d.ExecuteNonQuery();
            hpiconn.Close();

            ddwn_UnitColor1.Items.Clear();

            string query = "select Color from SystemModleColorsTBL";

            SqlCommand cmd = new SqlCommand(query, hpiconn);
            hpiconn.Open();
            SqlDataReader dr;
            dr = cmd.ExecuteReader();

            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    ddwn_UnitColor1.Items.Add(dr[0].ToString());
                    //ddwn_UnitColor2.Items.Add(dr[0].ToString());
                }
            }

            ddwn_uDelColors.Visible = !true;
            btn_uCancelDelColor.Visible = !true;
            btn_uDelColor.Visible = !true;

            txt_ModelCode.Text = "";
            txt_uDescription.Text ="";



           // ddwn_UnitColor1.Text = "Red";//////////////////////////////////dto na ako

        //    txt_UnitQuan1.Text = "";
            txt_UnitInitPrice.Text = "";

            txt_ModelCode.ReadOnly = true;
            txt_uDescription.ReadOnly = true;
            ddwn_UnitColor1.Enabled = !true;
       //     txt_UnitQuan1.ReadOnly = true;
            txt_UnitInitPrice.ReadOnly = true;

            btn_UnitAdd.Visible = true;
            btn_uEditSave.Visible = false;
            btn_uCancelEdit.Visible = false;

            txt_ModelCode.ReadOnly = !true;
            txt_uDescription.ReadOnly = !true;
            ddwn_UnitColor1.Enabled = true;
        //    txt_UnitQuan1.ReadOnly = !true;
            txt_UnitInitPrice.ReadOnly = !true;
        }

        protected void btn_uCancelDelColor_Click(object sender, EventArgs e)
        {
            ddwn_uDelColors.Visible = !true;
            btn_uCancelDelColor.Visible = !true;
            btn_uDelColor.Visible = !true;
        }

        protected void grid_Units_SelectedIndexChanged(object sender, EventArgs e)
        {

            //hpiconn.Open();
            //string xx = "Select ModelNumber from SystemModelsTBL where ";
            //hpiconn.Close();
            //ViewState["modIDGetters"] = "";


            ViewState["ColorGetters"] = grid_Units.SelectedRow.Cells[3].Text;
            ViewState["IDGetters"] = grid_Units.SelectedRow.Cells[1].Text;
            string que = "Select ModelNumber,ModelCode,Description,Color,Quantity,initialPrice from SystemModelsTBL where ModelCode = '" + ViewState["IDGetters"].ToString() + "' AND Color = '" + ViewState["ColorGetters"] .ToString()+ "'";
            SqlCommand commssy = new SqlCommand(que, hpiconn);
            IDataReader rders;
            hpiconn.Open();
            rders = commssy.ExecuteReader();
            rders.Read();
            ViewState["modIDGetters"] = rders.GetInt32(0).ToString();
            ViewState["ModelCode"] = rders.GetString(1).ToString();
            ViewState["Description"] = rders.GetString(2).ToString();
            ViewState["Color"] = rders.GetString(3).ToString();
            ViewState["Quantity"] = rders.GetString(4).ToString();
            ViewState["Price"] = rders.GetString(5).ToString();
            hpiconn.Close();

            txt_ModelCode.ReadOnly = true;
            txt_uDescription.ReadOnly = true;
            ddwn_UnitColor1.Enabled = !true;
         //   txt_UnitQuan1.ReadOnly = true;
            txt_UnitInitPrice.ReadOnly = true;
            try
            {
                txt_ModelCode.Text = ViewState["ModelCode"].ToString();
                txt_uDescription.Text = ViewState["Description"].ToString();
                ddwn_UnitColor1.Text = ViewState["Color"].ToString();
           //     txt_UnitQuan1.Text = ViewState["Quantity"].ToString();
                txt_UnitInitPrice.Text = ViewState["Price"].ToString();
            }
            catch (ArgumentOutOfRangeException ex)
            {
                string adds = "insert into SystemModleColorsTBL (Color)values('" + ViewState["Color"].ToString() + "')";
                hpiconn.Open();
                SqlCommand ad = new SqlCommand(adds, hpiconn);
                ad.ExecuteNonQuery();
                hpiconn.Close();

                ddwn_UnitColor1.Items.Clear();
                ddwn_uDelColors.Items.Clear();

                string query = "select Color from SystemModleColorsTBL";

                SqlCommand cmd = new SqlCommand(query, hpiconn);
                hpiconn.Open();
                SqlDataReader dr;
                dr = cmd.ExecuteReader();

                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        ddwn_UnitColor1.Items.Add(dr[0].ToString());
                        ddwn_uDelColors.Items.Add(dr[0].ToString());
                    }
                }
                hpiconn.Close();

                txt_ModelCode.Text = ViewState["ModelCode"].ToString();
                txt_uDescription.Text = ViewState["Description"].ToString();
                ddwn_UnitColor1.Text = ViewState["Color"].ToString();
           //     txt_UnitQuan1.Text = ViewState["Quantity"].ToString();
                txt_UnitInitPrice.Text = ViewState["Price"].ToString();
            }
            
        }

        protected void btn_UnitEdit_Click(object sender, EventArgs e)
        {
            txt_ModelCode.ReadOnly = !true;
            txt_uDescription.ReadOnly = !true;
            ddwn_UnitColor1.Enabled = true;
         //   txt_UnitQuan1.ReadOnly = !true;
            txt_UnitInitPrice.ReadOnly = !true;

            btn_UnitAdd.Visible = false;

            btn_uEditSave.Visible = true;
            btn_uCancelEdit.Visible = true;

            btn_UnitEdit.Visible = false;
        }

        protected void btn_uCancelEdit_Click(object sender, EventArgs e)
        {
            btn_UnitAdd.Visible = true; 
            btn_uEditSave.Visible = false;
            btn_uCancelEdit.Visible = false;

            txt_ModelCode.ReadOnly = !true;
            txt_uDescription.ReadOnly = !true;
            ddwn_UnitColor1.Enabled = true;
          //  txt_UnitQuan1.ReadOnly = !true;
            txt_UnitInitPrice.ReadOnly = !true;

            txt_ModelCode.Text = "";
            txt_uDescription.Text = "";
            ddwn_UnitColor1.SelectedIndex = 0;
         //   txt_UnitQuan1.Text = "";
            txt_UnitInitPrice.Text = "";

            btn_UnitEdit.Visible = true;
        }

        protected void btn_uEditSave_Click(object sender, EventArgs e)
        {
            string reffff = ViewState["modIDGetters"].ToString();

            string mod = txt_ModelCode.Text;
            string desc = txt_uDescription.Text;
            string color = ddwn_UnitColor1.SelectedItem.ToString();
          //  string quant = txt_UnitQuan1.Text;
            string initP = txt_UnitInitPrice.Text;

            string x = Convert.ToDecimal(initP).ToString("#,##0.00");

            string save = "UPDATE SystemModelsTBL SET ModelCode = '" + mod + "', Description = '" + desc + "', Color = '" + color + "',initialPrice = '" + x + "' WHERE ModelNumber = '" + reffff + "'";
            hpiconn.Open();
            SqlCommand up = new SqlCommand(save,hpiconn);
            up.ExecuteNonQuery();
            hpiconn.Close();

            grid_Units.DataBind();


            btn_UnitAdd.Visible = true;
            btn_uEditSave.Visible = false;
            btn_uCancelEdit.Visible = false;

            txt_ModelCode.ReadOnly = !true;
            txt_uDescription.ReadOnly = !true;
            ddwn_UnitColor1.Enabled = true;
        //    txt_UnitQuan1.ReadOnly = !true;
            txt_UnitInitPrice.ReadOnly = !true;

            txt_ModelCode.Text = "";
            txt_uDescription.Text = "";
            ddwn_UnitColor1.SelectedIndex = 0;
         //   txt_UnitQuan1.Text = "";
            txt_UnitInitPrice.Text = "";

            btn_UnitEdit.Visible = true;

        }

        protected void btn_UnitDelete_Click(object sender, EventArgs e)
        {
            hpiconn.Open();
            string del = "delete from SystemModelsTBL where ModelNumber = '" + ViewState["modIDGetters"].ToString() + "'";
            SqlCommand sa = new SqlCommand(del,hpiconn);
            sa.ExecuteNonQuery();
            hpiconn.Close();

            grid_Units.DataBind();
        }

        protected void btn_uClear_Click(object sender, EventArgs e)
        {
            btn_UnitAdd.Visible = true;
            btn_uEditSave.Visible = false;
            btn_uCancelEdit.Visible = false;

            txt_ModelCode.ReadOnly = !true;
            txt_uDescription.ReadOnly = !true;
            ddwn_UnitColor1.Enabled = true;
          //  txt_UnitQuan1.ReadOnly = !true;
            txt_UnitInitPrice.ReadOnly = !true;

            txt_ModelCode.Text = "";
            txt_uDescription.Text = "";
            ddwn_UnitColor1.SelectedIndex = 0;
         //   txt_UnitQuan1.Text = "";
            txt_UnitInitPrice.Text = "";

            btn_UnitEdit.Visible = true;
        }

        protected void Button14_Click(object sender, EventArgs e)
        {
            //Response.Write("Iloveu baby");
        }

        protected void btn_pdelete_Click(object sender, EventArgs e)
        {
            hpiconn.Open();
            string del = "delete from SystemPartsTBL where PartID = '" + ViewState["partIDGetters"].ToString() + "'";
            SqlCommand sa = new SqlCommand(del, hpiconn);
            sa.ExecuteNonQuery();
            hpiconn.Close();

            grid_Parts.DataBind();
        }





        protected void grid_Parts_SelectedIndexChanged(object sender, EventArgs e)
        {
            ViewState["ColorGetterss"] = grid_Parts.SelectedRow.Cells[3].Text;
            ViewState["IDGetterss"] = grid_Parts.SelectedRow.Cells[1].Text;
            string que = "Select * from SystemPartsTBL where PartNumber = '" + ViewState["IDGetterss"].ToString() + "' AND Color = '" + ViewState["ColorGetterss"].ToString() + "'";
            SqlCommand commssy = new SqlCommand(que, hpiconn);
            IDataReader rders;
            hpiconn.Open();
            rders = commssy.ExecuteReader();
            rders.Read();
            ViewState["partIDGetters"] = rders.GetInt32(0).ToString();
            ViewState["PartNumber"] = rders.GetString(1).ToString();
            ViewState["Descriptionn"] = rders.GetString(2).ToString();
            ViewState["Colorr"] = rders.GetString(3).ToString();
            ViewState["Quantityy"] = rders.GetString(4).ToString();
            ViewState["Pricee"] = rders.GetString(5).ToString();
            hpiconn.Close();

            txt_PartNumber.ReadOnly = true;
            txt_Description.ReadOnly = true;
            ddwn_pColor.Enabled = !true;
          //  txt_pQuan.ReadOnly = true;
            txt_Price.ReadOnly = true;
            try
            {
                txt_PartNumber.Text = ViewState["PartNumber"].ToString();
                txt_Description.Text = ViewState["Descriptionn"].ToString();
                ddwn_pColor.Text = ViewState["Colorr"].ToString();
            //    txt_pQuan.Text = ViewState["Quantityy"].ToString();
                txt_Price.Text = ViewState["Pricee"].ToString();
            }
            catch (ArgumentOutOfRangeException ex)
            {
                string adds = "insert into SystemPartColorsTBL (Color)values('" + ViewState["Colorr"].ToString() + "')";
                hpiconn.Open();
                SqlCommand ad = new SqlCommand(adds, hpiconn);
                ad.ExecuteNonQuery();
                hpiconn.Close();

                ddwn_pColor.Items.Clear();
                ddwn_delColor.Items.Clear();

                string query = "select Color from SystemPartColorsTBL";

                SqlCommand cmd = new SqlCommand(query, hpiconn);
                hpiconn.Open();
                SqlDataReader dr;
                dr = cmd.ExecuteReader();

                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        ddwn_pColor.Items.Add(dr[0].ToString());
                        ddwn_delColor.Items.Add(dr[0].ToString());
                    }
                }
                hpiconn.Close();

                txt_PartNumber.Text = ViewState["PartNumber"].ToString();
                txt_Description.Text = ViewState["Descriptionn"].ToString();
                ddwn_pColor.Text = ViewState["Colorr"].ToString();
              //  txt_pQuan.Text = ViewState["Quantityy"].ToString();
                txt_Price.Text = ViewState["Pricee"].ToString();
            }
        }

        protected void Button10_Click(object sender, EventArgs e)
        {
            
        }

        protected void btn_pAdd_Click(object sender, EventArgs e)
        {
            int done = 0;
            string idtest1 = txt_PartNumber.Text;
            string colortest2 = ddwn_pColor.Text;
         //   int quan = Convert.ToInt32(txt_pQuan.Text);

            int rows = grid_Parts.Rows.Count;
            for (int cv = 0; cv <= rows - 1; cv++)
            {
                ViewState["partNumber"] = grid_Parts.Rows[cv].Cells[1].Text;
                ViewState["colors"] = grid_Parts.Rows[cv].Cells[3].Text;

                if (ViewState["partNumber"].ToString() == idtest1)
                {
                    if (ViewState["colors"].ToString() == colortest2)
                    {

                        int num = Convert.ToInt32(grid_Parts.Rows[cv].Cells[4].Text);
                        int added = num/* + quan*/;
                        string av = added.ToString();


                        string ass = txt_Price.Text;

                        string x = Convert.ToDecimal(ass).ToString("#,##0.00");
                        //hpiconn.Open();
                        //string d = "Select ";
                        //hpiconn.Close();

                        string save = "UPDATE SystemPartsTBL SET Quantity = '" + av + "' , InitialPrice = '" + x + "' WHERE PartNumber = '" + ViewState["partNumber"].ToString() + "' AND Color ='" + ViewState["colors"].ToString() + "'";
                        hpiconn.Open();
                        SqlCommand up = new SqlCommand(save, hpiconn);
                        up.ExecuteNonQuery();
                        hpiconn.Close();

                        grid_Parts.DataBind();

                        done = 1;

                        break;
                    }
                }
                else 
                {
                    
                }
            }

            if (done == 0)
            {
                string Pno = txt_PartNumber.Text;
                string Pdesc = txt_Description.Text;
                string PColor = ddwn_pColor.Text;
               // string PQuan = txt_pQuan.Text;
                //string PInitPrice = txt_Price.Text;

                string ass = txt_Price.Text;

                string x = Convert.ToDecimal(ass).ToString("#,##0.00");

                hpiconn.Open();
                string a = "insert into SystemPartsTBL (PartNumber,Description,Color,Quantity,InitialPrice)values('" + Pno + "','" + Pdesc + "','" + PColor + "','0','" + x + "')";
                SqlCommand c = new SqlCommand(a, hpiconn);
                c.ExecuteNonQuery();
                hpiconn.Close();
                grid_Parts.DataBind();

            }
            done = 0;
            

            


            

           
        }

        protected void btn_pSave_Click(object sender, EventArgs e)
        {
            string reffff = ViewState["partIDGetters"].ToString();

            string modd = txt_PartNumber.Text;
            string descc = txt_Description.Text;
            string colorr = ddwn_pColor.SelectedItem.ToString();
           // string quantt = txt_pQuan.Text;
            string initPp = txt_Price.Text;

            string x = Convert.ToDecimal(initPp).ToString("#,##0.00");

            string save = "UPDATE SystemPartsTBL SET PartNumber = '" + modd + "', Description = '" + descc + "', Color = '" + colorr + "', InitialPrice = '" + x + "' WHERE PartID = '" + reffff + "'";
            hpiconn.Open();
            SqlCommand up = new SqlCommand(save, hpiconn);
            up.ExecuteNonQuery();
            hpiconn.Close();

            grid_Parts.DataBind();


            btn_pAdd.Visible = true;
            btn_pSave.Visible = false;
            btn_pCancel.Visible = false;

            txt_PartNumber.ReadOnly = !true;
            txt_Description.ReadOnly = !true;
            ddwn_pColor.Enabled = true;
          //  txt_pQuan.ReadOnly = !true;
            txt_Price.ReadOnly = !true;

            txt_PartNumber.Text = "";
            txt_Description.Text = "";
            ddwn_pColor.SelectedIndex = 0;
           // txt_pQuan.Text = "";
            txt_Price.Text = "";

            btn_pEdit.Visible = true;
        }

        protected void btn_pCancel_Click(object sender, EventArgs e)
        {
            btn_pAdd.Visible = true;
            btn_pSave.Visible = false;
            btn_pCancel.Visible = false;

            txt_PartNumber.ReadOnly = !true;
            txt_Description.ReadOnly = !true;
            ddwn_pColor.Enabled = true;
//txt_pQuan.ReadOnly = !true;
            txt_Price.ReadOnly = !true;

            txt_PartNumber.Text = "";
            txt_Description.Text = "";
            ddwn_pColor.SelectedIndex = 0;
           // txt_pQuan.Text = "";
            txt_Price.Text = "";

            btn_pEdit.Visible = true;
        }

        protected void btn_pEdit_Click(object sender, EventArgs e)
        {
            txt_PartNumber.ReadOnly = !true;
            txt_Description.ReadOnly = !true;
            ddwn_pColor.Enabled = true;
           // txt_pQuan.ReadOnly = !true;
            txt_Price.ReadOnly = !true;

            btn_pAdd.Visible = false;

            btn_pSave.Visible = true;
            btn_pCancel.Visible = true;

            btn_pEdit.Visible = false;
        }

        protected void btn_pClear_Click(object sender, EventArgs e)
        {
            btn_pAdd.Visible = true;
            btn_pSave.Visible = false;
            btn_pCancel.Visible = false;

            txt_PartNumber.ReadOnly = !true;
            txt_Description.ReadOnly = !true;
            ddwn_pColor.Enabled = true;
        //    txt_pQuan.ReadOnly = !true;
            txt_Price.ReadOnly = !true;

            txt_PartNumber.Text = "";
            txt_Description.Text = "";
            ddwn_pColor.SelectedIndex = 0;
         //   txt_pQuan.Text = "";
            txt_Price.Text = "";

            btn_pEdit.Visible = true;
        }

        protected void btn_pAddColor_Click(object sender, EventArgs e)
        {
            // ddwn_UnitColor1.Items.Add(new ListItem("txt_box1.Text"));
            txt_AddColor.Visible = true;
            btn_pCancelcolor.Visible = true;
            btn_pAdcolor.Visible = true;
        }

        protected void btn_pDelColor_Click(object sender, EventArgs e)
        {
            ddwn_delColor.Items.Clear();
            string query = "select Color from SystemPartColorsTBL";

            SqlCommand cmd = new SqlCommand(query, hpiconn);
            hpiconn.Open();
            SqlDataReader dr;
            dr = cmd.ExecuteReader();

            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    ddwn_delColor.Items.Add(dr[0].ToString());
                    //ddwn_UnitColor2.Items.Add(dr[0].ToString());
                }
            }
            hpiconn.Close();

            ddwn_delColor.Visible = true;
            btn_pCancedel.Visible = true;
            btn_pDeldel.Visible = true;
        }

        protected void btn_pCancelcolor_Click(object sender, EventArgs e)
        {
            txt_AddColor.Visible = false;
            btn_pCancelcolor.Visible = false;
            btn_pAdcolor.Visible = false;
        }

        protected void btn_pAdcolor_Click(object sender, EventArgs e)
        {
            hpiconn.Open();
            string add = "insert into SystemPartColorsTBL(Color)values('" + txt_AddColor.Text + "')";
            SqlCommand m = new SqlCommand(add, hpiconn);
            m.ExecuteNonQuery();
            hpiconn.Close();

            ddwn_pColor.Items.Clear();

            string query = "select Color from SystemPartColorsTBL";

            SqlCommand cmd = new SqlCommand(query, hpiconn);
            hpiconn.Open();
            SqlDataReader dr;
            dr = cmd.ExecuteReader();

            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    ddwn_pColor.Items.Add(dr[0].ToString());
                    ddwn_delColor.Items.Add(dr[0].ToString());
                }
            }
            hpiconn.Close();
            txt_AddColor.Visible = false;
            btn_pCancelcolor.Visible = false;
            btn_pAdcolor.Visible = false;
        }

        protected void btn_pCancedel_Click(object sender, EventArgs e)
        {
            ddwn_delColor.Visible = !true;
            btn_pCancedel.Visible = !true;
            btn_pDeldel.Visible = !true;
        }

        protected void btn_pDeldel_Click(object sender, EventArgs e)
        {
            string text = ddwn_delColor.Text;
            hpiconn.Open();
            string del = "DELETE FROM SystemPartColorsTBL WHERE Color = '" + text + "'";
            SqlCommand d = new SqlCommand(del, hpiconn);
            d.ExecuteNonQuery();
            hpiconn.Close();

            ddwn_pColor.Items.Clear();

            string query = "select Color from SystemPartColorsTBL";

            SqlCommand cmd = new SqlCommand(query, hpiconn);
            hpiconn.Open();
            SqlDataReader dr;
            dr = cmd.ExecuteReader();

            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    ddwn_pColor.Items.Add(dr[0].ToString());
                    //ddwn_UnitColor2.Items.Add(dr[0].ToString());
                }
            }

            ddwn_delColor.Visible = !true;
            btn_pCancedel.Visible = !true;
            btn_pDeldel.Visible = !true;

            txt_PartNumber.Text = "";
            txt_Description.Text = "";



            // ddwn_UnitColor1.Text = "Red";//////////////////////////////////

           // txt_pQuan.Text = "";
            txt_Price.Text = "";

            txt_PartNumber.ReadOnly = true;
            txt_Description.ReadOnly = true;
            ddwn_pColor.Enabled = !true;
            //txt_pQuan.ReadOnly = true;
            txt_Price.ReadOnly = true;

            btn_pAdd.Visible = true;
            btn_pSave.Visible = false;
            btn_pCancel.Visible = false;

            txt_PartNumber.ReadOnly = !true;
            txt_Description.ReadOnly = !true;
            ddwn_pColor.Enabled = true;
           // txt_pQuan.ReadOnly = !true;
            txt_Price.ReadOnly = !true;
        }
        string sel = "select Status from SystemPOUnitsTBL where status = 'unseen'";
        string sel2 = "select Status from SystemPOPartsTBL where status = 'unseen'";
        protected void Timer1_Tick(object sender, EventArgs e)
        {
            SqlCommand com = new SqlCommand(sel, hpiconn);
            IDataReader r;
            hpiconn.Open();
            r = com.ExecuteReader();
            int a = 0;
            while (r.Read())
            {

                string not = r.GetString(0).ToString();//secon row
                if (not == "UNSEEN")
                    a++;
            }
            hpiconn.Close();

            SqlCommand coma = new SqlCommand(sel2, hpiconn);
            IDataReader ra;
            hpiconn.Open();
            ra = coma.ExecuteReader();
            int aa = 0;
            while (ra.Read())
            {
                string not = ra.GetString(0).ToString();
                if (not == "UNSEEN")
                aa++;
            }
            hpiconn.Close();

            int ax = a + aa;

            if (a != 0 || aa != 0)
            {
                lbl_notifN.Visible = !true;
                lbl_NumberNewPO.Visible = true;
                lbl_NumberNewPO.Text = ax.ToString();
            }
            else if (a == 0 || aa == 0)
            {
                lbl_notifN.Visible = true;
                lbl_NumberNewPO.Visible = false;
            }

            
        }

        protected void grid_Sent_SelectedIndexChanged(object sender, EventArgs e)
        {
            string po = grid_Sent.SelectedRow.Cells[1].Text;
            string datepo = grid_Sent.SelectedRow.Cells[2].Text;

            if (po != txt_POnum.Text)
            {
                txt_Total.Text = "";
                txt_TotalCharge.Text = "";
                txt_Discount.Text = "";
                txt_DiscountedAmount.Text = "";
                txt_GrandTotal.Text = "";
                txt_Vats.Text = "";
                //ddwn_TermsOfPayment.SelectedIndex = 0;
            }
            txt_POnum.Text = po;
            txt_DateOfPO.Text = datepo;

            hpiconn.Open();
            string ass = "select ModelCode,Description,Color,Quantity from a" + po + "_Units";
            //Response.Write(ass);
            SqlDataAdapter adapter = new SqlDataAdapter(ass, hpiconn);

            DataSet set = new DataSet();
            adapter.Fill(set);

            //grid_PurchaseOrderDetails1.DataSource = null;

            grid_PO1.DataSource = set.Tables[0];
            grid_PO1.DataBind();
            hpiconn.Close();


            try
            {
                hpiconn.Open();
                string ass1 = "select ModelCode,Description,Color,Quantity from b" + po + "_Units";
                //Response.Write(ass);
                SqlDataAdapter adapter1 = new SqlDataAdapter(ass1, hpiconn);

                DataSet set1 = new DataSet();
                adapter1.Fill(set1);

                //grid_PurchaseOrderDetails1.DataSource = null;

                grid_PO2.DataSource = set1.Tables[0];
                grid_PO2.DataBind();
                hpiconn.Close();
                lbl_Added.Visible = true;
            }
            catch (Exception ex)
            {
                hpiconn.Close();
                Response.Write(ex.Message);
               lbl_Added.Visible = !true;
                grid_PO2.DataSource = null;
                grid_PO2.DataBind();
            }


            //MultiView1.ActiveViewIndex = 1;
            //MultiView4.ActiveViewIndex = 0;


            hpiconn.Open();
            string u = "update systemPOUnitsTBL set Status = 'Viewed' where  purchaseorderNumber = '" + po + "'";
            SqlCommand q = new SqlCommand(u, hpiconn);
            q.ExecuteNonQuery();
            hpiconn.Close();
            grid_update1.Visible = false;
            grid_update2.Visible = false;

            if (ddwn_Category.Text == "Units")
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
            else if (ddwn_Category.Text == "Spare Parts")
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
            ViewState["pon"] = grid_Sent.SelectedRow.Cells[1].Text;

            lbl_SI.Visible = false;
            txt_SalesInvoice.Visible = !true;
            btn_CreateSI.Visible = false;
            btn_CancelProcess.Visible = false;

            MultiView1.ActiveViewIndex = 2;
            Timer4.Enabled = !true;
            Timer5.Enabled = !true;
        }

        protected void btn_CreateSI_Click(object sender, EventArgs e)
        {
            //string num3 = "";
            //Random rnd = new Random();
            //int num1 = rnd.Next(1000, 9999);
            //int num2 = rnd.Next(1000, 9999);

            //var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            //var random = new Random();
            //var list = Enumerable.Repeat(0, 2).Select(x => chars[random.Next(chars.Length)]);
            //string v = string.Join("", list);

            //string final = v + num1.ToString() + num2.ToString();
            //txt_SalesInvoice.Text = final;
            lbl_SI.Visible = !false;
            txt_SalesInvoice.Visible = true;

            lbl_DeliveryDate.Visible = true;
            txt_DeliveryDate.Visible = true;
            btn_Proceed.Visible = true;
            //btn_Cancel.Visible = true;
            btn_Cal.Visible = true;
        }

        protected void btn_Cancel_Click(object sender, EventArgs e)
        {
            lbl_DeliveryDate.Visible = !true;
            txt_DeliveryDate.Visible = !true;
            btn_Proceed.Visible = !true;
            btn_Cancel.Visible = !true;
            btn_Cal.Visible = !true;

            txt_DeliveryDate.Text = "";
            txt_SalesInvoice.Text = "";
        }

        protected void btn_Check_Click(object sender, EventArgs e)
        {
            

            grid_update1.Visible = true;
            grid_update2.Visible = true;

            Timer4.Enabled = true;
            Timer5.Enabled = true;
        }

        //protected void Timer2_Tick(object sender, EventArgs e)
        //{
        //    string data1 = "";
        //    string data2 = "";
        //    string data3 = "";
        //    string data4 = "";
        //    string data5 = "";
        //    //1creates column
        //    DataTable dt = new DataTable();
        //    dt.Columns.AddRange(new DataColumn[5] { new DataColumn("Mocel Code"), new DataColumn("Color"), new DataColumn("Stocks"), new DataColumn("Back Orders"), new DataColumn("Unit Price") });
        //    ViewState["Ordersas"] = dt;
        //    //2creates column
        //    //1binds the column to the gridview
        //    grid_update1.DataSource = (DataTable)ViewState["Ordersas"];
        //    grid_update1.DataBind();
        //    //2binds the column to the gridview
        //    //1declares a datatable
        //    DataTable dts = (DataTable)ViewState["Ordersas"];
        //    //2decalres a datatable


        //    int cont = grid_PO1.Rows.Count;
        //    for (int x = 0; x <= cont - 1; x++)
        //    {
        //        hpiconn.Open();
        //        string mc = grid_PO1.Rows[x].Cells[1].Text;
        //        string col = grid_PO1.Rows[x].Cells[3].Text;
        //        string se = "select ModelCode, color,quantity,price from SystemModelsTBL where modelcode = '" + mc + "' and color = '" + col + "'";
        //        SqlCommand come = new SqlCommand(se, hpiconn);
        //        IDataReader rr;
        //        rr = come.ExecuteReader();
        //        while (rr.Read())
        //        {
        //            data1 = rr.GetString(0).ToString();//modelcode
        //            data2 = rr.GetString(1).ToString();//color
        //            data3 = rr.GetString(2).ToString();//stocks
        //            int d1 = Convert.ToInt32(grid_PO1.Rows[x].Cells[4].Text);
        //            int d2 = (Convert.ToInt32(data3) - d1);
        //            if (d2 >= 0)
        //            {
        //                data4 = "0";
        //            }
        //            else if (d2 < 0)
        //            {
        //                string d3 = (Convert.ToDouble(d2) * -1).ToString();
        //                data4 = d3;
        //            }
        //            data5 = rr.GetString(3).ToString();
        //            dts.Rows.Add(data1, data2, data3, data4, data5);
        //        }
        //        hpiconn.Close();
        //    }


        //    ViewState["Ordersas"] = dts;
        //    grid_update1.DataSource = (DataTable)ViewState["Ordersas"];
        //    grid_update1.DataBind();
        //}

        //protected void Timer3_Tick(object sender, EventArgs e)
        //{
        //    string data1 = "";
        //    string data2 = "";
        //    string data3 = "";
        //    string data4 = "";
        //    string data5 = "";
        //    //1creates column
        //    DataTable dt = new DataTable();
        //    dt.Columns.AddRange(new DataColumn[5] { new DataColumn("Model Code"), new DataColumn("Color"), new DataColumn("Stocks"), new DataColumn("Back Orders"), new DataColumn("Unit Price") });
        //    ViewState["Ordera"] = dt;
        //    //2creates column
        //    //1binds the column to the gridview
        //    grid_Ups2.DataSource = (DataTable)ViewState["Ordera"];
        //    grid_Ups2.DataBind();
        //    //2binds the column to the gridview
        //    //1declares a datatable
        //    DataTable dts = (DataTable)ViewState["Ordera"];
        //    //2decalres a datatable


        //    int cont = grid_PO2.Rows.Count;
        //    for (int x = 0; x <= cont - 1; x++)
        //    {
        //        hpiconn.Open();
        //        string mc = grid_PO2.Rows[x].Cells[1].Text;
        //        string col = grid_PO2.Rows[x].Cells[3].Text;
        //        string se = "select ModelCode, color,quantity,price from SystemModelsTBL where modelcode = '" + mc + "' and color = '" + col + "'";
        //        SqlCommand come = new SqlCommand(se, hpiconn);
        //        IDataReader rr;
        //        rr = come.ExecuteReader();
        //        while (rr.Read())
        //        {
        //            data1 = rr.GetString(0).ToString();//modelcode
        //            data2 = rr.GetString(1).ToString();//color
        //            data3 = rr.GetString(2).ToString();//stocks
        //            int d1 = Convert.ToInt32(grid_PO2.Rows[x].Cells[4].Text);
        //            int d2 = (Convert.ToInt32(data3) - d1);
        //            if (d2 >= 0)
        //            {
        //                data4 = "0";
        //            }
        //            else if (d2 < 0)
        //            {
        //                string d3 = (Convert.ToDouble(d2) * -1).ToString();
        //                data4 = d3;
        //            }
        //            data5 = rr.GetString(3).ToString();
        //            dts.Rows.Add(data1, data2, data3, data4, data5);
        //        }
        //        hpiconn.Close();
        //    }


        //    ViewState["Ordera"] = dts;
        //    grid_Ups2.DataSource = (DataTable)ViewState["Ordera"];
        //    grid_Ups2.DataBind();
        //}


        protected void btn_Cal_Click(object sender, ImageClickEventArgs e)
        {
            if (cal_Calendar.Visible == true)
            {
                cal_Calendar.Visible = !true;
            }

            else
            {
 cal_Calendar.Visible = true;
            }
            
        }

        protected void cal_Calendar_SelectionChanged(object sender, EventArgs e)
        {
            txt_DeliveryDate.Text = cal_Calendar.SelectedDate.ToLongDateString();
            cal_Calendar.Visible = false;
        }

        protected void cal_Calendar_DayRender(object sender, DayRenderEventArgs e)
        {
            if (e.Day.Date < DateTime.Now.Date)
            {
                e.Day.IsSelectable = false;
                e.Cell.ForeColor = System.Drawing.Color.Red;
                e.Cell.Font.Strikeout = true;
            }
        }

        //protected void Button28_Click(object sender, EventArgs e)
        //{
             

        //}

        protected void btn_Attach_Click(object sender, EventArgs e)
        {
            //Timer4.Enabled = true;
            //Timer5.Enabled = true;
            //Timer2.Enabled = true;
            //Timer3.Enabled = true;
            //string po = txt_POnum.Text;
          
            //hpiconn.Open();
            //string ass1 = "select ModelCode,Description,Color,Quantity , BackOrders from a" + po + "_Units";
            ////Response.Write(ass);
            //SqlDataAdapter adapter1 = new SqlDataAdapter(ass1, hpiconn);

            //DataSet set1 = new DataSet();
            //adapter1.Fill(set1);

            ////grid_PurchaseOrderDetails1.DataSource = null;

            //grid_PO1.DataSource = set1.Tables[0];
            //grid_PO1.DataBind();
            //hpiconn.Close();

          
            //hpiconn.Open();
            //string ass2 = "select ModelCode,Description,Color,Quantity , BackOrders from b" + po + "_Units";
            ////Response.Write(ass);
            //SqlDataAdapter adapter2 = new SqlDataAdapter(ass2, hpiconn);

            //DataSet set2 = new DataSet();
            //adapter2.Fill(set2);

            ////grid_PurchaseOrderDetails1.DataSource = null;

            //grid_PO2.DataSource = set2.Tables[0];
            //grid_PO2.DataBind();
            //hpiconn.Close();
            
        }

        protected void Button31_Click(object sender, EventArgs e)
        {
            MultiView1.ActiveViewIndex = 2;
        }

        protected void Timer2_Tick(object sender, EventArgs e)
        {
            string data1 = "";
            string data2 = "";
            string data3 = "";
            string data4 = "";
            string data5 = "";
            //1creates column
            DataTable dt = new DataTable();
            dt.Columns.AddRange(new DataColumn[5] { new DataColumn("Model Code"), new DataColumn("Color"), new DataColumn("Stocks"), new DataColumn("Back Orders"), new DataColumn("Unit Price") });
            ViewState["Ordersas"] = dt;
            //2creates column
            //1binds the column to the gridview
            grid_update1.DataSource = (DataTable)ViewState["Ordersas"];
            grid_update1.DataBind();
            //2binds the column to the gridview
            //1declares a datatable
            DataTable dts = (DataTable)ViewState["Ordersas"];
            //2decalres a datatable


            int cont = grid_PO1.Rows.Count;
            for (int x = 0; x <= cont - 1; x++)
            {
                hpiconn.Open();
                string mc = grid_PO1.Rows[x].Cells[0].Text;
                string col = grid_PO1.Rows[x].Cells[2].Text;
                string se = "select ModelCode, color,quantity,Initialprice from SystemModelsTBL where modelcode = '" + mc + "' and color = '" + col + "'";
                SqlCommand come = new SqlCommand(se, hpiconn);
                IDataReader rr;
                rr = come.ExecuteReader();
                while (rr.Read())
                {
                    data1 = rr.GetString(0).ToString();//modelcode
                    data2 = rr.GetString(1).ToString();//color
                    data3 = rr.GetString(2).ToString();//stocks
                    
                    int d1 = Convert.ToInt32(grid_PO1.Rows[x].Cells[3].Text);
                    int d2 = (Convert.ToInt32(data3) - d1);
                    if (d2 >= 0)
                    {
                        data4 = "0";
                    }
                    else if (d2 < 0)
                    {
                        string d3 = (Convert.ToDouble(d2) * -1).ToString();
                        data4 = d3;
                    }
                    data5 = rr.GetString(3).ToString();//price
                    dts.Rows.Add(data1, data2, data3, data4, data5);
                }
                hpiconn.Close();
            }


            ViewState["Ordersas"] = dts;
            grid_update1.DataSource = (DataTable)ViewState["Ordersas"];
            grid_update1.DataBind();
        }

        protected void Timer3_Tick(object sender, EventArgs e)
        {
            string data1 = "";
            string data2 = "";
            string data3 = "";
            string data4 = "";
            string data5 = "";
            string subdata3 = "";
            string sdata3 = "";
           // int jk = 0;
            //1creates column
            DataTable dt = new DataTable();
            dt.Columns.AddRange(new DataColumn[5] { new DataColumn("Model Code"), new DataColumn("Color"), new DataColumn("Stocks"), new DataColumn("Back Orders"), new DataColumn("Unit Price") });
            ViewState["Ordera"] = dt;
            //2creates column
            //1binds the column to the gridview
            grid_update2.DataSource = (DataTable)ViewState["Ordera"];
            grid_update2.DataBind();
            //2binds the column to the gridview
            //1declares a datatable
            DataTable dts = (DataTable)ViewState["Ordera"];
            //2decalres a datatable
            string mc = "";
            string col = "";
            bool yes = false;
            int anse = 0;
            int cont = grid_PO2.Rows.Count;
            int d1 = 0;
            int d2 = 0;
           // int[] ars = new int[150];
            for (int x = 0; x <= cont - 1; x++)
            {
                hpiconn.Open();
                mc = grid_PO2.Rows[x].Cells[0].Text;
                col = grid_PO2.Rows[x].Cells[2].Text;
                string se = "select ModelCode, color,quantity,initialprice from SystemModelsTBL where modelcode = '" + mc + "' and color = '" + col + "'";
                SqlCommand come = new SqlCommand(se, hpiconn);
                IDataReader rr;
                rr = come.ExecuteReader();
                while (rr.Read())
                {
                    data1 = rr.GetString(0).ToString();//modelcode
                    data2 = rr.GetString(1).ToString();//color
                    data3 = rr.GetString(2).ToString();//stocks
                        d1 = Convert.ToInt32(grid_PO2.Rows[x].Cells[3].Text);
                        d2 = (Convert.ToInt32(data3) - d1);
                        if (d2 >= 0)
                        {
                            data4 = "0";
                        }
                        else if (d2 < 0)
                        {
                            string d3 = (Convert.ToDouble(d2) * -1).ToString();
                            data4 = d3;
                        }

                        data5 = rr.GetString(3).ToString();//price
                        dts.Rows.Add(data1, data2, data3, data4, data5);
                }
                hpiconn.Close();
            }
            ViewState["Ordera"] = dts;
            grid_update2.DataSource = (DataTable)ViewState["Ordera"];
            grid_update2.DataBind();
        }

        protected void Button30_Click(object sender, EventArgs e)
        {

        }

        protected void btn_Compute_Click(object sender, EventArgs e)
        {
             double finalanse =0 ;
             double pfinalanse = 0; double fin = 0;
            // Response.Write(grid_PO2.Rows.Count);
            //  int gv = grid_PO1.Rows[0].Cells.Count
            if (grid_PO2.Rows.Count == 0)
            {
                //1totalcharge
                double totnum = 0;
                int x = grid_PO1.Rows.Count;
                for (int a = 0; a <= x - 1; a++)
                {
                    if (grid_PO1.Rows[a].Cells[5].Text != "N/A")
                    {
                        double num = Convert.ToInt32(grid_PO1.Rows[a].Cells[4 + 1].Text);
                        totnum = totnum + num;
                    }
                }
                fin = totnum * Convert.ToDouble(txt_ServiceCharge.Text);
                string q = Convert.ToDecimal(fin).ToString("#,##0.00");
                txt_TotalCharge.Text = q.ToString();
                //2totalcharge
            }
            else
            {
                //1totalcharge
                double totnum = 0;
                double totnum1 = 0;
                int x = grid_PO1.Rows.Count;
                for (int a = 0; a <= x - 1; a++)
                {
                    if (grid_PO1.Rows[a].Cells[5].Text != "N/A")
                    {
                        try
                        {
                            double num = Convert.ToInt32(grid_PO1.Rows[a].Cells[5].Text);
                            totnum = totnum + num;
                            if (grid_PO2.Rows[a].Cells[5].Text != "N/A")
                            {
                                double num1 = Convert.ToInt32(grid_PO2.Rows[a].Cells[5].Text);
                                totnum1 = totnum1 + num1;
                            }
                        }
                        catch
                        {
 
                        }
                    }
                }

                fin = (totnum + totnum1) * Convert.ToDouble(txt_ServiceCharge.Text);
                string q = Convert.ToDecimal(fin).ToString("#,##0.00");
                txt_TotalCharge.Text = q.ToString();
                //2totalcharge
            }
            //sub-total

            int g = grid_PO1.Rows.Count;
            
            double[] arr = new double[g];
            double[] disc = new double[g];
            
            for (int x = 0; x <= g - 1; x++)
            {
                if (grid_PO1.Rows[x].Cells[5].Text != "N/A")
                {
                    string a1 = grid_PO1.Rows[x].Cells[0].Text;
                    string a2 = grid_PO1.Rows[x].Cells[2].Text;
                    double a3 = Convert.ToDouble(grid_PO1.Rows[x].Cells[5].Text);

                    string wery = "select initialprice from systemmodelstbl where modelcode = '" + a1 + "' and color = '" + a2 + "'";
                    SqlCommand mm = new SqlCommand(wery, hpiconn);
                    // SqlCommand com = new SqlCommand(query, conn);
                    SqlDataReader r;
                    hpiconn.Open();
                    r = mm.ExecuteReader();

                    r.Read();
                    double price = Convert.ToDouble(r.GetString(0));
                    hpiconn.Close();
                    double ans = price * a3;

                    arr[x] = ans;

                    //for discounted ammount
                    hpiconn.Open();
                    string discountgetter = "Select DiscountQuantity ,percentage from SystemDiscountsTBL where category = 'Units'";
                    SqlCommand comander = new SqlCommand(discountgetter, hpiconn);
                    IDataReader rader;
                    rader = comander.ExecuteReader();


                    rader.Read();
                    string a = rader.GetString(0).ToString();
                    string b = rader.GetString(1).ToString();// a = ito ung "unit" per percentage
                    hpiconn.Close();
                    if (Convert.ToDouble(a) <= a3)//kapag positive number ang dis and nadivide sa by 10 ang inorder or more than ten ang inorder
                    {
                        // double dis = a3 / Convert.ToDouble(a);//dis = kung ilang 10 meron sa a3(quantity na inorder)
                        double dis = a3 % Convert.ToDouble(a);// mod = kung ilan ang natira sa a3 kung kukuhaan ng by 10
                        double dis1 = a3 - dis;
                        double mod = dis * price;
                        // double dis2 =  Convert.ToDouble(a);

                        // double per = dis2 * Convert.ToDouble(a);//per = ito ung percentage na ibabawas every <number> in my case, 10 units 
                        //hpiconn.Open();
                        //string priceGetter = "Select price from SystemModelsTBL where modelcode = '" + a1 + "' and color = '" + a2 + "'";
                        //SqlCommand gets = new SqlCommand(priceGetter, hpiconn);
                        //IDataReader der;
                        //der = gets.ExecuteReader();


                        //der.Read();
                        //double initprice = Convert.ToDouble(der.GetString(0));// price = ito ung price ng isang unit
                        //hpiconn.Close();
                        double tots = price * dis1;
                        double anse = tots * Convert.ToDouble("0." + Convert.ToDouble(b));
                        pfinalanse = tots - anse;
                        finalanse = pfinalanse + mod;
                       // Response.Write(a + "a <br/>");
                       // Response.Write(a3 + "a3 <br/>");
                       // Response.Write(dis + "dis <br/>");
                       // Response.Write(dis1 + "dis1 <br/>");
                       // Response.Write(mod + " mod <br/>");
                       //// Response.Write(dis2 + " dis2 <br/>");
                       // Response.Write(tots + " tots <br/>");
                       // Response.Write(anse + " anse <br/>");
                       // Response.Write(finalanse + " ff <br/><br/>");

                    }
                    else
                    {
                        finalanse = price * a3;
                      //  Response.Write(finalanse + " <br/>");
                    }//Response.Write(dis + " ito ung kung ilan ang 10 sa loob ng unorder" + "<br/>");

                    //for discounted ammount
                disc[x] = finalanse;
                }

               


            }



            double final = 0;
            //int sa = -1;
            foreach (double answer in arr)
            {
              //  sa++;
                final += answer;
                //Response.Write(arr[sa] + "<br/>");
            }
            //    txt_Total.Text = final.ToString();

            double discount = 0;
            double discounts = 0;
            //int sd = -1;
            foreach (double discs in disc)
            {
              //  sd++;
                discount += discs;
                //Response.Write(disc[sd] + "<br/><br/>");
            }
            //Response.Write(sd1 + "<br/><br/>");
            
            int g1 = grid_PO2.Rows.Count;
            double[] arr1 = new double[g1];
            int gs = grid_PO2.Rows.Count;
            double[] discsb = new double[gs];
            if(!(g1<=0))
            {
                for (int x = 0; x <= g1 - 1; x++)
                {
                    if (grid_PO2.Rows[x].Cells[5].Text != "N/A")
                    {
                        string a1 = grid_PO2.Rows[x].Cells[0].Text;
                        string a2 = grid_PO2.Rows[x].Cells[2].Text;
                        double a3 = Convert.ToDouble(grid_PO2.Rows[x].Cells[5].Text);

                        string wery = "select initialprice from systemmodelstbl where modelcode = '" + a1 + "' and color = '" + a2 + "'";
                        SqlCommand mm = new SqlCommand(wery, hpiconn);
                        // SqlCommand com = new SqlCommand(query, conn);
                        SqlDataReader r;
                        hpiconn.Open();
                        r = mm.ExecuteReader();

                        r.Read();
                        double price = Convert.ToDouble(r.GetString(0));
                        hpiconn.Close();
                        double ans = price * a3;


                        arr1[x] = ans;

                        //for discounted ammount
                        hpiconn.Open();
                        string discountgetter = "Select DiscountQuantity, percentage from SystemDiscountsTBL where category = 'Units'";
                        SqlCommand comander = new SqlCommand(discountgetter, hpiconn);
                        IDataReader rader;
                        rader = comander.ExecuteReader();


                        rader.Read();
                        string a = rader.GetString(0).ToString();
                        string b = rader.GetString(1).ToString();// a = ito ung "unit" per percentage
                        hpiconn.Close();
                        if (Convert.ToDouble(a) <= a3)//kapag positive number ang dis and nadivide sa by 10 ang inorder or more than ten ang inorder
                        {
                            // double dis = a3 / Convert.ToDouble(a);//dis = kung ilang 10 meron sa a3(quantity na inorder)
                            double dis = a3 % Convert.ToDouble(a);// mod = kung ilan ang natira sa a3 kung kukuhaan ng by 10
                            double dis1 = a3 - dis;
                            double mod = dis * price;
                            // double dis2 =  Convert.ToDouble(a);

                            // double per = dis2 * Convert.ToDouble(a);//per = ito ung percentage na ibabawas every <number> in my case, 10 units 
                            //hpiconn.Open();
                            //string priceGetter = "Select price from SystemModelsTBL where modelcode = '" + a1 + "' and color = '" + a2 + "'";
                            //SqlCommand gets = new SqlCommand(priceGetter, hpiconn);
                            //IDataReader der;
                            //der = gets.ExecuteReader();


                            //der.Read();
                            //double initprice = Convert.ToDouble(der.GetString(0));// price = ito ung price ng isang unit
                            //hpiconn.Close();
                            double tots = price * dis1;
                            double anse = tots * Convert.ToDouble("0." + Convert.ToDouble(b));
                            pfinalanse = tots - anse;
                            finalanse = pfinalanse + mod;
                           // Response.Write(a + "a <br/>");
                           // Response.Write(a3 + "a3 <br/>");
                           // Response.Write(dis + "dis <br/>");
                           // Response.Write(dis1 + "dis1 <br/>");
                           // Response.Write(mod + " mod <br/>");
                           //// Response.Write(dis2 + " dis2 <br/>");
                           // Response.Write(tots + " tots <br/>");
                           // Response.Write(anse + " anse <br/>");
                          //  Response.Write(finalanse + " ff <br/><br/>");

                        }
                        else
                        {
                            finalanse = price * a3;
                           // Response.Write(finalanse + " <br/>");
                        }//Response.Write(dis + " ito ung kung ilan ang 10 sa loob ng unorder" + "<br/>");

                        discsb[x] = finalanse;

                    }
                    
                }
                //int sa1 = -1;
                foreach (double answer in arr1)
                {
                    //sa1++;
                    final += answer;
                  //  Response.Write(arr1[sa1] + "<br/>");
                    
                }
                
                //int sd1 = -1;
                foreach (double discs1 in discsb)
                {
                //    sd1++;
                    discounts += discs1;
                     //Response.Write(discs[sd1] + "<br/><br/>");
                }
                
             

            }
                
            
          

            //double final1 = 0;

            double fdiscount = discount + discounts;
            //double anss = final;// +final1;
            string qt = Convert.ToDecimal(final).ToString("#,##0.00");
            txt_Total.Text = qt.ToString();

            string qt1 = Convert.ToDecimal(fdiscount).ToString("#,##0.00");
            txt_DiscountedAmount.Text = qt1.ToString();
            //sub-total
            //vat
            double vat1 = fdiscount * Convert.ToDouble("0."+Convert.ToDouble(lbl_VAT.Text.Replace("%","")));
            double fvat = discount + vat1;
             //vat
            string qt11 = Convert.ToDecimal(vat1).ToString("#,##0.00");
            txt_Vats.Text = qt11.ToString();

            //total
            double total = fdiscount + vat1 + fin;
            //total
            string qt111 = Convert.ToDecimal(total).ToString("#,##0.00");
            txt_GrandTotal.Text = qt111.ToString();
            ////////////////////////////////////////////////////////////////////////////////////////
            //percentage calculator
            int bilang = disc.Count();

            double[] percents = new double[bilang];
            for(int fvck = 0; fvck <= bilang - 1 ; fvck++)
            {
                double shit1 = disc[fvck];
                double shit2 = arr[fvck];
                if (shit1 != shit2)
                {
                    double damn = shit2 - shit1;
                    double omg = damn / shit2;
                    double yahoo = omg * 100.00;
                    //decimal value = Convert.ToDecimal(yahoo);
                    //value = Math.Round(value);
                    percents[fvck] = /*Convert.ToDouble(value)*/yahoo;
                }
                else
                {
                    percents[fvck] = 0;
                }
            }
            //b
            int bilangs = discsb.Count();
            double[] percentsb = new double[bilangs];
            try
            {
                for (int fvck = 0; fvck <= bilangs - 1; fvck++)
                {
                    double shit1 = discsb[fvck];//discounted
                    double shit2 = arr1[fvck];//undiscounted
                    if (shit1 != shit2)
                    {
                        double damn = shit2 - shit1;
                        double omg = damn / shit2;
                        double yahoo = omg * 100.00;

                        ////string text = "19500.52352941176471";
                        //decimal value = Convert.ToDecimal(yahoo);
                        ////decimal.TryParse(text, out value);
                        //value = Math.Round(value);
                        ////text = value.ToString();

                        percentsb[fvck] = /*Convert.ToDouble(value)*/yahoo;
                    }
                    else
                    {
                        percentsb[fvck] = 0;
                    }
                }
            }
            catch
            {
 
            }
            //b
            //percentage calculator
            //invoice table creator
            Random srnd = new Random();
            int snum1 = srnd.Next(1000, 9999);
            int snum2 = srnd.Next(1000, 9999);

            var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            var random = new Random();
            var list = Enumerable.Repeat(0, 2).Select(x => chars[random.Next(chars.Length)]);
            string v = string.Join("", list);

            string sfinal = v + snum1.ToString() + snum2.ToString();
            txt_SalesInvoice.Text = sfinal;

            //string inv = txt_SalesInvoice.Text;
            if (sfinal != "" || sfinal != null)
            {
                string asa = "create table [a" + sfinal + "_Units] ([id] [int] identity (1,1) not null, [s_number] [varchar] (50), [ModelCode] [varchar] (100), [Description] [varchar] (100), [Color] [varchar] (100),[Quantity] [varchar] (100), [BackOrders] [varchar] (100),[UnitPrice] [varchar] (100),[Discount] [varchar] (100), [Amount] [varchar] (100), [Purchasable] [varchar] (100))";

                SqlCommand ccs = new SqlCommand(asa, recsconn);
                recsconn.Open();
                ccs.ExecuteNonQuery();
                recsconn.Close();
                int ge1 = grid_PO2.Rows.Count;
                if (!(ge1 <= 0))
                {
                    string asa1 = "create table [b" + sfinal + "_Units] ([id] [int] identity (1,1) not null, [s_number] [varchar] (50), [ModelCode] [varchar] (100), [Description] [varchar] (100), [Color] [varchar] (100),[Quantity] [varchar] (100), [BackOrders] [varchar] (100),[UnitPrice] [varchar] (100),[Discount] [varchar] (100), [Amount] [varchar] (100), [Purchasable] [varchar] (100))";

                    SqlCommand ccs1 = new SqlCommand(asa1, recsconn);
                    recsconn.Open();
                    ccs1.ExecuteNonQuery();
                    recsconn.Close();
                }
            }
            //invoice table creator
            //insert percentagediscount ,amount into invoice table
            int lops = bilang;
            recsconn.Open();
            for (int hj = 0; hj <= lops - 1; hj++)
            {
                int sn = hj + 1;
                double uncount = percents[hj];
                //string idk = "insert into a" + sfinal + "_units (s_number,Discount)values('"+sn+"','"+uncount+"%')";
                //SqlCommand jjj = new SqlCommand(idk,recsconn);
                //jjj.ExecuteNonQuery();
                //amount
               // double a = uncount * 100;
                double m = disc[hj];
                //double o = arr[hj] - m;
                //amount
                string kk = Convert.ToDecimal(m).ToString("#,##0.00");

                string idk = "insert into a" + sfinal + "_units (s_number,Discount,Amount)values('" + sn + "','" + uncount + "%','"+kk+"')";
                SqlCommand jjj = new SqlCommand(idk, recsconn);
                jjj.ExecuteNonQuery();
            }
            recsconn.Close();

            try
            {
                int loops = bilangs;
                recsconn.Open();
                for (int hj = 0; hj <= loops - 1; hj++)
                {
                    int sn = hj + 1;
                    double uncount = percentsb[hj];
                    double m = discsb[hj];
                    string kk = Convert.ToDecimal(m).ToString("#,##0.00");
                    string idk = "insert into b" + sfinal + "_units (s_number,Discount,Amount)values('" + sn + "','" + uncount + "%','" + kk + "')";
                    SqlCommand jjj = new SqlCommand(idk, recsconn);
                    jjj.ExecuteNonQuery();
                }
                recsconn.Close();
            }
            catch { }
            //insert percentagediscount ,amount into invoice table
            btn_CreateSI.Visible = !false;
            btn_CancelProcess.Visible = !false;
            ViewState["invoice"] = sfinal;


        }

        protected void Timer4_Tick(object sender, EventArgs e)
        {
            string po = txt_POnum.Text;
            int c = grid_update1.Rows.Count;
            //  int c2 = 
            for (int a = 0; a <= c - 1; a++)
            {
                string mcs = grid_update1.Rows[a].Cells[0].Text;
                string cs = grid_update1.Rows[a].Cells[1].Text;
                string bo = grid_update1.Rows[a].Cells[3].Text;
                string query = "update a" + po + "_Units set backorders = '" + bo + "' where modelCode = '" + mcs + "' and color = '" + cs + "'";
                SqlCommand com = new SqlCommand(query, hpiconn);
                hpiconn.Open();
                com.ExecuteNonQuery();
                hpiconn.Close();
            }

            //N/A indicaotr
            int a2 = grid_PO1.Rows.Count;
            for (int ax = 0; ax <= a2 - 1; ax++)
            {
                hpiconn.Open();
                string mc = grid_PO1.Rows[ax].Cells[0].Text;
                string c1 = grid_PO1.Rows[ax].Cells[2].Text;
                SqlCommand check_User_Name = new SqlCommand("SELECT * FROM SystemmodelsTBl WHERE (modelcode = '" + mc + "') and (color = '" + c1 + "')", hpiconn);
                //check_User_Name.Parameters.AddWithValue("@user", txtBox_UserName.Text);
                SqlDataReader reader = check_User_Name.ExecuteReader();

                if (reader.HasRows == false)
                {
                    hpiconn.Close();
                    //User Exists
                    string query = "update a" + po + "_Units set backorders = 'N/A' where modelCode = '" + mc + "' and color = '" + c1 + "'";
                    SqlCommand com = new SqlCommand(query, hpiconn);
                    hpiconn.Open();
                    com.ExecuteNonQuery();
                    hpiconn.Close();
                }
                hpiconn.Close();
                //else
                //{
                //    //User NOT Exists
                //}

            }
            //N/A indicaotr
            hpiconn.Open();
            string ass1 = "select ModelCode,Description,Color,Quantity , BackOrders ,Available as Purchasable from a" + po + "_Units";
            //Response.Write(ass);
            SqlDataAdapter adapter1 = new SqlDataAdapter(ass1, hpiconn);

            DataSet set1 = new DataSet();
            adapter1.Fill(set1);

            //grid_PurchaseOrderDetails1.DataSource = null;

            grid_PO1.DataSource = set1.Tables[0];
            grid_PO1.DataBind();
            hpiconn.Close();

            //avilable viewer
            int j = grid_PO1.Rows.Count;
            for (int er = 0; er <= j - 1; er++)
            {
                string mc = grid_PO1.Rows[er].Cells[0].Text;
                string c1 = grid_PO1.Rows[er].Cells[2].Text;

                try
                {
                    int o = Convert.ToInt32(grid_PO1.Rows[er].Cells[3].Text);
                    string jhe = grid_PO1.Rows[er].Cells[4].Text;
                    int m = 0;//onvert.ToInt32(grid_PO1.Rows[er].Cells[4].Text);
                    if (jhe == "N/A")
                    {
                        string ea = "update a" + po + "_Units set available = 'N/A' where modelCode = '" + mc + "' and color = '" + c1 + "'";
                        SqlCommand je = new SqlCommand(ea, hpiconn);
                        hpiconn.Open();
                        je.ExecuteNonQuery();
                        hpiconn.Close();
                    }
                    else if (jhe != "N/A")
                    {
                        m = Convert.ToInt32(grid_PO1.Rows[er].Cells[4].Text);
                        if (m == 0)
                        {

                            string ea = "update a" + po + "_Units set available = '" + o + "' where modelCode = '" + mc + "' and color = '" + c1 + "'";
                            SqlCommand je = new SqlCommand(ea, hpiconn);
                            hpiconn.Open();
                            je.ExecuteNonQuery();
                            hpiconn.Close();
                        }
                        else
                        {
                            int ans = o - m;
                            string ea = "update a" + po + "_Units set available = '" + ans + "' where modelCode = '" + mc + "' and color = '" + c1 + "'";
                            SqlCommand je = new SqlCommand(ea, hpiconn);
                            hpiconn.Open();
                            je.ExecuteNonQuery();
                            hpiconn.Close();
                        }
                    }
                }
                catch { }
            }
                //ViewState["data"] = null;

                    //avilable viewer

                //int row = grid_PO1.Rows.Count;
                //for (int f = 0; f <= row - 1; f++)
                //{
                //    string mc1 = grid_PO1.Rows[f].Cells[0].Text;
                //    string colr = grid_PO1.Rows[f].Cells[2].Text;

                //    int re = grid_PO2.Rows.Count;
                //    for (int we = 0; we <= re - 1; we++)
                //    {
                //        ViewState["we"] = we;
                //        string mc11 = grid_PO2.Rows[Convert.ToInt32(ViewState["we"])].Cells[0].Text;
                //        string colr1 = grid_PO2.Rows[Convert.ToInt32(ViewState["we"])].Cells[2].Text;
                //        if (mc1 == mc11)
                //        {
                //            if (colr == colr1)
                //            {
                //                int ds = grid_PO2.Rows[0].Cells.Count;
                //                if (ds != 3)
                //                {
                //                    try
                //                    {
                //                        string tre = grid_PO1.Rows[Convert.ToInt32(ViewState["we"])].Cells[5].Text;
                //                        string num = grid_update1.Rows[f].Cells[2].Text;
                //                        int anse = (Convert.ToInt32(num) - Convert.ToInt32(tre));

                //                        //ViewState["data"] = anse.ToString();//test.Text = ViewState["data"].ToString();
                //                        //ViewState["we"] = we;

                                        
                //                    }
                //                    catch { }
                //                    break;

                //                }
                //            }
                //        }
                //    }
                //}

                //string tre = "";
                //string num = "";
                //int anse = 0;
                //int row = grid_PO1.Rows.Count;
                //for (int f = 0; f <= row - 1; f++)
                //{
                //    string mc1 = grid_PO1.Rows[f].Cells[0].Text;
                //    string colr = grid_PO1.Rows[f].Cells[2].Text;

                //    string sele = "select modelcode ,color from b" + po + "_UNits where modelcode = '" + mc1 + "' and color = '" + colr + "'";
                //    SqlCommand sql = new SqlCommand(sele, hpiconn);
                //    SqlDataReader fs;
                //    hpiconn.Open();
                //    fs = sql.ExecuteReader();
                //    if (fs.HasRows)
                //    {
                //        tre = grid_PO1.Rows[f].Cells[5].Text;


                //        num = grid_update1.Rows[f].Cells[2].Text;
                //        anse = (Convert.ToInt32(num) - Convert.ToInt32(tre));
                //        break;
                //    }
                //    hpiconn.Close();
                //}
                //   // int Index = ((GridViewRow)((sender as Control)).NamingContainer).RowIndex;
                //    ViewState["data"] = anse.ToString();

                    //int re = grid_PO2.Rows.Count;
                    //for (int we = 0; we <= re - 1; we++)
                    //{
                    //    string mc11 = grid_PO2.Rows[we].Cells[0].Text;
                    //    string colr1 = grid_PO2.Rows[we].Cells[2].Text;
                    //    if (mc1 == mc11)
                    //    {
                    //        if (colr == colr1)
                    //        {
                    //            int ds = grid_PO2.Rows[0].Cells.Count;
                    //            if (ds != 3)
                    //            {
                    //                try
                    //                {
                    //                    string tre = grid_PO1.Rows[we].Cells[5].Text;
                    //                    string num = grid_update1.Rows[f].Cells[2].Text;
                    //                    int anse = (Convert.ToInt32(num) - Convert.ToInt32(tre));

                    //                    ViewState["data"] = anse.ToString();//test.Text = ViewState["data"].ToString();
                    //                    //ViewState["we"] = we;
                    //                }
                    //                catch { }
                    //                break;

                    //            }
                    //        }
                    //    }
                    //}
                //}
            
        }

        protected void Timer5_Tick(object sender, EventArgs e)
        {
            try
            {
                string po = txt_POnum.Text;
                int c1 = grid_update2.Rows.Count;
                for (int a = 0; a <= c1 - 1; a++)
                {
                    string mcs = grid_update2.Rows[a].Cells[0].Text;
                    string cs = grid_update2.Rows[a].Cells[1].Text;
                    string bo = grid_update2.Rows[a].Cells[3].Text;
                    string query = "update b" + po + "_Units set backorders = '" + bo + "' where modelCode = '" + mcs + "' and color = '" + cs + "'";
                    SqlCommand com = new SqlCommand(query, hpiconn);
                    hpiconn.Open();
                    com.ExecuteNonQuery();
                    hpiconn.Close();
                }
                //N/A indicaotr
                int a2 = grid_PO2.Rows.Count;
                for (int ax = 0; ax <= a2 - 1; ax++)
                {
                    hpiconn.Open();
                    string mc = grid_PO2.Rows[ax].Cells[0].Text;
                    string c1a = grid_PO2.Rows[ax].Cells[2].Text;
                    SqlCommand check_User_Name = new SqlCommand("SELECT * FROM SystemmodelsTBl WHERE (modelcode = '" + mc + "') and (color = '" + c1a + "')", hpiconn);
                    //check_User_Name.Parameters.AddWithValue("@user", txtBox_UserName.Text);
                    SqlDataReader reader = check_User_Name.ExecuteReader();

                    if (reader.HasRows == false)
                    {
                        hpiconn.Close();
                        //User Exists
                        string query = "update b" + po + "_Units set backorders = 'N/A' where modelCode = '" + mc + "' and color = '" + c1a + "'";
                        SqlCommand com = new SqlCommand(query, hpiconn);
                        hpiconn.Open();
                        com.ExecuteNonQuery();
                        hpiconn.Close();
                    }
                    hpiconn.Close();
                    //else
                    //{
                    //    //User NOT Exists
                    //}

                }
                //N/A indicaotr
                hpiconn.Open();
                string ass2 = "select ModelCode,Description,Color,Quantity , BackOrders,Available as Purchasable from b" + po + "_Units";
                //Response.Write(ass);
                SqlDataAdapter adapter2 = new SqlDataAdapter(ass2, hpiconn);

                DataSet set2 = new DataSet();
                adapter2.Fill(set2);

                //grid_PurchaseOrderDetails1.DataSource = null;

                grid_PO2.DataSource = set2.Tables[0];
                grid_PO2.DataBind();
                hpiconn.Close();

                //avilable viewer
                int j = grid_PO2.Rows.Count;
                for (int er = 0; er <= j - 1; er++)
                {
                    string mc = grid_PO2.Rows[er].Cells[0].Text;
                    string c1a = grid_PO2.Rows[er].Cells[2].Text;


                    try
                    {
                        int o = Convert.ToInt32(grid_PO2.Rows[er].Cells[3].Text);
                        string jhe = grid_PO2.Rows[er].Cells[4].Text;
                        int m = 0; //Convert.ToInt32(grid_PO2.Rows[er].Cells[4].Text);
                        if (jhe == "N/A")
                        {
                            string ea = "update b" + po + "_Units set available = 'N/A' where modelCode = '" + mc + "' and color = '" + c1a + "'";
                            SqlCommand je = new SqlCommand(ea, hpiconn);
                            hpiconn.Open();
                            je.ExecuteNonQuery();
                            hpiconn.Close();
                        }
                        else if (jhe != "N/A")
                        {
                            m = Convert.ToInt32(grid_PO2.Rows[er].Cells[4].Text);
                            if (m == 0)
                            {

                                string ea = "update b" + po + "_Units set available = '" + o + "' where modelCode = '" + mc + "' and color = '" + c1a + "'";
                                SqlCommand je = new SqlCommand(ea, hpiconn);
                                hpiconn.Open();
                                je.ExecuteNonQuery();
                                hpiconn.Close();
                            }
                            else
                            {
                                int ans = o - m;
                                string ea = "update b" + po + "_Units set available = '" + ans + "' where modelCode = '" + mc + "' and color = '" + c1a + "'";
                                SqlCommand je = new SqlCommand(ea, hpiconn);
                                hpiconn.Open();
                                je.ExecuteNonQuery();
                                hpiconn.Close();
                            }
                        }
                    }
                    catch { }
                }

                //avilable viewer
            }
            catch
            {
 
            }
        }

        protected void Timer6_Tick(object sender, EventArgs e)
        {
            hpiconn.Open();
            string discountgetter = "Select Percentage, DiscountQuantity from SystemDiscountsTBL where category = 'Units'";
            SqlCommand comander = new SqlCommand(discountgetter, hpiconn);
            IDataReader rader;
            rader = comander.ExecuteReader();


            rader.Read();
            string a = rader.GetString(0) + "%";
            string b = rader.GetString(1);
            hpiconn.Close();

            txt_Discount.Text = a + " per " + b + " same models.";
        }

        protected void Timer7_Tick(object sender, EventArgs e)
        {
            string vat = "select vat from systemchargesTBL where chargenumber = 1";
            hpiconn.Open();
            SqlCommand cam = new SqlCommand(vat, hpiconn);
            SqlDataReader red;
            red = cam.ExecuteReader();
            red.Read();
            lbl_VAT.Text = red.GetString(0) + "%";
            hpiconn.Close();
        }

        //protected void Button26_Click(object sender, EventArgs e)
        //{
        //    MultiView1.ActiveViewIndex = 3;
           
        //}

        protected void btn_SIReport_Click(object sender, EventArgs e)
        {
            MultiView5.ActiveViewIndex = 1;
            MultiView6.ActiveViewIndex = 0;
        }

        protected void btn_Proceed_Click(object sender, EventArgs e)
        {
            string inv = txt_SalesInvoice.Text;
            string refnum = txt_POnum.Text;
            bool tell = false;

            int nums = grid_PO1.Rows.Count + grid_PO2.Rows.Count;
            string[] bos = new string[nums];
            for (int lop = 0; lop <= nums - 1; lop++)
            {
                try
                {
                    string geta = grid_PO1.Rows[lop].Cells[4].Text;
                    bos[lop] = geta;
                }
                catch
                {
                    try
                    {
                        string geta = grid_PO2.Rows[lop - grid_PO1.Rows.Count].Cells[4].Text;
                        bos[lop] = geta;
                    }
                    catch
                    {

                    }
                }
            }



            for (int check = 0; check <= nums - 1; check++)
            {
                string data = bos[check];
                if (data != "0" && data != "N/A")
                {
                    hpiconn.Open();
                    string apdate = "update systempounitstbl set remarks = 'Back Orders' where purchaseordernumber = '" + refnum + "'";
                    SqlCommand border = new SqlCommand(apdate, hpiconn);
                    border.ExecuteNonQuery();
                    hpiconn.Close();
                    tell = true;
                    break;
                }
            }
            //hpiconn.Open();
            //string rem = "select remarks from systempounitstbl where purchaseordernumber = '"+refnum+"'";
            //SqlCommand cms = new SqlCommand(rem,hpiconn);
            //SqlDataReader ff;
            //ff = cms.ExecuteReader();
            //ff.Read();
            //string stat = ff.GetString(0).ToString();
            //hpiconn.Close();


            //string stat = "";
            //int numbr = grid_PO1.Rows.Count;
            //for (int d = 1; d <= numbr - 1; d++)
            //{
            //    stat = grid_PO1.Rows[d].Cells[4].Text;
            //    if (stat == "N/A")
            //        break;
            //}
            //try
            //{
            //    int numbrs = grid_PO2.Rows.Count;
            //    for (int d = 1; d <= numbrs - 1; d++)
            //    {
            //        stat = grid_PO2.Rows[d].Cells[4].Text;
            //        if (stat == "N/A")
            //            break;
            //    }
            //}
            //catch { }
            if (tell == false)
            {
                hpiconn.Open();
                string apdate = "update systempounitstbl set remarks = 'Purchased' where purchaseordernumber = '" + refnum + "'";
                SqlCommand border = new SqlCommand(apdate, hpiconn);
                border.ExecuteNonQuery();
                hpiconn.Close();
            }


            //Backorder indicator(remarks)
            //int row1 = grid_PO1.Rows.Count;
            //for (int gg = 0 ; gg <= row1-1; gg++)
            //{
            //    string bo = grid_PO1.Rows[gg].Cells[3].Text;
            //    if (bo != "0")
            //    {
            //        hpiconn.Open();
            //        string apdate = "update systempounitstbl set remarks = 'Back Orders' where purchaseordernumber = '" + refnum + "'";
            //        SqlCommand border = new SqlCommand(apdate, hpiconn);
            //        border.ExecuteNonQuery();
            //        hpiconn.Close();
            //    }
            //    else if (bo == "0")
            //    {
            //        hpiconn.Open();
            //        string apdate = "update systempounitstbl set remarks = 'Purchased' where purchaseordernumber = '" + refnum + "'";
            //        SqlCommand border = new SqlCommand(apdate, hpiconn);
            //        border.ExecuteNonQuery();
            //        hpiconn.Close();
            //    }
            //}
            //try
            //{
            //    int row2 = grid_PO2.Rows.Count;
            //    for (int gg = 0; gg <= row2 - 1; gg++)
            //    {
            //        string bo = grid_PO2.Rows[gg].Cells[3].Text;
            //        if (bo != "0")
            //        {
            //            hpiconn.Open();
            //            string apdate = "update systempounitstbl set remarks = 'Back Orders' where purchaseordernumber = '" + refnum + "'";
            //            SqlCommand border = new SqlCommand(apdate, hpiconn);
            //            border.ExecuteNonQuery();
            //            hpiconn.Close();
            //        }
            //        else if (bo == "0")
            //        {
            //            hpiconn.Open();
            //            string apdate = "update systempounitstbl set remarks = 'Purchased' where purchaseordernumber = '" + refnum + "'";
            //            SqlCommand border = new SqlCommand(apdate, hpiconn);
            //            border.ExecuteNonQuery();
            //            hpiconn.Close();
            //        }
            //    }
            //}
            //catch
            //{

            //}
            //Backorder Indicator(remarks)
            //input data to column remainingquantity
            //hpiconn.Open();
            //string ins = "insert into a" + refnum + "_Units (remainingquantity)values()";
            //SqlCommand comm = new SqlCommand(ins, hpiconn);
            //char refID = refnum[0];
            //string cat = "";
            //if (refID.ToString() == "0")
            //{
            //    cat = "Unit";
            //}
            //else if (refID.ToString() == "1")
            //{
            //    cat = "Part";
            //}
            //comm.Parameters.AddWithValue("@cat", cat);
            //comm.Parameters.AddWithValue("@pon", txt_POnum.Text);
            //comm.Parameters.AddWithValue("@sin", txt_SalesInvoice.Text);
            //comm.Parameters.AddWithValue("@dr", txt_DateOfPO.Text);
            //comm.Parameters.AddWithValue("@dc", DateTime.Now.ToLongDateString());
            //comm.Parameters.AddWithValue("@dd", txt_DeliveryDate.Text);
            //comm.Parameters.AddWithValue("@stat", "NEW");
            //comm.Parameters.AddWithValue("@st", txt_Total.Text);
            //comm.Parameters.AddWithValue("@d", txt_Discount.Text);
            //comm.Parameters.AddWithValue("@da", txt_DiscountedAmount.Text);
            //comm.Parameters.AddWithValue("@t", lbl_VAT.Text);
            //comm.Parameters.AddWithValue("@v", txt_Vats.Text);
            //comm.Parameters.AddWithValue("@sc", txt_ServiceCharge.Text);
            //comm.Parameters.AddWithValue("@tsc", txt_TotalCharge.Text);
            //comm.Parameters.AddWithValue("@total", txt_GrandTotal.Text);
            //comm.ExecuteNonQuery();
            //hpiconn.Close();
            //input data to column remainingquantity

            //create recordtable
            //string asa = "create table [a" + inv + "_Units] ([id] [int] identity (1,1) not null, [s_number] [varchar] (50), [ModelCode] [varchar] (100), [Description] [varchar] (100), [Color] [varchar] (100),[Quantity] [varchar] (100), [BackOrders] [varchar] (100),[UnitPrice] [varchar] (100),[Discount] [varchar] (100), [Amount] [varchar] (100), [Purchasable] [varchar] (100))";

            //SqlCommand ccs = new SqlCommand(asa, recsconn);
            //recsconn.Open();
            //ccs.ExecuteNonQuery();
            //recsconn.Close();
            //create recordtable

            //insert data to table
            recsconn.Open();
            int count = grid_PO1.Rows.Count;
            for (int aq = 0; aq <= count - 1; aq++)
            {
                //string ins1 = "insert into a" + inv + "_Units (ModelCOde,Description,color,quantity,backorders,purchasable)values(@mod,@desc,@col,@quan,@bkorders,@purchase)";
                //SqlCommand comms = new SqlCommand(ins1, recsconn);
                //string aa = "", ab = "", ac = "", ad = "", ae = "", af = "";


                //aa = grid_PO1.Rows[aq].Cells[0].Text;
                //ab = grid_PO1.Rows[aq].Cells[1].Text;
                //ac = grid_PO1.Rows[aq].Cells[2].Text;
                //ad = grid_PO1.Rows[aq].Cells[3].Text;
                //ae = grid_PO1.Rows[aq].Cells[4].Text;
                //af = grid_PO1.Rows[aq].Cells[5].Text;

                //comms.Parameters.AddWithValue("@mod", aa);
                //comms.Parameters.AddWithValue("@desc", ab);
                //comms.Parameters.AddWithValue("@col", ac);
                //comms.Parameters.AddWithValue("@quan", ad);
                //comms.Parameters.AddWithValue("@bkorders", ae);
                //comms.Parameters.AddWithValue("@purchase", af);
                //comms.ExecuteNonQuery();


                string ins1 = "update a" + inv + "_Units set ModelCOde = @mod,Description = @desc,color = @col,quantity = @quan,backorders = @bkorders,purchasable = @purchase where s_number = '"+(aq+1)+"'";
                SqlCommand comms = new SqlCommand(ins1, recsconn);
                string aa = "", ab = "", ac = "", ad = "", ae = "", af = "";


                aa = grid_PO1.Rows[aq].Cells[0].Text;
                ab = grid_PO1.Rows[aq].Cells[1].Text;
                ac = grid_PO1.Rows[aq].Cells[2].Text;
                ad = grid_PO1.Rows[aq].Cells[3].Text;
                ae = grid_PO1.Rows[aq].Cells[4].Text;
                af = grid_PO1.Rows[aq].Cells[5].Text;

                comms.Parameters.AddWithValue("@mod", aa);
                comms.Parameters.AddWithValue("@desc", ab);
                comms.Parameters.AddWithValue("@col", ac);
                comms.Parameters.AddWithValue("@quan", ad);
                comms.Parameters.AddWithValue("@bkorders", ae);
                comms.Parameters.AddWithValue("@purchase", af);
                comms.ExecuteNonQuery();



            }
            recsconn.Close();
            try
            {
                int g1 = grid_PO2.Rows.Count;
                if (!(g1 <= 0))
                {
                    //string asa1 = "create table [b" + inv + "_Units] ([id] [int] identity (1,1) not null, [s_number] [varchar] (50), [ModelCode] [varchar] (100), [Description] [varchar] (100), [Color] [varchar] (100),[Quantity] [varchar] (100), [BackOrders] [varchar] (100),[UnitPrice] [varchar] (100),[Discount] [varchar] (100), [Amount] [varchar] (100), [Purchasable] [varchar] (100))";

                    //SqlCommand ccs1 = new SqlCommand(asa1, recsconn);
                    //recsconn.Open();
                    //ccs1.ExecuteNonQuery();
                    //recsconn.Close();

                    recsconn.Open();
                    int count1 = grid_PO2.Rows.Count;
                    for (int aq = 0; aq <= count1 - 1; aq++)
                    {
                        //string ins2 = "insert into b" + inv + "_Units (ModelCode,Description,color,quantity,backorders,purchasable)values(@mod,@desc,@col,@quan,@bkorders,@purchase)";
                        //SqlCommand commss = new SqlCommand(ins2, recsconn);
                        //string aa1 = "", ab1 = "", ac1 = "", ad1 = "", ae1 = "", af1 = "";

                        //aa1 = grid_PO2.Rows[aq].Cells[0].Text;
                        //ab1 = grid_PO2.Rows[aq].Cells[1].Text;
                        //ac1 = grid_PO2.Rows[aq].Cells[2].Text;
                        //ad1 = grid_PO2.Rows[aq].Cells[3].Text;
                        //ae1 = grid_PO2.Rows[aq].Cells[4].Text;
                        //af1 = grid_PO2.Rows[aq].Cells[5].Text;

                        //commss.Parameters.AddWithValue("@mod", aa1);
                        //commss.Parameters.AddWithValue("@desc", ab1);
                        //commss.Parameters.AddWithValue("@col", ac1);
                        //commss.Parameters.AddWithValue("@quan", ad1);
                        //commss.Parameters.AddWithValue("@bkorders", ae1);
                        //commss.Parameters.AddWithValue("@purchase", af1);
                        //commss.ExecuteNonQuery();

                        string ins2 = "update b" + inv + "_Units set ModelCOde = @mod,Description = @desc,color = @col,quantity = @quan,backorders = @bkorders,purchasable = @purchase where s_number = '" + (aq + 1) + "'";
                        SqlCommand commss = new SqlCommand(ins2, recsconn);
                        string aa1 = "", ab1 = "", ac1 = "", ad1 = "", ae1 = "", af1 = "";

                        aa1 = grid_PO2.Rows[aq].Cells[0].Text;
                        ab1 = grid_PO2.Rows[aq].Cells[1].Text;
                        ac1 = grid_PO2.Rows[aq].Cells[2].Text;
                        ad1 = grid_PO2.Rows[aq].Cells[3].Text;
                        ae1 = grid_PO2.Rows[aq].Cells[4].Text;
                        af1 = grid_PO2.Rows[aq].Cells[5].Text;

                        commss.Parameters.AddWithValue("@mod", aa1);
                        commss.Parameters.AddWithValue("@desc", ab1);
                        commss.Parameters.AddWithValue("@col", ac1);
                        commss.Parameters.AddWithValue("@quan", ad1);
                        commss.Parameters.AddWithValue("@bkorders", ae1);
                        commss.Parameters.AddWithValue("@purchase", af1);
                        commss.ExecuteNonQuery();
                    }
                    recsconn.Close();
                }
            }
            catch
            {

            }
            string counter = "SELECT COUNT(*) as Numbers FROM systempurchaseorderstbl ";
            SqlCommand c11 = new SqlCommand(counter, recsconn);
            SqlDataReader re11;
            recsconn.Open();
            re11 = c11.ExecuteReader();
            re11.Read();
            int number1 = Convert.ToInt32(re11.GetInt32(0));
            recsconn.Close();

            bool same = false;
            for (int dw = 1; dw <= number1; dw++)
            {
                try
                {
                    string ch = "select purchaseorder from systempurchaseorderstbl where s_number = '" + dw + "'";
                    SqlCommand takte = new SqlCommand(ch, recsconn);
                    SqlDataReader tt;
                    recsconn.Open();
                    tt = takte.ExecuteReader();
                    tt.Read();
                    string datum = tt.GetString(0);
                    recsconn.Close();

                    if (datum == txt_POnum.Text)
                    {
                        same = true;
                        break;
                    }
                }
                catch
                {
                    if (recsconn.State == ConnectionState.Open)
                        recsconn.Close();
                }
            }

            if (same == false)
            {
                if (recsconn.State == ConnectionState.Open)
                    recsconn.Close();
                char refIDs = refnum[0];
                string cats = "";
                if (refIDs.ToString() == "0")
                {
                    cats = "Unit";
                }
                else if (refIDs.ToString() == "1")
                {
                    cats = "Part";
                }
                string ord = "insert into systempurchaseorderstbl (category,purchaseorder,datereceived, status)values('" + cats + "','" + txt_POnum.Text + "','" + txt_DateOfPO.Text + "','New')";
                SqlCommand take = new SqlCommand(ord, recsconn);
                recsconn.Open();
                take.ExecuteNonQuery();
                recsconn.Close();

                string remova = "UPDATE systempurchaseorderstbl SET s_number = NULL";
                SqlCommand mssa = new SqlCommand(remova, recsconn);
                recsconn.Open();
                mssa.ExecuteNonQuery();
                recsconn.Close();

                string snumba = "With cte As(Select id, s_number,  Row_Number() Over (Order By s_number) As rn From systempurchaseorderstbl)Update cte Set s_number = rn";
                SqlCommand ssqusa = new SqlCommand(snumba, recsconn);
                recsconn.Open();
                ssqusa.ExecuteNonQuery();
                recsconn.Close();
            }



            //insert data to table
            //credentials saver
            var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

            var random = new Random();
            var list = Enumerable.Repeat(0, 6).Select(x => chars[random.Next(chars.Length)]);
            string letters = string.Join("", list);
            
            string Fletter = letters[0].ToString() + letters[1].ToString();
            
            Random rnd = new Random();
            int num1 = rnd.Next(1000000,9999999);

            string Lletter = letters[2].ToString() + letters[3].ToString();
            string Suffix = letters[4].ToString() + letters[5].ToString();

            string delno = Fletter + num1.ToString() + Lletter + " " + Suffix;


            recsconn.Open();
            string ins = "insert into systemRecordcredentialstbl (category,ponumber,invoiceno,deliveryno,datereceived,datecreated,deliverydate,status,subtotal,discount,discountedamount,tax,vat,servicecharge,totalservicecharge,total,AmountPaid,PaymentRemarks)values(@cat,@pon,@sin,@delno,@dr,@dc,@dd,@stat,@st,@d,@da,@t,@v,@sc,@tsc,@total,@am,@pr)";
            SqlCommand comm = new SqlCommand(ins, recsconn);
            char refID = refnum[0];
            string cat = "";
            if (refID.ToString() == "0")
            {
                cat = "Unit";
            }
            else if (refID.ToString() == "1")
            {
                cat = "Part";
            }
            comm.Parameters.AddWithValue("@cat", cat);
            comm.Parameters.AddWithValue("@pon", txt_POnum.Text);
            comm.Parameters.AddWithValue("@sin", txt_SalesInvoice.Text);
            comm.Parameters.AddWithValue("@delno", delno);
            comm.Parameters.AddWithValue("@dr", txt_DateOfPO.Text);
            comm.Parameters.AddWithValue("@dc", DateTime.Now.ToLongDateString());
            comm.Parameters.AddWithValue("@dd", txt_DeliveryDate.Text);
            comm.Parameters.AddWithValue("@stat", "New");
            comm.Parameters.AddWithValue("@st", txt_Total.Text);
            comm.Parameters.AddWithValue("@d", txt_Discount.Text);
            comm.Parameters.AddWithValue("@da", txt_DiscountedAmount.Text);
            comm.Parameters.AddWithValue("@t", lbl_VAT.Text);
            comm.Parameters.AddWithValue("@v", txt_Vats.Text);
            comm.Parameters.AddWithValue("@sc", txt_ServiceCharge.Text);
            comm.Parameters.AddWithValue("@tsc", txt_TotalCharge.Text);
            comm.Parameters.AddWithValue("@total", txt_GrandTotal.Text);
            comm.Parameters.AddWithValue("@am", "0.00");
            comm.Parameters.AddWithValue("@pr", "UNPAID");
            comm.ExecuteNonQuery();
            recsconn.Close();
            //invoice save
            //credentials saver
            //alter table
            //string alter = "ALTER TABLE a" + refnum + "_Units ADD UnitPrice varchar(100),Discount varchar(100),Amount varchar(100)";
            //SqlCommand mn = new SqlCommand(alter,hpiconn);
            //hpiconn.Open();
            //mn.ExecuteNonQuery();
            //hpiconn.Close();

            //try
            //{
            //    string alter1 = "ALTER TABLE b" + refnum + "_Units ADD UnitPrice varchar(100),Discount varchar(100),Amount varchar(100)";
            //    SqlCommand mn1 = new SqlCommand(alter1, hpiconn);
            //    hpiconn.Open();
            //    mn1.ExecuteNonQuery();
            //    hpiconn.Close();
            //}
            //catch
            //{
            //    hpiconn.Close();
            //}
            //alter table

            //reset s_number
            recsconn.Open();
            string removers = "UPDATE systemRecordcredentialstbl SET s_number = NULL";
            SqlCommand mss = new SqlCommand(removers, recsconn);
            mss.ExecuteNonQuery();

            string snumbers = "With cte As(Select id, s_number,  Row_Number() Over (Order By s_number) As rn From systemRecordcredentialstbl)Update cte Set s_number = rn";
            SqlCommand ssqus = new SqlCommand(snumbers, recsconn);

            ssqus.ExecuteNonQuery();
            recsconn.Close();
            //reset s_number

            //enter data to column unitprice,discount and amount\
            //unitprice
            string numgetter = "SELECT COUNT(*) as Numbers FROM a" + refnum + "_Units ";
            SqlCommand c1 = new SqlCommand(numgetter, hpiconn);
            SqlDataReader re1;
            hpiconn.Open();
            re1 = c1.ExecuteReader();
            re1.Read();
            int number = Convert.ToInt32(re1.GetInt32(0));
            hpiconn.Close();


            for (int d = 1; d <= number; d++)
            {
                try
                {
                    string test = grid_PO1.Rows[d - 1].Cells[4].Text;
                    if (test != "N/A")
                    {
                        hpiconn.Open();
                        string que = "select modelcode,color from a" + refnum + "_units where s_number = '" + d + "'";
                        SqlCommand c2 = new SqlCommand(que, hpiconn);
                        SqlDataReader re2;
                        re2 = c2.ExecuteReader();
                        re2.Read();
                        string mc = re2.GetString(0).ToString();
                        string c = re2.GetString(1).ToString();
                        hpiconn.Close();

                        hpiconn.Open();
                        string pricegetter = "select initialprice from systemModelstbl where modelcode = '" + mc + "' and color = '" + c + "'";
                        SqlCommand c3 = new SqlCommand(pricegetter, hpiconn);
                        SqlDataReader re3;
                        re3 = c3.ExecuteReader();
                        re3.Read();
                        string price = re3.GetString(0).ToString();
                        hpiconn.Close();

                        recsconn.Open();
                        string input = "update a" + inv + "_Units set Unitprice = '" + price + "' where modelcode = '" + mc + "' and color = '" + c + "'";
                        SqlCommand c4 = new SqlCommand(input, recsconn);
                        c4.ExecuteNonQuery();
                        recsconn.Close();
                    }

                }
                catch (Exception ex)
                {

                }

                try
                {
                    int g1 = grid_PO2.Rows.Count;
                    double[] arr1 = new double[g1];
                    if (!(g1 <= 0))
                    {
                        string test = grid_PO2.Rows[d - 1].Cells[4].Text;
                        if (test != "N/A")
                        {
                            hpiconn.Open();
                            string queb = "select modelcode,color from b" + refnum + "_units where s_number = '" + d + "'";
                            SqlCommand c2b = new SqlCommand(queb, hpiconn);
                            SqlDataReader re2b;
                            re2b = c2b.ExecuteReader();
                            re2b.Read();
                            string mcb = re2b.GetString(0).ToString();
                            string cb = re2b.GetString(1).ToString();
                            hpiconn.Close();

                            hpiconn.Open();
                            string pricegetterb = "select initialprice from systemModelstbl where modelcode = '" + mcb + "' and color = '" + cb + "'";
                            SqlCommand c3b = new SqlCommand(pricegetterb, hpiconn);
                            SqlDataReader re3b;
                            re3b = c3b.ExecuteReader();
                            re3b.Read();
                            string priceb = re3b.GetString(0).ToString();
                            hpiconn.Close();

                            recsconn.Open();
                            string inputb = "update b" + inv + "_Units set Unitprice = '" + priceb + "' where modelcode = '" + mcb + "' and color = '" + cb + "'";
                            SqlCommand c4b = new SqlCommand(inputb, recsconn);
                            c4b.ExecuteNonQuery();
                            recsconn.Close();
                        }
                    }
                }
                catch
                {
                    if (hpiconn.State == ConnectionState.Open)
                        hpiconn.Close();
                }

            }
            //unitprice
            //discount

            //discount
            //enter data to column unitprice,discount and amount

            int r = grid_PO1.Rows.Count;
            for (int s = 0; s <= r - 1; s++)
            {
                s++;
                if (hpiconn.State == ConnectionState.Open)
                    hpiconn.Close();
                hpiconn.Open();
                string replace = "select backorders from a" + refnum + "_units where s_number = '" + s + "'";
                SqlCommand ads = new SqlCommand(replace, hpiconn);
                SqlDataReader rs;
                rs = ads.ExecuteReader();
                rs.Read();
                string replacer = rs.GetString(0).ToString();
                hpiconn.Close();
                hpiconn.Open();
                string dist = "update a" + refnum + "_units set Quantity = '" + replacer + "' where s_number = '" + s + "'";
                SqlCommand adss = new SqlCommand(dist, hpiconn);
                adss.ExecuteNonQuery();
                hpiconn.Close();
                s--;
            }
            try
            {
                int g1 = grid_PO2.Rows.Count;
                if (!(g1 <= 0))
                {
                    int r1 = grid_PO2.Rows.Count;
                    for (int s = 0; s <= r1 - 1; s++)
                    {
                        s++;
                        hpiconn.Open();
                        string replace = "select backorders from b" + refnum + "_units where s_number = '" + s + "'";
                        SqlCommand ads = new SqlCommand(replace, hpiconn);
                        SqlDataReader rs;
                        rs = ads.ExecuteReader();
                        rs.Read();
                        string replacer = rs.GetString(0).ToString();
                        hpiconn.Close();
                        hpiconn.Open();
                        string dist = "update b" + refnum + "_units set Quantity = '" + replacer + "' where s_number = '" + s + "'";
                        SqlCommand adss = new SqlCommand(dist, hpiconn);
                        adss.ExecuteNonQuery();
                        hpiconn.Close();
                        s--;
                    }
                }
            }
            catch { }

            //deductions
            int ans = 0;
            int de = grid_PO1.Rows.Count;
            for (int ded = 0; ded <= de - 1; ded++)
            {
                string dat = grid_PO1.Rows[ded].Cells[5].Text;
                string code = grid_PO1.Rows[ded].Cells[0].Text;
                string mocolr = grid_PO1.Rows[ded].Cells[2].Text;

                if (dat != "N/A")
                {
                    hpiconn.Open();
                    string det = "select quantity from systemmodelstbl where modelcode = '" + code + "' and color = '" + mocolr + "'";
                    SqlCommand mz = new SqlCommand(det, hpiconn);
                    SqlDataReader xx;
                    xx = mz.ExecuteReader();
                    xx.Read();
                    string ret = xx.GetString(0).ToString();// minusan
                    hpiconn.Close();
                    int fdat = Convert.ToInt32(dat);//minuser
                    ans = Convert.ToInt32(ret) - fdat;
                    if (ans < 0)
                    {
                        ans = 0;
                        hpiconn.Open();
                        string save = "update systemmodelstbl set quantity = '" + ans + "' where modelcode = '" + code + "' and color = '" + mocolr + "'";
                        SqlCommand mns = new SqlCommand(save, hpiconn);
                        mns.ExecuteNonQuery();
                        hpiconn.Close();
                        ans = 0;
                    }
                    else if (ans >= 0)
                    {
                        hpiconn.Open();
                        string save = "update systemmodelstbl set quantity = '" + ans + "' where modelcode = '" + code + "' and color = '" + mocolr + "'";
                        SqlCommand mns = new SqlCommand(save, hpiconn);
                        mns.ExecuteNonQuery();
                        hpiconn.Close();
                        ans = 0;
                    }
                }
            }
            try
            {
                int de1 = grid_PO2.Rows.Count;
                for (int ded = 0; ded <= de1 - 1; ded++)
                {
                    string dat = grid_PO2.Rows[ded].Cells[5].Text;
                    string code = grid_PO2.Rows[ded].Cells[0].Text;
                    string mocolr = grid_PO2.Rows[ded].Cells[2].Text;

                    if (dat != "N/A")
                    {
                        hpiconn.Open();
                        string det = "select quantity from systemmodelstbl where modelcode = '" + code + "' and color = '" + mocolr + "'";
                        SqlCommand mz = new SqlCommand(det, hpiconn);
                        SqlDataReader xx;
                        xx = mz.ExecuteReader();
                        xx.Read();
                        string ret = xx.GetString(0).ToString();// minusan
                        hpiconn.Close();
                        int fdat = Convert.ToInt32(dat);//minuser
                        ans = Convert.ToInt32(ret) - fdat;
                        if (ans < 0)
                        {
                            ans = 0;
                            hpiconn.Open();
                            string save = "update systemmodelstbl set quantity = '" + ans + "' where modelcode = '" + code + "' and color = '" + mocolr + "'";
                            SqlCommand mns = new SqlCommand(save, hpiconn);
                            mns.ExecuteNonQuery();
                            hpiconn.Close();
                            ans = 0;
                        }
                        else if (ans >= 0)
                        {
                            hpiconn.Open();
                            string save = "update systemmodelstbl set quantity = '" + ans + "' where modelcode = '" + code + "' and color = '" + mocolr + "'";
                            SqlCommand mns = new SqlCommand(save, hpiconn);
                            mns.ExecuteNonQuery();
                            hpiconn.Close();
                            ans = 0;
                        }
                    }
                }
            }
            catch
            { }
            //deductions
            //update
            Update();
            //update

            //create order report
            try
            {
                string a = "create table [a" + refnum + "_Units] ([id] [int] identity (1,1) not null,[s_number] [varchar] (50), [ModelCode] [varchar] (100), [Description] [varchar] (100), [Color] [varchar] (100), [Quantity] [varchar] (100))";

                SqlCommand cc = new SqlCommand(a, recsconn);
                recsconn.Open();
                cc.ExecuteNonQuery();
                recsconn.Close();



                int cx = grid_PO1.Rows.Count;
                recsconn.Open();
                for (int ax = 0; ax <= cx - 1; ax++)
                {
                    string MCodes = grid_PO1.Rows[ax].Cells[0].Text;
                    string Decss = grid_PO1.Rows[ax].Cells[1].Text;
                    string Colors = grid_PO1.Rows[ax].Cells[2].Text;
                    string Qq = grid_PO1.Rows[ax].Cells[3].Text;
                    int j = ax + 1;
                    string ins1 = "insert into a" + refnum + "_Units (s_number,ModelCode,Description,Color,Quantity)values('" + j.ToString() + "','" + MCodes + "','" + Decss + "','" + Colors + "','" + Qq + "')";
                    SqlCommand mss1 = new SqlCommand(ins1, recsconn);
                    mss1.ExecuteNonQuery();
                }
                recsconn.Close();


                if (grid_PO2.Rows.Count == 0)// meaning walang laman ang grid_Addeds at walang b
                {

                }
                else
                {
                    string re111 = refnum;
                    string a1 = "create table [b" + re111 + "_Units] ([id] [int] identity (1,1) not null,[s_number] [varchar] (50), [ModelCode] [varchar] (100), [Description] [varchar] (100), [Color] [varchar] (100), [Quantity] [varchar] (100))";

                    SqlCommand cc1 = new SqlCommand(a1, recsconn);
                    recsconn.Open();
                    cc1.ExecuteNonQuery();
                    recsconn.Close();

                    int cx1 = grid_PO2.Rows.Count;
                    recsconn.Open();
                    for (int ax = 0; ax <= cx1 - 1; ax++)
                    {
                        string MCodes = grid_PO2.Rows[ax].Cells[0].Text;
                        string Decss = grid_PO2.Rows[ax].Cells[1].Text;
                        string Colors = grid_PO2.Rows[ax].Cells[2].Text;
                        string Qq = grid_PO2.Rows[ax].Cells[3].Text;
                        int j = ax + 1;
                        string insq = "insert into b" + refnum + "_Units (s_number,ModelCode,Description,Color,Quantity)values('" + j.ToString() + "','" + MCodes + "','" + Decss + "','" + Colors + "','" + Qq + "')";
                        SqlCommand mssq = new SqlCommand(insq, recsconn);
                        mssq.ExecuteNonQuery();
                    }

                    //string notif = "update SystemNotifierTBL set notifier = 'YES'";
                    //SqlCommand mn = new SqlCommand(notif,conn);
                    //mn.ExecuteNonQuery();
                    recsconn.Close();
                }
            }
            catch
            {
                // ORDER report already created
            }
            //create order report
            //create delivery table
            string nodel = delno.Replace(" ", "");

            string deliver = "create table [a" + nodel + "_Units] ([id] [int] identity (1,1) not null, [s_number] [varchar] (50), [ModelCode] [varchar] (100),[Color] [varchar] (100),[Quantity] [varchar] (100),[Damaged] [varchar] (100))";

            SqlCommand dver = new SqlCommand(deliver, recsconn);
            recsconn.Open();
            dver.ExecuteNonQuery();
            recsconn.Close();

            int cxw = grid_PO1.Rows.Count;
            recsconn.Open();
            for (int ax = 0; ax <= cxw - 1; ax++)
            {
                string MCodes = grid_PO1.Rows[ax].Cells[0].Text;
                //string Decss = grid_PO1.Rows[ax].Cells[1].Text;
                string Colors = grid_PO1.Rows[ax].Cells[2].Text;
                string Qq = grid_PO1.Rows[ax].Cells[3].Text;
                int j = ax + 1;
                string ins1 = "insert into a" + nodel + "_Units (s_number,ModelCode,Color,Quantity,Damaged)values('" + j.ToString() + "','" + MCodes + "','" + Colors + "','" + Qq + "','0')";
                SqlCommand mss1 = new SqlCommand(ins1, recsconn);
                mss1.ExecuteNonQuery();
            }
            recsconn.Close();

            if (grid_PO2.Rows.Count == 0)// meaning walang laman ang grid_Addeds at walang b
            {

            }
            else
            {
                string del2 = "create table [b" + nodel + "_Units] ([id] [int] identity (1,1) not null, [s_number] [varchar] (50), [ModelCode] [varchar] (100),[Color] [varchar] (100),[Quantity] [varchar] (100))";

                SqlCommand fg = new SqlCommand(del2, recsconn);
                recsconn.Open();
                fg.ExecuteNonQuery();
                recsconn.Close();

                int cx1 = grid_PO2.Rows.Count;
                recsconn.Open();
                for (int ax = 0; ax <= cx1 - 1; ax++)
                {
                    string MCodes = grid_PO2.Rows[ax].Cells[0].Text;
                    //string Decss = grid_PO2.Rows[ax].Cells[1].Text;
                    string Colors = grid_PO2.Rows[ax].Cells[2].Text;
                    string Qq = grid_PO2.Rows[ax].Cells[3].Text;
                    int j = ax + 1;
                    string insq = "insert into b" + nodel + "_Units (s_number,ModelCode,Color,Quantity)values('" + j.ToString() + "','" + MCodes + "','" + Colors + "','" + Qq + "')";
                    SqlCommand mssq = new SqlCommand(insq, recsconn);
                    mssq.ExecuteNonQuery();
                }
                recsconn.Close();
            }
            //create delivery table




            //create billing table
            //string nodel = delno.Replace(" ", "");

            string bill = "create table [billA" + inv + "_Units] ([id] [int] identity (1,1) not null, [s_number] [varchar] (50), [ModelCode] [varchar] (100),[Description] [varchar] (100),[Color] [varchar] (100),[Purchasable] [varchar] (100),[UnitPrice] [varchar] (100),[Amount] [varchar] (100))";

            SqlCommand biller = new SqlCommand(bill, recsconn);
            recsconn.Open();
            biller.ExecuteNonQuery();
            recsconn.Close();

            //int ggNaTo = grid_aSI.Rows.Count;
            //recsconn.Open();
            //for (int ax = 0; ax <= ggNaTo - 1; ax++)
            //{
            //    string MCodes = grid_aSI.Rows[ax].Cells[0].Text;
            //    string Decss = grid_aSI.Rows[ax].Cells[1].Text;
            //    string Colors = grid_aSI.Rows[ax].Cells[2].Text;
            //    string Qq = grid_aSI.Rows[ax].Cells[5].Text;
            //    string Up = grid_aSI.Rows[ax].Cells[6].Text;
            //    string Am = grid_aSI.Rows[ax].Cells[8].Text;
            //    int j = ax + 1;
            //    string ins1 = "insert into billA" + nodel + "_Units (s_number,ModelCode,Description,Color,Purchasable,UnitPrice,Amount)values('" + j.ToString() + "','" + MCodes + "','" + Colors + "','" + Qq + "','" + Up + "','" + Am + "')";
            //    SqlCommand mss1 = new SqlCommand(ins1, recsconn);
            //    mss1.ExecuteNonQuery();
            //}
            //recsconn.Close();

            if (grid_PO2.Rows.Count == 0)// meaning walang laman ang grid_Addeds at walang b
            {

            }
            else
            {
                string del2 = "create table [billB" + inv + "_Units] ([id] [int] identity (1,1) not null, [s_number] [varchar] (50), [ModelCode] [varchar] (100),[Description] [varchar] (100),[Color] [varchar] (100),[Purchasable] [varchar] (100),[UnitPrice] [varchar] (100),[Amount] [varchar] (100))";

                SqlCommand fg = new SqlCommand(del2, recsconn);
                recsconn.Open();
                fg.ExecuteNonQuery();
                recsconn.Close();

                //int ggNaToHaha = grid_bSI.Rows.Count;
                //recsconn.Open();
                //for (int ax = 0; ax <= ggNaToHaha - 1; ax++)
                //{
                //    string MCodes = grid_bSI.Rows[ax].Cells[0].Text;
                //    string Decss = grid_bSI.Rows[ax].Cells[1].Text;
                //    string Colors = grid_bSI.Rows[ax].Cells[2].Text;
                //    string Qq = grid_bSI.Rows[ax].Cells[5].Text;
                //    string Up = grid_bSI.Rows[ax].Cells[6].Text;
                //    string Am = grid_bSI.Rows[ax].Cells[8].Text;
                //    int j = ax + 1;
                //    string ins1 = "insert into billB" + nodel + "_Units (s_number,ModelCode,Description,Color,Purchasable,UnitPrice,Amount)values('" + j.ToString() + "','" + MCodes + "','" + Colors + "','" + Qq + "','" + Up + "','" + Am + "')";
                //    SqlCommand mss1 = new SqlCommand(ins1, recsconn);
                //    mss1.ExecuteNonQuery();
                //}
                //recsconn.Close();
            }
            //create billing table
            MultiView1.ActiveViewIndex = 3;
            MultiView5.ActiveViewIndex = 0;
        }
        private void Update()
        {
            hpiconn.Open();
            string range = "Select reorder,critical from systeminventorysettingtbl where id = 1";
            SqlCommand comander = new SqlCommand(range, hpiconn);
            SqlDataReader rader;
            rader = comander.ExecuteReader();

            rader.Read();
            string reoder = rader.GetString(0);
            string critical = rader.GetString(1);
            hpiconn.Close();

            int a = Convert.ToInt32(reoder);
            int b = Convert.ToInt32(critical);

            hpiconn.Open();
            string set = "update systemmodelstbl set status = 'Safety' where quantity > " + a + "";
            SqlCommand bb = new SqlCommand(set, hpiconn);
            bb.ExecuteNonQuery();
            hpiconn.Close();

            hpiconn.Open();
            string set2 = "update systemmodelstbl set status = 'Re-order' where quantity between " + b + " and " + a + "";
            SqlCommand bb2 = new SqlCommand(set2, hpiconn);
            bb2.ExecuteNonQuery();
            hpiconn.Close();

            hpiconn.Open();
            string set1 = "update systemmodelstbl set status = 'Critical' where quantity <= " + b + "";
            SqlCommand bb1 = new SqlCommand(set1, hpiconn);
            bb1.ExecuteNonQuery();
            hpiconn.Close();

            hpiconn.Open();
            string set3 = "update systemmodelstbl set status = 'Out' where quantity = '0'";
            SqlCommand bb3 = new SqlCommand(set3, hpiconn);
            bb3.ExecuteNonQuery();
            hpiconn.Close();

            stable();

        }

        protected void btn_Reports_Click(object sender, EventArgs e)
        {
            MultiView1.ActiveViewIndex = 3;
            MultiView5.ActiveViewIndex = 0;
        }
        string sels = "select Status from systemPurchaseorderstbl where status = 'new'";
        //string sels2 = "select Status from SystemPOSICredentialsTBL where status = 'new'";
        protected void Timer8_Tick(object sender, EventArgs e)
        {
            SqlCommand com = new SqlCommand(sels, recsconn);
            IDataReader r;
            recsconn.Open();
            r = com.ExecuteReader();
            int a = 0;
            while (r.Read())
            {

                string not = r.GetString(0).ToString();//secon row
                if (not == "New")
                    a++;
            }
            recsconn.Close();

            //SqlCommand coma = new SqlCommand(sels2, hpiconn);
            //IDataReader ra;
            //hpiconn.Open();
            //ra = coma.ExecuteReader();
            //int aa = 0;
            //while (ra.Read())
            //{
            //    string not = ra.GetString(0).ToString();
            //    if (not == "NEW")
            //        aa++;
            //}
            //hpiconn.Close();

            int ax = a/* + aa*/;

            if (a != 0/* || aa != 0*/)
            {
                //Dataset;
                lbl_noNotif.Visible = !true;
                lbl_YesNotif.Visible = true;
                lbl_YesNotif.Text = ax.ToString();
            }
            else if (a == 0/* || aa == 0*/)
            {
                lbl_noNotif.Visible = true;
                lbl_YesNotif.Visible = false;
            }
        }

        protected void grid_Reports_SelectedIndexChanged(object sender, EventArgs e)
        {//
            ViewState["ponn"] = grid_Reports.SelectedRow.Cells[1].Text;
            //order report
            //a
            string numb = ViewState["ponn"].ToString();
            txt_poNumbers.Text = numb;
            txt_DatePO.Text = grid_Reports.SelectedRow.Cells[2].Text;
            recsconn.Open();
            string order = "select modelcode,description,color,quantity from a"+numb+"_Units";
            SqlDataAdapter adaptase = new SqlDataAdapter(order, recsconn);

            DataSet seteda = new DataSet();
            adaptase.Fill(seteda);

            grid_orderReport1.DataSource = seteda.Tables[0];
            grid_orderReport1.DataBind();
            recsconn.Close();
            //a
            //b
            try
            {
                recsconn.Open();
                string border = "select modelcode,description,color,quantity from b" + numb + "_Units";
                SqlDataAdapter badaptase = new SqlDataAdapter(border, recsconn);

                DataSet bseteda = new DataSet();
                badaptase.Fill(bseteda);

                grid_orderReport2.DataSource = bseteda.Tables[0];
                grid_orderReport2.DataBind();
                recsconn.Close();

                lbl_Ads.Visible = true;
            }
            catch
            {
                if (recsconn.State == ConnectionState.Open)
                    recsconn.Close();
            }
            //b
            //order report

            //sales invoice
            if (recsconn.State == ConnectionState.Open)
                recsconn.Close();
            recsconn.Open();
            string list = "select invoiceno as 'Invoice No.',Deliveryno as 'Delivery No.',datecreated as 'Date Generated',Status from systemrecordcredentialstbl where ponumber = '" + ViewState["ponn"].ToString() +"'";
            SqlDataAdapter adaptas = new SqlDataAdapter(list, recsconn);

            DataSet seted = new DataSet();
            adaptas.Fill(seted);

            grid_invoices.DataSource = seted.Tables[0];
            grid_invoices.DataBind();
            recsconn.Close();
            //sales invoice
            
            //delivery
            recsconn.Open();
            string dellist = "select Deliveryno as 'Delivery No.',invoiceno as 'Invoice No.',datecreated as 'Date Generated',Status from systemrecordcredentialstbl where ponumber = '" + ViewState["ponn"].ToString() + "'";
            SqlDataAdapter deladaptas = new SqlDataAdapter(dellist, recsconn);

            DataSet delseted = new DataSet();
            deladaptas.Fill(delseted);

            grid_DeliveryList.DataSource = delseted.Tables[0];
            grid_DeliveryList.DataBind();
            recsconn.Close();
            //delivery
            //billing
            recsconn.Open();
            string bill = "select invoiceno as 'Invoice No.',datecreated as 'Date Generated' from systemrecordcredentialstbl where ponumber = '" + ViewState["ponn"].ToString() + "'";
            SqlDataAdapter billadapter = new SqlDataAdapter(bill, recsconn);

            DataSet billset = new DataSet();
            billadapter.Fill(billset);

            grid_Billing.DataSource = billset.Tables[0];
            grid_Billing.DataBind();
            recsconn.Close();
            //billing
            
           // string pos = grid_Reports.SelectedRow.Cells[1].Text;
           // string inv = grid_Reports.SelectedRow.Cells[2].Text;
           // recsconn.Open();
           // string mq = "select ponumber,invoiceno,dateReceived,deliverydate,subtotal,discount,discountedamount,tax,vat,servicecharge,totalservicecharge,total from systemrecordcredentialstbl where ponumber = '" + pos + "' and invoiceno = '" + inv + "'";
           // SqlCommand sir = new SqlCommand(mq, recsconn);
           // SqlDataReader d;
           // d = sir.ExecuteReader();
           // d.Read();

           // ViewState["po"] = d.GetString(0).ToString();
           // ViewState["inv"] = d.GetString(1).ToString();
           // ViewState["dr"] = d.GetString(2).ToString();
           //// ViewState["dc"] = d.GetString(3).ToString();//
           // ViewState["dd"] = d.GetString(3).ToString();
           // ViewState["st"] = d.GetString(4).ToString();
           // ViewState["dis"] = d.GetString(5).ToString();
           // ViewState["da"] = d.GetString(6).ToString();
           // ViewState["tax"] = d.GetString(7).ToString();
           // ViewState["vat"] = d.GetString(8).ToString();
           // ViewState["sc"] = d.GetString(9).ToString();
           // ViewState["tsc"] = d.GetString(10).ToString();
           // ViewState["total"] = d.GetString(11).ToString();
           // recsconn.Close();

           // string update = "update systemrecordcredentialstbl set status = 'Viewed' where ponumber = '" + pos + "' and invoiceno = '" + inv + "'";
           // SqlCommand up = new SqlCommand(update, recsconn);
           // recsconn.Open();
           // up.ExecuteNonQuery();
           // recsconn.Close();

           // //salesinvoice
           // lbl_PONumber.Text = ViewState["po"].ToString();
           // lbl_SalesInvoice.Text = ViewState["inv"].ToString();
           // lbl_DateReceived.Text = ViewState["dr"].ToString();
           // lbl_Delivery.Text = ViewState["dd"].ToString();

           // recsconn.Open();

           // string ass2 = "select  ModelCode,Description,Color,Quantity,BackOrders,Purchasable,UnitPrice,Discount,Amount from a" + inv + "_Units";
           // SqlDataAdapter adapter2 = new SqlDataAdapter(ass2, recsconn);
           // DataSet set2 = new DataSet();
           // adapter2.Fill(set2);
           // grid_aSI.DataSource = set2.Tables[0];
           // grid_aSI.DataBind();
           // recsconn.Close();


           // grid_bSI.DataSource = null;
           // grid_bSI.DataBind();
           // try
           // {
           //     recsconn.Open();
           //     string bass2 = "select  ModelCode,Description,Color,Quantity,BackOrders,Purchasable,UnitPrice,Discount,Amount from b" + inv + "_Units";
           //     SqlDataAdapter badapter2 = new SqlDataAdapter(bass2, recsconn);
           //     DataSet bset2 = new DataSet();
           //     badapter2.Fill(bset2);
           //     grid_bSI.DataSource = bset2.Tables[0];
           //     grid_bSI.DataBind();
           //     recsconn.Close();
           //     lbl_AddedItems.Visible = true;
           // }
           // catch
           // {

           // }

           // txt_sTotal.Text = ViewState["st"].ToString();
           // txt_Disc.Text = ViewState["dis"].ToString();
           // txt_DiscAmount.Text = ViewState["da"].ToString();
           // lbl_Vatt.Text = ViewState["tax"].ToString();
           // txt_Vatt.Text = ViewState["vat"].ToString();
           // txt_SCharge.Text = ViewState["sc"].ToString();
           // txt_TotalsCharge.Text = ViewState["tsc"].ToString();
           // txt_GTotal.Text = ViewState["total"].ToString();
           // //salesinvoice


            MultiView1.ActiveViewIndex = 4;
            MultiView6.ActiveViewIndex = 0;



        }

        protected void btn_OrderReports_Click(object sender, EventArgs e)
        {
            MultiView5.ActiveViewIndex = 0;
        }

        protected void btn_BillingReport_Click(object sender, EventArgs e)
        {
            MultiView5.ActiveViewIndex = 2;
            MultiView8.ActiveViewIndex = 0;
        }

        protected void btn_DeliveryReport_Click(object sender, EventArgs e)
        {
            MultiView5.ActiveViewIndex = 3;
            MultiView7.ActiveViewIndex = 0;
        }

        protected void Button26_Click(object sender, EventArgs e)
        {
            int a = grid_update2.Rows.Count;
            Response.Write(a);
        }

        protected void btn_Inventory_Click(object sender, EventArgs e)
        {
            
            MultiView1.ActiveViewIndex = 5;
            hpiconn.Open();
            string range = "Select reorder,critical from systeminventorysettingtbl where id = 1";
            SqlCommand comander = new SqlCommand(range, hpiconn);
            SqlDataReader rader;
            rader = comander.ExecuteReader();

            rader.Read();
            txt_Reorder.Text = rader.GetString(0);
            txt_Crit.Text = rader.GetString(1);
            hpiconn.Close();

            Update();
        }

        protected void ddwn_Stats_SelectedIndexChanged(object sender, EventArgs e)
        {
            stable();
        }

        protected void btn_refsh_Click(object sender, EventArgs e)
        {

        }

        protected void grid_Inv_SelectedIndexChanged(object sender, EventArgs e)
        {
            stable();
            ViewState["Quans"] = grid_Inv.SelectedRow.Cells[4].Text;
            ViewState["d1"] = grid_Inv.SelectedRow.Cells[1].Text;
            ViewState["d2"] = grid_Inv.SelectedRow.Cells[3].Text;
        }

        protected void btn_SaveQ_Click(object sender, EventArgs e)
        {
            string d1 = null;
            string d2 = null;
            try
            {
                d1 = ViewState["d1"].ToString();
                d2 = ViewState["d2"].ToString();
            }
            catch
            {
                d1 = null;
                d2 = null;
            }
            
            if (d1 != null || d2 != null)
            {
                int addan = Convert.ToInt32(ViewState["Quans"]);
                addan += Convert.ToInt32(txt_Quans.Text);

                string upd = "update systemmodelstbl set quantity = '" + addan + "' where modelcode = '" + d1 + "' and color ='" + d2 + "'";
                SqlCommand dc = new SqlCommand(upd, hpiconn);
                hpiconn.Open();
                dc.ExecuteNonQuery();
                hpiconn.Close();
                grid_Inv.SelectedIndex = -1;
                ViewState["d1"] = null;
                ViewState["d2"] = null;
                Update();
                stable();
            }
            

            
            
        }

        protected void btn_ClearQ_Click(object sender, EventArgs e)
        {
            txt_Quans.Text = "";
            stable();
        }

        private void stable()
        {
            hpiconn.Open();
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
                string ss = "select modelcode ,description, color,quantity,status from systemmodelstbl where status = 'Safety'";
                SqlDataAdapter mz = new SqlDataAdapter(ss, hpiconn);
                DataSet set = new DataSet();
                mz.Fill(set);
                grid_Inv.DataSource = set.Tables[0];
                grid_Inv.DataBind();
            }
            else if (ddwn_Stats.SelectedItem.Text == "Re-order Points")
            {
                grid_Inv.DataSourceID = null;
                string ss = "select modelcode ,description, color,quantity,status from systemmodelstbl where status = 'Re-order'";
                SqlDataAdapter mz = new SqlDataAdapter(ss, hpiconn);
                DataSet set = new DataSet();
                mz.Fill(set);
                grid_Inv.DataSource = set.Tables[0];
                grid_Inv.DataBind();
            }
            else if (ddwn_Stats.SelectedItem.Text == "Critical Levels")
            {
                grid_Inv.DataSourceID = null;
                string ss = "select modelcode ,description, color,quantity,status from systemmodelstbl where status = 'Critical'";
                SqlDataAdapter mz = new SqlDataAdapter(ss, hpiconn);
                DataSet set = new DataSet();
                mz.Fill(set);
                grid_Inv.DataSource = set.Tables[0];
                grid_Inv.DataBind();
            }
            else if (ddwn_Stats.SelectedItem.Text == "Out of Stock")
            {
                grid_Inv.DataSourceID = null;
                string ss = "select modelcode ,description, color,quantity,status from systemmodelstbl where status = 'Out'";
                SqlDataAdapter mz = new SqlDataAdapter(ss, hpiconn);
                DataSet set = new DataSet();
                mz.Fill(set);
                grid_Inv.DataSource = set.Tables[0];
                grid_Inv.DataBind();
            }
            hpiconn.Close();
        }
        string bago = "select Status from SysteminboxTBL where status = 'new'";
      //  string sel2 = "select Status from SystemPOPartsTBL where status = 'unseen'";
        protected void Timer9_Tick(object sender, EventArgs e)
        {
            SqlCommand com = new SqlCommand(bago, hpiconn);
            SqlDataReader r;
            hpiconn.Open();
            r = com.ExecuteReader();
            int a = 0;
            while (r.Read())
            {

                string not = r.GetString(0).ToString();//secon row
                if (not == "New")
                    a++;
            }
            hpiconn.Close();

            //SqlCommand coma = new SqlCommand(sel2, hpiconn);
            //IDataReader ra;
            //hpiconn.Open();
            //ra = coma.ExecuteReader();
            //int aa = 0;
            //while (ra.Read())
            //{
            //    string not = ra.GetString(0).ToString();
            //    if (not == "UNSEEN")
            //        aa++;
            //}
            //hpiconn.Close();

            int ax = a/* + aa*/;

            if (a != 0 /*|| aa != 0*/)
            {
                lbl_notifN0.Visible = !true;
                lbl_NumberNewPO0.Visible = true;
                lbl_NumberNewPO0.Text = ax.ToString();
            }
            else if (a == 0/* || aa == 0*/)
            {
                lbl_notifN0.Visible = true;
                lbl_NumberNewPO0.Visible = false;
            }
        }

        protected void Button26_Click1(object sender, EventArgs e)
        {
            

        }

        protected void btn_EditCrit_Click(object sender, EventArgs e)
        {
            txt_Reorder.ReadOnly = false;
            txt_Crit.ReadOnly = false;

            btn_SaveCrit.Visible = !false;
            btn_CancelCrit.Visible = !false;
            btn_EditCrit.Visible = false;

            ViewState["data1"] = txt_Reorder.Text;
            ViewState["data2"] = txt_Crit.Text;

            stable();
        }

        protected void btn_SaveCrit_Click(object sender, EventArgs e)
        {
            string upds = "update systeminventorysettingtbl set reorder = '" + txt_Reorder.Text + "', critical = '" + txt_Crit.Text + "' where id = '1'";
            SqlCommand dcs = new SqlCommand(upds, hpiconn);
            hpiconn.Open();
            dcs.ExecuteNonQuery();
            hpiconn.Close();

            txt_Reorder.ReadOnly = !false;
            txt_Crit.ReadOnly = !false;

            btn_SaveCrit.Visible = false;
            btn_CancelCrit.Visible = false;
            btn_EditCrit.Visible = !false;

            Update();
            stable();
        }

        protected void btn_CancelCrit_Click(object sender, EventArgs e)
        {
            txt_Reorder.Text = ViewState["data1"].ToString();
            txt_Crit.Text = ViewState["data2"].ToString();

            txt_Reorder.ReadOnly = !false;
            txt_Crit.ReadOnly = !false;

            btn_SaveCrit.Visible = false;
            btn_CancelCrit.Visible = false;
            btn_EditCrit.Visible = !false;
        }

        protected void btn_Setting_Click(object sender, EventArgs e)
        {
            lbl_reOrder.Visible = true;
            txt_Reorder.Visible = true;
            lbl_CretLev.Visible = true;
            txt_Crit.Visible = true;
            btn_EditCrit.Visible = true;
            btn_Close.Visible = true;

            stable();
        }

        protected void btn_Close_Click(object sender, EventArgs e)
        {
            lbl_reOrder.Visible = !true;
            txt_Reorder.Visible = !true;
            lbl_CretLev.Visible = !true;
            txt_Crit.Visible = !true;
            btn_EditCrit.Visible = !true;
            btn_SaveCrit.Visible = false;
            btn_CancelCrit.Visible = false;

            txt_Reorder.ReadOnly = !false;
            txt_Crit.ReadOnly = !false;
            btn_Close.Visible = !true;

            stable();
        }

        protected void grid_invoices_SelectedIndexChanged(object sender, EventArgs e)
        {
             string ppp = ViewState["ponn"].ToString();
             string inv = grid_invoices.SelectedRow.Cells[1].Text;
             recsconn.Open();
             string mq = "select ponumber,invoiceno,dateReceived,deliverydate,subtotal,discount,discountedamount,tax,vat,servicecharge,totalservicecharge,total from systemrecordcredentialstbl where ponumber = '" + ppp + "' and invoiceno = '" + inv + "'";
             SqlCommand sir = new SqlCommand(mq, recsconn);
             SqlDataReader d;
             d = sir.ExecuteReader();
             d.Read();

             ViewState["po"] = d.GetString(0).ToString();
             ViewState["inv"] = d.GetString(1).ToString();
             ViewState["dr"] = d.GetString(2).ToString();
             // ViewState["dc"] = d.GetString(3).ToString();//
             ViewState["dd"] = d.GetString(3).ToString();
             ViewState["st"] = d.GetString(4).ToString();
             ViewState["dis"] = d.GetString(5).ToString();
             ViewState["da"] = d.GetString(6).ToString();
             ViewState["tax"] = d.GetString(7).ToString();
             ViewState["vat"] = d.GetString(8).ToString();
             ViewState["sc"] = d.GetString(9).ToString();
             ViewState["tsc"] = d.GetString(10).ToString();
             ViewState["total"] = d.GetString(11).ToString();
             recsconn.Close();

             string update = "update systemrecordcredentialstbl set status = 'Viewed' where ponumber = '" + ppp + "' and invoiceno = '" + inv + "'";
             SqlCommand up = new SqlCommand(update, recsconn);
             recsconn.Open();
             up.ExecuteNonQuery();
             recsconn.Close();

             //salesinvoice
             lbl_PONumber.Text = ViewState["po"].ToString();
             lbl_SalesInvoice.Text = ViewState["inv"].ToString();
             lbl_DateReceived.Text = ViewState["dr"].ToString();
             lbl_Delivery.Text = ViewState["dd"].ToString();

             recsconn.Open();

             string ass2 = "select  ModelCode,Description,Color,Quantity,BackOrders,Purchasable,UnitPrice,Discount,Amount from a" + inv + "_Units";
             SqlDataAdapter adapter2 = new SqlDataAdapter(ass2, recsconn);
             DataSet set2 = new DataSet();
             adapter2.Fill(set2);
             grid_aSI.DataSource = set2.Tables[0];
             grid_aSI.DataBind();
             recsconn.Close();


             grid_bSI.DataSource = null;
             grid_bSI.DataBind();
             try
             {
                 recsconn.Open();
                 string bass2 = "select  ModelCode,Description,Color,Quantity,BackOrders,Purchasable,UnitPrice,Discount,Amount from b" + inv + "_Units";
                 SqlDataAdapter badapter2 = new SqlDataAdapter(bass2, recsconn);
                 DataSet bset2 = new DataSet();
                 badapter2.Fill(bset2);
                 grid_bSI.DataSource = bset2.Tables[0];
                 grid_bSI.DataBind();
                 recsconn.Close();
                 lbl_AddedItems.Visible = true;
             }
             catch
             {

             }

             txt_sTotal.Text = ViewState["st"].ToString();
             txt_Disc.Text = ViewState["dis"].ToString();
             txt_DiscAmount.Text = ViewState["da"].ToString();
             lbl_Vatt.Text = ViewState["tax"].ToString();
             txt_Vatt.Text = ViewState["vat"].ToString();
             txt_SCharge.Text = ViewState["sc"].ToString();
             txt_TotalsCharge.Text = ViewState["tsc"].ToString();
             txt_GTotal.Text = ViewState["total"].ToString();
             //salesinvoice


             MultiView6.ActiveViewIndex = 1;
        }

        protected void btn_haulerAdd_Click(object sender, EventArgs e)
        {
            lbl_haulerName.Visible = true;
            txt_AddHauler.Visible = true;
            lbl_haulerPlate.Visible = true;
            txt_AddPlate.Visible = true;
            btn_SaveHauler.Visible = true;
            btn_CancelHauler.Visible = true;

            txt_EditHauler.Visible = !true;
            txt_EditPlate.Visible = !true;
            btn_UpdateHauler.Visible = !true;

            txt_EditHauler.Text = "";
            txt_EditPlate.Text = "";
        }

        protected void btn_haulerEdit_Click(object sender, EventArgs e)
        {
            string haul = "";
            string plat = ""; 
            try
            {
                haul = ViewState["name"].ToString();
                plat = ViewState["plate"].ToString();
            }
            catch { }

            if (haul != "" && plat != "")
            {
                txt_EditHauler.Text = haul;
                txt_EditPlate.Text = plat;

                lbl_haulerName.Visible = true;
                txt_EditHauler.Visible = true;
                lbl_haulerPlate.Visible = true;
                txt_EditPlate.Visible = true;
                btn_UpdateHauler.Visible = true;
                btn_CancelHauler.Visible = true;

                txt_AddHauler.Visible = !true;
                txt_AddPlate.Visible = !true;
                btn_SaveHauler.Visible = !true;

                txt_AddHauler.Text = "";
                txt_AddPlate.Text = "";
            }

            
        }

        protected void btn_CancelHauler_Click(object sender, EventArgs e)
        {
            canceller();
        }

        private void canceller()
        {
            lbl_haulerName.Visible = !true;
            txt_AddHauler.Visible = !true;
            txt_EditHauler.Visible = !true;
            lbl_haulerPlate.Visible = !true;
            txt_AddPlate.Visible = !true;
            txt_EditPlate.Visible = !true;
            btn_SaveHauler.Visible = !true;
            btn_UpdateHauler.Visible = !true;
            btn_CancelHauler.Visible = !true;

            txt_AddHauler.Text = "";
            txt_AddPlate.Text = "";
            txt_EditHauler.Text = "";
            txt_EditPlate.Text = "";
        }

        protected void btn_SaveHauler_Click(object sender, EventArgs e)
        {
            bool tru = false;
            string addhauler = "";
            string addplate = "";
            int nums = grid_Haulers.Rows.Count;
            if (nums == 0)
            {
                nums += 1;
            }
            for (int h = 0; h <= nums - 1; h++)
            {
                string hauler = "";
                string plate = "";
                try
                {
                    hauler = grid_Haulers.Rows[h].Cells[2].Text;
                    plate = grid_Haulers.Rows[h].Cells[3].Text;
                }
                catch
                {
                    hauler = null;
                    plate = null;
                }
                addhauler = txt_AddHauler.Text;
                addplate = txt_AddPlate.Text;

                if (hauler == "" || hauler == null || plate == "" || plate == null)
                {
                    adders(addhauler, addplate);
                    break;
                }
                else
                {
                    string comp = "select hauler,plate from systemhaulerstbl where s_number = '" + (h + 1) + "'";
                    SqlCommand read = new SqlCommand(comp,hpiconn);
                    SqlDataReader red;
                    hpiconn.Open();
                    red = read.ExecuteReader();
                    red.Read();
                    string data1 = red.GetString(0);
                    string data2 = red.GetString(1);
                    hpiconn.Close();
                    if (data1 == addhauler && data2 == addplate)
                    {
                        tru = !true;
                        break;
                    }
                    else
                    {
                        tru = true;
                    }
                }
            }
            if (tru == true)
            {
                //add
                adders(addhauler, addplate);
            }

            //txt_AddHauler.Text = "";
            //txt_AddPlate.Text = "";

            resetter();
            canceller();
            grid_Haulers.DataBind();
        }

        private void resetter()
        {
            //reset s_number
            hpiconn.Open();
            string removers = "UPDATE systemhaulerstbl SET s_number = NULL";
            SqlCommand mss = new SqlCommand(removers, hpiconn);
            mss.ExecuteNonQuery();

            string snumbers = "With cte As(Select id, s_number,  Row_Number() Over (Order By s_number) As rn From systemhaulerstbl)Update cte Set s_number = rn";
            SqlCommand ssqus = new SqlCommand(snumbers, hpiconn);

            ssqus.ExecuteNonQuery();
            hpiconn.Close();
            //reset s_number
        }

        private void adders(string addh,string addp)
        {
            string addhauler = addh;
            string addplate = addp;
            string add = "insert into systemhaulerstbl (hauler,plate)values('" + addhauler + "','" + addplate + "')";
            SqlCommand adds = new SqlCommand(add, hpiconn);
            hpiconn.Open();
            adds.ExecuteNonQuery();
            hpiconn.Close();
            grid_Haulers.DataBind();

            //reset s_number
            hpiconn.Open();
            string removers = "UPDATE systemhaulerstbl SET s_number = NULL";
            SqlCommand mss = new SqlCommand(removers, hpiconn);
            mss.ExecuteNonQuery();

            string snumbers = "With cte As(Select id, s_number,  Row_Number() Over (Order By s_number) As rn From systemhaulerstbl)Update cte Set s_number = rn";
            SqlCommand ssqus = new SqlCommand(snumbers, hpiconn);

            ssqus.ExecuteNonQuery();
            hpiconn.Close();
            //reset s_number
        }

        protected void btn_UpdateHauler_Click(object sender, EventArgs e)
        {
            if (txt_EditHauler.Text != "" || txt_EditHauler.Text != null && txt_EditPlate.Text != "" || txt_EditPlate.Text != null)
            {
                string addhauler = "";
                string addplate = "";
                string updateN = ViewState["s_number"].ToString();
                string updateH = ViewState["name"].ToString();
                string updateP = ViewState["plate"].ToString();
                bool tru = false;
                int nums = grid_Haulers.Rows.Count;
                for (int h = 0; h <= nums - 1; h++)
                {
                    string comp = "select hauler,plate from systemhaulerstbl where s_number = '" + (h + 1) + "'";
                    SqlCommand read = new SqlCommand(comp, hpiconn);
                    SqlDataReader red;
                    hpiconn.Open();
                    red = read.ExecuteReader();
                    red.Read();
                    string data1 = red.GetString(0);
                    string data2 = red.GetString(1);
                    hpiconn.Close();
                    if (data1 == updateH && data2 == updateP)
                    {
                        tru = !true;
                        break;
                    }
                    else
                    {
                        tru = true;
                    }
                }
                if (tru == true)
                {
                    string updates = "update systemhaulerstbl set hauler = '" + txt_EditHauler.Text + "', plate = '" + txt_EditPlate.Text + "' where s_number = '" + updateN + "'";
                    SqlCommand aps = new SqlCommand(updates, hpiconn);
                    hpiconn.Open();
                    aps.ExecuteNonQuery();
                    hpiconn.Close();
                    

                    ViewState["s_number"] = null;
                    ViewState["name"] = null;
                    ViewState["plate"] = null;
                    resetter();
                    grid_Haulers.DataBind();
                    canceller();
                }
                
            }
            
        }

        protected void grid_Haulers_SelectedIndexChanged(object sender, EventArgs e)
        {
            ViewState["s_number"] = grid_Haulers.SelectedRow.Cells[1].Text;
            ViewState["name"] = grid_Haulers.SelectedRow.Cells[2].Text;
            ViewState["plate"] = grid_Haulers.SelectedRow.Cells[3].Text;

            if (txt_EditHauler.Visible == true)
            {
                txt_EditHauler.Text = ViewState["name"].ToString();
                txt_EditPlate.Text = ViewState["plate"].ToString();
            }
        }

        protected void btn_haulerRemove_Click(object sender, EventArgs e)
        {
            string remove = ViewState["s_number"].ToString();
            if (remove != null)
            {
                string remover = "delete from systemhaulerstbl where s_number = '" + remove + "'";
                SqlCommand hj = new SqlCommand(remover, hpiconn);
                hpiconn.Open();
                hj.ExecuteNonQuery();
                hpiconn.Close();
                resetter();
                grid_Haulers.DataBind();
                
                canceller();
            }
        }


        protected void grid_DeliveryList_SelectedIndexChanged(object sender, EventArgs e)
        {
            string pons = ViewState["ponn"].ToString();
            string delnum = grid_DeliveryList.SelectedRow.Cells[1].Text;
            recsconn.Open();
            string mq = "select ponumber,deliveryno,dateReceived,deliverydate,status,hauler,plateno,gatepassno,note from systemrecordcredentialstbl where ponumber = '" + pons + "' and deliveryno = '" + delnum + "'";
            SqlCommand sir = new SqlCommand(mq, recsconn);
            SqlDataReader d;
            d = sir.ExecuteReader();
            d.Read();

            ViewState["po"] = d.GetString(0).ToString();
            ViewState["delnos"] = d.GetString(1).ToString();
            ViewState["dre"] = d.GetString(2).ToString();
            ViewState["deld"] = d.GetString(3).ToString();
            ViewState["sttat"] = d.GetString(4).ToString();
            try
            {
                ViewState["hauler"] = d.GetString(5).ToString();
                ViewState["platenumber"] = d.GetString(6).ToString();
                ViewState["getpass"] = d.GetString(7).ToString();
                ViewState["note"] = d.GetString(8).ToString();
            }
            catch
            {
                ViewState["hauler"] = null;
                ViewState["platenumber"] = null;
                ViewState["getpass"] = null;
                ViewState["note"] = null;
            }

            recsconn.Close();

            //must reset status

            //delivery
            txt_delPO.Text = ViewState["po"].ToString();
            txt_poDate.Text = ViewState["dre"].ToString();
            txt_delDate.Text = ViewState["deld"].ToString();
            txt_DelNumber.Text = ViewState["delnos"].ToString();

            string refdel = delnum.Replace(" ","");

            recsconn.Open();
            string ass2 = "select  ModelCode,Color,Quantity from a" + refdel + "_Units";
            SqlDataAdapter adapter2 = new SqlDataAdapter(ass2, recsconn);
            DataSet set2 = new DataSet();
            adapter2.Fill(set2);
            grid_Delivery1.DataSource = set2.Tables[0];
            grid_Delivery1.DataBind();
            recsconn.Close();

            try
            {
                recsconn.Open();
                string bass2 = "select  ModelCode,Color,Quantity from b" + refdel + "_Units";
                SqlDataAdapter badapter2 = new SqlDataAdapter(bass2, recsconn);
                DataSet bset2 = new DataSet();
                badapter2.Fill(bset2);
                grid_Delivery2.DataSource = bset2.Tables[0];
                grid_Delivery2.DataBind();
                recsconn.Close();
                lbl_Addeds.Visible = true;
            }
            catch
            {
 
            }

            if (ViewState["hauler"] == null)
            {
                btn_Set.Enabled = !false;
                btn_Confirm.Enabled = !false;

                txt_hauler.Text = "(None)";
                txt_plate.Text = "(None)";
                txt_GatePass.Text = "(None)";
            }
            else
            {
                txt_hauler.Text = ViewState["hauler"].ToString();
                txt_plate.Text = ViewState["platenumber"].ToString();
                txt_GatePass.Text = ViewState["getpass"].ToString();

                btn_Set.Enabled = false;
                btn_Confirm.Enabled = false;
            }
            //delivery
            //note
            if (ViewState["note"] != null)
            {
                lbl_Note.Visible = true;
                lbl_NoteContent.Text = ViewState["note"].ToString();
                lbl_NoteContent.Visible = true;
            }
            else
            {
                lbl_Note.Visible = !true;
                lbl_NoteContent.Visible = !true;
            }
            //note
            //haulers
           
            //settings
            lbl_SelectHauler.Visible = !true;
            ddwn_haulers.Visible = !true;
            lbl_SelectPlate.Visible = !true;
            ddwn_plates.Visible = !true;
            lbl_Include.Visible = !true;
            checks_Notes.Visible = !true;
            //settings


            MultiView7.ActiveViewIndex = 1;
        }

        protected void txt_DelNumber_TextChanged(object sender, EventArgs e)
        {

        }

        protected void btn_AddGPass_Click(object sender, EventArgs e)
        {
            //Random rnd = new Random();
            //int num1 = rnd.Next(100000,999999);
            //txt_GatePass.Text = DateTime.Now.Year.ToString() + "-" + num1.ToString() + " " + txt_DelNumber.Text[12].ToString() + txt_DelNumber.Text[13].ToString();
        }

        protected void ddwn_haulers_SelectedIndexChanged(object sender, EventArgs e)
        {
            txt_hauler.Text = ddwn_haulers.SelectedItem.Text;
            txt_plate.Text = "(None)";
            txt_GatePass.Text = "(None)";
            if (txt_hauler.Text == "(None)")
            {
                txt_plate.Text = "(None)";
                txt_GatePass.Text = "(None)";
            }
             //plate number load
            ddwn_plates.Items.Clear();
            string queryp = "select plate from SystemHaulersTBL where hauler = '" + txt_hauler.Text + "'";

            SqlCommand cmdp = new SqlCommand(queryp, hpiconn);
            hpiconn.Open();
            SqlDataReader drp;
            drp = cmdp.ExecuteReader();
            ddwn_plates.Items.Add("(None)");
            if (drp.HasRows)
            {
                while (drp.Read())
                {
                    ddwn_plates.Items.Add(drp[0].ToString());
                }
            }
            hpiconn.Close();
            //plate number load
        }

        protected void ddwn_plates_SelectedIndexChanged(object sender, EventArgs e)
        {
            txt_plate.Text = ddwn_plates.SelectedItem.Text;
            if (txt_plate.Text == "(None)")
            {
                txt_GatePass.Text = "(None)";
            }
            else 
            {
                Random rnd = new Random();
                int num1 = rnd.Next(100000, 999999);
                txt_GatePass.Text = DateTime.Now.Year.ToString() + "-" + num1.ToString() + " " + txt_DelNumber.Text[12].ToString() + txt_DelNumber.Text[13].ToString();
            }
        }

        protected void btn_Set_Click(object sender, EventArgs e)
        {
            lbl_SelectHauler.Visible = true;
            ddwn_haulers.Visible = true;
            lbl_SelectPlate.Visible = true;
            ddwn_plates.Visible = true;
         //   btn_ok.Visible = true;
            lbl_Include.Visible = true;
            checks_Notes.Visible = true;

            //hauler load
            ddwn_haulers.Items.Clear();
            string query = "select distinct hauler from SystemHaulersTBL";

            SqlCommand cmd = new SqlCommand(query, hpiconn);
            hpiconn.Open();
            SqlDataReader dr;
            dr = cmd.ExecuteReader();
            ddwn_haulers.Items.Add("(None)");
            ddwn_plates.Items.Add("(None)");
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    ddwn_haulers.Items.Add(dr[0].ToString());
                }
            }
            hpiconn.Close();
            //hauler load
        }

        protected void btn_ok_Click(object sender, EventArgs e)
        {
            //lbl_SelectHauler.Visible = !true;
            //ddwn_haulers.Visible = !true;
            //lbl_SelectPlate.Visible = !true;
            //ddwn_plates.Visible = !true;
            //btn_ok.Visible = !true;

            //ddwn_plates.Items.Clear();
            //ddwn_haulers.Items.Clear();
        }

        protected void btn_Confirm_Click(object sender, EventArgs e)
        {
            string note = "";
            foreach (ListItem li in checks_Notes.Items)
            {
                if (li.Selected)
                {
                    note += li.Text + "</br>";
                }
            }


            string haul = txt_hauler.Text;
            string plates = txt_plate.Text;
            string gatePass = txt_GatePass.Text;

            if (haul == "" || haul == null || plates == "" || plates == null || gatePass == "" || gatePass == null)
            {
                //do nothing
            }
            else if (haul != "(None)" || plates != "(None)" || gatePass != "(None)")
            {
                string apdet = "update systemrecordcredentialstbl set hauler = '" + haul + "', plateno = '" + plates + "',gatepassno = '" + gatePass + "',note = '" + note + "' where ponumber = '" + txt_delPO.Text + "' and deliveryno = '" + txt_DelNumber.Text + "'";
                SqlCommand kj = new SqlCommand(apdet, recsconn);
                recsconn.Open();
                kj.ExecuteNonQuery();
                recsconn.Close();

                btn_Set.Enabled = false;
                btn_Confirm.Enabled = false;

                lbl_SelectHauler.Visible = !true;
                ddwn_haulers.Visible = !true;
                lbl_SelectPlate.Visible = !true;
                ddwn_plates.Visible = !true;
                //  btn_ok.Visible = !true;

                ddwn_plates.Items.Clear();
                ddwn_haulers.Items.Clear();
                lbl_NoteContent.Text = note;
                lbl_NoteContent.Visible = true;
                lbl_Note.Visible = true;

                lbl_Include.Visible = !true;
                checks_Notes.Visible = !true;

                lbl_SelectHauler.Visible = !true;
                ddwn_haulers.Visible = !true;
                lbl_SelectPlate.Visible = !true;
                ddwn_plates.Visible = !true;
                //    btn_ok.Visible = !true;

                ddwn_plates.Items.Clear();
                ddwn_haulers.Items.Clear();
            }
            //SEND REPORTS TO HEAD OFFICE AND MAGNA
            string transfer = "select category,ponumber,invoiceno,deliveryno,datereceived,datecreated,deliverydate,status,subtotal,discount,discountedamount,tax,vat,servicecharge,totalservicecharge,total,hauler,plateno,gatepassno,note from systemrecordcredentialstbl where ponumber = '" + txt_delPO.Text + "' and deliveryno = '" + txt_DelNumber.Text + "'";//,amountpaid,paymentremarks,
            SqlCommand trans = new SqlCommand(transfer,recsconn);
            SqlDataReader hh;
            recsconn.Open();
            hh = trans.ExecuteReader();
            hh.Read();
            string category = hh.GetString(0).ToString();
            string ponumber = hh.GetString(1).ToString();
            string invoiceno = hh.GetString(2).ToString();
            string deliveryno = hh.GetString(3).ToString();
            string datereceived = hh.GetString(4).ToString();
            string datecreated = hh.GetString(5).ToString();
            string deliverydate = hh.GetString(6).ToString();
            string status = hh.GetString(7).ToString();
            string subtotal = hh.GetString(8).ToString();
            string discount = hh.GetString(9).ToString();
            string discountedamount = hh.GetString(10).ToString();
            string tax = hh.GetString(11).ToString();
            string vat = hh.GetString(12).ToString();
            string servicecharge = hh.GetString(13).ToString();
            string totalservicecharge = hh.GetString(14).ToString();
            string total = hh.GetString(15).ToString();
            //string amountpaid = hh.GetString(16).ToString();
            //string paymentremarks = hh.GetString(16).ToString();
            string hauler = null;
            string plateno = null;
            string gatepassno = null;
            string notes = null;
            try
            {
                hauler = hh.GetString(16).ToString();
                plateno = hh.GetString(17).ToString();
                gatepassno = hh.GetString(18).ToString();
                notes = hh.GetString(19).ToString();
            }
            catch
            {
                hauler = null;
                plateno = null;
                gatepassno = null;
                notes = null;
            }

            
            recsconn.Close();

            magnaconn.Open();
            string insert = "insert into systemreportsTBL (category,ponumber,invoiceno,deliveryno,datereceived,datecreated,deliverydate,status,subtotal,discount,discountedamount,tax,vat,servicecharge,totalservicecharge,total,hauler,plateno,gatepassno,note)values(@a,@b,@c,@d,@e,@f,@g,@h,@i,@j,@k,@l,@m,@n,@o,@p,@s,@t,@u,@v)";//,amountpaid,paymentremarks,,@q,@r,
            SqlCommand comm = new SqlCommand(insert, magnaconn);
            comm.Parameters.AddWithValue("@a", category);
            comm.Parameters.AddWithValue("@b", ponumber);
            comm.Parameters.AddWithValue("@c", invoiceno);
            comm.Parameters.AddWithValue("@d", deliveryno);
            comm.Parameters.AddWithValue("@e", datereceived);
            comm.Parameters.AddWithValue("@f", datecreated);
            comm.Parameters.AddWithValue("@g", deliverydate);
            comm.Parameters.AddWithValue("@h", status);
            comm.Parameters.AddWithValue("@i", subtotal);
            comm.Parameters.AddWithValue("@j", discount);
            comm.Parameters.AddWithValue("@k", discountedamount);
            comm.Parameters.AddWithValue("@l", tax);
            comm.Parameters.AddWithValue("@m", vat);
            comm.Parameters.AddWithValue("@n", servicecharge);
            comm.Parameters.AddWithValue("@o", totalservicecharge);
            comm.Parameters.AddWithValue("@p", total);
            //comm.Parameters.AddWithValue("@q", amountpaid);
            //comm.Parameters.AddWithValue("@r", paymentremarks);
            comm.Parameters.AddWithValue("@s", hauler);
            comm.Parameters.AddWithValue("@t", plateno);
            comm.Parameters.AddWithValue("@u", gatepassno);
            comm.Parameters.AddWithValue("@v", notes);
            comm.ExecuteNonQuery();
            magnaconn.Close();

            //reset s_number
            magnaconn.Open();
            string removers = "UPDATE systemReportstbl SET s_number = NULL";
            SqlCommand mss = new SqlCommand(removers, magnaconn);
            mss.ExecuteNonQuery();

            string snumbers = "With cte As(Select id, s_number,  Row_Number() Over (Order By s_number) As rn From systemReportstbl)Update cte Set s_number = rn";
            SqlCommand ssqus = new SqlCommand(snumbers, magnaconn);

            ssqus.ExecuteNonQuery();
            magnaconn.Close();
            //reset s_number
            //SEND REPORTS TO HEAD OFFICE AND MAGNA
        }

        protected void btn_CancelProcess_Click(object sender, EventArgs e)
        {
            txt_SalesInvoice.Text = "";
            txt_DeliveryDate.Text = "";

            lbl_SI.Visible = false;
            txt_SalesInvoice.Visible = false;
            lbl_DeliveryDate.Visible = false;
            txt_DeliveryDate.Visible = false;
            btn_Cal.Visible = false;
            cal_Calendar.Visible = false;
            btn_Proceed.Visible = false;

            txt_Total.Text = "";
            txt_DiscountedAmount.Text = "";
            txt_Vats.Text = "";
            txt_ServiceCharge.Text = "";
            txt_TotalCharge.Text = "";
            txt_GrandTotal.Text = "";

            MultiView1.ActiveViewIndex = 0;

            //droping of SI table
            string inv = ViewState["invoice"].ToString();
            string drop = "drop table a" + inv + "_units";
            SqlCommand o = new SqlCommand(drop,recsconn);
            recsconn.Open();
            o.ExecuteNonQuery();
            recsconn.Close();

            try
            {
                //string inv = ViewState["invoice"].ToString();
                string dropb = "drop table b" + inv + "_units";
                SqlCommand ob = new SqlCommand(dropb, recsconn);
                recsconn.Open();
                ob.ExecuteNonQuery();
                recsconn.Close();
            }
            catch
            {
 
            }
            //droping of SI table
        }

        protected void grid_Billing_SelectedIndexChanged(object sender, EventArgs e)
        {
            string pons = ViewState["ponn"].ToString();
            string invnum = grid_Billing.SelectedRow.Cells[1].Text;
            ViewState["pons"] = pons;
            ViewState["invnum"] = invnum;
            recsconn.Open();
            string mq = "select ponumber,dateReceived,total,paymentremarks,discountedamount,tax,vat,totalservicecharge,invoiceno from systemrecordcredentialstbl where ponumber = '" + pons + "' and invoiceno = '" + invnum + "'";
            SqlCommand sir = new SqlCommand(mq, recsconn);
            SqlDataReader d;
            d = sir.ExecuteReader();
            d.Read();

            ViewState["po"] = d.GetString(0).ToString();
            ViewState["date"] = d.GetString(1).ToString();
            ViewState["totbill"] = d.GetString(2).ToString();
            try
            {
                //ViewState["paid"] = d.GetString(3).ToString();
                ViewState["Premarks"] = d.GetString(3).ToString();
            }
            catch
            {
                ViewState["paid"] = null;
                ViewState["Premarks"] = null;
            }
            ViewState["rowamount"] = d.GetString(4).ToString();
            ViewState["taxs"] = d.GetString(5).ToString();
            ViewState["vats"] = d.GetString(6).ToString();
            ViewState["totsercha"] = d.GetString(7).ToString();
            ViewState["invc"] = d.GetString(8).ToString();


            recsconn.Close();
            //must reset status
            lbl_BillPO.Text = ViewState["po"].ToString();
            lbl_DatePO.Text = ViewState["date"].ToString();
            lbl_InvcNumber.Text = ViewState["invc"].ToString();



            recsconn.Open();

            string ass2 = "select  ModelCode as 'Model Code',Description,Color,Purchasable as 'Quantity',UnitPrice as 'Unit Price',Amount from a" + invnum + "_Units";
            SqlDataAdapter adapter2 = new SqlDataAdapter(ass2, recsconn);
            DataSet set2 = new DataSet();
            adapter2.Fill(set2);
            grid_BillingA.DataSource = set2.Tables[0];
            grid_BillingA.DataBind();
            recsconn.Close();


            grid_BillingB.DataSource = null;
            grid_BillingB.DataBind();
            try
            {
                recsconn.Open();
                string bass2 = "select  ModelCode as 'Model Code',Description,Color,Purchasable as 'Quantity',UnitPrice as 'Unit Price',Amount from b" + invnum + "_Units";
                SqlDataAdapter badapter2 = new SqlDataAdapter(bass2, recsconn);
                DataSet bset2 = new DataSet();
                badapter2.Fill(bset2);
                grid_BillingB.DataSource = bset2.Tables[0];
                grid_BillingB.DataBind();
                recsconn.Close();
                lbl_Adde.Visible = true;
            }
            catch
            {

            }

            txt_subtotal.Text = ViewState["rowamount"].ToString();
            lbl_Taxx.Text = ViewState["taxs"].ToString();
            txt_taxs.Text = ViewState["vats"].ToString();
            txt_shiping.Text = ViewState["totsercha"].ToString();
            txt_tots.Text = ViewState["totbill"].ToString();
            recsconn.Close();

            

          

            MultiView8.ActiveViewIndex = 1;
            MultiView9.ActiveViewIndex = 0;
        }

        protected void btn_Bill_Click(object sender, EventArgs e)
        {
            MultiView9.ActiveViewIndex = 0;
        }

        protected void btn_Payment_Click(object sender, EventArgs e)
        {
            recsconn.Open();
            string mq = "select amountpaid from systemrecordcredentialstbl where ponumber = '" + ViewState["pons"].ToString() + "' and invoiceno = '" + ViewState["invnum"].ToString()+ "'";
            SqlCommand sir = new SqlCommand(mq, recsconn);
            SqlDataReader d;
            d = sir.ExecuteReader();
            d.Read();
            ViewState["paid"] = d.GetString(0).ToString();
            recsconn.Close();

            lbl_POnum.Text = ViewState["po"].ToString();
            lbl_DateRec.Text = ViewState["date"].ToString();
            lbl_InvNum.Text = ViewState["invc"].ToString();
            lbl_TotalBill.Text = "₱ "+ViewState["totbill"].ToString();
            string paid = ViewState["paid"].ToString();
            string qt1 = Convert.ToDecimal(paid).ToString("#,##0.00");
            txt_DiscountedAmount.Text = qt1.ToString();
            lbl_AmountPaid.Text = "₱ " + qt1;
            lbl_Remarks.Text = ViewState["Premarks"].ToString();


            if (lbl_Remarks.Text == "Paid")
            {
                btn_UpdatePayment.Enabled = false;
            }
            else
            {
                btn_UpdatePayment.Enabled = !false;
            }
                
            MultiView9.ActiveViewIndex = 1;
        }

        protected void btn_UpdatePayment_Click(object sender, EventArgs e)
        {
            txt_Payments.Visible = true;
            btn_AddPayment.Visible = true;
            Cancel_Payment.Visible = true;
            btn_UpdatePayment.Visible = !true;
        }
        
        protected void btn_AddPayment_Click(object sender, EventArgs e)
        {
            string paid = "select amountPaid from systemrecordcredentialstbl where ponumber = '" + lbl_POnum.Text + "' and invoiceno = '" + lbl_InvNum.Text+ "'";
            SqlCommand jj = new SqlCommand(paid,recsconn);
            SqlDataReader rr;
            recsconn.Open();
            rr = jj.ExecuteReader();
            rr.Read();
            string amount = rr.GetString(0);
            recsconn.Close();
            string answer = "";
            if (!(String.IsNullOrEmpty(txt_Payments.Text)))
            {
                answer = (Convert.ToDouble(amount) + Convert.ToDouble(txt_Payments.Text)).ToString();
            }
            else
            {
                answer = amount;
            }
            string qe = Convert.ToDecimal(answer).ToString("#,##0.00");
            string add = "update systemrecordcredentialstbl set amountpaid = '" + qe + "' where ponumber = '" + lbl_POnum.Text + "' and invoiceno = '" + lbl_InvNum.Text + "'";
            SqlCommand po = new SqlCommand(add,recsconn);
            recsconn.Open();
            po.ExecuteNonQuery();
            recsconn.Close();

            string newamount = "select amountpaid from systemrecordcredentialstbl where ponumber = '" + lbl_POnum.Text + "' and invoiceno = '" + lbl_InvNum.Text + "'";
            SqlCommand ff = new SqlCommand(newamount,recsconn);
            SqlDataReader oo;
            recsconn.Open();
            oo = ff.ExecuteReader();
            oo.Read();
            string newamt = oo.GetString(0);
            recsconn.Close();

           // ViewState["paid"] = newamt;
            string qt1 = Convert.ToDecimal(newamt).ToString("#,##0.00");
            txt_DiscountedAmount.Text = qt1.ToString();
            lbl_AmountPaid.Text = "₱ " + qt1;


            hidder();
            btn_UpdatePayment.Visible = true;
            txt_Payments.Text = "";


            double total = Convert.ToDouble(ViewState["totbill"]);
              
            if (Convert.ToDouble(newamt) > total)
            {
                string rem = "update systemrecordcredentialstbl set amountPaid = '"+total+"' where ponumber = '" + lbl_POnum.Text + "' and invoiceno = '" + lbl_InvNum.Text + "'";
                SqlCommand rema = new SqlCommand(rem, recsconn);
                recsconn.Open();
                rema.ExecuteNonQuery();
                recsconn.Close();

                remark();
            }
            else if (lbl_AmountPaid.Text != "₱ 0.00" && lbl_AmountPaid.Text != lbl_TotalBill.Text)
            {
                string rem = "update systemrecordcredentialstbl set paymentremarks = 'Partially Paid' where ponumber = '" + lbl_POnum.Text + "' and invoiceno = '" + lbl_InvNum.Text + "'";
                SqlCommand rema = new SqlCommand(rem, recsconn);
                recsconn.Open();
                rema.ExecuteNonQuery();
                recsconn.Close(); 
                
                remark();
            }
            else if (lbl_AmountPaid.Text == lbl_TotalBill.Text)
            {
                string rem = "update systemrecordcredentialstbl set paymentremarks = 'Paid' where ponumber = '" + lbl_POnum.Text + "' and invoiceno = '" + lbl_InvNum.Text + "'";
                SqlCommand rema = new SqlCommand(rem, recsconn);
                recsconn.Open();
                rema.ExecuteNonQuery();
                recsconn.Close();

                remark();
            }
            if (lbl_Remarks.Text == "Paid")
            {
                btn_UpdatePayment.Enabled = false;
            }
        }

        private void remark()
        {
            string remarks = "select paymentremarks from systemrecordcredentialstbl where ponumber = '" + lbl_POnum.Text + "' and invoiceno = '" + lbl_InvNum.Text + "'";
            SqlCommand ffs = new SqlCommand(remarks, recsconn);
            SqlDataReader oos;
            recsconn.Open();
            oos = ffs.ExecuteReader();
            oos.Read();
            lbl_Remarks.Text = oos.GetString(0);
            recsconn.Close();
        }
        protected void Cancel_Payment_Click(object sender, EventArgs e)
        {
            hidder();
            btn_UpdatePayment.Visible = true;
            txt_Payments.Text = "";
        }
        private void hidder()
        {
            txt_Payments.Visible = !true;
            btn_AddPayment.Visible = !true;
            Cancel_Payment.Visible = !true;
            
        }

        protected void btn_Stock_Click(object sender, EventArgs e)
        {
            try
            {
                // MultiView1.ActiveViewIndex = 6;
                lbl_SecStockReport.Visible = !true;
                // if (k)
                MultiView1.ActiveViewIndex = 6;
                lbl_PONumba.Text = txt_POnum.Text;

                //1creates column
                DataTable dt = new DataTable();
                dt.Columns.AddRange(new DataColumn[4] { new DataColumn("Model Code"), new DataColumn("Description"), new DataColumn("Color"), new DataColumn("Status") });
                ViewState["orders1"] = dt;
                //2creates column
                DataTable dts = (DataTable)ViewState["orders1"];

                int stock = grid_PO1.Rows.Count;
                for (int count = 0; count <= stock - 1; count++)
                {
                    string data1 = grid_PO1.Rows[count].Cells[0].Text;
                    string data2 = grid_PO1.Rows[count].Cells[2].Text;

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


                int stock = grid_PO1.Rows.Count;
                for (int count = 0; count <= stock - 1; count++)
                {
                    string data1 = grid_PO1.Rows[count].Cells[1].Text;
                    string data2 = grid_PO1.Rows[count].Cells[2].Text;
                    string data3 = grid_PO1.Rows[count].Cells[3].Text;
                    string data4 = "(None)";


                    dts.Rows.Add(data1, data2, data3, data4);

                }


                ViewState["orders1"] = dts;
                grid_StockReport1.DataSource = (DataTable)ViewState["orders1"];
                grid_StockReport1.DataBind();
            }

            try
            {
                //1creates column
                DataTable dt1 = new DataTable();
                dt1.Columns.AddRange(new DataColumn[4] { new DataColumn("Model Code"), new DataColumn("Description"), new DataColumn("Color"), new DataColumn("Status") });
                ViewState["orders2"] = dt1;
                //2creates column
                DataTable dts1 = (DataTable)ViewState["orders2"];

                int stock1 = grid_PO2.Rows.Count;
                for (int count = 0; count <= stock1 - 1; count++)
                {
                    string data1 = grid_PO2.Rows[count].Cells[0].Text;
                    string data2 = grid_PO2.Rows[count].Cells[2].Text;

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
            MultiView1.ActiveViewIndex = 2;
        }

        protected void grid_Drafts_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void btn_DelDraft_Click(object sender, EventArgs e)
        {

        }

        protected void btn_DeleteAll_Click(object sender, EventArgs e)
        {

        }

        protected void btn_Compose_Click(object sender, EventArgs e)
        {

        }

        protected void btn_Upload_Click(object sender, EventArgs e)
        {

        }

        protected void btn_SaveDraft_Click(object sender, EventArgs e)
        {

        }

        protected void btn_SendMess_Click(object sender, EventArgs e)
        {

        }

        protected void btn_messaging_Click(object sender, EventArgs e)
        {
            MultiView1.ActiveViewIndex = 7;
        }

        protected void btn_Inbox_Click(object sender, EventArgs e)
        {
            hpiconn.Open();
            string ss = "select datereceived as 'Date Received',Time from systeminboxtbl";
            SqlDataAdapter mz = new SqlDataAdapter(ss, hpiconn);
            DataSet set = new DataSet();
            mz.Fill(set);
            grid_Inbox.DataSource = set.Tables[0];
            grid_Inbox.DataBind();
            hpiconn.Close();

            txt_MessageContent.Text = "";

            MultiView10.ActiveViewIndex = 0;
        }

        

        


    }
   
}