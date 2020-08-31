# JN Soundboard
A program written in C# using the NAudio library that uses hotkeys to play sounds into a chosen sound device. It is similar to [EXP Soundboard](https://sourceforge.net/projects/expsoundboard/), except that JN Soundboard is not as cross-platform as EXP, but, there are more features in JN than EXP.

Want to help out? Make a pull request! :)

Features:
* Can play MP3, WAV, WMA, M4A, and AC3 audio files
* Play sounds through any sound device (speakers, virtual audio cable, etc.)
* Microphone loopback (loops microphone sound through playback device)
* Add, edit, remove, and clear key combinations
* Can play a random sound out of multiple (just select multiple files when adding a hotkey)
* Save (and load) soundfiles/soundboards to JSON file(s).
* Hotkey that stops currently playing sound
* Hotkeys that load json files containing hotkeys
* Text-to-speech
* Record sound files using specified audio device (as wav format)
* Set volume for each sound file (0.0 - 1.0 ie: 0% - 100%)
* Set recording directory to keep recordings in specific location (saved in settings)
Requires: 
* .NET Framework 4.6
* NAudio

How to play sound effects over microphone:
You can't really play it "over" the microphone, however you can route them both through a virtual audio cable.
To do that, first install a virtual audio cable (I recommend VB-Audio's Cable & Voicemeeter)[VB-CABLE](https://www.vb-audio.com/Cable/index.htm), [VB-Voicemeeter](https://www.vb-audio.com/Voicemeeter/index.htm).
.
set the playback device to the virtual audio cable, then set the loopback device to your microphone.
Lastly, in the application that is going to use the microphone, set the microphone device to "VB-Audio Virtual Cable".

Screenshots: 

![Main window](https://i.imgur.com/qFWhGF2.jpg)

![Add keys window](https://i.imgur.com/tnUnLNV.jpg)

![Settings window](https://i.imgur.com/yYsm1TR.jpg)

![Text-to-speech window](https://i.imgur.com/EoPayHn.png)

