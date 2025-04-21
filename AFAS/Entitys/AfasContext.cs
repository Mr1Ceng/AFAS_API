using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace AFAS.Entitys;

public partial class AfasContext : DbContext
{
    public AfasContext()
    {
    }

    public AfasContext(DbContextOptions<AfasContext> options)
        : base(options)
    {
    }

    public virtual DbSet<BAnswer> BAnswers { get; set; }

    public virtual DbSet<BAnswerS1> BAnswerS1s { get; set; }

    public virtual DbSet<BAnswerS1A> BAnswerS1As { get; set; }

    public virtual DbSet<BAnswerS2> BAnswerS2s { get; set; }

    public virtual DbSet<BAnswerS2A> BAnswerS2As { get; set; }

    public virtual DbSet<BAnswerS3> BAnswerS3s { get; set; }

    public virtual DbSet<BAnswerS3A> BAnswerS3As { get; set; }

    public virtual DbSet<BAnswerS4> BAnswerS4s { get; set; }

    public virtual DbSet<BAnswerS5> BAnswerS5s { get; set; }

    public virtual DbSet<BAnswerT1> BAnswerT1s { get; set; }

    public virtual DbSet<BAnswerT1A> BAnswerT1As { get; set; }

    public virtual DbSet<BAnswerT2> BAnswerT2s { get; set; }

    public virtual DbSet<BAnswerT2A> BAnswerT2As { get; set; }

    public virtual DbSet<BAnswerT3> BAnswerT3s { get; set; }

    public virtual DbSet<BAnswerT3A> BAnswerT3As { get; set; }

    public virtual DbSet<BDictionary> BDictionaries { get; set; }

    public virtual DbSet<BDictionaryItem> BDictionaryItems { get; set; }

    public virtual DbSet<BQuestion> BQuestions { get; set; }

    public virtual DbSet<BQuestionS1> BQuestionS1s { get; set; }

    public virtual DbSet<BQuestionS2> BQuestionS2s { get; set; }

    public virtual DbSet<BQuestionS3> BQuestionS3s { get; set; }

    public virtual DbSet<BQuestionS4> BQuestionS4s { get; set; }

    public virtual DbSet<BQuestionS5> BQuestionS5s { get; set; }

    public virtual DbSet<BQuestionT1> BQuestionT1s { get; set; }

    public virtual DbSet<BQuestionT1A> BQuestionT1As { get; set; }

    public virtual DbSet<BQuestionT1Q> BQuestionT1Qs { get; set; }

    public virtual DbSet<BQuestionT2> BQuestionT2s { get; set; }

    public virtual DbSet<BQuestionT2A> BQuestionT2As { get; set; }

    public virtual DbSet<BQuestionT2Q> BQuestionT2Qs { get; set; }

    public virtual DbSet<BQuestionT3> BQuestionT3s { get; set; }

    public virtual DbSet<BQuestionnaire> BQuestionnaires { get; set; }

