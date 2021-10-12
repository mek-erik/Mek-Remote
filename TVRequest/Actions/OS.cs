using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;


namespace TVRequest.Actions
{
    class OS
    {
        public static string GetOS()
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
            {
                return "mac";
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                return "windows";
            }
            else // TODO: Linux
            {
                return "windows";
            }
        }
    }
}
