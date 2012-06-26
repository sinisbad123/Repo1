using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data.SqlClient;

namespace WebApplication1
{
  public partial class WebForm1 : System.Web.UI.Page
  {
    string ConnString;
    SqlConnection scnBuboy;

    protected void Page_Load(object sender, EventArgs e)
    {
      ConnString = "Server = localhost; Database = BuboyDBTO0B2; "
        + "UID = sa; Password = benilde";
      scnBuboy = new SqlConnection(ConnString);
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
      /* validate user name and password */
      /* SELECT */

      string strValidateUser = "SELECT * FROM tblUsers ";
      strValidateUser += "WHERE UserName = '" + txtUserName.Text + "' ";
      strValidateUser += "AND Password = '" + txtPassword.Text + "' ";

      scnBuboy.Open();
      SqlCommand scmValidate = new SqlCommand(strValidateUser, scnBuboy);
      SqlDataReader sdrValidate = scmValidate.ExecuteReader();

      if (sdrValidate.HasRows)
      {
        sdrValidate.Read();
        if (txtUserName.Text == "Admin")
        {
          Label2.Text = "Login successful";
          Session.Add("UserName", sdrValidate["Name"].ToString());
          Response.Redirect("WebForm2.aspx");
        }
        else
        {
          Session.Add("UserID", sdrValidate["UserID"].ToString());
          Response.Redirect("WebForm3.aspx");
        }
      }
      else
      {
        Label2.Text = "Invalid user name and password";
      }
      scnBuboy.Close();
    }
  }
}