using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;
using System.Security.Cryptography.X509Certificates;

namespace prj_it.Pages.teacher_pages
{
    public class main_tModel : PageModel
    {
        public List<St_info> st_list = new List<St_info>();
        public void OnGet()
        {
            try {

                String connetionString = "Data Source=DESKTOP-73R3HS1\\SQLEXPRESS;Initial Catalog=school;Integrated Security=True";
                using (SqlConnection conn = new SqlConnection(connetionString))
                {
                    conn.Open();


                    string select_st = "SELECT * FROM student";
                    using (SqlCommand cmd = new SqlCommand(select_st, conn))
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while(reader.Read())
                            {
                                St_info st = new St_info();
                                st.id = ""+reader.GetInt32(0);
                                st.name= reader.GetString(1);
                                st.age = "" + reader.GetInt32(2);
                                st.clz = ""+ reader.GetInt32(3);
                                st.pwd = reader.GetString(4);
                                st.email= reader.GetString(5);
                                st_list.Add(st);

                            }

                        }

                    }

                    

                }

                

            }
            catch (Exception ex)
            {

            }
            
        }

        


        


    }

    public class St_info
    {
        public string id;
        public string name;
        public string age;
        public string clz;
        public string email;
        public string pwd;

    }
    public class Ass_info
    {
        public string a_id;
        public string a_path;
        public string date;
        public string s_id;
        public string t_id;
        public string sub_date;
        public string status;
    }
}
