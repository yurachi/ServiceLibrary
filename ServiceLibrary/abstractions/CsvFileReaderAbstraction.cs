using System;
using System.IO;
using System.Text;
using LumenWorks.Framework.IO.Csv;

namespace ServiceLibrary.abstractions
{
    public class CsvFileReaderAbstraction : CsvReader
    {
        private StreamReader m_csvStream;

        public CsvFileReaderAbstraction(StreamReader reader) : base(reader, true)
        {
            m_csvStream = reader;
        }

        public static CsvFileReaderAbstraction Create(string filename) 
        {
            var csvFile = File.Open(filename, FileMode.Open, FileAccess.Read, FileShare.Read); //TODO: make an instance aware whether the file should be closed
            var csvStream = new StreamReader(csvFile, Encoding.UTF8, false);
            return new CsvFileReaderAbstraction(csvStream);
        }

        public new void Dispose()
        {
            m_csvStream.Close();
            base.Dispose();
        }
    }
}
