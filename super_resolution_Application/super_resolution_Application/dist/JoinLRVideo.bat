del outpu.mp4
ffmpeg -i %1 -i %2 -filter_complex  "nullsrc=size=1280x480 [base]; [0:v] setpts=PTS-STARTPTS, scale=640x480 [left]; [1:v] setpts=PTS-STARTPTS, scale=640x480 [right]; [base][left] overlay=shortest=1 [tmp1]; [tmp1][right] overlay=shortest=1:x=640"^
   output.mp4