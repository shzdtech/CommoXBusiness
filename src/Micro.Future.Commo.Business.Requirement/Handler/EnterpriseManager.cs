using Micro.Future.Commo.Business.Abstraction.BizInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Micro.Future.Business.Common;
using Micro.Future.Commo.Business.Abstraction.BizObject;
using Micro.Future.Business.DataAccess.Commo.CommonInterface;
using Micro.Future.Business.DataAccess.Commo.CommoHandler;
using Micro.Future.Business.DataAccess.Commo.CommoObject;

namespace Micro.Future.Commo.Business.Requirement.Handler
{
    public class EnterpriseManager : IEnterpriseManager
    {
        private IEnterprise enterpriseHandler = null;

        public EnterpriseManager()
        {
            enterpriseHandler = new EnterpriseHandler();
        }

        public EnterpriseManager(IEnterprise enterpriseDAL)
        {
            enterpriseHandler = enterpriseDAL;
        }

        public BizTResult<int> AddEnterprise(EnterpriseInfo enterprise)
        {
            var entity = EnterpriseToEntityObject(enterprise);
            int enterpriseId = enterpriseHandler.AddEnterprise(entity); 

            if(enterpriseId > 0)
            {
                return new BizTResult<int>(enterpriseId);
            }
            else
            {
                return new BizTResult<int>(0, new BizException("添加失败！"));
            }
        }

        public EnterpriseInfo QueryEnterpriseInfo(int enterpriseId)
        {
            var entity = enterpriseHandler.QueryEnterpriseInfo(enterpriseId);
            return EnterpriseToBizObject(entity);
        }

        public BizTResult<bool> UpdateEnterprise(EnterpriseInfo enterprise)
        {
            var entity = EnterpriseToEntityObject(enterprise);
            bool result = enterpriseHandler.UpdateEnterprise(entity);
            if (result)
            {
                return new BizTResult<bool>(true);
            }
            else
            {
                return new BizTResult<bool>(false, new BizException("保存失败！"));
            }
        }

        private EnterpriseInfo EnterpriseToBizObject(Enterprise entity)
        {
            EnterpriseInfo info = new EnterpriseInfo();

            return info;
        }

        private Enterprise EnterpriseToEntityObject(EnterpriseInfo info)
        {
            Enterprise entity = new Enterprise();

            return entity;
        }
    }
}
