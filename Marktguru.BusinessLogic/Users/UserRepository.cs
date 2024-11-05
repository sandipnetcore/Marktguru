using Marktguru.BusinessLogic.Configurations;
using MarktguruAssignment.DataModels.Login;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Marktguru.BusinessLogic.Users
{
    public class UserRepository
    {
        private SymmetricSecurityKey PrivateSymmetricKey
        {
            get
            {
                return new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration.PrivateKey));
            }
        }

        public JWTConfigurationSettings Configuration { get; }
        
        public UserRepository(JWTConfigurationSettings JWTConfig) 
        {
            Configuration = JWTConfig;
        }

        private User? GetUser(string UserName, string Password)
        {
            return User.AllUsers.Where(x => x.UserName == UserName && x.Password == Password).FirstOrDefault();
        }

        public User? GetUserByUserName(string UserName)
        {
            return User.AllUsers.Where(x => x.UserName == UserName).FirstOrDefault();
        }

        public string? UserLogin(LoginCredentialsDataModel loginCredentials)
        {
            try
            {
                var user = GetUser(loginCredentials.UserName, loginCredentials.Password);

                //Dont genrate the token if User doesnt exist.
                if (user == null)
                {
                    return null;
                }

                //User is found. Now lets allow the application to trust the user.
                //Lets have some claims for the user.
                //Since it is dummy application so I am adding the same email address for all users :). 

                var claimsList = new List<Claim>()
            {
                new Claim("UserName", loginCredentials.UserName),
                new Claim("email", "someEmail@website.com"),
            };

                //We can all add the user roles to the claims. This is also an identity for the user.
                foreach (var role in user.UserRoles)
                {
                    claimsList.Add(new Claim(ClaimTypes.Role, role));
                }

                //Create the token
                var token = new JwtSecurityTokenHandler().WriteToken(_GenerateToken(claimsList));

                return token;
            }
            catch
            {
                throw;
            }

        }

        private JwtSecurityToken _GenerateToken(List<Claim> claims)
        {
            var token = new JwtSecurityToken(
                    issuer: Configuration.IssuerUrl,
                    audience: Configuration.AudienceUrl,
                    expires: DateTime.Now.AddSeconds(Configuration.ExpiryInSeconds),
                    claims: claims,
                    signingCredentials: new SigningCredentials(PrivateSymmetricKey, SecurityAlgorithms.HmacSha256)
                    );

            return token;
        }

        public JwtSecurityToken? GetTokenOnValidation(string RequestToken)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                tokenHandler.ValidateToken(RequestToken, new TokenValidationParameters
                {
                    ValidateAudience = true,
                    ValidateIssuer = true,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = PrivateSymmetricKey,
                    ClockSkew = TimeSpan.Zero,
                    ValidIssuer = Configuration.IssuerUrl,
                    ValidAudience = Configuration.AudienceUrl
                }, out SecurityToken securityToken);

                if (securityToken == null)
                {
                    return null;
                }

                var token = (JwtSecurityToken)securityToken;
                return token;
            }
            catch
            {
                throw;
            }
        }
    }
}
