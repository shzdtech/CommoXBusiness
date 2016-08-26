using Micro.Future.Commo.Business.Abstraction.BizInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace Micro.Future.Common.Business.xUnit
{
    public class MatchMakerTests : BaseTest
    {
        private IMatchMakerManager _makerManager = null;

        public MatchMakerTests()
        {
            _makerManager = serviceProvider.GetService<IMatchMakerManager>();
        }

        [Fact]
        public void Test_Make()
        {
            _makerManager.Make();
        }
    }
}
