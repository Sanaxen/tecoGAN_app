# tecoGAN_app  

`..........original................. tecoGAN ..........`
![](./images/output.gif)  
[Roman Holiday (1953) Original Trailer](https://www.youtube.com/watch?v=X_hyQgdGmU8)  
* * * *
Windows application
<img src="./images/image00.png">
* * * *
[tecoGAN](https://github.com/thunil/TecoGAN)![](https://github.com/thunil/TecoGAN/raw/master/resources/tecoGAN-lizard.gif)
![](https://github.com/thunil/TecoGAN/raw/master/resources/tecoGAN-armour.gif)
![](https://github.com/thunil/TecoGAN/raw/master/resources/tecoGAN-spider.gif)  
  
**Windows10+Anaconda3**
## Requirements  
ImageMagick-7.0.10-Q16


```

_ _ _
## setup memo
It worked correctly with the installation of tensorflow 1.15 (confirmed on win10 and win7).
However, older versions of the torch will fail to install and cannot be trained.
**`pip install -r requirements.txt`**

## How to super-resolution video
Converts videos into serialized images.

`ffmpeg -i test.mp4 -vcodec png main/LR/calendar/image_%04d.png
`  
**super-resolution**  
`tecoGAN.bat`

output -> main/results/output_%03d.png
To return the output result to a movie, do as follows.  

`ffmpeg -r 24 -i output_%04d.png -vcodec libx264 -pix_fmt yuv420p -r 24 test.mp4`  

Change the codec and format accordingly. Since "24" in the command is the frame rate, enter the frame rate of the original movie.
