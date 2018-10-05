using System;
using System.Collections.Generic;
using System.Text;

namespace Interfaces
{
    public interface IPolynomial
    {
        IField Field { get; }
        int Degree { get; }
        string HexadecimalRepresentation { get; }
        string BinaryRepresentation { get; }
    }
}
