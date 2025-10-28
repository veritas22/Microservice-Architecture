using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Broker
{
    public interface ISendMessage
    {
        Task PublishMessage(string text);

    }
}
