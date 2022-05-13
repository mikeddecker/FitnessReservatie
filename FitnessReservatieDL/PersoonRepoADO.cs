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
            SqlConnection connection = GetConnection();
            string query = "SELECT * FROM dbo.Klant WHERE email=@email;";
            try {
                using (SqlCommand command = connection.CreateCommand()) {
                    connection.Open();
                    command.Parameters.AddWithValue("@email", email);
                    command.CommandText = query;

                    Persoon persoon = null; // email is uniek, dus er komt sowieso slechts 1 persoon uit.
                    IDataReader reader = command.ExecuteReader();
                    while (reader.Read()) {
                        int id = (int)reader["id"];
                        string voornaam = (string)reader["voornaam"];
                        string achternaam = (string)reader["achternaam"];
                        bool isAdmin = (bool)reader["isAdmin"];
                        if (isAdmin) {
                            persoon = new Admin(id, voornaam, achternaam, email);
                        } else {
                            persoon = new Klant(id, voornaam, achternaam, email);
                        }
                    }
                    return persoon;
                }
            } catch (Exception ex) {
                throw new PersoonRepoADOException("SelecteerPersoon", ex);
            }
            finally {
                connection.Close();
            }
        }
    }
}
