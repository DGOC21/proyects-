using System.ComponentModel.DataAnnotations;

namespace AlumPromBD.Models
{
    public class Alumno
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Este campo es requerido")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "Este campo es requerido")]
        [Range(0, 10, ErrorMessage = "Ingrese una nota válida (entre 0 y 10)")]
        public double Nota1 { get; set; }

        [Required(ErrorMessage = "Este campo es requerido")]
        [Range(0, 10, ErrorMessage = "Ingrese una nota válida (entre 0 y 10)")]
        public double Nota2 { get; set; }

        [Required(ErrorMessage = "Este campo es requerido")]
        [Range(0, 10, ErrorMessage = "Ingrese una nota válida (entre 0 y 10)")]
        public double Nota3 { get; set; }

        public double Promedio { get; set; }

        public bool Estado { get; set; }
    }
}
