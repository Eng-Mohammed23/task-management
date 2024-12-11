using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaskManagement.Api.Entities;

namespace TaskManagement.Api.Persistence.EntitiesConfigurations;

public class TaskConfigurations : IEntityTypeConfiguration<MyTask>
{
    public void Configure(EntityTypeBuilder<MyTask> builder)
    {
        
        builder.Property(x => x.Title).HasMaxLength(300);
    }
}