using System;
using System.Collections.Generic;

public static class RPNCalculator
{
    public static float Evaluate(string expression, Dictionary<string, float> variables)
    {
        Stack<float> stack = new Stack<float>();
        string[] tokens = expression.Split(' ');

        foreach (string token in tokens)
        {
            if (float.TryParse(token, out float number))
            {
                stack.Push(number);
            }
            else if (variables.ContainsKey(token))
            {
                stack.Push(variables[token]);
            }
            else
            {
                float b = stack.Pop();
                float a = stack.Pop();

                switch (token)
                {
                    case "+": stack.Push(a + b); break;
                    case "-": stack.Push(a - b); break;
                    case "*": stack.Push(a * b); break;
                    case "/": stack.Push(a / b); break;
                    case "%": stack.Push(a % b); break;
                    default: throw new InvalidOperationException($"Unknown operator: {token}");
                }
            }
        }

        return stack.Pop();
    }
}