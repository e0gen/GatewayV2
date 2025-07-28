# Unit Testing Guidelines

## Testing Framework
- Use **NUnit** as the testing framework
- Use **Moq** for mocking dependencies
- Follow **Arrange-Act-Assert (AAA)** pattern for test structure

## Naming Conventions

### Test Method Naming
Follow the **Osherove's naming convention**:
```csharp
public void <UnitOfWork>_<StateUnderTest>_<ExpectedBehavior>()
```

### Examples:
```csharp
private Mock<IProductRepository> ProductRepositoryMock { get; set; }

[SetUp]
public void SetUp()
{
    ProductRepositoryMock = new Mock<IProductRepository>();
}

private ProductService CreateService()
{
    return new ProductService(ProductRepositoryMock.Object);
}

[Test]
public void AddProduct_ProductNameExists_ThrowsException()
{
    var service = CreateService();
    var product = new Product { Name = "ExistingProduct", Price = 10.99m };
    ProductRepositoryMock.Setup(x => x.Exists(product.Name)).Returns(true);

    Assert.Throws<InvalidOperationException>(() => service.AddProduct(product));
    ProductRepositoryMock.Verify(x => x.Add(It.IsAny<Product>()), Times.Never);
}

[Test]
public void AddProduct_ValidProduct_AddsToRepository()
{
    var service = CreateService();
    var product = new Product { Name = "NewProduct", Price = 10.99m };
    ProductRepositoryMock.Setup(x => x.Exists(product.Name)).Returns(false);

    service.AddProduct(product);

    ProductRepositoryMock.Verify(x => x.Add(It.Is<Product>(p => 
        p.Name == product.Name && 
        p.Price == product.Price)), 
        Times.Once);
}
```

## Test Structure
1. **Arrange**: Set up test data and mocks
2. **Act**: Execute the method under test
3. **Assert**: Verify the outcome

## Best Practices
- Keep tests small and focused on a single behavior
- Use descriptive test names that explain the test case
- Avoid logic in tests (no loops, conditionals)
- Test one assertion per test when possible
- Use meaningful mock data
- Keep tests independent and isolated
- Avoid testing implementation details, focus on behavior
- **Do not include comments in test code** - the test name and code should be self-documenting
- **Use separate factory methods for test data creation** - Create test data using dedicated factory methods to improve test maintainability and reduce code duplication
