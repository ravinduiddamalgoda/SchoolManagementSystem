using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Data.SqlClient;
using System.Globalization;

namespace prj_it.Pages.teacher_pages
{
    public class create_stModel : PageModel
    {
        // st1@gmail.com 1234
        public St_info st = new St_info();

        public string err = "";
        public string suc = "";
        public void OnGet()
        {
        }

        public void OnPost() {

            st.name = Request.Form["name"];
            st.age = Request.Form["age"];
            st.email = Request.Form["email"];
            st.clz = Request.Form["class"];
            st.pwd = Request.Form["password"];

            if (st.pwd.Length == 0 || st.age.Length == 0 ||
                st.email.Length == 0 || st.name.Length == 0 || st.clz.Length == 0
                )
            {
                err = "All Fields Are Requested";
                return;
            }

            // add data to database
            try {
                String connetionString = "Data Source=DESKTOP-73R3HS1\\SQLEXPRESS;Initial Catalog=school;Integrated Security=True";
                using (SqlConnection conn = new SqlConnection(connetionString))
                {
                    conn.Open();


                    string insert_st = "INSERT INTO student (s_name ,age , class, pwd, email) VALUES (@name , @age , @clz , @pwd , @email);";
                    using (SqlCommand cmd = new SqlCommand(insert_st, conn))
                    {
                        cmd.Parameters.AddWithValue("@name", st.name);
                        cmd.Parameters.AddWithValue("@age", st.age);
                        cmd.Parameters.AddWithValue("@clz", st.clz);
                        cmd.Parameters.AddWithValue("@pwd", st.pwd);
                        cmd.Parameters.AddWithValue("@email", st.email);

                        cmd.ExecuteNonQuery();  
                    }
                }
            }
            catch(Exception ex)
            {
                err = ex.Message;
                return;
            }


            // clear all fields
            st.email = "";st.pwd = ""; st.name = ""; st.age = ""; st.clz = "";
            suc = "All fields add successfully";

            Response.Redirect("/teacher_pages/main_t");

        }
    
    
    }
}
