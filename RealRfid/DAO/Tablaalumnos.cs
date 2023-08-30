using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Data;

namespace RealRfid.DAO{
    class Tablaalumnos:Conexion{
        string cmd;
        public DataTable tabla(){
            cmd = "SELECT * FROM alumnos where asis='NO'";
            MySqlDataAdapter adp = new MySqlDataAdapter(cmd,conecxion());
            DataTable Consulta = new DataTable();
            adp.Fill(Consulta);
            return Consulta;
        }
    }
}
