using FluentAssertions;
using Impact.Basket.Api.Domain.Model.Filters;
using Impact.Basket.Api.Helpers;
using Impact.Basket.Api.Services;

namespace Impact.Basket.Api.UnitTests.Helpers
{
    [TestClass]
    public class PaginationHelpersTests
    {
        UriService uriService = new UriService("http://example.com/");

        [TestMethod]
        public void CreatePaginatedResponse_ValidData_ReturnsPagedResponseWithNextAndPreviousPages()
        {
            // Arrange
            var paginationFilter = new PaginationFilter(2, 10);
            var response = new List<string> { "Item 1", "Item 2", "Item 3" };

            // Act
            var result = PaginationHelpers.CreatePaginatedResponse(uriService, paginationFilter, response);

            // Assert
            result.Should().NotBeNull();
            result.Data.Should().BeEquivalentTo(response);
            result.PageNumber.Should().Be(2);
            result.PageSize.Should().Be(10);
            result.NextPage.Should().Be("http://example.com/?pageNumber=3&pageSize=10");
            result.PreviousPage.Should().Be("http://example.com/?pageNumber=1&pageSize=10");
        }

        [TestMethod]
        public void CreatePaginatedResponse_NoPreviousPage_ReturnsPagedResponseWithNullPrevioustPage()
        {
            // Arrange
            var paginationFilter = new PaginationFilter(1, 10);
            var response = new List<string> { "Item 1", "Item 2", "Item 3" };

            // Act
            var result = PaginationHelpers.CreatePaginatedResponse(uriService, paginationFilter, response);

            // Assert
            result.Should().NotBeNull();
            result.Data.Should().BeEquivalentTo(response);
            result.PageNumber.Should().Be(1);
            result.PageSize.Should().Be(10);
            result.NextPage.Should().Be("http://example.com/?pageNumber=2&pageSize=10");
            result.PreviousPage.Should().BeNull();
        }

        [TestMethod]
        public void CreatePaginatedResponse_EmptyResponse_ReturnsPagedResponseWithNullNextAndPreviousPages()
        {
            // Arrange
            var paginationFilter = new PaginationFilter(1, 10);
            var response = new List<string>();

            // Act
            var result = PaginationHelpers.CreatePaginatedResponse(uriService, paginationFilter, response);

            // Assert
            result.Should().NotBeNull();
            result.Data.Should().BeEquivalentTo(response);
            result.PageNumber.Should().Be(1);
            result.PageSize.Should().Be(10);
            result.NextPage.Should().BeNull();
            result.PreviousPage.Should().BeNull();
        }
    }
}
