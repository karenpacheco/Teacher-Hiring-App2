using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeacherHiring.ViewModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TeacherHiring.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class WelcomePage : ContentPage
    {
        WelcomePageViewModel viewModel;
        public WelcomePage()
        {
            //Content = tblView();
            //Title = "Inicio";
            InitializeComponent();
            BindingContext = viewModel = new WelcomePageViewModel();
        }

        public TableView tblView()
        {
            TableView _tblView = new TableView();
            _tblView.VerticalOptions = LayoutOptions.FillAndExpand;
            _tblView.Root = new TableRoot {
                tblSectionInformacion("Pagina de inicio")

            };
            _tblView.Intent = TableIntent.Form;
            _tblView.HasUnevenRows = true; //que respete tamaño de view cells
            return _tblView;
        }
        public TableSection tblSectionInformacion(string TitleSection)
        {
            TableSection _tblSection = new TableSection(TitleSection)
            {
                new EntryCell { Label = "", Text = "Bienvenido:" },
                new EntryCell { Label = "", Text = "Karen" }
                //new EntryCell { Label = "", Text = App.UsuarioLogged.ClaveAcceso.ToString() }
            };
            return _tblSection;
        }

    }
}