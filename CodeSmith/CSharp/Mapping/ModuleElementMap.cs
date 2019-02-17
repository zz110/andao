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

namespace Cp.Data.Mapping
{
    public partial class ModuleElementMap
        : System.Data.Entity.ModelConfiguration.EntityTypeConfiguration<Cp.Data.Entities.ModuleElement>
    {
        public ModuleElementMap()
        {
            // table
            ToTable("ModuleElement", "dbo");

            // keys
            HasKey(t => t.Id);

            // Properties
            Property(t => t.Id)
                .HasColumnName("Id")
                .HasMaxLength(50)
                .IsRequired();
            Property(t => t.DomId)
                .HasColumnName("DomId")
                .HasMaxLength(255)
                .IsRequired();
            Property(t => t.Name)
                .HasColumnName("Name")
                .HasMaxLength(255)
                .IsRequired();
            Property(t => t.Attr)
                .HasColumnName("Attr")
                .HasMaxLength(500)
                .IsRequired();
            Property(t => t.Script)
                .HasColumnName("Script")
                .HasMaxLength(500)
                .IsRequired();
            Property(t => t.Icon)
                .HasColumnName("Icon")
                .HasMaxLength(255)
                .IsRequired();
            Property(t => t.Class)
                .HasColumnName("Class")
                .HasMaxLength(255)
                .IsRequired();
            Property(t => t.Remark)
                .HasColumnName("Remark")
                .HasMaxLength(200)
                .IsRequired();
            Property(t => t.Sort)
                .HasColumnName("Sort")
                .IsRequired();
            Property(t => t.ModuleId)
                .HasColumnName("ModuleId")
                .HasMaxLength(50)
                .IsRequired();
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
