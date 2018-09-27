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
using System.Web.Services;
using System.Collections.Specialized;
using System.Drawing;
using System.Net;
using System.Net.Mail;//namespace for Regex

namespace HondaMagnacycleProcurementProject
{
    public partial class MagnacycleUser : System.Web.UI.Page
    {
        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["MagnaAdministratorConnectionString"].ConnectionString);
        SqlConnection hpiconn = new SqlConnection(ConfigurationManager.ConnectionStrings["HPILogInConnectionString"].ConnectionString);
        SqlConnection magnaconn = new SqlConnection(ConfigurationManager.ConnectionStrings["MagnacycleConnectionString"].ConnectionString);
        SqlConnection recsconn = new SqlConnection(ConfigurationManager.ConnectionStrings["HPIPORecordConnectionString"].ConnectionString);
        
        protected void Page_Load(object sender, EventArgs e)
        {
            lbl_Uname.Text = Request.QueryString["User"];
            if (ddwn_tab2_Select.SelectedItem.Text == "Units")
            {
                //grid_PurchaseOrderDetails.DataSourceID = "src_SavedUnits";
                //grid_PurchaseOrderDetails.DataBind();
                if (txt_PONumber.Visible == false)
                MultiView4.ActiveViewIndex = 0;
                else
                MultiView4.ActiveViewIndex = 1;

            }
            else if (ddwn_tab2_Select.SelectedItem.Text == "Spare Parts")
            {
                //grid_PurchaseOrderDetails.DataSourceID = "src_SavedParts";
                //grid_PurchaseOrderDetails.DataBind();
                MultiView4.ActiveViewIndex = 2;
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
        }

        protected void btn_LogInSettings_Click(object sender, EventArgs e)
        {
            MultiView3.ActiveViewIndex = 1;

            conn.Open();
            string rref = lbl_Uname.Text;
            string q3 = "Select * from SystemUserTBl where UserName = '" + rref + "'";
            SqlCommand comms = new SqlCommand(q3, conn);
            IDataReader r;
            r = comms.ExecuteReader();
            r.Read();
            ViewState["idno"] = r.GetString(0);//first row
            string fname = r.GetString(1);
            string sname = r.GetString(2);
            string pword = r.GetString(4);
            conn.Close();

            lbl_UserID.Text = ViewState["idno"].ToString();
            lbl_Fname.Text = fname;
            lbl_Lname.Text = sname;
            txt_Password.Attributes.Add("value", pword);
            txt_Password.ReadOnly = true;

            ViewState["pass"] = pword;

        }

        protected void btn_ChangePassword_Click(object sender, EventArgs e)
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
                            string q1 = "UPDATE SystemUserTBL SET Password = '" + txt_newPass.Text + "' where EmployeeIDNumber = '" + ViewState["idno"].ToString() +"'";
                            SqlCommand comm = new SqlCommand(q1, conn);
                            conn.Open();
                            comm.ExecuteNonQuery();
                            conn.Close();

                            conn.Open();
                            string qry3 = "Select Password from SystemUserTBl where EmployeeIDNumber = '" + ViewState["idno"].ToString() +"'";
                            SqlCommand commas = new SqlCommand(qry3, conn);
                            IDataReader rder;
                            rder = commas.ExecuteReader();
                            rder.Read();
                            string pssword = rder.GetString(0);
                            conn.Close();

                            txt_Password.Attributes.Add("value", pssword);
                        }
                        catch(Exception ex)
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
                else if (txt_CurrentPass.Text != txt_Password.Text)
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

        protected void btn_PurchaseOrderInfo_Click(object sender, EventArgs e)
        {
            MultiView3.ActiveViewIndex = 0;
        }

        protected void btn_PurchaseOrderDetails_Click(object sender, EventArgs e)
        {

            MultiView2.ActiveViewIndex = 1;
            if (txt_PONumber.Visible == true)
            {
                MultiView4.ActiveViewIndex = 1;
            }
            else
            {
                MultiView4.ActiveViewIndex = 0;
            }

            //if (ddwn_tab1_Select.SelectedItem.Text == "Units")
            //{
            //    grid_AvialableUnitsSpareParts.DataSourceID = "src_AvailUnits";
            //    grid_AvialableUnitsSpareParts.DataBind();
            //}
            //else if (ddwn_tab1_Select.SelectedItem.Text == "Spare Parts")
            //{
            //    grid_AvialableUnitsSpareParts.DataSourceID = "src_AvailParts";
            //    grid_AvialableUnitsSpareParts.DataBind();
            //}
            if (ddwn_tab2_Select.SelectedItem.Text == "Units")
            {
                //grid_PurchaseOrderDetails.DataSourceID = "src_SavedUnits";
                //grid_PurchaseOrderDetails.DataBind();
                if (txt_PONumber.Visible == false)
                    MultiView4.ActiveViewIndex = 0;
                else
                    MultiView4.ActiveViewIndex = 1;

            }
            else if (ddwn_tab2_Select.SelectedItem.Text == "Spare Parts")
            {
                //grid_PurchaseOrderDetails.DataSourceID = "src_SavedParts";
                //grid_PurchaseOrderDetails.DataBind();
                MultiView4.ActiveViewIndex = 2;
            }
            grid_PurchaseOrderDetails.DataBind();
        }

        protected void btn_Invoice_Click(object sender, EventArgs e)
        {
            

        }

