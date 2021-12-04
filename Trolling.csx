using System.Reflection;

interface IFunction<T>
{
    T Result { get; }
}

class Variable<T> : IFunction<T>
{
    public T Value { private get; set; }
    public T Result => Value;
}

class ParametrelessFunctionInvoker<TReturn> : IFunction<TReturn>
{
    public Func<TReturn> Function { private get; set; }
    public TReturn Result => new Variable<Func<TReturn>> { Value = Function }.Result.Invoke();
}

class FunctionWithOneArgumentInvoker<TArg, TReturn> : IFunction<TReturn>
{
    public Func<TArg, TReturn> Function { private get; set; }
    public TArg FunctionArgument1 { private get; set; }
    public TReturn Result => Function.Invoke(FunctionArgument1);
}

class Constructor<TClass> : IFunction<TClass>
{
    public object[] Arguments { private get; set; }
    public TClass Result => (TClass) new FunctionWithOneArgumentInvoker<object[], object>
                        {
                            Function = new Variable<ConstructorInfo>
                            {
                                Value = new FunctionWithOneArgumentInvoker<Type[], ConstructorInfo>
                                {
                                    Function = new Variable<Type>
                                    {
                                        Value = new ParametrelessFunctionInvoker<Type>
                                        {
                                            Function = new Variable<Func<Type>> { Value = () => typeof(TClass) }.Result
                                        }.Result
                                    }.Result.GetConstructor,
                                    FunctionArgument1 = new []
                                    {
                                        new Variable<Type>
                                        {
                                            Value = new ParametrelessFunctionInvoker<Type>
                                            {
                                                Function = new Variable<Func<Type>> { Value = () => typeof(TClass) }.Result
                                            }.Result
                                        }.Result
                                    }
                                }.Result
                            }.Result.Invoke,
                            FunctionArgument1 = Arguments
                        }.Result;
}


class TestClass
{
    string String { get; set; }
    int Integer { get; set; }
    float Float { get; set; }
}

var variable = new Variable<TestClass> { Value = new Constructor<TestClass>().Result }.Result;
