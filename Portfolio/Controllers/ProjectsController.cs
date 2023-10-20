using Microsoft.AspNetCore.Mvc;
using Portfolio.Models;
using Portfolio.Services;

namespace Portfolio.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProjectsController : ControllerBase
    {
        public JsonDataService JsonProjectService { get; }
        
        [HttpGet]
        public IEnumerable<Project> Get() => JsonProjectService.GetProjects();

        [HttpPatch]
        public ActionResult Patch([FromBody] Project project)
        {
            JsonProjectService.AddProject(project);
            return Ok();
        }

        public ProjectsController(JsonDataService projectService)
        {
            JsonProjectService = projectService;
        }

    }
}
