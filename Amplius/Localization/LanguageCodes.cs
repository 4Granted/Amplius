using Amplius.Experimental;
using System;
using System.Collections.Generic;
using System.Text;

namespace Amplius.Localization
{
    public enum LanguageCodes
    {
        Om,
        Ab,
        Aa,
        Af,
        Sq,
        Am,
        Ar,
        Hy,
        As,
        Ay,
        Az,
        Ba,
        Eu,
        Bn,
        Dz,
        Bh,
        Bi,
        Br,
        Bg,
        My,
        Be,
        Km,
        Ca,
        Zh,
        Co,
        Hr,
        Cs,
        Da,
        Nl,
        En,
        Eo,
        Et,
        Fo,
        Fj,
        Fi,
        Fr,
        Fy,
        Gl,
        Ka,
        De,
        El,
        Kl,
        Gn,
        Gu,
        Ha,
        He,
        Hi,
        Hu,
        Is,
        Id,
        Ia,
        Ie,
        Ik,
        Iu,
        Ga,
        It,
        Ja,
        Jw,
        Kn,
        Ks,
        Kk,
        Rw,
        Ky,
        Rn,
        Ko,
        Ku,
        Lo,
        La,
        Lv,
        Ln,
        Lt,
        Mk,
        Mg,
        Ms,
        Ml,
        Mt,
        Mi,
        Mr,
        Mo,
        Mn,
        Na,
        Ne,
        No,
        Oc,
        Or,
        Ps,
        Fa,
        Pl,
        Pt,
        Pa,
        Qu,
        Rm,
        Ro,
        Ru,
        Sm,
        Sg,
        Sa,
        Gd,
        Sr,
        Sh,
        St,
        Tn,
        Sn,
        Sd,
        Si,
        Ss,
        Sk,
        Sl,
        So,
        Es,
        Su,
        Sw,
        Sv,
        Tl,
        Tg,
        Ta,
        Tt,
        Te,
        Th,
        Bo,
        Ti,
        To,
        Ts,
        Tr,
        Tk,
        Tw,
        Ug,
        Uk,
        Ur,
        Uz,
        Vi,
        Vo,
        Cy,
        Wo,
        Xh,
        Yi,
        Yo,
        Za,
        Zu,
    }

    public enum Languages
    {
        Oromo,
        Abkhazian,
        Afar,
        Afrikaans,
        Albanian,
        Amharic,
        Arabic,
        Armenian,
        Assamese,
        Aymara,
        Azerbaijani,
        Bashkir,
        Basque,
        Bengali,
        Bhutani,
        Bihari,
        Bislama,
        Breton,
        Bulgarian,
        Burmese,
        Byelorussian,
        Cambodian,
        Catalan,
        Chinese,
        Corsican,
        Croatian,
        Czech,
        Danish,
        Dutch,
        English,
        Esperanto,
        Estonian,
        Faeroese,
        Fiji,
        Finnish,
        French,
        Frisian,
        Galician,
        Georgian,
        German,
        Greek,
        Greenlandic,
        Guarani,
        Gujarati,
        Hausa,
        Hebrew,
        Hindi,
        Hungarian,
        Icelandic,
        Indonesian,
        Interlingua,
        Interlingue,
        Inupiak,
        Inuktitut,
        Irish,
        Italian,
        Japanese,
        Javanese,
        Kannada,
        Kashmiri,
        Kazakh,
        Kinyarwanda,
        Kirghiz,
        Kirundi,
        Korean,
        Kurdish,
        Laothian,
        Latin, 
        Latvian, Lettish,
        Lingala,
        Lithuanian,
        Macedonian,
        Malagasy,
        Malay,
        Malayalam,
        Maltese,
        Maori,
        Marathi,
        Moldavian,
        Mongolian,
        Nauru,
        Nepali,
        Norwegian,
        Occitan,
        Oriya,
        Pashto, Pushto,
        Persian,
        Polish,
        Portuguese,
        Punjabi,
        Quechua,
        Rhaeto_Romance,
        Romanian,
        Russian,
        Samoan,
        Sangro,
        Sanskrit,
        Scots_Gaelic,
        Serbian,
        Serbo_Croatian,
        Sesotho,
        Setswana,
        Shona,
        Sindhi,
        Singhalese,
        Siswati,
        Slovak,
        Slovenian,
        Somali,
        Spanish,
        Sudanese,
        Swahili,
        Swedish,
        Tagalog,
        Tajik,
        Tamil,
        Tatar,
        Tegulu,
        Thai,
        Tibetan,
        Tigrinya,
        Tonga,
        Tsonga,
        Turkish,
        Turkmen,
        Twi,
        Uigur,
        Ukrainian,
        Urdu,
        Uzbek,
        Vietnamese,
        Volapuk,
        Welch,
        Wolof,
        Xhosa,
        Yiddish,
        Yoruba,
        Zhuang,
        Zulu,
    }

    public static class Language
    {
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
    }
}
