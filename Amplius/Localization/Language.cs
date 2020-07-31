using Amplius.Experimental;
using Amplius.Utils;
using System.Collections.Generic;
using System.IO;

namespace Amplius.Localization
{
#nullable enable
    /// <summary>
    /// A container for language localizations
    /// </summary>
    public static class Language
    {
        private static readonly Dictionary<Languages, Dictionary<NamespaceKey, string>> localizations = new Dictionary<Languages, Dictionary<NamespaceKey, string>>();
        private static readonly Languages fallback = Languages.English;

        /// <summary>
        /// Sets a specific <see cref="Languages"/> localization data table
        /// </summary>
        /// <param name="code">Language code to bind to</param>
        /// <param name="dict">Localization data table</param>
        public static void SetLocalization(Languages code, Dictionary<NamespaceKey, string> dict) => localizations[code] = dict;
        /// <summary>
        /// Localizes a <see cref="NamespaceKey"/> to the specified <see cref="Languages"/> code
        /// </summary>
        /// <param name="unlocalized">Unlocalized <see cref="NamespaceKey"/></param>
        /// <param name="code">Language to localize to</param>
        /// <returns>Returns a localized string</returns>
        public static string? Localize(NamespaceKey? unlocalized, Languages? code = null)
        {
            if (unlocalized == null)
                return null;
            if (!localizations.ContainsKey(code ?? fallback))
                return null;

            var lang = localizations[code ?? fallback];

            if (!lang.ContainsKey(unlocalized))
                return null;

            var localized = lang[unlocalized];

            return localized;
        }
        /// <summary>
        /// Deserializes a file—specifically a <c>.lang</c> one—to a <see cref="Dictionary{TKey, TValue}"/>
        /// </summary>
        /// <param name="path">Path to the file</param>
        /// <returns>Returns the deserialized localization file</returns>
        public static Dictionary<NamespaceKey, string>? DeserializeLanguageConfig(string path)
        {
            if (!File.Exists(path))
                return null;

            var localizations = new Dictionary<NamespaceKey, string>();
            var lines = File.ReadAllLines(path);

            foreach (var line in lines)
            {
                var split = line.Split('=');
                var key = NamespaceKey.From(split[0], '.');
                var localization = split[1];

                if (key != null && !string.IsNullOrEmpty(localization))
                    localizations.Add(key, localization);
            }

            return localizations;
        }

