using System.Threading.Tasks;
using FluentAssertions;
using Moq;
using TransityEasy.Api.Core.Handlers;
using TransityEasy.Api.Core.Models.ApiResponse;
using TransityEasy.Api.Core.Models.Request;
using TransityEasy.Api.Core.Models.Result;
using TransityEasy.Api.Core.Services;
using Xunit;

namespace TransitEasy.Api.Core.Tests.Handlers
{
    public class NearbyStopsRequestHandlerTests
    {
        private readonly Mock<ITranslinkApiService> _translinkApiServiceMock;
        private readonly IRequestHandler<NearbyStopsInfoRequest, NearbyStopsInfoResult> _sut;

        public NearbyStopsRequestHandlerTests()
        {
            _translinkApiServiceMock = new Mock<ITranslinkApiService>();
            _sut = new NearbyStopsRequestHandler(_translinkApiServiceMock.Object); 
        }

        [Fact]
        public async Task GivenTranslinkApiSuccessResult_WhenHandle_ThenReturnNearbyStopsResultSuccess()
        {
            //Arrange
            var request = new NearbyStopsInfoRequest
            {
                Latitude = 11.045,
                Longitude = 12.056,
                Radius = 10
            }; 
            _translinkApiServiceMock.Setup(tas => tas.GetNearbyStops(It.IsAny<double>(), It.IsAny<double>(), It.IsAny<int>()))
                .ReturnsAsync(new StopsResponseResult());

            //Act
            var result = await _sut.HandleRequest(request);

            //Assert
            result.ResponseStatus.Should().Be(StatusCode.Success);
        }
    }
}
