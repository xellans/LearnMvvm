﻿using DataBase;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Inerfaces
{
    public interface IRepository<T>
    {
        ReadOnlyObservableCollection<People> GetPeopleCollection();
        ReadOnlyObservableCollection<Product> GetProductCollection();

        bool Any<T>(Func<T, bool> predicate) where T : class;
        T? Remove(T t);
        T? Clone(T t);
        T? Add(T t);
    }
}