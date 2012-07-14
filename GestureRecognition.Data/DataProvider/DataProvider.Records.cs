using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GestureRecognition.Data.Models;

namespace GestureRecognition.Data.DataProvider
{
    public partial class DataProvider
    {
        public IEnumerable<Records> GetRecords()
        {
            var records = from r in _dbStore.Records
                          select r;
            return records.ToList();
        }

        public int AddNewRecord(string recordUrl, bool isRgb)
        {
            var newRecord = new Records { AbsolutePath = recordUrl, IsRgb = isRgb };
            _dbStore.Records.Add(newRecord);
            return _dbStore.SaveChanges();
        }
    }
}
