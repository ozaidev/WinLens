namespace WinLens.Models;

/// <summary>
/// Single source of truth for the target languages offered across the UI
/// (settings window + overlay picker). Codes are ISO-639-1.
/// </summary>
public static class LanguageList
{
    public static readonly (string Code, string Label)[] Items =
    {
        ("en", "English"),
        ("it", "Italiano"),
        ("es", "Español"),
        ("fr", "Français"),
        ("de", "Deutsch"),
        ("pt", "Português"),
        ("nl", "Nederlands"),
        ("ja", "日本語"),
        ("zh", "中文"),
        ("ko", "한국어"),
        ("ru", "Русский"),
        ("ar", "العربية"),
    };

    public static string LabelFor(string code)
    {
        foreach (var (c, label) in Items)
            if (string.Equals(c, code, System.StringComparison.OrdinalIgnoreCase))
                return label;
        return code;
    }
}
