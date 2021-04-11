# Debian

# Add User my to sudo group
```sh
su root
sudo adduser my sudo # Add user my to sudo users
# restart
```

# Fullscreen in Hyper-V
```sh
sudo nano /etc/default/grub
GRUB_CMDLINE_LINUX_DEFAULT=”quiet splash video=hyperv_fb:1920x1080”
sudo update-grub
# restart
```

# Install Chrome
Download file: dpkg -i google-chrome-stable_current_amd64.deb
```sh
sudo apt install fonts-liberationsudo # dependency
dpkg -i google-chrome-stable_current_amd64.deb
```

# Install Visual Code
Download file: code_1.55.1-1617808414_amd64.deb
```sh
dpkg -i code_1.55.1-1617808414_amd64.deb
```

# Install SSH for Putty
```sh
sudo apt-get update
sudo install openssh-server
sudo systemctl status ssh # Check status
```

# IP Address
```sh
ip address # Search inet 0.0.0.0
sudo apt install hyperv-daemons # Shows ip address in Hyper-V manager
```

# Install Git
```sh
sudo apt install git
git --version
```

# Install .NET 5
https://docs.microsoft.com/en-us/dotnet/core/install/linux-debian

# Install Node.js
https://github.com/nodesource/distributions/blob/master/README.md
```sh
su root
# Using Debian, as root
curl -fsSL https://deb.nodesource.com/setup_lts.x | bash -
apt-get install -y nodejs
node -v
```

# Install MS-SQL Server
https://docs.microsoft.com/en-us/sql/linux/quickstart-install-connect-ubuntu?view=sql-server-ver15
Follow: For Ubuntu 18.04:
```sh
sudo add-apt-repository "$(wget -qO- https://packages.microsoft.com/config/ubuntu/18.04/mssql-server-2019.list)"
sudo apt-get update
sudo apt-get install -y mssql-server
sudo /opt/mssql/bin/mssql-conf setup
systemctl status mssql-server --no-pager # Check status

# MS-SQL server tools
curl https://packages.microsoft.com/config/ubuntu/18.04/prod.list | sudo tee /etc/apt/sources.list.d/msprod.list
sudo apt-get update 
sudo apt-get install mssql-tools unixodbc-dev
sudo apt-get update 
sudo apt-get install mssql-tools
echo 'export PATH="$PATH:/opt/mssql-tools/bin"' >> ~/.bash_profile
# Close terminal
sqlcmd -S localhost -U SA -P '<YourPassword>'
CREATE DATABASE ApplicationDemo
GO
EXIT
```
