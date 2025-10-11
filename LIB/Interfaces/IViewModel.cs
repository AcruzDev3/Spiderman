using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LIB.Interfaces
{
    public interface IViewModel<T>
    {
       void Create(T model);
    }
}
