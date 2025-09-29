using Microsoft.AspNetCore.Mvc;

namespace ProjectHub.Api.Controllers;

// TODO: Decide whether to expose standalone /tasks routes or nest under projects before wiring CRUD handlers.
[ApiController]
[Route("api/[controller]")]
public class TasksController : ControllerBase
{
    // TODO: Implement task CRUD endpoints and ensure Project relationship validation.
}
