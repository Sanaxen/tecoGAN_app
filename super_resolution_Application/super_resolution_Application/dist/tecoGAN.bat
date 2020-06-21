set current_dir=%~dp0
set TECOGANPATH=%current_dir%/main
set INPUTPATH=%1
set OUTPUTPATH=%2
set IMAGENAME=%3

del /Q ./results/%IMAGENAME%/*.png

%TECOGANPATH%/main.exe --cudaID 0 --output_dir %OUTPUTPATH% --summary_dir %OUTPUTPATH%/log/ ^
--mode inference --input_dir_LR %INPUTPATH% --output_pre %IMAGENAME% ^
--num_resblock 16  --checkpoint %TECOGANPATH%/model/TecoGAN --output_ext png


:python main.py --cudaID 0 --output_dir ./results/ --summary_dir ./results/log/ --mode inference --input_dir_LR ./LR/calendar --output_pre calendar  --num_resblock 16  --checkpoint ./model/TecoGAN --output_ext png
