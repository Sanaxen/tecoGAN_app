set dir=%~dp0
set python_venv=%~dp0\env
:pause


set CUDA_VISIBLE_DEVICES=0
PATH=%python_venv%;%python_venv%\Library\mingw-w64\bin;%python_venv%\Library\usr\bin;%python_venv%\Library\bin;%python_venv%\Scripts;%python_venv%\bin;%PATH%
