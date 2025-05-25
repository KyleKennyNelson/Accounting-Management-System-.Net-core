using Microsoft.AspNetCore.Authorization;

namespace api.Models.AuthModels
{
    public class PolicyRequirementModel : IAuthorizationRequirement
    {
        public string PolicyName { get; }

        public PolicyRequirementModel(string policyName)
        {
            PolicyName = policyName;
        }
    }
}
