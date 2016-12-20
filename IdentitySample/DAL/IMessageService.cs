using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentitySample.DAL
{
    interface IMessageService
    {
        Task SendAsync(MailOptions message);
    }
}
