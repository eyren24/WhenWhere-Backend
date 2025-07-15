using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;

namespace Database.Data;

public partial class AppDbContext : DbContext {
    private readonly IHostEnvironment _environment;
    private readonly ITokenService _tokenService;

    public MyDbContext(DbContextOptions<AppDbContext> options, ITokenService tokenService,
        IHostEnvironment environment) : base(options) {
        _tokenService = tokenService;
        _environment = environment;
    }

    public TokenInfoDTO TokenInfo { get; private set; }

    private void SetUserInfo() {
        var dati = _tokenService.GetInfoToken();
        if (dati == null)
            return;
        TokenInfo = dati;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
        SetUserInfo();
        optionsBuilder
            .EnableSensitiveDataLogging()
            .EnableDetailedErrors();
    }

    /*partial void OnModelCreatingPartial(ModelBuilder modelBuilder) {
        base.OnModelCreating(modelBuilder);
        /*modelBuilder.Entity<Utenti>()
            .HasQueryFilter(u => TokenInfo.Ruolo == ERuolo.Amministratore || u.id == TokenInfo.UtenteId);
    }*/
}