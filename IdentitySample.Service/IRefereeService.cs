﻿using IdentitySample.Entities;
using System.Collections.Generic;

namespace IdentitySample.Service
{
    public interface IRefereeService : IEntityService<Referee>
    {
        Referee GetById(int Id);
    }
}
