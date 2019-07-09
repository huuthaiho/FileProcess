using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileProcessor.src
{
    public interface ILog
    {
        void LogWriter(string logMessage);
    }
}
