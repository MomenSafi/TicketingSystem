using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TickitingSystems.DBEntities;

namespace TickitingSystems.Controllers
{
    public class IssueDetailsController : Controller
    {
        DBEntities.TicketingSystemContext context = new DBEntities.TicketingSystemContext();
        public IActionResult AddNewIssue()
        {
            ViewBag.Project = context.Projects.Where(x => x.IsUnderSupport == true).ToList();
            ViewBag.Serverity = context.Serverities.ToList();
            ViewBag.IssueStatus = context.IssueStatuses.ToList();

            return View();
        }

        public IActionResult SaveNewIssue(Models.IssueDetailsModel issueDetailsModel)
        {
            DBEntities.IssueDetail obj = new DBEntities.IssueDetail();

            obj.ProjectId = issueDetailsModel.ProjectId;
            obj.ServerityId = issueDetailsModel.ServerityId;
            obj.IssueStatusId = issueDetailsModel.IssueStatusId;
            obj.EnviromentType = issueDetailsModel.EnviromentType;
            obj.IssueDescription = issueDetailsModel.IssueDescription;
            obj.CreatedDate = DateTime.Now;

            context.IssueDetails.Add(obj);
            context.SaveChanges();


            return RedirectToAction("GetAllIssues");
        }

        public IActionResult GetAllIssues()
        {
            List<Models.IssueDetailsModel> lst = new List<Models.IssueDetailsModel>();

            lst = (from obj in context.IssueDetails.ToList()
                   join _pro in context.Projects.ToList() on obj.ProjectId equals _pro.ProjectId
                   join _ser in context.Serverities.ToList() on obj.ServerityId equals _ser.ServerityId
                   join _status in context.IssueStatuses.ToList() on obj.IssueStatusId equals _status.IssueStatesId
                   select new Models.IssueDetailsModel
                   {
                       IssueDetailsId = obj.IssueDetailsId,
                       ProjectName = _pro.ProjectName,
                       ServerityName = _ser.ServerityName,
                       IssueStatusName = _status.IssueStatesName,
                       EnviromentType = obj.EnviromentType,
                       IssueDescription = obj.IssueDescription,
                       CreatedDate = obj.CreatedDate,
                       ResolvedDate = obj.ResolvedDate,
                       ClosedDate = obj.ClosedDate,
                   }).ToList();




            //var result = (from obj in context.IssueDetails.Include(x => x.Project).Include(x => x.Serverity).Include(x => x.IssueStatus).ToList()
            //              select new Models.IssueDetailsModel
            //              {
            //                  IssueDetailsId = obj.IssueDetailsId,
            //                  ProjectName = obj.Project.ProjectName,
            //                  ServerityName = obj.Serverity.ServerityName,
            //                  IssueStatusName = obj.IssueStatus.IssueStatesName,
            //                  EnviromentType = obj.EnviromentType,
            //                  IssueDescription = obj.IssueDescription,
            //                  CreatedDate = obj.CreatedDate,

            //              }).ToList();

            return View(lst);
        }

        public IActionResult EditIssueDetails(int Id)
        {
            ViewBag.Project = context.Projects.ToList();
            ViewBag.Serverity = context.Serverities.ToList();
            ViewBag.IssueStatus = context.IssueStatuses.ToList();

            Models.IssueDetailsModel model = new Models.IssueDetailsModel();
            model = (from x in context.IssueDetails.Where(x => x.IssueDetailsId == Id)
                     select new Models.IssueDetailsModel
                     {
                         IssueDetailsId = x.IssueDetailsId,
                         ProjectId = x.ProjectId,
                         ServerityId = x.ServerityId,
                         IssueStatusId = x.IssueStatusId,
                         IssueDescription = x.IssueDescription,
                         EnviromentType = x.EnviromentType,
                     }).FirstOrDefault();


            return View(model);
        }

        public IActionResult UpdateIssue(Models.IssueDetailsModel issueDetailsModel)
        {
            DBEntities.IssueDetail obj = context.IssueDetails.Where(x => x.IssueDetailsId == issueDetailsModel.IssueDetailsId).FirstOrDefault();

            obj.ProjectId = issueDetailsModel.ProjectId;
            obj.IssueStatusId = issueDetailsModel.IssueStatusId;
            obj.ServerityId = issueDetailsModel.ServerityId;
            obj.EnviromentType = issueDetailsModel.EnviromentType;
            obj.IssueDescription = issueDetailsModel.IssueDescription;

            if (issueDetailsModel.IssueStatusId == 3)
            {
                obj.ResolvedDate = DateTime.Now;
            }
            if (issueDetailsModel.IssueStatusId == 4)
            {
                obj.ClosedDate = DateTime.Now;
            }

            context.SaveChanges();

            DBEntities.IssueDetailsLog log = new DBEntities.IssueDetailsLog();
            log.IssueDetailsId = obj.IssueDetailsId;
            log.IssueStatusId = issueDetailsModel.IssueStatusId;
            log.ActionDate = DateTime.Now;

            context.IssueDetailsLogs.Add(log);
            context.SaveChanges();


            return RedirectToAction("GetAllIssues");
        }

        public IActionResult ViewLogTransaction(int Id)
        {
            List<Models.IssueDetailsLogModel> lst = new List<Models.IssueDetailsLogModel>();

            lst = (from obj in context.IssueDetailsLogs
                   join issueStatuses in context.IssueStatuses on obj.IssueStatusId equals issueStatuses.IssueStatesId
                   where obj.IssueDetailsId == Id
                   select new Models.IssueDetailsLogModel
                   {
                       IssueDetailsId = obj.IssueDetailsId,
                       IssueStatusName = issueStatuses.IssueStatesName,
                       ActionDate = obj.ActionDate,

                   }).ToList();

            return View(lst);
        }

    }
}
