It's *really* easy to work with the DLL, however, getting an
ASP.NET application to run that wasn't created on your machine
can be a bit tricky due to IIS bindings and ASPNET process rights.


Installing the demo project
***************************

If you're rather new to ASP.NET, the easiest way to get the
demo working is probably to make it an application of the
default web site:

- Copy the templatedemo folder to your web root folder (e.g. C:\inetpub\wwwroot)
- Start "Internet Information Services" (Start - Settings - Control Panel - Administrative Tools)
- In IIS Manager, locate the templatedemo folder (under the Default Web Site)
- Rightclick it and select "Properties"
- Press the "Create" button (part of the Application Settings) and accept the default name
- Press OK

- Open the solution (demo application.sln) and run the demo


IMPORTANT: In any case, make sure that the ASPNET system
account has the privilegies to access the folder of the
web application or you will get a runtime exception.


If you still have troubles getting it to run, just create
a new project in VS.NET, set a reference to the engine's DLL
(in the lib folder) and import the web forms / templates.


Newsletter
**********

If you want to keep up-to-date, you may subscribe to the newsletter:
http://www.evolvesoftware.ch/newsletter/subscribe.aspx


Questions and Feedback
**********************

Please post questions at codeproject so other users have the
opportunity to get answers as well - I'll answer
your questions very quickly:
http://www.codeproject.com/aspnet/sumitemplatecontrols.asp


If you like the project, please vote for it at
CodeProject so it won't vanish too soon. If you
don't like it, I guess it's only fair if you rate
it as well :-)


Have fun!

Philipp
codeproject@evolvesoftware.ch