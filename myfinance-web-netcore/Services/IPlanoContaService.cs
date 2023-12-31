using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using myfinance_web_netcore.Domain;
using myfinance_web_netcore.Models;

namespace myfinance_web_netcore.Services
{
    public interface IPlanoContaService
    {
         IEnumerable<PlanoContaModel> ListarPlanoContas();

         void Salvar(PlanoContaModel model);

         PlanoContaModel RetornarRegistro(int id);

         void Excluir(int id);
    }
}

