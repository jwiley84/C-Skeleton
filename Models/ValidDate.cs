using System;
using System.Globalization;
using System.ComponentModel.DataAnnotations;

namespace skeleton
{
  [AttributeUsage(AttributeTargets.Property |
    AttributeTargets.Field, AllowMultiple = false)]
  sealed public class ValidDate : ValidationAttribute
  {
    public override bool IsValid(object value)
    {
      bool result = false;
      if (value is DateTime)
      {
        if ((DateTime)value > DateTime.UtcNow)
        {
          result = true;
        }
      }
      return result;
    }
  }
}