namespace Amplius.Extensibility
{
    public interface IExtensionInfo
    {
        public string Name { get; }
        public Version Version { get; }
        public Version AppVersion { get; }
    }
}
