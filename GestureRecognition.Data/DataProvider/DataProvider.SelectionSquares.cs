using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GestureRecognition.Data.Models;

namespace GestureRecognition.Data.DataProvider
{
    public partial class DataProvider
    {
        public void Save(SelectionSquares obj)
        {
            _dbStore.SelectionsSquares.Add(obj);
            _dbStore.SaveChange();
        }

        public IEnumerable<SelectionSquares> GetSelectionSquares()
        {
            var sq = from s in _dbStore.SelectionsSquares
                     select s;
            return sq;
        }
    }
}
