﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Uni.Scan.Domain.Entities;
using Uni.Scan.Infrastructure.Contexts;
using Uni.Scan.Infrastructure.Extensions;

namespace Uni.Scan.Infrastructure.Contexts
{
    public partial class UniContext
    {

        public UniContext(DbContextOptions<UniContext> options)
            : base(options)
        {
        }

        public virtual DbSet<LogisticTask> LogisticTasks { get; set; }

        public virtual DbSet<InventoryTask> InventoryTasks { get; set; }
        public virtual DbSet<InventoryTaskOperation> InventoryTaskOperations { get; set; }
        public virtual DbSet<InventoryTaskItem> InventoryTaskItems { get; set; }
        public virtual DbSet<LogisticArea> LogisticAreas { get; set; }
        public virtual DbSet<LogisticTaskDetail> LogisticTaskDetails { get; set; }
        public virtual DbSet<LogisticTaskLabel> LogisticTaskLabels { get; set; }
        public virtual DbSet<StockAnomaly> StockAnomalies { get; set; } 
        public virtual DbSet<Material> Materials { get; set; }
        public virtual DbSet<LogisticParametres> LogisticParametre { get; set; }
        public virtual DbSet<LabelTemplate> LabelTemplates { get; set; }
        public virtual DbSet<ScanningCode> ScanningCodes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            OnIdentityModelCreating(modelBuilder);

            modelBuilder.Entity<LogisticTask>(entity =>
            {
                entity.ToTable("LogisticTask");
                modelBuilder.SetStringMaxLenght<LogisticTask>(250);
            });

            modelBuilder.Entity<LogisticArea>(entity =>
            {
                entity.ToTable("LogisticArea");

                modelBuilder.SetStringMaxLenght<LogisticArea>(250);
            });

            modelBuilder.Entity<LogisticTaskDetail>(entity =>
            {
                entity.ToTable(nameof(LogisticTaskDetail));
                entity.HasOne(d => d.Task)
                    .WithMany(p => p.LogisticTaskDetails)
                    .HasForeignKey(d => d.TaskId)
                    .OnDelete(DeleteBehavior.NoAction)
                    .HasConstraintName("FK_TaskTaskLine");

                modelBuilder.SetStringMaxLenght<LogisticTaskDetail>(250);
            });

            //modelBuilder.Entity<LogisticTaskMaterialInput>(entity =>
            //{
            //    entity.ToTable(nameof(LogisticTaskMaterialInput));
            //    entity.HasOne(d => d.Task)
            //        .WithMany(p => p.LogisticTaskMaterialInputs)
            //        .HasForeignKey(d => d.TaskId)
            //        .OnDelete(DeleteBehavior.NoAction)
            //        .HasConstraintName("FK_TaskLogisticTaskMaterialInput");

            //    modelBuilder.SetStringMaxLenght<LogisticTaskMaterialInput>(250);
            //});

            //modelBuilder.Entity<LogisticTaskMaterialOutput>(entity =>
            //{
            //    entity.ToTable(nameof(LogisticTaskMaterialOutput)  );
            //    entity.HasOne(d => d.Task)
            //        .WithMany(p => p.LogisticTaskMaterialOutputs)
            //        .HasForeignKey(d => d.TaskId)
            //        .OnDelete(DeleteBehavior.NoAction)
            //        .HasConstraintName("FK_TaskLogisticTaskMaterialOutput");

            //    modelBuilder.SetStringMaxLenght<LogisticTaskMaterialOutput>(250);
            //});

            modelBuilder.Entity<LogisticTaskLabel>(entity =>
            {
                entity.ToTable(nameof(LogisticTaskLabel));
                entity.HasOne(d => d.Task)
                    .WithMany(p => p.LogisticTaskLabels)
                    .HasForeignKey(d => d.TaskId)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("FK_TaskLineLabel");

                modelBuilder.SetStringMaxLenght<LogisticTaskLabel>(250);
            });

           
            modelBuilder.Entity<Material>(entity =>
            {
                entity.ToTable(nameof(Material));

                modelBuilder.SetStringMaxLenght<Material>(250);
            });

            modelBuilder.Entity<LabelTemplate>(entity =>
            {
                entity.ToTable(nameof(LabelTemplate));

                modelBuilder.SetStringMaxLenght<LabelTemplate>(250);
            });
            modelBuilder.Entity<StockAnomaly>(entity =>
            {
                entity.ToTable(nameof(StockAnomaly));

                modelBuilder.SetStringMaxLenght<StockAnomaly>(250);
            });
            modelBuilder.Entity<LogisticParametres>(entity =>
            {
                entity.ToTable(nameof(LogisticParametres));

                modelBuilder.SetStringMaxLenght<LogisticParametres>(250);
            });
            modelBuilder.Entity<ScanningCode>(entity =>
            {
                entity.ToTable(nameof(ScanningCode));

                modelBuilder.SetStringMaxLenght<LogisticParametres>(250);
            });
            #region inventory

            modelBuilder.Entity<InventoryTask>(entity =>
            {
                entity.ToTable(nameof(InventoryTask) );
                modelBuilder.SetStringMaxLenght<InventoryTask>(250);
            });
   
            modelBuilder.Entity<InventoryTaskOperation>(entity =>
            {
                entity.ToTable(nameof(InventoryTaskOperation));
                entity.HasOne(d => d.Task)
                    .WithMany(p => p.InventoryTaskOperations)
                    .HasForeignKey(d => d.TaskObjectID)
                    .OnDelete(DeleteBehavior.ClientNoAction)
                    .HasConstraintName("FK_InventoryTask_Operation");

                modelBuilder.SetStringMaxLenght<InventoryTaskOperation>(250);
            });
            modelBuilder.Entity<InventoryTaskItem>(entity =>
            {
                entity.ToTable(nameof(InventoryTaskItem) );
                entity.HasOne(d => d.Operation)
                    .WithMany(p => p.InventoryTaskItems)
                    .HasForeignKey(d => d.OperationObjectID)
                    .OnDelete(DeleteBehavior.ClientNoAction)
                    .HasConstraintName("FK_InventoryTaskOperation_Item");

                modelBuilder.SetStringMaxLenght<InventoryTaskItem>(250);
            });

            #endregion




            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}