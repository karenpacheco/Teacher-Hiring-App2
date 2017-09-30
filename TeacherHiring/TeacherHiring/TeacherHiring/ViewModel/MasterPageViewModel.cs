using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TeacherHiring.Views;
using Xamarin.Forms;

namespace TeacherHiring.ViewModel
{
    public class MasterPageViewModel:BaseViewModel
    {
        public ObservableCollection<MasterPageMenuItem> MenuItems { get; set; }
        public ICommand SignOut { get; set; }

        private string m_LabelUsrText;

        public string LabelUsrText
        {
            get
            {
                return m_LabelUsrText;
            }
            set
            {
                m_LabelUsrText = value;
                OnPropertyChanged("LabelUsrText");
            }
        }

        private string m_LabelTipoPerText;

        public string LabelTipoPerText
        {
            get
            {
                return m_LabelTipoPerText;
            }
            set
            {
                m_LabelTipoPerText = value;
                OnPropertyChanged("LabelTipoPerText");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public MasterPageViewModel()
        {
            LabelUsrText = m_LabelUsrText;
            LabelUsrText = App.UsuarioLogged.ClaveAcceso;
            
            if (App.UsuarioLogged.IdTipoPerson == 1)
            {
                LabelTipoPerText = "Profesor";
                MenuItems = new ObservableCollection<MasterPageMenuItem>(new[]
                {
                new MasterPageMenuItem(){Id=0,Title="Inicio",TargetType = typeof(WelcomePage),IconPath="ic_view_dashboard.png" },
                new MasterPageMenuItem(){Id=4,Title="Registrar Asesoria",TargetType = typeof(RegAsesoriaPage),IconPath="ic_view_dashboard.png" },
                new MasterPageMenuItem(){Id=2,Title="Confirmar Asesoria",TargetType = typeof(ConfAsesoriaPage),IconPath="ic_view_dashboard.png" },
                new MasterPageMenuItem(){Id=1,Title="Asesorias Aceptadas",TargetType = typeof(AsesoriasPage),IconPath="ic_view_dashboard.png" },

                 });
            }
            else
            {
                LabelTipoPerText = "Alumno";
                MenuItems = new ObservableCollection<MasterPageMenuItem>(new[]
                  {
                new MasterPageMenuItem(){Id=0,Title="Inicio",TargetType = typeof(WelcomePage),IconPath="ic_view_dashboard.png" },
                new MasterPageMenuItem(){Id=2,Title="Solicitar Asesorias",TargetType = typeof(SolicitudPage),IconPath="ic_view_dashboard.png" },
                new MasterPageMenuItem(){Id=2,Title="Solicitudes Realizadas",TargetType = typeof(SolRealizadaPage),IconPath="ic_view_dashboard.png" },
                  });
            }
          
            SignOut = new Command(async () => await DeleteSession());

        }
        
        async Task DeleteSession()
        {
            var askClose = await Application.Current.MainPage.DisplayAlert(
                "Cerrar Sesion",
                "¿Estas Seguro que deseas cerrar sesion?",
                "Cancelar","Ok" 
            );

            if (askClose)
                return;

            NavigateToCurrentPage(new LoginPage());
        }

    }

    

}
