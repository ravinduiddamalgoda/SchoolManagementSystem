using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace prj_it.Pages.teacher_pages.assignment
{
    public class add_asgnModel : PageModel
    {
        public void OnGet()
        {
        }
        public Ass_info as_1 = new Ass_info();
        public string err = "";
        public string suc = "";
        public void OnPost() {

            as_1.a_path = Request.Form["a_path"];
            as_1.s_id = Request.Form["s_id_in"];
            as_1.t_id = Request.Form["t_id_in"];
            as_1.sub_date = Request.Form["sub_date"];

            if (as_1.a_path.Length == 0 || as_1.s_id.Length == 0 || as_1.t_id.Length == 0 || as_1.sub_date.Length == 0)
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


                    string insert_st = "INSERT INTO assignment (a_path ,s_id ,t_id , submission , stat) VALUES (@a_path , @s_id , @t_id , @sub_val , @stat);";
                    using (SqlCommand cmd = new SqlCommand(insert_st, conn))
                    {
                        cmd.Parameters.AddWithValue("@a_path", as_1.a_path);
                        cmd.Parameters.AddWithValue("@s_id", as_1.s_id);
                        cmd.Parameters.AddWithValue("@t_id", as_1.t_id);
                        cmd.Parameters.AddWithValue("@sub_val", as_1.sub_date);
                        cmd.Parameters.AddWithValue("@stat" , "pending");

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
            as_1.a_path  = ""; as_1.s_id = ""; as_1.t_id = ""; as_1.sub_date = "";
            suc = "All fields add successfully";

            Response.Redirect("/teacher_pages/assignment/index");



        }
    }
}
