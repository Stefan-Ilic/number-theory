using System;
using System.Collections.Generic;
using System.Text;
using Interfaces;

namespace Tests
{
    internal class MockDal : IDataAccessLayer
    {
        private readonly List<long> _primeNumbers = new List<long> { 2, 3, 5, 7, 11, 13, 17, 19, 23 };

        public bool? IsPrime(long numberToTest)
        {
            if (numberToTest > LargestTestedNumber)
            {
                return null;
            }

            return _primeNumbers.Contains(numberToTest);
        }

        public long LargestTestedNumber { get; set; } = 1500;
        public void AddNewPrimeNumber(long newPrime)
        {
            _primeNumbers.Add(newPrime);
        }
    }
}
