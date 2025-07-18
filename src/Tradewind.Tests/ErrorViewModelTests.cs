using Tradewind.Models;

namespace Tradewind.Tests;

public class ErrorViewModelTests
{
    [Fact]
    public void ErrorViewModel_RequestId_CanBeSet()
    {
        // Arrange
        var errorViewModel = new ErrorViewModel();
        var expectedRequestId = "12345";

        // Act
        errorViewModel.RequestId = expectedRequestId;

        // Assert
        Assert.Equal(expectedRequestId, errorViewModel.RequestId);
    }

    [Fact]
    public void ErrorViewModel_RequestId_CanBeNull()
    {
        // Arrange
        var errorViewModel = new ErrorViewModel();

        // Act
        errorViewModel.RequestId = null;

        // Assert
        Assert.Null(errorViewModel.RequestId);
    }

    [Fact]
    public void ShowRequestId_WhenRequestIdIsNotNullOrEmpty_ReturnsTrue()
    {
        // Arrange
        var errorViewModel = new ErrorViewModel
        {
            RequestId = "12345"
        };

        // Act
        var result = errorViewModel.ShowRequestId;

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void ShowRequestId_WhenRequestIdIsNull_ReturnsFalse()
    {
        // Arrange
        var errorViewModel = new ErrorViewModel
        {
            RequestId = null
        };

        // Act
        var result = errorViewModel.ShowRequestId;

        // Assert
        Assert.False(result);
    }

    [Fact]
    public void ShowRequestId_WhenRequestIdIsEmpty_ReturnsFalse()
    {
        // Arrange
        var errorViewModel = new ErrorViewModel
        {
            RequestId = ""
        };

        // Act
        var result = errorViewModel.ShowRequestId;

        // Assert
        Assert.False(result);
    }

    [Fact]
    public void ShowRequestId_WhenRequestIdIsWhitespace_ReturnsTrue()
    {
        // Arrange
        var errorViewModel = new ErrorViewModel
        {
            RequestId = "   "
        };

        // Act
        var result = errorViewModel.ShowRequestId;

        // Assert - IsNullOrEmpty returns false for whitespace, so ShowRequestId should be true
        Assert.True(result);
    }

    [Theory]
    [InlineData("abc123")]
    [InlineData("request-id-001")]
    [InlineData("1")]
    [InlineData("very-long-request-id-with-many-characters")]
    public void ShowRequestId_WhenRequestIdHasValue_ReturnsTrue(string requestId)
    {
        // Arrange
        var errorViewModel = new ErrorViewModel
        {
            RequestId = requestId
        };

        // Act
        var result = errorViewModel.ShowRequestId;

        // Assert
        Assert.True(result);
    }

    [Theory]
    [InlineData("")]
    public void ShowRequestId_WhenRequestIdIsNullOrEmpty_ReturnsFalse(string? requestId)
    {
        // Arrange
        var errorViewModel = new ErrorViewModel
        {
            RequestId = requestId
        };

        // Act
        var result = errorViewModel.ShowRequestId;

        // Assert
        Assert.False(result);
    }

    [Fact]
    public void ShowRequestId_WhenRequestIdIsNullValue_ReturnsFalse()
    {
        // Arrange
        var errorViewModel = new ErrorViewModel
        {
            RequestId = null
        };

        // Act
        var result = errorViewModel.ShowRequestId;

        // Assert
        Assert.False(result);
    }

    [Theory]
    [InlineData("   ")]
    [InlineData("\t")]
    [InlineData("\n")]
    [InlineData("\r\n")]
    public void ShowRequestId_WhenRequestIdIsWhitespaceOnly_ReturnsTrue(string requestId)
    {
        // Arrange
        var errorViewModel = new ErrorViewModel
        {
            RequestId = requestId
        };

        // Act
        var result = errorViewModel.ShowRequestId;

        // Assert - IsNullOrEmpty returns false for whitespace, so ShowRequestId should be true
        Assert.True(result);
    }

    [Fact]
    public void ErrorViewModel_DefaultConstructor_InitializesWithNullRequestId()
    {
        // Arrange & Act
        var errorViewModel = new ErrorViewModel();

        // Assert
        Assert.Null(errorViewModel.RequestId);
        Assert.False(errorViewModel.ShowRequestId);
    }
}
