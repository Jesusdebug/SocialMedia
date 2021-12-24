using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SocialMedia.Core.Entities;

namespace SocialMedia.Infraestructure.Data.Cofigurationa
{
    class UserConfiguration:IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("Usuario");

            builder.HasKey(e => e.Id);

            builder.Property(e => e.Id)
                .HasColumnName("IdUsuario");

            builder.Property(e => e.FirtName)
                .HasColumnName("Nombres")
                .IsRequired()
                .HasMaxLength(50)
                .IsUnicode(false);

            builder.Property(e => e.LastName)
                .HasColumnName("Apellidos")
                .IsRequired()
                .HasMaxLength(50)
                .IsUnicode(false);

            builder.Property(e => e.Email)
                .IsRequired()
                .HasMaxLength(30)
                .IsUnicode(false);

            builder.Property(e => e.DateOfBirth)
                .HasColumnName("FechaNacimiento")
                .HasColumnType("date");

            builder.Property(e => e.Telephone)
                .HasColumnName("Telefono")
                .HasMaxLength(10)
                .IsUnicode(false);

            builder.Property(e => e.IsActivo)
                .HasColumnName("Activo");
        }
    
    }
}
