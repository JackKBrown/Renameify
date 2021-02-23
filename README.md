# Renameify
simple tool to recursively prepend and or append text to filenames


## TODO LIST

* The preview should have ticky boxes so you can not apply it to specific files. 
* allow trailing whitespace between the list of things to copy
* multiple things to exclude seperated by commas like the copy files list
* config options. Remember previously input files and text withing options after closing application then reopening 
* allow saved presets for the above config.
* add another box instead of destination directory that it copies to instead of destination if it isn't empty and if it is empty default to renamify destination box.
* update the preivew after copying files (BUG)
* when opening a directory to select if a directory is already selected open up the view to that directory.

## DONE LIST

### V0.2 21-01-21
* File > exit works now
* Clicking the box left of  "select" showing folder path should function like clicking select. (So when left side of window is shrunk, it's still functional)
* named the thing which appears in the window-top appropriately
* Align the left size of the prepend and append typey boxes with each other.
* Added a toggle button to turn on/off the path to the file at the right.
* auto preview
* In the select menu, clicking cancel should retain the previously entered folder instead of remove it. 
* There should be a exclude box to auto make it not apply to files with specified text in
* Added find replace function

### V0.3 23-02-21
* Added a copy box that copies files from src directory to the root of the destination directory which is also the directory to renameify things in.
