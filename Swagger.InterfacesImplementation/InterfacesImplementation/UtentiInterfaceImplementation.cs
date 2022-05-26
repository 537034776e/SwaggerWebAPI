using Swagger.Interfaces.Interfaces;
using Swagger.ORM.Data;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Swagger.InterfacesImplementation.InterfacesImplementation
{
    public class UtentiInterfaceImplementation : UtenteInterface
    {
        public List<Utenti> GetListaUtenti()
        {
            List<Utenti> listUtenti = new List<Utenti>();

            try
            {
                using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["TestSwagger"].ConnectionString.ToString()))
                {
                    using (SqlCommand cmd = new SqlCommand("GetListaUtenti", con))
                    {


                        cmd.CommandType = CommandType.StoredProcedure;
                        con.Open();
                        using (SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                        {
                            while (reader.Read())
                            {
                                listUtenti.Add(PopulateUtenti(reader));
                            }
                        }
                        con.Close();
                    }
                }

            }
            catch (Exception ex)
            {
            }
            finally
            {

            }

            return listUtenti;
        }

        public Utenti RegistraUtente(string password)
        {
            Utenti utente;
            int idutente = 0;
            try
            {
                using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["TestSwagger"].ConnectionString.ToString()))
                {
                    using (SqlCommand cmd = new SqlCommand("AddUtente", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add("@Password", SqlDbType.VarChar).Value = password;


                        cmd.Parameters.Add(new SqlParameter("@IdUtente", SqlDbType.Int));
                        cmd.Parameters["@IdUtente"].Direction = ParameterDirection.Output;

                        con.Open();
                        cmd.ExecuteNonQuery();
                        con.Close();

                        idutente = Convert.ToInt32(cmd.Parameters["@IdUtente"].Value);
                    }
                }
            }
            catch (Exception ex)
            {
            }
            finally
            {
                if (idutente != 0)
                {
                    utente = new Utenti()
                    {
                        utente = idutente,
                        password = password
                    };
                }
                else
                {
                    utente = null;
                }
            }
            return utente;

        }


        private Utenti PopulateUtenti(SqlDataReader reader)
        {
            Utenti utente = new Utenti();

            try
            {


                utente.utente = ((int)reader.GetValue(reader.GetOrdinal("utente")));
                utente.password = reader.GetValue(reader.GetOrdinal("password")).ToString();
               

            }
            catch (Exception ex)
            {

            }
            finally
            {

            }
            return utente;
        }
    }
}

