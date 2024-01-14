using System.Text.RegularExpressions;

namespace ResizeTheRecoveryPartition
{
    internal class DiskHelper
    {
        public DiskHelper()
        {
            SetValues();
        }
        public List<DiskInfo> DiskInfoList;
        public List<PartitionInfo> PartitionInfoList;

        private void SetValues()
        {
            DiskInfoList = new List<DiskInfo>();
            PartitionInfoList = new List<PartitionInfo>();

            string output = CMDCommands.CreateDiskpartScriptCommand("list disk");

            // Parse the disk information
            List<DiskInfo> diskInfoList = DiskInfo.ParseDiskInfo(output);

            // Example: Display information in a ComboBox
            foreach (var diskInfo in diskInfoList)
            {
                //Console.WriteLine($"Disk {diskInfo.DiskId}: {diskInfo.Status}, Size: {diskInfo.Size}, Free: {diskInfo.Free}");

                string partitionInfoOutput = CMDCommands.CreateDiskpartScriptCommand($"sel disk {diskInfo.DiskId}\nlist part");

                // Parse the partition information
                List<PartitionInfo> partitionInfoList = PartitionInfo.ParsePartitionInfo(partitionInfoOutput);

                if (partitionInfoList.Count > 0 && partitionInfoList.Any(x => x.Type.ToLower().Contains("recovery")) && partitionInfoList.Any(x => x.Type.ToLower().Contains("primary")))
                {
                    foreach (var partitionInfo in partitionInfoList.Where(x => x.Type.ToLower().Contains("primary")))
                    {
                        //Console.WriteLine($"Partition {partitionInfo.PartitionNumber}: Type: {partitionInfo.Type}, Size: {partitionInfo.Size}, Offset: {partitionInfo.Offset}");
                        partitionInfo.DiskId = diskInfo.DiskId;
                        PartitionInfoList.Add(partitionInfo);
                    }
                    var recoveryPartition = partitionInfoList.FirstOrDefault(x => x.Type.ToLower().Contains("recovery"));
                    if (recoveryPartition != null)
                    {
                        diskInfo.RecoveryPartitionNumber = recoveryPartition.PartitionNumber;
                        diskInfo.RecoverySize = recoveryPartition.Size;
                        diskInfo.Is700OrLessThen = NumberExtractor.ExtractNumber(recoveryPartition.Size) <= 700;
                    }

                    DiskInfoList.Add(diskInfo);
                }

            }
        }

        public string SetNewRecoverySize(int diskId, int partitionId, int diskRecoveryPartitionNumber)
        {
            string fileData =string.Format(
                "list disk\n"+
                "sel disk {0}\n"+
                "list part\n"+
                "sel part {1}\n"+
                "shrink desired=250 minimum=250\n"+
                "sel part {2}\n"+
                "delete partition override\n"+
                "list disk\n"+
                "create partition primary id=de94bba4-06d1-4d40-a16a-bfd50179d6ac\n"+
                "gpt attributes =0x8000000000000001\n"+
                "format quick fs=ntfs label=”Windows RE tools”\n"+
                "list vol\n"+
                "exit" , diskId, partitionId, diskRecoveryPartitionNumber);

            var output = CMDCommands.CreateDiskpartScriptCommand(fileData);
            return output;
        }
    }
}
