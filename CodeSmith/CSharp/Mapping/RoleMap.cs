﻿//------------------------------------------------------------------------------
// <autogenerated>
//     This code was generated by a CodeSmith Template.
//
//     DO NOT MODIFY contents of this file. Changes to this
//     file will be lost if the code is regenerated.
// </autogenerated>
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OpenAuth.Repository.Mapping
{
    public partial class RoleMap
        : System.Data.Entity.ModelConfiguration.EntityTypeConfiguration<OpenAuth.Repository.Domain.Role>
    {
        public RoleMap()
        {
            // table
            ToTable("Role", "dbo");

            // keys
            HasKey(t => t.Id);

            // Properties
            Property(t => t.Id)
                .HasColumnName("Id")
                .HasMaxLength(50)
                .IsRequired();
            Property(t => t.Name)
                .HasColumnName("Name")
                .HasMaxLength(255)
                .IsRequired();
            Property(t => t.Status)
                .HasColumnName("Status")
                .IsRequired();
            Property(t => t.CreateTime)
                .HasColumnName("CreateTime")
                .IsRequired();
            Property(t => t.CreateId)
                .HasColumnName("CreateId")
                .HasMaxLength(50)
                .IsOptional();
            Property(t => t.TypeName)
                .HasColumnName("TypeName")
                .HasMaxLength(20)
                .IsOptional();
            Property(t => t.TypeId)
                .HasColumnName("TypeId")
                .HasMaxLength(50)
                .IsOptional();

            // Relationships
        }
    }
}
