using ComingAndLivingSystem.Data;
using ComingAndLivingSystem.Interfaces;
using ComingAndLivingSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ComingAndLivingSystem.Controllers
{
    public class KPPController : Controller
    {
        private readonly IShiftRepository _shiftRepository;

        public KPPController(IShiftRepository shiftRepository)
        {
            _shiftRepository = shiftRepository;
        }
        [HttpPost("start-shift/{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        public IActionResult StartShift(int id, [FromBody] DateTime startTime)
        {
            try
            {
                _shiftRepository.StartShift(id, startTime);
                return Ok("Смена начата успешно.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("end-shift/{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        public IActionResult EndShift(int id, [FromBody] DateTime endTime)
        {
            try
            {
                _shiftRepository.EndShift(id, endTime);
                return Ok("Смена завершена успешно.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("current-shift/{id}")]
        [ProducesResponseType(200, Type = typeof(Shift))]
        [ProducesResponseType(404)]
        public IActionResult GetCurrentShift(int id)
        {
            var shift = _shiftRepository.GetCurrentShift(id);
            if (shift == null)
            {
                return NotFound("Текущая смена не найдена.");
            }
            return Ok(shift);
        }
    }
}
