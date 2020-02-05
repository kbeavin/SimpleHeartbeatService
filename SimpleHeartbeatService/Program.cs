using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Topshelf;

namespace SimpleHeartbeatService // Used Tim Coreys youtube video https://www.youtube.com/watch?v=y64L-3HKuP0
{
    class Program
    {
        static void Main(string[] args)
        {
            var exitCode = HostFactory.Run(x => // TopShelf specific code they have good documentation http://docs.topshelf-project.com/en/latest/
            {
                x.Service<Heartbeat>(s => // we can not use x for lambda here becuase we are already in it so use s for service
                {
                    s.ConstructUsing(heartbeat => new Heartbeat()); // using empty constructor
                    s.WhenStarted(heartbeat => heartbeat.Start()); // when you want to start call the start method on the heartbeat class
                    s.WhenStopped(heartbeat => heartbeat.Stop()); // when you want to stop call the stop method on the heartbeat class
                });

                x.RunAsLocalSystem(); // this will give us all the permissions we need 

                x.SetServiceName("HeartbeatService"); // Set the machine friendly name
                x.SetDisplayName("Heartbeat Service"); // Set the user friendly name
                x.SetDescription("This is the sample heartbeat service used in a YouTube demo."); // Set the description
            });

            int exitCodeValue = (int)Convert.ChangeType(exitCode, exitCode.GetTypeCode()); // We need to cast exitCode to int becuase the output of HostFactory.Run() is an Enum
            Environment.ExitCode = exitCodeValue; // If the Main method returns void, you can use this property to set the exit code that will be returned to the calling environment. Per Microsoft Docs
        }
    }
}

/* Simple Deployment

    1) Right click SimpleHeartbeatService project and select open folder in file explorer
    2) Navigate to bin then to Debug and copy all contents (dont actually need pdb)
    3) Then create new folder and paste contents to folder (C:\temp\HeartbeatService is where I did this locally)
    4) For Actual Squirrel like Deployment Go on and figure that out on your own otherwise this is the simplest deployment
     
*/

/* Install service to Services Manager
 
    1) Run Administrator Command Prompt: press win to open start menu, type cmd then pres ctrl + shift + enter
    2) In command prompt type cd then paste in path in my case C:\temp\HeartbeatService press enter
    3) Run "simpleheartbeatservice.exe install start" this command will install that service and start it too
    4) You can test by monitoring txt file with notepad++ and starting stopping with the service manager
    5) Run "simpleheartbeatservice.exe uninstall" do not leave this example running or it will blow up your computer!
     
*/