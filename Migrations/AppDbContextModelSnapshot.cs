using ArtikelKu.Api.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

#nullable disable

namespace ArtikelKu.Api.Migrations;

[DbContext(typeof(AppDbContext))]
partial class AppDbContextModelSnapshot : ModelSnapshot
{
    protected override void BuildModel(ModelBuilder modelBuilder)
    {
#pragma warning disable 612, 618
        modelBuilder
            .HasAnnotation("ProductVersion", "8.0.25")
            .HasAnnotation("Relational:MaxIdentifierLength", 64);

        modelBuilder.Entity("ArtikelKu.Api.Models.Article", entity =>
        {
            entity.Property<int>("Id")
                .ValueGeneratedOnAdd()
                .HasColumnType("INTEGER")
                .HasAnnotation("Sqlite:Autoincrement", true);

            entity.Property<string>("AuthorName")
                .IsRequired()
                .HasColumnType("TEXT");

            entity.Property<string>("Content")
                .IsRequired()
                .HasColumnType("TEXT");

            entity.Property<DateTime>("CreatedAt")
                .HasColumnType("TEXT");

            entity.Property<string>("Summary")
                .IsRequired()
                .HasColumnType("TEXT");

            entity.Property<string>("Title")
                .IsRequired()
                .HasColumnType("TEXT");

            entity.HasKey("Id");
            entity.ToTable("Articles");
        });

        modelBuilder.Entity("ArtikelKu.Api.Models.User", entity =>
        {
            entity.Property<int>("Id")
                .ValueGeneratedOnAdd()
                .HasColumnType("INTEGER")
                .HasAnnotation("Sqlite:Autoincrement", true);

            entity.Property<DateTime>("CreatedAt")
                .HasColumnType("TEXT");

            entity.Property<string>("Email")
                .IsRequired()
                .HasColumnType("TEXT");

            entity.Property<string>("PasswordHash")
                .IsRequired()
                .HasColumnType("TEXT");

            entity.Property<string>("Username")
                .IsRequired()
                .HasColumnType("TEXT");

            entity.HasKey("Id");
            entity.HasIndex("Email").IsUnique();
            entity.ToTable("Users");
        });
#pragma warning restore 612, 618
    }
}
