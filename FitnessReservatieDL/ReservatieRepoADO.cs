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

        public List<ReservatieDetail> GeefToekomstigeReservatieDetais(int klantnummer) {
            SqlConnection conn = GetConnection();
            string query = "SELECT d.*,t.toestel,tt.Beginuur,tt.Einduur " +
                "FROM ReservatieDetail d " +
                "LEFT JOIN Reservatie r ON d.reservatieID=r.id " +
                "LEFT JOIN Toestel t ON t.id=d.toestelID " +
                "LEFT JOIN Tijdslot tt ON tt.ID=d.tijdslotID  " +
                "LEFT JOIN Klant k ON k.id = r.klantnummer " +
                "WHERE r.klantnummer=@klantnummer AND DATEADD(DAY, -1, GETDATE())<d.datum";
            List<ReservatieDetail> details = new List<ReservatieDetail>();
            try {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand()) {
                    cmd.CommandText = query;
                    cmd.Parameters.AddWithValue("@klantnummer", klantnummer);

                    IDataReader reader = cmd.ExecuteReader();
                    while (reader.Read()) {
                        DateTime datum = (DateTime)reader["datum"];
                        Tijdslot tijdslot = new Tijdslot((int)reader["tijdslotID"], (TimeSpan)reader["Beginuur"], (TimeSpan)reader["Einduur"]);
                        Toestel toestel = new Toestel((string)reader["toestel"], true);
                        toestel.ZetId((int)reader["toestelID"]);
                        ReservatieDetail detail = new ReservatieDetail(datum, tijdslot, toestel);
                        details.Add(detail);
                    }
                }
                return details;
            } catch (Exception ex) {
                throw new ReservatieRepoADOException("GeefToekomstigeReservatieDetails", ex);
            }
            finally {
                conn.Close();
            }

        }
        public void SchrijfReservatieInDB(Reservatie reservatie) {
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
                }

            } catch (Exception ex) {
                tran.Rollback();
                throw new ReservatieRepoADOException("SchrijfReservatieInDB", ex);
            }
            finally {
                conn.Close();
            }
        }

        public List<Tijdslot> GeefTijdsloten() {
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
                    return tijdsloten;
                }

            } catch (Exception ex) {
                throw new ReservatieRepoADOException("GeefTijdsloten", ex);
            }
            finally {
                conn.Close();
            }
        }

    }
}
