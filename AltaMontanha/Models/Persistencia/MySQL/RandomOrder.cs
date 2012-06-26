using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NHibernate.Criterion;
using NHibernate.SqlCommand;
using NHibernate;

namespace AltaMontanha.Models.Persistencia.MySQL
{
    public class RandomOrder : Order
    {
        public RandomOrder() : base("", true) { }
        public override SqlString ToSqlString(
            ICriteria criteria, ICriteriaQuery criteriaQuery)
        {
            return new SqlString("RAND()");
        }
    }  
}