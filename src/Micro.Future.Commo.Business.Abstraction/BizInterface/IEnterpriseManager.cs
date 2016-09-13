using Micro.Future.Business.Common;
using Micro.Future.Commo.Business.Abstraction.BizObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Micro.Future.Commo.Business.Abstraction.BizInterface
{
    public interface IEnterpriseManager
    {
        EnterpriseInfo QueryEnterpriseInfo(int enterpriseId);

        bool EmailHasBeenRegistered(string email);

        BizTResult<int> AddEnterprise(EnterpriseInfo enterprise);

        BizTResult<bool> UpdateEnterprise(EnterpriseInfo enterprise);

        BizTResult<IList<EnterpriseInfo>> QueryEnterprises(string name, EnterpriseStateType? stateType);

        bool UpdateEnterpriseState(int enterpriseId, EnterpriseStateType stateType);

        void SaveEmailVerifyCode(string requestId, string email, string code, DateTime sendTime);

        bool HasExceedLimitationPerDay(string email);

        bool CanResend(string email);

        bool CheckEmailVerifyCode(string email, string verificationCode);
    }
}
