using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Interface
{
    public interface IOCRService
    {
        public Task<string> ExtractReceiptDataAsync(Stream fileStream, string tessDataPath);
    }
}
