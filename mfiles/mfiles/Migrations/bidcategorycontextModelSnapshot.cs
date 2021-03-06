// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using mfiles.Db_Context;

namespace mfiles.Migrations
{
    [DbContext(typeof(bidcategorycontext))]
    partial class bidcategorycontextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                .HasAnnotation("ProductVersion", "3.1.11")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            modelBuilder.Entity("mfiles.Entities.core_bid", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<int>("applicant_id")
                        .HasColumnType("integer");

                    b.Property<int>("category_id")
                        .HasColumnType("integer");

                    b.HasKey("id");

                    b.HasIndex("category_id");

                    b.ToTable("core_bid");

                    b.HasData(
                        new
                        {
                            id = 1,
                            applicant_id = 2,
                            category_id = 1
                        },
                        new
                        {
                            id = 2,
                            applicant_id = 2,
                            category_id = 2
                        },
                        new
                        {
                            id = 3,
                            applicant_id = 3,
                            category_id = 1
                        },
                        new
                        {
                            id = 4,
                            applicant_id = 3,
                            category_id = 2
                        },
                        new
                        {
                            id = 5,
                            applicant_id = 2,
                            category_id = 3
                        },
                        new
                        {
                            id = 6,
                            applicant_id = 8,
                            category_id = 1
                        },
                        new
                        {
                            id = 7,
                            applicant_id = 8,
                            category_id = 2
                        },
                        new
                        {
                            id = 8,
                            applicant_id = 8,
                            category_id = 3
                        });
                });

            modelBuilder.Entity("mfiles.Entities.core_category", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("name")
                        .IsRequired()
                        .HasColumnType("character varying(200)")
                        .HasMaxLength(200);

                    b.HasKey("id");

                    b.ToTable("core_category");

                    b.HasData(
                        new
                        {
                            id = 1,
                            name = "supply of boots"
                        },
                        new
                        {
                            id = 2,
                            name = "repair of forklifts"
                        },
                        new
                        {
                            id = 3,
                            name = "containers"
                        });
                });

            modelBuilder.Entity("mfiles.Entities.core_bid", b =>
                {
                    b.HasOne("mfiles.Entities.core_category", "core_Category")
                        .WithMany("core_Bids")
                        .HasForeignKey("category_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
