using SQLite;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;
using System.Collections.ObjectModel;
namespace TeacherHiring.DataBase.Access
{
    class MateriaDataAccess
    {
        private SQLiteConnection database;
        private static object collisionLock = new object();
        public ObservableCollection<DataBase.Model.Materia> Materia { get; set; }

        public MateriaDataAccess()
        {
            database = DependencyService.Get<ISQLite>().DbConnection();
            database.CreateTable<DataBase.Model.Materia>();
            this.Materia = new ObservableCollection<DataBase.Model.Materia>(database.Table<DataBase.Model.Materia>());

        }

        public DataBase.Model.Materia GetMateriaById(int id)
        {
            lock (collisionLock)
            {
                return database.Table<DataBase.Model.Materia>().FirstOrDefault(Materia => Materia.MateriaId == id);
            }
        }

        public int SaveMateria(DataBase.Model.Materia materia)
        {
            lock (collisionLock)
            {
                if (materia.MateriaId != 0)
                {
                    database.Update(materia);
                    return materia.MateriaId;
                }
                else
                {
                    database.Insert(materia);
                    return materia.MateriaId;
                }
            }
        }
    }
}
