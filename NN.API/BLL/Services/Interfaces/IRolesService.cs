﻿using DataModels.Core;
using DataModels.Services.Auth;
using DataModels.Services.Roles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services.Interfaces
{
    public interface IRolesService
    {
        CreateRoleOutput CreateRole(CreateRoleInput input);
        
    }
}
