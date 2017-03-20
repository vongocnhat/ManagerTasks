namespace ManagerTasks.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Task
    {
        public int Id { get; set; }

        [Display(Name = "Type Task Title")]
        public int IdTypeTask { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "Task Title")]
        public string Title { get; set; }

        [Display(Name = "From Date")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime FromDate { get; set; }

        [Display(Name = "Deadline Date")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime DeadlineDate { get; set; }


        [Required]
        [StringLength(250)]
        public string Description { get; set; }

        [Display(Name = "Unit Per")]
        [DisplayFormat(DataFormatString = "{0:0.##}", ApplyFormatInEditMode = true)]
        public double UnitPer { get; set; }

        [Display(Name = "Is Active")]
        public bool IsActive { get; set; }

        public virtual TypeTask TypeTask { get; set; }
    }
}
