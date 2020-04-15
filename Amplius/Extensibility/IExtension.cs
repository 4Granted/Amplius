namespace Amplius.Extensibility
{
    public interface IExtension<I, C> where I : IExtensionInfo
    {
        public void Initialize(C context);

        public I GetInfo();
    }
}
