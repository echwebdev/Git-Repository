﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModels.Core
{
    [Serializable]
    public class KeyValueModel
    {
        public long Key { get; set; }
        public string Value { get; set; }
    }
}
