using AutoMapper;
using Entities.DataTransferObjects;
using Entities.Models;
using Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MokshenAPI.ActionFilters;
using System.ComponentModel.Design;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace MokshenAPI.Controllers
{
    [Route("api/exercise")]
    [ApiController]
    public class ExercisesController : ControllerBase
    {
        private readonly IRepositoryManager _repository;
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;
        public ExercisesController(IRepositoryManager repository, ILoggerManager logger, IMapper mapper)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
        }
        [HttpGet(Name = "GetAllExercises"), Authorize(Roles = "Student, Admin")]
        public async Task<IActionResult> GetAllExercises()
        {
            var exercises = await _repository.Exercise.GetExercisesAsync(trackChanges: false);
            var exercisesDto = _mapper.Map<IEnumerable<ExerciseDto>>(exercises);
            return Ok(exercisesDto);
        }
        [HttpGet("{id}", Name = "ExerciseById"), Authorize(Roles = "Student, Admin")]
        public async Task<IActionResult> GetExercise(Guid id)
        {
            var exercise = await _repository.Exercise.GetExerciseAsync(id, trackChanges: false);
            if (exercise == null)
            {
                _logger.LogInfo($"Exercise with id: {id} doesn't exist in the database.");
                return NotFound();
            }
            else
            {
                var exerciseDto = _mapper.Map<ExerciseDto>(exercise);
                return Ok(exerciseDto);
            }
        }
        [HttpPost, Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateExercise([FromBody] ExerciseForCreationDto exercise)
        {
            var exerciseEntity = _mapper.Map<Exercise>(exercise);
            _repository.Exercise.CreateExercise(exerciseEntity);
            await _repository.SaveAsync();
            var exerciseToReturn = _mapper.Map<ExerciseDto>(exerciseEntity);
            return CreatedAtRoute("ExerciseById", new { id = exerciseToReturn.Id }, exerciseToReturn);
        }
        [HttpDelete("{id}"), Authorize(Roles = "Admin")]
        [ServiceFilter(typeof(ValidateExerciseExistsAttribute))]
        public async Task<IActionResult> DeleteExercise(Guid id)
        {
            var exercise = HttpContext.Items["exercise"] as Exercise;
            _repository.Exercise.DeleteExercise(exercise);
            await _repository.SaveAsync();
            return NoContent();
        }
    }
}
