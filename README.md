# AutoVPNConnection

Automatically monitors and maintains connection to your VPN.

## Features

- Checks VPN connection status every 5 minutes
- Automatically connects to "YOUR-VPN-NAME-HERE" if disconnected
- Runs silently in the background (no windows)
- Error logging to `%LOCALAPPDATA%\AutoVPNConnection\errors.log`

## Setup for Automatic Startup

### Option 1: Task Scheduler (Recommended)

1. Build the application in Release mode
2. Open Task Scheduler (search for "Task Scheduler" in Windows)
3. Click "Create Task" (not "Create Basic Task")
4. **General tab:**
   - Name: "Auto VPN Connection"
   - Check "Run whether user is logged on or not"
   - Check "Run with highest privileges"
   - Configure for: Windows 10/11
5. **Triggers tab:**
   - New Trigger
   - Begin the task: "At startup"
   - Delay task for: 30 seconds (to allow network initialization)
6. **Actions tab:**
   - New Action
   - Action: "Start a program"
   - Program/script: Browse to `AutoVPNConnection.exe` in your bin\Release folder
7. **Conditions tab:**
   - Uncheck "Start the task only if the computer is on AC power"
8. Click OK and enter your Windows password

### Option 2: Startup Folder

1. Build the application in Release mode
2. Press `Win + R` and type: `shell:startup`
3. Create a shortcut to `AutoVPNConnection.exe` in this folder
4. Right-click the shortcut → Properties → Run: Minimized

## VPN Requirements

- The VPN connection "YOUR-VPN-NAME-HERE" must be already configured in Windows
- The VPN should be configured to connect without prompting for credentials (or use saved credentials)

## Troubleshooting

- Check error logs at: `%LOCALAPPDATA%\AutoVPNConnection\errors.log`
- Ensure the VPN connection name matches exactly: "YOUR-VPN-NAME-HERE"
- Verify VPN credentials are saved in the connection settings
- Run the application manually first to test connectivity

## Customization

To change settings, edit `Program.cs`:
- `VPN_NAME`: Change the VPN connection name
- `CHECK_INTERVAL_MINUTES`: Change how often to check (default: 5 minutes)
