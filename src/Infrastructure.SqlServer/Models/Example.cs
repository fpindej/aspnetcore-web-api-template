using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.SqlServer.Models;

internal sealed class Example : IEntityTypeConfiguration<Example>
{
    [Key]
    [Description("Primary key for the Example entity.")]
    public int Id { get; set; }

    [MaxLength(length: 50)]
    [Description("Name of the Example entity.")]
    public string Name { get; set; } = null!;

    [Range(minimum: 0, double.MaxValue, ErrorMessage = "Price must be a positive value.")]
    [Precision(precision: 18, scale: 2)]
    [Description("Price of the Example entity.")]
    public decimal Price { get; set; }

    [MaxLength(length: 200)]
    [Description("Description of the Example entity.")]
    public string? Description { get; set; }

    public void Configure(EntityTypeBuilder<Example> builder)
    {
        builder.HasKey(p => p.Id);

        builder.Property(p => p.Id)
            .ValueGeneratedOnAdd();

        builder.Property(p => p.Name)
            .IsRequired()
            .HasMaxLength(maxLength: 50);

        builder.Property(p => p.Price)
            .IsRequired()
            .HasColumnType("decimal(18,2)");

        builder.Property(p => p.Description)
            .HasMaxLength(maxLength: 200);
    }
}