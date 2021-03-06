﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using MySql.Data.MySqlClient;
using System.Web.Configuration;
using PlatformManage.Backstage.MySql_utils;
using PlatformManage.Backstage.utils;

namespace PlatformManage.Backstage.child {
    public partial class user_list : System.Web.UI.Page {
        protected void Page_Load(object sender, EventArgs e) {
            if (!IsPostBack) {
                FilledCurrentDataGrid();
            }
        }

        private static DataTable g_dt = null;
        private string[] other_cabinet_column = { "序号", "合同编号", "项目", "业主", "合同确定时间" };
        protected void FilledCurrentDataGrid() {
            string select_string = "SELECT * FROM ORDER_FORM";
            MySqlCmd.MySqlAdapter adapter = new MySqlCmd.MySqlAdapter();
            adapter.grid_view = this.GridView1;
            adapter.columns = other_cabinet_column;
            FormFillTools.FilledCurrentForm(WebConfigurationManager.ConnectionStrings["senshang_database_connection_string"].ToString(),
                                            select_string, ref adapter);
            g_dt = adapter.data_table;
        }

        protected void FilledCurrentDataGrid(string search_string) {
            string select_string = "SELECT * FROM ORDER_FORM WHERE `" + Database.convert_columns_name("item") + "` = \"" +
                                   search_string.Trim() + "\" OR `" + Database.convert_columns_name("owner") + "` =\"" + search_string.Trim()
                                   + "\" OR `" + Database.convert_columns_name("confirm_number") + "` =\"" + search_string.Trim() + "\"";
            MySqlCmd.MySqlAdapter adapter = new MySqlCmd.MySqlAdapter();
            adapter.grid_view = this.GridView1;
            adapter.columns = other_cabinet_column;
            FormFillTools.FilledCurrentForm(WebConfigurationManager.ConnectionStrings["senshang_database_connection_string"].ToString(),
                                            select_string, ref adapter);
            g_dt = adapter.data_table;
        }

        private static int page_count = 0;
        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e) {
            this.GridView1.PageIndex = e.NewPageIndex;
            page_count = e.NewPageIndex;
            FilledCurrentDataGrid();
        }

        protected void SearchButton_Click(object sender, EventArgs e) {
            FilledCurrentDataGrid(this.search_text.Value.ToString());
        }

        private void create_cmd(ref MySqlCmd.MySqlContext udata) {
            //udata.context = "UPDATE ORDER_FORM SET `" + Database.convert_columns_name(this.real_install_time.ID)
            //                + "`=\"" + this.real_install_time.Value
            //                + "\", `" + Database.convert_columns_name(this.last_install_issue.ID)
            //                + "`=\"" + this.last_install_issue.Value
            //                + "\", `" + Database.convert_columns_name(this.comment.ID)
            //                + "`=\"" + this.comment.Value
            //                + "\" WHERE `序号`=\"" + this.number.Value + "\"";
        }

        protected void SubmitButton_Click(object sender, EventArgs e) {
            MySqlCmd.MySqlContext input_udata = new MySqlCmd.MySqlContext();
            input_udata.conn = MySqlCmd.Connection(WebConfigurationManager.ConnectionStrings["senshang_database_connection_string"].ToString());
            input_udata.status = MySqlRequest.UPDATE;
            input_udata.create_cmd = create_cmd;

            MySqlCmd.SetMySqlCommand(ref input_udata);
            FilledCurrentDataGrid();
        }

        protected void GridView1_SelectedIndexChanging(object sender, GridViewSelectEventArgs e) {
            //ClientScript.RegisterStartupScript(this.Page.GetType(), "", "<script>document.getElementById(\"ShowButton\").click()</script>");

            //int count = e.NewSelectedIndex + 7 * page_count;
            //for (int i = 0; i < g_dt.Rows[count].ItemArray.Length; i++) {
            //    this.number.Value = g_dt.Rows[count].ItemArray[0].ToString();
            //    this.confirm_number.Value = g_dt.Rows[count].ItemArray[1].ToString();
            //    this.item.Value = g_dt.Rows[count].ItemArray[2].ToString();
            //    this.owner.Value = g_dt.Rows[count].ItemArray[3].ToString();
            //}
        }
    }
}