        [Todo("Set each language to its' respective two character code.")]
        public static LanguageCodes LanguageToCode(Languages language)
        {
            switch (language)
            {
                case Languages.Oromo:
                    return LanguageCodes.Om;
                case Languages.Abkhazian:
                    return LanguageCodes.Om;
                case Languages.Afar:
                    return LanguageCodes.Om;
                case Languages.Afrikaans:
                    return LanguageCodes.Om;
                case Languages.Albanian:
                    return LanguageCodes.Om;
                case Languages.Amharic:
                    return LanguageCodes.Om;
                case Languages.Arabic:
                    return LanguageCodes.Om;
                case Languages.Armenian:
                    return LanguageCodes.Om;
                case Languages.Assamese:
                    return LanguageCodes.Om;
                case Languages.Aymara:
                    return LanguageCodes.Om;
                case Languages.Azerbaijani:
                    return LanguageCodes.Om;
                case Languages.Bashkir:
                    return LanguageCodes.Om;
                case Languages.Basque:
                    return LanguageCodes.Om;
                case Languages.Bengali:
                    return LanguageCodes.Om;
                case Languages.Bhutani:
                    return LanguageCodes.Om;
                case Languages.Bihari:
                    return LanguageCodes.Om;
                case Languages.Bislama:
                    return LanguageCodes.Om;
                case Languages.Breton:
                    return LanguageCodes.Om;
                case Languages.Bulgarian:
                    return LanguageCodes.Om;
                case Languages.Burmese:
                    return LanguageCodes.Om;
                case Languages.Byelorussian:
                    return LanguageCodes.Om;
                case Languages.Cambodian:
                    return LanguageCodes.Om;
                case Languages.Catalan:
                    return LanguageCodes.Om;
                case Languages.Chinese:
                    return LanguageCodes.Om;
                case Languages.Corsican:
                    return LanguageCodes.Om;
                case Languages.Croatian:
                    return LanguageCodes.Om;
                case Languages.Czech:
                    return LanguageCodes.Om;
                case Languages.Danish:
                    return LanguageCodes.Om;
                case Languages.Dutch:
                    return LanguageCodes.Om;
                case Languages.English:
                    return LanguageCodes.Om;
                case Languages.Esperanto:
                    return LanguageCodes.Om;
                case Languages.Estonian:
                    return LanguageCodes.Om;
                case Languages.Faeroese:
                    return LanguageCodes.Om;
                case Languages.Fiji:
                    return LanguageCodes.Om;
                case Languages.Finnish:
                    return LanguageCodes.Om;
                case Languages.French:
                    return LanguageCodes.Om;
                case Languages.Frisian:
                    return LanguageCodes.Om;
                case Languages.Galician:
                    return LanguageCodes.Om;
                case Languages.Georgian:
                    return LanguageCodes.Om;
                case Languages.German:
                    return LanguageCodes.Om;
                case Languages.Greek:
                    return LanguageCodes.Om;
                case Languages.Greenlandic:
                    return LanguageCodes.Om;
                case Languages.Guarani:
                    return LanguageCodes.Om;
                case Languages.Gujarati:
                    return LanguageCodes.Om;
                case Languages.Hausa:
                    return LanguageCodes.Om;
                case Languages.Hebrew:
                    return LanguageCodes.Om;
                case Languages.Hindi:
                    return LanguageCodes.Om;
                case Languages.Hungarian:
                    return LanguageCodes.Om;
                case Languages.Icelandic:
                    return LanguageCodes.Om;
                case Languages.Indonesian:
                    return LanguageCodes.Om;
                case Languages.Interlingua:
                    return LanguageCodes.Om;
                case Languages.Interlingue:
                    return LanguageCodes.Om;
                case Languages.Inupiak:
                    return LanguageCodes.Om;
                case Languages.Inuktitut:
                    return LanguageCodes.Om;
                case Languages.Irish:
                    return LanguageCodes.Om;
                case Languages.Italian:
                    return LanguageCodes.Om;
                case Languages.Japanese:
                    return LanguageCodes.Om;
                case Languages.Javanese:
                    return LanguageCodes.Om;
                case Languages.Kannada:
                    return LanguageCodes.Om;
                case Languages.Kashmiri:
                    return LanguageCodes.Om;
                case Languages.Kazakh:
                    return LanguageCodes.Om;
                case Languages.Kinyarwanda:
                    return LanguageCodes.Om;
                case Languages.Kirghiz:
                    return LanguageCodes.Om;
                case Languages.Kirundi:
                    return LanguageCodes.Om;
                case Languages.Korean:
                    return LanguageCodes.Om;
                case Languages.Kurdish:
                    return LanguageCodes.Om;
                case Languages.Laothian:
                    return LanguageCodes.Om;
                case Languages.Latin:
                    return LanguageCodes.Om;
                case Languages.Latvian:
                case Languages.Lettish:
                    return LanguageCodes.Lv;
                case Languages.Lingala:
                    return LanguageCodes.Om;
                case Languages.Lithuanian:
                    return LanguageCodes.Om;
                case Languages.Macedonian:
                    return LanguageCodes.Om;
                case Languages.Malagasy:
                    return LanguageCodes.Om;
                case Languages.Malay:
                    return LanguageCodes.Om;
                case Languages.Malayalam:
                    return LanguageCodes.Om;
                case Languages.Maltese:
                    return LanguageCodes.Om;
                case Languages.Maori:
                    return LanguageCodes.Om;
                case Languages.Marathi:
                    return LanguageCodes.Om;
                case Languages.Moldavian:
                    return LanguageCodes.Om;
                case Languages.Mongolian:
                    return LanguageCodes.Om;
                case Languages.Nauru:
                    return LanguageCodes.Om;
                case Languages.Nepali:
                    return LanguageCodes.Om;
                case Languages.Norwegian:
                    return LanguageCodes.Om;
                case Languages.Occitan:
                    return LanguageCodes.Om;
                case Languages.Oriya:
                    return LanguageCodes.Om;
                case Languages.Pashto:
                case Languages.Pushto:
                    return LanguageCodes.Ps;
                case Languages.Persian:
                    return LanguageCodes.Om;
                case Languages.Polish:
                    return LanguageCodes.Om;
                case Languages.Portuguese:
                    return LanguageCodes.Om;
                case Languages.Punjabi:
                    return LanguageCodes.Om;
                case Languages.Quechua:
                    return LanguageCodes.Om;
                case Languages.Rhaeto_Romance:
                    return LanguageCodes.Om;
                case Languages.Romanian:
                    return LanguageCodes.Om;
                case Languages.Russian:
                    return LanguageCodes.Om;
                case Languages.Samoan:
                    return LanguageCodes.Om;
                case Languages.Sangro:
                    return LanguageCodes.Om;
                case Languages.Sanskrit:
                    return LanguageCodes.Om;
                case Languages.Scots_Gaelic:
                    return LanguageCodes.Om;
                case Languages.Serbian:
                    return LanguageCodes.Om;
                case Languages.Serbo_Croatian:
                    return LanguageCodes.Om;
                case Languages.Sesotho:
                    return LanguageCodes.Om;
                case Languages.Setswana:
                    return LanguageCodes.Om;
                case Languages.Shona:
                    return LanguageCodes.Om;
                case Languages.Sindhi:
                    return LanguageCodes.Om;
                case Languages.Singhalese:
                    return LanguageCodes.Om;
                case Languages.Siswati:
                    return LanguageCodes.Om;
                case Languages.Slovak:
                    return LanguageCodes.Om;
                case Languages.Slovenian:
                    return LanguageCodes.Om;
                case Languages.Somali:
                    return LanguageCodes.Om;
                case Languages.Spanish:
                    return LanguageCodes.Om;
                case Languages.Sudanese:
                    return LanguageCodes.Om;
                case Languages.Swahili:
                    return LanguageCodes.Om;
                case Languages.Swedish:
                    return LanguageCodes.Om;
                case Languages.Tagalog:
                    return LanguageCodes.Om;
                case Languages.Tajik:
                    return LanguageCodes.Om;
                case Languages.Tamil:
                    return LanguageCodes.Om;
                case Languages.Tatar:
                    return LanguageCodes.Om;
                case Languages.Tegulu:
                    return LanguageCodes.Om;
                case Languages.Thai:
                    return LanguageCodes.Om;
                case Languages.Tibetan:
                    return LanguageCodes.Om;
                case Languages.Tigrinya:
                    return LanguageCodes.Om;
                case Languages.Tonga:
                    return LanguageCodes.Om;
                case Languages.Tsonga:
                    return LanguageCodes.Om;
                case Languages.Turkish:
                    return LanguageCodes.Om;
                case Languages.Turkmen:
                    return LanguageCodes.Om;
                case Languages.Twi:
                    return LanguageCodes.Om;
                case Languages.Uigur:
                    return LanguageCodes.Om;
                case Languages.Ukrainian:
                    return LanguageCodes.Om;
                case Languages.Urdu:
                    return LanguageCodes.Om;
                case Languages.Uzbek:
                    return LanguageCodes.Om;
                case Languages.Vietnamese:
                    return LanguageCodes.Om;
                case Languages.Volapuk:
                    return LanguageCodes.Om;
                case Languages.Welch:
                    return LanguageCodes.Om;
                case Languages.Wolof:
                    return LanguageCodes.Om;
                case Languages.Xhosa:
                    return LanguageCodes.Om;
                case Languages.Yiddish:
                    return LanguageCodes.Om;
                case Languages.Yoruba:
                    return LanguageCodes.Om;
                case Languages.Zhuang:
                    return LanguageCodes.Om;
                case Languages.Zulu:
                    return LanguageCodes.Om;
                default:
                    return LanguageCodes.En;
            }
        }

        [Todo("Set each code to its' respective language name.")]
        public static Languages CodeToLanguage(LanguageCodes code)
        {
            return Languages.Abkhazian;
        }
    }
}
