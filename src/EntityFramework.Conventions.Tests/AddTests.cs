using System.Data.Entity;
using System.Linq;
using Xunit;

namespace EntityFramework.Conventions.Tests
{
    public class AddTests
    {
        [Fact]
        public void CanAddConventionWithDefaultConstructor()
        {
            var modelBuilder = new DbModelBuilder();
            modelBuilder.Conventions.Add<TestConvention>();

            var convention = modelBuilder.Conventions
                .AsEnumerable()
                .OfType<TestConvention>()
                .SingleOrDefault();

            Assert.NotNull(convention);
        }

        [Fact]
        public void CanAddManyConventions()
        {
            var modelBuilder = new DbModelBuilder();
            modelBuilder.Conventions.Add(new TestConvention(), new TestConvention());

            var conventionCount = modelBuilder.Conventions
                .AsEnumerable()
                .OfType<TestConvention>()
                .Count();

            Assert.Equal(2, conventionCount);
        }

        [Fact]
        public void CanAddSingleConvention()
        {
            var convention = new TestConvention();

            var modelBuilder = new DbModelBuilder();
            modelBuilder.Conventions.Add(convention);

            Assert.Contains(convention, modelBuilder.Conventions.AsEnumerable());
        }
    }
}
