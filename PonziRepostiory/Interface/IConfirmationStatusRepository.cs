﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PonziRepostiory.Interface
{
    
    public interface IConfirmationStatusRepository : IRepository<ConfirmationStatus>
    {
        IEnumerable<ConfirmationStatus> GetAllConfirmationStatus();
    }
}