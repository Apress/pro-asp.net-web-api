using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AspNetWebApi.DependencyResolution
{
    public interface ITaxCalculator
    {
        decimal Calculate(decimal amount);
    }
}
