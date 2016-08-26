using Micro.Future.Business.MatchMaker.Abstraction.Models;
using Micro.Future.Business.MatchMaker.Commo.Models;
using Micro.Future.Business.MongoDB.Commo.Handler;
using Micro.Future.Commo.Business.Abstraction.BizInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Micro.Future.Commo.Business.Requirement.Handler
{
    public class MatchMakerManager : IMatchMakerManager
    {
        private BaseMatchMaker _matchMaker = null;

        public MatchMakerManager()
        {
            MatcherHandler matcherHandler = new MatcherHandler();
            _matchMaker = new RankingMatchMaker(matcherHandler);
        }

        public MatchMakerManager(BaseMatchMaker matchMaker)
        {
            _matchMaker = matchMaker;
        }

        public void Make()
        {
            _matchMaker.make();
        }
    }
}
