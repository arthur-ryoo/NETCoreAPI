using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
// Install Symstem.Data.SqlClient
using System.Data.SqlClient;
using System.Data;
using NETCoreAPI.Models;

namespace NETCoreAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        // Dependency Injection
        private readonly IConfiguration _configuration;

        public DepartmentController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        public JsonResult Get()
        {
            DataTable departmentTable = new DataTable();
            // Database connection string
            using(SqlConnection sqlConnection = new SqlConnection(_configuration.GetConnectionString("EmployeeAppCon")))
            {
                sqlConnection.Open();
                SqlDataAdapter sqlDa = new SqlDataAdapter("sp_GetAllDepartmentIdAndName", sqlConnection);
                sqlDa.SelectCommand.CommandType = CommandType.StoredProcedure;
                sqlDa.Fill(departmentTable);
            }

            return new JsonResult(departmentTable);
        }

        [HttpPost]
        public JsonResult Post(Department dep)
        {
            DataTable departmentTable = new DataTable();
            // Database connection string
            using (SqlConnection sqlConnection = new SqlConnection(_configuration.GetConnectionString("EmployeeAppCon")))
            {
                sqlConnection.Open();
                SqlCommand sqlCmd = new SqlCommand("sp_InsertDepartmentByName", sqlConnection);
                sqlCmd.CommandType = CommandType.StoredProcedure;
                sqlCmd.Parameters.AddWithValue("DepartmentName", dep.DepartmentName);
                sqlCmd.ExecuteNonQuery();
            }

            return new JsonResult("Added Successfully");
        }

        [HttpPut]
        public JsonResult Put(Department dep)
        {
            string query = @"
                    update dbo.Department set
                    DepartmentName = '" + dep.DepartmentName + @"'
                    where DepartmentId = " + dep.DepartmentId + @"
                    ";
            DataTable table = new DataTable();
            // Database connection string
            string sqlDataSource = _configuration.GetConnectionString("EmployeeAppCon");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);

                    myReader.Close();
                    myCon.Close();
                }
            }

            return new JsonResult("Updated Successfully");
        }

        [HttpDelete("{id}")]
        public JsonResult Delete(int id)
        {
            string query = @"
                    delete from dbo.Department
                    where DepartmentId = " + id + @"
                    ";
            DataTable table = new DataTable();
            // Database connection string
            string sqlDataSource = _configuration.GetConnectionString("EmployeeAppCon");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);

                    myReader.Close();
                    myCon.Close();
                }
            }

            return new JsonResult("Deleted Successfully");
        }
    }
}
