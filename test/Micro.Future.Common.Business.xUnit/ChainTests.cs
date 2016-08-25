﻿using Micro.Future.Commo.Business.Abstraction.BizInterface;
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

        //[Fact]
        public void Test_QueryAllChains()
        {
            var allChains = _chainManager.QueryAllChains(Commo.Business.Abstraction.BizObject.ChainStatusType.OPEN);
        }

        //[Fact]
        public void Test_QueryChainsByUserId()
        {
            string userId = "103";
            var allChains = _chainManager.QueryChainsByUserId(userId, Commo.Business.Abstraction.BizObject.ChainStatusType.OPEN);
        }


        //[Fact]
        public void Test_LockUnlockChain()
        {
            int chainId = 11028;
            var chainInfo = _chainManager.GetChainInfo(chainId);

            if(chainInfo.ChainStatus == ChainStatusType.OPEN)
            {
                var lockSuccess = _chainManager.LockChain(chainId);
                Assert.True(lockSuccess);

                chainInfo = _chainManager.GetChainInfo(chainId);
                Assert.Equal<ChainStatusType>(chainInfo.ChainStatus, ChainStatusType.LOCKED);

                var unlockSuccess = _chainManager.UnlockChain(chainId);
                Assert.True(unlockSuccess);

                chainInfo = _chainManager.GetChainInfo(chainId);
                Assert.Equal<ChainStatusType>(chainInfo.ChainStatus, ChainStatusType.OPEN);
            }
            else if(chainInfo.ChainStatus == ChainStatusType.LOCKED)
            {
                var unlockSuccess = _chainManager.UnlockChain(chainId);
                Assert.True(unlockSuccess);

                chainInfo = _chainManager.GetChainInfo(chainId);
                Assert.Equal<ChainStatusType>(chainInfo.ChainStatus, ChainStatusType.OPEN);

                var lockSuccess = _chainManager.LockChain(chainId);
                Assert.True(lockSuccess);

                chainInfo = _chainManager.GetChainInfo(chainId);
                Assert.Equal<ChainStatusType>(chainInfo.ChainStatus, ChainStatusType.LOCKED);
            }
        }

        //[Fact]
        public void Test_ConfirmChain()
        {
            int chainId = 11028;
            int tradeId = 0;
            var isConfirmed = _chainManager.ComfirmChain(chainId, out tradeId);

            Assert.True(isConfirmed);
            Assert.NotEqual<int>(tradeId, 0);
        }
    }
}