using System.Data;
using System.Data.SqlClient;

namespace CoreAPI_November.Model
{
    public class EmpDB
    {
        SqlConnection con;
        public EmpDB()
        {
            con = new SqlConnection("Data Source=SDR\\SQLEXPRESS;Initial Catalog=companydb;Integrated Security=True");
        }
        public string InsertEmployee(EmployeeCls emp)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("emp_insert", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ename", emp.name);
                cmd.Parameters.AddWithValue("@eaddr", emp.address);
                cmd.Parameters.AddWithValue("@esal", emp.salary);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                return "Employee Inserted Successfully";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public string loginEmployee(int id, string name)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("emp_login", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@eid", id);
                cmd.Parameters.AddWithValue("@ename", name);
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    con.Close();
                    return "Login Successful";
                }
                else
                {
                    con.Close();
                    return "Invalid Credentials";
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public List<EmployeeCls> ViewEmployees()
        {
            try
            {
                List<EmployeeCls> empList = new List<EmployeeCls>();
                SqlCommand cmd = new SqlCommand("emp_view", con);
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    EmployeeCls emp = new EmployeeCls();
                    emp.id = Convert.ToInt32(reader["Emp_id"]);
                    emp.name = reader["Emp_Name"].ToString();
                    emp.address = reader["Emp_Address"].ToString();
                    emp.salary = Convert.ToInt32(reader["Emp_salary"]);
                    empList.Add(emp);
                }
                con.Close();
                return empList;
            }
            catch (Exception)
            {
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
                return null;
            }

        }


        public string UpdateEmployee(EmployeeCls emp,int id)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("emp_update", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@eid", id);
                cmd.Parameters.AddWithValue("@addr", emp.address);
                cmd.Parameters.AddWithValue("@esal", emp.salary);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                return "Employee Updated Successfully";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public string DeleteEmployee(int id)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("emp_delete", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@eid", id);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                return "Employee Deleted Successfully";
            }
            catch (Exception ex)
            {
                if(con.State == ConnectionState.Open)   
                {
                    con.Close();
                }
                return ex.Message;
            }
        }

        public EmployeeCls emp_profile(int id)
        {
            EmployeeCls emp = null;

            try
            {
                using (SqlCommand cmd = new SqlCommand("emp_profile", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@eid", id);

                    con.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            emp = new EmployeeCls();
                            emp.id = Convert.ToInt32(reader["Emp_id"]);
                            emp.name = reader["Emp_Name"].ToString();
                            emp.address = reader["Emp_Address"].ToString();
                            emp.salary = Convert.ToInt32(reader["Emp_salary"]);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // log ex.Message as needed
                emp = null;
            }
            finally
            {
                if (con.State == ConnectionState.Open)
                    con.Close();
            }

            return emp;
        }



    }
}

