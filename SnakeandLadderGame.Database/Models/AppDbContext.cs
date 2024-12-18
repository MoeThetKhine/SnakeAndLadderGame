using Microsoft.EntityFrameworkCore;

namespace SnakeandLadderGame.Database.Models;

public partial class AppDbContext : DbContext
{
    public AppDbContext()
    {
    }

    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    #region DBSet

    public virtual DbSet<TblBoard> TblBoards { get; set; }

    public virtual DbSet<TblCell> TblCells { get; set; }

    public virtual DbSet<TblGame> TblGames { get; set; }

    public virtual DbSet<TblGamePlayer> TblGamePlayers { get; set; }

    public virtual DbSet<TblGameWinner> TblGameWinners { get; set; }

    public virtual DbSet<TblPlayer> TblPlayers { get; set; }

    #endregion

    #region Connection

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=.;Database=SnakeandLadderGame;User Id=sa;Password=sasa@123;TrustServerCertificate=True;");

    #endregion

    #region OnModelCreating

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

        #region TblBoard

        modelBuilder.Entity<TblBoard>(entity =>
        {
            entity.HasKey(e => e.BoardId).HasName("PK__Tbl_Boar__F9646BD2DAFE5E71");

            entity.ToTable("Tbl_Board");

            entity.Property(e => e.BoardId)
                .HasMaxLength(250)
                .HasColumnName("BoardID");
        });

        #endregion

        #region TblCell

        modelBuilder.Entity<TblCell>(entity =>
        {
            entity.HasKey(e => e.CellId).HasName("PK__Tbl_Cell__EA424A282C365540");

            entity.ToTable("Tbl_Cell");

            entity.Property(e => e.CellId)
                .HasMaxLength(250)
                .HasColumnName("CellID");
            entity.Property(e => e.BoardId)
                .HasMaxLength(250)
                .HasColumnName("BoardID");
            entity.Property(e => e.CellType).HasMaxLength(50);

            entity.HasOne(d => d.Board).WithMany(p => p.TblCells)
                .HasForeignKey(d => d.BoardId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Tbl_Cell__BoardI__3A81B327");
        });

        #endregion

        #region TblGame

        modelBuilder.Entity<TblGame>(entity =>
        {
            entity.HasKey(e => e.GameId).HasName("PK__Tbl_Game__2AB897DD55A7EDAC");

            entity.ToTable("Tbl_Games");

            entity.Property(e => e.GameId)
                .HasMaxLength(250)
                .HasColumnName("GameID");
            entity.Property(e => e.BoardId)
                .HasMaxLength(250)
                .HasColumnName("BoardID");
            entity.Property(e => e.EndDate).HasColumnType("datetime");
            entity.Property(e => e.GameStatus).HasMaxLength(50);
            entity.Property(e => e.StartDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.Board).WithMany(p => p.TblGames)
                .HasForeignKey(d => d.BoardId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Tbl_Games__Board__4222D4EF");
        });

        #endregion

        modelBuilder.Entity<TblGamePlayer>(entity =>
        {
            entity.HasKey(e => e.GamePlayerId).HasName("PK__Tbl_Game__2D47DFAE33E63707");

            entity.ToTable("Tbl_GamePlayers");

            entity.Property(e => e.GamePlayerId)
                .HasMaxLength(250)
                .HasColumnName("GamePlayerID");
            entity.Property(e => e.Color).HasMaxLength(50);
            entity.Property(e => e.CurrentPosition).HasDefaultValue(1);
            entity.Property(e => e.GameId)
                .HasMaxLength(250)
                .HasColumnName("GameID");
            entity.Property(e => e.PlayerId)
                .HasMaxLength(250)
                .HasColumnName("PlayerID");
            entity.Property(e => e.PlayerStatus).HasMaxLength(50);

            entity.HasOne(d => d.Game).WithMany(p => p.TblGamePlayers)
                .HasForeignKey(d => d.GameId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Tbl_GameP__GameI__47DBAE45");

            entity.HasOne(d => d.Player).WithMany(p => p.TblGamePlayers)
                .HasForeignKey(d => d.PlayerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Tbl_GameP__Playe__48CFD27E");
        });

        modelBuilder.Entity<TblGameWinner>(entity =>
        {
            entity.HasKey(e => e.GameWinnerId).HasName("PK__Tbl_Game__EB42F7ACB0FF77F6");

            entity.ToTable("Tbl_GameWinners");

            entity.Property(e => e.GameWinnerId)
                .HasMaxLength(250)
                .HasColumnName("GameWinnerID");
            entity.Property(e => e.GameId)
                .HasMaxLength(250)
                .HasColumnName("GameID");
            entity.Property(e => e.PlayerId)
                .HasMaxLength(250)
                .HasColumnName("PlayerID");

            entity.HasOne(d => d.Game).WithMany(p => p.TblGameWinners)
                .HasForeignKey(d => d.GameId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Tbl_GameW__GameI__4BAC3F29");

            entity.HasOne(d => d.Player).WithMany(p => p.TblGameWinners)
                .HasForeignKey(d => d.PlayerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Tbl_GameW__Playe__4CA06362");
        });

        modelBuilder.Entity<TblPlayer>(entity =>
        {
            entity.HasKey(e => e.PlayerId).HasName("PK__Tbl_Play__4A4E74A8CDE9AC8A");

            entity.ToTable("Tbl_Player");

            entity.HasIndex(e => e.Email, "UQ__Tbl_Play__A9D105343FDAC302").IsUnique();

            entity.Property(e => e.PlayerId)
                .HasMaxLength(250)
                .HasColumnName("PlayerID");
            entity.Property(e => e.Email).HasMaxLength(255);
            entity.Property(e => e.PlayerName).HasMaxLength(100);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    #endregion

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
