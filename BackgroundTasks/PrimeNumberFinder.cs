using System;
using System.Collections.Generic;
using System.Text;
using Interfaces;

namespace BackgroundTasks
{
    public class PrimeNumberFinder
    {
        private readonly IDataAccessLayer _dal;
        public PrimeNumberFinder(IDataAccessLayer dal)
        {
            _dal = dal;
        }

        private static bool IsPrime(long numbertocheck)
        {
            if (numbertocheck == 1) return false;
            if (numbertocheck == 2) return true;

            var limit = Math.Ceiling(Math.Sqrt(numbertocheck));

            for (var i = 2; i <= limit; i++)
            {
                if (numbertocheck % i == 0) return false;
            }
            return true;
        }

        public bool ShouldSearch { get; set; }

        public void FindPrimeNumbers()
        {
            while (ShouldSearch)
            {
                var numbertoCheck = _dal.LargestTestedNumber + 1;
                if (IsPrime(numbertoCheck))
                {
                    _dal.AddNewPrimeNumber(numbertoCheck);
                }
                _dal.LargestTestedNumber++;
            }
        }
    }
}
