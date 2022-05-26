using Swagger.ORM.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Swagger.Interfaces.Interfaces
{
    public interface UtenteInterface
    {
        List<Utenti> GetListaUtenti();
        Utenti RegistraUtente(string password);

    }
}
