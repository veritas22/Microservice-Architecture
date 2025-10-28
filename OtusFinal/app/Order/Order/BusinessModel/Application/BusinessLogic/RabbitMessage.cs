using Application.Interfaces.Broker;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace Application.BusinessLogic
{
    class RabbitMessage : ILogicMessage
    {
        ISendMessage _sendMessage;
        public RabbitMessage(ISendMessage sendMessage)
        {
            _sendMessage = sendMessage;
        }
        public async Task<string> SendMessageLogic(string name) 
        {
            var text = "Успешно оплачен";
            
            return "отправили сообщение";
        }

    }
}
