using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Micro.Future.Commo.Business.Abstraction.BizObject
{
    public class RequirementRuleNames
    {

        #region 企业相关的规则

        /// <summary>
        /// 国企，还是外企
        /// </summary>
        public const string EnterpriseNature = "企业性质";

        public const string EnterpriseRegisteredCaptial = "注册资金";

        public const string EnterpriseRegisteredState = "注册省份";

        public const string EnterpriseRegisteredCity = "注册城市";

        #endregion

        #region 货物相关的规则

        public const string ProductWarehouseState = "商品仓库所在省份";

        public const string ProductWarehouseCity = "商品仓库所在城市";

        #endregion


        #region 资金相关

        public const string MoneyTradeAmount = "交易量";

        #endregion



        #region 支付相关的规则

        public const string PaymentPayMethod = "支付方式";



        #endregion
    }
}
