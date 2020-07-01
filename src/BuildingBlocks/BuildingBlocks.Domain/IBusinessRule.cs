﻿using System;
using System.Collections.Generic;
using System.Text;

namespace BuildingBlocks.Domain
{
    public interface IBusinessRule
    {
        bool IsValid();
        string Message { get; }
    }
}
