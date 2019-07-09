using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.Reflection;
using DataModel;

namespace FileProcessor
{
    public class TextFileProcessor : IFileProcessor
    {
        public TextFileProcessor() { }

        public FileType FileValidate(string filename)
        {
            using (StreamReader sr = File.OpenText(filename))
            {
                string firstline = sr.ReadLine();
                if (firstline == null) return FileType.UNDEFINE;

                if (Regex.Matches(firstline, PatternLibrary.commaPattern).Count > 1) return FileType.COMMA;
                if (Regex.Matches(firstline, PatternLibrary.hyphenPattern).Count > 1) return FileType.HYPHEN;
                if (Regex.Matches(firstline, PatternLibrary.hashPattern).Count > 1) return FileType.HASH;
            }

            return FileType.UNDEFINE;
        }

        public List<Employeer> ReadFiles(List<string> files)
        {

            List<Employeer> lstEmployeer = new List<Employeer>();

            try
            {
                foreach (string file in files)
                {
                    //check file exist
                    if (!File.Exists(file)) continue;

                    char[] separator= new char[1] ;

                    //file format validation
                    FileType filetype = FileValidate(file);

                    switch (filetype)
                    {
                        case FileType.COMMA:
                              separator[0] =  ',' ;
                            break;

                        case FileType.HYPHEN:
                            separator[0] = '-';
                            break;

                        case FileType.HASH:
                            separator[0] = '#';
                            break;

                        case FileType.UNDEFINE:
                            continue;
                    }

                    // Open file and read raw data
                    using (StreamReader sr = File.OpenText(file))
                    {
                        string rawData = "";
                        while ((rawData = sr.ReadLine()) != null)
                        {
                            // Map raw data with Employeer object
                            Employeer employeer = MapEmployeerObj(separator, rawData);
                            lstEmployeer.Add(employeer);
                        }

                        //write file import Log with ILog for each file
                    }
                }

                return lstEmployeer;
            }
            catch (Exception ex)
            {
                //write error Log with ILog
                throw ex;
            }
            finally
            {
                files.ForEach(f => { if (File.Exists(f)) File.Delete(f); });
            }
        }

        /// <summary>
        /// Read file Async
        /// </summary>
        /// <param name="files"></param>
        /// <returns></returns>
        public async Task<List<Employeer>> ReadFilesAsyn(List<string> files)
        {

            List<Employeer> lstEmployeer = new List<Employeer>();

            await Task.Run(() =>
            {
                lstEmployeer =  ReadFiles(files);
            });

            return lstEmployeer;
        }

        /// <summary>
        /// Read files Paralel
        /// </summary>
        /// <param name="files"></param>
        /// <returns></returns>
        public List<Employeer> ReadFilesParalel(List<string> files)
        {
            throw new NotImplementedException();
        }

        private Employeer MapEmployeerObj(char[] separator, string rawData)
        {
            Type type = typeof(Employeer);
            PropertyInfo[] properties = type.GetProperties();
            Employeer employeer = new Employeer();
            int NumberOfFields = properties.Length;

       
            for (int i = 0; i < properties.Length; i++)
            {
                PropertyInfo property = properties[i];
                List<dynamic> attributes = property.GetCustomAttributes(true).ToList();
                string patternEx = attributes[0].Pattern;
                int minlength = attributes[1] == null ? 0 : attributes[1].Length;

                string[] fields = rawData.Split(separator, NumberOfFields);

                foreach (string field in fields)
                {
                    string propertyValue = field.Trim();
                    if (Regex.IsMatch(propertyValue, patternEx) && propertyValue.Length >= minlength)
                    {
                        property.SetValue(employeer, propertyValue);
                        continue;
                    }      
                }
            }

            return employeer;
        }
    }
}
