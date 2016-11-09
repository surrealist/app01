
using System;
using System.Collections.Generic;
using Xunit;
using Xunit.Abstractions;

namespace App01.Facts {
  public class CalculatorFact {

    public class AddMethod : IDisposable {

      private Calculator c;
      private readonly ITestOutputHelper _output;
       
      // Setup
      public AddMethod(ITestOutputHelper output) {
        c = new Calculator();
        _output = output;
      }

      [Theory]
      [InlineData("1", "1", "2")]
      [InlineData("2", "2", "4")]
      //[MemberData("OneDigitInputs", 10)]
      public void OneDigit(string v1, string v2, string expected) { 
        var result = c.Add(v1, v2);

        _output.WriteLine($"{v1} + {v2} = {result}");
        Assert.Equal(expected, result);
      }

      public static IEnumerable<Object[]> OneDigitInputs(int count) {
        for (int i = 0; i < count; i++) {
          yield return new object[] {
            i.ToString(), i.ToString(), i.ToString()
          };
        }
      }

      //[Fact]
      //public void OneDigit_2() {  
      //  var result = c.Add("2", "2"); 
      //  Assert.Equal("4", result);
      //}

      [Fact]
      public void NegativeValues() { 
        var result = c.Add("-5", "8");
        Assert.Equal("3", result);
      }

      [Fact]
      public void EmptyStringOrNull_TreatAsZero() { 
        var result1 = c.Add(null, "9");
        var result2 = c.Add("", "9");
        var result3 = c.Add("9", null);
        var result4 = c.Add("9", "");

        Assert.Equal("9", result1);
        Assert.Equal("9", result2);
        Assert.Equal("9", result3);
        Assert.Equal("9", result4);
      }

      // [Fact]
      // public void TwoDigits() { 
      //   var result = c.Add("11", "22");
      //   Assert.Equal("33", result);
      // }
     

      [Fact]
      public void OnlyDigitCharacterIsAllowed() {
        var ex = Assert.Throws<ArgumentException>(() => {

          c.Add("1a", "2");

        });

        Assert.Equal("value1", ex.ParamName);
      }

      [Fact]
      public void AddTwoDigits() {
       var result =  c.Add("11", "25");
        Assert.Equal("36", result);
      }

      // Teardown
      public void Dispose() {
        c = null; 
      }
    }
  }
}
