using System;
using Hangfire;
using HangfireOne.Services;
using Microsoft.AspNetCore.Mvc;

namespace HangfireOne.Controllers
{
    [Route("api/[controller]")]
    public class JobCallerController : Controller
    {
        [HttpGet("StaticJobCall")]
        public IActionResult StaticJobCall()
        {
            BackgroundJob.Enqueue(() => StaticService.Count());
            return Ok();
        }

        [HttpGet("StaticJobCallWithAttempt")]
        public IActionResult StaticJobCallWithAttempt()
        {
            BackgroundJob.Enqueue(() => StaticService.CountWithCrashChance());
            return Ok();
        }

        [HttpGet("InstanceJobCall")]
        public IActionResult InstanceJobCall()
        {
            BackgroundJob.Enqueue<InstanceService>(
                service => service.CallBackgroundJob());
            return Ok();
        }

        [HttpGet("InstanceScheduledJobCall")]
        public IActionResult InstanceScheduledJobCall()
        {
            BackgroundJob.Schedule<InstanceService>(
                methodCall: service => service.CallScheduledJob(),
                delay: TimeSpan.FromSeconds(3));
            return Ok();
        }

        [HttpGet("AddOrUpdateRecurrentJob")]
        public IActionResult AddOrUpdateRecurrentJob()
        {
            RecurringJob.AddOrUpdate<InstanceService>(
                recurringJobId: "job-id",
                methodCall: service => service.CallRecurrentJob(),
                cronExpression: Cron.Minutely,
                queue: "critical",
                timeZone: System.TimeZoneInfo.Utc);
            return Ok();
        }

        [HttpGet("TriggerRecurrentJob")]
        public IActionResult TriggerRecurrentJob()
        {
            RecurringJob.Trigger("job-id");
            return Ok();
        }

        [HttpGet("RemoveRecurrentJob")]
        public IActionResult RemoveRecurrentJob()
        {
            RecurringJob.RemoveIfExists("job-id");
            return Ok();
        }
    }
}