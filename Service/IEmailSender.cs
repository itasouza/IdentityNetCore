using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityNetCore.Service
{
    public interface IEmailSender
    {
        Task EnviarEmail(string email, string assunto, string mensagem);
    }
}
