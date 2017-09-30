using System;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Collections.ObjectModel;

namespace TeacherHiring
{
    public class teacherAPI
    {
        const string restURL = "http://online.cuprum.com/webapixamarin/api/{0}";
        HttpClient _client;

        public teacherAPI()
        {
            _client = new HttpClient();
        }
        public async Task<DataBase.Model.Usuario> Authenticate(Usuario Usuario, DataBase.Model.Usuario UsuarioLogged)
        {
            var uri = new Uri(string.Format(restURL, "Authenticate/Authenticate/"));
            try
            {
                var json = JsonConvert.SerializeObject(Usuario);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response = null;
                response = await _client.PostAsync(uri, content);
                if (response.IsSuccessStatusCode)
                {
                    var token = response.Headers.GetValues("Token");
                    var tokenexpiry = response.Headers.GetValues("TokenExpiry");
                    UsuarioLogged.Token = token.First();
                    UsuarioLogged.TokenExpiry = new DateTime(long.Parse(tokenexpiry.First().ToString()));
                }
            }
            catch (WebException e)
            {
                HttpWebResponse response = (HttpWebResponse)e.Response;
                if (response != null)
                {
                    if (response.StatusCode == HttpStatusCode.Unauthorized)
                    {
                        string challenge = null;
                        //challenge = response.GetResponseHeader("WWW-Authenticate");
                        //if (challenge != null)
                        //  Debug.WriteLine("\nThe following challenge was raised by the server:{0}", challenge);
                    }
                    else
                        Debug.WriteLine("\nThe following WebException was raised : {0}", e.Message);
                }
                else
                    Debug.WriteLine("\nResponse Received from server was null");

            }
            catch (Exception e)
            {
                Debug.WriteLine("\nThe following Exception was raised : {0}", e.Message);
            }
            finally
            {
                _client.Dispose();
            }
            return UsuarioLogged;
        }

        public async Task<List<Materia>> GetListMaterias(string Token)
        {

            var uri = string.Format(restURL, "Materia/GetListMateriaApps/");
            //public ObservableCollection<Materia> Materias = new ObservableCollection<Materia>();
            List<Materia> Materias = new List<Materia>();
            HttpRequestMessage requestMessage = new HttpRequestMessage(HttpMethod.Get, uri);
            // response = await _client.GetStreamAsync(requestMessage);
            DataBase.Model.Materia Materia = new DataBase.Model.Materia();
            try
            {
                var json = JsonConvert.SerializeObject(Token);
                var content = new StringContent(json, Encoding.UTF8, "token");
                HttpResponseMessage response = null;
                uri = string.Format(uri, content);
                response = await _client.GetAsync(uri);

                if (response.IsSuccessStatusCode)
                {
                    //Materias = response.Headers.GetValues("Token");
                }
            }
            catch (WebException e)
            {
                HttpWebResponse response = (HttpWebResponse)e.Response;
                if (response != null)
                {
                    if (response.StatusCode == HttpStatusCode.Unauthorized)
                    {
                        string challenge = null;
                 }
                    else
                        Debug.WriteLine("\nThe following WebException was raised : {0}", e.Message);
                }
                else
                    Debug.WriteLine("\nResponse Received from server was null");

            }
            catch (Exception e)
            {
                Debug.WriteLine("\nThe following Exception was raised : {0}", e.Message);
            }
            finally
            {
                _client.Dispose();
            }
            return Materias;
        }



    }
}
