using API.Data;
using API.Handlers.FunctionalCharacteristic;
using GAE.AIQ.API.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Handlers
{
    public class AuthorizationPolicyProvider : DefaultAuthorizationPolicyProvider
    {
        private readonly AuthorizationOptions _options;
        private readonly ApplicationDbContext _dbContext;

        public AuthorizationPolicyProvider(IOptions<AuthorizationOptions> options, IDataBaseFactory dataBaseFactory) : base(options)
        {
            _dbContext = dataBaseFactory.GetContext();
            _options = options.Value;

        }

        public override async Task<AuthorizationPolicy> GetPolicyAsync(string policyName)
        {

            var policy = await base.GetPolicyAsync(policyName);

            if (policy == null)
            {

                List<Data.Entities.FunctionalCharacteristic> Characteristics = await FunctionalCharacteristicSingleton.Instance.Characteristics(_dbContext);
                var poli = Characteristics.Where(z => z.Name == policyName).FirstOrDefault();
                foreach (var item in Characteristics)
                {
                    _options.AddPolicy(item.Name, policy => policy.Requirements.Add(new PermissionRequirement(item.Id)));
                }


                policy = new AuthorizationPolicyBuilder()
                   .AddRequirements(new PermissionRequirement(poli.Id))
                   .Build();

            }

            return policy;
        }
    }
}
