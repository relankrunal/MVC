using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
namespace BL
{
    public class EmployeeBL
    {
        DataSet ds;
        string connectionstring = ConfigurationManager.ConnectionStrings["SQL"].ConnectionString;
        public IEnumerable<Employee> Employees
        {
            get
            {
                List<Employee> employees = new List<Employee>();
                string connectionstring = ConfigurationManager.ConnectionStrings["SQL"].ConnectionString;
                using (SqlConnection con = new SqlConnection(connectionstring))
                {
                    SqlCommand cmd = new SqlCommand("spGetAllEmployees", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();
                    ds = new DataSet();
                    SqlDataAdapter da = new SqlDataAdapter();
                    da.SelectCommand = cmd;
                    da.Fill(ds);

                }


                return employees = ds.Tables[0].AsEnumerable().Select(dataRow => new Employee
                {
                    Name = dataRow.Field<string>("Name"),
                    EmployeeId = dataRow.Field<int>("EmployeeId"),
                    City = dataRow.Field<string>("City"),
                    DepartmentId = dataRow.Field<int>("DepartmentId"),
                    Gender = dataRow.Field<string>("Gender"),
                }).ToList();
            }

        }

         
        public void AddEmployee(Employee employee)
        {
           
            using (SqlConnection con = new SqlConnection(connectionstring))
            {
                SqlCommand cmd = new SqlCommand("spAddEmployee", con);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter sqlParameter = new SqlParameter();

                sqlParameter.ParameterName = "@Name";
                sqlParameter.Value = employee.Name;
                cmd.Parameters.Add(sqlParameter);

                SqlParameter ParameterGender = new SqlParameter();
                ParameterGender.ParameterName= "@Gender";
                ParameterGender.Value = employee.Gender;
                cmd.Parameters.Add(ParameterGender);

                SqlParameter sqlParameterCity = new SqlParameter();
                sqlParameterCity.ParameterName = "@City";
                sqlParameterCity.Value = employee.City;
                cmd.Parameters.Add(sqlParameterCity);

                SqlParameter sqlParameter1 = new SqlParameter();
                sqlParameter1.ParameterName = "@Department";
                sqlParameter1.Value = employee.DepartmentId;
                cmd.Parameters.Add(sqlParameter1);

                con.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void UpdateEmployee(Employee employee)
        {
            
            using (SqlConnection con = new SqlConnection(connectionstring))
            {
                SqlCommand cmd = new SqlCommand("spUpdateEmployee", con);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter EId = new SqlParameter();

                EId.ParameterName = "@EmployeeId";
                EId.Value = employee.EmployeeId;
                cmd.Parameters.Add(EId);

                SqlParameter sqlParameter = new SqlParameter();
                sqlParameter.ParameterName = "@Name";
                sqlParameter.Value = employee.Name;
                cmd.Parameters.Add(sqlParameter);

                SqlParameter ParameterGender = new SqlParameter();
                ParameterGender.ParameterName = "@Gender";
                ParameterGender.Value = employee.Gender;
                cmd.Parameters.Add(ParameterGender);

                SqlParameter sqlParameterCity = new SqlParameter();
                sqlParameterCity.ParameterName = "@City";
                sqlParameterCity.Value = employee.City;
                cmd.Parameters.Add(sqlParameterCity);

                SqlParameter sqlParameter1 = new SqlParameter();
                sqlParameter1.ParameterName = "@Department";
                sqlParameter1.Value = employee.DepartmentId;
                cmd.Parameters.Add(sqlParameter1);

                con.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void DeleteEmployee(int Id)
        {
            using (SqlConnection con = new SqlConnection(connectionstring))
            {
                SqlCommand cmd = new SqlCommand("spDeleteEmployee", con);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter EId = new SqlParameter();
                EId.ParameterName = "@EmployeeId";
                EId.Value = Id;
                cmd.Parameters.Add(EId);

                con.Open();
                cmd.ExecuteNonQuery();
            }
        }
    }
}
