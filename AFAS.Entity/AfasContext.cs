using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace AFAS.Entity;

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

    public virtual DbSet<BEvaluationStandard> BEvaluationStandards { get; set; }

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

    public virtual DbSet<BSpiralMaze> BSpiralMazes { get; set; }

    public virtual DbSet<BUser> BUsers { get; set; }

    public virtual DbSet<BUserToken> BUserTokens { get; set; }

    public virtual DbSet<LogApi> LogApis { get; set; }

    public virtual DbSet<LogDebug> LogDebugs { get; set; }

    public virtual DbSet<LogException> LogExceptions { get; set; }

    public virtual DbSet<LogToken> LogTokens { get; set; }

    public virtual DbSet<LogUserLogin> LogUserLogins { get; set; }

    public virtual DbSet<SPara> SParas { get; set; }

    public virtual DbSet<SService> SServices { get; set; }

    public virtual DbSet<SSystem> SSystems { get; set; }

    public virtual DbSet<STerminal> STerminals { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlite("Data Source=../AFAS.Database/AFAS.db;Cache=Shared;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<BAnswer>(entity =>
        {
            entity.HasKey(e => new { e.AnswerId, e.QuestionnaireId });

            entity.ToTable("b_Answer");

            entity.Property(e => e.AnswerId)
                .HasDefaultValue("")
                .HasColumnType("NVARCHAR(25)");
            entity.Property(e => e.QuestionnaireId)
                .HasDefaultValue("")
                .HasColumnType("NVARCHAR(6)");
            entity.Property(e => e.Advantage)
                .HasDefaultValue("")
                .HasColumnType("NVARCHAR(50)");
            entity.Property(e => e.LevelCode)
                .HasDefaultValue("")
                .HasColumnType("NVARCHAR(10)");
            entity.Property(e => e.QuestionnaireDate)
                .HasDefaultValueSql("''")
                .HasColumnType("NVARCHAR(10)");
            entity.Property(e => e.RadarImage).HasDefaultValue("");
            entity.Property(e => e.Remark)
                .HasDefaultValue("")
                .HasColumnType("NVARCHAR(50)");
            entity.Property(e => e.Simage)
                .HasDefaultValue("")
                .HasColumnName("SImage");
            entity.Property(e => e.Sresult)
                .HasDefaultValue("")
                .HasColumnType("NVARCHAR(500)")
                .HasColumnName("SResult");
            entity.Property(e => e.Status)
                .HasDefaultValue("")
                .HasColumnType("NVARCHAR(10)");
            entity.Property(e => e.SuggestedCourse)
                .HasDefaultValue("")
                .HasColumnType("NVARCHAR(50)");
            entity.Property(e => e.TeacherId)
                .HasDefaultValue("")
                .HasColumnType("NVARCHAR(32)");
            entity.Property(e => e.Timage)
                .HasDefaultValue("")
                .HasColumnName("TImage");
            entity.Property(e => e.Tresult)
                .HasDefaultValue("")
                .HasColumnType("NVARCHAR(500)")
                .HasColumnName("TResult");
            entity.Property(e => e.UserId)
                .HasDefaultValue("")
                .HasColumnType("NVARCHAR(32)");
            entity.Property(e => e.Weak)
                .HasDefaultValue("")
                .HasColumnType("NVARCHAR(50)");
        });

        modelBuilder.Entity<BAnswerS1>(entity =>
        {
            entity.HasKey(e => new { e.AnswerId, e.QuestionId });

            entity.ToTable("b_Answer_S1");

            entity.Property(e => e.AnswerId)
                .HasDefaultValue("")
                .HasColumnType("NVARCHAR(25)");
            entity.Property(e => e.QuestionId)
                .HasDefaultValue("")
                .HasColumnType("NVARCHAR(6)");
            entity.Property(e => e.OriginScore).HasColumnType("INT");
            entity.Property(e => e.Remark)
                .HasDefaultValue("")
                .HasColumnType("NVARCHAR(200)");
            entity.Property(e => e.StandardScore).HasColumnType("INT");
        });

        modelBuilder.Entity<BAnswerS1A>(entity =>
        {
            entity.HasKey(e => new { e.AnswerId, e.QuestionId, e.GridType });

            entity.ToTable("b_Answer_S1_A");

            entity.Property(e => e.AnswerId)
                .HasDefaultValue("")
                .HasColumnType("NVARCHAR(25)");
            entity.Property(e => e.QuestionId)
                .HasDefaultValue("")
                .HasColumnType("NVARCHAR(6)");
            entity.Property(e => e.GridType)
                .HasDefaultValue("")
                .HasColumnType("NVARCHAR(10)");
            entity.Property(e => e.TimeConsume).HasColumnType("INT");
        });

        modelBuilder.Entity<BAnswerS2>(entity =>
        {
            entity.HasKey(e => new { e.AnswerId, e.QuestionId });

            entity.ToTable("b_Answer_S2");

            entity.Property(e => e.AnswerId)
                .HasDefaultValue("")
                .HasColumnType("NVARCHAR(25)");
            entity.Property(e => e.QuestionId)
                .HasDefaultValue("")
                .HasColumnType("NVARCHAR(6)");
            entity.Property(e => e.ErrorNumber).HasColumnType("INT");
            entity.Property(e => e.ErrorRate).HasColumnType("DECIMAL(6,2)");
            entity.Property(e => e.MarkNumber).HasColumnType("INT");
            entity.Property(e => e.Remark)
                .HasDefaultValue("")
                .HasColumnType("NVARCHAR(200)");
            entity.Property(e => e.StandardScore).HasColumnType("INT");
            entity.Property(e => e.TimeConsume).HasColumnType("INT");
        });

        modelBuilder.Entity<BAnswerS2A>(entity =>
        {
            entity.HasKey(e => new { e.AnswerId, e.QuestionId, e.GridRow, e.GridColumn });

            entity.ToTable("b_Answer_S2_A");

            entity.Property(e => e.AnswerId)
                .HasDefaultValue("")
                .HasColumnType("NVARCHAR(25)");
            entity.Property(e => e.QuestionId)
                .HasDefaultValue("")
                .HasColumnType("NVARCHAR(6)");
            entity.Property(e => e.GridRow).HasColumnType("INT");
            entity.Property(e => e.GridColumn).HasColumnType("INT");
            entity.Property(e => e.Selected).HasColumnType("BIT");
        });

        modelBuilder.Entity<BAnswerS3>(entity =>
        {
            entity.HasKey(e => new { e.AnswerId, e.QuestionId });

            entity.ToTable("b_Answer_S3");

            entity.Property(e => e.AnswerId)
                .HasDefaultValue("")
                .HasColumnType("NVARCHAR(25)");
            entity.Property(e => e.QuestionId)
                .HasDefaultValue("")
                .HasColumnType("NVARCHAR(6)");
            entity.Property(e => e.ErrorNumber).HasColumnType("INT");
            entity.Property(e => e.Remark)
                .HasDefaultValue("")
                .HasColumnType("NVARCHAR(200)");
            entity.Property(e => e.RightNumber).HasColumnType("INT");
            entity.Property(e => e.StandardScore).HasColumnType("INT");
            entity.Property(e => e.TimeConsume).HasColumnType("INT");
        });

        modelBuilder.Entity<BAnswerS3A>(entity =>
        {
            entity.HasKey(e => new { e.AnswerId, e.QuestionId, e.GridRow, e.GridColumn });

            entity.ToTable("b_Answer_S3_A");

            entity.Property(e => e.AnswerId)
                .HasDefaultValue("")
                .HasColumnType("NVARCHAR(25)");
            entity.Property(e => e.QuestionId)
                .HasDefaultValue("")
                .HasColumnType("NVARCHAR(6)");
            entity.Property(e => e.GridRow).HasColumnType("INT");
            entity.Property(e => e.GridColumn).HasColumnType("INT");
            entity.Property(e => e.Value).HasColumnType("INT");
        });

        modelBuilder.Entity<BAnswerS4>(entity =>
        {
            entity.HasKey(e => new { e.AnswerId, e.QuestionId });

            entity.ToTable("b_Answer_S4");

            entity.Property(e => e.AnswerId)
                .HasDefaultValue("")
                .HasColumnType("NVARCHAR(25)");
            entity.Property(e => e.QuestionId)
                .HasDefaultValue("")
                .HasColumnType("NVARCHAR(6)");
            entity.Property(e => e.AnswerImage).HasDefaultValue("");
            entity.Property(e => e.CrossNumber).HasColumnType("INT");
            entity.Property(e => e.QuestionImage).HasDefaultValue("");
            entity.Property(e => e.Remark)
                .HasDefaultValue("")
                .HasColumnType("NVARCHAR(200)");
            entity.Property(e => e.StandardScore).HasColumnType("INT");
            entity.Property(e => e.TimeConsume).HasColumnType("INT");
        });

        modelBuilder.Entity<BAnswerS5>(entity =>
        {
            entity.HasKey(e => new { e.AnswerId, e.QuestionId });

            entity.ToTable("b_Answer_S5");

            entity.Property(e => e.AnswerId)
                .HasDefaultValue("")
                .HasColumnType("NVARCHAR(25)");
            entity.Property(e => e.QuestionId)
                .HasDefaultValue("")
                .HasColumnType("NVARCHAR(6)");
            entity.Property(e => e.AnswerImage).HasDefaultValue("");
            entity.Property(e => e.ErrorNumber).HasColumnType("INT");
            entity.Property(e => e.QuestionImage).HasDefaultValue("");
            entity.Property(e => e.Remark)
                .HasDefaultValue("")
                .HasColumnType("NVARCHAR(200)");
            entity.Property(e => e.ShapeNumber).HasColumnType("INT");
            entity.Property(e => e.StandardScore).HasColumnType("INT");
            entity.Property(e => e.TimeConsume).HasColumnType("INT");
        });

        modelBuilder.Entity<BAnswerT1>(entity =>
        {
            entity.HasKey(e => new { e.AnswerId, e.QuestionId });

            entity.ToTable("b_Answer_T1");

            entity.Property(e => e.AnswerId)
                .HasDefaultValue("")
                .HasColumnType("NVARCHAR(25)");
            entity.Property(e => e.QuestionId)
                .HasDefaultValue("")
                .HasColumnType("NVARCHAR(6)");
            entity.Property(e => e.ErrorNumber).HasColumnType("INT");
            entity.Property(e => e.Number1).HasColumnType("INT");
            entity.Property(e => e.Number2).HasColumnType("INT");
            entity.Property(e => e.Number3).HasColumnType("INT");
            entity.Property(e => e.Remark)
                .HasDefaultValue("")
                .HasColumnType("NVARCHAR(200)");
            entity.Property(e => e.StandardScore).HasColumnType("INT");
        });

        modelBuilder.Entity<BAnswerT1A>(entity =>
        {
            entity.HasKey(e => new { e.AnswerId, e.QuestionId, e.QuestionSort, e.AnswerSort });

            entity.ToTable("b_Answer_T1_A");

            entity.Property(e => e.AnswerId)
                .HasDefaultValue("")
                .HasColumnType("NVARCHAR(25)");
            entity.Property(e => e.QuestionId)
                .HasDefaultValue("")
                .HasColumnType("NVARCHAR(6)");
            entity.Property(e => e.QuestionSort).HasColumnType("INT");
            entity.Property(e => e.AnswerSort)
                .HasDefaultValue("")
                .HasColumnType("NVARCHAR(10)");
        });

        modelBuilder.Entity<BAnswerT2>(entity =>
        {
            entity.HasKey(e => new { e.AnswerId, e.QuestionId });

            entity.ToTable("b_Answer_T2");

            entity.Property(e => e.AnswerId)
                .HasDefaultValue("")
                .HasColumnType("NVARCHAR(25)");
            entity.Property(e => e.QuestionId)
                .HasDefaultValue("")
                .HasColumnType("NVARCHAR(6)");
            entity.Property(e => e.Number1).HasColumnType("INT");
            entity.Property(e => e.Number2).HasColumnType("INT");
            entity.Property(e => e.Remark)
                .HasDefaultValue("")
                .HasColumnType("NVARCHAR(200)");
            entity.Property(e => e.StandardScore).HasColumnType("INT");
        });

        modelBuilder.Entity<BAnswerT2A>(entity =>
        {
            entity.HasKey(e => new { e.AnswerId, e.QuestionId, e.QuestionSort, e.AnswerSort });

            entity.ToTable("b_Answer_T2_A");

            entity.Property(e => e.AnswerId)
                .HasDefaultValue("")
                .HasColumnType("NVARCHAR(25)");
            entity.Property(e => e.QuestionId)
                .HasDefaultValue("")
                .HasColumnType("NVARCHAR(6)");
            entity.Property(e => e.QuestionSort).HasColumnType("INT");
            entity.Property(e => e.AnswerSort)
                .HasDefaultValue("")
                .HasColumnType("NVARCHAR(10)");
        });

        modelBuilder.Entity<BAnswerT3>(entity =>
        {
            entity.HasKey(e => new { e.AnswerId, e.QuestionId });

            entity.ToTable("b_Answer_T3");

            entity.Property(e => e.AnswerId)
                .HasDefaultValue("")
                .HasColumnType("NVARCHAR(25)");
            entity.Property(e => e.QuestionId)
                .HasDefaultValue("")
                .HasColumnType("NVARCHAR(6)");
            entity.Property(e => e.Level1).HasColumnType("INT");
            entity.Property(e => e.Level2).HasColumnType("INT");
            entity.Property(e => e.Remark)
                .HasDefaultValue("")
                .HasColumnType("NVARCHAR(200)");
            entity.Property(e => e.StandardScore).HasColumnType("INT");
        });

        modelBuilder.Entity<BAnswerT3A>(entity =>
        {
            entity.HasKey(e => new { e.AnswerId, e.QuestionId, e.QuestionType, e.QuestionSort, e.Level });

            entity.ToTable("b_Answer_T3_A");

            entity.Property(e => e.AnswerId)
                .HasDefaultValue("")
                .HasColumnType("NVARCHAR(25)");
            entity.Property(e => e.QuestionId)
                .HasDefaultValue("")
                .HasColumnType("NVARCHAR(6)");
            entity.Property(e => e.QuestionType).HasColumnType("BIT");
            entity.Property(e => e.QuestionSort).HasColumnType("INT");
            entity.Property(e => e.Level).HasColumnType("INT");
            entity.Property(e => e.Value)
                .HasDefaultValue("")
                .HasColumnType("NVARCHAR(50)");
        });

        modelBuilder.Entity<BDictionary>(entity =>
        {
            entity.HasKey(e => e.DictionaryId);

            entity.ToTable("b_Dictionary");

            entity.Property(e => e.DictionaryId)
                .HasDefaultValue("")
                .HasColumnType("NVARCHAR(50)");
            entity.Property(e => e.DictionaryName)
                .HasDefaultValue("")
                .HasColumnType("NVARCHAR(50)");
            entity.Property(e => e.Introduce)
                .HasDefaultValue("")
                .HasColumnType("NVARCHAR(500)");
            entity.Property(e => e.Sort).HasColumnType("INT");
            entity.Property(e => e.Status)
                .HasDefaultValue("")
                .HasColumnType("NVARCHAR(10)");
        });

        modelBuilder.Entity<BDictionaryItem>(entity =>
        {
            entity.HasKey(e => new { e.DictionaryId, e.ItemId });

            entity.ToTable("b_Dictionary_Item");

            entity.Property(e => e.DictionaryId)
                .HasDefaultValue("")
                .HasColumnType("NVARCHAR(50)");
            entity.Property(e => e.ItemId)
                .HasDefaultValue("")
                .HasColumnType("NVARCHAR(10)");
            entity.Property(e => e.Field1)
                .HasDefaultValue("")
                .HasColumnType("NVARCHAR(50)");
            entity.Property(e => e.Field2)
                .HasDefaultValue("")
                .HasColumnType("NVARCHAR(50)");
            entity.Property(e => e.Field3)
                .HasDefaultValue("")
                .HasColumnType("NVARCHAR(50)");
            entity.Property(e => e.Introduce)
                .HasDefaultValue("")
                .HasColumnType("NVARCHAR(500)");
            entity.Property(e => e.ItemName)
                .HasDefaultValue("")
                .HasColumnType("NVARCHAR(50)");
            entity.Property(e => e.ParentItemId)
                .HasDefaultValue("")
                .HasColumnType("NVARCHAR(10)");
            entity.Property(e => e.Sort).HasColumnType("INT");
            entity.Property(e => e.Status)
                .HasDefaultValue("")
                .HasColumnType("NVARCHAR(10)");
        });

        modelBuilder.Entity<BEvaluationStandard>(entity =>
        {
            entity.HasKey(e => e.LevelCode);

            entity.ToTable("b_Evaluation_Standard");

            entity.Property(e => e.LevelCode)
                .HasDefaultValue("")
                .HasColumnType("NVARCHAR(10)");
            entity.Property(e => e.LevelName)
                .HasDefaultValue("")
                .HasColumnType("NVARCHAR(50)");
            entity.Property(e => e.S1).HasColumnType("INT");
            entity.Property(e => e.S2).HasColumnType("INT");
            entity.Property(e => e.S3).HasColumnType("INT");
            entity.Property(e => e.S4).HasColumnType("INT");
            entity.Property(e => e.S5).HasColumnType("INT");
            entity.Property(e => e.T1).HasColumnType("INT");
            entity.Property(e => e.T2).HasColumnType("INT");
            entity.Property(e => e.T3).HasColumnType("INT");
        });

        modelBuilder.Entity<BQuestion>(entity =>
        {
            entity.HasKey(e => e.QuestionId);

            entity.ToTable("b_Question");

            entity.Property(e => e.QuestionId)
                .HasDefaultValue("")
                .HasColumnType("NVARCHAR(6)");
            entity.Property(e => e.Instruction)
                .HasDefaultValue("")
                .HasColumnType("NVARCHAR(500)");
            entity.Property(e => e.Instruction2)
                .HasDefaultValue("")
                .HasColumnType("NVARCHAR(500)");
            entity.Property(e => e.Instruction3)
                .HasDefaultValue("")
                .HasColumnType("NVARCHAR(500)");
            entity.Property(e => e.Instruction4)
                .HasDefaultValue("")
                .HasColumnType("NVARCHAR(500)");
            entity.Property(e => e.Precautions)
                .HasDefaultValue("")
                .HasColumnType("NVARCHAR(200)");
            entity.Property(e => e.QuestionCode)
                .HasDefaultValue("")
                .HasColumnType("NVARCHAR(10)");
            entity.Property(e => e.QuestionName)
                .HasDefaultValue("")
                .HasColumnType("NVARCHAR(50)");
            entity.Property(e => e.QuestionnaireId)
                .HasDefaultValue("")
                .HasColumnType("NVARCHAR(6)");
        });

        modelBuilder.Entity<BQuestionS1>(entity =>
        {
            entity.HasKey(e => new { e.QuestionId, e.GridType, e.GridSort });

            entity.ToTable("b_Question_S1");

            entity.Property(e => e.QuestionId)
                .HasDefaultValue("")
                .HasColumnType("NVARCHAR(6)");
            entity.Property(e => e.GridType)
                .HasDefaultValue("")
                .HasColumnType("NVARCHAR(10)");
            entity.Property(e => e.GridSort).HasColumnType("INT");
            entity.Property(e => e.GridValue).HasColumnType("INT");
        });

        modelBuilder.Entity<BQuestionS2>(entity =>
        {
            entity.HasKey(e => new { e.QuestionId, e.GridRow, e.GridColumn });

            entity.ToTable("b_Question_S2");

            entity.Property(e => e.QuestionId)
                .HasDefaultValue("")
                .HasColumnType("NVARCHAR(6)");
            entity.Property(e => e.GridRow).HasColumnType("INT");
            entity.Property(e => e.GridColumn).HasColumnType("INT");
            entity.Property(e => e.GridValue).HasColumnType("INT");
            entity.Property(e => e.IsTrue).HasColumnType("BIT");
        });

        modelBuilder.Entity<BQuestionS3>(entity =>
        {
            entity.HasKey(e => new { e.QuestionId, e.GridRow, e.GridColumn });

            entity.ToTable("b_Question_S3");

            entity.Property(e => e.QuestionId)
                .HasDefaultValue("")
                .HasColumnType("NVARCHAR(6)");
            entity.Property(e => e.GridRow).HasColumnType("INT");
            entity.Property(e => e.GridColumn).HasColumnType("INT");
            entity.Property(e => e.GridValue).HasColumnType("INT");
        });

        modelBuilder.Entity<BQuestionS4>(entity =>
        {
            entity.HasKey(e => e.QuestionId);

            entity.ToTable("b_Question_S4");

            entity.Property(e => e.QuestionId)
                .HasDefaultValue("")
                .HasColumnType("NVARCHAR(6)");
        });

        modelBuilder.Entity<BQuestionS5>(entity =>
        {
            entity.HasKey(e => new { e.QuestionId, e.ImageId });

            entity.ToTable("b_Question_S5");

            entity.Property(e => e.QuestionId)
                .HasDefaultValue("")
                .HasColumnType("NVARCHAR(6)");
            entity.Property(e => e.ImageId)
                .HasDefaultValue("")
                .HasColumnType("NVARCHAR(25)");
            entity.Property(e => e.Image).HasDefaultValue("");
        });

        modelBuilder.Entity<BQuestionT1>(entity =>
        {
            entity.HasKey(e => e.QuestionId);

            entity.ToTable("b_Question_T1");

            entity.Property(e => e.QuestionId)
                .HasDefaultValue("")
                .HasColumnType("NVARCHAR(6)");
            entity.Property(e => e.Number1).HasColumnType("INT");
            entity.Property(e => e.Number2).HasColumnType("INT");
            entity.Property(e => e.Number3).HasColumnType("INT");
            entity.Property(e => e.NumberQuestion)
                .HasDefaultValue("")
                .HasColumnType("NVARCHAR(500)");
            entity.Property(e => e.StoryQuestion)
                .HasDefaultValue("")
                .HasColumnType("NVARCHAR(500)");
        });

        modelBuilder.Entity<BQuestionT1A>(entity =>
        {
            entity.HasKey(e => new { e.QuestionId, e.QuestionSort, e.AnswerSort });

            entity.ToTable("b_Question_T1_A");

            entity.Property(e => e.QuestionId)
                .HasDefaultValue("")
                .HasColumnType("NVARCHAR(6)");
            entity.Property(e => e.QuestionSort).HasColumnType("INT");
            entity.Property(e => e.AnswerSort)
                .HasDefaultValue("")
                .HasColumnType("NVARCHAR(10)");
            entity.Property(e => e.Answer)
                .HasDefaultValue("")
                .HasColumnType("NVARCHAR(50)");
            entity.Property(e => e.IsTrue).HasColumnType("BIT");
        });

        modelBuilder.Entity<BQuestionT1Q>(entity =>
        {
            entity.HasKey(e => new { e.QuestionId, e.QuestionSort });

            entity.ToTable("b_Question_T1_Q");

            entity.Property(e => e.QuestionId)
                .HasDefaultValue("")
                .HasColumnType("NVARCHAR(6)");
            entity.Property(e => e.QuestionSort).HasColumnType("INT");
            entity.Property(e => e.QuestionQ)
                .HasDefaultValue("")
                .HasColumnType("NVARCHAR(50)")
                .HasColumnName("Question_Q");
            entity.Property(e => e.QuestionType)
                .HasDefaultValue("")
                .HasColumnType("NVARCHAR(10)");
        });

        modelBuilder.Entity<BQuestionT2>(entity =>
        {
            entity.HasKey(e => e.QuestionId);

            entity.ToTable("b_Question_T2");

            entity.Property(e => e.QuestionId)
                .HasDefaultValue("")
                .HasColumnType("NVARCHAR(6)");
            entity.Property(e => e.Number1).HasColumnType("INT");
            entity.Property(e => e.Number2).HasColumnType("INT");
            entity.Property(e => e.Question)
                .HasDefaultValue("")
                .HasColumnType("NVARCHAR(500)");
        });

        modelBuilder.Entity<BQuestionT2A>(entity =>
        {
            entity.HasKey(e => new { e.QuestionId, e.QuestionSort, e.AnswerSort });

            entity.ToTable("b_Question_T2_A");

            entity.Property(e => e.QuestionId)
                .HasDefaultValue("")
                .HasColumnType("NVARCHAR(6)");
            entity.Property(e => e.QuestionSort).HasColumnType("INT");
            entity.Property(e => e.AnswerSort)
                .HasDefaultValue("")
                .HasColumnType("NVARCHAR(10)");
            entity.Property(e => e.Answer)
                .HasDefaultValue("")
                .HasColumnType("NVARCHAR(50)");
            entity.Property(e => e.IsTrue).HasColumnType("BIT");
        });

        modelBuilder.Entity<BQuestionT2Q>(entity =>
        {
            entity.HasKey(e => new { e.QuestionId, e.QuestionSort });

            entity.ToTable("b_Question_T2_Q");

            entity.Property(e => e.QuestionId)
                .HasDefaultValue("")
                .HasColumnType("NVARCHAR(6)");
            entity.Property(e => e.QuestionSort).HasColumnType("INT");
            entity.Property(e => e.QuestionQ1)
                .HasDefaultValue("")
                .HasColumnType("NVARCHAR(50)")
                .HasColumnName("Question_Q1");
            entity.Property(e => e.QuestionQ2)
                .HasDefaultValue("")
                .HasColumnType("NVARCHAR(50)")
                .HasColumnName("Question_Q2");
        });

        modelBuilder.Entity<BQuestionT3>(entity =>
        {
            entity.HasKey(e => new { e.QuestionId, e.QuestionType, e.QuestionSort, e.Level });

            entity.ToTable("b_Question_T3");

            entity.Property(e => e.QuestionId)
                .HasDefaultValue("")
                .HasColumnType("NVARCHAR(6)");
            entity.Property(e => e.QuestionType).HasColumnType("BIT");
            entity.Property(e => e.QuestionSort).HasColumnType("INT");
            entity.Property(e => e.Level).HasColumnType("INT");
            entity.Property(e => e.QuestionA)
                .HasDefaultValue("")
                .HasColumnType("NVARCHAR(50)")
                .HasColumnName("Question_A");
            entity.Property(e => e.QuestionQ)
                .HasDefaultValue("")
                .HasColumnType("NVARCHAR(50)")
                .HasColumnName("Question_Q");
        });

        modelBuilder.Entity<BQuestionnaire>(entity =>
        {
            entity.HasKey(e => e.QuestionnaireId);

            entity.ToTable("b_Questionnaire");

            entity.Property(e => e.QuestionnaireId)
                .HasDefaultValue("")
                .HasColumnType("NVARCHAR(6)");
            entity.Property(e => e.QuestionnaireName)
                .HasDefaultValue("")
                .HasColumnType("NVARCHAR(50)");
            entity.Property(e => e.Remark)
                .HasDefaultValue("")
                .HasColumnType("NVARCHAR(200)");
            entity.Property(e => e.VersionName)
                .HasDefaultValue("")
                .HasColumnType("NVARCHAR(50)");
        });

        modelBuilder.Entity<BSpiralMaze>(entity =>
        {
            entity.HasKey(e => e.Age);

            entity.ToTable("b_Spiral_Maze");

            entity.Property(e => e.Age)
                .ValueGeneratedNever()
                .HasColumnType("INT");
            entity.Property(e => e.Perturbation).HasColumnType("INT");
            entity.Property(e => e.RingNumber).HasColumnType("INT");
            entity.Property(e => e.Spacing).HasColumnType("INT");
        });

        modelBuilder.Entity<BUser>(entity =>
        {
            entity.HasKey(e => e.UserId);

            entity.ToTable("b_User");

            entity.Property(e => e.UserId)
                .HasDefaultValue("")
                .HasColumnType("NVARCHAR(32)");
            entity.Property(e => e.Account)
                .HasDefaultValue("")
                .HasColumnType("NVARCHAR(50)");
            entity.Property(e => e.Age).HasColumnType("INT");
            entity.Property(e => e.AvatarUrl).HasDefaultValue("");
            entity.Property(e => e.Gender)
                .HasDefaultValue("")
                .HasColumnType("NVARCHAR(10)");
            entity.Property(e => e.IsDeveloper).HasColumnType("BIT");
            entity.Property(e => e.Mobile)
                .HasDefaultValue("")
                .HasColumnType("NVARCHAR(11)");
            entity.Property(e => e.NickName)
                .HasDefaultValue("")
                .HasColumnType("NVARCHAR(50)");
            entity.Property(e => e.Password)
                .HasDefaultValue("")
                .HasColumnType("NVARCHAR(64)");
            entity.Property(e => e.Role)
                .HasDefaultValue("")
                .HasColumnType("NVARCHAR(10)");
            entity.Property(e => e.UserName)
                .HasDefaultValue("")
                .HasColumnType("NVARCHAR(50)");
        });

        modelBuilder.Entity<BUserToken>(entity =>
        {
            entity.HasKey(e => e.UserId);

            entity.ToTable("b_User_Token");

            entity.Property(e => e.UserId)
                .HasDefaultValue("")
                .HasColumnType("NVARCHAR(32)");
            entity.Property(e => e.CreateStamp)
                .HasDefaultValueSql("''")
                .HasColumnType("NVARCHAR(20)");
            entity.Property(e => e.LoginExpires).HasColumnType("INT");
            entity.Property(e => e.TokenData).HasDefaultValue("");
        });

        modelBuilder.Entity<LogApi>(entity =>
        {
            entity.HasKey(e => e.LogId);

            entity.ToTable("log_API");

            entity.Property(e => e.AbsoluteUri)
                .HasDefaultValue("")
                .HasColumnType("NVARCHAR(500)");
            entity.Property(e => e.AuthType)
                .HasDefaultValue("")
                .HasColumnType("NVARCHAR(20)");
            entity.Property(e => e.Exception).HasDefaultValue("");
            entity.Property(e => e.IpAddress)
                .HasDefaultValue("")
                .HasColumnType("NVARCHAR(20)");
            entity.Property(e => e.IsSuccess).HasColumnType("BIT");
            entity.Property(e => e.PhysicalPath)
                .HasDefaultValue("")
                .HasColumnType("NVARCHAR(500)");
            entity.Property(e => e.SiteUrl)
                .HasDefaultValue("")
                .HasColumnType("NVARCHAR(200)");
            entity.Property(e => e.TerminalId)
                .HasDefaultValue("")
                .HasColumnType("NVARCHAR(50)");
            entity.Property(e => e.TerminalSpan).HasColumnType("BIGINT");
            entity.Property(e => e.TimeStamp)
                .HasDefaultValueSql("''")
                .HasColumnType("NVARCHAR(20)");
            entity.Property(e => e.Token)
                .HasDefaultValue("")
                .HasColumnType("NVARCHAR(32)");
            entity.Property(e => e.TokenSpan).HasColumnType("BIGINT");
            entity.Property(e => e.UserAgent)
                .HasDefaultValue("")
                .HasColumnType("NVARCHAR(500)");
            entity.Property(e => e.UserLanguages)
                .HasDefaultValue("")
                .HasColumnType("NVARCHAR(10)");
        });

        modelBuilder.Entity<LogDebug>(entity =>
        {
            entity.HasKey(e => e.LogId);

            entity.ToTable("log_Debug");

            entity.Property(e => e.Content)
                .HasDefaultValue("")
                .HasColumnType("NVARCHAR(500)");
            entity.Property(e => e.Data).HasDefaultValue("");
            entity.Property(e => e.Message)
                .HasDefaultValue("")
                .HasColumnType("NVARCHAR(50)");
            entity.Property(e => e.Remark)
                .HasDefaultValue("")
                .HasColumnType("NVARCHAR(200)");
            entity.Property(e => e.TimeStamp)
                .HasDefaultValue("")
                .HasColumnType("NVARCHAR(20)");
        });

        modelBuilder.Entity<LogException>(entity =>
        {
            entity.HasKey(e => e.LogId);

            entity.ToTable("log_Exception");

            entity.Property(e => e.ActionKey)
                .HasDefaultValue("")
                .HasColumnType("NVARCHAR(32)");
            entity.Property(e => e.DebugData).HasDefaultValue("");
            entity.Property(e => e.DisposeMessage)
                .HasDefaultValue("")
                .HasColumnType("NVARCHAR(50)");
            entity.Property(e => e.DisposeStamp)
                .HasDefaultValue("")
                .HasColumnType("NVARCHAR(20)");
            entity.Property(e => e.DisposeUserId)
                .HasDefaultValue("")
                .HasColumnType("NVARCHAR(32)");
            entity.Property(e => e.Exception).HasDefaultValue("");
            entity.Property(e => e.ExceptionCode).HasColumnType("INT");
            entity.Property(e => e.ExceptionName)
                .HasDefaultValue("")
                .HasColumnType("NVARCHAR(50)");
            entity.Property(e => e.ExceptionType)
                .HasDefaultValue("")
                .HasColumnType("NVARCHAR(50)");
            entity.Property(e => e.FunctionName)
                .HasDefaultValue("")
                .HasColumnType("NVARCHAR(200)");
            entity.Property(e => e.IsDispose).HasColumnType("BIT");
            entity.Property(e => e.Message)
                .HasDefaultValue("")
                .HasColumnType("NVARCHAR(500)");
            entity.Property(e => e.PageKey)
                .HasDefaultValue("")
                .HasColumnType("NVARCHAR(32)");
            entity.Property(e => e.TimeStamp)
                .HasDefaultValue("")
                .HasColumnType("NVARCHAR(20)");
            entity.Property(e => e.Token)
                .HasDefaultValue("")
                .HasColumnType("NVARCHAR(32)");
        });

        modelBuilder.Entity<LogToken>(entity =>
        {
            entity.HasKey(e => e.LogId);

            entity.ToTable("log_Token");

            entity.Property(e => e.AppId)
                .HasDefaultValue("")
                .HasColumnType("NVARCHAR(18)");
            entity.Property(e => e.AuthType)
                .HasDefaultValue("")
                .HasColumnType("NVARCHAR(20)");
            entity.Property(e => e.IsDeveloper).HasColumnType("BIT");
            entity.Property(e => e.IsStaff).HasColumnType("BIT");
            entity.Property(e => e.Mobile)
                .HasDefaultValue("")
                .HasColumnType("NVARCHAR(11)");
            entity.Property(e => e.NickName)
                .HasDefaultValue("")
                .HasColumnType("NVARCHAR(50)");
            entity.Property(e => e.TerminalId)
                .HasDefaultValue("")
                .HasColumnType("NVARCHAR(50)");
            entity.Property(e => e.TimeStamp)
                .HasDefaultValueSql("''")
                .HasColumnType("NVARCHAR(20)");
            entity.Property(e => e.Token)
                .HasDefaultValue("")
                .HasColumnType("NVARCHAR(32)");
            entity.Property(e => e.TokenData).HasDefaultValue("");
            entity.Property(e => e.UserId)
                .HasDefaultValue("")
                .HasColumnType("NVARCHAR(32)");
            entity.Property(e => e.UserName)
                .HasDefaultValue("")
                .HasColumnType("NVARCHAR(50)");
        });

        modelBuilder.Entity<LogUserLogin>(entity =>
        {
            entity.HasKey(e => e.LogId);

            entity.ToTable("log_User_Login");

            entity.Property(e => e.AppId)
                .HasDefaultValue("")
                .HasColumnType("NVARCHAR(18)");
            entity.Property(e => e.BenchmarkLevel).HasColumnType("INT");
            entity.Property(e => e.DebugData).HasDefaultValue("");
            entity.Property(e => e.DeviceBrand)
                .HasDefaultValue("")
                .HasColumnType("NVARCHAR(50)");
            entity.Property(e => e.DeviceModel)
                .HasDefaultValue("")
                .HasColumnType("NVARCHAR(50)");
            entity.Property(e => e.Exception).HasDefaultValue("");
            entity.Property(e => e.IpAddress)
                .HasDefaultValue("")
                .HasColumnType("NVARCHAR(20)");
            entity.Property(e => e.IsSuccess).HasColumnType("BIT");
            entity.Property(e => e.LoginMethod)
                .HasDefaultValue("")
                .HasColumnType("NVARCHAR(20)");
            entity.Property(e => e.NewToken)
                .HasDefaultValue("")
                .HasColumnType("NVARCHAR(32)");
            entity.Property(e => e.OsType)
                .HasDefaultValue("")
                .HasColumnType("NVARCHAR(50)");
            entity.Property(e => e.OsVersion)
                .HasDefaultValue("")
                .HasColumnType("NVARCHAR(50)");
            entity.Property(e => e.PixelRatio).HasColumnType("Float");
            entity.Property(e => e.PostData).HasDefaultValue("");
            entity.Property(e => e.ScreenHeight).HasColumnType("INT");
            entity.Property(e => e.ScreenWidth).HasColumnType("INT");
            entity.Property(e => e.TerminalId)
                .HasDefaultValue("")
                .HasColumnType("NVARCHAR(50)");
            entity.Property(e => e.TimeStamp)
                .HasDefaultValueSql("''")
                .HasColumnType("NVARCHAR(20)");
            entity.Property(e => e.UserAgent)
                .HasDefaultValue("")
                .HasColumnType("NVARCHAR(500)");
            entity.Property(e => e.UserLanguages)
                .HasDefaultValue("")
                .HasColumnType("NVARCHAR(10)");
            entity.Property(e => e.WindowHeight).HasColumnType("INT");
            entity.Property(e => e.WindowWidth).HasColumnType("INT");
        });

        modelBuilder.Entity<SPara>(entity =>
        {
            entity.HasKey(e => e.ParaId);

            entity.ToTable("s_Para");

            entity.Property(e => e.ParaId)
                .HasDefaultValue("")
                .HasColumnType("NVARCHAR(50)");
            entity.Property(e => e.ParaName)
                .HasDefaultValue("")
                .HasColumnType("NVARCHAR(50)");
            entity.Property(e => e.ParaType)
                .HasDefaultValue("")
                .HasColumnType("NVARCHAR(50)");
            entity.Property(e => e.ParaValue)
                .HasDefaultValue("")
                .HasColumnType("NVARCHAR(200)");
            entity.Property(e => e.Remark)
                .HasDefaultValue("")
                .HasColumnType("NVARCHAR(200)");
        });

        modelBuilder.Entity<SService>(entity =>
        {
            entity.HasKey(e => e.ServiceId);

            entity.ToTable("s_Service");

            entity.Property(e => e.ServiceId)
                .HasDefaultValue("")
                .HasColumnType("NVARCHAR(50)");
            entity.Property(e => e.CorsUrls)
                .HasDefaultValue("")
                .HasColumnType("NVARCHAR(500)");
            entity.Property(e => e.CreateStamp)
                .HasDefaultValueSql("''")
                .HasColumnType("NVARCHAR(20)");
            entity.Property(e => e.ModifyStamp)
                .HasDefaultValueSql("''")
                .HasColumnType("NVARCHAR(20)");
            entity.Property(e => e.Remark)
                .HasDefaultValue("")
                .HasColumnType("NVARCHAR(200)");
            entity.Property(e => e.RootUrl)
                .HasDefaultValue("")
                .HasColumnType("NVARCHAR(50)");
            entity.Property(e => e.ServiceCode).HasColumnType("INT");
            entity.Property(e => e.ServiceName)
                .HasDefaultValue("")
                .HasColumnType("NVARCHAR(10)");
            entity.Property(e => e.ServiceType)
                .HasDefaultValue("")
                .HasColumnType("NVARCHAR(10)");
            entity.Property(e => e.SystemId)
                .HasDefaultValue("")
                .HasColumnType("NVARCHAR(10)");
            entity.Property(e => e.Timeout).HasColumnType("INT");
            entity.Property(e => e.VirtualPath)
                .HasDefaultValue("")
                .HasColumnType("NVARCHAR(20)");
        });

        modelBuilder.Entity<SSystem>(entity =>
        {
            entity.HasKey(e => e.SystemId);

            entity.ToTable("s_System");

            entity.Property(e => e.SystemId)
                .HasDefaultValue("")
                .HasColumnType("NVARCHAR(10)");
            entity.Property(e => e.CreateStamp)
                .HasDefaultValueSql("''")
                .HasColumnType("NVARCHAR(20)");
            entity.Property(e => e.ModifyStamp)
                .HasDefaultValueSql("''")
                .HasColumnType("NVARCHAR(20)");
            entity.Property(e => e.SystemCode).HasColumnType("INT");
            entity.Property(e => e.SystemName)
                .HasDefaultValue("")
                .HasColumnType("NVARCHAR(10)");
            entity.Property(e => e.SystemType)
                .HasDefaultValue("")
                .HasColumnType("NVARCHAR(10)");
        });

        modelBuilder.Entity<STerminal>(entity =>
        {
            entity.HasKey(e => e.TerminalId);

            entity.ToTable("s_Terminal");

            entity.Property(e => e.TerminalId)
                .HasDefaultValue("")
                .HasColumnType("NVARCHAR(50)");
            entity.Property(e => e.CreateStamp)
                .HasDefaultValueSql("''")
                .HasColumnType("NVARCHAR(20)");
            entity.Property(e => e.IsSite).HasColumnType("BIT");
            entity.Property(e => e.ModifyStamp)
                .HasDefaultValueSql("''")
                .HasColumnType("NVARCHAR(20)");
            entity.Property(e => e.Remark)
                .HasDefaultValue("")
                .HasColumnType("NVARCHAR(200)");
            entity.Property(e => e.SystemId)
                .HasDefaultValue("")
                .HasColumnType("NVARCHAR(10)");
            entity.Property(e => e.TerminalCode).HasColumnType("INT");
            entity.Property(e => e.TerminalKey)
                .HasDefaultValue("")
                .HasColumnType("NVARCHAR(18)");
            entity.Property(e => e.TerminalName)
                .HasDefaultValue("")
                .HasColumnType("NVARCHAR(10)");
            entity.Property(e => e.TerminalSecret)
                .HasDefaultValue("")
                .HasColumnType("NVARCHAR(32)");
            entity.Property(e => e.TerminalType)
                .HasDefaultValue("")
                .HasColumnType("NVARCHAR(10)");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
