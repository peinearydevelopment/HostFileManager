using System.Collections.Generic;
using System.IO;
using System.Security.AccessControl;

namespace HostFileManager
{
    public class HostFileManager
    {
        private const string HostsFile = @"\Windows\System32\Drivers\Etc\Hosts";
        private const string HostsFileTemp = HostsFile + "-tmp";
        private const string HostsBackup = HostsFile + ".BAK";
        private List<string> _lines;

        public static string HostsFileName => HostsFile;

        public List<string> ReadFile()
        {
            return new List<string>(File.ReadAllLines(HostsFile));
        }

        public void ReplaceFile(List<string> newLines)
        {
            File.WriteAllLines(HostsFileTemp, newLines);
            CopySecurityInformation(HostsFile, HostsFileTemp);
            File.Replace(HostsFileTemp, HostsFile, HostsBackup);
        }

        // From http://msdn.microsoft.com/en-us/library/system.io.file.setaccesscontrol.aspx
        private static void CopySecurityInformation(string source, string dest)
        {
            var sourceFileSecurity = File.GetAccessControl(source, AccessControlSections.All);
            var destFileSecurity = new FileSecurity();
            var sourceDescriptor = sourceFileSecurity.GetSecurityDescriptorSddlForm(AccessControlSections.All);
            destFileSecurity.SetSecurityDescriptorSddlForm(sourceDescriptor);
            File.SetAccessControl(dest, sourceFileSecurity);

            var fileAttributes = File.GetAttributes(source);
            File.SetAttributes(dest, fileAttributes);
        }
    }
}
