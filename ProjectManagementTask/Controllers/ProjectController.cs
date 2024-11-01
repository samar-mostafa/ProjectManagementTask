﻿using AutoMapper;
using Humanizer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjectManagement.Errors;
using ProjectManagement.Models;
using ProjectManagement.Services;
using ProjectManagementTask.Dtos;
using System.Security.Claims;

namespace ProjectManagementTask.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectController : ControllerBase
    {
        private readonly IGenericService<Project> _projectService;
        private readonly IMapper _mapper;
        public ProjectController(IGenericService<Project> projectService, IMapper mapper)
        {
            _projectService = projectService;
            _mapper = mapper;
        }

        [HttpGet("AllProjects")]
        public async Task<IActionResult> Get()
        {
            var projects = _projectService.GetAll(p=>!p.IsDeleted);

            var response = _mapper.Map<IEnumerable<ProjectDto>>(projects);

            return Ok(response);
        }


        [HttpPost("NewProject")]
        public IActionResult Create([FromBody] AddProjectDto project)
        {
            if (!ModelState.IsValid)
                return BadRequest(new ApiResponse(400, "Sorry,Try again later."));
            var entity = _mapper.Map<Project>(project);
            _projectService.Insert(entity);

            return Ok(new ApiResponse(200, "Added Successfully"));
        }

        [HttpPost("ChangeStatus")]
        public IActionResult ChangeStatus(string id)
        {
            var entity = _projectService.GetById(id);
            if (entity == null)
                return BadRequest(new ApiResponse(400, "project not found"));

            entity.Status= !entity.Status;
            _projectService.Update(entity);
            return Ok(new ApiResponse(200, "status changed Successfully"));

        }

        [HttpPost("Edit")]
        public IActionResult Edit(EditProjectDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(new ApiResponse(400, "Sorry,Try again later."));

            var entity = _projectService.GetById(dto.Id);
            if (entity == null)
                return BadRequest(new ApiResponse(400, "project not found"));

             entity = _mapper.Map(dto,entity);
            _projectService.Update(entity);
            return Ok(new ApiResponse(200, "edited  Successfully"));
        }

        [HttpPost("Delete")]
        public IActionResult Delete(string id) 
        {
            var entity = _projectService.GetById(id);
            if (entity == null)
                return BadRequest(new ApiResponse(400, "project not found"));
            entity.IsDeleted = true;
            _projectService.Update(entity);
            return Ok(new ApiResponse(200, "deleted Successfully"));


        }
    }
}
