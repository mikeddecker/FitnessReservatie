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

        public Persoon SelecteerPersoon(string email, int? id) {
            SqlConnection conn = GetConnection();
            string klantQuery = "SELECT * FROM Klant";
            string emailQueryEinde = " WHERE email=@email";
            string idQueryEinde = " WHERE id=@id";


            Persoon persoon = null; // email is uniek, dus er komt sowieso slechts 1 persoon uit.

            try {
                conn.Open();
                using (SqlCommand klantcmd = conn.CreateCommand()) {
                    // email of id query aanpassen en parameters toevoegen
                    if (id == null) {
                        klantQuery += emailQueryEinde;
                        klantcmd.Parameters.AddWithValue("@email", email);
                    } else {
                        klantQuery += idQueryEinde;
                        klantcmd.Parameters.AddWithValue("@id", id);
                    }
                    klantcmd.CommandText = klantQuery;

                    // Persoon (meestal klant) ophalen
                    IDataReader reader = klantcmd.ExecuteReader();
                    while (reader.Read()) {
                        int idKlant = (int)reader["id"];
                        string voornaam = (string)reader["voornaam"];
                        string achternaam = (string)reader["achternaam"];
                        bool isAdmin = (bool)reader["isAdmin"];
                        string emailKlant = (string)reader["email"];
                        if (isAdmin) {
                            persoon = new Admin(idKlant, voornaam, achternaam, emailKlant);
                        } else {
                            persoon = new Klant(idKlant, voornaam, achternaam, emailKlant);
                        }
                    }


                }
                return persoon;
            } catch (Exception ex) {
                throw new PersoonRepoADOException("SelecteerPersoon", ex);
            }
            finally {
                conn.Close();
            }
        }
    }
}
