using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interfaces
{
    public interface IExerciseRepository
    {
        Task<IEnumerable<Exercise>> GetExercisesAsync(bool trackChanges);
        Task<Exercise> GetExerciseAsync(Guid id, bool trackChanges);
        void CreateExercise(Exercise exercise);
        void DeleteExercise(Exercise exercise);
    }
}
