using Microsoft.AspNetCore.Mvc;
using TetPee.Repositories;
using TetPee.Repositories.Entity;
using TetPee.Services.User;

namespace TetPee.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController: ControllerBase
{
    private readonly AppDbContext _dbContext;
    //cái này nâng cao lúc sau sẽ giải thích
    public UserController(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    //HTTP method: GET, POST, DELETE, PUT, PATCH
    //PARAM: Query string, path param, body param
    
    //Query string: http?//localhost:5000/User?name=abc
        //name va age là query string
        //query string nằm sau dấu ?
        
    //Path (Route) Param: http//localhost:5000/User/123
        //123 là path param hoặc route param
        //path param nằm trong đường dẫn
        
    //Get là ko có body
    //POST, PUT, PATCH có body
    
    //tại sao phải dùng body: tránh để lộ những thông tin ko mong muốn
    //ví dụ: username, password
    //ko thể http//localhost:5000/login?user=abc&password=cba
    
    //get all users: GET http//localhost:5000/User
    //get user by id: GET http//localhost:5000/User/{id}
    //update user by ud: PUT http//localhost:5000/User/{id}
    //delete user by ud: DELETE http//localhost:5000/User/{id}
    
    
    //GET user
    [HttpGet(template: "")]
    //(template: ""): path param
    //query param là các biến nằm sau dấu chấm hỏi
    //
    public IActionResult GetUsers([FromQuery]string? searchTerm)
    {
        var user = _dbContext.Users.ToList();
        return Ok(user);

    }
    
    [HttpGet(template: "{id}")]
    //id là này là from route
    //ko phải from query
    public IActionResult GetUsersById([FromRoute]Guid id)
    {
        //var user = _dbContext.Users.ToList();
        //return Ok(Users)
        return Ok("Get all users");
    }
    
    //POST create user
    [HttpPost(template: "")]
    public IActionResult CreateUsser([FromBody] Request.CreateUserRequest request)
    //khai báo body
    {
        var user = new User()
        {
            FirstName = request.FirstName,
            LastName = request.LastName,
            Email = request.Email,
            HashedPassword =  request.Password,
        };
        
        _dbContext.Users.Add(user);
        _dbContext.SaveChanges();
        
        //return Ok(Users)
        Console.WriteLine(request);
        return Ok("Create user successfully");
    }
    
    //DELETE user
    [HttpDelete(template: "{id}")]
    public IActionResult DeleteUsersById(Guid id)
    {
        //var user = _dbContext.Users.ToList();
        //return Ok(Users)
        return Ok("Delete successfully");
    }
    
    //PUT update user
    [HttpPut(template: "{id}")]
    public IActionResult UpdateUserById(Guid id)
    {
        //var user = _dbContext.Users.ToList();
        //return Ok(Users)
        return Ok("Update successfully");
    }
}