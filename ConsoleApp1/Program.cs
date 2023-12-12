using ClassLibrary1;
using System.Runtime.CompilerServices;

[assembly: IgnoresAccessChecksTo("ClassLibrary1")]

var calculator = new Calculator();

calculator.Value = "Hello World!";

calculator.Calc();

Console.ReadLine();
