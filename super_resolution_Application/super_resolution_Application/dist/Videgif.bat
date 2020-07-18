set VIDEO=%1
set FRAME_RATE=%2
set SCALE=%3

if "%3"=="" set SCALE=2



ffmpeg -r %FRAME_RATE% -i %VIDEO% -vf "scale=trunc(iw/%SCALE%)*%SCALE%:trunc(ih/%SCALE%)*%SCALE%" -r %FRAME_RATE% output.gif
