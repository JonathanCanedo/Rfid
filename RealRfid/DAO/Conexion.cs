using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace RealRfid.DAO{
    class Conexion{
        MySqlConnection cone;
        string cad;

        public MySqlConnection conecxion(){
            cad = "datasource = 127.0.0.1; port = 3306; username = root; password =; database = alumnos";
            cone = new MySqlConnection(cad);
            return cone;
        }
        public void abrir(){
            cone.Open();
        }
        public void cerrar(){
            cone.Close();
        }
    }
}