        protected void btn_AddUnitsParts_Click(object sender, EventArgs e)
        {
            //DataTable dts = new DataTable();
           // DataTable dts = (DataTable)ViewState["Orders"];
            if (txt_tab1_Quantity.Text == "" || txt_tab1_Quantity.Text == null)
            {
                Response.Write("jj");
            }
            else
            {
                if (ddwn_tab1_Select.Text == "Units")
                {

                    if (grid_UnitsPurchaseOrder.Rows.Count == 0)
                    {
                        string num3="";
                        Random rnd = new Random();
                        int num1 = rnd.Next(10000, 99999);
                        int num2 = rnd.Next(1000, 9999);
                        if (ddwn_tab1_Select.Text == "Units")
                        {
                            num3 = "0"+num1.ToString() + num2.ToString();
                        }
                        else if (ddwn_tab1_Select.Text == "Spare Parts")
                        {
                            num3 = "1" + num1.ToString() + num2.ToString();
                        }
                        
                        txt_POrefNumber.Text = num3.ToString();

                        

                        


                        DataTable dt = new DataTable();
                        dt.Columns.AddRange(new DataColumn[4] { new DataColumn("ModelCode"), new DataColumn("Description"), new DataColumn("Color"), new DataColumn("Quantity") });
                        ViewState["Orders"] = dt;

                        grid_UnitsPurchaseOrder.DataSource = (DataTable)ViewState["Orders"];
                        grid_UnitsPurchaseOrder.DataBind();

                        DataTable dts = (DataTable)ViewState["Orders"];
                        dts.Rows.Add(ViewState["MCode"].ToString(), ViewState["Decs"].ToString(), ViewState["Color"].ToString(), txt_tab1_Quantity.Text);
                        ViewState["Orders"] = dts;

                        grid_UnitsPurchaseOrder.DataSource = (DataTable)ViewState["Orders"];
                        grid_UnitsPurchaseOrder.DataBind();




                        ////poiu = PORefNo_TXT.Text;
                        ////Date_Of_PO_TXT.Text = DateTime.Now.ToLongDateString();

                        //string a = "create table [a" + num3 + "_Units] ([id] [int] identity (1,1) not null, [ModelCode] [varchar] (100), [Description] [varchar] (100), [Color] [varchar] (100), [Quantity] [varchar] (100))";

                        //SqlCommand cc = new SqlCommand(a, conn);
                        //conn.Open();
                        //cc.ExecuteNonQuery();
                        //conn.Close();


                        //conn.Open();
                        //string query1 = "insert into a" + num3 + "_Units" + " (ModelCode,Description,Color,Quantity)values('" + ViewState["MCode"].ToString() + "','" + ViewState["Decs"].ToString() + "','" + ViewState["Color"].ToString() + "','" + txt_tab1_Quantity.Text + "')";
                        //SqlCommand commm = new SqlCommand(query1, conn);
                        //Response.Write(query1);
                        //commm.ExecuteNonQuery();
                        //conn.Close();

                        //string ass = "select ModelCode,Description,Color,Quantity from a" + num3 + "_Units";

                        //SqlDataAdapter adapter = new SqlDataAdapter(ass, conn);
                        //conn.Open();
                        //DataSet set = new DataSet();
                        //adapter.Fill(set);
                        //grid_UnitsPurchaseOrder.DataSource = set;
                        //grid_UnitsPurchaseOrder.DataBind();
                        //conn.Close();
                    }
                    else
                    {
                        int done = 0;

                        string modeltest = ViewState["MCode"].ToString();
                        string colortest = ViewState["Color"].ToString();

                        int rows = grid_UnitsPurchaseOrder.Rows.Count;
                        for (int cv = 0; cv <= rows - 1; cv++)
                        {
                            ViewState["ModelCode"] = grid_UnitsPurchaseOrder.Rows[cv].Cells[1].Text;
                            ViewState["Colors"] = grid_UnitsPurchaseOrder.Rows[cv].Cells[3].Text;

                            if (ViewState["ModelCode"].ToString() == modeltest)
                            {
                                if (ViewState["Colors"].ToString() == colortest)
                                {
                                    int nums = Convert.ToInt32(grid_UnitsPurchaseOrder.Rows[cv].Cells[4].Text);
                                    int nums1 = Convert.ToInt32(txt_tab1_Quantity.Text);

                                    int nums2 = nums1 + nums;
                                    string final = nums2.ToString();

                                    //int inds = -1;

                                    for (int i = 0; i < grid_UnitsPurchaseOrder.Rows.Count; i++)
                                    {
                                        foreach (DataControlFieldCell df in grid_UnitsPurchaseOrder.Rows[i].Cells)
                                        {
                                            if (df.Text == modeltest)
                                            {
                                                foreach (DataControlFieldCell df2 in grid_UnitsPurchaseOrder.Rows[i].Cells)
                                                {
                                                    if (df2.Text == colortest)
                                                    {
                                                        DataTable dts = (DataTable)ViewState["Orders"];
                                                        DataRow dr;
                                                        dr = dts.Rows[i];
                                                        dr["Quantity"] = final;
                                                        grid_UnitsPurchaseOrder.DataSource = (DataTable)ViewState["Orders"];
                                                        grid_UnitsPurchaseOrder.DataBind();
                                                       // Response.Write(i.ToString()); 
                                                        break;
                                                    }
                                                }

                                            }
                                        }

                                    }

                                    //grid_UnitsPurchaseOrder.Rows[cv].Cells[4].Text = final;
                                          //string searchingFor = "Something";
                                          //int rowIndex = 0;
                                          //foreach (DataControlFieldCell row in grid_UnitsPurchaseOrder.Rows)
                                          //{
                                          //  foreach (DataGridViewCell cell in row.Cells)
                                          //  {
                                          //    if (cell.Value.ToString() == searchingFor)
                                          //      rowIndex = row.Index;
                                          //      break;
                                          //  }
                                          //    break;
                                          //}
                                    //for (int i = 0; i < grid_UnitsPurchaseOrder.Rows.Count; i++)
                                    //{
                                    //    foreach (DataControlFieldCell df in grid_UnitsPurchaseOrder.Rows[i].Cells)
                                    //   {
                                    //       if (df.Text == "test")
                                    //       {
                                                
                                    //            Response.Write(i.ToString());break;
                                    //       }
                                    //    }
                                    //    break;
                                    //}


                                    //int s = grid_UnitsPurchaseOrder.Rows.Count;
                                    //for (int ss = 0; ss <= s - 1; ss++)
                                    //{
                                    //    string a;
                                    //    int ind = grid_UnitsPurchaseOrder.
                                    //    break;
                                    //}
                                    
                                    
                                    


                                    done = 1;
                                    break;
                                }
                            }
                        }

                        if (done == 0)
                        {
                            DataTable dts = (DataTable)ViewState["Orders"];
                            dts.Rows.Add(ViewState["MCode"].ToString(), ViewState["Decs"].ToString(), ViewState["Color"].ToString(), txt_tab1_Quantity.Text);
                            ViewState["Orders"] = dts;

                            grid_UnitsPurchaseOrder.DataSource = (DataTable)ViewState["Orders"];
                            grid_UnitsPurchaseOrder.DataBind();
                            
                        }

                        

                        
                        //conn.Open();
                        //string query1 = "insert into a" + txt_POrefNumber.Text + "_Units" + " (ModelCode,Description,Color,Quantity)values('" + ViewState["MCode"].ToString() + "','" + ViewState["Decs"].ToString() + "','" + ViewState["Color"].ToString() + "','" + txt_tab1_Quantity.Text + "')";
                        //SqlCommand commm = new SqlCommand(query1, conn);
                        //Response.Write(query1);
                        //commm.ExecuteNonQuery();
                        //conn.Close();

                        //string ass = "select ModelCode,Description,Color,Quantity from a" + txt_POrefNumber.Text + "_Units";

                        //SqlDataAdapter adapter = new SqlDataAdapter(ass, conn);
                        //conn.Open();
                        //DataSet set = new DataSet();
                        //adapter.Fill(set);
                        //grid_UnitsPurchaseOrder.DataSource = set;
                        //grid_UnitsPurchaseOrder.DataBind();
                        //conn.Close();
                    }

                }
            }
            
        }

        protected void grid_AvialableUnitsSpareParts_SelectedIndexChanged(object sender, EventArgs e)
        {
            ViewState["MCode"] = grid_AvialableUnitsSpareParts.SelectedRow.Cells[1].Text;
            ViewState["Decs"] = grid_AvialableUnitsSpareParts.SelectedRow.Cells[2].Text;
            ViewState["Color"] = grid_AvialableUnitsSpareParts.SelectedRow.Cells[3].Text;
            //ViewState["Quan"] = txt_tab1_Quantity.Text;
        }

        protected void grid_UnitsPurchaseOrder_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        protected void btn_Remove_Click(object sender, EventArgs e)
        {
            grid_UnitsPurchaseOrder.DeleteRow(grid_UnitsPurchaseOrder.SelectedIndex);
        }

