using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data.SqlClient;

namespace WebApplication1
{
  public partial class WebForm3 : System.Web.UI.Page
  {
    string strConnString;
    SqlConnection scnBuboy;

    protected void Page_Load(object sender, EventArgs e)
    {
      strConnString = "SERVER = localhost; DATABASE = BuboyDBTO0B2; "
        + "UID = sa; PWD = benilde;";
      scnBuboy = new SqlConnection(strConnString);

      /* retrieve Name from tblUsers given UserID */
      /* SELECT */
      
      string strUserID = Session["UserID"].ToString();

      scnBuboy.Open();
      string strGetUser = "SELECT * FROM tblUsers ";
      strGetUser += "WHERE UserID = " + strUserID;
      SqlCommand scmGetUser = new SqlCommand(strGetUser, scnBuboy);
      SqlDataReader sdrUser = scmGetUser.ExecuteReader();      
      sdrUser.Read();
      lblWelcome.Text = "Contacts for " + sdrUser["Name"].ToString();

      scmGetUser.Dispose();
      sdrUser.Close();

      /* retrieve Contacts of given UserID */
      string strGetContacts = "SELECT * FROM tblContacts ";
      strGetContacts += "WHERE UserID = " + strUserID;
      SqlCommand scmGetContacts = new SqlCommand(strGetContacts, scnBuboy);
      SqlDataReader sdrContacts = scmGetContacts.ExecuteReader();
      GridView1.DataSource = sdrContacts;
      GridView1.DataBind();

      scmGetContacts.Dispose();
      sdrContacts.Close();

      scnBuboy.Close();
    }
  }
}