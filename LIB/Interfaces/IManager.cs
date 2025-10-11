using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LIB.Interfaces
{
    internal interface IManager<T>
    {
        Task<T> GetOne(int id);

        Task<List<T>> GetAll();

        Task<int> Create(T viewModel);

        Task<int> Delete(int id);

        Task<int> Exists(T viewModel);
    }
}
