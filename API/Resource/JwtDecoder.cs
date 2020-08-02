using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace NeGelio.Service.LoginProcess
{
    public class JwtDecoder
    {
        public LoginUser DecodeJwt(string jwt)
        {
            var token = new JwtSecurityToken(jwtEncodedString: jwt);
            var loginUser = new LoginUser()
            {
                Id = Convert.ToInt32(token.Claims.Where(x => x.Type == ClaimTypes.Actor).FirstOrDefault().Value),
                Name = token.Claims.Where(x => x.Type == ClaimTypes.Name).FirstOrDefault().Value,
                Role = token.Claims.Where(x => x.Type == ClaimTypes.Role).FirstOrDefault().Value,
                Email = token.Claims.Where(x => x.Type == ClaimTypes.Email).FirstOrDefault().Value
            };
            return loginUser;
        }
    }
}
