using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TeacherHiring.Views;
using TeacherHiring.Model;
using Xamarin.Forms;
using TeacherHiring.Modelos;

namespace TeacherHiring
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new LoginPage());
        }

        public static Profesor ProfesorLogged { get; set; }
        public static List<ProfesorMateria> MateriasSelected { get; set; }
        public static Alumno AlumnoLogged { get; set; }
        public static List<AlumnoMateria> MateriasAlumnoSelected { get; set; }
        public static DataBase.Model.Usuario UsuarioLogged { get; set; }


        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
