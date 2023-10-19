using Microsoft.AspNetCore.Mvc;

namespace TickitingSystems.Controllers
{
    public class ProjectController : Controller
    {
        DBEntities.TicketingSystemContext context = new DBEntities.TicketingSystemContext();

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult AddNewProject()
        {
            return View();
        }

        public IActionResult SaveNewProject(Models.ProjectModel projectModel)
        {

            DBEntities.Project project = new DBEntities.Project();

            project.ClientName = projectModel.ClientName;
            project.ContactPerson = projectModel.ContactPerson;
            project.DeliverDate = projectModel.DeliverDate;
            project.IsUnderSupport = projectModel.IsUnderSupport;
            project.ProjectName = projectModel.ProjectName;

            context.Projects.Add(project);
            context.SaveChanges();


            return RedirectToAction("GetAllProjects");
        }

        public IActionResult EditProject(int Id)
        {
            DBEntities.Project project = new DBEntities.Project();

            project = context.Projects.Where(x => x.ProjectId == Id).FirstOrDefault();

            Models.ProjectModel projectModel = new Models.ProjectModel();
            projectModel.ProjectId = project.ProjectId;
            projectModel.ClientName = project.ClientName;
            projectModel.ContactPerson = project.ContactPerson;
            projectModel.DeliverDate = project.DeliverDate;
            projectModel.IsUnderSupport = project.IsUnderSupport;
            projectModel.ProjectName = project.ProjectName;


            return View(projectModel);
        }

        public IActionResult UpdateProject(Models.ProjectModel projectModel)
        {

            DBEntities.Project project = new DBEntities.Project();
            project = context.Projects.Where(x => x.ProjectId == projectModel.ProjectId).FirstOrDefault();

            project.ClientName = projectModel.ClientName;
            project.ContactPerson = projectModel.ContactPerson;
            project.DeliverDate = projectModel.DeliverDate;
            project.IsUnderSupport = projectModel.IsUnderSupport;
            project.ProjectName = project.ProjectName;

            context.SaveChanges();

            return RedirectToAction("GetAllProjects");
        }

        public IActionResult GetAllProjects()
        {
            List<Models.ProjectModel> lst = new List<Models.ProjectModel>();
            lst = (from obj in context.Projects.ToList()
                   select new Models.ProjectModel
                   {
                       ProjectId = obj.ProjectId,
                       ProjectName = obj.ProjectName,
                       ContactPerson = obj.ContactPerson,
                       ClientName = obj.ClientName,
                       DeliverDate = obj.DeliverDate,
                       IsUnderSupport = obj.IsUnderSupport,

                   }).ToList();

            return View(lst);
        }
    }
}
