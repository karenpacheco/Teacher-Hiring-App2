using SQLite;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;
using System.Collections.ObjectModel;

namespace TeacherHiring.DataBase.Access
{
    class UsuarioDataAccess
    {
        private SQLiteConnection database;
        private static object collisionLock = new object();
        public ObservableCollection<DataBase.Model.Usuario> Usuario { get; set; }
        // Data operation here ...
        
        public UsuarioDataAccess()
        {
            database = DependencyService.Get<ISQLite>().DbConnection();
            database.CreateTable<DataBase.Model.Usuario>();
            this.Usuario =  new ObservableCollection<DataBase.Model.Usuario>(database.Table<DataBase.Model.Usuario>());
            // If the table is empty, initialize the collection
            /*if (!database.Table<DataBase.Model.Usuario>().Any())
            {
                AddNewCustomer();
            }*/
        }

        public void AddUser(Usuario _user)
        {
            this.Usuario.Add(new DataBase.Model.Usuario
            {
                Id = _user.Id,
                Nombre = _user.Nombre,
                ClaveAcceso = _user.ClaveAcceso,
                Contrasena = _user.Contrasena,
                IdTipoPerson = _user.IdTipoPerson,
                ClientCreatedOn = _user.ClientCreatedOn,
                ClientID = _user.ClientID,
                ClientSecret = _user.ClientSecret,
                Token = _user.Token,
                TokenExpiry = _user.TokenExpiry
            });
        }

        public IEnumerable<DataBase.Model.Usuario> GetUsers()
        {
            lock (collisionLock)
            {
                var query = from user in database.Table<DataBase.Model.Usuario>()
                            select user;
                return query.AsEnumerable();
            }
        }

        public DataBase.Model.Usuario GetUsuarioById(int id)
        {
            lock (collisionLock)
            {
                return database.Table<DataBase.Model.Usuario>().FirstOrDefault(Usuario => Usuario.Id == id);
            }
        }

        public DataBase.Model.Usuario GetUsuarioByClaveAcceso(string claveAcceso)
        {
            lock (collisionLock)
            {
                return database.Table<DataBase.Model.Usuario>().FirstOrDefault(Usuario => Usuario.ClaveAcceso == claveAcceso);
            }
        }

        public int SaveUser(DataBase.Model.Usuario usuario)
        {
            lock (collisionLock)
            {
                if (usuario.Id != 0)
                {
                    database.Update(usuario);
                    return usuario.Id;
                }
                else
                {
                    database.Insert(usuario);
                    return usuario.Id;
                }
            }
        }


    }
}