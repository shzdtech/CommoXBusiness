using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Micro.Future.Commo.Business.Abstraction.BizObject
{
    public class ValidationResult
    {
        public bool HasError
        {
            get {
                return string.IsNullOrEmpty(ErrorMessage) || (FieldsErrors != null && FieldsErrors.Count > 0);
            }
        }

        public string ErrorMessage { get; set; }

        /// <summary>
        /// key=错误字段;value=错误信息
        /// </summary>
        public Dictionary<string,string> FieldsErrors { get; set; }
    }
}
