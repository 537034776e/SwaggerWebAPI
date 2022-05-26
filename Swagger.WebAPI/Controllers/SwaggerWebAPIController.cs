using Swagger.ORM.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;
using Swagger.Interfaces.Interfaces;
using Swagger.InterfacesImplementation.InterfacesImplementation;

namespace SwaggerWebAPI.Controllers
{
    public class SwaggerWebAPIController : ApiController
    {
        private UtenteInterface _utenteInterface = null;

        public SwaggerWebAPIController(UtenteInterface utenteInterface)
        {
            this._utenteInterface = utenteInterface;
        }


        [Route("api/Calcolatrice/{operazione}/{numero1}/{numero2}")]
        [HttpGet]
        public IHttpActionResult Calculator2(int numero1, int numero2, string operazione)
        {
            int calcolo;
            switch (operazione)
            {
                case "Addizione":
                    calcolo = numero1 + numero2;
                    return Ok("Numero1: " + numero1 + ", Numero2: " + numero2 + ", Operazione eseguita: " + operazione + ", Risultato: " + calcolo);
                case "Sottrazione":

                    if (numero2 < numero1)
                    {
                        calcolo = numero1 - numero2;
                        return Ok("Numero1: " + numero1 + ", Numero2: " + numero2 + ", Operazione eseguita: " + operazione+", Risultato: "+calcolo);
                    }
                    else
                    {
                        var response = new HttpResponseMessage(HttpStatusCode.BadRequest)
                        {
                            Content = new StringContent(string.Format("Impossibile effettuare la sottrazione.")),
                            ReasonPhrase = "Il risultato non può essere negativo."
                        };

                        throw new HttpResponseException(response);
                    }
                case "Divisione":

                    if (numero2 != 0)
                    {
                        calcolo = numero1 / numero2;
                        return Ok("Numero1: " + numero1 + ", Numero2: " + numero2 + ", Operazione eseguita: " + operazione + ", Risultato: " + calcolo);
                    }
                    else
                    {
                        var response = new HttpResponseMessage(HttpStatusCode.BadRequest)
                        {
                            Content = new StringContent(string.Format("Impossibile effettuare la divisione.")),
                            ReasonPhrase = "Il divisore non può essere zero."
                        };

                        throw new HttpResponseException(response);
                    }
                  
                case "Moltiplicazione":
                    calcolo = numero1 * numero2;
                    return Ok("Numero1: " + numero1 + ", Numero2: " + numero2 + ", Operazione eseguita: " + operazione + ", Risultato: " + calcolo);
            }

            return Ok("Valore non valido per il campo operazione ("+operazione+")");

        }

        [Route("api/VerificaPassword/{utente}/{password}/")]
        [HttpGet]
        public IHttpActionResult VerificaPassword(string password,int utente)
        {
            List<Utenti> listaUtenti = this._utenteInterface.GetListaUtenti();

            if (listaUtenti.Where(x=>x.password==password && x.utente==utente).ToList().Count>0)
            {
                return Ok("Esiste l'utente con la password indicata.");
            }
            else
            {
                return Ok("Utente non presente.");
            }


        }

        [Route("api/RegistraUtente/{password}/")]
        [HttpPost]
        public IHttpActionResult RegistraUtente(string password)
        {
            Utenti utente = this._utenteInterface.RegistraUtente(password);

            if (utente == null)
            {
                return Ok("Impossibile registrare l'utente. Esiste un altro utente con la password indicata.");
            }
            else
            {
                return Ok("Registrazione effettuata.");
            }


        }

    }
}
