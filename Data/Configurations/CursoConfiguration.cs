using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GerenciadorCursos.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GerenciadorCursos.Data.Configurations
{
        public class CursoConfiguration : IEntityTypeConfiguration<Curso>
    {
        public void Configure(EntityTypeBuilder<Curso> builder)
        {
            builder.ToTable("Curso");
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Titulo).HasColumnType("VARCHAR(80)").IsRequired();
            builder.Property(p => p.HorasDeDuracao).HasColumnType("INT").IsRequired();
            builder.Property(p => p.Status).HasConversion<string>().IsRequired();
        }
    }
}