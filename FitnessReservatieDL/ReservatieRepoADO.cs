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
    public class ReservatieRepoADO : IReservatieRepository {
        private string connectieString;
        public ReservatieRepoADO(string connectieString) {
            this.connectieString = connectieString;
        }

        private SqlConnection GetConnection() {
            return new SqlConnection(connectieString);
        }
        public List<Reservatie> GeefReservaties() {
            SqlConnection conn = GetConnection();
            string query = "SELECT r.id, d.datum, r.tijdslotID, t.Beginuur, t.Einduur, r.klantnummer, k.voornaam, k.achternaam, k.email, k.isAdmin, r.toestelID, tt.toestel FROM dbo.Reservatie r" +
                            " LEFT JOIN dbo.ReservatieDetail d ON r.id = d.reservatieID" +
                            " LEFT JOIN dbo.Tijdslot t ON d.tijdslotID = t.ID" +
                            " LEFT JOIN dbo.Klant k ON r.klantnummer = k.id" +
                            " LEFT JOIN dbo.Toestel tt ON d.toestelID = tt.id" +
                            " WHERE d.datum >= DATEADD(day, -1, GETDATE())" +
                            " ORDER BY d.datum,t.Beginuur;";

            List<Reservatie> reservaties = new List<Reservatie>();
            conn.Open();
            try {
                using (SqlCommand cmd = conn.CreateCommand()) {
                    cmd.CommandText = query;
                    IDataReader reader = cmd.ExecuteReader();
                    while (reader.Read()) {
                        int reservatieID = (int)reader["id"];
                        Klant k = new Klant((int)reader["klantnummer"], (string)reader["voornaam"], (string)reader["achternaam"], (string)reader["email"]);
                        Reservatie reservatie = new Reservatie(k);
                        reservatie.ZetReservatieID(reservatieID);
                        if (reservaties.Contains(reservatie)) {
                            //reservaties;
                        }
                    }
                }
                return reservaties;
            } catch (Exception ex) {
                throw new ReservatieRepoADOException("GeefReservaties", ex);
            }
            finally {
                conn.Close();
            }
            //return null;

        }
        public Reservatie SchrijfReservatieInDB(Reservatie reservatie) {
            SqlConnection conn = GetConnection();
            string queryReservatie = "INSERT INTO dbo.Reservatie(klantnummer) "
                + "output INSERTED.ID VALUES(@klantnummer);";
            string queryDetail = "INSERT INTO dbo.ReservatieDetail(datum,tijdslotID,toestelID,reservatieID) " +
                " VALUES(@datum,@tijdslotID,@toestelID,@reservatieID);";

            conn.Open();
            SqlTransaction tran = conn.BeginTransaction();
            try {
                using (SqlCommand reservatieCommand = conn.CreateCommand()) {
                    reservatieCommand.Transaction = tran;
                    reservatieCommand.CommandText = queryReservatie;
                    reservatieCommand.Parameters.AddWithValue("@klantnummer", reservatie.Klant.ID);
                    int reservatieID = (int)reservatieCommand.ExecuteScalar();
                    reservatie.ZetReservatieID(reservatieID);

                    foreach (ReservatieDetail detail in reservatie.ReservatieDetails) {
                        using (SqlCommand detailCommand = conn.CreateCommand()) {
                            detailCommand.Transaction = tran;
                            detailCommand.CommandText = queryDetail;
                            detailCommand.Parameters.AddWithValue("@datum", detail.Datum);
                            detailCommand.Parameters.AddWithValue("@tijdslotID", detail.Tijdslot.TijdslotID);
                            detailCommand.Parameters.AddWithValue("@toestelID", detail.Toestel.ToestelID);
                            detailCommand.Parameters.AddWithValue("@reservatieID", reservatieID);
                            detailCommand.ExecuteNonQuery();
                        }
                    }

                    tran.Commit();
                    return reservatie;
                }

            } catch (Exception ex) {
                tran.Rollback();
                throw new ReservatieRepoADOException("SchrijfReservatieInDB", ex);
            }
            finally {
                conn.Close();
            }
        }

    }
}
