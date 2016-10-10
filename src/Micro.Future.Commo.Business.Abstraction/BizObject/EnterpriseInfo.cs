﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Micro.Future.Commo.Business.Abstraction.BizObject
{
    public class EnterpriseInfo
    {
        public int EnterpriseId { get; set; }

        /// <summary>
        /// 企业名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 企业地址
        /// </summary>
        public string Address { get; set; }
        /// <summary>
        /// 联系人
        /// </summary>
        public string Contacts { get; set; }
        /// <summary>
        /// 企业注册代码
        /// </summary>
        public string RegisterNumber { get; set; }
        /// <summary>
        /// 注册时间
        /// </summary>
        public DateTime RegisterTime { get; set; }
        /// <summary>
        /// 注册资金
        /// </summary>
        public double RegisterCapital { get; set; }
        /// <summary>
        /// 注册地址
        /// </summary>
        public string RegisterAddress { get; set; }

        /// <summary>
        /// 法人代表
        /// </summary>
        public string LegalRepresentative { get; set; }
        /// <summary>
        /// 开票量
        /// </summary>
        public double InvoicedQuantity { get; set; }
        /// <summary>
        /// 企业类型
        /// </summary>
        public int BusinessTypeId { get; set; }
        /// <summary>
        /// 企业业务范围
        /// </summary>
        public string BusinessRange { get; set; }
        /// <summary>
        /// 信用等级
        /// </summary>
        public int ReputationGrade { get; set; }
        /// <summary>
        /// 年检情况
        /// </summary>
        public string AnnualInspection { get; set; }
        /// <summary>
        /// 上年度营业额
        /// </summary>
        public double PreviousSales { get; set; }
        /// <summary>
        /// 利润
        /// </summary>
        public double PreviousProfit { get; set; }
        /// <summary>
        /// 支付方式
        /// </summary>
        public int PaymentMethodId { get; set; }
        /// <summary>
        /// 注册银行
        /// </summary>
        public int RegisterBankId { get; set; }
        /// <summary>
        /// 企业开户账号
        /// </summary>
        public string RegisterAccount { get; set; }
        /// <summary>
        /// 在本系统注册时间
        /// </summary>
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// 企业状态
        /// </summary>
        public EnterpriseStateType EnterpriseState { get; set; }

        public string EmailAddress { get; set; }

        public string MobilePhone { get; set; }

        /// <summary>
        /// 营业执照等证件保存路径
        /// </summary>
        public string LicenseImagePath { get; set; }

        /// <summary>
        /// 传真
        /// </summary>
        public string Fax { get; set; }


        #region


        //企业开票资料
        public string InvoiceMaterial { get; set; }
        
        //企业开户仓库
        public string RegisterWarehouse { get; set; }
        //每月最大交易量
        public string MaxTradeAmountPerMonth { get; set; }

        //企业交易支付银行信息：开户行名称/帐号/行号/地址/是否开通银承电子票口

        ///// <summary>
        ///// 开户行名称
        ///// </summary>
        //public string PaymentBankName { get; set; }

        ///// <summary>
        ///// 帐号
        ///// </summary>
        //public string PaymentBankAccount { get; set; }

        ///// <summary>
        ///// 行号
        ///// </summary>
        //public int PaymentBankId { get; set; }
        ///// <summary>
        ///// 地址
        ///// </summary>
        //public string PaymentBankAddress { get; set; }

        /// <summary>
        /// 是否开通银承电子票口
        /// </summary>
        public bool IsAcceptanceBillETicket { get; set; }

        //企业营业执照
        //企业开户银行
        //发票面额
        //企业类型：国企（地方国企）/央企/上市公司/混合制/民营企业
        //主营范围

        #endregion

    }
}
