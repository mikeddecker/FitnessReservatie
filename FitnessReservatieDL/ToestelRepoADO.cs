using FitnessReservatieBL.Domein;
using FitnessReservatieBL.Interfaces;
using FitnessReservatieDL.Exceptions;
using System;
using System.Collections.Generic;
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
            string query = "UPDATE dbo.Toestel SET verwijderd='true' WHERE id=@id";
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
            throw new NotImplementedException();
        }

    }
}
