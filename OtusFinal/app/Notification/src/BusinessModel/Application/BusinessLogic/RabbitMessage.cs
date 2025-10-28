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
            var text = "";


            if (true)
            {
                text = "Руслан";
            }
            else 
            {
                text = "Габбазов";
            }


           await _sendMessage.PublishMessage(text + name);
            
            return "отправили сообщение";
        }

    }
}
