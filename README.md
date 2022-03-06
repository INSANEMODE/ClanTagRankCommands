# ClanTagRankCommands

# This Project is unmaintained, and no longer works on latest versions of IW4M-Admin.

Please considor making your own plugin, and pair it with https://github.com/fedddddd/t6-gsc-utils for functions like setting clan tag, renaming, etc.

c# plugin for iw4m-admin that interacts with ClanTagRank to set the ClanTag of players, either by iw4m-admin permission level, or a custom one set with ```!setRank <player> <tag>```.


# Commands
```(!sr) !setRank <player> <tag>```

```(!rr) !ResetRank <player>```

by default, these require permission level Admin or above, this can be changed,

along with the command names and alias in ```./Configuration/CommnadConfiguration.json```

# Installation
download the latest ClanTagRankCommands.dll from the releases page and place it in the Plugins folder of your iw4m-admin folder, and then head over to https://github.com/INSANEMODE/ClanTagRank and follow the instructions there.

# Configuration
You can change the default rank names in ```./Configuration/ClanTagRankCommands.json```
if you do not have it, make sure you are up to date, and you run iw4m-admin to generate the default Configuration.
