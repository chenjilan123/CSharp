using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Xunit;

namespace CSharp
{
    public class DynamicAssert
    {
        private object _target;
        private Type _targetType;
        public DynamicAssert(object target)
        {
            _target = target;
            _targetType = _target.GetType();
        }
        public void AssertMethod(MethodBase testMethod, params object[] param)
        {
            var methodName = testMethod.Name;
            var parameters = testMethod.GetParameters();
            var targetMethod = _targetType.GetMethod(methodName);
            var except = param[param.Length - 1];
            var result = targetMethod.Invoke(_target, param.Take(param.Length - 1).ToArray());

            //预测值为结构类型
            if (!except.GetType().IsByRef)
            {
                Assert.Equal(except, result);
            }
            //引用类型
            else
            {

            }
        }
    }
}
