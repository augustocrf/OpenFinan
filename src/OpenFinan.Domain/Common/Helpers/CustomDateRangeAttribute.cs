using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
public class CustomDateRangeAttribute : RangeAttribute
{
    public CustomDateRangeAttribute() : base(typeof(DateTime), DateTime.Now.ToString(), DateTime.Now.AddYears(20).ToString()) 
    { } 
}