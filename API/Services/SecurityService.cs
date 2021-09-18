using API.Data.Entities;
using API.Data.Models;
using API.Data.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace API.Services
{
    public class SecurityService : ISecurityService
    {
        private readonly IUserRepository usersRepository;
        private readonly IFunctionalCharacteristicByRoleRepository functionalCharacteristicByRoleRepository;
        private readonly IFunctionalCharacteristicRepository functionalCharacteristicRepository;
        private readonly IOptions<ApiConfig> apiConfig;
        public SecurityService(IUserRepository _usersRepository, IFunctionalCharacteristicByRoleRepository _functionalCharacteristicByRoleRepository, IFunctionalCharacteristicRepository _functionalCharacteristicRepository, IOptions<ApiConfig> _apiConfig)
        {
            usersRepository = _usersRepository;
            apiConfig = _apiConfig;
            functionalCharacteristicByRoleRepository = _functionalCharacteristicByRoleRepository;
            functionalCharacteristicRepository = _functionalCharacteristicRepository;
        }
        public async Task<IActionResult> Login(AuthInput authInput)
        {
            try
            {
                var user = await usersRepository.GetUserByName(authInput.UserName);

                #region Validations



                if (user == null)
                {
                    return new UnauthorizedObjectResult(new ResultError() { Type = "No autorizado", Message = "Usuario / Clave incorrectos" });
                }
                //se delega la responsabilidad del hashing al frontEnd
                if (user.Password != authInput.Password)
                {
                    return new UnauthorizedObjectResult(new ResultError() { Type = "No autorizado", Message = "Usuario / Clave incorrectos" });
                }
                #endregion

                #region TokenCreation

                int permissions = await GetPermissionsKey(user.RoleId);
                // authentication successful so generate jwt token
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(apiConfig.Value.Secret);
                var claims = new List<Claim>
                                {
                                        new Claim(ClaimTypes.Name, user.UserName),
                                        new Claim(ClaimTypes.Email,user.Email),
                                        new Claim("userId",user.Id.ToString()),
                                        new Claim("permissions",permissions.ToString())
                                };


                claims.Add(new Claim(ClaimTypes.Role, user.Role.Name));
                claims.Add(new Claim("assignedRolesId", user.Role.Id.ToString()));

                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(claims),
                    Expires = DateTime.UtcNow.AddHours(apiConfig.Value.ExpirationHoursCredentials),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                };
                var token = tokenHandler.CreateToken(tokenDescriptor);
                string tokenResult = tokenHandler.WriteToken(token);

                return new OkObjectResult(new { tokens = tokenResult });
            }
            catch (Exception ex)
            {
                return new NotFoundObjectResult(new ResultError() { Type = "Excepcion", Message = ex.Message });
            }
            #endregion
        }

        private async Task<List<PermissionInfo>> GetPermissionsList(int roleId)
        {
            List<PermissionInfo> permissionsInfo = new List<PermissionInfo>();

            List<FunctionalCharacteristicByRole> functionalCharacteristicsByRole = await functionalCharacteristicByRoleRepository.FindElementsForRoleId(roleId);

            List<FunctionalCharacteristic> allFunctionalCharacteristic = await functionalCharacteristicRepository.GetAll();

            foreach (FunctionalCharacteristic functionalCharacteristic in allFunctionalCharacteristic)
            {
                PermissionInfo permissionInfo = new PermissionInfo();
                permissionInfo.Id = functionalCharacteristic.Id;
                permissionInfo.Name = functionalCharacteristic.Name;
                permissionInfo.Granted = false;

                if (functionalCharacteristicsByRole.Any(x => x.FunctionalCharacteristicId.Equals(functionalCharacteristic.Id)))
                {
                    permissionInfo.Granted = true;
                }

                permissionsInfo.Add(permissionInfo);
            }

            return permissionsInfo;

        }

        public async Task<int> GetPermissionsKey(int roleId)
        {
            List<PermissionInfo> permissions = await GetPermissionsList(roleId);
            string binary = string.Join("", permissions.Select(x => Convert.ToInt32(x.Granted)).ToList());
            int numberBinaryPermmission = Convert.ToInt32(binary, 2);
            return numberBinaryPermmission;
        }
    }
}
