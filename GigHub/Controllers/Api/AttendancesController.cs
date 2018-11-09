using GigHub.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Microsoft.AspNet.Identity;
using GigHub.Core.Dtos;
using GigHub.Persistance;
using GigHub.Core;

namespace GigHub.Controllers.Api
{

    public class AttendancesController : ApiController
    {
        private IUnitOfWork _unitOfWork;
        public AttendancesController(IUnitOfWork unitOfWork)    
        {
            _unitOfWork = unitOfWork;
        }
        [HttpPost]
        public IHttpActionResult Attend(AttendanceDto dto)
        {
            string userId = User.Identity.GetUserId();
            
            var attendance = _unitOfWork.Attendance.GetAttendace(dto.GigId, userId);
            if(attendance != null)
                return BadRequest("The attendance already exists!");

            var attendances = new Attendance
            {
                GigId = dto.GigId,
                AttendeeId = userId
            };

            _unitOfWork.Attendance.Add(attendances);
            _unitOfWork.Complete();

            return Ok();
        }

        [HttpDelete]
        public IHttpActionResult DeleteAttendance(int id)
        {
            string userId = User.Identity.GetUserId();
            var attendace = _unitOfWork.Attendance.GetAttendace(id, userId);
            if (attendace == null)
                return NotFound();

            _unitOfWork.Attendance.Remove(attendace);
            _unitOfWork.Complete();
            return Ok(id);
        }
    }
}
