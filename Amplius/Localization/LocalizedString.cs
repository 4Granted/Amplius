using Amplius.Utils;

namespace Amplius.Localization
{
#nullable enable
    /// <summary>
    /// A wrapper for a <see cref="NamespaceKey"/> implementating <see cref="INamespaced"/> and <see cref="ILocalizable"/>
    /// </summary>
    public sealed class LocalizedString : INamespaced, ILocalizable
    {
        public NamespaceKey? Key => key;

        private readonly NamespaceKey? key;

        public LocalizedString(NamespaceKey? key) => this.key = key;
        public LocalizedString(string? key) => this.key = NamespaceKey.From(key);

        /// <summary>
        /// Localizies the internal <see cref="NamespaceKey"/> to the specified <see cref="Languages"/> code
        /// </summary>
        /// <param name="code">Language to localize to</param>
        /// <returns>Returns the localized string</returns>
        public string? Localize(Languages? code = null) => Language.Localize(key, code);

        public static implicit operator NamespaceKey?(LocalizedString? ls) => ls?.key;
        public static implicit operator LocalizedString?(string? str) => str;
    }
}
