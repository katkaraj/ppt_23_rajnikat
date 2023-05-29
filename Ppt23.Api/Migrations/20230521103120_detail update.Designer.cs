﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Ppt23.Api.Data;

#nullable disable

namespace Ppt23.Api.Migrations
{
    [DbContext(typeof(PptDbContext))]
    [Migration("20230521103120_detail update")]
    partial class detailupdate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "7.0.4");

            modelBuilder.Entity("Ppt23.Api.Data.Revize", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("DateTime")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<Guid>("VybaveniId")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("VybaveniId");

                    b.ToTable("Revizes");
                });

            modelBuilder.Entity("Ppt23.Api.Data.Vybaveni", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("BoughtDateTime")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("LastRevisionDateTime")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("Price")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.ToTable("Vybavenis");
                });

            modelBuilder.Entity("Ppt23.Api.Data.Revize", b =>
                {
                    b.HasOne("Ppt23.Api.Data.Vybaveni", "Vybaveni")
                        .WithMany("Revizes")
                        .HasForeignKey("VybaveniId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Vybaveni");
                });

            modelBuilder.Entity("Ppt23.Api.Data.Vybaveni", b =>
                {
                    b.Navigation("Revizes");
                });
#pragma warning restore 612, 618
        }
    }
}
