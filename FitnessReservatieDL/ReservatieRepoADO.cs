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

        public List<Toestel> GeefMogelijkeToestellen(DateTime datum, Tijdslot tijdslot) {
            SqlConnection conn = GetConnection();
            string query = "SELECT id, toestel FROM dbo.Toestel WHERE verwijderd ='False' AND beschikbaar = 'True' AND id NOT IN (SELECT toestelID FROM dbo.ReservatieDetail WHERE (datum=@datum AND tijdslotID=@tijdslotID));";

            List<Toestel> toestellen = new List<Toestel>();
            conn.Open();
            try {
                using (SqlCommand cmd = conn.CreateCommand()) {
                    cmd.CommandText = query;
                    cmd.Parameters.AddWithValue("@datum", datum);
                    cmd.Parameters.AddWithValue("@tijdslotID", tijdslot.TijdslotID);
                    IDataReader reader = cmd.ExecuteReader();
                    while (reader.Read()) {
                        Toestel t = new Toestel((string)reader["toestel"], true);
                        t.ZetId((int)reader["id"]);
                        toestellen.Add(t);
                    }
                }
                return toestellen;
            } catch (Exception ex) {
                throw new ReservatieRepoADOException("GeefMogelijkeToestellen", ex);
            }
            finally {
                conn.Close();
            }
        }

        public List<ReservatieDetail> GeefToekomstigeReservatieDetais(int klantnummer) {

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
