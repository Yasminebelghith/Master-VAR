import cv2
import numpy as np
from tensorflow.python.keras.models import load_model


def loadimage(filename):
    im = cv2.imread(filename,cv2.IMREAD_GRAYSCALE)
    im = im/255
    im = cv2.resize(im,(28,28))
    im = np.reshape(im,im.shape+(1,))
    im = np.reshape(im, (1,) + im.shape)
    return im

model = load_model('Fashion_MNIST_model.h5')
image = loadimage('sample_image.png')
print(np.argmax(model.predict(image), axis=-1))