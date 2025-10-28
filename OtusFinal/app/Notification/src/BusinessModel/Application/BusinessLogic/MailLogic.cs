using Application.Interfaces;
using Entity;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.BusinessLogic
{
    public class MailLogic:IMailLogic
    {
        readonly IEFStore _eFStore;
        public MailLogic(IEFStore eFStore)
        {
            _eFStore = eFStore;
        }

        public async Task LogMailAsync(Mail mail)
        {
            CancellationTokenSource cancelTokenSource = new CancellationTokenSource();
            CancellationToken cancel = cancelTokenSource.Token;
            await _eFStore.AddMail(mail, cancel);
        }
    }
}
