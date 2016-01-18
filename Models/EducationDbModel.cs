using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Web;


namespace Education_Solution.Models
{
    public class EducationDbModel : DbContext
    {
       public EducationDbModel():base("name=DefaultConnection")
        {

        }

       public DbSet<Question> Question { get; set; }
    }
    
    public class Question
    {
        [Key]
        public int Id { get; set; }
        [StringLength(50)]
        [Index("IX_OPQ", 1, IsUnique = true)]
        public string Owner { get; set; }
        [Index("IX_OPQ", 2, IsUnique = true)]
        [StringLength(50)]
        public string Paper { get; set; }
        [Required]
        [Index("IX_OPQ", 3, IsUnique = true)]
        [StringLength(50)]
        public string QuestionName { get; set; }
        [Required]
        [StringLength(int.MaxValue)]
        public string Text { get; set; }
    }
}