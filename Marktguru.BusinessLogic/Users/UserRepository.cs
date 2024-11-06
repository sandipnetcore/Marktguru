using Marktguru.BusinessLogic.Configurations;
using MarktguruAssignment.DataModels.Login;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Marktguru.BusinessLogic.Users
{
    /// <summary>
    /// Purpose of the Class is to Authenticate the user.
    /// Authentication procedure includes - 
    /// Creating the JWT for a User, Validating the JWT for a User
    /// and Finding a user by User Name.
    /// </summary>
    public class UserRepository
    {
        private SymmetricSecurityKey PrivateSymmetricKey
        {
            get
            {
                return new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration.PrivateKey));
            }
        }

        private JWTConfigurationSettings Configuration { get; }
        
        public UserRepository(JWTConfigurationSettings JWTConfig) 
        {
            Configuration = JWTConfig;
        }

        /// <summary>
        /// Get user bt the username
        /// </summary>
        /// <param name="UserName">string</param>
        /// <returns>User?</returns>
        public User? GetUserByUserName(string UserName)
        {
            return User.AllUsers.Where(x => x.UserName == UserName).FirstOrDefault();
        }

        /// <summary>
        /// Log in the user and return the JWT
        /// </summary>
        /// <param name="loginCredentials">LoginCredentialsDataModel</param>
        /// <returns>string?</returns>
        public string? UserLogin(LoginCredentialsDataModel loginCredentials)
        {
            try
            {
                //Get user from DB/Azure/Any source
                var user = GetUser(loginCredentials.UserName, loginCredentials.Password);

                //Don't genrate the token if User doesn't exist.
                if (user == null)
                {
                    return null;
                }

                //User is found. Now lets allow the application to trust the user and create JWT.
                //Lets have some claims for the user.
                //Since it is dummy application so I am adding the same email address for all users :). 
                //Ideally the claims should be unique to a user, that user can share,
                //so that we can understand the specifics of the user.
                //It should not contain too sensistive data, as JWT can easily be decoded.
                var claimsList = new List<Claim>()
                {
                    new Claim("UserName", loginCredentials.UserName),
                    new Claim("email", "someEmail@website.com"),
                };

                //We can add the user roles to the claims. This is also an identity for the user.
                //These roles will help us identify what the user is authorized to.
                //A user can have multiple roles.
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

        private User? GetUser(string UserName, string Password)
        {
            return User.AllUsers.Where(x => x.UserName == UserName && x.Password == Password).FirstOrDefault();
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
    }
}
