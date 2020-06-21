set FRAME_RATE=%1
set IMAGEDIR=%2

@echo off
setlocal enabledelayedexpansion
set n=10000
for /f "usebackq delims=" %%i in (`dir /b /on %IMAGEDIR%\*.png`) do (
    move "%IMAGEDIR%\%%~i" "%IMAGEDIR%\!n:~-4!.png"
    set /a n+=1
)
endlocal

ffmpeg -r %FRAME_RATE% -i %IMAGEDIR%\%%04d.png -vcodec libx264 -pix_fmt yuv420p -vf "scale=trunc(iw/2)*2:trunc(ih/2)*2" -r %FRAME_RATE% test.mp4
