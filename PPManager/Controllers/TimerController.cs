using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PPManager.Data;
using PPManager.DTOs;
using PPManager.Models;

namespace PPManager.Controllers
{
    [Authorize]
    public class TimerController : Controller
    {
        private readonly AppDbContext _appDbContext;
        private readonly UserManager<MyUser> _userManager;

        public TimerController(AppDbContext appDbContext, UserManager<MyUser> userManager)
        {
            _appDbContext = appDbContext;
            _userManager = userManager;
        }

        [HttpPost("/Timer/SetTime")]
        public async Task<IActionResult> SetTime([FromBody] TimerSetDTO timerData)
        {
            if (timerData == null || timerData.Start >= timerData.End)
            {
                return BadRequest("Invalid data");
            }
            var timeRecord = new TimerRecord
            {
                Duration = timerData.Duration,
                Start = timerData.Start,
                End = timerData.End,
                UserId = _userManager.GetUserId(User)
            };

            await _appDbContext.TimerRecord.AddAsync(timeRecord);
            await _appDbContext.SaveChangesAsync();
            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> WeekTimerUse()
        {
            var oneWeekAgo = DateTime.Now.AddDays(-5);
            var userId = _userManager.GetUserId(User);

            var dailyUsage = await _appDbContext.TimerRecord
                .Where(tr => tr.UserId == userId && tr.Start >= oneWeekAgo)
                .GroupBy(ts => ts.Start.Date)
                .Select(g => new
                {
                    Day = g.Key,
                    TotalDuration = g.Sum(ts => ts.Duration) 
                })
                .ToListAsync();

            return Ok(dailyUsage);
        }

    }
}
