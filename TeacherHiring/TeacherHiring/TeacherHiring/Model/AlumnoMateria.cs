using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeacherHiring
{
    public class AlumnoMateria
    {
        public int IdAlumnoMateria { get; set; }
        public int IdProfesorMateria { get; set; }
        public int IdAlumno { get; set; }
        public int IdProfesor { get; set; }
        public int IdMateria { get; set; }
        public string NombreAlumno { get; set; }
        public string NombreMateria { get; set; }
        public string NombreProfesor { get; set; }
        public DateTime FechaHora { get; set; }
        public decimal Latitud { get; set; }
        public decimal Longitud { get; set; }
        public Boolean Aceptada { get; set; }
    }
}
