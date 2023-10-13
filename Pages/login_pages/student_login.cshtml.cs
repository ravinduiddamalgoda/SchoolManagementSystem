using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;
using System.Security.Cryptography.X509Certificates;

namespace prj_it.Pages.login_pages
{
    public class student_loginModel : PageModel
    {
        public void OnGet()
        {
        }
        //student login  st1@gmail.com 1234
        public string err = "";
        public string suc = "";
        public const string SessionKeyName = "_id";
        public string name;
        public string id_val;  
        public void OnPost() { 
            
            string email = Request.Form["s_email"]; // getting database details.
            string password = Request.Form["s_pwd"];

            if (email == null || password == null)// check validations
            {
                err = "Please Enter Data!!!";
                return;
            }
            else
            {
                //database connection
                string sql_query_id = "SELECT ID , s_name FROM student WHERE email = '"+ email + "' AND pwd = '" + password + "'";
                bool check = false;
                try {
                    String connetionString = "Data Source=DESKTOP-73R3HS1\\SQLEXPRESS;Initial Catalog=school;Integrated Security=True";
                    SqlConnection conn = new SqlConnection(connetionString);

                    conn.Open();

                    SqlCommand cmd = new SqlCommand(sql_query_id, conn);

                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        id_val = ""+ reader.GetInt32(0);
                        name = reader.GetString(1);
                        suc = "Login Success!!";
                        check= true;
                    }
                    else
                    {
                        err = "User Dosen't Exist!!!";

                        //return;

                    }
                }

                catch(SqlException ex)
                {
                    err = ex.Message;

                }
                
                 email = "";
                 password = "";
                if (check)
                {
                 
                    HttpContext.Session.SetString(SessionKeyName, id_val);
                    HttpContext.Session.SetString("name", name);
                   
                    Response.Redirect("/student_pages/student_main");

                }

                }

        }    
    }
}
