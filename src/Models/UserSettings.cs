using System.Windows.Input;
using WinLens.Native;

namespace WinLens.Models;

public sealed class UserSettings
{
    public string TargetLanguage { get; set; } = "en";

    /// <summary>OCR source language tag, or "auto" to try all installed recognizers.</summary>
    public string SourceLanguage { get; set; } = "auto";

    public HotkeyModifiers HotkeyModifiers { get; set; } = HotkeyModifiers.Control | HotkeyModifiers.Alt;
    public Key HotkeyKey { get; set; } = Key.T;
}
