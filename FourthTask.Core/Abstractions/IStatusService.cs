using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FourthTask.Core.Abstractions
{
    public interface IStatusService
    {
        public Task<int> GetStatusByName(string name);
    }
}
