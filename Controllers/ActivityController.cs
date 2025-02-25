﻿using API.Data;
using API.DTOs;
using API.entities;
using API.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    public class ActivityController : BaseApiController
    {
        private readonly DataContext _context;
        private readonly ITokenService _tokenService;

        public ActivityController(DataContext context, ITokenService tokenService)
        {
            _context = context;
            _tokenService = tokenService;
        }

        [HttpGet("All_Activities")]
        public async Task<ActionResult<IEnumerable<Activity>>> GetActivities()
        {
            return await _context.Activities.ToListAsync();
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Activity>> GetActivity(int id)
        {
            return await _context.Activities.FindAsync(id);
        }

        [HttpPost("Add_Activity")]
        public async Task<ActionResult<ActivityDto>> AddActivity(ActivityDto activityDto)
        {
            
            
            var activity = new Activity
            {
                ActivityName = activityDto.ActivityName,
                ActivityDate = activityDto.ActivityDate,   
                ActivityType = activityDto.ActivityType,
                ActivityPlace = activityDto.ActivityPlace,
                ChefEquipeId= activityDto.ChefEquipeId,
            };

            _context.Activities.Add(activity);
            await _context.SaveChangesAsync();

            return new ActivityDto
            {
                ActivityName = activity.ActivityName,
                ActivityDate = activity.ActivityDate,
                ActivityType = activity.ActivityType,
                ActivityPlace = activity.ActivityPlace
            };
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<IEnumerable<Activity>>> RemoveActivity(int id)
        {
            var activity = await _context.Activities.FindAsync(id);

            _context.Activities.Remove(activity!);
            await _context.SaveChangesAsync();

            return await GetActivities();
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<Activity>> UpdateActivity(int id, ActivityDto activityDto)
        {
            var activity = await _context.Activities.FindAsync(id);

            activity.ActivityName = activityDto.ActivityName;
            activity.ActivityDate = activityDto.ActivityDate;
            activity.ActivityType = activityDto.ActivityType;
            activity.ActivityPlace = activityDto.ActivityPlace;

            await _context.SaveChangesAsync();
            return await GetActivity(id);
        }

    }
}
