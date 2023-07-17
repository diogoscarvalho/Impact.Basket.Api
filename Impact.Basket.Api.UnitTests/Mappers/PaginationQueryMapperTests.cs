using FluentAssertions;
using Impact.Basket.Api.Mappers;
using Impact.Basket.Api.Models.Requests.Queries;

namespace Impact.Basket.Api.UnitTests.Mappers
{
    [TestClass]
    public class PaginationQueryMapperTests
    {
        [TestMethod]
        public void ToPaginationFilter_ConvertsPaginationQueryToPaginationFilter()
        {
            // Arrange
            var paginationQuery = new PaginationQuery
            {
                PageNumber = 2,
                PageSize = 20
            };

            // Act
            var paginationFilter = paginationQuery.TopaginationFilter();

            // Assert
            paginationFilter.PageNumber.Should().Be(2);
            paginationFilter.PageSize.Should().Be(20);
        }

        [TestMethod]
        public void ToPaginationFilter_DefaultValuesForPaginationQuery_ConvertsToPaginationFilterWithDefaultValues()
        {
            // Arrange
            var paginationQuery = new PaginationQuery();

            // Act
            var paginationFilter = paginationQuery.TopaginationFilter();

            // Assert
            paginationFilter.PageNumber.Should().Be(1);
            paginationFilter.PageSize.Should().Be(100);
        }
    }

}
