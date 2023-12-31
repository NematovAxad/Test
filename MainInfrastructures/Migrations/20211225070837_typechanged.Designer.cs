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
    [Migration("20211225070837_typechanged")]
    partial class typechanged
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

            modelBuilder.Entity("Domain.Models.ContentManager", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("FilePath")
                        .HasColumnName("file_path")
                        .HasColumnType("text");

                    b.Property<string>("FullName")
                        .HasColumnName("full_name")
                        .HasColumnType("text");

                    b.Property<int>("OrganizationId")
                        .HasColumnName("organization_id")
                        .HasColumnType("integer");

                    b.Property<string>("Phone")
                        .HasColumnName("phone")
                        .HasColumnType("text");

                    b.Property<string>("Position")
                        .HasColumnName("position")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("OrganizationId");

                    b.ToTable("content_manager","organizations");
                });

            modelBuilder.Entity("Domain.Models.Deadline", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<DateTime>("DeadlineDate")
                        .HasColumnName("deadline_date")
                        .HasColumnType("timestamp without time zone");

                    b.Property<bool>("IsActive")
                        .HasColumnName("is_active")
                        .HasColumnType("boolean");

                    b.Property<int>("Quarter")
                        .HasColumnName("quarter")
                        .HasColumnType("integer");

                    b.Property<int>("Year")
                        .HasColumnName("year")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.ToTable("deadline","ranking");
                });

            modelBuilder.Entity("Domain.Models.EmployeeStatistics", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<int>("CentralManagementEmployees")
                        .HasColumnName("central_management_employees")
                        .HasColumnType("integer");

                    b.Property<int>("CentralManagementPositions")
                        .HasColumnName("central_management_positions")
                        .HasColumnType("integer");

                    b.Property<int>("DepartmentHeadEmployees")
                        .HasColumnName("department_head_employees")
                        .HasColumnType("integer");

                    b.Property<int>("DepartmentHeadPositions")
                        .HasColumnName("department_head_positions")
                        .HasColumnType("integer");

                    b.Property<int>("HeadEmployees")
                        .HasColumnName("head_employees")
                        .HasColumnType("integer");

                    b.Property<int>("HeadPositions")
                        .HasColumnName("head_positions")
                        .HasColumnType("integer");

                    b.Property<int>("OrganizationId")
                        .HasColumnName("organization_id")
                        .HasColumnType("integer");

                    b.Property<int>("OtherEmployees")
                        .HasColumnName("other_employees")
                        .HasColumnType("integer");

                    b.Property<int>("OtherPositions")
                        .HasColumnName("other_positions")
                        .HasColumnType("integer");

                    b.Property<int>("ProductionPersonnelsEmployee")
                        .HasColumnName("production_personnels_employee")
                        .HasColumnType("integer");

                    b.Property<int>("ProductionPersonnelsPosition")
                        .HasColumnName("production_personnels_position")
                        .HasColumnType("integer");

                    b.Property<int>("ServiceStuffEmployee")
                        .HasColumnName("service_stuff_employee")
                        .HasColumnType("integer");

                    b.Property<int>("ServiceStuffPositions")
                        .HasColumnName("service_stuff_positions")
                        .HasColumnType("integer");

                    b.Property<int>("SpecialistsEmployee")
                        .HasColumnName("specialists_employee")
                        .HasColumnType("integer");

                    b.Property<int>("SpecialistsPosition")
                        .HasColumnName("specialists_position")
                        .HasColumnType("integer");

                    b.Property<int>("SubordinationEmployees")
                        .HasColumnName("subordination_employees")
                        .HasColumnType("integer");

                    b.Property<int>("SubordinationPositions")
                        .HasColumnName("subordination_positions")
                        .HasColumnType("integer");

                    b.Property<int>("TechnicalStuffEmployee")
                        .HasColumnName("technical_stuff_employee")
                        .HasColumnType("integer");

                    b.Property<int>("TechnicalStuffPositions")
                        .HasColumnName("technical_stuff_positions")
                        .HasColumnType("integer");

                    b.Property<int>("TerritorialManagementEmployees")
                        .HasColumnName("territorial_management_employees")
                        .HasColumnType("integer");

                    b.Property<int>("TerritorialManagementPositions")
                        .HasColumnName("territorial_management_positions")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("OrganizationId");

                    b.ToTable("employee_statistics","organizations");
                });

            modelBuilder.Entity("Domain.Models.Field", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<double>("MaxRate")
                        .HasColumnName("max_rate")
                        .HasColumnType("double precision");

                    b.Property<string>("Name")
                        .HasColumnName("name")
                        .HasColumnType("text");

                    b.Property<int>("SphereId")
                        .HasColumnName("sphere_id")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("SphereId");

                    b.ToTable("field","ranking");
                });

            modelBuilder.Entity("Domain.Models.OrganizationApps", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("AndroidAppLink")
                        .HasColumnName("android_app_link")
                        .HasColumnType("text");

                    b.Property<bool>("HasAndroidApp")
                        .HasColumnName("has_android_app")
                        .HasColumnType("boolean");

                    b.Property<bool>("HasIosApp")
                        .HasColumnName("has_ios_app")
                        .HasColumnType("boolean");

                    b.Property<bool>("HasOtherApps")
                        .HasColumnName("has_other_apps")
                        .HasColumnType("boolean");

                    b.Property<bool>("HasResponsiveWebsite")
                        .HasColumnName("has_responsive_website")
                        .HasColumnType("boolean");

                    b.Property<string>("IosAppLink")
                        .HasColumnName("ios_app_link")
                        .HasColumnType("text");

                    b.Property<int>("OrganizationId")
                        .HasColumnName("organization_id")
                        .HasColumnType("integer");

                    b.Property<string>("OtherAppLink")
                        .HasColumnName("other_app_link")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("OrganizationId");

                    b.ToTable("organization_apps","organizations");
                });

            modelBuilder.Entity("Domain.Models.OrganizationDocuments", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

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

                    b.Property<string>("MainPurpose")
                        .HasColumnName("main_purpose")
                        .HasColumnType("text");

                    b.Property<int>("OrganizationId")
                        .HasColumnName("organization_id")
                        .HasColumnType("integer");

                    b.Property<string>("Path")
                        .HasColumnName("path")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("OrganizationId");

                    b.ToTable("organization_documents","organizations");
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

            modelBuilder.Entity("Domain.Models.RankTable", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<int>("FieldId")
                        .HasColumnName("field_id")
                        .HasColumnType("integer");

                    b.Property<bool>("IsException")
                        .HasColumnName("is_exception")
                        .HasColumnType("boolean");

                    b.Property<int>("OrganizationId")
                        .HasColumnName("organization_id")
                        .HasColumnType("integer");

                    b.Property<int>("Quarter")
                        .HasColumnName("quarter")
                        .HasColumnType("integer");

                    b.Property<double>("Rank")
                        .HasColumnName("rank")
                        .HasColumnType("double precision");

                    b.Property<int>("SphereId")
                        .HasColumnName("sphere_id")
                        .HasColumnType("integer");

                    b.Property<int>("Year")
                        .HasColumnName("year")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("FieldId");

                    b.HasIndex("OrganizationId");

                    b.HasIndex("SphereId");

                    b.ToTable("rank_table","ranking");
                });

            modelBuilder.Entity("Domain.Models.Regions", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("Name")
                        .HasColumnName("Name")
                        .HasColumnType("text");

                    b.Property<int>("ParentId")
                        .HasColumnName("parent_id")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.ToTable("regions","organizations");
                });

            modelBuilder.Entity("Domain.Models.ReplacerOrgHead", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("Fax")
                        .HasColumnName("fax")
                        .HasColumnType("text");

                    b.Property<string>("FilePath")
                        .HasColumnName("file_path")
                        .HasColumnType("text");

                    b.Property<string>("FirstName")
                        .HasColumnName("first_name")
                        .HasColumnType("text");

                    b.Property<string>("LastName")
                        .HasColumnName("last_name")
                        .HasColumnType("text");

                    b.Property<string>("MidName")
                        .HasColumnName("mid_name")
                        .HasColumnType("text");

                    b.Property<int>("OrganizationId")
                        .HasColumnName("organization_id")
                        .HasColumnType("integer");

                    b.Property<string>("Phone")
                        .HasColumnName("phone")
                        .HasColumnType("text");

                    b.Property<string>("Position")
                        .HasColumnName("position")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("OrganizationId");

                    b.ToTable("replacer_org_head","organizations");
                });

            modelBuilder.Entity("Domain.Models.SecondSection.HelplineInfo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<bool>("CanGiveFeedbackToHelpline")
                        .HasColumnName("can_give_feedback_to_helpline")
                        .HasColumnType("boolean");

                    b.Property<string>("HelplineNumber")
                        .HasColumnName("helpline")
                        .HasColumnType("text");

                    b.Property<bool>("OfficialSiteHasHelpline")
                        .HasColumnName("official_site_has_helpline")
                        .HasColumnType("boolean");

                    b.Property<bool>("OfficialSiteHasHelplinefeedback")
                        .HasColumnName("official_site_has_helpline_feedback")
                        .HasColumnType("boolean");

                    b.Property<int>("OrganizationId")
                        .HasColumnName("organization_id")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("OrganizationId");

                    b.ToTable("helpline_info","organizations");
                });

            modelBuilder.Entity("Domain.Models.SecondSection.OrgDataFiller", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("Contacts")
                        .HasColumnName("contacts")
                        .HasColumnType("text");

                    b.Property<string>("FullName")
                        .HasColumnName("full_name")
                        .HasColumnType("text");

                    b.Property<int>("OrganizationId")
                        .HasColumnName("organization_id")
                        .HasColumnType("integer");

                    b.Property<string>("Position")
                        .HasColumnName("position")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("OrganizationId");

                    b.ToTable("org_data_filler","organizations");
                });

            modelBuilder.Entity("Domain.Models.SecondSection.OrgHelpline", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<bool>("AcceptableResponseTime")
                        .HasColumnName("acceptable_response_time")
                        .HasColumnType("boolean");

                    b.Property<bool>("HasOnlineConsultant")
                        .HasColumnName("has_online_consultant")
                        .HasColumnType("boolean");

                    b.Property<bool>("OperatesInWorkingDay")
                        .HasColumnName("operates_in_working_day")
                        .HasColumnType("boolean");

                    b.Property<int>("OrganizationId")
                        .HasColumnName("organization_id")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("OrganizationId");

                    b.ToTable("org_helpline","organizations");
                });

            modelBuilder.Entity("Domain.Models.SecondSection.OrganizationMessengers", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("MessengerLink")
                        .HasColumnName("messenger_link")
                        .HasColumnType("text");

                    b.Property<int>("OrganizationId")
                        .HasColumnName("organization_id")
                        .HasColumnType("integer");

                    b.Property<string>("ReasonNotFilling")
                        .HasColumnName("reason_not_filling")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("OrganizationId");

                    b.ToTable("organization_messengers","organizations");
                });

            modelBuilder.Entity("Domain.Models.SecondSection.OrganizationSocialSites", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<int>("OrganizationId")
                        .HasColumnName("organization_id")
                        .HasColumnType("integer");

                    b.Property<string>("SocialSiteLink")
                        .HasColumnName("social_site_link")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("OrganizationId");

                    b.ToTable("org_social_sites","organizations");
                });

            modelBuilder.Entity("Domain.Models.Sphere", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("Name")
                        .HasColumnName("name")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("sphere","ranking");
                });

            modelBuilder.Entity("Domain.Models.SubOrgStatistics", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<int>("CentralManagements")
                        .HasColumnName("central_managements")
                        .HasColumnType("integer");

                    b.Property<int>("OrganizationId")
                        .HasColumnName("organization_id")
                        .HasColumnType("integer");

                    b.Property<int>("Others")
                        .HasColumnName("others")
                        .HasColumnType("integer");

                    b.Property<int>("Subordinations")
                        .HasColumnName("subordinations")
                        .HasColumnType("integer");

                    b.Property<int>("TerritorialManagements")
                        .HasColumnName("territorial_managements")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("OrganizationId");

                    b.ToTable("sub_org_statistics","organizations");
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

                    b.Property<int>("OrganizationId")
                        .HasColumnName("parent_id")
                        .HasColumnType("integer");

                    b.Property<string>("OwnerType")
                        .HasColumnName("owner_type")
                        .HasColumnType("text");

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

            modelBuilder.Entity("Domain.Models.ContentManager", b =>
                {
                    b.HasOne("Domain.Models.Organizations", "Organizations")
                        .WithMany()
                        .HasForeignKey("OrganizationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Domain.Models.EmployeeStatistics", b =>
                {
                    b.HasOne("Domain.Models.Organizations", "Organizations")
                        .WithMany()
                        .HasForeignKey("OrganizationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Domain.Models.Field", b =>
                {
                    b.HasOne("Domain.Models.Sphere", "Sphere")
                        .WithMany()
                        .HasForeignKey("SphereId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Domain.Models.OrganizationApps", b =>
                {
                    b.HasOne("Domain.Models.Organizations", "Organization")
                        .WithMany()
                        .HasForeignKey("OrganizationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Domain.Models.OrganizationDocuments", b =>
                {
                    b.HasOne("Domain.Models.Organizations", "Organization")
                        .WithMany()
                        .HasForeignKey("OrganizationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Domain.Models.RankTable", b =>
                {
                    b.HasOne("Domain.Models.Field", "Field")
                        .WithMany()
                        .HasForeignKey("FieldId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Models.Organizations", "Organization")
                        .WithMany("OrgRanks")
                        .HasForeignKey("OrganizationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Models.Sphere", "Sphere")
                        .WithMany()
                        .HasForeignKey("SphereId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Domain.Models.ReplacerOrgHead", b =>
                {
                    b.HasOne("Domain.Models.Organizations", "Organizations")
                        .WithMany()
                        .HasForeignKey("OrganizationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Domain.Models.SecondSection.HelplineInfo", b =>
                {
                    b.HasOne("Domain.Models.Organizations", "Organizations")
                        .WithMany()
                        .HasForeignKey("OrganizationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Domain.Models.SecondSection.OrgDataFiller", b =>
                {
                    b.HasOne("Domain.Models.Organizations", "Organizations")
                        .WithMany()
                        .HasForeignKey("OrganizationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Domain.Models.SecondSection.OrgHelpline", b =>
                {
                    b.HasOne("Domain.Models.Organizations", "Organizations")
                        .WithMany()
                        .HasForeignKey("OrganizationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Domain.Models.SecondSection.OrganizationMessengers", b =>
                {
                    b.HasOne("Domain.Models.Organizations", "Organizations")
                        .WithMany()
                        .HasForeignKey("OrganizationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Domain.Models.SecondSection.OrganizationSocialSites", b =>
                {
                    b.HasOne("Domain.Models.Organizations", "Organizations")
                        .WithMany()
                        .HasForeignKey("OrganizationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Domain.Models.SubOrgStatistics", b =>
                {
                    b.HasOne("Domain.Models.Organizations", "Organizations")
                        .WithMany()
                        .HasForeignKey("OrganizationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Domain.Models.SubOrganizations", b =>
                {
                    b.HasOne("Domain.Models.Organizations", "Organization")
                        .WithMany("SubOrganizations")
                        .HasForeignKey("OrganizationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
