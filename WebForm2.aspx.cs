using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data.SqlClient;

namespace WebApplication1
{
  public partial class WebForm2 : System.Web.UI.Page
  {
    string strConnString;
    SqlConnection scnBuboy;

    protected void Page_Load(object sender, EventArgs e)
    {
      strConnString = "SERVER = localhost; Database = BuboyDBTO0B2; "
      + "UID = sa; PWD = benilde";
      scnBuboy = new SqlConnection(strConnString);

      string strName = Session["UserName"].ToString();
      lblWelcome.Text = "Welcome, " + strName;

      // IsPostBack
      // - true => nag PostBack ung page (button click, dropdown item select, etc.)
      // - false => sa unang load ng page

      /* load tblUsers to our GridView */
      /* SELECT */
      scnBuboy.Open();
      if (!IsPostBack)
        RefreshGrid();

      scnBuboy.Close();
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
      /* add user to tblUsers and update GridView */
      /* INSERT */

      scnBuboy.Open();
      string strAddUser = "INSERT INTO tblUsers (UserName, Password, Name) ";
      strAddUser += "VALUES ";
      strAddUser += "('" + txtUserName.Text + "', ";
      strAddUser += "'" + txtPassword.Text + "', ";
      strAddUser += "'" + txtName.Text + "') ";
      SqlCommand scmAddUser = new SqlCommand(strAddUser, scnBuboy);
      scmAddUser.ExecuteNonQuery();
      scmAddUser.Dispose();

      RefreshGrid();

      scnBuboy.Close();
    }

    protected void btnContacts_Click(object sender, EventArgs e)
    {
      Session.Add("UserID", GridView1.SelectedRow.Cells[1].Text);
      Response.Redirect("WebForm3.aspx");
    }

    void RefreshGrid()
    {
      string strGetUsers = "SELECT * FROM tblUsers ";
      SqlCommand scmGetUsers = new SqlCommand(strGetUsers, scnBuboy);
      SqlDataReader sdrGetUsers = scmGetUsers.ExecuteReader();
      GridView1.DataSource = sdrGetUsers;
      GridView1.DataBind();
      scmGetUsers.Dispose();
      sdrGetUsers.Close();

      txtUserName.Text = "";
      txtPassword.Text = "";
      txtName.Text = "";
    }

    protected void btnEditUser_Click(object sender, EventArgs e)
    {
      if (GridView1.SelectedIndex == -1)
        return;

      /* update selected user */
      string strEditUser = "UPDATE tblUsers ";
      strEditUser += "SET UserName = '" + txtUserName.Text + "', ";
      strEditUser += "Password = '" + txtPassword.Text + "', ";
      strEditUser += "Name = '" + txtName.Text + "' ";
      strEditUser += "WHERE UserID = " + GridView1.SelectedRow.Cells[1].Text;

      scnBuboy.Open();
      SqlCommand scmEditUser = new SqlCommand(strEditUser, scnBuboy);
      scmEditUser.ExecuteNonQuery();
      scmEditUser.Dispose();

      RefreshGrid();

      scnBuboy.Close();
    }

    protected void btnDeleteUser_Click(object sender, EventArgs e)
    {
      if (GridView1.SelectedIndex == -1)
        return;

      /* delete selected user */
      string strRemoveUser = "DELETE FROM tblUsers ";
      strRemoveUser += "WHERE UserID = " + GridView1.SelectedRow.Cells[1].Text;

      scnBuboy.Open();
      SqlCommand scmRemoveUser = new SqlCommand(strRemoveUser, scnBuboy);
      scmRemoveUser.ExecuteNonQuery();
      scmRemoveUser.Dispose();

      RefreshGrid();

      scnBuboy.Close();
    }

    protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
    {
      /* copy data from gridview to textboxes */

      txtUserName.Text = GridView1.SelectedRow.Cells[2].Text;
      txtPassword.Text = GridView1.SelectedRow.Cells[3].Text;
      txtName.Text = GridView1.SelectedRow.Cells[4].Text;
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
      /* search given username */

      if (txtSearch.Text.Trim() == "")
      {
        scnBuboy.Open();
        RefreshGrid();
        scnBuboy.Close();
        return;
      }

      string strSearch = "SELECT * FROM tblUsers ";
      strSearch += "WHERE " + drpFields.Text + " = '" + txtSearch.Text + "' ";

      scnBuboy.Open();
      SqlCommand scmSearch = new SqlCommand(strSearch, scnBuboy);
      SqlDataReader sdrSearch = scmSearch.ExecuteReader();
      GridView1.DataSource = sdrSearch;
      GridView1.DataBind();
      scmSearch.Dispose();
      sdrSearch.Close();

      scnBuboy.Close();
    }
  }
}