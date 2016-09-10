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

using Microsoft.Extensions.DependencyInjection;


namespace Micro.Future.Common.Business.xUnit
{
    public class RequirmentTests : BaseTest
    {
        private IRequirementManager manager = null;
        private static List<UserInfo> fakeUsers = null;

        public RequirmentTests()
        {
            //fakeUsers = CreateFakeUsers(1000, 50);
            manager = serviceProvider.GetService<IRequirementManager>();
        }

        [Fact]
        public void Test_QueryRequirementsByUser()
        {
            //1675
            string userId = "0";
            var bizResult =  manager.QueryRequirements(userId);

            Assert.False(bizResult.HasError);
            Assert.NotNull(bizResult.Result);

            var requirements = bizResult.Result;
            int count = requirements.Count();
            Assert.NotEqual<int>(count, 0);
        }




        [Fact]
        public void Test_SearchRequirements()
        {
            //1675

            RequirementSearchCriteria criteria = new RequirementSearchCriteria()
            {
                ProductName = "铁,锌",
                PageNo = 1,
                PageSize = 10
            };


            criteria.OrderByFields = new List<OrderByInfo>()
            {
                new OrderByInfo()
                {
                     Field = "CreateTime",
                     OrderBy = "desc"
                }
            };


            var bizResult = manager.SearchRequirements(criteria);
            
        }


        [Fact]
        public void Test_QueryRequirementsByEnterpriseId()
        {
            //1675
            int enterpriseId = 123;
            var bizResult = manager.QueryRequirementsByEnterpriseId(enterpriseId,null);

            Assert.False(bizResult.HasError);
            Assert.NotNull(bizResult.Result);

            var requirements = bizResult.Result;
            int count = requirements.Count();
            Assert.NotEqual<int>(count, 0);
        }

        [Fact]
        public void Test_QueryRequirementInfo()
        {
            //1675
            int requirementId = 5743;
            var bizResult = manager.QueryRequirementInfo(requirementId);

            Assert.False(bizResult.HasError);
            Assert.NotNull(bizResult.Result);

            var requirements = bizResult.Result;
            Assert.NotEqual<int>(requirements.RequirementId, 0);
        }

        [Fact]
        public void Test_QueryRequirementChains()
        {
            //1675
            int requirementId = 5743;
            var bizResult = manager.QueryRequirementChains(requirementId);

            Assert.False(bizResult.HasError);
            Assert.NotNull(bizResult.Result);

            var requirements = bizResult.Result;
            Assert.NotEqual<int>(requirements.Count(), 0);
        }

