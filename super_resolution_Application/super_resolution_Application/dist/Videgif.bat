set VIDEO=%1
set FRAME_RATE=%2


ffmpeg -r %FRAME_RATE% -i %VIDEO% -vf "scale=trunc(iw/2)*2:trunc(ih/2)*2" -r %FRAME_RATE% output.gif
