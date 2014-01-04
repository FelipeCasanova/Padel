using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Padel.Web.Mvc.Validation
{
    public class DataTypeAttributeAdapter : DataAnnotationsModelValidator<DataTypeAttribute>
    {
        public DataTypeAttributeAdapter(ModelMetadata metadata, ControllerContext context, DataTypeAttribute attribute)
            : base(metadata, context, attribute) { }

        public override System.Collections.Generic.IEnumerable<ModelClientValidationRule> GetClientValidationRules()
        {
            if (Attribute.DataType == DataType.EmailAddress)
            {
                return new[] { new ModelClientValidationEmailRule(Attribute.FormatErrorMessage(Metadata.GetDisplayName())) };
            }

            return base.GetClientValidationRules();
        }
    }
}