# Changelog

All notable changes to this project are documented here. The format is based on
[Keep a Changelog](https://keepachangelog.com/), and the project follows
[Semantic Versioning](https://semver.org/).

## [1.2.0] - 2026-05-25

### Added
- Full set of around 100 translation target languages (was 12). Pick any of them directly;
  the dropdown supports type-to-jump.
- Your most-recently-used target languages are pinned to the top of the picker for quick switching.

### Changed
- The overlay's right-click menu now matches the dark theme, and the text-selection highlight
  is clearer so you can see what you're selecting.

### Removed
- The translation-provider note in the settings footer.

## [1.1.1] - 2026-05-25

### Performance
- Memory is now released after each translation. The overlay frees the full-screen screenshot
  and per-line boxes on close, the reclaim runs once the window is torn down, and the working
  set is trimmed so the process returns to a few MB at idle instead of lingering around 200 MB.

## [1.1.0] - 2026-05-25

### Added
- "Translating…" spinner while a capture is being processed, so it's clear work is happening.
- The app logo is now used as the brand mark in the control panel and the translation overlay.

### Changed
- OCR recognizers now run in parallel instead of one after another, so a multi-language screen
  translates noticeably faster.

### Performance
- Idle memory dropped from about 350 MB to about 94 MB, and the per-translation spike is lower.
  The screenshot is now copied once on its way to the OCR engine instead of several times, and
  the large buffers from each cycle are reclaimed afterwards.

## [1.0.0] - 2026-05-25

First public release.

### Added
- In-place screen translation: press a global hotkey and every on-screen line is translated
  over the original, with a matched background and an auto-fit font.
- System-tray app with a dark control panel (target language, hotkey, source language,
  launch at startup, translate now).
- Translation overlay with a live target-language picker, and copy original / translation.
- Windows OCR pipeline with a multi-script auto mode: it runs every installed recognizer and
  keeps the blocks each one reads well (Latin and CJK on the same screen), then drops overlaps.
- 2x upscaling before OCR for better accuracy on small UI text.
- "Add OCR languages in Windows" link; installed packs are detected live.
- Per-monitor-v2 DPI awareness and multi-monitor virtual-screen capture.
- `--settings` and `--translate` command-line flags.

[1.2.0]: https://github.com/marco-beltrame/WinLens/releases/tag/v1.2.0
[1.1.1]: https://github.com/marco-beltrame/WinLens/releases/tag/v1.1.1
[1.1.0]: https://github.com/marco-beltrame/WinLens/releases/tag/v1.1.0
[1.0.0]: https://github.com/marco-beltrame/WinLens/releases/tag/v1.0.0
