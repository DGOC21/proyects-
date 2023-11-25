using Microsoft.EntityFrameworkCore;

namespace AlumPromBD.Models
{
    public class AlumnoContext: DbContext
    {
        public DbSet<Alumno> Alumnos { get; set; }

        public AlumnoContext(DbContextOptions options) : base(options)
        {

        }
    }
}
