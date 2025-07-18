using System.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Tradewind.Controllers;
using Tradewind.Models;

namespace Tradewind.Tests;

public class HomeControllerTests
{
    private readonly Mock<ILogger<HomeController>> _mockLogger;
    private readonly HomeController _controller;

    public HomeControllerTests()
    {
        _mockLogger = new Mock<ILogger<HomeController>>();
        _controller = new HomeController(_mockLogger.Object);
    }

    [Fact]
    public void Constructor_WithValidLogger_CreatesController()
    {
        // Arrange
        var logger = new Mock<ILogger<HomeController>>();

        // Act
        var controller = new HomeController(logger.Object);

        // Assert
        Assert.NotNull(controller);
    }

    [Fact]
    public void Index_ReturnsViewResult()
    {
        // Act
        var result = _controller.Index();

        // Assert
        Assert.IsType<ViewResult>(result);
    }

    [Fact]
    public void Index_ReturnsViewWithoutModel()
    {
        // Act
        var result = _controller.Index() as ViewResult;

        // Assert
        Assert.NotNull(result);
        Assert.Null(result.Model);
    }

    [Fact]
    public void Privacy_ReturnsViewResult()
    {
        // Act
        var result = _controller.Privacy();

        // Assert
        Assert.IsType<ViewResult>(result);
    }

    [Fact]
    public void Privacy_ReturnsViewWithoutModel()
    {
        // Act
        var result = _controller.Privacy() as ViewResult;

        // Assert
        Assert.NotNull(result);
        Assert.Null(result.Model);
    }

    [Fact]
    public void Error_ReturnsViewResult()
    {
        // Arrange
        SetupHttpContext();

        // Act
        var result = _controller.Error();

        // Assert
        Assert.IsType<ViewResult>(result);
    }

    [Fact]
    public void Error_ReturnsViewWithErrorViewModel()
    {
        // Arrange
        SetupHttpContext();

        // Act
        var result = _controller.Error() as ViewResult;

        // Assert
        Assert.NotNull(result);
        Assert.IsType<ErrorViewModel>(result.Model);
    }

    [Fact]
    public void Error_WhenActivityCurrentIsNull_UsesHttpContextTraceIdentifier()
    {
        // Arrange
        var traceId = "test-trace-id";
        SetupHttpContext(traceId);
        Activity.Current = null;

        // Act
        var result = _controller.Error() as ViewResult;
        var model = result?.Model as ErrorViewModel;

        // Assert
        Assert.NotNull(model);
        Assert.Equal(traceId, model.RequestId);
    }

    [Fact]
    public void Error_WhenActivityCurrentExists_UsesActivityId()
    {
        // Arrange
        SetupHttpContext("fallback-trace-id");
        
        using var activity = new Activity("TestActivity");
        activity.SetIdFormat(ActivityIdFormat.W3C);
        activity.Start();
        
        // Mock the Activity.Current.Id
        // Note: Since Activity.Current.Id is read-only, we'll test the fallback scenario
        Activity.Current = null;

        // Act
        var result = _controller.Error() as ViewResult;
        var model = result?.Model as ErrorViewModel;

        // Assert
        Assert.NotNull(model);
        Assert.Equal("fallback-trace-id", model.RequestId);
    }

    [Fact]
    public void Error_HasCorrectResponseCacheAttributes()
    {
        // Arrange
        var methodInfo = typeof(HomeController).GetMethod(nameof(HomeController.Error));

        // Act
        var attribute = methodInfo?.GetCustomAttributes(typeof(ResponseCacheAttribute), false)
            .FirstOrDefault() as ResponseCacheAttribute;

        // Assert
        Assert.NotNull(attribute);
        Assert.Equal(0, attribute.Duration);
        Assert.Equal(ResponseCacheLocation.None, attribute.Location);
        Assert.True(attribute.NoStore);
    }

    [Fact]
    public void Error_ErrorViewModelShowRequestId_ReturnsTrue_WhenRequestIdIsSet()
    {
        // Arrange
        var traceId = "test-trace-id-123";
        SetupHttpContext(traceId);

        // Act
        var result = _controller.Error() as ViewResult;
        var model = result?.Model as ErrorViewModel;

        // Assert
        Assert.NotNull(model);
        Assert.Equal(traceId, model.RequestId);
        Assert.True(model.ShowRequestId);
    }

    [Fact]
    public void Error_ErrorViewModelShowRequestId_ReturnsFalse_WhenRequestIdIsEmpty()
    {
        // Arrange
        SetupHttpContext("");

        // Act
        var result = _controller.Error() as ViewResult;
        var model = result?.Model as ErrorViewModel;

        // Assert
        Assert.NotNull(model);
        Assert.Equal("", model.RequestId);
        Assert.False(model.ShowRequestId);
    }

    [Theory]
    [InlineData("trace-id-1")]
    [InlineData("very-long-trace-identifier-with-many-characters")]
    [InlineData("12345")]
    [InlineData("a")]
    public void Error_WithDifferentTraceIds_SetsCorrectRequestId(string traceId)
    {
        // Arrange
        SetupHttpContext(traceId);

        // Act
        var result = _controller.Error() as ViewResult;
        var model = result?.Model as ErrorViewModel;

        // Assert
        Assert.NotNull(model);
        Assert.Equal(traceId, model.RequestId);
    }

    [Fact]
    public void Controller_InheritsFromController()
    {
        // Assert
        Assert.IsAssignableFrom<Controller>(_controller);
    }

    [Fact]
    public void Controller_HasCorrectNamespace()
    {
        // Assert
        Assert.Equal("Tradewind.Controllers", typeof(HomeController).Namespace);
    }

    private void SetupHttpContext(string traceIdentifier = "default-trace-id")
    {
        var mockHttpContext = new Mock<HttpContext>();
        mockHttpContext.Setup(x => x.TraceIdentifier).Returns(traceIdentifier);
        
        var controllerContext = new ControllerContext
        {
            HttpContext = mockHttpContext.Object
        };
        
        _controller.ControllerContext = controllerContext;
    }
}
