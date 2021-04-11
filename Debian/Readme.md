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
