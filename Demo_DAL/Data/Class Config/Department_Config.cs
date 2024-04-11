using Demo_DAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo_DAL.Data.Class_Config
{
    internal class Department_Config : IEntityTypeConfiguration<Department>
    {
        public void Configure(EntityTypeBuilder<Department> builder)
        {
            builder.ToTable("Departments");

            builder.HasKey(d=> d.ID);
            builder.Property(d=> d.ID).UseIdentityColumn(10,10);

            builder.HasIndex(d=>d.Code).IsUnique();
            builder.Property(d => d.Code).HasColumnType("nvarchar")
                                        .HasMaxLength(30)
                                        .IsRequired();

            builder.Property(d=> d.Name).HasColumnType("nvarchar")
                                        .HasMaxLength(30)
                                        .IsRequired();

            builder.HasMany(d => d.Employees)
                    .WithOne(e=> e.Department_Nav)
                    .HasForeignKey(e=> e.Department_ID)
                    .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
