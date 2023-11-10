using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class Exercise
    {
        [Column("ExerciseId")]
        public Guid Id { get; set; }
        [Required(ErrorMessage = "Answer is a required field.")]
        public string Answer { get; set; }
        [Required(ErrorMessage = "Answer is a required field.")]
        public string Description { get; set; }
    }
}
