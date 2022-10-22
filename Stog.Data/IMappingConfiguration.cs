﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stog.Data
{
    /// <summary>
    /// Represents database context model mapping configuration.
    /// </summary>
    public interface IMappingConfiguration
    {
        int Order { get; set; }

        /// <summary>
        /// Apply this mapping configuration.
        /// </summary>
        /// <param name="modelBuilder">The builder being used to construct the model for the database context.</param>
        void ApplyConfiguration(ModelBuilder modelBuilder);
    }
}
