using System;
using System.Collections.Generic;
using System.Text;

namespace Interfaces
{
    public interface IDataAccessLayer
    {
        bool? IsPrime(long numberToTest);
        long LargestTestedNumber { get; set; }
        void AddNewPrimeNumber(long newPrime);
    }  
}
