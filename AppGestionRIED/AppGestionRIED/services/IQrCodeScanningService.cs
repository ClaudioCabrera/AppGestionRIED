using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AppGestionRIED.services
{
    public interface IQrCodeScanningService
    {
        Task<string> ScanAsync();
    }
}
