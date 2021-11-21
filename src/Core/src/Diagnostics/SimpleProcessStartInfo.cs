// Copyright (c) Teroneko.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Diagnostics;

namespace Teronis.Diagnostics
{
    public class SimpleProcessStartInfo
    {
        internal ProcessStartInfo ProcessStartInfo;

        /// <summary>
        /// Creates 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="args"></param>
        /// <param name="workingDirectory">The working directory is not used to find the executable. Instead, its value applies to the process that is started and only has meaning within the context of the new process.</param>
        public SimpleProcessStartInfo(
            string name,
            string? args = null,
            string? workingDirectory = null)
        {
            if (args is null) {
                ProcessStartInfo = new ProcessStartInfo(name);
            } else {
                ProcessStartInfo = new ProcessStartInfo(name, args);
            }

            if (workingDirectory != null) {
                ProcessStartInfo.WorkingDirectory = workingDirectory;
            }

            ProcessStartInfo.UseShellExecute = false;
            ProcessStartInfo.RedirectStandardOutput = true;
            ProcessStartInfo.RedirectStandardError = true;
            ProcessStartInfo.CreateNoWindow = true;
        }
    }
}
