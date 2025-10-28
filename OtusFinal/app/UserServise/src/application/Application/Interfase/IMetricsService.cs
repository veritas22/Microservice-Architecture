using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfase
{
    public interface IMetricsService
    {
        void RecordUserClick();
        void RecordResponseTime(double value);
        void RecordRequest();
        void RecordMemoryConsumption(double value);
    }
}
