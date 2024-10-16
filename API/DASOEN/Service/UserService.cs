using DASOEN.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Web;
using DASOEN.provider;
using Newtonsoft.Json.Linq;

namespace DASOEN.Service
{
    public class UserService
    {
        public DASOEN.provider.SQLAdapter SQL = new DASOEN.provider.SQLAdapter();
        public User GetUserByCredentials(string UserName, string password)
        {

            if (ValidateUser(UserName, password)==false){
                return null;
            }
            User user = new User() { Id = "1", UserName = UserName, Password = password };
            if (user != null)
            {
                user.Password = string.Empty;
            }
            return user;
        }
        private Boolean ValidateUser(String User,String Password)
        {
            String sMensajeError = "";
            string sql = "select Activo from " + ConfigurationManager.AppSettings["TablaValidacionUsuario"];
            string sResponse = SQL.TraeValores(sql, new String[] { "Nombre","Password", "Empresa","UsuarioWeb" }, new String[] { User,Password, SQL.IdEmpresa, "1" },"Gen");
            //string sResponse = SQL.TraeValores(sql, new String[] { "Nombre", "Password", "Empresa" }, new String[] { User, Password, SQL.IdEmpresa });

            JObject JResponse = new JObject();
            try
            {
                JResponse = JObject.Parse(sResponse.Replace("[","").Replace("]", ""));
            }
            catch (Exception Error)
            {
                return false;
            }

            Boolean response = false;
            try
            {
                response = Convert.ToBoolean(Convert.ToInt32(JResponse["Activo"].ToString()));
            }
            catch(Exception)
            {
                response = false;
            }

            return response;
        }
    }
}