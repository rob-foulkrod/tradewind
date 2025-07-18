# Tradewind

A modern ASP.NET Core 9.0 MVC web application built with comprehensive testing, CI/CD, and Azure deployment infrastructure.

## Table of Contents

- [Basic App Structure](#basic-app-structure)
- [Prerequisites](#prerequisites)
- [Running the App](#running-the-app)
- [Testing the App](#testing-the-app)
- [App Design](#app-design)
- [Infrastructure & Deployment](#infrastructure--deployment)
- [CI/CD Pipeline](#cicd-pipeline)
- [Contributing](#contributing)

## Basic App Structure

The application follows the standard ASP.NET Core MVC architecture pattern:

```
src/
├── Tradewind/                      # Main web application
│   ├── Controllers/                # MVC Controllers
│   │   └── HomeController.cs       # Home page controller
│   ├── Models/                     # Data models and view models
│   │   └── ErrorViewModel.cs       # Error handling model
│   ├── Views/                      # Razor view templates
│   │   ├── Home/                   # Home controller views
│   │   └── Shared/                 # Shared layouts and partials
│   ├── wwwroot/                    # Static files (CSS, JS, images)
│   │   └── lib/                    # Client-side libraries (jQuery, Bootstrap)
│   ├── Program.cs                  # Application entry point and configuration
│   ├── appsettings.json            # Application configuration
│   └── Tradewind.csproj            # Project file
└── Tradewind.Tests/                # Unit tests
    ├── HomeControllerTests.cs      # Controller tests
    ├── ErrorViewModelTests.cs      # Model tests
    └── Tradewind.Tests.csproj      # Test project file

infra/                              # Azure infrastructure as code
├── main.bicep                      # Main deployment template
├── webapp.bicep                    # Web app specific resources
├── dev.parameters.json             # Development environment parameters
└── abbrev.json                     # Azure resource abbreviations

.github/
└── workflows/
    └── ci.yml                      # GitHub Actions CI/CD pipeline
```

### Key Components

- **Controllers**: Handle HTTP requests and coordinate between models and views
- **Models**: Data structures and business logic
- **Views**: Razor templates for rendering HTML responses
- **Static Assets**: CSS, JavaScript, and other client-side resources
- **Configuration**: Environment-specific settings and app configuration

## Prerequisites

To run this application locally, you need:

- [.NET 9.0 SDK](https://dotnet.microsoft.com/download/dotnet/9.0) or later
- A code editor (recommended: [Visual Studio](https://visualstudio.microsoft.com/) or [VS Code](https://code.visualstudio.com/))
- [Git](https://git-scm.com/) for version control

Optional for deployment:
- [Azure CLI](https://docs.microsoft.com/en-us/cli/azure/install-azure-cli) for infrastructure deployment
- Azure subscription for hosting

## Running the App

### Local Development

1. **Clone the repository**:
   ```bash
   git clone https://github.com/rob-foulkrod/tradewind.git
   cd tradewind
   ```

2. **Restore dependencies**:
   ```bash
   dotnet restore src/Tradewind/Tradewind.csproj
   ```

3. **Build the application**:
   ```bash
   dotnet build src/Tradewind/Tradewind.csproj
   ```

4. **Run the application**:
   ```bash
   cd src/Tradewind
   dotnet run
   ```

   The application will be available at:
   - **HTTPS**: `https://localhost:5001`
   - **HTTP**: `http://localhost:5000`

### Development Mode

For development with hot reload:
```bash
cd src/Tradewind
dotnet watch run
```

This will automatically rebuild and restart the application when code changes are detected.

### Configuration

The application uses standard ASP.NET Core configuration:
- `appsettings.json`: Base configuration
- `appsettings.Development.json`: Development-specific overrides
- Environment variables: Can override any configuration value

## Testing the App

The application includes a comprehensive test suite with 35 tests covering controllers and models.

### Running Tests

1. **Run all tests**:
   ```bash
   dotnet test src/Tradewind.Tests/Tradewind.Tests.csproj
   ```

2. **Run tests with detailed output**:
   ```bash
   dotnet test src/Tradewind.Tests/Tradewind.Tests.csproj --verbosity normal
   ```

3. **Run tests with code coverage**:
   ```bash
   dotnet test src/Tradewind.Tests/Tradewind.Tests.csproj --collect:"XPlat Code Coverage"
   ```

### Test Structure

- **HomeControllerTests.cs**: Tests for the home controller actions
  - Constructor validation
  - Action method return types
  - Error handling scenarios
  - HTTP context integration

- **ErrorViewModelTests.cs**: Tests for error view model behavior
  - Property validation
  - Display logic
  - Edge cases and null handling

### Testing Framework

- **xUnit**: Primary testing framework
- **Moq**: Mocking framework for dependencies
- **Microsoft.AspNetCore.Mvc.Testing**: Integration testing helpers
- **coverlet.collector**: Code coverage collection

## App Design

### Architecture Overview

Tradewind follows the **Model-View-Controller (MVC)** architectural pattern:

```
┌─────────────────┐    ┌─────────────────┐    ┌─────────────────┐
│                 │    │                 │    │                 │
│      View       │◄───┤   Controller    │◄───┤     Model       │
│   (Razor Pages) │    │  (HTTP Handler) │    │ (Business Logic)│
│                 │    │                 │    │                 │
└─────────────────┘    └─────────────────┘    └─────────────────┘
```

### Design Principles

1. **Separation of Concerns**: Clear separation between presentation, business logic, and data access
2. **Dependency Injection**: Built-in DI container for loose coupling
3. **Configuration-based**: Environment-specific configuration without code changes
4. **Testability**: Comprehensive unit testing with mocking
5. **Security**: HTTPS enforcement, HSTS, and data protection

### Key Design Decisions

- **ASP.NET Core 9.0**: Latest LTS version with modern features
- **MVC Pattern**: Proven architecture for web applications
- **Minimal APIs**: Used for lightweight configuration in Program.cs
- **Static Assets**: Integrated asset management with MapStaticAssets()
- **Error Handling**: Centralized error handling with custom error pages
- **Logging**: Built-in logging with structured logging support

### Request Flow

1. **HTTP Request** → ASP.NET Core Pipeline
2. **Routing** → Route matching and controller selection
3. **Controller** → Action method execution
4. **Model Binding** → Request data to action parameters
5. **Business Logic** → Action method processing
6. **View Rendering** → Razor engine processing
7. **HTTP Response** → Rendered HTML to client

### Security Features

- **HTTPS Redirection**: All traffic redirected to HTTPS
- **HSTS**: HTTP Strict Transport Security headers
- **Data Protection**: Built-in data protection for sensitive data
- **Anti-forgery**: CSRF protection for forms
- **Content Security Policy**: Headers for XSS protection

## Infrastructure & Deployment

### Azure Infrastructure

The application is deployed to Azure using Infrastructure as Code (IaC) with Bicep templates:

#### Resources Created

- **Resource Group**: Container for all resources
- **App Service Plan**: Hosting plan (S1 tier)
- **Web App**: ASP.NET Core 9.0 application hosting
- **Application Insights**: Application performance monitoring
- **Log Analytics Workspace**: Centralized logging

#### Bicep Templates

- **main.bicep**: Subscription-level deployment orchestration
- **webapp.bicep**: Web application and monitoring resources
- **dev.parameters.json**: Environment-specific parameters

#### Deployment Architecture

```
┌─────────────────┐    ┌─────────────────┐    ┌─────────────────┐
│                 │    │                 │    │                 │
│   GitHub Actions│───►│   Azure Web App │───►│ App Insights    │
│   (CI/CD)       │    │   (.NET 9.0)    │    │ (Monitoring)    │
│                 │    │                 │    │                 │
└─────────────────┘    └─────────────────┘    └─────────────────┘
```

### Local Infrastructure Setup

To deploy the infrastructure manually:

1. **Login to Azure**:
   ```bash
   az login
   ```

2. **Deploy infrastructure**:
   ```bash
   cd infra
   az deployment sub create \
     --template-file main.bicep \
     --location "EastUS2" \
     --parameters dev.parameters.json
   ```

## CI/CD Pipeline

The project uses GitHub Actions for continuous integration and deployment:

### Pipeline Stages

1. **Build and Test**
   - Checkout code
   - Setup .NET 9.0
   - Restore dependencies
   - Build solution
   - Run tests with coverage

2. **Security Analysis**
   - CodeQL static analysis
   - Security vulnerability scanning
   - Dependency scanning

3. **Azure Deployment** (main/develop branches only)
   - Deploy Azure infrastructure
   - Build application for release
   - Deploy to Azure Web App

### Pipeline Configuration

- **Triggers**: Push to main/develop branches, pull requests
- **Runtime**: Windows-latest runners
- **Environment**: Development environment for deployments
- **Security**: OIDC authentication with Azure
- **Monitoring**: Coverage collection and reporting

### Environment Variables

Required secrets for deployment:
- `AZURE_CLIENT_ID`: Azure service principal client ID
- `AZURE_TENANT_ID`: Azure tenant ID  
- `AZURE_SUBSCRIPTION_ID`: Azure subscription ID

## Contributing

1. Fork the repository
2. Create a feature branch (`git checkout -b feature/your-feature`)
3. Make your changes
4. Run tests locally (`dotnet test`)
5. Commit your changes (`git commit -am 'Add your feature'`)
6. Push to the branch (`git push origin feature/your-feature`)
7. Create a Pull Request

### Development Standards

- Follow C# coding conventions
- Write unit tests for new functionality
- Update documentation for significant changes
- Ensure all tests pass before submitting PR
- Use meaningful commit messages

---

For questions or support, please create an issue in the repository.