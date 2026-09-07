# FTP client

Simple ftp client for android, ios, mac and windows using .net MAUI Blazor hybrid.

## Layout for android
<img src="README.images/main layout android.png" height="500">
On this screen you can see connection form with save, delete and load connection profiles 
<br/>
which holds host name, user name, password and port number.
<br/>
<br/>
Below that there is downloads path picker.
<br/>
<br/>
And the table below holds files and folders information with buttons at the top to
<br/>
download selected file, create directory, rename file/directory and delete file/directory.
<br/>
Next to those buttons there are connection icon, refresh button and return button.
<br/>
At the bottom of the table there is pagination toolbar.
<br/>
<br/>
And at the bottom of the screen there is file uploader.

## Content view for android
<img src="README.images/content view android.png" height="500">

- You can see here informations about files and folders on the server that are:
  - Name
  - Size
  - Last modification date
- When clicking on an item, you select it.

## Layout for windows
<img src="README.images/main layout windows.png" height="400">
It looks a little bit diffrent because of grid layout but functionality is the same.
<br/>
<br/>
On this screen you can see usage of file uploader.

## External libraries:
UI elements built with <a href="https://github.com/MudBlazor/MudBlazor">MudBlazor</a>.
<br/>
FTP protocol communication built with <a href="https://github.com/robinrodricks/FluentFTP">FluentFTP</a>.
<br/>
Connection profiles and downloads path are stored in <a href="https://github.com/praeclarum/sqlite-net">SQLite local DataBase for .net</a>.
