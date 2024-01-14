# ResizeTheRecoveryPartition

> [!NOTE]  
> **This software is only compatible with GPT partition.**

---

Update issue: `KB5034441: Windows Recovery Environment update for Windows 10, version 21H2 and 22H2: January 9, 2024`

If you are getting 0x80070643 error code in your Windows 10 device and you are unable to install KB5034441 update in your device or unable to log into the Desktop, this application will help you in fixing the issue.

![image](https://github.com/AkramAlQaifi/ResizeTheRecoveryPartition/assets/80940602/3dd9da9c-9ea1-4c0e-a428-e11ccd17980e)

---

Microsoft said that this update requires 250 MB of free space in the recovery partition

> [!IMPORTANT] 
>This update requires 250 MB of free space in the recovery partition to install successfully. If the recovery partition does not have sufficient free space, this update will fail. In this case, you will receive the following error message:
>**0x80070643 - ERROR_INSTALL_FAILURE**
>To avoid this error or recover from this failure, please follow the Instructions to manually resize your partition to install the WinRE update and then try installing this update.

---
![image](https://github.com/AkramAlQaifi/ResizeTheRecoveryPartition/assets/80940602/27ffea82-83d4-4114-8797-36a4fae99b61)

## How to work

1. Open ResizeTheRecoveryPartition Application
2. Select the disk.
3. Select the partition to shrink by 250 MB.
4. Click the Run button.
5. Wait for the operation to complete.
6. After the operation completes, the program will display a message and then automatically close.
7. Check for updates from Windows Update and install all pending updates.
8. After all packages are updated, restart your computer.

---

## References
**Microsoft update** [**Article**](https://support.microsoft.com/en-us/topic/kb5034441-windows-recovery-environment-update-for-windows-10-version-21h2-and-22h2-january-9-2024-62c04204-aaa5-4fee-a02a-2fdea17075a8)

**Instructions to manually resize your partition to install the WinRE update** [**Article**](https://support.microsoft.com/topic/kb5028997-instructions-to-manually-resize-your-partition-to-install-the-winre-update-400faa27-9343-461c-ada9-24c8229763bf)

**Fix 0x80070643 - Error Install Failure when trying to install Windows Update** [**Article**](https://www.ghacks.net/2024/01/10/fix-0x80070643-error-install-failure-when-trying-to-install-windows-update/)

**Fix 0x80070643 Error Code from KB5034441 Update in Windows 10** [**Article**](https://www.askvg.com/fix-0x80070643-error-code-from-kb5034441-update-in-windows-10/)

---

> [!NOTE]  
> **I apologize for the rushed code and the unpolished design. I built this program in a hurry due to my busy schedule. I don't have the time to develop this program further, but any contributions are welcome.**
