# GitHub Instructions

## Project Structure

This is a **Tradewind** ASP.NET Core MVC web application for trading platform solutions.

### Key Directories
- **`src/Tradewind/`** - Main web application
  - **`Controllers/`** - MVC controllers handling HTTP requests
  - **`Views/`** - Razor view templates for UI rendering
  - **`Models/`** - Data models and view models
  - **`wwwroot/`** - Static files (CSS, JS, images)
  - **`Properties/`** - Application configuration
- **`src/Tradewind.Tests/`** - Unit tests for the application
- **`.github/`** - GitHub workflows and repository documentation

### Purpose
The Tradewind application provides a modern, responsive trading platform interface with:
- Landing page showcasing platform features
- Real-time market data integration capabilities
- Secure user authentication system
- Mobile-responsive design with Bootstrap
- Trading analytics and insights

## Commit Message Format

Use Conventional Commits format for all commit messages:

```
<type>: <description>
```

### Types
- `feat`: New feature
- `fix`: Bug fix
- `docs`: Documentation changes
- `style`: Code formatting (no logic changes)
- `refactor`: Code restructuring (no new features or fixes)
- `test`: Adding or updating tests
- `chore`: Maintenance tasks

### Examples
```
feat: add user login functionality
fix: resolve database connection timeout
docs: update API documentation
style: format code with prettier
refactor: extract validation logic
test: add unit tests for auth service
chore: update dependencies
```

### Breaking Changes
Add `!` after the type for breaking changes:
```
feat!: remove deprecated API endpoints
```