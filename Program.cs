using System.Management;

namespace DeviceScanner
{
    public class Program
    {
        static void Main(string[] args)
        {
     
            ManagementObjectSearcher searcher = new ManagementObjectSearcher("SELECT * FROM Win32_PnPEntity");

            foreach (ManagementObject device in searcher.Get())
            {
                // Display device information
                Console.WriteLine("Device:");
                Console.WriteLine("  Name: " + device["Name"]);
                Console.WriteLine("  Description: " + device["Description"]);
                Console.WriteLine("  Manufacturer: " + device["Manufacturer"]);
                Console.WriteLine("  Status: " + device["Status"]);
                Console.WriteLine("  DeviceID: " + device["DeviceID"]);


                string classification = "Unknown";
                if (device["Description"] != null)
                {
                    string description = device["Description"].ToString().ToLower();
                    if (description.Contains("usb") || description.Contains("external"))
                    {
                        classification = "External";
                    }
                    else if (description.Contains("internal"))
                    {
                        classification = "Internal";
                    }
                }
                Console.WriteLine("  Classification: " + classification);

                PropertyData hardwareIds = device.Properties["HardwareID"];
                if (hardwareIds != null && hardwareIds.Value != null)
                {
                    string[] hardwareIdArray = (string[])hardwareIds.Value;
                    Console.WriteLine("  Hardware IDs:");
                    foreach (string hardwareId in hardwareIdArray)
                    {
                        Console.WriteLine("    " + hardwareId);
                    }
                }

         
                PropertyData compatibleIds = device.Properties["CompatibleID"];
                if (compatibleIds != null && compatibleIds.Value != null)
                {
                    string[] compatibleIdArray = (string[])compatibleIds.Value;
                    Console.WriteLine("  Compatible IDs:");
                    foreach (string compatibleId in compatibleIdArray)
                    {
                        Console.WriteLine("    " + compatibleId);
                    }
                }

                Console.WriteLine("---------------------------------------");

            }
        }
    }
}
