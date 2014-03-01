using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate;
using NHibernate.Criterion;

namespace Padel.Infrastructure.Utilities.Extensions
{
    public static class IQueryOverExtensions
    {
        public static IQueryOver<troot, tsubtype> Or<troot, tsubtype>(this IQueryOver<troot, tsubtype> input, params ICriterion[] criteria)
        {
            if (criteria.Length == 0)
                return input;
            else if (criteria.Length == 1)
                return input.Where(criteria[0]);
            else
            {
                var or = Restrictions.Or(criteria[0], criteria[1]);
                for (int i = 2; i < criteria.Length; i++)
                    or = Restrictions.Or(or, criteria[i]);

                return input.Where(or);
            }
        }
    }
}
