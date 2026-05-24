using System.Windows;

namespace WinLens.Models;

/// <summary>
/// One OCR-detected line of text. Coordinates are in source-image pixels
/// (same coordinate space as the captured screenshot).
/// </summary>
public sealed class OcrBlock
{
    public required string OriginalText { get; init; }
    public string TranslatedText { get; set; } = "";
    public required Rect BoundingRect { get; init; }
    public string? DetectedLanguage { get; init; }
}
