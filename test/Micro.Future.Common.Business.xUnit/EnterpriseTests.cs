using Micro.Future.Commo.Business.Abstraction.BizInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using Microsoft.Extensions.DependencyInjection;
using Micro.Future.Commo.Business.Abstraction.BizObject;

namespace Micro.Future.Common.Business.xUnit
{
    public class EnterpriseTests : BaseTest
    {
        private IEnterpriseManager _enterpriseManager = null;
        public EnterpriseTests()
        {
            _enterpriseManager = serviceProvider.GetService<IEnterpriseManager>();
        }


        [Fact]
        public void Test_QueryEnterpriseInfo()
        {
            int enterpriseId = 205;
            var enterpriseInfo =  _enterpriseManager.QueryEnterpriseInfo(enterpriseId);

            Assert.NotNull(enterpriseInfo);
        }

        [Fact]
        public void Test_UpdateEnterpriseInfo()
        {
            int enterpriseId = 205;
            var enterpriseInfo = _enterpriseManager.QueryEnterpriseInfo(enterpriseId);
            enterpriseInfo.InvoiceMaterial = "test111";
            enterpriseInfo.RegisterWarehouse = "上海金山xxx仓库";
            enterpriseInfo.MaxTradeAmountPerMonth = "10亿";
            enterpriseInfo.IsAcceptanceBillETicket = true;


            var result =  _enterpriseManager.UpdateEnterprise(enterpriseInfo);

            Assert.True(result.Result);

            enterpriseInfo = _enterpriseManager.QueryEnterpriseInfo(enterpriseId);

            Assert.Equal<string>("111111", enterpriseInfo.Address);
        }

        [Fact]
        public void Test_UpdateEnterpriseState()
        {
            int enterpriseId = 100;
            var result = _enterpriseManager.UpdateEnterpriseState(enterpriseId, Commo.Business.Abstraction.BizObject.EnterpriseStateType.REJECTED);

            var enterpriseInfo = _enterpriseManager.QueryEnterpriseInfo(enterpriseId);

            Assert.Equal<Commo.Business.Abstraction.BizObject.EnterpriseStateType>(Commo.Business.Abstraction.BizObject.EnterpriseStateType.REJECTED, enterpriseInfo.EnterpriseState);
        }

        [Fact]
        public void Test_QueryEnterprises()
        {
            var result = _enterpriseManager.QueryEnterprises(null, Commo.Business.Abstraction.BizObject.EnterpriseStateType.UNAPPROVED);
            Assert.False(result.HasError);
            Assert.NotNull(result.Result);
            Assert.NotEqual<int>(0, result.Result.Count);
        }

        [Fact]
        public void Test_AddEnterprise()
        {
            EnterpriseInfo enterprise = new EnterpriseInfo()
            {
                Name = "mm company",
                Address = "mm address",
                EmailAddress = "mm@mm.com",
                Contacts = "mm",
                MobilePhone = "13568745893",
                Fax = "1234556"
            };

           var result =  _enterpriseManager.AddEnterprise(enterprise);

        }
    }
}
