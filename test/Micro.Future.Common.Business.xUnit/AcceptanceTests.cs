using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Micro.Future.Commo.Business.Abstraction.BizInterface;
using Xunit;
using Micro.Future.Commo.Business.Abstraction.BizObject;

namespace Micro.Future.Common.Business.xUnit
{
    public class AcceptanceTests : BaseTest
    {
        private IAcceptanceManager _acceptanceManager = null;

        public AcceptanceTests()
        {
            _acceptanceManager = serviceProvider.GetService<IAcceptanceManager>();
        }

        [Fact]
        public void Test_QueryAllAcceptance()
        {
            var results = _acceptanceManager.QueryAllAcceptance();
        }

        [Fact]
        public void Test_AddAcceptance()
        {
            AcceptanceInfo acc = new AcceptanceInfo()
            {
                AcceptanceType = "电票",
                Amount = 100,
                BankName = "中国银行",
                CreateTime = DateTime.Now,
                DrawTime = DateTime.Now.AddDays(30),
                DueDate = DateTime.Now.AddMonths(10),
            };

            var accId = _acceptanceManager.CreateAcceptance(acc);
        }


    }
}
