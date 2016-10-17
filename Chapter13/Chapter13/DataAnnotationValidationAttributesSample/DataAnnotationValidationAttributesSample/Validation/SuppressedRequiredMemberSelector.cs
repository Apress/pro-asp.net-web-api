using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Formatting;
using System.Reflection;
using System.Web;

namespace DataAnnotationValidationAttributesSample.Validation {

    public class SuppressedRequiredMemberSelector 
        : IRequiredMemberSelector {

        public bool IsRequiredMember(MemberInfo member) {

            return false;
        }
    }
}