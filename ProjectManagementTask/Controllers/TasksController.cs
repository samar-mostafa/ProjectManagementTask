using AutoMapper;
using Humanizer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjectManagement.Errors;
using ProjectManagement.Models;
using ProjectManagement.Services;
using ProjectManagement.Dtos;
using System.Security.Claims;
using ProjectManagement.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using ProjectManagement.Helpers;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;


namespace TaskManagementTask.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = AppRoles.Manager)]
    public class TasksController : ControllerBase
    {
        private readonly IGenericService<ProjectManagement.Models.Task> _TaskService;
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;
        public TasksController(IGenericService<ProjectManagement.Models.Task> TaskService, 
            IMapper mapper, UserManager<User> userManager)
        {
            _TaskService = TaskService;
            _mapper = mapper;
            _userManager = userManager;
        }

        [HttpGet("AllTasks")]
        public IActionResult Get(int page = 1, int pageSize = 10)
        {
            var query = _TaskService.GetAll(t=>!t.IsDeleted); 

            int totalRecords =  query.AsQueryable().Count();
            var totalPages = (int)Math.Ceiling(totalRecords / (double)pageSize);

            page = Math.Max(1, page);
            pageSize = Math.Max(1, pageSize);

            var tasks = query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            var response = new PaginatedResponse<TaskDto>
            {
                Data = _mapper.Map<IEnumerable<TaskDto>>(tasks),
                Page = page,
                PageSize = pageSize,
                TotalPages = totalPages,
                TotalRecords = totalRecords
            };

            return Ok(response);
        }

        [HttpPost("NewTask")]
        public IActionResult Create([FromBody] AddTaskDto task)
        {
            if (!ModelState.IsValid)
                return BadRequest(new ApiResponse(400, "Sorry,Try again later."));
            var entity = _mapper.Map<ProjectManagement.Models.Task>(task);
            entity.Status = (int)task.Status;
            _TaskService.Insert(entity);

            return Ok(new ApiResponse(200, "Added Successfully"));
        }

        [HttpPost("ChangeStatus")]
        public IActionResult ChangeStatus(string id , TaskStatusEnum status)
        {
            var entity = _TaskService.GetById(id);
            if (entity == null)
                return BadRequest(new ApiResponse(400, "Task not found"));

            entity.Status =(int)status;
            _TaskService.Update(entity);
            return Ok(new ApiResponse(200, "status changed Successfully"));

        }

        [Authorize(Policy = "EmployeePolicy")]
        [HttpPost("Edit")]
        public IActionResult Edit(EditTaskDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(new ApiResponse(400, "Sorry,Try again later."));
            var userId = _userManager.GetUserId(User);
            var entity = _TaskService.GetById(dto.Id);
            if (entity == null || entity.AssignedToId != int.Parse(userId))
                return BadRequest(new ApiResponse(400, "You are not allowed to update this task"));

            _mapper.Map(dto, entity);
            _TaskService.Update(entity);
            return Ok(new ApiResponse(200, "edited  Successfully"));
        }

        [HttpPost("Delete")]
        public IActionResult Delete(string id)
        {
            var entity = _TaskService.GetById(id);
            if (entity == null)
                return BadRequest(new ApiResponse(400, "Task not found"));
            entity.IsDeleted = true;
            _TaskService.Update(entity);
            return Ok(new ApiResponse(200, "deleted Successfully"));


        }

        [HttpPost("AssignTo")]
        public IActionResult AssignTo(string taskId ,int userId)
        {
            var entity = _TaskService.GetById(taskId);
            if (entity == null  )
                return BadRequest(new ApiResponse(400, "Task not found"));
            else if (entity.AssignedToId != null)
                return BadRequest(new ApiResponse(400, "Task  Allready has assigned to user"));
            entity.AssignedToId = userId;
            _TaskService.Update(entity);
            return Ok(new ApiResponse(200, "Task assigned Successfully"));

        }

        [HttpGet("Overdue")]
        public IActionResult Overdue() {

            var tasks = _TaskService.
                    GetAll(t => t.EndDate < DateTime.Today &&
                    t.Status == (int)TaskStatusEnum.InProgress && !t.IsDeleted).Select(t => new
                    {
                        Name = t.Name,
                        EndDate = t.EndDate,
                        Status = Enum.GetName(typeof(TaskStatusEnum), t.Status),

                    }).ToList();
            if (tasks == null)
                return BadRequest(new ApiResponse(404, "there is no overdue tasks"));
            else 
                return Ok(tasks);
        }
    }
}
