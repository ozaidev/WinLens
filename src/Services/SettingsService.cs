using System;
using System.Globalization;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using WinLens.Models;

namespace WinLens.Services;

public sealed class SettingsService
{
    private static readonly string Dir =
        Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "WinLens");
    private static readonly string FilePath = Path.Combine(Dir, "settings.json");

    private static readonly JsonSerializerOptions Json = new()
    {
        WriteIndented = true,
        Converters = { new JsonStringEnumConverter() },
    };

    public UserSettings Current { get; private set; } = new();

    /// <summary>True when settings were read from an existing file (i.e. not a first run).</summary>
    public bool LoadedFromDisk { get; private set; }

    public UserSettings Load()
    {
        bool loadedFromDisk = false;
        try
        {
            if (File.Exists(FilePath))
            {
                var json = File.ReadAllText(FilePath);
                var loaded = JsonSerializer.Deserialize<UserSettings>(json, Json);
                if (loaded != null)
                {
                    Current = loaded;
                    loadedFromDisk = true;
                }
            }
        }
        catch
        {
            // Corrupt file — ignore and fall back to defaults.
        }

        if (!loadedFromDisk)
        {
            // First run: pick the user's Windows display language as the default
            // target. Use the OS API instead of CultureInfo because the .NET
            // CLI / hosting can override the process culture to en-US.
            var sys = GetUserDisplayLanguage();
            if (!string.IsNullOrEmpty(sys))
                Current.TargetLanguage = sys;
        }

        LoadedFromDisk = loadedFromDisk;
        return Current;
    }

    [DllImport("kernel32.dll", CharSet = CharSet.Unicode)]
    private static extern int GetUserDefaultLocaleName(StringBuilder lpLocaleName, int cchLocaleName);

    private static string GetUserDisplayLanguage()
    {
        try
        {
            var sb = new StringBuilder(85); // LOCALE_NAME_MAX_LENGTH
            if (GetUserDefaultLocaleName(sb, sb.Capacity) > 0)
            {
                var name = sb.ToString();
                if (!string.IsNullOrEmpty(name))
                    return name.Split('-')[0];
            }
        }
        catch { /* fallback below */ }

        try { return CultureInfo.InstalledUICulture.TwoLetterISOLanguageName; }
        catch { return "en"; }
    }

    public void Save(UserSettings settings)
    {
        Current = settings;
        Directory.CreateDirectory(Dir);
        File.WriteAllText(FilePath, JsonSerializer.Serialize(settings, Json));
    }
}
