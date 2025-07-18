# GitHub Instructions

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