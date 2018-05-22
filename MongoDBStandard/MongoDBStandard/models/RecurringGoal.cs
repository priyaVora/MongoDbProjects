using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MongoDBStandard.models
{
    public class RecurringGoal : Goal
    {
        public TimeSpan Frequency { get; set; }
    }
}
