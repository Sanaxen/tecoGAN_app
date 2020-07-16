call envset.bat

cd tecoGAN
python main.py --output_dir ./results/  --summary_dir ./results/log/ ^
 --mode inference --input_dir_LR ./LR/calendar --output_pre calendar ^
 --num_resblock 16 --checkpoint ./model/TecoGAN --output_ext png
 
:pause
