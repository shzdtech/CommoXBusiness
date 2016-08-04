using Micro.Future.Commo.Business.Abstraction.BizInterface;
using Micro.Future.Commo.Business.Requirement.Handler;
using Micro.Future.Commo.Business.Abstraction.BizObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using Micro.Future.Business.Common;

namespace Micro.Future.Common.Business.xUnit
{
    public class RequirmentTests
    {
        public RequirmentTests()
        {
            IRequirementManager manager = new RequirementManager();
            manager.AddRequirementInfo(new Commo.Business.Abstraction.BizObject.RequirementInfo());

            var info = new Commo.Business.Abstraction.BizObject.RequirementInfo();
            var rules = new List<RequirementRuleInfo>();
            rules.Add(new RequirementRuleInfo());
            info.Rules = rules;
        }

        [Fact]
        public void Test_AddRequirement()
        {
            IRequirementManager manager = new RequirementManager();

            RequirementInfo requirement = new RequirementInfo();
            requirement.UserId = 1111;
            requirement.EnterpriseId = 1111;

            requirement.ProductName = "铜";
            requirement.Type = RequirementType.Sale;
            requirement.WarehouseState = "浙江";
            requirement.WarehouseCity = "杭州";
            requirement.WarehouseAddress1 = "滨江区文山路xxx号";
            requirement.ProductPrice = 1000m;
            requirement.ProductQuantity = 100;
            requirement.ProductUnit = "吨";


            List<RequirementRuleInfo> rules = new List<RequirementRuleInfo>();

            //必须是国有企业
            RequirementRuleInfo rule = new RequirementRuleInfo();
            rule.RuleType = 1;
            rule.Key = "企业类型";
            rule.Value = "国企";
            rule.OperationType = RequirementRuleOperation.Equal;
            rules.Add(rule);

            //注册资本500万以上
            rule = new RequirementRuleInfo();
            rule.RuleType = 1;
            rule.Key = "注册资本";
            rule.Value = "5000000";
            rule.OperationType = RequirementRuleOperation.MoreThan;
            rules.Add(rule);

            requirement.Rules = rules;

            BizTResult<bool> bizResult = manager.AddRequirementInfo(requirement);

            Assert.False(bizResult.HasError);
            Assert.True(bizResult.Result);

        }
    }

   
}
