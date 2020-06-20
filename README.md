# tecoGAN_app  

Windows application
<img src="./images/image00.png">
* * * *
[tecoGAN](https://github.com/thunil/TecoGAN)![](https://github.com/thunil/TecoGAN/raw/master/resources/tecoGAN-lizard.gif)
![](https://github.com/thunil/TecoGAN/raw/master/resources/tecoGAN-armour.gif)
![](https://github.com/thunil/TecoGAN/raw/master/resources/tecoGAN-spider.gif)  
#Make it a Windows application with pyinstaller
**Windows10+Anaconda3**


```
pip install --ignore-installed --upgrade tensorflow-gpu==1.15.0.


pip install numpy>=1.14.3
pip install scipy>=1.0.1
pip install scikit-image>=0.13.0
pip install matplotlib>=1.5.1
pip install pandas>=0.23.1
pip install Keras>=2.1.2
pip install opencv-python>=2.4.11
pip install ipython>=7.4.0

pip install torch==1.5.0+cu92 torchvision==0.6.0+cu92 -f https://download.pytorch.org/whl/torch_stable.html


pip install pyinstaller


pyinstaller main.py
```
pyinstaller has terminated normally.

However, it gives an error at run time.It worked by doing the following

copy directory  -> TecoGAN-master/dist
```
TecoGAN-master/model
TecoGAN-master/LR
TecoGAN-master/lib
```

copy directory  -> TecoGAN-master/dist

```
[Users]\bccat\Anaconda3\envs\tecoGAN\Lib\site-packages/google
[Users]\bccat\Anaconda3\envs\tecoGAN\Lib\site-packages/absl
[Users]\bccat\Anaconda3\envs\tecoGAN\Lib\site-packages/wrapt
[Users]\bccat\Anaconda3\envs\tecoGAN\Lib\site-packages/gast
[Users]\bccat\Anaconda3\envs\tecoGAN\Lib\site-packages/astor

[Users]\bccat\Anaconda3\envs\tecoGAN\Lib\site-packages/keras_applications
[Users]\bccat\Anaconda3\envs\tecoGAN\Lib\site-packages/keras_preprocessing
[Users]\bccat\Anaconda3\envs\tecoGAN\Lib\site-packages/tensorflow_estimator
```

copy file  -> TecoGAN-master/dist
```
termcolor.py
```
It became a Windows application.