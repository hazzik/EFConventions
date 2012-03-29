using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration.Configuration;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Linq.Expressions;

namespace EntityFramework.Conventions
{
    public static class ConventionsConfigurationEx
    {
        private static readonly Lazy<Func<ConventionsConfiguration, ICollection<IConvention>>> ConventionsLazy = new Lazy<Func<ConventionsConfiguration, ICollection<IConvention>>>(BuildConventions);

        private static Func<ConventionsConfiguration, ICollection<IConvention>> Conventions
        {
            get { return ConventionsLazy.Value; }
        }

        public static void Add<TConvention>(this ConventionsConfiguration self, TConvention convention)
            where TConvention : IConvention
        {
            Conventions(self).Add(convention);
        }

        public static void Add<TConvention>(this ConventionsConfiguration self)
            where TConvention : IConvention, new()
        {
            Conventions(self).Add(new TConvention());
        }

        public static void Add(this ConventionsConfiguration self, params IConvention[] conventions)
        {
            var collection = Conventions(self);
            foreach (var convention in conventions)
                collection.Add(convention);
        }

        internal static IEnumerable<IConvention> AsEnumerable(this ConventionsConfiguration self)
        {
            return Conventions(self).AsEnumerable();
        }

        private static Func<ConventionsConfiguration, ICollection<IConvention>> BuildConventions()
        {
            var parameter = Expression.Parameter(typeof (ConventionsConfiguration));

            var conventions = Expression.Lambda<Func<ConventionsConfiguration, ICollection<IConvention>>>(
                Expression.Field(parameter, "_conventions"),
                parameter);

            return conventions.Compile();
        }
    }
}
