﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data.SqlClient;
using System.Data;

namespace WebApplication1
{
  public partial class WebForm4 : System.Web.UI.Page
  {
    string strConnString = "SERVER = taft-cl3tpc; DATABASE = BuboyDBTO0B2; "
      + "UID = sa; PWD = benilde";
    SqlConnection scnBuboy;
    protected void Page_Load(object sender, EventArgs e)
    {
      scnBuboy = new SqlConnection(strConnString);
    }

    protected void btnAddItem_Click(object sender, EventArgs e)
    {
      /* add items to gridview */
      DataTable dtbItems;
      if (Session["dtbItems"] != null)
      {
        // type casting
        dtbItems = (DataTable)Session["dtbItems"];
      }
      else
      {
        dtbItems = new DataTable("Sales Details");
        dtbItems.Columns.Add("Sales Details ID");
        dtbItems.Columns.Add("Item Name");
        dtbItems.Columns.Add("Quantity");
        dtbItems.Columns.Add("Cost");
      }

      DataRow drwItem = dtbItems.NewRow();      
      drwItem["Cost"] = txtCost.Text;
      drwItem["Item Name"] = txtItemName.Text;
      drwItem["Quantity"] = txtQuantity.Text;
      drwItem["Sales Details ID"] = "";

      dtbItems.Rows.Add(drwItem);

      Session.Add("dtbItems", dtbItems);

      GridView2.DataSource = dtbItems;
      GridView2.DataBind();

      txtItemName.Text = "";
      txtQuantity.Text = "";
    }

    protected void btnSaveSO_Click(object sender, EventArgs e)
    {
      /* save Sales Order and Sales Details to database */      

      /* get TotalCost from GridView2 */
      double tmpTotalCost = 0.0;
      for (int i = 0; i < GridView2.Rows.Count; i++)
        tmpTotalCost += double.Parse(GridView2.Rows[i].Cells[4].Text);
      
      /* INSERT INTO tblSalesOrder ... */
      string strAddSO = "INSERT INTO tblSalesOrder (Date, Name, TotalCost) ";
      strAddSO += "VALUES ";
      strAddSO += "('" + txtDate.Text + "', '" + txtName.Text + "', ";
      strAddSO += tmpTotalCost + ") ";
      SqlCommand scmAddSO = new SqlCommand(strAddSO, scnBuboy);
      scmAddSO.ExecuteNonQuery();
      scmAddSO.Dispose();

      /* INSERT INTO tblSalesDetails ... */

      string strGetSO = "SELECT MAX(SOID) AS [SOID] FROM tblSalesOrder ";
      SqlCommand scmGetSO = new SqlCommand(strGetSO, scnBuboy);
      int intSOID = Convert.ToInt32(scmGetSO.ExecuteScalar());

      string strAddItem;
      for (int i = 0; i < GridView2.Rows.Count; i++)
      {
        strAddItem = "INSERT INTO tblSalesDetails ";
        strAddItem += "(ItemName, Quantity, Cost, SOID) ";
        strAddItem += "VALUES ";
        strAddItem += "('" + GridView2.Rows[i].Cells[2].Text + "', ";
        strAddItem += GridView2.Rows[i].Cells[3].Text + ", ";
        strAddItem += GridView2.Rows[i].Cells[4].Text + ", ";
        strAddItem += intSOID + ") ";
        SqlCommand scmAddItem = new SqlCommand(strAddItem, scnBuboy);
        scmAddItem.ExecuteNonQuery();
        scmAddItem.Dispose();
      }

      scnBuboy.Close();
    }
  }
}