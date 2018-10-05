using System;
using System.Collections.Generic;
using System.Text;
using Interfaces;

namespace DTOs
{
    public class Field : IField
    {
        public Field(Set set)
        {
            Set = set;
        }

        public Set Set { get; }
    }
}
