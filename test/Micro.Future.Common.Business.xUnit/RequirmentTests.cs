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
            string userId = "0";
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

        //[Fact]
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

            //UserInfo user = null;
            //for (int i = fromId; i <= fromId + totalCount; i++)
            //{
            //    user = new UserInfo();
            //    user.UserId = i;
            //    user.UserName = i.ToString();
            //    user.EnterpriseId = i;

            //    fakeUsers.Add(user);
            //}

            return fakeUsers;
        }


        [Fact]
        public void Test_AddRequirements()
        {
            //read csv file to table
            string[][] tableuser = ReadCvs("E:\\PICT\\csv\\User.csv");
            string[][] tablebuy = ReadCvs("E:\\PICT\\csv\\buy.csv");
            string[][] tablesal = ReadCvs("E:\\PICT\\csv\\sal.csv");
            string[][] tablesub = ReadCvs("E:\\PICT\\csv\\sub.csv");

            IRequirementManager manager = new RequirementManager();

            int internalcnt = 0;
            for (int i = 0; i < 3; i++)
            {                
                for (int j = 0+ internalcnt; j < 3 + internalcnt; j++)
                {

                    RequirementInfo requirementbuy = new RequirementInfo();
                    //add 10 buy requriements 
                    requirementbuy.UserId = tableuser[i][0];
                    requirementbuy.EnterpriseId = Convert.ToInt16(tableuser[i][1]);

                    requirementbuy.CreateTime = DateTime.Now;
                    List<RequirementRuleInfo> rulesbuy = new List<RequirementRuleInfo>();
                    RequirementRuleInfo rulebuy = new RequirementRuleInfo();

                    requirementbuy.State = RequirementState.OPEN;
                    requirementbuy.Type = RequirementType.Buy;
                    requirementbuy.PaymentAmount = Convert.ToDecimal(tablebuy[j][2]);
                    requirementbuy.PaymentDateTime = tablebuy[j][3];
                    requirementbuy.PaymentType = tablebuy[j][4];
                    requirementbuy.WarehouseAccount = tablebuy[j][5];
                    requirementbuy.ProductType = tablebuy[j][6];
                    requirementbuy.ProductName = tablebuy[j][7];
                    requirementbuy.ProductSpecification = tablebuy[j][8];
                    requirementbuy.ProductPrice = Convert.ToDecimal(tablebuy[j][9]);
                    requirementbuy.ProductQuantity = Convert.ToDecimal(tablebuy[j][10]);
                    requirementbuy.WarehouseAddress1 = tablebuy[j][11];
                    requirementbuy.InvoiceValue = tablebuy[j][12];
                    requirementbuy.InvoiceIssueDateTime = tablebuy[j][13];
                    requirementbuy.InvoiceTransferMode = tablebuy[j][14];

                    rulebuy.RuleType = 1;
                    rulebuy.Key = tablebuy[j][16];
                    rulebuy.Value = tablebuy[j][17];

                    rulesbuy.Add(rulebuy);
                    requirementbuy.Rules = rulesbuy;

                    BizTResult<RequirementInfo> bizResultbuy = manager.AddRequirementInfo(requirementbuy);

                    Assert.False(bizResultbuy.HasError);
                    Assert.NotNull(bizResultbuy.Result);

                    //add 10 sal requirements
                    RequirementInfo requirementsal = new RequirementInfo();

                    requirementsal.UserId = tableuser[i][0];
                    requirementsal.EnterpriseId = Convert.ToInt16(tableuser[i][1]);

                    requirementsal.CreateTime = DateTime.Now;
                    List<RequirementRuleInfo> rulessal = new List<RequirementRuleInfo>();
                    RequirementRuleInfo rulesal = new RequirementRuleInfo();

                    requirementsal.State = RequirementState.OPEN;
                    requirementsal.Type = RequirementType.Sale;
                    requirementsal.ProductType = tablesal[j][2];
                    requirementsal.ProductName = tablesal[j][3];
                    requirementsal.ProductSpecification = tablesal[j][4];
                    requirementsal.ProductPrice = Convert.ToDecimal(tablesal[j][5]);
                    requirementsal.ProductQuantity = Convert.ToDecimal(tablesal[j][6]);
                    requirementsal.WarehouseAddress1 = tablesal[j][7];
                    requirementsal.InvoiceValue = tablesal[j][8];
                    requirementsal.InvoiceIssueDateTime = tablesal[j][9];
                    requirementsal.InvoiceTransferMode = tablesal[j][10];

                    rulesal.RuleType = 1;
                    rulesal.Key = tablesal[j][12];
                    rulesal.Value = tablesal[j][13];

                    rulessal.Add(rulesal);
                    requirementsal.Rules = rulessal;

                    BizTResult<RequirementInfo> bizResultsal = manager.AddRequirementInfo(requirementsal);

                    Assert.False(bizResultsal.HasError);
                    Assert.NotNull(bizResultsal.Result);

                    //add 10 sub requirements
                    RequirementInfo requirementsub = new RequirementInfo();

                    requirementsub.UserId = tableuser[i][0];
                    requirementsub.EnterpriseId = Convert.ToInt16(tableuser[i][1]);

                    requirementsub.CreateTime = DateTime.Now;
                    List<RequirementRuleInfo> rulessub = new List<RequirementRuleInfo>();
                    RequirementRuleInfo rulesub = new RequirementRuleInfo();

                    requirementsub.State = RequirementState.OPEN;
                    requirementsub.Type = RequirementType.Subsidy;
                    requirementsub.TradeAmount = Convert.ToDecimal(tablesub[j][0]);
                    requirementsub.TradeProfit = Convert.ToDecimal(tablesub[j][1]);
                    requirementsub.BusinessRange = tablesub[j][2];
                    requirementsub.InvoiceValue = tablesub[j][3];
                    requirementsub.InvoiceIssueDateTime = tablesub[j][4];
                    requirementsub.InvoiceTransferMode = tablesub[j][5];

                    rulesub.RuleType = 1;
                    rulesub.Key = tablesub[j][6]; ;
                    rulesub.Value = tablesub[j][7];

                    rulessub.Add(rulesub);
                    requirementsub.Rules = rulessub;

                    BizTResult<RequirementInfo> bizResultsub = manager.AddRequirementInfo(requirementsub);

                    Assert.False(bizResultsub.HasError);
                    Assert.NotNull(bizResultsub.Result);

                }

                internalcnt = internalcnt + 3;

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
                string[] sArray = line.Split(',');
                list.Add(sArray);
            }
            return list.ToArray();
        }

    }


}
