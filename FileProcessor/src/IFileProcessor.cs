using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataModel;
namespace FileProcessor
{
    public interface IFileProcessor
    {
        List<Employeer> ReadFiles(List<string> files);
        Task<List<Employeer>> ReadFilesAsyn(List<string> files);
        List<Employeer> ReadFilesParalel(List<string> files);

        FileType FileValidate(string filename);
    }
}
