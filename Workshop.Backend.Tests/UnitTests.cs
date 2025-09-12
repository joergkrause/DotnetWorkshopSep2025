using Grpc.Core;
using Grpc.Core.Testing;
using Microsoft.Extensions.Logging;
using Moq;
using Workshop.BackendApi.Services;
using Workshop.Persistence.Respositories;

namespace Workshop.Backend.Tests
{
  [TestClass]
  public sealed class TestRepo
  {
    
    public required Mock<ILogger<CustomerService>> loggerMock;
    public required Mock<ICustomerRepository> customerRepoMock;

    [TestInitialize]
    public void Initialize()
    {
      loggerMock = new Mock<ILogger<CustomerService>>();
      customerRepoMock = new Mock<ICustomerRepository>();
    }


    [TestMethod]
    public void GetAllAsync_Success()
    {
      // Arrange
      var sut = new CustomerService(loggerMock.Object, customerRepoMock.Object);
      customerRepoMock.Setup(repo => repo.GetAllAsync()).ReturnsAsync(new List<DomainModels.Customer>
      {
        new DomainModels.Customer { Id = 1, Name = "John Doe", Email = "test@test.de", Phone = "1234567890" },
        new DomainModels.Customer { Id = 2, Name = "Jane Smith", Email = "jane@test.de" , Phone = "0987654321" }
      });
      // Act
      
      var reply = sut.GetCustomers(new(), TestServerCallContext.Create()).Result;

      // Assert
      Assert.AreEqual(2, reply.Customers.Count);
      Assert.AreEqual("John Doe", reply.Customers[0].Name);
      Assert.AreEqual("Jane Smith", reply.Customers[1].Name);

    }
  }

  public class TestServerCallContext : ServerCallContext
  {
    private readonly Metadata _requestHeaders;
    private readonly CancellationToken _cancellationToken;
    private readonly Metadata _responseTrailers;
    private readonly AuthContext _authContext;
    private readonly Dictionary<object, object> _userState;
    private WriteOptions? _writeOptions;

    public Metadata? ResponseHeaders { get; private set; }

    private TestServerCallContext(Metadata requestHeaders, CancellationToken cancellationToken)
    {
      _requestHeaders = requestHeaders;
      _cancellationToken = cancellationToken;
      _responseTrailers = new Metadata();
      _authContext = new AuthContext(string.Empty, new Dictionary<string, List<AuthProperty>>());
      _userState = new Dictionary<object, object>();
    }

    protected override string MethodCore => "MethodName";
    protected override string HostCore => "HostName";
    protected override string PeerCore => "PeerName";
    protected override DateTime DeadlineCore { get; }
    protected override Metadata RequestHeadersCore => _requestHeaders;
    protected override CancellationToken CancellationTokenCore => _cancellationToken;
    protected override Metadata ResponseTrailersCore => _responseTrailers;
    protected override Status StatusCore { get; set; }
    protected override WriteOptions? WriteOptionsCore { get => _writeOptions; set { _writeOptions = value; } }
    protected override AuthContext AuthContextCore => _authContext;

    protected override ContextPropagationToken CreatePropagationTokenCore(ContextPropagationOptions? options)
    {
      throw new NotImplementedException();
    }

    protected override Task WriteResponseHeadersAsyncCore(Metadata responseHeaders)
    {
      if (ResponseHeaders != null)
      {
        throw new InvalidOperationException("Response headers have already been written.");
      }

      ResponseHeaders = responseHeaders;
      return Task.CompletedTask;
    }

    protected override IDictionary<object, object> UserStateCore => _userState;

    public static TestServerCallContext Create(Metadata? requestHeaders = null, CancellationToken cancellationToken = default)
    {
      return new TestServerCallContext(requestHeaders ?? new Metadata(), cancellationToken);
    }
  }

}
