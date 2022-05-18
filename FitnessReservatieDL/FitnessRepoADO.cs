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
            }
            finally {
                conn.Close();
            }
        }

        public IReadOnlyList<Toestel> GeefToestellen() {
            SqlConnection conn = GetConnection();
            string query = "SELECT * FROM dbo.Toestel WHERE verwijderd=@verwijderd;";
            try {
                List<Toestel> toestellen = new List<Toestel>();
                using (SqlCommand cmd = conn.CreateCommand()) {
                    conn.Open();
                    cmd.CommandText = query;
                    cmd.Parameters.AddWithValue("verwijderd", false);

                    IDataReader reader = cmd.ExecuteReader();
                    while (reader.Read()) {
                        Toestel t = new Toestel((string)reader["toestel"], (bool)reader["beschikbaar"]);
                        t.ZetId((int)reader["id"]);
                        toestellen.Add(t);
                    }
                }
                return toestellen.AsReadOnly();

            } catch (Exception ex) {
                throw new FitnessRepoADOException("GeefToestellen", ex);
            }
            finally {
                conn.Close();
            }
        }
    }
}