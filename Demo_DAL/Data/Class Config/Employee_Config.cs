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
    internal class Employee_Config : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder.HasKey(e => e.ID);
            builder.Property(e => e.ID).UseIdentityColumn();

            builder.Property(e=> e.Name).IsRequired().HasColumnType("nvarchar").HasMaxLength(100);

            builder.Property(e=> e.Address).IsRequired();

            builder.Property(e=> e.Salary).HasColumnType("decimal(12,2)");

            builder.Property(e=> e.Gender)
                   .HasConversion(
                        
                        (Gender)=> Gender.ToString(),  // save in databas
                        (GenderAsString)=> (Sex) Enum.Parse(typeof(Sex),GenderAsString,true)  // return from database (enum)
                                );

            builder.Property(e => e.EmpType)
                    .HasConversion(
                        (t) => t.ToString(),     // save in databas
                        (t) => (EmployeeType) Enum.Parse(typeof(EmployeeType), t, true)  // return from database (enum)
                );
        }
    }
}
