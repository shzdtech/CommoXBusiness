using Micro.Future.Commo.Business.Abstraction.BizInterface;
using Micro.Future.Commo.Business.Abstraction.BizObject;
using Micro.Future.Commo.Business.Requirement.Handler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using Microsoft.Extensions.DependencyInjection;

namespace Micro.Future.Common.Business.xUnit
{
    public class ChainTests : BaseTest
    {
        private IChainManager _chainManager = null;

        public ChainTests()
        {
            _chainManager = serviceProvider.GetService<IChainManager>();
        }

        [Fact]
        public void Test_QueryAllChains()
        {
            var bizResult = _chainManager.QueryAllChains(Commo.Business.Abstraction.BizObject.ChainStatusType.CONFIRMED);
        }

        [Fact]
        public void Test_QueryChainsByUserId()
        {
            string userId = "66770166-b0ce-47be-b3a9-c913e245cab3";
            var allChains = _chainManager.QueryChainsByUserId(userId, Commo.Business.Abstraction.BizObject.ChainStatusType.OPEN);
        }

        [Fact]
        public void Test_QueryChainsByRequirementId()
        {
            int requirementId = 6441;

            var bizResult = _chainManager.QueryChainsByRequirementId(requirementId, ChainStatusType.OPEN);
        }


        [Fact]
        public void Test_LockUnlockChain()
        {
            int chainId = 11404;
            string userId = "110022";
            var chainInfo = _chainManager.GetChainInfo(chainId);

            if(chainInfo.ChainStatus == ChainStatusType.OPEN)
            {
                var lockSuccess = _chainManager.LockChain(userId, chainId);
                Assert.True(lockSuccess);

                chainInfo = _chainManager.GetChainInfo(chainId);
                Assert.Equal<ChainStatusType>(chainInfo.ChainStatus, ChainStatusType.LOCKED);

                var unlockSuccess = _chainManager.UnlockChain(userId, chainId);
                Assert.True(unlockSuccess);

                chainInfo = _chainManager.GetChainInfo(chainId);
                Assert.Equal<ChainStatusType>(chainInfo.ChainStatus, ChainStatusType.OPEN);
            }
            else if(chainInfo.ChainStatus == ChainStatusType.LOCKED)
            {
                var unlockSuccess = _chainManager.UnlockChain(userId, chainId);
                Assert.True(unlockSuccess);

                chainInfo = _chainManager.GetChainInfo(chainId);
                Assert.Equal<ChainStatusType>(chainInfo.ChainStatus, ChainStatusType.OPEN);

                var lockSuccess = _chainManager.LockChain(userId, chainId);
                Assert.True(lockSuccess);

                chainInfo = _chainManager.GetChainInfo(chainId);
                Assert.Equal<ChainStatusType>(chainInfo.ChainStatus, ChainStatusType.LOCKED);
            }
            else if(chainInfo.ChainStatus == ChainStatusType.CONFIRMED)
            {
                var lockSuccess = _chainManager.LockChain(userId, chainId);
                Assert.True(lockSuccess);

                chainInfo = _chainManager.GetChainInfo(chainId);
                Assert.Equal<ChainStatusType>(chainInfo.ChainStatus, ChainStatusType.LOCKED);
            }
        }

        [Fact]
        public void Test_ConfirmChain()
        {
            int chainId = 10088;
            string userId = "110022";
            int tradeId = 0;
            var isConfirmed = _chainManager.ComfirmChain(userId, chainId, out tradeId);

            Assert.True(isConfirmed);
            Assert.NotEqual<int>(tradeId, 0);
        }

        [Fact]
        public void Test_FindReplaceRequirement()
        {
            int chanId = 11417;
            var requirments = _chainManager.FindReplacedRequirementsForChain(chanId, 0);
        }

        [Fact]
        public void Test_ReplaceChainRequirement()
        {
            int chainId = 11417;
            var result = _chainManager.ReplaceRequirementsForChain(chainId, new List<int>() { 0 }, new List<int> { 6679 });
        }


        [Fact]
        public void AutoMatchRequirements()
        {
            IList<int> requirmentIds = new List<int>() { -1, 1022, 0, 1023 };

            var chain = _chainManager.AutoMatchRequirements("111", requirmentIds, null, true);
        }
    }
}
