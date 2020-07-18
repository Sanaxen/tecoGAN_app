set FRAME_RATE=%1
set IMAGEDIR=%2
set SCALE=%3

if "%3"=="" set SCALE=2

@echo off
setlocal enabledelayedexpansion
set n=10000
for /f "usebackq delims=" %%i in (`dir /b /on %IMAGEDIR%\*.png`) do (
    move "%IMAGEDIR%\%%~i" "%IMAGEDIR%\!n:~-4!.png"
    set /a n+=1
)
endlocal

del test.mp4
ffmpeg -r %FRAME_RATE% -i %IMAGEDIR%\%%04d.png -vcodec libx264 -pix_fmt yuv420p -vf "scale=trunc(iw/%SCALE%)*%SCALE%:trunc(ih/%SCALE%)*%SCALE%" -r %FRAME_RATE% test.mp4
