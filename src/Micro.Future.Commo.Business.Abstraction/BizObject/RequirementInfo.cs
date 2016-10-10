using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Micro.Future.Commo.Business.Abstraction.BizObject
{
    public class RequirementInfo
    {
        #region 需求自己的属性

        /// <summary>
        /// 需求ID
        /// </summary>
        public int RequirementId { get; set; }

        /// <summary>
        /// 用户Id
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// 企业Id
        /// </summary>
        public int EnterpriseId { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 修改时间
        /// </summary>
        public DateTime ModifyTime { get; set; }

        /// <summary>
        /// 需求状态
        /// </summary>
        public RequirementState State { get; set; }

        /// <summary>
        /// 需求类型：买/卖/补贴
        /// </summary>
        public RequirementType Type { get; set; }

        #endregion



        #region 销售

        /// <summary>
        /// 货物名称，
        /// </summary>
        public string ProductName { get; set; }


        /// <summary>
        /// 货物类型：有色、化工等
        /// </summary>
        public string ProductType { get; set; }

        /// <summary>
        /// 不需要了？
        /// 货物规格：Cu_Ag>=99.95%
        /// </summary>
        public string ProductSpecification { get; set; }

        /// <summary>
        /// 不需要了？
        /// 货物单价：
        /// </summary>
        public decimal ProductPrice { get; set; }

        /// <summary>
        /// 货物数量
        /// </summary>
        public decimal ProductQuantity { get; set; }

        /// <summary>
        /// 不需要了？
        /// 获取单位， 吨
        /// </summary>
        public string ProductUnit { get; set; }

        #region 仓储信息

        /// <summary>
        /// 仓库省份/区 如：上海、北京、浙江、江苏
        /// </summary>
        public string WarehouseState { get; set; }

        /// <summary>
        /// 仓库 城市，如：上海、北京、杭州、无锡
        /// </summary>
        public string WarehouseCity { get; set; }

        /// <summary>
        /// 详细地址1
        /// </summary>
        public string WarehouseAddress1 { get; set; }

        /// <summary>
        /// 详细地址2
        /// </summary>
        public string WarehouseAddress2 { get; set; }

        #endregion

        /// <summary>
        /// 电子仓单 - new
        /// </summary>
        public string EWarehouseReceipt { get; set; }


        #endregion

        #region 采购


        /// <summary>
        /// 资金金额
        /// </summary>
        public decimal PaymentAmount { get; set; }

        /// <summary>
        /// 货款支付时间
        /// </summary>
        public string PaymentDateTime { get; set; }

        /// <summary>
        /// 支付方式
        /// </summary>
        public string PaymentType { get; set; }

        /// <summary>
        /// 支付银行开户行名称 - new
        /// </summary>
        public string PaymentBankName { get; set; }

        /// <summary>
        /// 支付银行帐号 - new
        /// </summary>
        public string PaymentBankAccount { get; set; }

        /// <summary>
        /// 支付银行行号 - new
        /// </summary>
        public int PaymentBankId { get; set; }

        /// <summary>
        /// 支付银行地址 - new
        /// </summary>
        public string PaymentBankAddress { get; set; }

        /// <summary>
        /// 支付银行是否开通银承电子票口  - new
        /// </summary>
        public bool IsAcceptanceBillETicket { get; set; }

        #endregion

        #region 购销

        /// <summary>
        /// 合同总金额
        /// </summary>
        public decimal TradeAmount { get; set; }

        /// <summary>
        /// 合同利润
        /// </summary>
        public decimal TradeProfit { get; set; }

        /// <summary>
        /// 企业类型
        /// 1=国有企业/2=私有企业/3=外商独资/4=中外合资/5=港澳独资
        /// </summary>
        public string EnterpriseType { get; set; }

        /// <summary>
        /// 经营范围
        /// </summary>
        public string BusinessRange { get; set; }


        /// <summary>
        /// 不需要了？
        /// 补贴额度，比如：我要1个亿的贸易量，我补贴贸易量的5%
        /// </summary>
        public decimal Subsidies { get; set; }

        /// <summary>
        /// 支付风控 - new
        /// </summary>
        public string PaymentRiskControl { get; set; }

        #endregion


        #region 3种类型 公共属性

        /// <summary>
        /// 仓库开户
        /// </summary>
        public string WarehouseAccount { get; set; }

        /// <summary>
        /// 发票面额
        /// </summary>
        public string InvoiceValue { get; set; }

        /// <summary>
        /// 开票要求 - new
        /// </summary>
        public string InvoiceRequirement { get; set; }

        /// <summary>
        /// 发票开具时间
        /// </summary>
        public string InvoiceIssueDateTime { get; set; }

        /// <summary>
        /// 发票交接方式
        /// </summary>
        public string InvoiceTransferMode { get; set; }

        #endregion

        public string RequirementRemarks { get; set; }
      

        /// <summary>
        /// 需求的撮合规则
        /// </summary>
        public IEnumerable<RequirementRuleInfo> Rules { get; set; }
    }
}
