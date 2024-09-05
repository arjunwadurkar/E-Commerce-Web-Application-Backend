using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using ProjectBackend.Data;
using ProjectBackend.Entities;
using ProjectBackend.UsersDTO;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ProjectBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly DbContextData _context;
        private readonly IConfiguration _configuration;
        private SymmetricSecurityKey _key;

        public UsersController(DbContextData context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;

        }

        [HttpPost("login")]
        public ActionResult Login(UserDTO userobj)
        {


            var user = _context.UsersData.FirstOrDefault(x => x.email == userobj.email);

            if (user == null)
                return BadRequest("User not found");

            if (!BCrypt.Net.BCrypt.Verify(userobj.password, user.password))
                return BadRequest("Incorrect password");

            // code to make jwt token


            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.NameId, userobj.email),
                new Claim(JwtRegisteredClaimNames.UniqueName, userobj.email),
            };

            _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["TokenKey"]));
            var creds = new SigningCredentials(_key, SecurityAlgorithms.HmacSha512Signature);

            var tokenDiscriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddSeconds(10),
                SigningCredentials = creds,
                Issuer = _configuration["ValidIssuer"],
                Audience = _configuration["ValidAudience"],
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDiscriptor);
            var User = "";

            var AuthModel = new
            {
                token = tokenHandler.WriteToken(token),
                valid = token.ValidTo,
                User = user
            };

            return Ok(AuthModel);
        }


        [HttpPost("adduser")]
        public ActionResult PostUser([FromBody] UsersData userobj)
        {
            var isUserExist = _context.UsersData.FirstOrDefault(x => x.email == userobj.email);

            if (isUserExist != null)
                return BadRequest("Email is already registered");

            string passwordHash = BCrypt.Net.BCrypt.HashPassword(userobj.password);
            userobj.password = passwordHash;

            _context.UsersData.Add(userobj);
            _context.SaveChanges();

            return Ok();
        }



        [HttpGet("SetRoleOfUser")]
        public ActionResult<List<UserSetRoleDTO>> GetADDProduct()
        {
            var users = _context.UsersData.Select(u => new UserSetRoleDTO
            {
                id = u.id,
                name = u.name,
                email = u.email,

                role = u.role // Assuming your UsersData entity has a property called 'role'
            }).ToList();

            return users;
        }

        [HttpPut("UpdateUserRole/{id}")]
        public ActionResult UpdateUserRole(int id, [FromBody] UserSetRoleDTO newrole) {

            var user = _context.UsersData.FirstOrDefault(u => u.id == id);
            if (user == null)
            {
                return BadRequest("User Not Found");
            }

            user.name = newrole.name;
            user.email = newrole.email;
            user.role = newrole.role;

            _context.SaveChanges();

            return Ok(new { Massege = "Setting Role Sccessfully" });
        }

        [HttpDelete("DeleteUserById/{id}")]
        public ActionResult DeleteById(int id)
        {
            var user = _context.UsersData.Find(id);
            if (user != null)
            {
                _context.UsersData.Remove(user);
                _context.SaveChanges();
                return Ok(new { Massege = "Delete Sucessfully" });
            }
            return NotFound("Product Not Found");



        }



        [HttpGet("GetUserById/{id}")]
        public ActionResult<UpdateProfileDTO> GetUserById(int id)
        {
            var user = _context.UsersData.Find(id);
            if (user == null)
            {

                return BadRequest();
            }
            var userDto = new UpdateProfileDTO
            {
                name = user.name,
                email = user.email,
                mobile = user.mobile,
                address = user.address,
                state = user.state,
                city = user.city,
                pin = user.pin,
            };

            return Ok(userDto);

        }

        [HttpPut("UpdateProfile/{id}")]
        public ActionResult UpdateProfile([FromRoute]int id, [FromBody] UpdateProfileDTO updateUser)
        {

            var user = _context.UsersData.FirstOrDefault(u => u.id == id);
            if (user == null)
            {
                return BadRequest("User Not Found");
            }

            user.name = updateUser.name;
            user.email = updateUser.email;
            user.mobile = updateUser.mobile;
            user.address = updateUser.address;
            user.state = updateUser.state;
            user.city = updateUser.city;
            user.pin = updateUser.pin;

            _context.SaveChanges();

            return Ok(new { Massege = "User Updated" });
        }

        [HttpPost("CheckPasswordByID/{id}")]
        public ActionResult CheckPassword(int id, [FromBody] PasswordDTO obj)
        {
            //Console.WriteLine(password);
            var user = _context.UsersData.FirstOrDefault(x => x.id == id);

            if (user == null)
                return BadRequest("User not found");

            if (!BCrypt.Net.BCrypt.Verify(obj.password, user.password))
                return BadRequest("Incorrect password");

            return Ok(new { Massage = "Password Verified" });

        }

        [HttpPut("UpdatePasswordById/{id}")]
        public ActionResult UpdatePaswordById([FromRoute]int id, [FromBody] PasswordDTO userobj)
        {
            var user = _context.UsersData.FirstOrDefault(x => x.id == id);

            if (user == null)
                return BadRequest(new { Massage = "Oops" });

           

            string passwordHash = BCrypt.Net.BCrypt.HashPassword(userobj.password);
            userobj.password = passwordHash;
            user.password = userobj.password;
            _context.SaveChanges();
            return Ok(new { Massage = "Password Update Sucessfully" });

        }
       
        
    }
}
