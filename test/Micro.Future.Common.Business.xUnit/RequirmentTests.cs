using Micro.Future.Commo.Business.Abstraction.BizInterface;
using Micro.Future.Commo.Business.Requirement.Handler;
using Micro.Future.Commo.Business.Abstraction.BizObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using Micro.Future.Business.Common;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Text;



namespace Micro.Future.Common.Business.xUnit
{
    public class RequirmentTests
    {

        private static List<UserInfo> fakeUsers = null;

        public RequirmentTests()
        {
            fakeUsers = CreateFakeUsers(1000, 50);
        }

        //[Fact]
        public void Test_QueryRequirementsByUser()
        {
            //1675
            int userId = 0;
            IRequirementManager manager = new RequirementManager();
            var bizResult =  manager.QueryRequirements(userId);

            Assert.False(bizResult.HasError);
            Assert.NotNull(bizResult.Result);

            var requirements = bizResult.Result;
            int count = requirements.Count();
            Assert.NotEqual<int>(count, 0);
        }

        //[Fact]
        public void Test_QueryRequirementInfo()
        {
            //1675
            int requirementId = 5743;
            IRequirementManager manager = new RequirementManager();
            var bizResult = manager.QueryRequirementInfo(requirementId);

            Assert.False(bizResult.HasError);
            Assert.NotNull(bizResult.Result);

            var requirements = bizResult.Result;
            Assert.NotEqual<int>(requirements.RequirementId, 0);
        }

        //[Fact]
        public void Test_QueryRequirementChains()
        {
            //1675
            int requirementId = 5743;
            IRequirementManager manager = new RequirementManager();
            var bizResult = manager.QueryRequirementChains(requirementId);

            Assert.False(bizResult.HasError);
            Assert.NotNull(bizResult.Result);

            var requirements = bizResult.Result;
            Assert.NotEqual<int>(requirements.Count(), 0);
        }

