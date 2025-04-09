using CsvHelper;
using CsvHelper.Configuration;
using System;
using System.Globalization;
using System.IO;

namespace SalesApplication.Core.Helper
{
  
public class DecimalEmptyValueConverter : CsvHelper.TypeConversion.DecimalConverter
    {
        public override object ConvertFromString(string text, IReaderRow row, MemberMapData memberMapData)
        {
            if (string.IsNullOrWhiteSpace(text))
            {
                return 0m; // Convert empty or null to 0
            }

            return base.ConvertFromString(text, row, memberMapData); // Use base conversion otherwise
        }
    }

   
}
