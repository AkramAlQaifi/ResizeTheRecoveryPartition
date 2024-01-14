using System.Text.RegularExpressions;

namespace ResizeTheRecoveryPartition
{
    internal class PartitionInfo
    {
        public int DiskId { get; set; }
        public int PartitionNumber { get; set; }
        public string Type { get; set; }
        public string Size { get; set; }
        public string Offset { get; set; }

        public static List<PartitionInfo> ParsePartitionInfo(string output)
        {
            List<PartitionInfo> partitionInfoList = new List<PartitionInfo>();

            // Define a regular expression pattern to match the table rows
            string pattern = @"Partition (\d+)\s+(\w+)\s+([\d\s]+[A-Za-z]+\s*)\s+([\d\s]+[A-Za-z]+\s*)";

            // Use Regex to match the pattern in the output
            MatchCollection matches = Regex.Matches(output, pattern);

            foreach (Match match in matches)
            {
                PartitionInfo partitionInfo = new PartitionInfo
                {
                    PartitionNumber = int.Parse(match.Groups[1].Value),
                    Type = match.Groups[2].Value,
                    Size = match.Groups[3].Value.Trim(),
                    Offset = match.Groups[4].Value.Trim()
                };

                partitionInfoList.Add(partitionInfo);
            }

            return partitionInfoList;
        }
    }
}
