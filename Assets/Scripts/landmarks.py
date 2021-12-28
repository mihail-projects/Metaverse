import cv2
import mediapipe
import csv
import os
import pickle

mp_drawing = mediapipe.solutions.drawing_utils
mp_drawing_styles = mediapipe.solutions.drawing_styles
mp_hands = mediapipe.solutions.hands

fields = ['x0', 'y0', 'z0', 'x1', 'y1', 'z1', 'x2', 'y2', 'z2', 'x3', 'y3', 'z3', 'x4', 'y4', 'z4',
          'x5', 'y5', 'z5', 'x6', 'y6', 'z6', 'x7', 'y7', 'z7', 'x8', 'y8', 'z8', 'x9', 'y9', 'z9',
          'x10', 'y10', 'z10', 'x11', 'y11', 'z11', 'x12', 'y12', 'z12', 'x13', 'y13', 'z13', 'x14',
          'y14', 'z14', 'x15', 'y15', 'z15', 'x16', 'y16', 'z16', 'x17', 'y17', 'z17', 'x18', 'y18',
          'z18', 'x19', 'y19', 'z19', 'x20', 'y20', 'z20']

if os.path.exists("model.pkl"):
    with open("model.pkl", "rb") as model_file:
        model = pickle.load(model_file)

# For webcam input:

if cv2.VideoCapture(0) is None:
    cap = cv2.VideoCapture(1, cv2.CAP_DSHOW)
else:
    cap = cv2.VideoCapture(0, cv2.CAP_DSHOW)

cap.set(cv2.CAP_PROP_FRAME_WIDTH, 1280)
cap.set(cv2.CAP_PROP_FRAME_HEIGHT, 720)


with mp_hands.Hands(model_complexity=0, min_detection_confidence=0.5, min_tracking_confidence=0.5) as hands:

    while cap.isOpened():

        fps = cap.get(cv2.CAP_PROP_FPS)
        success, image = cap.read()

        if not success:
            print("Ignoring empty camera frame.")
            # If loading a video, use 'break' instead of 'continue'.
            break

        # To improve performance, optionally mark the image as not writeable to pass by reference.
        image.flags.writeable = False
        image = cv2.cvtColor(image, cv2.COLOR_BGR2RGB)
        results = hands.process(image)

        # Draw the hand annotations on the image.
        image.flags.writeable = True
        image = cv2.cvtColor(image, cv2.COLOR_RGB2BGR)

        if results.multi_hand_landmarks:

            if len(results.multi_hand_landmarks) >= 1:

                mp_drawing.draw_landmarks(image, results.multi_hand_landmarks[0], mp_hands.HAND_CONNECTIONS, mp_drawing_styles.get_default_hand_landmarks_style(), mp_drawing_styles.get_default_hand_connections_style())

                if os.path.exists('C:/Metaverse/empty0.csv'):

                    list = []

                    for j in range(21):
                        list.append(f'{results.multi_hand_landmarks[0].landmark[j].x:.7f}')
                        list.append(f'{results.multi_hand_landmarks[0].landmark[j].y:.7f}')
                        list.append(f'{results.multi_hand_landmarks[0].landmark[j].z:.7f}')

                    f = open('C:/Metaverse/empty0.csv', "a", newline="")
                    writer = csv.writer(f)
                    writer.writerow(list)
                    f.close()
                    os.rename('C:/Metaverse/empty0.csv', 'C:/Metaverse/full0.csv')
            
            if len(results.multi_hand_landmarks) == 2:

                mp_drawing.draw_landmarks(image, results.multi_hand_landmarks[1], mp_hands.HAND_CONNECTIONS, mp_drawing_styles.get_default_hand_landmarks_style(), mp_drawing_styles.get_default_hand_connections_style())

                if os.path.exists('C:/Metaverse/empty1.csv'):

                    list = []

                    for j in range(21):
                        list.append(f'{results.multi_hand_landmarks[1].landmark[j].x:.7f}')
                        list.append(f'{results.multi_hand_landmarks[1].landmark[j].y:.7f}')
                        list.append(f'{results.multi_hand_landmarks[1].landmark[j].z:.7f}')

                    f = open('C:/Metaverse/empty1.csv', "a", newline="")
                    writer = csv.writer(f)
                    writer.writerow(list)
                    f.close()
                    os.rename('C:/Metaverse/empty1.csv', 'C:/Metaverse/full1.csv')

        # Flip the image horizontally for a selfie-view display.
        cv2.imshow('Detector', cv2.flip(image, 1))

        if cv2.waitKey(5) & 0xFF == 27:
            break

cap.release()
