using System;
using System.Collections.Generic;
using System.Text;

namespace Interfaces
{
    public interface IGroup<out T>
    {
        int Order { get; }
        bool IsCyclic { get; }
        bool IsSymmetric { get; }
        IEnumerable<T> Generators { get; }
        IEnumerable<IGroup<T>> SubGroups { get; }
        Operation Operation { get; }
        IEnumerable<T> Elements { get; }
    }
}