        protected void grid_UnitsPurchaseOrder_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int index = Convert.ToInt32(e.RowIndex);
            DataTable dt = ViewState["Orders"] as DataTable;
            dt.Rows[index].Delete();
            ViewState["Orders"] = dt;
            grid_UnitsPurchaseOrder.DataSource = ViewState["Orders"] as DataTable;
            grid_UnitsPurchaseOrder.DataBind();
        }

        protected void grid_UnitsPurchaseOrder_RowDeleted(object sender, GridViewDeletedEventArgs e)
        {
        }

        protected void btn_DontSavePO_Click(object sender, EventArgs e)
        {
            grid_UnitsPurchaseOrder.Columns.Clear();
            grid_UnitsPurchaseOrder.DataBind();

            txt_POrefNumber.Text = "";
        }

        protected void btn_SavePO_Click(object sender, EventArgs e)
        {
            bool ax = false;
            string Rno = txt_POrefNumber.Text;
            int PONs = grid_PurchaseOrderDetails.Rows.Count;
            for (int x = 0; x <= PONs - 1; x++)
            {
                string POno = grid_PurchaseOrderDetails.Rows[x].Cells[1].Text;
                if (Rno == POno)
                {
                    //string dels = "delete table";


                    string remove = grid_PurchaseOrderDetails.SelectedRow.Cells[1].Text;
                    SqlCommand commms = new SqlCommand("delete from SystemUnitsPONTBL where PurchaseOrderNumber = " + Rno, magnaconn);
                    //commm.Parameters.AddWithValue("@reff", ViewState["EmployerIDUserRemover"]);
                    magnaconn.Open();
                    commms.ExecuteNonQuery();
                    magnaconn.Close();

                    //grid_PurchaseOrderDetails.DataBind();

                    string del = "drop table a" + Rno + "_Units";
                    SqlCommand cs = new SqlCommand(del, magnaconn);
                    magnaconn.Open();
                    cs.ExecuteNonQuery();
                    magnaconn.Close();



                    string a = "create table [a" + txt_POrefNumber.Text + "_Units] ([id] [int] identity (1,1) not null,[s_number] [varchar] (50), [ModelCode] [varchar] (100), [Description] [varchar] (100), [Color] [varchar] (100), [Quantity] [varchar] (100), [BackOrders] [varchar] (100), [Percentage] [varchar] (100))";

                    SqlCommand cc = new SqlCommand(a, magnaconn);
                    magnaconn.Open();
                    cc.ExecuteNonQuery();
                    magnaconn.Close();

                    int counts = grid_UnitsPurchaseOrder.Rows.Count;
                    magnaconn.Open();
                    for (int c = 0; c <= counts - 1; c++)
                    {
                        ViewState["MCode"] = grid_UnitsPurchaseOrder.Rows[c].Cells[1].Text;
                        ViewState["Decs"] = grid_UnitsPurchaseOrder.Rows[c].Cells[2].Text;
                        ViewState["Color"] = grid_UnitsPurchaseOrder.Rows[c].Cells[3].Text;
                        ViewState["Q"] = grid_UnitsPurchaseOrder.Rows[c].Cells[4].Text;
                        int xs = c + 1;
                        string query1 = "insert into a" + txt_POrefNumber.Text + "_Units" + " (s_number,ModelCode,Description,Color,Quantity)values('"+xs.ToString()+"','" + ViewState["MCode"].ToString() + "','" + ViewState["Decs"].ToString() + "','" + ViewState["Color"].ToString() + "','" + ViewState["Q"].ToString() + "')";
                        SqlCommand commm = new SqlCommand(query1, magnaconn);
                        //Response.Write(query1);
                        commm.ExecuteNonQuery();
                    }
                    magnaconn.Close();

                    grid_UnitsPurchaseOrder.Columns.Clear();
                    grid_UnitsPurchaseOrder.DataBind();



                    string date = DateTime.Now.ToLongDateString();

                    magnaconn.Open();
                    string querys1 = "insert into SystemUnitsPONTBL (PurchaseOrderNumber,DateCreated,DateSent)values('" + txt_POrefNumber.Text + "','" + date + "','___/___/___')";
                    SqlCommand mands = new SqlCommand(querys1, magnaconn);
                    //Response.Write(query1);
                    mands.ExecuteNonQuery();
                    magnaconn.Close();

                    
                    //databind
                    string PON = txt_POrefNumber.Text;
                    lbl_PORefNo.Visible = true;
                    txt_PONumber.Visible = true;
                    txt_PONumber.Text = PON;


                    //string que = "slect ModelCode,Description,Color,Quantity from a" + PON + "_Units";
                    //conn.Open();
                    //SqlCommand scom = new SqlCommand(que, conn);
                    //scom.ExecuteNonQuery();
                    //conn.Close();
                    magnaconn.Open();
                    string ass = "select ModelCode,Description,Color,Quantity from a" + PON + "_Units";
                    //Response.Write(ass);
                    SqlDataAdapter adapter = new SqlDataAdapter(ass, magnaconn);

                    DataSet set = new DataSet();
                    adapter.Fill(set);

                    //grid_PurchaseOrderDetails1.DataSource = null;

                    grid_PurchaseOrderDetails1.DataSource = set.Tables[0];
                    grid_PurchaseOrderDetails1.DataBind();
                    magnaconn.Close();
                    //databind
                    txt_POrefNumber.Text = "";
                    ax = true;
                    break;
                }
            }

            if (ax == false)
            {
                string asa = "create table [a" + txt_POrefNumber.Text + "_Units] ([id] [int] identity (1,1) not null, [s_number] [varchar] (50), [ModelCode] [varchar] (100), [Description] [varchar] (100), [Color] [varchar] (100), [Quantity] [varchar] (100), [BackOrders] [varchar] (100), [Available] [varchar] (100), [Percentage] [varchar] (100))";

                SqlCommand ccs = new SqlCommand(asa, magnaconn);
                magnaconn.Open();
                ccs.ExecuteNonQuery();
                magnaconn.Close();

                int countss = grid_UnitsPurchaseOrder.Rows.Count;
                magnaconn.Open();
                for (int c = 0; c <= countss - 1; c++)
                {
                    ViewState["MCode"] = grid_UnitsPurchaseOrder.Rows[c].Cells[1].Text;
                    ViewState["Decs"] = grid_UnitsPurchaseOrder.Rows[c].Cells[2].Text;
                    ViewState["Color"] = grid_UnitsPurchaseOrder.Rows[c].Cells[3].Text;
                    ViewState["Q"] = grid_UnitsPurchaseOrder.Rows[c].Cells[4].Text;
                    int a = c + 1;
                    string query1 = "insert into a" + txt_POrefNumber.Text + "_Units" + " (s_number,ModelCode,Description,Color,Quantity)values('" + a.ToString() + "','" + ViewState["MCode"].ToString() + "','" + ViewState["Decs"].ToString() + "','" + ViewState["Color"].ToString() + "','" + ViewState["Q"].ToString() + "')";
                    SqlCommand commm = new SqlCommand(query1, magnaconn);
                    //Response.Write(query1);
                    commm.ExecuteNonQuery();
                }
                magnaconn.Close();

                grid_UnitsPurchaseOrder.Columns.Clear();
                grid_UnitsPurchaseOrder.DataBind();



                string dates = DateTime.Now.ToLongDateString();

                magnaconn.Open();
                string querys1s = "insert into SystemUnitsPONTBL (PurchaseOrderNumber,DateCreated,DateSent)values('" + txt_POrefNumber.Text + "','" + dates + "','-/-/-')";
                SqlCommand mandss = new SqlCommand(querys1s, magnaconn);
                //Response.Write(query1);
                mandss.ExecuteNonQuery();
                magnaconn.Close();

                txt_POrefNumber.Text = "";
                //eak;
            }
            

            
        }

        protected void grid_PurchaseOrderDetails_SelectedIndexChanged(object sender, EventArgs e)
        {
            ViewState["pon"] = grid_PurchaseOrderDetails.SelectedRow.Cells[1].Text;
            
            
                string PON = grid_PurchaseOrderDetails.SelectedRow.Cells[1].Text;
                lbl_PORefNo.Visible = true;
                txt_PONumber.Visible = true;
                txt_PONumber.Text = PON;


                //string que = "slect ModelCode,Description,Color,Quantity from a" + PON + "_Units";
                //conn.Open();
                //SqlCommand scom = new SqlCommand(que, conn);
                //scom.ExecuteNonQuery();
                //conn.Close();
                magnaconn.Open();
                string ass = "select ModelCode,Description,Color,Quantity from a" + PON + "_Units";
                //Response.Write(ass);
                SqlDataAdapter adapter = new SqlDataAdapter(ass, magnaconn);
                
                DataSet set = new DataSet();
                adapter.Fill(set);

                //grid_PurchaseOrderDetails1.DataSource = null;

                grid_PurchaseOrderDetails1.DataSource = set.Tables[0];
                grid_PurchaseOrderDetails1.DataBind();
                magnaconn.Close();

                //string ass = "select ModelCode,Description,Color,Quantity from a" + PON + "_Units";
                //SqlCommand coms = new SqlCommand(ass, conn);
                //conn.Open();
                //coms.ExecuteNonQuery();
                //conn.Close();

                //DataTable dts = (DataTable)ViewState["SavedOrders"];
                //dts.Rows.Add(ViewState["MCode"].ToString(), ViewState["Decs"].ToString(), ViewState["Color"].ToString(), txt_tab1_Quantity.Text);
                //ViewState["SavedOrders"] = dts;

                //grid_UnitsPurchaseOrder.DataSource = (DataTable)ViewState["SavedOrders"];
                //grid_UnitsPurchaseOrder.DataBind();


                MultiView4.ActiveViewIndex = 1;
            ////}
            //reports
                try
                {
                    ViewState["ponn"] = grid_PurchaseOrderDetails.SelectedRow.Cells[1].Text;
                    //order report
                    //a
                    string numb = ViewState["ponn"].ToString();
                    txt_POnums.Text = numb;
                    txt_Date.Text = grid_PurchaseOrderDetails.SelectedRow.Cells[2].Text;
                    recsconn.Open();
                    string order = "select modelcode,description,color,quantity from a" + numb + "_Units";
                    SqlDataAdapter adaptase = new SqlDataAdapter(order, recsconn);

                    DataSet seteda = new DataSet();
                    adaptase.Fill(seteda);

                    grid_OrderReport1.DataSource = seteda.Tables[0];
                    grid_OrderReport1.DataBind();
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

                        grid_OrderReport2.DataSource = bseteda.Tables[0];
                        grid_OrderReport2.DataBind();
                        recsconn.Close();

                        lbl_ads.Visible = true;
                    }
                    catch
                    {
                        if (recsconn.State == ConnectionState.Open)
                            recsconn.Close();
                    }
                    //b
                    //order report
                    //sales invoice
                    if (magnaconn.State == ConnectionState.Open)
                        magnaconn.Close();
                    magnaconn.Open();
                    string lists = "select invoiceno as 'Invoice No.',Deliveryno as 'Delivery No.',datecreated as 'Date Generated',Status from systemreportstbl where ponumber = '" + ViewState["ponn"].ToString() + "'";
                    SqlDataAdapter adapters = new SqlDataAdapter(lists, magnaconn);

                    DataSet seteds = new DataSet();
                    adapters.Fill(seteds);

                    grid_Userinvoices.DataSource = seteds.Tables[0];
                    grid_Userinvoices.DataBind();
                    magnaconn.Close();

                    //sales invoice
                    //delivery
                    magnaconn.Open();
                    string dellist = "select Deliveryno as 'Delivery No.',invoiceno as 'Invoice No.',datecreated as 'Date Generated',Status from systemreportstbl where ponumber = '" + ViewState["ponn"].ToString() + "'";
                    SqlDataAdapter deladaptas = new SqlDataAdapter(dellist, magnaconn);

                    DataSet delseted = new DataSet();
                    deladaptas.Fill(delseted);

                    grid_DeliveryList.DataSource = delseted.Tables[0];
                    grid_DeliveryList.DataBind();
                    magnaconn.Close();
                    //delivery

                }
                catch { }
            
        }

        protected void btn_AvailableUnitsSpareParts_Click(object sender, EventArgs e)
        {
            MultiView2.ActiveViewIndex = 0;
        }

        protected void btn_Back_Click(object sender, EventArgs e)
        {
            if (ddwn_tab2_Select.SelectedItem.Text == "Units")
            {
                //grid_PurchaseOrderDetails.DataSourceID = "src_SavedUnits";
                //grid_PurchaseOrderDetails.DataBind();
                MultiView4.ActiveViewIndex = 0;
            }
            else if (ddwn_tab2_Select.SelectedItem.Text == "Spare Parts")
            {
                //grid_PurchaseOrderDetails.DataSourceID = "src_SavedParts";
                //grid_PurchaseOrderDetails.DataBind();
                MultiView4.ActiveViewIndex = 2;
            }

            lbl_PORefNo.Visible = false;
            txt_PONumber.Text = "";
            txt_PONumber.Visible = false;
            ddwn_Colors.Items.Clear();
        }

        protected void btn_AddItem_Click(object sender, EventArgs e)
        {
            //int columnCount = ((DataTable)this.grid_PurchaseOrderDetails.DataSource).Columns.Count;
            //Response.Write("jerome"+columnCount.ToString());
            //Response.Write("jerome lang ");
            //grid_PurchaseOrderDetails.AutoGenerateSelectButton = false;
            //int columnCount = grid_PurchaseOrderDetails.Columns.Count;
            //Response.Write("jerome" + columnCount.ToString());
            //grid_PurchaseOrderDetails.AutoGenerateSelectButton = true;

            //grid_PurchaseOrderDetails1

            //adding column
            //DataTable dt = new DataTable();
            //dt.Columns.AddRange(new DataColumn[4] { new DataColumn("ModelCode"), new DataColumn("Description"), new DataColumn("Color"), new DataColumn("Quantity") });
            //ViewState["Orders"] = dt;

            //grid_UnitsPurchaseOrder.DataSource = (DataTable)ViewState["Orders"];
            //grid_UnitsPurchaseOrder.DataBind();
            ////adding column

            //DataTable dts = (DataTable)ViewState["Orders"];
            //dts.Rows.Add(ViewState["MCode"].ToString(), ViewState["Decs"].ToString(), ViewState["Color"].ToString(), txt_tab1_Quantity.Text);
            //ViewState["Orders"] = dts;

            //grid_UnitsPurchaseOrder.DataSource = (DataTable)ViewState["Orders"];
            //grid_UnitsPurchaseOrder.DataBind();
        }

        protected void ddwn_tab2_Select_SelectedIndexChanged(object sender, EventArgs e)
        {
            lbl_PORefNo.Visible = false;
            txt_PONumber.Text = "";
            txt_PONumber.Visible = false;
            ddwn_Colors.Items.Clear();
        }

        protected void grid_PurchaseOrderDetails1_SelectedIndexChanged(object sender, EventArgs e)
        {



            ddwn_Colors.Items.Clear();
            string code = grid_PurchaseOrderDetails1.SelectedRow.Cells[1].Text;
            ViewState["idssa"] = grid_PurchaseOrderDetails1.SelectedRow.Cells[1].Text;
            ViewState["mcID"] = grid_PurchaseOrderDetails1.SelectedIndex;
           // ViewState["colorr"] = grid_PurchaseOrderDetails1.SelectedRow.Cells[3].Text;

            ////if (txt_PONumber.Visible == true)
            ////{
            ////    string wow = grid_PurchaseOrderDetails.SelectedRow.Cells[1].Text;
            string query = "select color from SystemModelsTBL where ModelCode='" + code + "'";
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
        }

        protected void btn_DeletePO_Click(object sender, EventArgs e)
        {
            string remove = grid_PurchaseOrderDetails.SelectedRow.Cells[1].Text;
            SqlCommand commm = new SqlCommand("delete from SystemUnitsPONTBL where PurchaseOrderNumber = " + remove, magnaconn);
            //commm.Parameters.AddWithValue("@reff", ViewState["EmployerIDUserRemover"]);
            magnaconn.Open();
            commm.ExecuteNonQuery();
            magnaconn.Close();

            grid_PurchaseOrderDetails.DataBind();

            string del = "drop table a"+remove+"_Units";
            SqlCommand c = new SqlCommand(del,conn);
            conn.Open();
            c.ExecuteNonQuery();
            conn.Close();
        }

        protected void btn_EditItems_Click(object sender, EventArgs e)
        {
            //adding column
            DataTable dt = new DataTable();
            dt.Columns.AddRange(new DataColumn[4] { new DataColumn("ModelCode"), new DataColumn("Description"), new DataColumn("Color"), new DataColumn("Quantity") });
            ViewState["Orders"] = dt;

            grid_UnitsPurchaseOrder.DataSource = (DataTable)ViewState["Orders"];
            grid_UnitsPurchaseOrder.DataBind();
            //adding column

            DataTable dts = (DataTable)ViewState["Orders"];
            int counts = grid_PurchaseOrderDetails1.Rows.Count;

            for (int c = 0; c <= counts - 1; c++)
            {
                string mcode = grid_PurchaseOrderDetails1.Rows[c].Cells[1].Text;
                string desc = grid_PurchaseOrderDetails1.Rows[c].Cells[2].Text;
                string color = grid_PurchaseOrderDetails1.Rows[c].Cells[3].Text;
                string quan = grid_PurchaseOrderDetails1.Rows[c].Cells[4].Text; 
                dts.Rows.Add(mcode,desc,color,quan);
            }

           
            ViewState["Orders"] = dts;

            grid_UnitsPurchaseOrder.DataSource = (DataTable)ViewState["Orders"];
            grid_UnitsPurchaseOrder.DataBind();

            txt_POrefNumber.Text = txt_PONumber.Text;

            MultiView2.ActiveViewIndex = 0;
        }

        protected void btn_EditPO_Click(object sender, EventArgs e)
        {
            lbl_tab2_Quantity.Visible = true;
            txt_tab2_Quantity.Visible = true;
            lbl_tab2_Color.Visible = true;
            ddwn_Colors.Visible = true;
            btn_SaveEdit.Visible = true;
            bnt_CancelEdit.Visible = true;
        }

        protected void bnt_CancelEdit_Click(object sender, EventArgs e)
        {
            lbl_tab2_Quantity.Visible = !true;
            txt_tab2_Quantity.Visible = !true;
            lbl_tab2_Color.Visible = !true;
            ddwn_Colors.Visible = !true;
            btn_SaveEdit.Visible = !true;
            bnt_CancelEdit.Visible = !true;

            txt_tab2_Quantity.Text = "";
            ddwn_Colors.Items.Clear();
        }

        protected void btn_SaveEdit_Click(object sender, EventArgs e)
        {
            string change;
            string qs = "";
            //ViewState["mcc"] = grid_PurchaseOrderDetails1.SelectedRow.Cells[1].Text;
            ViewState["mcc"] = grid_PurchaseOrderDetails1.SelectedIndex;
            int index = Convert.ToInt32(ViewState["mcc"]);//index ng grid
            int finalIndex = index + 1;//index ng db(s_number)
            ViewState["colorr"] = grid_PurchaseOrderDetails1.SelectedRow.Cells[3].Text;
            if (txt_tab2_Quantity.Text == "" || txt_tab2_Quantity.Text == null || ddwn_Colors.Text == "" || ddwn_Colors.Text == null)
            {
                change = "update a" + txt_PONumber.Text + "_Units set Color = '" + ddwn_Colors.Text + "' where s_number = '" + finalIndex.ToString() + "'";// and Color = '"+ViewState["colorr"].ToString()+"'";
            }
            else
            {
                change = "update a" + txt_PONumber.Text + "_Units set Color = '" + ddwn_Colors.Text + "', Quantity = '" + txt_tab2_Quantity.Text + "' where s_number = '" + finalIndex.ToString() + "'";// and Color = '" + ViewState["colorr"].ToString()+"'";

            }
            magnaconn.Open();
            SqlCommand sql = new SqlCommand(change, magnaconn);
            sql.ExecuteNonQuery();
            magnaconn.Close();


            //databind
            string PON = txt_PONumber.Text;
                    lbl_PORefNo.Visible = true;
                    txt_PONumber.Visible = true;
                    txt_PONumber.Text = PON;


                    //string que = "slect ModelCode,Description,Color,Quantity from a" + PON + "_Units";
                    //conn.Open();
                    //SqlCommand scom = new SqlCommand(que, conn);
                    //scom.ExecuteNonQuery();
                    //conn.Close();
                    magnaconn.Open();
                    string ass = "select ModelCode,Description,Color,Quantity from a" + PON + "_Units";
                    //Response.Write(ass);
                    SqlDataAdapter adapter = new SqlDataAdapter(ass, magnaconn);

                    DataSet set = new DataSet();
                    adapter.Fill(set);

                    //grid_PurchaseOrderDetails1.DataSource = null;

                    grid_PurchaseOrderDetails1.DataSource = set.Tables[0];
                    grid_PurchaseOrderDetails1.DataBind();
                    magnaconn.Close();
                    //databind
                    try
                    {
                        //reading/gettign of data
                        string query = "WITH unitsCTE AS (SELECT *, Row_number() OVER (Partition BY ModelCode, Color ORDER BY s_number) AS rows FROM a" + PON + "_Units) SELECT ModelCode,Color,Quantity FROM unitsCTE WHERE rows > 1";
                        SqlCommand com = new SqlCommand(query, magnaconn);
                        IDataReader r;
                        magnaconn.Open();
                        r = com.ExecuteReader();

                        r.Read();
                        ViewState["code"] = r.GetString(0).ToString();//secon row
                        //ViewState["desc"] = r.GetString(1).ToString();
                        ViewState["colser"] = r.GetString(1).ToString();//secon row
                        ViewState["qquan"] = r.GetString(2).ToString();
                        magnaconn.Close();
                        //reading/gettign of data
                        
                        int old = Convert.ToInt32(ViewState["qquan"]);//Convert.ToInt32(txt_tab2_Quantity.Text);

                        //deleting of row1
                        qs = "WITH unitsCTE AS (SELECT *, Row_number() OVER (Partition BY ModelCode, Color ORDER BY s_number) AS rows FROM a" + PON + "_Units) delete from unitsCTE WHERE rows > 1";
                        SqlCommand mn = new SqlCommand(qs, magnaconn);
                        magnaconn.Open();
                        mn.ExecuteNonQuery();
                        magnaconn.Close();
                        //deleteing of row


                        string sel = "select Quantity from a" + PON + "_Units where ModelCode = '"+ViewState["code"].ToString()+"' and Color = '"+ViewState["colser"].ToString()+"' order by s_number";
                        SqlCommand cmd = new SqlCommand(sel, magnaconn);
                        magnaconn.Open();
                        SqlDataReader rdr;
                        rdr = cmd.ExecuteReader();
                        rdr.Read();
                        int dest = Convert.ToInt32(rdr.GetString(0));
                        magnaconn.Close();

                        int finals = old + dest;

                        string ups = "update a" + PON + "_Units set Quantity = '" + finals.ToString() + "' Where ModelCode ='" + ViewState["code"].ToString() + "' and Color = '" + ViewState["colser"].ToString() + "'";
                        SqlCommand ss = new SqlCommand(ups, magnaconn);
                        magnaconn.Open();
                        ss.ExecuteNonQuery();
                        magnaconn.Close();


                       

                        //databind
                        string PONs = txt_PONumber.Text;
                        lbl_PORefNo.Visible = true;
                        txt_PONumber.Visible = true;
                        txt_PONumber.Text = PONs;


                        //string que = "slect ModelCode,Description,Color,Quantity from a" + PON + "_Units";
                        //conn.Open();
                        //SqlCommand scom = new SqlCommand(que, conn);
                        //scom.ExecuteNonQuery();
                        //conn.Close();
                        magnaconn.Open();
                        string asss = "select ModelCode,Description,Color,Quantity from a" + PONs + "_Units";
                        //Response.Write(ass);
                        SqlDataAdapter adapters = new SqlDataAdapter(asss, magnaconn);

                        DataSet sets = new DataSet();
                        adapters.Fill(sets);

                        //grid_PurchaseOrderDetails1.DataSource = null;

                        grid_PurchaseOrderDetails1.DataSource = sets.Tables[0];
                        grid_PurchaseOrderDetails1.DataBind();
                        magnaconn.Close();
                        //databind
//////////////////////////////////////////////////
                        //reset s_number
                        magnaconn.Open();
                        string remover = "UPDATE a" + txt_PONumber.Text + "_Units SET s_number = NULL";
                        SqlCommand ms = new SqlCommand(remover, magnaconn);
                        ms.ExecuteNonQuery();

                        string snumber = "With cte As(Select id, s_number,  Row_Number() Over (Order By s_number) As rn From a" + txt_PONumber.Text + "_Units)Update cte Set s_number = rn";
                        SqlCommand ssqu = new SqlCommand(snumber, magnaconn);
                        
                        ssqu.ExecuteNonQuery();
                        magnaconn.Close();
                        //reset s_number

                    }
                    catch (Exception ex)
                    {
                        //conn.Close();
                        ////databind
                        //string PONs = txt_PONumber.Text;
                        //lbl_PORefNo.Visible = true;
                        //txt_PONumber.Visible = true;
                        //txt_PONumber.Text = PONs;


                        ////string que = "slect ModelCode,Description,Color,Quantity from a" + PON + "_Units";
                        ////conn.Open();
                        ////SqlCommand scom = new SqlCommand(que, conn);
                        ////scom.ExecuteNonQuery();
                        ////conn.Close();
                        //conn.Open();
                        //string asss = "select ModelCode,Description,Color,Quantity from a" + PONs + "_Units";
                        ////Response.Write(ass);
                        //SqlDataAdapter adapters = new SqlDataAdapter(asss, conn);

                        //DataSet sets = new DataSet();
                        //adapters.Fill(sets);

                        ////grid_PurchaseOrderDetails1.DataSource = null;

                        //grid_PurchaseOrderDetails1.DataSource = sets.Tables[0];
                        //grid_PurchaseOrderDetails1.DataBind();
                        //conn.Close();
                        ////databind
                        Response.Write("walang kaparehas");
                    }
        }

        protected void btn_RemovePO_Click(object sender, EventArgs e)
        {
            //string remove = ViewState["idssa"].ToString();
            int remove = grid_PurchaseOrderDetails1.SelectedIndex;
            int firemove = remove + 1;
            string co = "delete from a" + txt_PONumber.Text + "_Units where s_number = '"+firemove.ToString()+"'";
            SqlCommand commm = new SqlCommand(co, magnaconn);
            //commm.Parameters.AddWithValue("@reff", ViewState["EmployerIDUserRemover"]);
            //Response.Write(co);
            magnaconn.Open();
            commm.ExecuteNonQuery();
            magnaconn.Close();
/////////////////////////////////
            //reset s_number
            magnaconn.Open();
            string remover = "UPDATE a" + txt_PONumber.Text + "_Units SET s_number = NULL";
            SqlCommand ms = new SqlCommand(remover, magnaconn);
            ms.ExecuteNonQuery();

            string snumber = "With cte As(Select id, s_number,  Row_Number() Over (Order By s_number) As rn From a" + txt_PONumber.Text+ "_Units)Update cte Set s_number = rn";
            SqlCommand ssqu = new SqlCommand(snumber, magnaconn);
            
            ssqu.ExecuteNonQuery();
            magnaconn.Close();
            //reset s_number

            //grid_PurchaseOrderDetails1.DataBind();
            string PON = txt_PONumber.Text;
            lbl_PORefNo.Visible = true;
            txt_PONumber.Visible = true;
            txt_PONumber.Text = PON;


            //string que = "slect ModelCode,Description,Color,Quantity from a" + PON + "_Units";
            //conn.Open();
            //SqlCommand scom = new SqlCommand(que, conn);
            //scom.ExecuteNonQuery();
            //conn.Close();
            magnaconn.Open();
            string ass = "select ModelCode,Description,Color,Quantity from a" + PON + "_Units";
            //Response.Write(ass);
            SqlDataAdapter adapter = new SqlDataAdapter(ass, magnaconn);

            DataSet set = new DataSet();
            adapter.Fill(set);

            //grid_PurchaseOrderDetails1.DataSource = null;

            grid_PurchaseOrderDetails1.DataSource = set.Tables[0];
            grid_PurchaseOrderDetails1.DataBind();
            magnaconn.Close();

            //string del = "drop table a"+remove+"_Units";
            //SqlCommand c = new SqlCommand(del,conn);
            //conn.Open();
            //c.ExecuteNonQuery();
            //conn.Close();
            
            



            
        }

        protected void grid_UnitsPurchaseOrder_RowEditing(object sender, GridViewEditEventArgs e)
        {
            //grid_UnitsPurchaseOrder.EditIndex = e.NewEditIndex;
            //grid_UnitsPurchaseOrder.DataSource = (DataTable)ViewState["Orders"];
            //grid_UnitsPurchaseOrder.DataBind();
        }

        protected void grid_UnitsPurchaseOrder_RowCreated(object sender, GridViewRowEventArgs e)
        {

        }

        protected void btn_SendPO_Click(object sender, EventArgs e)
        {
            //object asx = sender;
            //EventArgs ab = e;
            string inserter = "";
            string refnum = ViewState["pon"].ToString();
            char refID = refnum[0];
            if (refID.ToString() == "0")//meaning units
            {
                inserter = "update SystemUnitsPONTBL set DateSent = '" + DateTime.Now.ToLongDateString() + "' where PurchaseOrderNumber = '" + refnum + "'";
            }
            else if (refID.ToString() == "1")//meaning Parts
            {
                inserter = "update SystemPartsPONTBL set DateSent =  '" + DateTime.Now.ToLongDateString() + "' where PurchaseOrderNumber = '" + refnum + "'";
            }

            magnaconn.Open();
            SqlCommand cm = new SqlCommand(inserter, magnaconn);
            cm.ExecuteNonQuery();
            magnaconn.Close();

            grid_PurchaseOrderDetails.DataBind();

            string received = grid_PurchaseOrderDetails.SelectedRow.Cells[3].Text;
            string qq = "insert into SystemUnitsOrderListsTBL (PurchaseOrderNumber,DateReceived,DateModified,DateSent,Status)values('" + refnum + "','" + received + "','-/-/-','-/-/-','UNSEEN')";
            SqlCommand mm = new SqlCommand(qq,conn);
            conn.Open();
            mm.ExecuteNonQuery();
            conn.Close();
            // sending po to admin
            //reset s_number
  ///////////////////////////////////////////////////
            conn.Open();
            
            string remover = "UPDATE SystemUnitsOrderListsTBL SET s_number = NULL";
            SqlCommand ms = new SqlCommand(remover, conn);
            ms.ExecuteNonQuery();

            string snumber = "With cte As(Select id, s_number,  Row_Number() Over (Order By s_number) As rn From SystemUnitsOrderListsTBL)Update cte Set s_number = rn";
            SqlCommand ssqu = new SqlCommand(snumber, conn);
            
            ssqu.ExecuteNonQuery();
            conn.Close();
            //reset s_number


           

            ////new s_number resetter
            //conn.Open();
            ////string drop = "ALTER TABLE a" + refnum + "_Units DROP PRIMARY KEY";
            ////SqlCommand s = new SqlCommand(drop,conn);
            ////s.ExecuteNonQuery();

            

            //int countss = grid_PurchaseOrderDetails1.Rows.Count;
            //for (int c = 0; c <= countss - 1; c++)
            //{
            //    //try
            //    //{


            //        int a = c + 1;
            //        string query1 = "update SystemUnitsOrderListsTBL set s_number = '" + a.ToString() + "' where s_number = NULL";
            //        SqlCommand commm = new SqlCommand(query1, conn);
            //        //Response.Write(query1);
            //        commm.ExecuteNonQuery();
            //        a++;
            //    //}
            //    //catch (Exception)
            //    //{
 
            //    //}

            //}
            //conn.Close();
            ////new s_number resetter
            //// sending po to admin
            string re = ViewState["pon"].ToString();
            string a = "create table [a" + re + "_Units] ([id] [int] identity (1,1) not null,[s_number] [varchar] (50), [ModelCode] [varchar] (100), [Description] [varchar] (100), [Color] [varchar] (100), [Quantity] [varchar] (100), [BackOrders] [varchar] (100), [Available] [varchar] (100), [Percentage] [varchar] (100))";

            SqlCommand cc = new SqlCommand(a, conn);
            conn.Open();
            cc.ExecuteNonQuery();
            conn.Close();

            int cx = grid_PurchaseOrderDetails1.Rows.Count;
            conn.Open();
            for (int ax = 0; ax <= cx - 1; ax++)
            {
                string MCodes = grid_PurchaseOrderDetails1.Rows[ax].Cells[1].Text;
                string Decss = grid_PurchaseOrderDetails1.Rows[ax].Cells[2].Text;
                string Colors = grid_PurchaseOrderDetails1.Rows[ax].Cells[3].Text;
                string Qq = grid_PurchaseOrderDetails1.Rows[ax].Cells[4].Text;
                int j = ax + 1;
                string ins = "insert into a" + re + "_Units (s_number,ModelCode,Description,Color,Quantity)values('" + j.ToString()+ "','" + MCodes + "','" + Decss + "','" + Colors + "','" + Qq + "')";
                SqlCommand mss = new SqlCommand(ins,conn);
                mss.ExecuteNonQuery();
            }

            //string notif = "update SystemNotifierTBL set notifier = 'YES'";
            //SqlCommand mn = new SqlCommand(notif,conn);
            //mn.ExecuteNonQuery();
            conn.Close();
        }

        protected void btn_recreatePO_Click(object sender, EventArgs e)
        {
            string num3 = "";
            Random rnd = new Random();
            int num1 = rnd.Next(10000, 99999);
            int num2 = rnd.Next(1000, 9999);

            string cha = txt_PONumber.Text;
            char refID = cha[0];

            if (refID.ToString() == "0")
            {
                num3 = "0" + num1.ToString() + num2.ToString();
            }
            else if (refID.ToString() == "1")
            {
                num3 = "1" + num1.ToString() + num2.ToString();
            }

            txt_POrefNumber.Text = num3.ToString();

            DataTable dt = new DataTable();
            dt.Columns.AddRange(new DataColumn[4] { new DataColumn("Model Code"), new DataColumn("Description"), new DataColumn("Color"), new DataColumn("Quantity") });
            ViewState["Orders"] = dt;

            grid_UnitsPurchaseOrder.DataSource = (DataTable)ViewState["Orders"];
            grid_UnitsPurchaseOrder.DataBind();

            int c = grid_PurchaseOrderDetails1.Rows.Count;
            for (int a = 0; a <=c-1; a++)
            {
                //ViewState["MCodes"] = grid_PurchaseOrderDetails1.Rows[a].Cells[1].Text;
                //ViewState["Decss"] = grid_PurchaseOrderDetails1.Rows[a].Cells[2].Text;
                //ViewState["Colors"] = grid_PurchaseOrderDetails1.Rows[a].Cells[3].Text;
                //ViewState["Qq"] = grid_PurchaseOrderDetails1.Rows[a].Cells[4].Text;

                string MCodes = grid_PurchaseOrderDetails1.Rows[a].Cells[1].Text;
                string Decss = grid_PurchaseOrderDetails1.Rows[a].Cells[2].Text;
                string Colors = grid_PurchaseOrderDetails1.Rows[a].Cells[3].Text;
                string Qq = grid_PurchaseOrderDetails1.Rows[a].Cells[4].Text;

                DataTable dts = (DataTable)ViewState["Orders"];
                //dts.Rows.Add(ViewState["MCodes"].ToString(), ViewState["Decss"].ToString(), ViewState["Colors"].ToString(), ViewState["Qq"].ToString());
                dts.Rows.Add(MCodes,Decss,Colors,Qq);
                ViewState["Orders"] = dts;

                grid_UnitsPurchaseOrder.DataSource = (DataTable)ViewState["Orders"];
                grid_UnitsPurchaseOrder.DataBind();
                //Response.Write(a.ToString());
            }

                
        }

        protected void Timer1_Tick(object sender, EventArgs e)
        {
            if (ddwn_tab1_Select.SelectedItem.Text == "Units")
            {
                grid_AvialableUnitsSpareParts.DataSourceID = "src_AvailUnits";
                grid_AvialableUnitsSpareParts.DataBind();
            }
            else if (ddwn_tab1_Select.SelectedItem.Text == "Spare Parts")
            {
                grid_AvialableUnitsSpareParts.DataSourceID = "src_AvailParts";
                grid_AvialableUnitsSpareParts.DataBind();
            }
        }

        protected void btn_UnitsPurchaseOrder_Click(object sender, EventArgs e)
        {

        }

        protected void btn_Reports_Click(object sender, EventArgs e)
        {
            MultiView2.ActiveViewIndex = 2;
            MultiView5.ActiveViewIndex = 0;
        }

        protected void btn_OrderReps_Click(object sender, EventArgs e)
        {
            MultiView5.ActiveViewIndex = 0;
           
        }

        protected void btn_SIReps_Click(object sender, EventArgs e)
        {
            MultiView5.ActiveViewIndex = 1;
            MultiView6.ActiveViewIndex = 0;
        }

        protected void btn_DelReps_Click(object sender, EventArgs e)
        {
            MultiView5.ActiveViewIndex = 2;
            MultiView7.ActiveViewIndex = 0;
        }

        protected void btn_Inventory_Click(object sender, EventArgs e)
        {
            MultiView3.ActiveViewIndex = 2;
        }

        protected void ddwn_cats_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void ddwn_Stats_SelectedIndexChanged(object sender, EventArgs e)
        {
            stable();
        }

        protected void btn_SaveQ_Click(object sender, EventArgs e)
        {

        }

        protected void btn_ClearQ_Click(object sender, EventArgs e)
        {

        }

        protected void btn_Setting_Click(object sender, EventArgs e)
        {
            lbl_reOrder.Visible = true;
            txt_Reorder.Visible = true;
            lbl_CretLev.Visible = true;
            txt_Crit.Visible = true;
            btn_EditCrit.Visible = true;
            btn_Close.Visible = true;

            string display = "select reorder,critical from systeminventorysettingtbl where id = '1'";
            SqlCommand jk = new SqlCommand(display,magnaconn);
            SqlDataReader hh;
            magnaconn.Open();
            hh = jk.ExecuteReader();
            hh.Read();
            string data1 = hh.GetString(0).ToString();
            string data2 = hh.GetString(1).ToString();
            magnaconn.Close();

            txt_Reorder.Text = data1;
            txt_Crit.Text = data2;

            stable();
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
            string numgetter = "SELECT COUNT(*) as Numbers FROM systeminventorysettingtbl ";
            SqlCommand c1 = new SqlCommand(numgetter, magnaconn);
            SqlDataReader re1;
            magnaconn.Open();
            re1 = c1.ExecuteReader();
            re1.Read();
            int number = Convert.ToInt32(re1.GetInt32(0));
            magnaconn.Close();

            if (number != 0)
            {
                string upds = "update systeminventorysettingtbl set reorder = '" + txt_Reorder.Text + "', critical = '" + txt_Crit.Text + "' where id = '1'";
                SqlCommand dcs = new SqlCommand(upds, magnaconn);
                magnaconn.Open();
                dcs.ExecuteNonQuery();
                magnaconn.Close();
            }
            else if (number == 0)
            {
                if (magnaconn.State == ConnectionState.Open)
                    magnaconn.Close();
                magnaconn.Open();
                string upds = "insert into SystemInventorySettingTBL (reorder,critical)values('" + txt_Reorder.Text + "','" + txt_Crit.Text + "')";
                SqlCommand dcs = new SqlCommand(upds, magnaconn);
                dcs.ExecuteNonQuery();
                magnaconn.Close();
            }
            //    Response.Write("save");
            //}
            //catch
            //{ if walay row
            //    
            //}

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

        //protected void grid_Inv_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    stable();
        //    ViewState["Quans"] = grid_Inv.SelectedRow.Cells[4].Text;
        //    ViewState["d1"] = grid_Inv.SelectedRow.Cells[1].Text;
        //    ViewState["d2"] = grid_Inv.SelectedRow.Cells[3].Text;
        //}

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

        private void Update()
        {
            try
            {
                if (magnaconn.State == ConnectionState.Open)
                    magnaconn.Close();
                magnaconn.Open();
                string range = "Select reorder,critical from systeminventorysettingtbl where id = 1";
                SqlCommand comander = new SqlCommand(range, magnaconn);
                SqlDataReader rader;
                rader = comander.ExecuteReader();

                rader.Read();
                string reoder = rader.GetString(0);
                string critical = rader.GetString(1);
                magnaconn.Close();

                int a = Convert.ToInt32(reoder);
                int b = Convert.ToInt32(critical);

                magnaconn.Open();
                string set = "update systemunitsinventorytbl set status = 'Safety' where quantity > " + a + "";
                SqlCommand bb = new SqlCommand(set, magnaconn);
                bb.ExecuteNonQuery();
                magnaconn.Close();

                magnaconn.Open();
                string set2 = "update systemunitsinventorytbl set status = 'Re-order' where quantity between " + b + " and " + a + "";
                SqlCommand bb2 = new SqlCommand(set2, magnaconn);
                bb2.ExecuteNonQuery();
                magnaconn.Close();

                magnaconn.Open();
                string set1 = "update systemunitsinventorytbl set status = 'Critical' where quantity <= " + b + "";
                SqlCommand bb1 = new SqlCommand(set1, magnaconn);
                bb1.ExecuteNonQuery();
                magnaconn.Close();

                magnaconn.Open();
                string set3 = "update systemunitsinventorytbl set status = 'Out' where quantity = '0'";
                SqlCommand bb3 = new SqlCommand(set3, magnaconn);
                bb3.ExecuteNonQuery();
                magnaconn.Close();
            }
            catch
            {
                if (magnaconn.State == ConnectionState.Open)
                    magnaconn.Close();
            }

            stable();

        }

        protected void grid_Userinvoices_SelectedIndexChanged(object sender, EventArgs e)
        {
            string ppp = ViewState["ponn"].ToString();
            string inv = grid_Userinvoices.SelectedRow.Cells[1].Text;
            magnaconn.Open();
            string mq = "select ponumber,invoiceno,dateReceived,deliverydate,subtotal,discount,discountedamount,tax,vat,servicecharge,totalservicecharge,total from systemreportstbl where ponumber = '" + ppp + "' and invoiceno = '" + inv + "'";
            SqlCommand sir = new SqlCommand(mq, magnaconn);
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
            magnaconn.Close();

            string update = "update systemreportstbl set status = 'Viewed' where ponumber = '" + ppp + "' and invoiceno = '" + inv + "'";
            SqlCommand up = new SqlCommand(update, magnaconn);
            magnaconn.Open();
            up.ExecuteNonQuery();
            magnaconn.Close();

            //salesinvoice
            lbl_POnum.Text = ViewState["po"].ToString();
            lbl_InvNum.Text = ViewState["inv"].ToString();
            lbl_Date.Text = ViewState["dr"].ToString();
            lbl_DeliveryDate.Text = ViewState["dd"].ToString();

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

            lbl_ST.Text = ViewState["st"].ToString();
            lbl_D.Text = ViewState["dis"].ToString();
            lbl_DA.Text = ViewState["da"].ToString();
            lbl_Percentage.Text = ViewState["tax"].ToString();
            lbl_VAT.Text = ViewState["vat"].ToString();
            lbl_SC.Text = ViewState["sc"].ToString();
            lbl_TSC.Text = ViewState["tsc"].ToString();
            lbl_T.Text = ViewState["total"].ToString();
            //salesinvoice


            MultiView6.ActiveViewIndex = 1;
        }

        protected void grid_DeliveryList_SelectedIndexChanged(object sender, EventArgs e)
        {
            string pons = ViewState["ponn"].ToString();
            string delnum = grid_DeliveryList.SelectedRow.Cells[1].Text;
            magnaconn.Open();
            string mq = "select ponumber,deliveryno,dateReceived,deliverydate,status,hauler,plateno,gatepassno,note from systemreportstbl where ponumber = '" + pons + "' and deliveryno = '" + delnum + "'";
            SqlCommand sir = new SqlCommand(mq, magnaconn);
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

            magnaconn.Close();

            //must reset status

            //delivery
            lbl_delPO.Text = ViewState["po"].ToString();
            lbl_delDate.Text = ViewState["dre"].ToString();
            lbl_delDDate.Text = ViewState["deld"].ToString();
            lbl_delNum.Text = ViewState["delnos"].ToString();

            string refdel = delnum.Replace(" ", "");
            ViewState["refdel"] = refdel;
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

            //if (ViewState["hauler"] == null)
            //{
            //    btn_Set.Enabled = !false;
            //    btn_Confirm.Enabled = !false;

            //    txt_hauler.Text = "(None)";
            //    txt_plate.Text = "(None)";
            //    txt_GatePass.Text = "(None)";
            //}
            //else
            //{
            lbl_Hauler.Text = ViewState["hauler"].ToString();
            lbl_PlateNum.Text = ViewState["platenumber"].ToString();
            lbl_GatePassNum.Text = ViewState["getpass"].ToString();

            //    btn_Set.Enabled = false;
            //    btn_Confirm.Enabled = false;
            //}
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
            //lbl_SelectHauler.Visible = !true;
            //ddwn_haulers.Visible = !true;
            //lbl_SelectPlate.Visible = !true;
            //ddwn_plates.Visible = !true;
            //lbl_Include.Visible = !true;
            //checks_Notes.Visible = !true;
            //settings


            MultiView7.ActiveViewIndex = 1;
        }

        protected void btn_UpdateInventory_Click(object sender, EventArgs e)
        {
            




            recsconn.Open();
            string ass2 = "select  ModelCode,Color,Damaged as 'No. of Damages' from a" + ViewState["refdel"].ToString() + "_Units";
            SqlDataAdapter adapter2 = new SqlDataAdapter(ass2, recsconn);
            DataSet set2 = new DataSet();
            adapter2.Fill(set2);
            grid_damages.DataSource = set2.Tables[0];
            grid_damages.DataBind();
            recsconn.Close();

            MultiView7.ActiveViewIndex = 2;
        }
        
        protected void grid_damages_SelectedIndexChanged(object sender, EventArgs e)
        {
            ViewState["selectedmc"] = grid_damages.SelectedRow.Cells[1].Text;
            ViewState["selectedc"] = grid_damages.SelectedRow.Cells[2].Text;
        }

        protected void btn_damagedSave_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(txt_numberDam.Text))
            {

            }
            else
            {
                recsconn.Open();
                string ass2 = "update a" + ViewState["refdel"].ToString() + "_Units set damaged = '" + txt_numberDam.Text + "' where modelcode = '" + ViewState["selectedmc"].ToString() + "' and color = '" + ViewState["selectedc"].ToString() + "'";
                SqlCommand mm = new SqlCommand(ass2, recsconn);
                // DataSet set2 = new DataSet();
                // adapter2.Fill(set2);
                // grid_damages.DataSource = set2.Tables[0];

                mm.ExecuteNonQuery();

                recsconn.Close();


                recsconn.Open();
                string ass2e = "select  ModelCode,Color,Damaged as 'No. of Damages' from a" + ViewState["refdel"].ToString() + "_Units";
                SqlDataAdapter adapter2 = new SqlDataAdapter(ass2e, recsconn);
                DataSet set2 = new DataSet();
                adapter2.Fill(set2);
                grid_damages.DataSource = set2.Tables[0];
                grid_damages.DataBind();
                recsconn.Close();
            }
        }

        protected void btn_ContUpdate_Click(object sender, EventArgs e)
        {
            int x = grid_Delivery1.Rows.Count;
            for (int y = 0; y <= x - 1; y++)
            {
                string mc = grid_Delivery1.Rows[y].Cells[0].Text;
                string c = grid_Delivery1.Rows[y].Cells[1].Text;
                string q = grid_Delivery1.Rows[y].Cells[2].Text;

                try
                {
                    recsconn.Open();
                    string ass2e = "select  Damaged from a" + ViewState["refdel"].ToString() + "_Units where modelcode = '" + mc + "' and color = '" + c + "' ";
                    SqlCommand dam = new SqlCommand(ass2e, recsconn);
                    SqlDataReader res;
                    res = dam.ExecuteReader();
                    res.Read();
                    string damaged = res.GetString(0).ToString();
                    recsconn.Close();

                    int dams = Convert.ToInt32(q) - Convert.ToInt32(damaged);

                    string add = "select quantity from systemunitsinventorytbl where modelcode = '" + mc + "' and color = '" + c + "'";
                    SqlCommand po = new SqlCommand(add, magnaconn);
                    SqlDataReader ko;
                    magnaconn.Open();
                    ko = po.ExecuteReader();
                    ko.Read();
                    string quan = ko.GetString(0).ToString();
                    magnaconn.Close();

                    int newq = Convert.ToInt32(quan) + Convert.ToInt32(dams);

                    string update = "update systemunitsinventorytbl set quantity = '" + newq + "' where modelcode = '" + mc + "' and color = '" + c + "'";
                    SqlCommand ys = new SqlCommand(update, magnaconn);
                    magnaconn.Open();
                    ys.ExecuteNonQuery();
                    magnaconn.Close();

                    Update();
                }
                catch // pag bago ang item or walang laman ang inventory
                {
                    if (magnaconn.State == ConnectionState.Open)
                        magnaconn.Close();

                    string description = "select description from a" + ViewState["ponn"].ToString() + "_units where modelcode = '" + mc + "' and color = '" + c + "'";
                    SqlCommand po = new SqlCommand(description, magnaconn);
                    SqlDataReader ko;
                    magnaconn.Open();
                    ko = po.ExecuteReader();
                    ko.Read();
                    string desc = ko.GetString(0).ToString();
                    magnaconn.Close();
                    //int d = grid_damages.Rows.Count;
                    //for (int w = 0; w <= d - 1; w++)
                    //{
                        string mc1 = grid_Delivery1.Rows[y].Cells[0].Text;
                        string c1 = grid_Delivery1.Rows[y].Cells[1].Text;

                        recsconn.Open();
                        string ass2e = "select  Damaged from a" + ViewState["refdel"].ToString() + "_Units where modelcode = '" + mc1 + "' and color = '" + c1 + "' ";
                        SqlCommand dam = new SqlCommand(ass2e, recsconn);
                        SqlDataReader res;
                        res = dam.ExecuteReader();
                        res.Read();
                        string damaged = res.GetString(0).ToString();
                        recsconn.Close();

                        int dams = Convert.ToInt32(q) - Convert.ToInt32(damaged);

                        magnaconn.Open();
                        string insert = "insert into systemunitsinventorytbl (modelcode,description,color,quantity)values('" + mc + "','" + desc + "','" + c + "','" + dams + "')";
                        SqlCommand mand = new SqlCommand(insert, magnaconn);
                        mand.ExecuteNonQuery();
                        magnaconn.Close();
                        Update();
                    //}
                        
                }

            }
        }

        protected void btn_replaced_Click(object sender, EventArgs e)
        {
            //if (String.IsNullOrEmpty(txt_numberDam.Text))
            //{

            //}
            //else
            //{
            string mc = grid_damages.SelectedRow.Cells[1].Text;
            string c = grid_damages.SelectedRow.Cells[2].Text;
            string q = grid_damages.SelectedRow.Cells[3].Text;

            string add = "select quantity from systemunitsinventorytbl where modelcode = '" + mc + "' and color = '" + c + "'";
            SqlCommand po = new SqlCommand(add, magnaconn);
            SqlDataReader ko;
            magnaconn.Open();
            ko = po.ExecuteReader();
            ko.Read();
            string quan = ko.GetString(0).ToString();
            magnaconn.Close();

            int newquan = Convert.ToInt32(q) + Convert.ToInt32(quan);

            string joke = "update systemunitsinventorytbl set quantity = '" + newquan + "'  where modelcode = '" + mc + "' and color = '" + c + "'";
            SqlCommand ITS = new SqlCommand(joke,magnaconn);
            magnaconn.Open();
            ITS.ExecuteNonQuery();
            magnaconn.Close();



                recsconn.Open();
                string ass2 = "update a" + ViewState["refdel"].ToString() + "_Units set damaged = '0' where modelcode = '" + ViewState["selectedmc"].ToString() + "' and color = '" + ViewState["selectedc"].ToString() + "'";
                SqlCommand mm = new SqlCommand(ass2, recsconn);
                // DataSet set2 = new DataSet();
                // adapter2.Fill(set2);
                // grid_damages.DataSource = set2.Tables[0];

                mm.ExecuteNonQuery();

                recsconn.Close();


                recsconn.Open();
                string ass2e = "select  ModelCode,Color,Damaged as 'No. of Damages' from a" + ViewState["refdel"].ToString() + "_Units";
                SqlDataAdapter adapter2 = new SqlDataAdapter(ass2e, recsconn);
                DataSet set2 = new DataSet();
                adapter2.Fill(set2);
                grid_damages.DataSource = set2.Tables[0];
                grid_damages.DataBind();
                recsconn.Close();
            //}
        }

        protected void grid_Inv_SelectedIndexChanged(object sender, EventArgs e)
        {
            ViewState["inv1"] = grid_Inv.SelectedRow.Cells[1].Text;
            ViewState["inv2"] = grid_Inv.SelectedRow.Cells[3].Text;
            ViewState["inv3"] = grid_Inv.SelectedRow.Cells[4].Text;
        }

        protected void btn_Deduct_Click(object sender, EventArgs e)
        {
            if (!(String.IsNullOrEmpty(txt_Sales.Text)))
            {
                int minus = Convert.ToInt32(txt_Sales.Text);
                int minee = Convert.ToInt32(ViewState["inv3"]);
                if (minus > minee)
                {
                    string dif = "update systemunitsinventorytbl set quantity = '0' where modelcode = '" + ViewState["inv1"].ToString() + "' and color = '" + ViewState["inv2"].ToString() + "'";
                    SqlCommand change = new SqlCommand(dif, magnaconn);
                    magnaconn.Open();
                    change.ExecuteNonQuery();
                    magnaconn.Close();

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
                    Update();
                }
                else if (minus < minee)
                {
                    int ff = Convert.ToInt32(ViewState["inv3"]) - Convert.ToInt32(txt_Sales.Text);

                    string dif = "update systemunitsinventorytbl set quantity = '" + ff + "' where modelcode = '" + ViewState["inv1"].ToString() + "' and color = '" + ViewState["inv2"].ToString() + "'";
                    SqlCommand change = new SqlCommand(dif, magnaconn);
                    magnaconn.Open();
                    change.ExecuteNonQuery();
                    magnaconn.Close();

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
                    Update();
                }
            }
        }

       


    }
}