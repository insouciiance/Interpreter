using System;
using System.Collections;

namespace Lab5.Commands
{
    public class VariablePool
    {
        private Hashtable pool;

        public static VariablePool Instance = new VariablePool();

        private VariablePool()
        {
            pool = new Hashtable();
        }

        public void SetValue(string varName,object? value)
        {
            if (pool.Contains(varName))
                pool[varName] = value;
            else
                pool.Add(varName,value);
        }
        
        public void SetValue<T>(string varName,T value)
        {
            if (pool.Contains(varName))
                pool[varName] = value;
            else
                pool.Add(varName,value);
        }

        public object? GetValue(string varName)
        {
            return pool[varName];
        }

        public T GetValue<T>(string varName)
        {
            object value = pool[varName];
            if (value is T castedVal)
                return castedVal;

            Console.WriteLine($"Cannot cast var:{varName} into target type:{typeof(T)} ");
            return default;
        }
    }
}