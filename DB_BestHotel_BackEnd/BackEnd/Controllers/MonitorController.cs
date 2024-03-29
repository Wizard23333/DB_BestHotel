﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using BackEnd.Models;
using BackEnd;
using Microsoft.AspNetCore.Http;

namespace BackEnd.Controllers
{
    [ApiController]
    public class MonitorController : ControllerBase
    {
        private readonly ILogger<MonitorController> _logger;

        public MonitorController(ILogger<MonitorController> logger)
        {
            _logger = logger;
        }
        //获取所有监控信息
        [HttpGet]
        [Route("api/[controller]")]
        [ApiResponseFilterAttribute]
        public List<MonitorRequest> GetMonitor()
        {
            if (HttpContext.Session.GetString("IsLogin") != "OK")
                return null;
            try
            {
                var data = DataAccess.FindMonitorInfo();
                return data;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return null;
        }
        //删除监控设备
        [HttpPost]
        [Route("api/[controller]/Del")]
        [ApiResponseFilterAttribute]
        public StatusCodeResult DelMonitor([FromBody] MonitorRequest value)
        {
            try
            {
                if (DataAccess.DelMonitor(value)==true)
                    return new StatusCodeResult(200);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return new StatusCodeResult(501); 
        }
        //添加监控设备
        [HttpPost]
        [Route("api/[controller]/Add")]
        [ApiResponseFilterAttribute]
        public StatusCodeResult AddMonitor([FromBody] MonitorRequest value)
        {
            try
            {
                if (DataAccess.AddMonitor(value) == true)
                    return new StatusCodeResult(200);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return new StatusCodeResult(501);
        }
    }
}
