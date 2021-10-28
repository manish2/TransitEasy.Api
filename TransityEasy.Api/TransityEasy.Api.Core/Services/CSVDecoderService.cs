using CsvHelper;
using CsvHelper.Configuration;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;

namespace TransityEasy.Api.Core.Services
{
    public class CSVDecoderService : IDisposable, ICSVDecoderService
    {
        private readonly CsvReader _csvReader; 
        public CSVDecoderService(StreamReader reader)
        {
            CsvConfiguration configuration = new (CultureInfo.InvariantCulture) {HasHeaderRecord = true };

            _csvReader = new CsvReader(reader, configuration);
        }
        public IEnumerable<T> GetRecords<T>() => _csvReader.GetRecords<T>(); 

        public void Dispose()
        {
            _csvReader.Dispose();
        }
    }
}
