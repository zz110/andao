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
    public partial class PlanMap
        : System.Data.Entity.ModelConfiguration.EntityTypeConfiguration<Cp.Data.Entities.Plan>
    {
        public PlanMap()
        {
            // table
            ToTable("Plan", "dbo");

            // keys
            HasKey(t => t.Id);

            // Properties
            Property(t => t.Id)
                .HasColumnName("Id")
                .HasMaxLength(50)
                .IsRequired();
            Property(t => t.PlanName)
                .HasColumnName("PlanName")
                .HasMaxLength(50)
                .IsOptional();
            Property(t => t.PlanStart)
                .HasColumnName("PlanStart")
                .IsOptional();
            Property(t => t.PlanEnd)
                .HasColumnName("PlanEnd")
                .IsOptional();
            Property(t => t.State)
                .HasColumnName("State")
                .IsOptional();
            Property(t => t.RatersId)
                .HasColumnName("RatersId")
                .HasMaxLength(5000)
                .IsOptional();
            Property(t => t.JudgeId)
                .HasColumnName("JudgeId")
                .HasMaxLength(5000)
                .IsOptional();
            Property(t => t.OpTime)
                .HasColumnName("OpTime")
                .HasMaxLength(50)
                .IsOptional();
            Property(t => t.OpAdmin)
                .HasColumnName("OpAdmin")
                .HasMaxLength(50)
                .IsOptional();
            Property(t => t.QuestionId)
                .HasColumnName("QuestionId")
                .HasMaxLength(50)
                .IsOptional();

            // Relationships
        }
    }
}
