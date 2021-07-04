using Microsoft.EntityFrameworkCore;
using Revifast.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace revifast.test.UnitTesting
{
    public class GeneralInfo
    {
        protected DbRevifastContext BuildContext(string DBName) {

            var opciones = new DbContextOptionsBuilder<DbRevifastContext>()
                    .UseInMemoryDatabase(DBName).Options;

            var dbContext = new DbRevifastContext(opciones);
            return dbContext;
        
        }

    }
}
