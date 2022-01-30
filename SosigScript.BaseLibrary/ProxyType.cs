namespace SosigScript.BaseLibrary
{
    public abstract class ProxyType<TTarget>
    {
        public TTarget Target { get; protected set; }
    }
}
