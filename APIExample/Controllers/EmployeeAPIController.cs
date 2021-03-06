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
        public IEnumerable<EmpProjectModel> Get()
        {
            try
            {
                var data = from e in db.Employees
                           join p in db.ProjectInfoes
                           on e.projid equals p.projid
                           select new EmpProjectModel { EmpID = e.EmpID, EmpName = e.EmpName, Dept = e.Dept, Desg = e.Desg, Salary = (double)e.Salary, projid = (int)e.projid, password = e.password };
                //this Get() method retrieves all employees from the table
                return data;
            }
            catch (Exception ex)
            {
                throw ex;
            }
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
        [Route("api/EmployeeAPI/Login/{name}/{pwd}")]
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
                    olddata.projid = newemp.projid;
                    olddata.password = newemp.password;
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
