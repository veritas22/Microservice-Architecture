using Domen;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Broker
{
    public interface ISendMessage
    {

        Task PublishMessage(int userID,float price, string queueMes, string text, bool status);
    }
}
