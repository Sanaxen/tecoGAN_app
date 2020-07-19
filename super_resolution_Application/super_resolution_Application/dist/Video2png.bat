set PNGPATH=%1
set VIDEO=%2
set SCALE=%3

if "%3"=="" set SCALE=4

del /Q %INPUTPATH%\*.png
del /Q %PNGPATH%\*.png
ffmpeg.exe -i %VIDEO% -vf "scale=iw/%SCALE%:-1" -vcodec png %PNGPATH%/%%04d.png