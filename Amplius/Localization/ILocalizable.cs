namespace Amplius.Localization
{
#nullable enable
    /// <summary>
    /// Represents an object capable of localizing to a specified <see cref="Languages"/> name
    /// </summary>
    public interface ILocalizable
    {
        public string? Localize(Languages? code = null);
    }
}
