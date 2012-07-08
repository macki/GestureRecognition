using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GestureRecognition.Data
{
    interface IDataContext
    {
        int SaveChange();
        T Attach<T>(T entity) where T : class;
        T Add<T>(T entity) where T : class;
        T Delete<T>(T entity) where T : class;
    }
}
