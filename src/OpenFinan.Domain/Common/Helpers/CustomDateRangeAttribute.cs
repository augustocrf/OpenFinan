using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
public class CustomDateRangeAttribute : RangeAttribute
{
    public CustomDateRangeAttribute() : base(typeof(DateTime), DateTime.Now.AddDays(5).ToString(), DateTime.Now.AddDays(40).ToString()) 
    { } 
}