using FitnessReservatieBL.Domein;
using FitnessReservatieBL.Interfaces;
using FitnessReservatieDL.Exceptions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessReservatieDL {
    public class FitnessRepoADO : IFitnessRepository {
        private string connectieString;
        public FitnessRepoADO(string connectieString) {
            this.connectieString = connectieString;
        }
        private SqlConnection GetConnection() {
            return new SqlConnection(connectieString);
        }


        public IReadOnlyList<Tijdslot> GeefTijdsloten() {
            SqlConnection conn = GetConnection();
            string query = "SELECT * FROM dbo.Tijdslot;";
            try {
                using (SqlCommand cmd = conn.CreateCommand()) {
                    cmd.CommandText = query;
                    conn.Open();
                    List<Tijdslot> tijdsloten = new List<Tijdslot>();
                    IDataReader reader = cmd.ExecuteReader();
                    while (reader.Read()) {
                        int i = (int)reader["id"];
                        TimeSpan beginuur = (TimeSpan)reader["beginuur"];
                        TimeSpan einduur = (TimeSpan)reader["einduur"];
                        Tijdslot t = new Tijdslot(i, beginuur, einduur);
                        tijdsloten.Add(t);
                    }
                    return tijdsloten.AsReadOnly();
                }

            } catch (Exception ex) {
                throw new FitnessRepoADOException("GeefTijdsloten", ex);
            } finally {
                conn.Close();
            }
    }

        public IReadOnlyList<Toestel> GeefToestellen() {
            return null; //TODO
            //throw new NotImplementedException();
        }
    }
}