set FRAME_RATE=%1
set OUTPUTPATH=%2
set IMAGENAME=%3

ffmpeg -r %FRAME_RATE% -i %OUTPUTPATH%/%IMAGENAME%\output_%%04d.png -vcodec libx264 -pix_fmt yuv420p -vf "scale=trunc(iw/2)*2:trunc(ih/2)*2" -r %FRAME_RATE% tecoGAN.mp4
