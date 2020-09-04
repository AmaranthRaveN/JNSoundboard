# JN Soundboard
A program written in C# using the NAudio library that uses hotkeys to play sounds into a chosen sound device. It is similar to [EXP Soundboard](https://sourceforge.net/projects/expsoundboard/), except that JN Soundboard is not as cross-platform as EXP, but, there are more features in JN than EXP.

Want to help out? Make a pull request! :)

Features:
* Installer for the soundboard.
* Can play MP3, WAV, WMA, M4A, and AC3 audio files.
* Play sounds through any sound device. (speakers, virtual audio cable, etc.)
* Microphone loopback. (loops microphone sound through playback device)
* Add, edit, remove, and clear key combinations.
* Can play a random sound from a board. (just select a board then press edit to give it a hotkey)
* Save (and load) hotkeys to JSON file.
* Hotkey that stops currently playing sound.
* Record sound files using specified audio device using hotkeys. (as wav format)
* Set volume for each sound file by selecting the soundfile and typing the volume in and pressing set volume. (0.0 - 1.0 ie: 0% - 100%)
* Set recording directory to keep recordings in specific location. (saved in settings)
* Set push-to-talk key & window to have the soundboard automatically press PTT key whenever a sound is played, and release when sound is finished.
* Multiple board support, just create boards and save sounds to them. Select a board to view ONLY the sounds within it to minimize clutter in the soundfile list view.
Requires: 
* .NET Framework 4.6
* NAudio

How to play sound effects over microphone:
You can't really play it "over" the microphone, however you can route them both through a virtual audio cable.
To do that, first install a virtual audio cable (I recommend [VB-CABLE](http://vb-audio.pagesperso-orange.fr/Cable/index.htm)), set the playback device to the virtual audio cable, then set the loopback device to your microphone.
Lastly, in the application that is going to use the microphone, set the microphone device to "VB-Audio Virtual Cable".

Screenshots: 

![Main window](https://i.imgur.com/qFWhGF2.jpg)

![Add keys window](https://i.imgur.com/tnUnLNV.jpg)

![Settings window](https://i.imgur.com/yYsm1TR.jpg)

![Text-to-speech window](https://i.imgur.com/EoPayHn.png)

