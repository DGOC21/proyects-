using Microsoft.EntityFrameworkCore;

namespace QRGenerador.Models
{
    public class QrContext: DbContext
    {
        public QrContext(DbContextOptions options): base(options) 
        {
        }
        public DbSet<QRAtributos> CodigosQr { get; set; }

    }
}
