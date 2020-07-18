set current_dir=%~dp0

cd main
call envset.bat

cd tecoGAN
del /Q ./results/LR/calendar/*.png

python main.py --cudaID 0 --output_dir ./results/ --summary_dir ./results/log/ ^
--mode inference --input_dir_LR ./LR/calendar --output_pre calendar  ^
--num_resblock 16  --checkpoint ./model/TecoGAN --output_ext png

cd %current_dir%