    public virtual DbSet<BUser> BUsers { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlite("Data Source=../AFAS.Database/AFAS.db");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<BAnswer>(entity =>
        {
            entity.HasKey(e => new { e.AnswerId, e.QuestionnaireId });

            entity.ToTable("b_Answer");

            entity.Property(e => e.AnswerId).HasColumnType("NVARCHAR(25)");
            entity.Property(e => e.QuestionnaireId).HasColumnType("NVARCHAR(6)");
            entity.Property(e => e.UserId).HasColumnType("NVARCHAR(32)");
        });

        modelBuilder.Entity<BAnswerS1>(entity =>
        {
            entity.HasKey(e => new { e.AnswerId, e.QuestionId });

            entity.ToTable("b_Answer_S1");

            entity.Property(e => e.AnswerId).HasColumnType("NVARCHAR(25)");
            entity.Property(e => e.QuestionId).HasColumnType("NVARCHAR(6)");
            entity.Property(e => e.OriginScore).HasColumnType("INT");
            entity.Property(e => e.Remark).HasColumnType("NVARCHAR(200)");
            entity.Property(e => e.StandardScore).HasColumnType("INT");
        });

        modelBuilder.Entity<BAnswerS1A>(entity =>
        {
            entity.HasKey(e => new { e.AnswerId, e.QuestionId, e.GridType });

            entity.ToTable("b_Answer_S1_A");

            entity.Property(e => e.AnswerId).HasColumnType("NVARCHAR(25)");
            entity.Property(e => e.QuestionId).HasColumnType("NVARCHAR(6)");
            entity.Property(e => e.GridType).HasColumnType("NVARCHAR(10)");
            entity.Property(e => e.TimeConsume).HasColumnType("INT");
        });

        modelBuilder.Entity<BAnswerS2>(entity =>
        {
            entity.HasKey(e => new { e.AnswerId, e.QuestionId });

            entity.ToTable("b_Answer_S2");

            entity.Property(e => e.AnswerId).HasColumnType("NVARCHAR(25)");
            entity.Property(e => e.QuestionId).HasColumnType("NVARCHAR(6)");
            entity.Property(e => e.ErrorNumber).HasColumnType("INT");
            entity.Property(e => e.ErrorRate).HasColumnType("DECIMAL(6,2)");
            entity.Property(e => e.MarkNumber).HasColumnType("INT");
            entity.Property(e => e.Remark).HasColumnType("NVARCHAR(200)");
            entity.Property(e => e.StandardScore).HasColumnType("INT");
            entity.Property(e => e.TimeConsume).HasColumnType("INT");
        });

        modelBuilder.Entity<BAnswerS2A>(entity =>
        {
            entity.HasKey(e => new { e.AnswerId, e.QuestionId, e.GridRow, e.GridColumn });

            entity.ToTable("b_Answer_S2_A");

            entity.Property(e => e.AnswerId).HasColumnType("NVARCHAR(25)");
            entity.Property(e => e.QuestionId).HasColumnType("NVARCHAR(6)");
            entity.Property(e => e.GridRow).HasColumnType("INT");
            entity.Property(e => e.GridColumn).HasColumnType("INT");
            entity.Property(e => e.Selected).HasColumnType("BIT");
        });

        modelBuilder.Entity<BAnswerS3>(entity =>
        {
            entity.HasKey(e => new { e.AnswerId, e.QuestionId });

            entity.ToTable("b_Answer_S3");

            entity.Property(e => e.AnswerId).HasColumnType("NVARCHAR(25)");
            entity.Property(e => e.QuestionId).HasColumnType("NVARCHAR(6)");
            entity.Property(e => e.ErrorNumber).HasColumnType("INT");
            entity.Property(e => e.Remark).HasColumnType("NVARCHAR(200)");
            entity.Property(e => e.RightNumber).HasColumnType("INT");
            entity.Property(e => e.StandardScore).HasColumnType("INT");
            entity.Property(e => e.TimeConsume).HasColumnType("INT");
        });

        modelBuilder.Entity<BAnswerS3A>(entity =>
        {
            entity.HasKey(e => new { e.AnswerId, e.QuestionId, e.GridRow, e.GridColumn });

            entity.ToTable("b_Answer_S3_A");

            entity.Property(e => e.AnswerId).HasColumnType("NVARCHAR(25)");
            entity.Property(e => e.QuestionId).HasColumnType("NVARCHAR(6)");
            entity.Property(e => e.GridRow).HasColumnType("INT");
            entity.Property(e => e.GridColumn).HasColumnType("INT");
            entity.Property(e => e.Value).HasColumnType("INT");
        });

        modelBuilder.Entity<BAnswerS4>(entity =>
        {
            entity.HasKey(e => new { e.AnswerId, e.QuestionId });

            entity.ToTable("b_Answer_S4");

            entity.Property(e => e.AnswerId).HasColumnType("NVARCHAR(25)");
            entity.Property(e => e.QuestionId).HasColumnType("NVARCHAR(6)");
            entity.Property(e => e.CrossNumber).HasColumnType("INT");
            entity.Property(e => e.Remark).HasColumnType("NVARCHAR(200)");
            entity.Property(e => e.StandardScore).HasColumnType("INT");
            entity.Property(e => e.TimeConsume).HasColumnType("INT");
        });

        modelBuilder.Entity<BAnswerS5>(entity =>
        {
            entity.HasKey(e => new { e.AnswerId, e.QuestionId });

            entity.ToTable("b_Answer_S5");

            entity.Property(e => e.AnswerId).HasColumnType("NVARCHAR(25)");
            entity.Property(e => e.QuestionId).HasColumnType("NVARCHAR(6)");
            entity.Property(e => e.ErrorNumber).HasColumnType("INT");
            entity.Property(e => e.Remark).HasColumnType("NVARCHAR(200)");
            entity.Property(e => e.ShapeNumber).HasColumnType("INT");
            entity.Property(e => e.StandardScore).HasColumnType("INT");
            entity.Property(e => e.TimeConsume).HasColumnType("INT");
        });

        modelBuilder.Entity<BAnswerT1>(entity =>
        {
            entity.HasKey(e => new { e.AnswerId, e.QuestionId });

            entity.ToTable("b_Answer_T1");

            entity.Property(e => e.AnswerId).HasColumnType("NVARCHAR(25)");
            entity.Property(e => e.QuestionId).HasColumnType("NVARCHAR(6)");
            entity.Property(e => e.ErrorNumber).HasColumnType("INT");
            entity.Property(e => e.Number1).HasColumnType("INT");
            entity.Property(e => e.Number2).HasColumnType("INT");
            entity.Property(e => e.Number3).HasColumnType("INT");
            entity.Property(e => e.Remark).HasColumnType("NVARCHAR(200)");
            entity.Property(e => e.StandardScore).HasColumnType("INT");
        });

        modelBuilder.Entity<BAnswerT1A>(entity =>
        {
            entity.HasKey(e => new { e.AnswerId, e.QuestionId, e.QuestionSort, e.AnswerSort });

            entity.ToTable("b_Answer_T1_A");

            entity.Property(e => e.AnswerId).HasColumnType("NVARCHAR(25)");
            entity.Property(e => e.QuestionId).HasColumnType("NVARCHAR(6)");
            entity.Property(e => e.QuestionSort).HasColumnType("INT");
            entity.Property(e => e.AnswerSort).HasColumnType("NVARCHAR(10)");
        });

        modelBuilder.Entity<BAnswerT2>(entity =>
        {
            entity.HasKey(e => new { e.AnswerId, e.QuestionId });

            entity.ToTable("b_Answer_T2");

            entity.Property(e => e.AnswerId).HasColumnType("NVARCHAR(25)");
            entity.Property(e => e.QuestionId).HasColumnType("NVARCHAR(6)");
            entity.Property(e => e.Number1).HasColumnType("INT");
            entity.Property(e => e.Number2).HasColumnType("INT");
            entity.Property(e => e.Remark).HasColumnType("NVARCHAR(200)");
            entity.Property(e => e.StandardScore).HasColumnType("INT");
        });

        modelBuilder.Entity<BAnswerT2A>(entity =>
        {
            entity.HasKey(e => new { e.AnswerId, e.QuestionId, e.QuestionSort, e.AnswerSort });

            entity.ToTable("b_Answer_T2_A");

            entity.Property(e => e.AnswerId).HasColumnType("NVARCHAR(25)");
            entity.Property(e => e.QuestionId).HasColumnType("NVARCHAR(6)");
            entity.Property(e => e.QuestionSort).HasColumnType("INT");
            entity.Property(e => e.AnswerSort).HasColumnType("NVARCHAR(10)");
        });

        modelBuilder.Entity<BAnswerT3>(entity =>
        {
            entity.HasKey(e => new { e.AnswerId, e.QuestionId });

            entity.ToTable("b_Answer_T3");

            entity.Property(e => e.AnswerId).HasColumnType("NVARCHAR(25)");
            entity.Property(e => e.QuestionId).HasColumnType("NVARCHAR(6)");
            entity.Property(e => e.Level1).HasColumnType("INT");
            entity.Property(e => e.Level2).HasColumnType("INT");
            entity.Property(e => e.Remark).HasColumnType("NVARCHAR(200)");
            entity.Property(e => e.StandardScore).HasColumnType("INT");
        });

        modelBuilder.Entity<BAnswerT3A>(entity =>
        {
            entity.HasKey(e => new { e.AnswerId, e.QuestionId, e.QuestionType, e.QuestionSort, e.Level });

            entity.ToTable("b_Answer_T3_A");

            entity.Property(e => e.AnswerId).HasColumnType("NVARCHAR(25)");
            entity.Property(e => e.QuestionId).HasColumnType("NVARCHAR(6)");
            entity.Property(e => e.QuestionType).HasColumnType("BIT");
            entity.Property(e => e.QuestionSort).HasColumnType("INT");
            entity.Property(e => e.Level).HasColumnType("INT");
            entity.Property(e => e.Value).HasColumnType("NVARCHAR(50)");
        });

        modelBuilder.Entity<BDictionary>(entity =>
        {
            entity.HasKey(e => e.DictionaryId);

            entity.ToTable("b_Dictionary");

            entity.Property(e => e.DictionaryId).HasColumnType("NVARCHAR(50)");
            entity.Property(e => e.DictionaryName).HasColumnType("NVARCHAR(50)");
            entity.Property(e => e.Introduce).HasColumnType("NVARCHAR(500)");
            entity.Property(e => e.Sort).HasColumnType("INT");
            entity.Property(e => e.Status).HasColumnType("NVARCHAR(10)");
        });

        modelBuilder.Entity<BDictionaryItem>(entity =>
        {
            entity.HasKey(e => new { e.DictionaryId, e.ItemId });

            entity.ToTable("b_Dictionary_Item");

            entity.Property(e => e.DictionaryId).HasColumnType("NVARCHAR(50)");
            entity.Property(e => e.ItemId).HasColumnType("NVARCHAR(10)");
            entity.Property(e => e.Field1).HasColumnType("NVARCHAR(50)");
            entity.Property(e => e.Field2).HasColumnType("NVARCHAR(50)");
            entity.Property(e => e.Field3).HasColumnType("NVARCHAR(50)");
            entity.Property(e => e.Introduce).HasColumnType("NVARCHAR(500)");
            entity.Property(e => e.ItemName).HasColumnType("NVARCHAR(50)");
            entity.Property(e => e.ParentItemId).HasColumnType("NVARCHAR(10)");
            entity.Property(e => e.Sort).HasColumnType("INT");
            entity.Property(e => e.Status).HasColumnType("NVARCHAR(10)");
        });

        modelBuilder.Entity<BQuestion>(entity =>
        {
            entity.HasKey(e => e.QuestionId);

            entity.ToTable("b_Question");

            entity.Property(e => e.QuestionId).HasColumnType("NVARCHAR(6)");
            entity.Property(e => e.Instruction).HasColumnType("NVARCHAR(500)");
            entity.Property(e => e.Instruction2).HasColumnType("NVARCHAR(500)");
            entity.Property(e => e.Instruction3).HasColumnType("NVARCHAR(500)");
            entity.Property(e => e.Instruction4).HasColumnType("NVARCHAR(500)");
            entity.Property(e => e.Precautions).HasColumnType("NVARCHAR(200)");
            entity.Property(e => e.QuestionCode).HasColumnType("NVARCHAR(2)");
            entity.Property(e => e.QuestionName).HasColumnType("NVARCHAR(50)");
            entity.Property(e => e.QuestionnaireId).HasColumnType("NVARCHAR(6)");
        });

        modelBuilder.Entity<BQuestionS1>(entity =>
        {
            entity.HasKey(e => new { e.QuestionId, e.GridType, e.GridSort });

            entity.ToTable("b_Question_S1");

            entity.Property(e => e.QuestionId).HasColumnType("NVARCHAR(6)");
            entity.Property(e => e.GridType).HasColumnType("NVARCHAR(10)");
            entity.Property(e => e.GridSort).HasColumnType("INT");
            entity.Property(e => e.GridValue).HasColumnType("INT");
        });

        modelBuilder.Entity<BQuestionS2>(entity =>
        {
            entity.HasKey(e => new { e.QuestionId, e.GridRow, e.GridColumn });

            entity.ToTable("b_Question_S2");

            entity.Property(e => e.QuestionId).HasColumnType("NVARCHAR(6)");
            entity.Property(e => e.GridRow).HasColumnType("INT");
            entity.Property(e => e.GridColumn).HasColumnType("INT");
            entity.Property(e => e.GridValue).HasColumnType("INT");
            entity.Property(e => e.IsTrue).HasColumnType("BIT");
        });

        modelBuilder.Entity<BQuestionS3>(entity =>
        {
            entity.HasKey(e => new { e.QuestionId, e.GridRow, e.GridColumn });

            entity.ToTable("b_Question_S3");

            entity.Property(e => e.QuestionId).HasColumnType("NVARCHAR(6)");
            entity.Property(e => e.GridRow).HasColumnType("INT");
            entity.Property(e => e.GridColumn).HasColumnType("INT");
            entity.Property(e => e.GridValue).HasColumnType("INT");
        });

        modelBuilder.Entity<BQuestionS4>(entity =>
        {
            entity.HasKey(e => e.QuestionId);

            entity.ToTable("b_Question_S4");

            entity.Property(e => e.QuestionId).HasColumnType("NVARCHAR(6)");
        });

        modelBuilder.Entity<BQuestionS5>(entity =>
        {
            entity.HasKey(e => new { e.QuestionId, e.ImageId });

            entity.ToTable("b_Question_S5");

            entity.Property(e => e.QuestionId).HasColumnType("NVARCHAR(6)");
            entity.Property(e => e.ImageId).HasColumnType("NVARCHAR(25)");
        });

        modelBuilder.Entity<BQuestionT1>(entity =>
        {
            entity.HasKey(e => e.QuestionId);

            entity.ToTable("b_Question_T1");

            entity.Property(e => e.QuestionId).HasColumnType("NVARCHAR(6)");
            entity.Property(e => e.Number1).HasColumnType("INT");
            entity.Property(e => e.Number2).HasColumnType("INT");
            entity.Property(e => e.Number3).HasColumnType("INT");
            entity.Property(e => e.NumberQuestion).HasColumnType("NVARCHAR(500)");
            entity.Property(e => e.StoryQuestion).HasColumnType("NVARCHAR(500)");
        });

        modelBuilder.Entity<BQuestionT1A>(entity =>
        {
            entity.HasKey(e => new { e.QuestionId, e.QuestionSort, e.AnswerSort });

            entity.ToTable("b_Question_T1_A");

            entity.Property(e => e.QuestionId).HasColumnType("NVARCHAR(6)");
            entity.Property(e => e.QuestionSort).HasColumnType("INT");
            entity.Property(e => e.AnswerSort).HasColumnType("NVARCHAR(10)");
            entity.Property(e => e.Answer).HasColumnType("NVARCHAR(50)");
            entity.Property(e => e.IsTrue).HasColumnType("BIT");
        });

        modelBuilder.Entity<BQuestionT1Q>(entity =>
        {
            entity.HasKey(e => new { e.QuestionId, e.QuestionSort });

            entity.ToTable("b_Question_T1_Q");

            entity.Property(e => e.QuestionId).HasColumnType("NVARCHAR(6)");
            entity.Property(e => e.QuestionSort).HasColumnType("INT");
            entity.Property(e => e.QuestionQ)
                .HasColumnType("NVARCHAR(50)")
                .HasColumnName("Question_Q");
            entity.Property(e => e.QuestionType).HasColumnType("NVARCHAR(10)");
        });

        modelBuilder.Entity<BQuestionT2>(entity =>
        {
            entity.HasKey(e => e.QuestionId);

            entity.ToTable("b_Question_T2");

            entity.Property(e => e.QuestionId).HasColumnType("NVARCHAR(6)");
            entity.Property(e => e.Number1).HasColumnType("INT");
            entity.Property(e => e.Number2).HasColumnType("INT");
            entity.Property(e => e.Question).HasColumnType("NVARCHAR(500)");
        });

        modelBuilder.Entity<BQuestionT2A>(entity =>
        {
            entity.HasKey(e => new { e.QuestionId, e.QuestionSort, e.AnswerSort });

            entity.ToTable("b_Question_T2_A");

            entity.Property(e => e.QuestionId).HasColumnType("NVARCHAR(6)");
            entity.Property(e => e.QuestionSort).HasColumnType("INT");
            entity.Property(e => e.AnswerSort).HasColumnType("NVARCHAR(10)");
            entity.Property(e => e.Answer).HasColumnType("NVARCHAR(50)");
            entity.Property(e => e.IsTrue).HasColumnType("BIT");
        });

        modelBuilder.Entity<BQuestionT2Q>(entity =>
        {
            entity.HasKey(e => new { e.QuestionId, e.QuestionSort });

            entity.ToTable("b_Question_T2_Q");

            entity.Property(e => e.QuestionId).HasColumnType("NVARCHAR(6)");
            entity.Property(e => e.QuestionSort).HasColumnType("INT");
            entity.Property(e => e.QuestionQ1)
                .HasColumnType("NVARCHAR(50)")
                .HasColumnName("Question_Q1");
            entity.Property(e => e.QuestionQ2)
                .HasColumnType("NVARCHAR(50)")
                .HasColumnName("Question_Q2");
        });

        modelBuilder.Entity<BQuestionT3>(entity =>
        {
            entity.HasKey(e => new { e.QuestionId, e.QuestionType, e.QuestionSort, e.Level });

            entity.ToTable("b_Question_T3");

            entity.Property(e => e.QuestionId).HasColumnType("NVARCHAR(6)");
            entity.Property(e => e.QuestionType).HasColumnType("BIT");
            entity.Property(e => e.QuestionSort).HasColumnType("INT");
            entity.Property(e => e.Level).HasColumnType("INT");
            entity.Property(e => e.QuestionA)
                .HasColumnType("NVARCHAR(50)")
                .HasColumnName("Question_A");
            entity.Property(e => e.QuestionQ)
                .HasColumnType("NVARCHAR(50)")
                .HasColumnName("Question_Q");
        });

        modelBuilder.Entity<BQuestionnaire>(entity =>
        {
            entity.HasKey(e => e.QuestionnaireId);

            entity.ToTable("b_Questionnaire");

            entity.Property(e => e.QuestionnaireId).HasColumnType("NVARCHAR(6)");
            entity.Property(e => e.QuestionnaireName).HasColumnType("NVARCHAR(50)");
            entity.Property(e => e.Remark).HasColumnType("NVARCHAR(200)");
            entity.Property(e => e.VersionName).HasColumnType("NVARCHAR(50)");
        });

        modelBuilder.Entity<BUser>(entity =>
        {
            entity.HasKey(e => e.UserId);

            entity.ToTable("b_User");

            entity.Property(e => e.UserId).HasColumnType("NVARCHAR(32)");
            entity.Property(e => e.Account).HasColumnType("NVARCHAR(50)");
            entity.Property(e => e.Age).HasColumnType("INT");
            entity.Property(e => e.Mobile).HasColumnType("NVARCHAR(11)");
            entity.Property(e => e.Password).HasColumnType("NVARCHAR(64)");
            entity.Property(e => e.Role).HasColumnType("NVARCHAR(10)");
            entity.Property(e => e.Sex).HasColumnType("NVARCHAR(10)");
            entity.Property(e => e.UserName).HasColumnType("NVARCHAR(50)");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
