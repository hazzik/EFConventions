using System.Data.Entity;
using Xunit;

namespace EntityFramework.Conventions.Tests
{
    public class AsEnumerableTests
    {
        [Fact]
        public void CanGetConventionsEnumeration()
        {
            var modelBuilder = new DbModelBuilder();
            var conventions = modelBuilder.Conventions.AsEnumerable();
            Assert.NotEmpty(conventions);
        }
    }
}
