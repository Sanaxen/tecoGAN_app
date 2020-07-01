copy %1 %3 /v /y
magick.exe %1 -affine %2 %3
:magick.exe %1 -gravity NorthEast %3
:echo magick.exe %1 -affine %2 %3
:pause
