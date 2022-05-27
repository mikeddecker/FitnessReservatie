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
    public class ToestelRepoADO : IToestelRepository {
        private string connectieString;
        public ToestelRepoADO(string connectieString) {
            this.connectieString = connectieString;
        }
        private SqlConnection GetConnection() {
            return new SqlConnection(connectieString);
        }
        public Dictionary<int, Toestel> GeefBeschikbareToestellen() {
            SqlConnection conn = GetConnection();
            string query = "SELECT id, toestel, beschikbaar FROM dbo.Toestel WHERE verwijderd='false';";
            try {
                conn.Open();
                Dictionary<int, Toestel> toestellen = new Dictionary<int, Toestel>();
                using (SqlCommand cmd = conn.CreateCommand()) {
                    cmd.CommandText = query;
                    IDataReader reader = cmd.ExecuteReader();
                    while (reader.Read()) {
                        Toestel t = new Toestel((string)reader["toestel"], (bool)reader["beschikbaar"]);
                        t.ZetId((int)reader["id"]);
                        toestellen.Add(t.ToestelID, t);
                    }
                }
                return toestellen;
            } catch (Exception ex) {
                throw new ToestelRepoADOException("GeefAlleToestellen", ex);
            }
        }
        public bool HeeftToestelToekomstigeReservaties(int toestelID) {
            SqlConnection conn = GetConnection();
            string query = "SELECT COUNT(*) FROM ReservatieDetail WHERE toestelID = @toestelID";
            try {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand()) {
                    cmd.CommandText = query;
                    cmd.Parameters.AddWithValue("@toestelID", toestelID);
                    int aantalReservatiesOpToestelID = (int)cmd.ExecuteScalar();
                    if (aantalReservatiesOpToestelID > 0) {
                        return true;
                    } else {
                        return false;
                    }
                }
            } catch (Exception ex) {
                throw new ToestelRepoADOException("HeefToestelToekomstigeReservaties", ex);
            }
            finally {
                conn.Close();
            }
        }
        public int SchrijfToestelInDB(Toestel nieuwToestel) {
            SqlConnection conn = GetConnection();
            string query = "INSERT INTO dbo.Toestel(toestel) output inserted.id VALUES(@type);";
            try {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand()) {
                    cmd.CommandText = query;
                    cmd.Parameters.AddWithValue("@type", nieuwToestel.Type);
                    int toestelID = (int)cmd.ExecuteScalar();
                    return toestelID;
                }
            } catch (Exception ex) {

                throw new ToestelRepoADOException("SchrijfToestelInDB", ex);
            }
            finally {
                conn.Close();
            }
        }
        public void UpdateToestelBeschikbaarheid(int toestelID, bool beschikbaar) {
            SqlConnection conn = GetConnection();
            string query = "UPDATE Toestel SET beschikbaar=@beschikbaar WHERE id=@toestelID";
            try {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand()) {
                    cmd.CommandText = query;
                    cmd.Parameters.AddWithValue("@toestelID", toestelID);
                    cmd.Parameters.AddWithValue("@beschikbaar", beschikbaar);
                    cmd.ExecuteNonQuery();
                }
            } catch (Exception ex) {
                throw new ToestelRepoADOException("ZetToestelOpOnbeschikbaar", ex);
            }
            finally {
                conn.Close();
            }
        }
        public void VerwijderToestel(int toestelID) {
            SqlConnection conn = GetConnection();
            string query = "UPDATE dbo.Toestel SET verwijderd='true', beschikbaar='false' WHERE id=@id";
            try {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand()) {
                    cmd.CommandText = query;
                    cmd.Parameters.AddWithValue("@id", toestelID);
                    cmd.ExecuteNonQuery();
                }
            } catch (Exception ex) {
                throw new ToestelRepoADOException("VerwijderToestel", ex);
            }
            finally {
                conn.Close();
            }
        }

        public List<int> GeefToestelIDsZonderOpenstaandeReservaties(string zoektekst) {
            SqlConnection conn = GetConnection();
            string query = "SELECT id FROM dbo.Toestel t " +
                "WHERE verwijderd='false' AND toestel=@toestelnaam AND t.id NOT IN " +
                    "(SELECT DISTINCT r.toestelID FROM dbo.ReservatieDetail r " +
                    "LEFT JOIN dbo.Tijdslot t ON t.ID=r.tijdslotID " +
                    "WHERE GETDATE()<(CAST(r.datum AS datetime) + CAST(t.Beginuur AS datetime)))";
            try {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand()) {
                    cmd.CommandText = query;
                    cmd.Parameters.AddWithValue("@toestelnaam", zoektekst);

                    List<int> toestelIDs = new List<int>();
                    IDataReader reader = cmd.ExecuteReader();
                    while (reader.Read()) {
                        toestelIDs.Add((int)reader["id"]);
                    }
                    return toestelIDs;
                }
            } catch (Exception ex) {
                throw new ToestelRepoADOException("GeefToestelIDsZonderOpenstaandeReservaties", ex);
            } finally {
                conn.Close();
            }
        }
    }
}
