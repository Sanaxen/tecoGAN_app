set FRAME_RATE=%1
set OUTPUTPATH=%2
set IMAGENAME=%3
set SCALE=%4

if "%4"=="" set SCALE=2

del tecoGAN.mp4
ffmpeg -r %FRAME_RATE% -i %OUTPUTPATH%/%IMAGENAME%\output_%%04d.png -vcodec libx264 -pix_fmt yuv420p -vf "scale=trunc(iw/%SCALE%)*%SCALE%:trunc(ih/%SCALE%)*%SCALE%" -r %FRAME_RATE% tecoGAN.mp4
