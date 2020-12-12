from keras.datasets import mnist
from keras.models import Sequential
import numpy as np
from keras.utils import np_utils
from tensorflow.python.keras.layers import Dense

def LoadAndPredict(testX, testY):

    num_pixels = testX.shape[1] * testX.shape[2]
    testX = testX.reshape((testX.shape[0], num_pixels)).astype('float32')

    testX = testX / 255

    testY = np_utils.to_categorical(testY)
    num_classes = testY.shape[1]

    model = Sequential()

    model.add(Dense(8, input_dim=784, kernel_initializer='normal',activation='relu'))
    model.add(Dense(10, kernel_initializer='normal',activation='softmax'))

    model.load_weights('App1_weights.h5')

    model.compile(loss='categorical_crossentropy', optimizer='adam',metrics=['accuracy'])

    predictions = model.predict(testX)

    loop = 0

    for i in predictions:
        print("Predicted class is : " + str(np.argmax(i)))
        print("Actual class is : " + str(np.argmax(testY[loop])))
        loop = loop + 1

(train_images,train_labels),(test_images,test_labels) = mnist.load_data()

testX = test_images[:5]
testY = test_labels[:5]
LoadAndPredict(testX,testY)