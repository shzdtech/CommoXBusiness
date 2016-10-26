using Micro.Future.Commo.Business.Abstraction.BizInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Micro.Future.Commo.Business.Abstraction.BizObject;
using Micro.Future.Business.DataAccess.Commo.CommonInterface;
using Micro.Future.Business.DataAccess.Commo.CommoObject;

namespace Micro.Future.Commo.Business.Requirement.Handler
{
    public class AcceptanceManager : IAcceptanceManager
    {
        private IAcceptance _acceptanceHandler = null;

        public AcceptanceManager(IAcceptance acceptanceHandler)
        {
            _acceptanceHandler = acceptanceHandler;
        }

        public int CreateAcceptance(AcceptanceInfo acceptance)
        {
            var newInfo = _acceptanceHandler.CreateAcceptance(new Acceptance()
            {
                AcceptanceType = acceptance.AcceptanceType,
                Amount = acceptance.Amount,
                BankName = acceptance.BankName,
                CreateTime = DateTime.Now,
                DrawTime = acceptance.DrawTime,
                DueDate = acceptance.DueDate,
                UpdateTime = DateTime.Now,
                 Subsidies = acceptance.Subsidies,
                  YearSubsidies = acceptance.YearSubsidies
            });
            return newInfo == null ? 0 : newInfo.AcceptanceId;
        }

        public bool DeleteAcceptance(int acceptanceId)
        {
            return _acceptanceHandler.DeleteAcceptance(acceptanceId);
        }

        public AcceptanceInfo QueryAcceptanceInfo(int infoId)
        {
            var acceptance = _acceptanceHandler.QueryAcceptance(infoId);
            if (acceptance == null)
                return null;

            return new AcceptanceInfo()
            {
                AcceptanceType = acceptance.AcceptanceType,
                Amount = acceptance.Amount,
                BankName = acceptance.BankName,
                DrawTime = acceptance.DrawTime,
                DueDate = acceptance.DueDate,
                UpdateTime = acceptance.UpdateTime,
                AcceptanceId = acceptance.AcceptanceId,
                IsDelete = acceptance.IsDelete,
                CreateTime = acceptance.CreateTime,
                 Subsidies = acceptance.Subsidies,
                  YearSubsidies = acceptance.YearSubsidies
            };
        }

        public IList<AcceptanceInfo> QueryAllAcceptance()
        {
            var list = _acceptanceHandler.QueryAllAcceptances();
            if (list == null || list.Count == 0)
                return null;

            IList<AcceptanceInfo> resultList = new List<AcceptanceInfo>();
            foreach (var acceptance in list)
            {
                resultList.Add(new AcceptanceInfo()
                {
                    AcceptanceType = acceptance.AcceptanceType,
                    Amount = acceptance.Amount,
                    BankName = acceptance.BankName,
                    DrawTime = acceptance.DrawTime,
                    DueDate = acceptance.DueDate,
                    UpdateTime = acceptance.UpdateTime,
                    AcceptanceId = acceptance.AcceptanceId,
                    IsDelete = acceptance.IsDelete,
                    CreateTime = acceptance.CreateTime,
                     Subsidies = acceptance.Subsidies,
                      YearSubsidies = acceptance.YearSubsidies
                });
            }

            return resultList;
        }

        public bool UpdateAcceptance(AcceptanceInfo acceptance)
        {
            return _acceptanceHandler.UpdateAcceptance(new Acceptance()
            {
                AcceptanceType = acceptance.AcceptanceType,
                Amount = acceptance.Amount,
                BankName = acceptance.BankName,
                DrawTime = acceptance.DrawTime,
                DueDate = acceptance.DueDate,
                UpdateTime = DateTime.Now,
                AcceptanceId = acceptance.AcceptanceId,
                IsDelete = acceptance.IsDelete,
                 Subsidies = acceptance.Subsidies,
                  YearSubsidies = acceptance.YearSubsidies
            });

        }
    }
}
