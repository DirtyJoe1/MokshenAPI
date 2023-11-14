using Entities;
using Entities.Models;
using Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Repository
{
    public class ExerciseRepository : RepositoryBase<Exercise>, IExerciseRepository
    {
        public ExerciseRepository(RepositoryContext repositoryContext): base(repositoryContext) { }
        public async Task<IEnumerable<Exercise>> GetExercisesAsync(bool trackChanges) => await FindAll(trackChanges).OrderBy(c => c.Id).ToListAsync();
        public async Task<Exercise> GetExerciseAsync(Guid exerciseId, bool trackChanges) => await FindByCondition(c => c.Id.Equals(exerciseId), trackChanges).SingleOrDefaultAsync();
        public void CreateExercise(Exercise exercise) => Create(exercise);
        public void DeleteExercise(Exercise exercise) => Delete(exercise);

    }
}
