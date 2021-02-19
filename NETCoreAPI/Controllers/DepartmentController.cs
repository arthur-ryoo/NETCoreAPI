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
            using (SqlConnection sqlConnection = new SqlConnection(_configuration.GetConnectionString("EmployeeAppCon")))
            {
                sqlConnection.Open();
                SqlCommand sqlCmd = new SqlCommand("sp_UpdateDepartmentNameById", sqlConnection);
                sqlCmd.CommandType = CommandType.StoredProcedure;
                sqlCmd.Parameters.AddWithValue("DepartmentId", dep.DepartmentId);
                sqlCmd.Parameters.AddWithValue("DepartmentName", dep.DepartmentName);
                sqlCmd.ExecuteNonQuery();
            }

            return new JsonResult("Updated Successfully");
        }

        [HttpDelete("{id}")]
        public JsonResult Delete(int id)
        {
            using (SqlConnection sqlConnection = new SqlConnection(_configuration.GetConnectionString("EmployeeAppCon")))
            {
                sqlConnection.Open();
                SqlCommand sqlCmd = new SqlCommand("sp_DeleteDepartmentById", sqlConnection);
                sqlCmd.CommandType = CommandType.StoredProcedure;
                sqlCmd.Parameters.AddWithValue("DepartmentId", id);
                sqlCmd.ExecuteNonQuery();
            }

            return new JsonResult("Deleted Successfully");
        }
    }
}
