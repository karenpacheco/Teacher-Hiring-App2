using Acr.UserDialogs;
using Plugin.DeviceInfo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TeacherHiring.Modelos;
using TeacherHiring.Views;
using Xamarin.Forms;


namespace TeacherHiring
{
	class LoginViewModel:BaseViewModel
    {
        DataBase.Model.Usuario _user;
        public DataBase.Model.Usuario User
        {
            get { return _user; }
            set { _user = value; RaisePropertyChanged(); }
        }

        bool _tipoPerson;
        public bool tipoPerson
        {
            get
            {
                return _tipoPerson;
            }
            set
            {
                _tipoPerson = value;
            }
        }


        public ICommand LoginCommand { get; set; }
        public LoginViewModel()
        {
            tipoPerson = _tipoPerson;
            LoginCommand = new Command(async () => await Login());
            User = new DataBase.Model.Usuario();

        }

        async Task Login()
        {
            if (IsBusy)
                return;

            IsBusy = true;
            try
            {
                Usuario personLogin = new Usuario();
                personLogin.ClientCreatedOn = DateTime.Now;
                personLogin.ClientID = string.Empty;
                personLogin.ClientSecret = string.Empty;
                personLogin.Id = 0;
                personLogin.IdTipoPerson = Convert.ToInt32(tipoPerson); 
                personLogin.Nombre = string.Empty;
                personLogin.ClaveAcceso = _user.ClaveAcceso;
                personLogin.Contrasena = _user.Contrasena;

               
                using (UserDialogs.Instance.Loading("Verificando.."))
                {
                    User = await ServiceAPI.Authenticate(personLogin, User );
                }
                if (string.IsNullOrEmpty(User.Token))
                {
                    UserDialogs.Instance.ShowError("Acceso denegado");
                    return;
                }
                else
                {
                    userGlobal.ClaveAcceso = User.ClaveAcceso;
                    userGlobal.IdTipoPerson = Convert.ToInt32(tipoPerson); //User.IdTipoPerson;
                    userGlobal.Contrasena = User.Contrasena;
                    userGlobal.Token = User.Token;

                    App.UsuarioLogged = User;
                    materias = await ServiceAPI.GetListMaterias(userGlobal.Token);

                    if (tipoPerson)
                    {
                        Profesor _myprof = new Profesor();
                        _myprof.idUsuario = User.Id;
                        _myprof.Nombre = "Karen";
                        _myprof.APaterno = "Pacheco";
                        _myprof.APaterno = "Cortez";
                        App.ProfesorLogged = _myprof;
                       // await Navigation.PushAsync(new ProfesorInformacion(_myprof));
                        //opciones para navegar
                        //push abrirla como si fuera un hijo
                        //pushmodal te abre una pantalla arriba pero ya no se puede regresar a la otra pantalla
                    }
                    else
                    {
                        //await DisplayAlert("Teacher App", "Lo sentimos" + _username, "Ok");
                        Alumno _myAlum = new Alumno();
                        _myAlum.IdAlumno = User.Id;//(new Random()).Next();
                        _myAlum.Matricula = "1234567";
                        _myAlum.Nombre = "Karen Pacheco";
                        _myAlum.Facultad = "FACPYA";
                        App.AlumnoLogged = _myAlum;
                      //  await Navigation.PushAsync(new AlumnoInformacion(_myAlum));
                    }

                    NavigateToCurrentPage(new ShellPage());
                }
                
                
                /*var deviceInfo = CrossDeviceInfo.Current;
                await Application.Current.MainPage.DisplayAlert("Id", deviceInfo.Id, "Ok");
                await Application.Current.MainPage.DisplayAlert("Language", deviceInfo.Idiom.ToString(), "Ok");
                await Application.Current.MainPage.DisplayAlert("Model", deviceInfo.Model, "Ok");
                await Application.Current.MainPage.DisplayAlert("Platform", deviceInfo.Platform.ToString(), "Ok");
                await Application.Current.MainPage.DisplayAlert("Version", deviceInfo.Version, "Ok");
                await Application.Current.MainPage.DisplayAlert("VersionNumber", deviceInfo.VersionNumber.Revision.ToString(), "Ok");
                */
            }
	    	catch { }
		    finally
		    {
		        IsBusy = false;
		    }
		}

        //public void Toggled_handler(object sender, ToggledEventArgs e)
        //{
        //    Update();
        //}
    }
}