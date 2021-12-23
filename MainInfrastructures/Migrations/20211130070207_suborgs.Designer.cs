﻿// <auto-generated />
using System;
using MainInfrastructures.Db;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace MainInfrastructures.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20211130070207_suborgs")]
    partial class suborgs
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                .HasAnnotation("ProductVersion", "3.1.11")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            modelBuilder.Entity("Domain.Models.BasedDocuments", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<int>("AcceptedOrg")
                        .HasColumnName("accepted_org")
                        .HasColumnType("integer");

                    b.Property<DateTime>("DocumentDate")
                        .HasColumnName("document_date")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("DocumentName")
                        .HasColumnName("document_name")
                        .HasColumnType("text");

                    b.Property<string>("DocumentNo")
                        .HasColumnName("document_no")
                        .HasColumnType("text");

                    b.Property<int>("DocumentType")
                        .HasColumnName("document_type")
                        .HasColumnType("integer");

                    b.Property<int>("OrganizationId")
                        .HasColumnName("organization_id")
                        .HasColumnType("integer");

                    b.Property<string>("Path")
                        .HasColumnName("path")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("OrganizationId");

                    b.ToTable("based_documents","organizations");
                });

            modelBuilder.Entity("Domain.Models.Organizations", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("AddressDistrict")
                        .HasColumnName("address_district")
                        .HasColumnType("text");

                    b.Property<string>("AddressHomeNo")
                        .HasColumnName("address_home_no")
                        .HasColumnType("text");

                    b.Property<string>("AddressProvince")
                        .HasColumnName("address_province")
                        .HasColumnType("text");

                    b.Property<string>("AddressStreet")
                        .HasColumnName("address_street")
                        .HasColumnType("text");

                    b.Property<string>("Department")
                        .HasColumnName("department")
                        .HasColumnType("text");

                    b.Property<string>("DirectorFirstName")
                        .HasColumnName("director_first_name")
                        .HasColumnType("text");

                    b.Property<string>("DirectorLastName")
                        .HasColumnName("director_last_name")
                        .HasColumnType("text");

                    b.Property<string>("DirectorMail")
                        .HasColumnName("director_mail")
                        .HasColumnType("text");

                    b.Property<string>("DirectorMidName")
                        .HasColumnName("director_mid_name")
                        .HasColumnType("text");

                    b.Property<string>("DirectorPosition")
                        .HasColumnName("director_position")
                        .HasColumnType("text");

                    b.Property<string>("Fax")
                        .HasColumnName("fax")
                        .HasColumnType("text");

                    b.Property<string>("FullName")
                        .HasColumnName("full_name")
                        .HasColumnType("text");

                    b.Property<bool>("IsActive")
                        .HasColumnType("boolean");

                    b.Property<int>("OrgCategory")
                        .HasColumnName("org_category")
                        .HasColumnType("integer");

                    b.Property<string>("OrgMail")
                        .HasColumnName("org_mail")
                        .HasColumnType("text");

                    b.Property<int>("OrgType")
                        .HasColumnName("org_type")
                        .HasColumnType("integer");

                    b.Property<string>("PhoneNumber")
                        .HasColumnName("phone_number")
                        .HasColumnType("text");

                    b.Property<string>("PostIndex")
                        .HasColumnName("post_index")
                        .HasColumnType("text");

                    b.Property<string>("ShortName")
                        .HasColumnName("short_name")
                        .HasColumnType("text");

                    b.Property<int>("UserServiceId")
                        .HasColumnName("user_service_id")
                        .HasColumnType("integer");

                    b.Property<string>("WebSite")
                        .HasColumnName("web_site")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("organization","organizations");
                });

            modelBuilder.Entity("Domain.Models.SubOrganizations", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("Contacts")
                        .HasColumnName("contacts")
                        .HasColumnType("text");

                    b.Property<string>("DirectorFirstName")
                        .HasColumnName("director_first_name")
                        .HasColumnType("text");

                    b.Property<string>("DirectorLastName")
                        .HasColumnName("director_last_name")
                        .HasColumnType("text");

                    b.Property<string>("DirectorMidName")
                        .HasColumnName("director_mid_name")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasColumnName("name")
                        .HasColumnType("text");

                    b.Property<string>("OfficialSite")
                        .HasColumnName("official_site")
                        .HasColumnType("text");

                    b.Property<int?>("OrganizationId")
                        .HasColumnType("integer");

                    b.Property<string>("OwnerType")
                        .HasColumnName("owner_type")
                        .HasColumnType("text");

                    b.Property<int>("ParentId")
                        .HasColumnName("parent_id")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("OrganizationId");

                    b.ToTable("sub_organization","organizations");
                });

            modelBuilder.Entity("Domain.Models.BasedDocuments", b =>
                {
                    b.HasOne("Domain.Models.Organizations", "Organization")
                        .WithMany("BasedDocuments")
                        .HasForeignKey("OrganizationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Domain.Models.SubOrganizations", b =>
                {
                    b.HasOne("Domain.Models.Organizations", "Organization")
                        .WithMany()
                        .HasForeignKey("OrganizationId");
                });
#pragma warning restore 612, 618
        }
    }
}