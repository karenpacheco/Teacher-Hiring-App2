using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace TeacherHiring.Modelos
{

    public class Alumno
    {
        public int IdAlumno { get; set; }

        public string Matricula
        {
            get;
            set;
        }
        public string Nombre
        {
            get;
            set;
        }
        public string Facultad
        {
            get;
            set;
        }

    }

}