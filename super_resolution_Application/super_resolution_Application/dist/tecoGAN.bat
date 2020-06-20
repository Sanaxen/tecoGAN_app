cd main
main.exe --cudaID 0 --output_dir ./results/ --summary_dir ./results/log/ ^
--mode inference --input_dir_LR ./LR/calendar --output_pre calendar ^
--num_resblock 16  --checkpoint ./model/TecoGAN --output_ext png

cd ..

:python main.py --cudaID 0 --output_dir ./results/ --summary_dir ./results/log/ --mode inference --input_dir_LR ./LR/calendar --output_pre calendar  --num_resblock 16  --checkpoint ./model/TecoGAN --output_ext png
