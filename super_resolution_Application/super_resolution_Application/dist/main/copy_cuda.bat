set cudapath="C:\Program Files\NVIDIA GPU Computing Toolkit\CUDA\v10.0\bin"
copy %cudapath%\cublas64_100.dll env /v /y
copy %cudapath%\cudart32_100.dll env /v /y
copy %cudapath%\cudart64_100.dll env /v /y
copy %cudapath%\cudnn64_7.dll env /v /y
copy %cudapath%\cufft64_100.dll env /v /y
copy %cudapath%\cufftw64_100.dll env /v /y
copy %cudapath%\cuinj64_100.dll env /v /y
copy %cudapath%\curand64_100.dll env /v /y
copy %cudapath%\cusolver64_100.dll env /v /y
copy %cudapath%\cusparse64_100.dll env /v /y

pause
exit

copy %cudapath%\nppc64_100.dll env /v /y
copy %cudapath%\nppial64_100.dll env /v /y
copy %cudapath%\nppicc64_100.dll env /v /y
copy %cudapath%\nppicom64_100.dll env /v /y
copy %cudapath%\nppidei64_100.dll env /v /y
copy %cudapath%\nppif64_100.dll env /v /y
copy %cudapath%\nppig64_100.dll env /v /y
copy %cudapath%\nppim64_100.dll env /v /y
copy %cudapath%\nppist64_100.dll env /v /y
copy %cudapath%\nppisu64_100.dll env /v /y
copy %cudapath%\nppitc64_100.dll env /v /y
copy %cudapath%\npps64_100.dll env /v /y
copy %cudapath%\nvblas64_100.dll env /v /y
copy %cudapath%\nvgraph64_100.dll env /v /y
copy %cudapath%\nvrtc-builtins64_100.dll env /v /y