        [Fact]
        public void Test_AddRequirement()
        {

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
            rule.Key = RequirementRuleNames.EnterpriseNature;
            rule.Value = "国企";
            rule.OperationType = RequirementRuleOperation.EQUAL;
            rules.Add(rule);

            //注册资本500万以上
            rule = new RequirementRuleInfo();
            rule.Key = RequirementRuleNames.EnterpriseRegisteredCaptial;
            rule.Value = "5000000";
            rule.OperationType = RequirementRuleOperation.GREATER;
            rules.Add(rule);

            //注册地
            rule = new RequirementRuleInfo();
            rule.Key = RequirementRuleNames.EnterpriseRegisteredState;
            rule.Value = "上海";
            rule.OperationType = RequirementRuleOperation.GREATER;
            rules.Add(rule);


            //支付方式，现金
            rule = new RequirementRuleInfo();
            rule.Key = RequirementRuleNames.PaymentPayMethod;
            rule.Value = "现金";
            rule.OperationType = RequirementRuleOperation.GREATER;
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
            string folder = Path.Combine(GetImportPath(), "TestData");
            string[][] tableuser = ReadCvs(Path.Combine(folder, "User.csv"));
            string[][] tablebuy = ReadCvs(Path.Combine(folder, "buy.csv"));
            string[][] tablesal = ReadCvs(Path.Combine(folder, "sal.csv"));
            string[][] tablesub = ReadCvs(Path.Combine(folder, "sub.csv"));


            int totalUsers = tableuser.Length;

            //add 10 buy requriements 
            for (int i = 0; i < tablebuy.Length; i++)
            {
                int currentUserIndex = i % totalUsers;
                var currentUser = tableuser[currentUserIndex];

                string userId = currentUser[0];
                int enterpriseId = int.Parse(currentUser[1]);


                var testData = tablebuy[i];
                RequirementInfo requirementbuy = new RequirementInfo();
                requirementbuy.UserId = userId;
                requirementbuy.EnterpriseId = enterpriseId;
                requirementbuy.CreateTime = DateTime.Now;

                List<RequirementRuleInfo> rulesbuy = new List<RequirementRuleInfo>();
                RequirementRuleInfo rulebuy = new RequirementRuleInfo();

                requirementbuy.State = RequirementState.OPEN;
                requirementbuy.Type = RequirementType.Buy;
                requirementbuy.PaymentAmount = Convert.ToDecimal(testData[2]);
                requirementbuy.TradeAmount = requirementbuy.PaymentAmount;
                requirementbuy.PaymentDateTime = testData[3];
                requirementbuy.PaymentType = testData[4];
                requirementbuy.WarehouseAccount = testData[5];
                requirementbuy.ProductType = testData[6];
                requirementbuy.ProductName = testData[7];
                requirementbuy.ProductSpecification = testData[8];
                requirementbuy.ProductPrice = Convert.ToDecimal(testData[9]);
                requirementbuy.ProductQuantity = Convert.ToDecimal(testData[10]);
                requirementbuy.WarehouseAddress1 = testData[11];
                requirementbuy.InvoiceValue = testData[12];
                requirementbuy.InvoiceIssueDateTime = testData[13];
                requirementbuy.InvoiceTransferMode = testData[14];

                rulebuy.Key = "EnterpriseType";
                rulebuy.Value = "1";
                rulebuy.OperationType = RequirementRuleOperation.EQUAL;

                rulesbuy.Add(rulebuy);
                requirementbuy.Rules = rulesbuy;

                var bizResultbuy = manager.AddRequirementInfo(requirementbuy);

                Assert.False(bizResultbuy.HasError);
                Assert.NotNull(bizResultbuy.Result);
            }


            //add 10 sal requirements
            for (int i = 0; i < tablesal.Length; i++)
            {
                int currentUserIndex = i % totalUsers;
                var currentUser = tableuser[currentUserIndex];

                string userId = currentUser[0];
                int enterpriseId = int.Parse(currentUser[1]);


                var testData = tablesal[i];
                
                RequirementInfo requirementsal = new RequirementInfo();

                requirementsal.UserId = userId;
                requirementsal.EnterpriseId = enterpriseId;

                requirementsal.CreateTime = DateTime.Now;
                List<RequirementRuleInfo> rulessal = new List<RequirementRuleInfo>();
                RequirementRuleInfo rulesal = new RequirementRuleInfo();

                requirementsal.State = RequirementState.OPEN;
                requirementsal.Type = RequirementType.Sale;
                requirementsal.ProductType = testData[2];
                requirementsal.ProductName = testData[3];
                requirementsal.ProductSpecification = testData[4];
                requirementsal.ProductPrice = Convert.ToDecimal(testData[5]);
                requirementsal.ProductQuantity = Convert.ToDecimal(testData[6]);
                requirementsal.WarehouseAddress1 = testData[7];
                requirementsal.InvoiceValue = testData[8];
                requirementsal.InvoiceIssueDateTime = testData[9];
                requirementsal.InvoiceTransferMode = testData[10];

                rulesal.Key = testData[12];
                rulesal.Value = testData[13];
                rulesal.OperationType = RequirementRuleOperation.EQUAL;

                rulessal.Add(rulesal);
                requirementsal.Rules = rulessal;

                BizTResult<RequirementInfo> bizResultsal = manager.AddRequirementInfo(requirementsal);

                Assert.False(bizResultsal.HasError);
                Assert.NotNull(bizResultsal.Result);
            }

            //add 10 sub requirements
            for (int i = 0; i < tablesub.Length; i++)
            {
                int currentUserIndex = i % totalUsers;
                var currentUser = tableuser[currentUserIndex];

                string userId = currentUser[0];
                int enterpriseId = int.Parse(currentUser[1]);


                var testData = tablesub[i];
                
                RequirementInfo requirementsub = new RequirementInfo();

                requirementsub.UserId = userId;
                requirementsub.EnterpriseId = enterpriseId;
                requirementsub.CreateTime = DateTime.Now;
                List<RequirementRuleInfo> rulessub = new List<RequirementRuleInfo>();
                RequirementRuleInfo rulesub = new RequirementRuleInfo();

                requirementsub.State = RequirementState.OPEN;
                requirementsub.Type = RequirementType.Subsidy;
                requirementsub.TradeAmount = Convert.ToDecimal(testData[0]);
                requirementsub.TradeProfit = Convert.ToDecimal(testData[1]);
                requirementsub.BusinessRange = testData[2];
                requirementsub.InvoiceValue = testData[3];
                requirementsub.InvoiceIssueDateTime = testData[4];
                requirementsub.InvoiceTransferMode = testData[5];

                
                rulesub.Key = testData[6]; ;
                rulesub.Value = testData[7];
                rulesub.OperationType = RequirementRuleOperation.EQUAL;

                rulessub.Add(rulesub);
                requirementsub.Rules = rulessub;

                BizTResult<RequirementInfo> bizResultsub = manager.AddRequirementInfo(requirementsub);

                Assert.False(bizResultsub.HasError);
                Assert.NotNull(bizResultsub.Result);
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


        public static string GetImportPath()
        {
            string baseFolder = AppDomain.CurrentDomain.BaseDirectory;

            return baseFolder.Substring(0, baseFolder.LastIndexOf(@"bin\Debug"));
        }
    }


}
