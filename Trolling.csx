using System.Reflection;
interface IFunction<T>
{
    T Result { get; }
}


class FileInfoConstructor : IFunction<FileInfo>
{
    public string Path { private get; set; }
    public FileInfo Result => new FileInfo(Path);
}

class Variable<T> : IFunction<T>
{
    public T Value { private get; set; }
    public T Result => Value;
}

class ReadAllLines : IFunction<string[]>
{
   public FileInfo File { get; set; }
   public string[] Result => File == null ? throw new NullReferenceException("trolling") : new [] { "lol" };
}

class ParametrelessFunctionInvoker<TReturn> : IFunction<TReturn>
{
    public Func<TReturn> Function { private get; set; }
    public TReturn Result => Function.Invoke();
}

class FunctionWithOneArgumentInvoker<TArg, TReturn> : IFunction<TReturn>
{
    public Func<TArg, TReturn> Function { private get; set; }
    public TArg FunctionArgument1 { private get; set; }
    public TReturn Result => Function.Invoke(FunctionArgument1);
}

class Constructor<TClass> : IFunction<TClass> where TClass : new()
{
    public TClass Object { private get; set; }
    public object[] Arguments { private get; set; }
    public TClass Result
    {
        get
        {
            var ctor = new Variable<ConstructorInfo>
            {
                Value = new FunctionWithOneArgumentInvoker<Type[], ConstructorInfo>
                {
                    Function = new Variable<Type>
                    {
                        Value = new ParametrelessFunctionInvoker<Type>
                        {
                            Function = Object.GetType
                        }.Result
                    }.Result.GetConstructor,
                    FunctionArgument1 = new []
                    {
                        new Variable<Type>
                        {
                            Value = new ParametrelessFunctionInvoker<Type>
                            {
                                Function = Object.GetType
                            }.Result
                        }.Result
                    }
                }.Result
            }.Result;

            return new
        }
    }
}

class TestClass
{
    string String { get; set; }
    int Integer { get; set; }
    float Float { get; set; }
}

