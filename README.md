![banner (3)](https://github.com/ntdll0/HiddenVNC/assets/164230949/23e37b1e-c304-4a3d-81eb-88f6fec4cada)
# Hidden VNC
My own unique conceptual implementation of technique called HVNC (Hidden VNC or sometimes also Hidden Desktop).<br>
> Client written in C++ with use of OpenCV for JPEG encoding and image resizing and WinSock for networking.<br>
> Server written in C# without use of any 3D party libraries.<br>

**About Functionality**<br>
We are using a safe queue system with mutex for synchronization between threads.<br>
In order to save some traffic, we are comparing each new MAT with previous one with a treshold that can be changed in settings.<br>
In server settings, we can configure things such as image quality (resize), JPEG compression quality, input cooldown (for receiving commands, keyboard events and mouse clicks) and process cooldown (between sending frames).<br>
Networking is quite simple here, as it's not main focus of this POC, but yet still produces some tolerable results.
![image](https://github.com/ntdll0/HiddenVNC/assets/164230949/91a010f0-6e45-4c81-8c7d-28e197b532be)

## What is HVNC?
HVNC, standing for "Hidden Virtual Network Computing" is a 
technique deployed allowing hidden stealthy remote control
with an experience similar to classic old fashioned remote desktop.
The main difference here is, that everything is on (for user invisible) virtual desktop.
Creating a virtual desktop is part of a feature that dates back to the days of windows XP, when it was first added.

A classic VNC usually interacts with remote client simply by emulating mouse clicks on a specific coordinates.
However given the fact that the desktop is not set as active one in the time we are working with it, 
we are unable to use classic approach as normal VNC's use.
Because of that, things are gonna get here a bit more complicated.
Crafting a fully fledged proof of concept requires us to write our own window manager,
enumerate windows belonging to our virtual desktop an manually paint each one on a bitmap by their Z order.
In order to emulate a mouse click, we have to use functions such as SendMessage or PostMessage,
manually compute relative coordinates and found a window which lies on coordinates of the click.

## Here, I provide a list of commonly used WINAPI functions and their use in our implementation:
`CreateDesktop` - Creates a new virtual workstation with custom name<br>
`SendMessage` - Sends a message to specific window by it's handle. Windows use these messages for events and communication<br>
`SetThreadDesktop` - Allows us to switch between virtual desktop we interact with on current thread<br>
`EnumWindows` - We need to keep track of windows and enumerate all of windows that belong to our virtual desktop<br>
`PrintWindow` - A function that is not commonly use today, used to copy window visual context into DC (works even when the window is not actively used desktop)<br>
`WindowFromPoint` - Quite useful function that helps us to find a window by desktop coordinates <br>
*PrintWindow requires us to specify a flag which in our case can be PW_RENDERFULLCONTENT from windows 8.1 and on. Not specifying this value will result in
old looking window borders to be present and also less compatibility with some browsers that use hardware acceleration (at least from my testing)*
<br><br>

## Why publishing a Proof Of Concept in a first place?
Hidden vnc has been observed in the wild already a plenty of times.<br>
Publishing a POC work may be cruical in spreading more knowledge and awareness about the subject.<br>
Also, given the proportions of hidden vnc, it could indeed be levaraged by redteams to commit ethical testing.<br>
As always, you take full responsibility from usage of this code on your own.<br>
Code here is provided strictly for academical, research and ethical purposes.<br>
I am publishing a release here, configured only for local connection and with a prompt on each start informing the user.<br>

### Note
> Code licensed under Gnu General Public License V3 https://www.gnu.org/licenses/gpl-3.0.en.html
