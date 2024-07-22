using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Auth.Attributes;
using Data.Models;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Reflection;
using System.Security.Claims;
using System.Web.Mvc;
using Auth;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Routing;
using Moq;
using ActionExecutingContext = Microsoft.AspNetCore.Mvc.Filters.ActionExecutingContext;

namespace TestProject1AuthTest.Attributes;

[TestClass]
[TestSubject(typeof(AuthorizeActionFilter))]
public class AuthorizeActionFilterTest
{
    
    private Mock<UserManager<User>> _userManagerMock;
    private Mock<ITokenUtils> _tokenUtilsMock;
    private AuthorizeActionFilter _filter;
    private ActionExecutingContext _context;
    private Mock<HttpContext> _httpContextMock;
    private Mock<HttpRequest> _requestMock;
    private Mock<ControllerActionDescriptor> _actionDescriptorMock;

    [TestInitialize]
    public void Setup()
    {
        var store = new Mock<IUserStore<User>>();
        _userManagerMock =new Mock<UserManager<User>>(store.Object, null, null, null, null, null, null, null, null);
        _tokenUtilsMock = new Mock<ITokenUtils>();
        _filter = new AuthorizeActionFilter(_userManagerMock.Object, _tokenUtilsMock.Object);

        _httpContextMock = new Mock<HttpContext>();
        _requestMock = new Mock<HttpRequest>();
        _context = new ActionExecutingContext(
            new ActionContext(_httpContextMock.Object, new RouteData(), new ControllerActionDescriptor()),
            new List<IFilterMetadata>(),
            new Dictionary<string, object>(),
            new Mock<Controller>().Object
        );

        _actionDescriptorMock = new Mock<ControllerActionDescriptor>();
        _context.ActionDescriptor = _actionDescriptorMock.Object;
        _httpContextMock.Setup(ctx => ctx.Request).Returns(_requestMock.Object);
    }

    [TestMethod]
    public void OnActionExecuting_NoAuthorizeAttribute_DoesNotSetResult()
    {
        // Arrange
        _actionDescriptorMock.SetupGet(ad => ad.MethodInfo).Returns(GetType().GetMethod(nameof(TestMethod)));

        // Act
        _filter.OnActionExecuting(_context);

        // Assert
        Assert.IsNull(_context.Result);
    }

    [Auth.Attributes.Authorize]
    public void TestMethod()
    {
    }

    [TestMethod]
    public void OnActionExecuting_NoToken_SetsUnauthorizedResult()
    {
        // Arrange
        var methodInfo = GetType().GetMethod(nameof(TestMethod));
        _actionDescriptorMock.SetupGet(ad => ad.MethodInfo).Returns(methodInfo);
        _requestMock.Setup(req => req.Headers).Returns(new HeaderDictionary());

        // Act
        _filter.OnActionExecuting(_context);

        // Assert
        Assert.IsInstanceOfType(_context.Result, typeof(UnauthorizedResult));
    }

    [TestMethod]
    public void OnActionExecuting_InvalidToken_SetsUnauthorizedResult()
    {
        // Arrange
        var methodInfo = GetType().GetMethod(nameof(TestMethod));
        _actionDescriptorMock.SetupGet(ad => ad.MethodInfo).Returns(methodInfo);
        var headers = new HeaderDictionary { { "Authorization", "Bearer invalid-token" } };
        _requestMock.Setup(req => req.Headers).Returns(headers);

        _tokenUtilsMock.Setup(tu => tu.GetJwtToken("invalid-token")).Returns((JwtSecurityToken)null);

        // Act
        _filter.OnActionExecuting(_context);

        // Assert
        Assert.IsInstanceOfType(_context.Result, typeof(UnauthorizedResult));
    }

    [TestMethod]
    public void OnActionExecuting_ValidTokenNoUserId_SetsUnauthorizedResult()
    {
        // Arrange
        var methodInfo = GetType().GetMethod(nameof(TestMethod));
        _actionDescriptorMock.SetupGet(ad => ad.MethodInfo).Returns(methodInfo);
        var headers = new HeaderDictionary { { "Authorization", "Bearer valid-token" } };
        _requestMock.Setup(req => req.Headers).Returns(headers);

        var jwtTokenMock = new Mock<JwtSecurityToken>();
        jwtTokenMock.Setup(token => token.Claims).Returns(new List<Claim>());

        _tokenUtilsMock.Setup(tu => tu.GetJwtToken("valid-token")).Returns(jwtTokenMock.Object);

        // Act
        _filter.OnActionExecuting(_context);

        // Assert
        Assert.IsInstanceOfType(_context.Result, typeof(UnauthorizedResult));
    }

    [TestMethod]
    public void OnActionExecuting_UserNotInRole_SetsUnauthorizedResult()
    {
        // Arrange
        var methodInfo = GetType().GetMethod(nameof(TestMethod));
        _actionDescriptorMock.SetupGet(ad => ad.MethodInfo).Returns(methodInfo);
        var headers = new HeaderDictionary { { "Authorization", "Bearer valid-token" } };
        _requestMock.Setup(req => req.Headers).Returns(headers);

        var claims = new List<Claim> { new Claim("user", "user-id") };
        var jwtTokenMock = new Mock<JwtSecurityToken>();
        jwtTokenMock.Setup(token => token.Claims).Returns(claims);

        _tokenUtilsMock.Setup(tu => tu.GetJwtToken("valid-token")).Returns(jwtTokenMock.Object);

        var user = new User();
        _userManagerMock.Setup(um => um.FindByIdAsync("user-id")).ReturnsAsync(user);
        _userManagerMock.Setup(um => um.IsInRoleAsync(user, It.IsAny<string>())).ReturnsAsync(false);

        // Act
        _filter.OnActionExecuting(_context);

        // Assert
        Assert.IsInstanceOfType(_context.Result, typeof(UnauthorizedResult));
    }

    [TestMethod]
    public void OnActionExecuting_UserInRole_DoesNotSetResult()
    {
        // Arrange
        var methodInfo = GetType().GetMethod(nameof(TestMethod));
        _actionDescriptorMock.SetupGet(ad => ad.MethodInfo).Returns(methodInfo);
        var headers = new HeaderDictionary { { "Authorization", "Bearer valid-token" } };
        _requestMock.Setup(req => req.Headers).Returns(headers);

        var claims = new List<Claim> { new Claim("user", "user-id") };
        var jwtTokenMock = new Mock<JwtSecurityToken>();
        jwtTokenMock.Setup(token => token.Claims).Returns(claims);

        _tokenUtilsMock.Setup(tu => tu.GetJwtToken("valid-token")).Returns(jwtTokenMock.Object);

        var user = new User();
        _userManagerMock.Setup(um => um.FindByIdAsync("user-id")).ReturnsAsync(user);
        _userManagerMock.Setup(um => um.IsInRoleAsync(user, It.IsAny<string>())).ReturnsAsync(true);

        // Act
        _filter.OnActionExecuting(_context);

        // Assert
        Assert.IsNull(_context.Result);
    }
}