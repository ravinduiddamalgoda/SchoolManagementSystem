using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using prj_it.Pages.teacher_pages;
using System.Data.SqlClient;
using Microsoft.AspNetCore.Session;

namespace prj_it.Pages.student_pages
{
    public class student_mainModel : PageModel
    {


        public List<Ass_info> as_list = new List<Ass_info>();
        public string err = "";
        public string ses_val;
        public void OnGet()
        {

           
            try
            {

                String connetionString = "Data Source=DESKTOP-73R3HS1\\SQLEXPRESS;Initial Catalog=school;Integrated Security=True";
                using (SqlConnection conn = new SqlConnection(connetionString))
                {
                    conn.Open();


                    string select_st = "SELECT * FROM assignment WHERE s_id = '"+ HttpContext.Session.GetString("_id") + "'";
                    using (SqlCommand cmd = new SqlCommand(select_st, conn))
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {

                            while (reader.Read())
                            {
                                Ass_info asgn1 = new Ass_info();

                                asgn1.a_id = "" + reader.GetInt32(0);
                                asgn1.a_path = reader.GetString(1);

                                asgn1.s_id = "" + reader.GetInt32(2);
                                asgn1.t_id = "" + reader.GetInt32(3);
                                asgn1.sub_date = reader.GetString(4);
                                asgn1.status = reader.GetString(5);

                                as_list.Add(asgn1);

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

    }
}

