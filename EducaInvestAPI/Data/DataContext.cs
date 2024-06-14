using EducaInvestAPI.Entities;
using EducaInvestAPI.Entities.Enums;
using EducaInvestAPI.Utils;
using Microsoft.EntityFrameworkCore;


namespace EducaInvestAPI.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        public DbSet<Projeto> TB_PROJETOS { get; set; }
        public DbSet<Cronograma> TB_CRONOGRAMAS { get; set; }
        public DbSet<Atividade> TB_ATIVIDADES { get; set; }
        public DbSet<Usuario> TB_USUARIOS { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=educainvestapi.database.windows.net;Database=DB-EDUCAINVEST;User Id=nathalli;Password=EIdb2024#;",
                options => options.EnableRetryOnFailure());
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Projeto>(entity =>
            {
                entity.ToTable("TB_PROJETOS");
                entity.Property(e => e.NomeProjeto)
                    .IsRequired()
                    .HasMaxLength(30);

                entity.Property(e => e.Subtitulo)
                    .HasMaxLength(65);

                entity.Property(e => e.DescricaoProjeto)
                    .HasMaxLength(255);

                entity.Property(e => e.CustoProjeto)
                    .HasColumnType("decimal(18, 2)");

                entity.HasOne(e => e.Usuario)
                      .WithMany(u => u.Projetos)
                      .HasForeignKey(e => e.UsuarioId);

                entity.HasOne(e => e.Cronograma)
                      .WithOne(c => c.Projeto)
                      .HasForeignKey<Cronograma>(c => c.ProjetoId);

            });

            modelBuilder.Entity<Cronograma>(entity =>
            {
                entity.ToTable("TB_CRONOGRAMAS");

                entity.HasOne(e => e.Projeto)
                      .WithOne(p => p.Cronograma)
                      .HasForeignKey<Cronograma>(e => e.ProjetoId);

                entity.HasMany(e => e.Atividades)
                      .WithOne(a => a.Cronograma)
                      .HasForeignKey(a => a.CronogramaId);
            });

            modelBuilder.Entity<Atividade>(entity =>
            {
                entity.ToTable("TB_ATIVIDADES");
                entity.Property(a => a.DescricaoAtividade).HasMaxLength(200);
                entity.Property(a => a.Percentual).HasColumnType("decimal(18, 2)");

                entity.HasOne(a => a.Cronograma)
                      .WithMany(c => c.Atividades)
                      .HasForeignKey(a => a.CronogramaId);
            });

            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.ToTable("TB_USUARIOS");
                entity.Property(e => e.Nome)
                    .IsRequired()
                    .HasMaxLength(30);

                entity.Property(e => e.Sobrenome)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.CPF)
                    .IsRequired()
                    .HasMaxLength(11);

               entity.Property(e => e.Telefone)
                    .IsRequired()
                    .HasMaxLength(11);

               entity.Property(e => e.LinkSocial)
                    .HasMaxLength(255);

               entity.Property(e => e.Cidade)
                    .HasMaxLength(100);

               entity.Property(e => e.Perfil)
                     .IsRequired();

               entity.HasMany(e => e.Projetos)
                     .WithOne(p => p.Usuario)
                     .HasForeignKey(p => p.UsuarioId);

            });

            Usuario usuario = new Usuario();
            Criptografia.CriarPasswordHash("123456", out byte[] hash, out byte[] salt);
            usuario.Id = 1;
            usuario.Perfil = PerfilUsuarioEnum.Administrador;
            usuario.Email = "educainvest.co@gmail.com";
            usuario.Nome = string.Empty;
            usuario.Sobrenome = string.Empty;
            usuario.CPF = string.Empty;
            usuario.Telefone = string.Empty;
            usuario.LinkSocial = string.Empty;
            usuario.Cidade = string.Empty;
            usuario.UF = EstadoEnum.DF;
            usuario.DataAcesso = DateTime.Now;
            usuario.PasswordString = string.Empty;
            usuario.PasswordHash = hash;
            usuario.PasswordSalt = salt;

            modelBuilder.Entity<Usuario>().HasData(usuario);
        }
    }
}
