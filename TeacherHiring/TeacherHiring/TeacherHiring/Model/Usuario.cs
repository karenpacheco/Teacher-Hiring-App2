using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeacherHiring
{
    public class Usuario
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string ClaveAcceso { get; set; }
        public string Contrasena { get; set; }
        public int IdTipoPerson { get; set; }
        public DateTime ClientCreatedOn { get; set; }
        public string ClientID { get; set; }
        public string ClientSecret { get; set; }
        public string Token { get; set; }
        public DateTime TokenExpiry { get; set; }
    }
}
