using System.Windows;
using System.Windows.Controls;

namespace ResizeTheRecoveryPartition
{
    public partial class MainWindow : Window
    {
        DiskHelper diskHelper = new DiskHelper();
        //List<DiskInfo> diskInfoList;
        List<PartitionInfo> partitionInfoList;
        public MainWindow()
        {
            InitializeComponent();
            LoadDisks();
        }

        private void LoadDisks()
        {
            partitionInfoList = diskHelper.PartitionInfoList;

            foreach (var diskInfo in diskHelper.DiskInfoList)
                diskComboBox.Items.Add(diskInfo);

            if(diskComboBox.Items.Count ==1)
                diskComboBox.SelectedItem = diskComboBox.Items[0];
        }

        private void RunButton_Click(object sender, RoutedEventArgs e)
        {
            var selectedDisk = diskComboBox.SelectedItem as DiskInfo;

            if (selectedDisk==null)
            {
                MessageBox.Show("Please select a disk.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (selectedDisk.Is700OrLessThen==false)
            {
                MessageBox.Show("You cannot continue, the recovery volume of the current disk is good", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            var selectedPartition = partitionInfoComboBox.SelectedItem as PartitionInfo;

            if (selectedPartition == null)
            {
                MessageBox.Show("Please select a disk partition.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            RunButton.IsEnabled = false;
            RunButton.Content = "Waite";

            outputTextBox.AppendText(CMDCommands.ExecuteCmdCommandWithoutExitCode("reagentc /info"));
            outputTextBox.AppendText(CMDCommands.ExecuteCmdCommandWithoutExitCode("reagentc /disable"));

            outputTextBox.AppendText( diskHelper.SetNewRecoverySize(selectedDisk.DiskId, selectedPartition.PartitionNumber, selectedDisk.RecoveryPartitionNumber));

            outputTextBox.AppendText(CMDCommands.ExecuteCmdCommandWithoutExitCode("reagentc /enable"));
            outputTextBox.AppendText(CMDCommands.ExecuteCmdCommandWithoutExitCode("reagentc /info"));

            MessageBox.Show("Successfully done,\r\nWe recommend restarting your computer.", "Successfully", MessageBoxButton.OK, MessageBoxImage.Information);
            Application.Current.Shutdown();
        }

        private void diskComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedDisk = diskComboBox.SelectedItem as DiskInfo;

            if (selectedDisk == null)
            {
                MessageBox.Show("Please select a disk.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            partitionInfoComboBox.Items.Clear();
            foreach (var partitionInfo in partitionInfoList)
            {
                partitionInfoComboBox.Items.Add(partitionInfo);
            }
            partitionInfoComboBox.IsEnabled = true;
        }
    }
}