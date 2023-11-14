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
        /// <summary>
        /// Получить все задания
        /// </summary>
        /// <param name="GetAllExercises"></param>
        /// <returns>Список заданий</returns>
        /// <response code="200">Успешно получен список заданий</response>
        /// <response code="400">Неправильная модель данных</response>
        /// <response code="401">Не авторизованно</response>
        [HttpGet(Name = "GetAllExercises"), Authorize(Roles = "Student, Admin")]
        public async Task<IActionResult> GetAllExercises()
        {
            var exercises = await _repository.Exercise.GetExercisesAsync(trackChanges: false);
            var exercisesDto = _mapper.Map<IEnumerable<ExerciseDto>>(exercises);
            return Ok(exercisesDto);
        }
        /// <summary>
        /// Получить задание по Id
        /// </summary>
        /// <param name="ExerciseById"></param>
        /// <returns>Задание</returns>
        /// <response code="200">Успешно получено задание</response>
        /// <response code="400">Неправильная модель данных</response>
        /// <response code="401">Не авторизованно</response>
        /// <response code="404">Не найдено задание</response>
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
        /// <summary>
        /// Создать задание
        /// </summary>
        /// <param name="CreateExercise"></param>
        /// <returns>Задание</returns>
        /// <response code="201">Успешно создано задание</response>
        /// <response code="400">Неправильная модель данных</response>
        /// <response code="401">Не авторизованно</response>
        /// <response code="403">Авторизованно с неверной ролью</response>
        [HttpPost(Name = "CreateExercise"), Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateExercise([FromBody] ExerciseForCreationDto exercise)
        {
            var exerciseEntity = _mapper.Map<Exercise>(exercise);
            _repository.Exercise.CreateExercise(exerciseEntity);
            await _repository.SaveAsync();
            var exerciseToReturn = _mapper.Map<ExerciseDto>(exerciseEntity);
            return CreatedAtRoute("ExerciseById", new { id = exerciseToReturn.Id }, exerciseToReturn);
        }
        /// <summary>
        /// Изменить задание
        /// </summary>
        /// <param name="UpdateExercise"></param>
        /// <returns>Задание</returns>
        /// <response code="204">Успешно изменено задание</response>
        /// <response code="400">Неправильная модель данных</response>
        /// <response code="401">Не авторизованно</response>
        /// <response code="403">Авторизованно с неверной ролью</response>
        /// <response code="404">Не найдено задание</response>
        [HttpPut("{id}", Name = "UpdateExercise"), Authorize(Roles = "Admin")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        [ServiceFilter(typeof(ValidateExerciseExistsAttribute))]
        public async Task<IActionResult> UpdateExercise(Guid id, [FromBody] ExerciseForUpdateDto exercise)
        {
            var exerciseEntity = HttpContext.Items["exercise"] as Exercise;
            if (exerciseEntity == null)
            {
                _logger.LogInfo($"Exercise with id: {id} doesn't exist in the database.");
                return NotFound();
            }
            _mapper.Map(exercise, exerciseEntity);
            await _repository.SaveAsync();
            return NoContent();
        }
        /// <summary>
        /// Удалить задание
        /// </summary>
        /// <param name="DeleteExercise"></param>
        /// <returns>No_Content</returns>
        /// <response code="204">Успешно удалено задание</response>
        /// <response code="400">Неправильная модель данных</response>
        /// <response code="401">Не авторизованно</response>
        /// <response code="403">Авторизованно с неверной ролью</response>
        /// <response code="404">Не найдено задание</response>
        [HttpDelete("{id}", Name = "DeleteExercise"), Authorize(Roles = "Admin")]
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
