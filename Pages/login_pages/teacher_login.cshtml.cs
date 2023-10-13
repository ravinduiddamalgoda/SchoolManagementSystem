using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace prj_it.Pages.login_pages
{
    public class teacher_loginModel : PageModel
    {

        public string err = "";
        public string suc = "";
        
        public void OnGet()
        {
        }

        public void OnPost()
        {

            string email = Request.Form["t_email"]; // getting database details.
            string password = Request.Form["t_pwd"];

            if (email == null || password == null)// check validations
            {
                err = "Please Enter Data!!!";
                return;
            }
            else
            {
                //database connection
                string sql_query_id = "SELECT tch_id FROM teacher WHERE email = '" + email + "' AND pwd = '" + password + "'";
                bool check = false;
                try
                {
                    String connetionString = "Data Source= DESKTOP-73R3HS1\\SQLEXPRESS;Initial Catalog=school;Integrated Security=True";
                    SqlConnection conn = new SqlConnection(connetionString);

                    conn.Open();

                    SqlCommand cmd = new SqlCommand(sql_query_id, conn);

                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        suc = "Login Success!!";
                        check = true;
                    }
                    else
                    {
                        err = "User Dosen't Exist!!!";

                        //return;

                    }
                }

                catch (SqlException ex)
                {
                    err = ex.Message;

                }

                email = "";
                password = "";
                if (check)
                {
                    Response.Redirect("/teacher_pages/main_t");

                }

            }

        }
    }
}
