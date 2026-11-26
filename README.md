## Credits and licenses

Made with Unity 6.3 LTS (6000.3.24f1).

### Graphics
- Pixel Platformer by Kenney, https://kenney.nl/assets/pixel-platformer, CC0

### Sound effects
All sound effects are from Interface Sounds by Kenney, https://kenney.nl/assets/interface-sounds, CC0, except enemy_windup.

| File | Event |
|---|---|
| jump | player jumps |
| coin | coin pickup |
| hit_1, hit_2, hit_3 | player takes damage (3 variants) |
| shoot | player shoots |
| ui_click | button click |
| win | win screen |
| lose | time is up |
| death | player dies |
| enemy_windup | enemy attack warning, made in jfxr (https://jfxr.frozenfractal.com/), own work |

### Music
| File | Where | Source | License |
|---|---|---|---|
| level_loop | levels | "Jump and Run game backround music" by Asep_bagus, https://pixabay.com/music/video-games-jump-and-run-game-backround-music-326882/ (marked as AI generated on Pixabay) | Pixabay Content License |
| menu_loop | menu and win screen | "Cyber Core" by oglsdl, https://opengameart.org/content/cyber-core | CC0 |

## Audio setup
- AudioMixer GameMix: Master, Music (child Tracks), SFX, UI.
- Music and SFX volume sliders use decibels (Log10 x 20) and are saved in save.json.
- Pause uses the Paused snapshot (Tracks: -6 dB and a lowpass filter).
- Hits and other repeated sounds use random pitch (0.92-1.08) and 3 clips.