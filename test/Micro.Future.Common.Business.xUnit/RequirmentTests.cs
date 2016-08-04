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

        private static List<UserInfo> fakeUsers = null;

        public RequirmentTests()
        {
            fakeUsers = CreateFakeUsers(1000, 20);
        }



        [Fact]
        public void Test_AddRequirement()
        {
            IRequirementManager manager = new RequirementManager();

            UserInfo user = fakeUsers[0];

            RequirementInfo requirement = new RequirementInfo();
            requirement.UserId = user.UserId;
            requirement.EnterpriseId = user.EnterpriseId;

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



        /// <summary>
        /// 创建一些测试用户数据
        /// </summary>
        /// <param name="fromId">起始的UserId</param>
        /// <param name="totalCount">创建多少个用户</param>
        /// <returns></returns>
        private static List<UserInfo> CreateFakeUsers(int fromId, int totalCount)
        {
            List<UserInfo> fakeUsers = new List<UserInfo>();

            UserInfo user = null;
            for (int i = fromId; i <= fromId + totalCount; i++)
            {
                user = new UserInfo();
                user.UserId = i;
                user.UserName = i.ToString();
                user.EnterpriseId = i;

                fakeUsers.Add(user);
            }

            return fakeUsers;
        }
    }

   
}
