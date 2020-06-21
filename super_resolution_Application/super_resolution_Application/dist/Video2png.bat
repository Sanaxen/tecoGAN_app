set PNGPATH=%1
set VIDEO=%2

del /Q %INPUTPATH%/%%04d.png
ffmpeg.exe -i %VIDEO% -vf "scale=iw/4:-1" -vcodec png %PNGPATH%/%%04d.png