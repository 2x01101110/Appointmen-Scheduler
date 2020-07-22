using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Application
{
    public static class Assembly
    {
        public static readonly System.Reflection.Assembly Application = typeof(Assembly).Assembly;
    }
}
