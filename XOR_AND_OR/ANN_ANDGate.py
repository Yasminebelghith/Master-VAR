import numpy as np

#############################################################################################################################################################################################################
#############################################################################################################################################################################################################
from tensorflow.python.keras.layers import Dense
from tensorflow.python.keras.models import Sequential


def sigmoid(n):

    # Define the sigmoid function as activation function
    return 1.0/(1.0 + np.exp(-n))

#############################################################################################################################################################################################################
#############################################################################################################################################################################################################


#############################################################################################################################################################################################################
#############################################################################################################################################################################################################
def sigmoidDerivative(n):

    return n*(1-n)
#############################################################################################################################################################################################################
#############################################################################################################################################################################################################


def forwardPropagationLayer(p, weights, biases):

    a = None  # the layer output

    # Multiply weights with the input vector (p) and add the bias   =>  n
    n = np.dot(p, weights) + biases

    # Pass the result to the activation function  =>  a
    a = sigmoid(n)

    return a
#############################################################################################################################################################################################################
#############################################################################################################################################################################################################



#############################################################################################################################################################################################################
#############################################################################################################################################################################################################
def main():

    #The network should receive as input two values (0 or 1) and should predict the target output

    #Input data
    points = np.array([
            [0, 0],
            [0, 1],
            [1, 0],
            [1, 1]
        ])

    #Labels
    labels = np.array([[0], [0], [0], [1]])

    # Initialize the weights and biases with random values
    inputSize = 2
    noNeuronsLayer1 = 2
    noNeuronsLayer2 = 1

    weightsLayer1 = np.random.uniform(size=(inputSize, noNeuronsLayer1))
    weightsLayer2 = np.random.uniform(size=(noNeuronsLayer1, noNeuronsLayer2))

    biasLayer1 = np.random.uniform(size=(1, noNeuronsLayer1))
    biasLayer2 = np.random.uniform(size=(1, noNeuronsLayer2))


    noEpochs = 5
    learningRate = 0.3


    # Train the network for noEpochs
    for _ in range(noEpochs):

        # Forward Propagation
        hidden_layer_output = forwardPropagationLayer(points, weightsLayer1, biasLayer1)
        predicted_output = forwardPropagationLayer(hidden_layer_output, weightsLayer2, biasLayer2)

        # Backpropagation
        bkProp_error = labels - predicted_output
        d_predicted_output = bkProp_error * sigmoidDerivative(predicted_output)

        error_hidden_layer = d_predicted_output.dot(weightsLayer2.T)
        d_hidden_layer = error_hidden_layer * sigmoidDerivative(hidden_layer_output)


        # Updating Weights and Biases
        weightsLayer2 = weightsLayer2 + hidden_layer_output.T.dot(d_predicted_output) * learningRate
        biasLayer2 = biasLayer2 + np.sum(d_predicted_output, axis=0, keepdims=True) * learningRate

        weightsLayer1 = weightsLayer1 + points.T.dot(d_hidden_layer) * learningRate
        biasLayer1 = biasLayer1 + np.sum(d_hidden_layer, axis=0, keepdims=True) * learningRate


    # Print weights and bias
    print("weightsLayer1 = {}".format(weightsLayer1))
    print("biasesLayer1 = {}".format(biasLayer1))

    print("weightsLayer2 = {}".format(weightsLayer2))
    print("biasLayer2 = {}".format(biasLayer2))

    # Display the results
    c = 0
    for i in range(len(labels)):
        outL1 = forwardPropagationLayer(points[i], weightsLayer1, biasLayer1)
        outL2 = forwardPropagationLayer(outL1, weightsLayer2, biasLayer2)
        print("Input = {} - Predict = {} - Label = {}  ".format(points[i], outL2, labels[i]))
        c = c + ((labels[i]-outL2)**2)
    error = (1/8)*c
    print("error = " + str(error) )

    # implementing XOR

    # training data
    training_data = np.array([[0, 0], [0, 1], [1, 0], [1, 1]], "float32")

    # Outputs expected for XOR
    target_data = np.array([[0], [1], [1], [0]], "float32")

    #model
    model = Sequential()
    model.add(Dense(4, input_dim=2, activation='sigmoid'))
    model.add(Dense(1, activation='sigmoid'))

    model.compile(loss='mean_squared_error', optimizer='adam', metrics=['binary_accuracy'])

    model.fit(training_data, target_data, epochs=400, verbose=1)

    print(model.predict(training_data).round())
#############################################################################################################################################################################################################
#############################################################################################################################################################################################################



#############################################################################################################################################################################################################
#############################################################################################################################################################################################################
if __name__ == "__main__":
    main()
#############################################################################################################################################################################################################
#############################################################################################################################################################################################################
