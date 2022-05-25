using FitnessReservatieBL.Domein;
using FitnessReservatieBL.Exceptions;
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
    public class PersoonRepoADO : IPersoonRepository {
        private string connectieString;
        public PersoonRepoADO(string connectieString) {
            this.connectieString = connectieString;
        }
        private SqlConnection GetConnection() {
            return new SqlConnection(connectieString);
        }

        public bool BestaatPersoon(string email) {
            SqlConnection connection = GetConnection();
            string query = "SELECT COUNT(*) FROM dbo.Klant WHERE email=@email;";
            try {
                using (SqlCommand command = connection.CreateCommand()) {
                    connection.Open();
                    command.Parameters.AddWithValue("@email", email);
                    command.CommandText = query;
                    int n = (int)command.ExecuteScalar();
                    if (n > 0) { return true; } else { return false; }
                }
            } catch (Exception ex) {
                throw new PersoonRepoADOException("BestaatKlant", ex);
            }
            finally {
                connection.Close();
            }
        }

        public Persoon SelecteerPersoon(string email) {
            SqlConnection conn = GetConnection();
            string query = "SELECT * FROM dbo.Klant WHERE email=@email;";

            //optioneel als het klant is (meeste gevallen)
            string toekomstigeReservatiesKlantQuery = "SELECT d.datum, d.tijdslotID, t.Beginuur, t.Einduur, d.toestelID, tt.toestel, d.reservatieID " +
                "FROM ReservatieDetail d " +
                "LEFT JOIN Reservatie r ON d.reservatieID = r.id " +
                "LEFT JOIN Toestel tt ON tt.id=d.toestelID " +
                "LEFT JOIN Tijdslot t ON d.tijdslotID=t.ID " +
                "WHERE datum>DATEADD(DAY, -1, GETDATE()) AND klantnummer=3 ORDER BY d.reservatieID";

            conn.Open();
            SqlTransaction tran = conn.BeginTransaction();
            try {
                using (SqlCommand cmd = conn.CreateCommand()) {
                    cmd.Transaction = tran;
                    cmd.Parameters.AddWithValue("@email", email);
                    cmd.CommandText = query;

                    Persoon persoon = null; // email is uniek, dus er komt sowieso slechts 1 persoon uit.
                    IDataReader reader = cmd.ExecuteReader();
                    while (reader.Read()) { // email is uniek, dus de loop wordt slechts 1 keer doorlopen.
                        int id = (int)reader["id"];
                        string voornaam = (string)reader["voornaam"];
                        string achternaam = (string)reader["achternaam"];
                        bool isAdmin = (bool)reader["isAdmin"];
                        if (isAdmin) {
                            persoon = new Admin(id, voornaam, achternaam, email);
                        } else {
                            persoon = new Klant(id, voornaam, achternaam, email);

                            // ophalen van toekomstige klantreservaties.
                            using (SqlCommand reservatieCommand = conn.CreateCommand()) {
                                reservatieCommand.Transaction = tran;
                                reservatieCommand.CommandText = toekomstigeReservatiesKlantQuery;
                                reservatieCommand.Parameters.AddWithValue("@klantnummer", id);
                                IDataReader reservatieReader = reservatieCommand.ExecuteReader();

                                while (reservatieReader.Read()) {
                                    // nieuwe lijn inlezen
                                    // ik lees reservatieDetai = sowieso nieuw
                                    DateTime datum = (DateTime)reservatieReader["datum"];
                                    Tijdslot tijdslot = new Tijdslot((int)reservatieReader["tijdslotID"], (TimeSpan)reservatieReader["Beginuur"], (TimeSpan)reservatieReader["Einduur"]);
                                    Toestel toestel = new Toestel((string)reservatieReader["toestel"], true);
                                    ReservatieDetail detail = new ReservatieDetail(datum, tijdslot, toestel);

                                    // de vraag: nieuw reservatieID of niet?
                                    int reservatieID = (int)reader["reservatieID"];
                                    if (((Klant)persoon).BevatReservatie(reservatieID)) {
                                        Reservatie nieuweReservatieVoorKlantobject = ((Klant)persoon).GeefReservatie(reservatieID);
                                        nieuweReservatieVoorKlantobject.VoegReservatieDetailToe(detail);
                                        ((Klant)persoon).VoegReservatieToe(nieuweReservatieVoorKlantobject);
                                    } else {
                                        Reservatie reservatie = new Reservatie((Klant)persoon);
                                        reservatie.ZetReservatieID(id);
                                    }
                                }
                            }
                        }
                    }
                    return persoon;
                }
            } catch (Exception ex) {
                tran.Rollback();
                throw new PersoonRepoADOException("SelecteerPersoon", ex);
            }
            finally {
                conn.Close();
            }
        }
    }
}
