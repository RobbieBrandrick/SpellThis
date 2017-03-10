using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Spellthis.Data;

namespace Spellthis.Migrations
{
    [DbContext(typeof(SpellThisContext))]
    [Migration("20170310121602_AddAudioUrl")]
    partial class AddAudioUrl
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.0-rtm-22752");

            modelBuilder.Entity("Spellthis.Models.Word", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("AddDate");

                    b.Property<string>("AudioFileLocation");

                    b.Property<string>("AudioFileWebUri");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("Words");
                });
        }
    }
}
