using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GestureRecognition.Data.DataProvider
{
    public partial class DataProvider
    {
        private Entities _dbStore;

        public DataProvider()
        {
            _dbStore = new Entities();
        }
    }
}
