namespace ManagerTasks.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class TasksDbContext : DbContext
    {
        public TasksDbContext()
            : base("name=TasksDbContext")
        {
        }

        public virtual DbSet<CategoryTask> CategoryTasks { get; set; }
        public virtual DbSet<Task> Tasks { get; set; }
        public virtual DbSet<TypeTask> TypeTasks { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CategoryTask>()
                .HasMany(e => e.TypeTasks)
                .WithOptional(e => e.CategoryTask)
                .HasForeignKey(e => e.IdCategoryTask);

            modelBuilder.Entity<TypeTask>()
                .HasMany(e => e.Tasks)
                .WithRequired(e => e.TypeTask)
                .HasForeignKey(e => e.IdTypeTask)
                .WillCascadeOnDelete(false);
        }
    }
}
