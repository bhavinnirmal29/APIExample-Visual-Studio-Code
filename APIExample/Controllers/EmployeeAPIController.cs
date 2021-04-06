using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using APIExample.Models;


namespace APIExample.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    [Route("api/EmployeeAPI")]
    public class EmployeeAPIController : ApiController
    {
       
        LTIMVCEntities db = new LTIMVCEntities();
        [Route("api/EmployeeAPI/GetAllEmployees")]
        
        [HttpGet]
        public IEnumerable<Employee> Get()
        {
            //this get() method retrieves all rows from the table
            return db.Employees.ToList();
        }
        [Route("api/EmployeeAPI/GetEmployeeByID/{id}")]
        [HttpGet]
        public Employee Get(int id)
        {
            try
            {
                var data = db.Employees.Where(x => x.EmpID == id).SingleOrDefault();
                if (data == null)
                    throw new Exception("Invalid Data");
                else
                    return data;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        [Route("api/EmployeeAPI/{name}/{pwd}")]
        [HttpGet]
        public string Get(string name, string pwd)
        {
            string result = "";
            try
            {
                var data = db.Employees.Where(x => x.EmpName == name && x.password == pwd);
                if (data.Count() == 0)
                    result = "Invalid Credentials";
                else
                    result = "Login Successfull";
            }
            catch (Exception e)
            {
                throw e;
            }
            return result;
        }
        [Route("api/EmployeeAPI/InsertEmployee")]
        [HttpPost]
        public bool Post([FromBody]Employee emp)
        {
            try
            {
                db.Employees.Add(emp);
                var res = db.SaveChanges();
                if (res > 0)
                    return true;
            }
            catch (Exception e)
            {
                throw e;
            }
            return false;
        }
        [Route("api/EmployeeAPI/UpdateEmployee/{id}")]
        [HttpPut]
        public bool Put(int id,[FromBody] Employee newemp)
        {
            try
            {
                var olddata = db.Employees.Where(x => x.EmpID == id).SingleOrDefault();
                if (olddata == null)
                    throw new Exception("Invalid Data");
                else
                {
                    olddata.EmpID = newemp.EmpID;
                    olddata.EmpName = newemp.EmpName;
                    olddata.Desg = newemp.Desg;
                    olddata.Dept = newemp.Dept;
                    olddata.Salary = newemp.Salary;
                    var res = db.SaveChanges();
                    if (res > 0)
                        return true;
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            return false;
        }
        [Route("api/EmployeeAPI/DeleteEmployee/{id}")]
        [HttpDelete]
        public bool Delete(int id)
        {
            try
            {
                var del = db.Employees.Where(x => x.EmpID == id).SingleOrDefault();
                if (del == null)
                {
                    throw new Exception("Invalid Data");
                }
                else
                {
                    db.Employees.Remove(del);
                    var res = db.SaveChanges();
                    if (res > 0)
                        return true;
                }
            }
            catch(Exception e)
            {
                throw e;
            }
            return false;
        }
    }
} 
