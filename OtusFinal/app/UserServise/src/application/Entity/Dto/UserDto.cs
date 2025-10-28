using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Dto
{
    public record UserDto(string Username, string FirstName, string LastName, string Email, string Phone);
}