        [Fact]
        public void Test_AddRequirement()
        {
            IRequirementManager manager = new RequirementManager();

            UserInfo user = fakeUsers[0];

            RequirementInfo requirement = new RequirementInfo();
            requirement.UserId = user.UserId;
            requirement.EnterpriseId = user.EnterpriseId;

            //requirement.ProductName = "铜";
            requirement.Type = RequirementType.Subsidy;
            //requirement.WarehouseState = "浙江";
            //requirement.WarehouseCity = "杭州";
            //requirement.WarehouseAddress1 = "滨江区文山路xxx号";
            //requirement.ProductPrice = 1000m;
            //requirement.ProductQuantity = 100;
            //requirement.ProductUnit = "吨";

            requirement.TradeAmount = 200000;
            //requirement.Subsidies = 20;


            List<RequirementRuleInfo> rules = new List<RequirementRuleInfo>();

            //必须是国有企业
            RequirementRuleInfo rule = new RequirementRuleInfo();
            rule.RuleType = 1;
            rule.Key = RequirementRuleNames.EnterpriseNature;
            rule.Value = "国企";
            rule.OperationType = RequirementRuleOperation.Equal;
            rules.Add(rule);

            //注册资本500万以上
            rule = new RequirementRuleInfo();
            rule.RuleType = 1;
            rule.Key = RequirementRuleNames.EnterpriseRegisteredCaptial;
            rule.Value = "5000000";
            rule.OperationType = RequirementRuleOperation.MoreThan;
            rules.Add(rule);

            //注册地
            rule = new RequirementRuleInfo();
            rule.RuleType = 1;
            rule.Key = RequirementRuleNames.EnterpriseRegisteredState;
            rule.Value = "上海";
            rule.OperationType = RequirementRuleOperation.MoreThan;
            rules.Add(rule);


            //支付方式，现金
            rule = new RequirementRuleInfo();
            rule.RuleType = 4;
            rule.Key = RequirementRuleNames.PaymentPayMethod;
            rule.Value = "现金";
            rule.OperationType = RequirementRuleOperation.MoreThan;
            rules.Add(rule);


            requirement.Rules = rules;

            BizTResult<RequirementInfo> bizResult = manager.AddRequirementInfo(requirement);

            Assert.False(bizResult.HasError);
            Assert.NotNull(bizResult.Result);

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

        //[Fact]
        public void Test_AddRequirements()
        {
            //read csv file to table
            string[][] table = ReadCvs("D:\\left.csv");

            IRequirementManager manager = new RequirementManager();

            UserInfo user = null;


            //for (int i = 0; i < 1000; i++)

            for (int i = 0; i < table.GetLength(0); i++)
            {
                if (String.Compare("None", table[i][7]) == 0)
                {
                    continue;
                }

                RequirementInfo requirement = new RequirementInfo();

                List<RequirementRuleInfo> rules = new List<RequirementRuleInfo>();
                RequirementRuleInfo rule = new RequirementRuleInfo();

                user = fakeUsers[i % 50];

                requirement.CreateTime = DateTime.Now;

                //
                requirement.UserId = user.UserId;
                requirement.EnterpriseId = user.EnterpriseId;

                requirement.ProductName = table[i][0];
                requirement.ProductPrice = 1000;
                requirement.ProductQuantity = Convert.ToDecimal(table[i][2]);
                requirement.ProductUnit = table[i][3];
                requirement.WarehouseState = table[i][4];
                requirement.WarehouseCity = table[i][5];
                requirement.WarehouseAddress1 = table[i][6];

                if (String.Compare("补贴", table[i][7]) == 0)
                {
                    requirement.Type = RequirementType.Subsidy;
                }
                if (String.Compare("买", table[i][7]) == 0)
                {
                    requirement.Type = RequirementType.Buy;
                }

                if (String.Compare("卖", table[i][7]) == 0)
                {
                    requirement.Type = RequirementType.Sale;
                }

                if (String.Compare("None", table[i][7]) == 0)
                {
                    requirement.Type = RequirementType.None;
                }

                requirement.TradeAmount = Convert.ToDecimal(table[i][8]);

                //1 = 企业规则、2 = 货物规则、3 = 资金规则、4 = 支付规则

                if (String.Compare("企业规则", table[i][9]) == 0)
                {
                    rule.RuleType = 1;

                }
                if (String.Compare("货物规则", table[i][9]) == 0)
                {
                    rule.RuleType = 2;
                }
                if (String.Compare("资金规则", table[i][9]) == 0)
                {
                    rule.RuleType = 3;
                }
                if (String.Compare("支付规则", table[i][9]) == 0)
                {
                    rule.RuleType = 4;
                }

                rule.Key = table[i][10];

                rule.Value = table[i][11];
                if (String.Compare("MoreThan", table[i][11]) == 0)
                {
                    rule.OperationType = RequirementRuleOperation.MoreThan;
                }
                if (String.Compare("LessThan", table[i][11]) == 0)
                {
                    rule.OperationType = RequirementRuleOperation.LessThan;
                }
                if (String.Compare("Equal", table[i][11]) == 0)
                {
                    rule.OperationType = RequirementRuleOperation.Equal;
                }

                rules.Add(rule);
                requirement.Rules = rules;

                BizTResult<RequirementInfo> bizResult = manager.AddRequirementInfo(requirement);

                Assert.False(bizResult.HasError);
                Assert.NotNull(bizResult.Result);
            }

        }


        public static string[][] ReadCvs(string fullname)
        {
            if (!File.Exists(fullname)) return new string[][] { };
            var lines = File.ReadAllLines(fullname).Skip(1);
            var list = new List<string[]>();
            var builder = new StringBuilder();
            foreach (var line in lines)
            {
                builder.Clear();
                var comma = false;
                var array = line.ToCharArray();
                var values = new List<string>();
                var length = array.Length - 1;
                var index = 0;
                while (index < length)
                {
                    var item = array[index++];
                    switch (item)
                    {
                        case ',':
                            if (comma)
                            {
                                builder.Append(item);
                            }
                            else
                            {
                                values.Add(builder.ToString());
                                builder.Clear();
                            }
                            break;
                        case '"':
                            comma = !comma;
                            break;
                        default:
                            builder.Append(item);
                            break;
                    }
                }
                var count = values.Count;
                if (count == 0) continue;
                list.Add(values.ToArray());
            }
            return list.ToArray();
        }


    }


}
