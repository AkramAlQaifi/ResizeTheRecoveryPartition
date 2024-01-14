using System.Text.RegularExpressions;

namespace ResizeTheRecoveryPartition;
class DiskInfo
{
    public int DiskId { get; set; }
    public string Status { get; set; }
    public string Size { get; set; }
    public string Free { get; set; }
    public string RecoverySize { get; set; }
    public int RecoveryPartitionNumber { get; set; }

    public bool Is700OrLessThen {  get; set; }

    //public string Dyn { get; set; }
    //public string Gpt { get; set; }

    public static List<DiskInfo> ParseDiskInfo(string output)
    {
        List<DiskInfo> diskInfoList = new List<DiskInfo>();

        // Define a regular expression pattern to match the table rows
        string pattern = @"Disk (\d+)\s+(\w+)\s+([\d\s]+[A-Za-z]+\s*)\s+([\d\s]+[A-Za-z]+\s*)";

        // Use Regex to match the pattern in the output
        MatchCollection matches = Regex.Matches(output, pattern);

        foreach (Match match in matches)
        {
            DiskInfo diskInfo = new DiskInfo
            {
                DiskId = int.Parse(match.Groups[1].Value),
                Status = match.Groups[2].Value.Trim(),
                Size = match.Groups[3].Value.Trim(),
                Free = match.Groups[4].Value.Trim(),
            };

            diskInfoList.Add(diskInfo);
        }

        return diskInfoList;
    }

}

