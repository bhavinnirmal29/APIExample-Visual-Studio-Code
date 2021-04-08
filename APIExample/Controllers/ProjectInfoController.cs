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
    [Route("api/ProjectInfo")]
    public class ProjectInfoController : ApiController
    {
        LTIMVCEntities db = new LTIMVCEntities();
        [Route("api/ProjectInfo/GetProjects")]
        [HttpGet]
        public IEnumerable<ProjectInfo> Get()
        {
            try
            {
                return db.ProjectInfoes.ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [Route("api/ProjectInfo/UpdateProject/{pid}")]
        [HttpPut]
        public bool Put(int ?pid,[FromBody]ProjectInfo pinfo)
        {
            try
            {
                int res = db.sp_updateProject(pid, pinfo.projname,pinfo.domain);
                if (res > 0)
                    return true;
            }
            catch(Exception e)
            {
                throw e;
            }
            return false;
        }
        [Route("api/ProjectInfo/SelectProjById/{id}")]
        [HttpGet]
        public sp_SelectProjectbyId_Result Get(int ?id)
        {
            try
            {
                var res = db.sp_SelectProjectbyId(id).SingleOrDefault();
                if (res == null)
                {
                    throw new Exception("Invalid Proj Id");
                }
                else
                {
                    return res;
                }
            }catch(Exception e)
            {
                throw e;
            }
        }
    }
}
