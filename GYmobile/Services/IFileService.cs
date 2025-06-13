using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GYmobile.Services
{
    public interface IFileService
    {
        Task<string> SaveFileAsync(Stream stream, string fileName, bool setRandomName = true);
    }
}
