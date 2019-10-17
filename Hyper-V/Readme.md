Shrink (*.vhd) File

Run CMD as Administrator

```cmd
diskpart
select vdisk file="C:\my.vhd"
attach vdisk readonly
compact vdisk
detach vdisk
```
