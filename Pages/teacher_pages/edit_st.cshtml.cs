using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace prj_it.Pages.teacher_pages
{
    public class edit_stModel : PageModel
    {
        public St_info st = new St_info();
        public string err = "";
        public string suc = "";

        public bool val = false;
        
        public void OnGet()
        {

            string id = Request.Query["id"];
            if (string.IsNullOrEmpty(id))
            {
                err = "NO Val";
                return;

                
            }
            try
            {

                String connetionString = "Data Source=DESKTOP-73R3HS1\\SQLEXPRESS;Initial Catalog=school;Integrated Security=True";
                using (SqlConnection conn = new SqlConnection(connetionString))
                {
                    conn.Open();


                    string select_st = "SELECT * FROM student WHERE id = '"+id+"'";
                    using (SqlCommand cmd = new SqlCommand(select_st, conn))
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {

                            if (reader.Read())
                            {
                                st.id = "" + reader.GetInt32(0);
                                st.name = reader.GetString(1);
                                st.age = "" + reader.GetInt32(2);
                                st.clz = "" + reader.GetInt32(3);
                                st.pwd = reader.GetString(4);
                                st.email = reader.GetString(5);
                                //st_list.Add(st);
                            }


                        }

                    }



                }



            }
            catch (Exception ex)
            {
                err = ex.Message;   
            }
        }
        

        public void OnPost()
        {
            string id = Request.Query["id"];
            st.id = Request.Query["id"];
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
            try
            {
                String connetionString = "Data Source=DESKTOP-73R3HS1\\SQLEXPRESS;Initial Catalog=school;Integrated Security=True";
                using (SqlConnection conn = new SqlConnection(connetionString))
                {
                    conn.Open();


                    string insert_st = "UPDATE student SET s_name = @name  , age = @age , class = @clz, pwd = @pwd , email = @email WHERE ID = '"+id+"';";
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
            catch (Exception ex)
            {
                err = ex.Message;
                return;
            }


            // clear all fields
            st.email = ""; st.pwd = ""; st.name = ""; st.age = ""; st.clz = "";
            suc = "All fields add successfully";

          
            Response.Redirect("/teacher_pages/main_t");

        }
    }
}
