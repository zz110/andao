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
    public partial class AnswerMap
        : System.Data.Entity.ModelConfiguration.EntityTypeConfiguration<Cp.Data.Entities.Answer>
    {
        public AnswerMap()
        {
            // table
            ToTable("Answer", "dbo");

            // keys
            HasKey(t => t.Id);

            // Properties
            Property(t => t.Id)
                .HasColumnName("Id")
                .HasMaxLength(50)
                .IsRequired();
            Property(t => t.RatersId)
                .HasColumnName("RatersId")
                .HasMaxLength(50)
                .IsOptional();
            Property(t => t.JudgeId)
                .HasColumnName("JudgeId")
                .HasMaxLength(50)
                .IsOptional();
            Property(t => t.Q1)
                .HasColumnName("Q1")
                .HasMaxLength(50)
                .IsOptional();
            Property(t => t.Q2)
                .HasColumnName("Q2")
                .HasMaxLength(50)
                .IsOptional();
            Property(t => t.Q3)
                .HasColumnName("Q3")
                .HasMaxLength(50)
                .IsOptional();
            Property(t => t.Q4)
                .HasColumnName("Q4")
                .HasMaxLength(50)
                .IsOptional();
            Property(t => t.Q5)
                .HasColumnName("Q5")
                .HasMaxLength(50)
                .IsOptional();
            Property(t => t.Q6)
                .HasColumnName("Q6")
                .HasMaxLength(50)
                .IsOptional();
            Property(t => t.Optime)
                .HasColumnName("Optime")
                .IsOptional();
            Property(t => t.State)
                .HasColumnName("State")
                .HasMaxLength(50)
                .IsOptional();
            Property(t => t.PlanId)
                .HasColumnName("PlanId")
                .HasMaxLength(50)
                .IsOptional();
            Property(t => t.PlanName)
                .HasColumnName("PlanName")
                .HasMaxLength(50)
                .IsOptional();

            // Relationships
        }
    }
}
