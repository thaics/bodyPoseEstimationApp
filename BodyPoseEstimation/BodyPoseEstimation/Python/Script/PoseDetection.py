import cv2
import mediapipe
import sys

class PoseDetection:
    def __init__(self):
        self.mp_drawing = mediapipe.solutions.drawing_utils
        self.mp_pose = mediapipe.solutions.pose
        self.mp_holistic = mediapipe.solutions.holistic

        self.joint_names = ['nose'
            , 'left_eye_inner'
            , 'left_eye'
            , 'left_eye_outer'
            , 'right_eye_inner'
            , 'right_eye'
            , 'right_eye_outer'
            , 'left_ear'
            , 'right_ear'
            , 'mouth_left'
            , 'mouth_right'
            , 'left_shoulder'
            , 'right_shoulder'
            , 'left_elbow'
            , 'right_elbow'
            , 'left_wrist'
            , 'right_wrist'
            , 'left_pinky'
            , 'right_pinky'
            , 'left_index'
            , 'right_index'
            , 'left_thumb'
            , 'right_thumb'
            , 'left_hip'
            , 'right_hip'
            , 'left_knee'
            , 'right_knee'
            , 'left_ankle'
            , 'right_ankle'
            , 'left_heel'
            , 'right_heel'
            , 'left_foot_index'
            , 'right_foot_index']

    def coordinate_image(self, input_path, output_path):
        with self.mp_pose.Pose(
                static_image_mode=True,
                model_complexity=2,
                min_detection_confidence=0.5) as pose:
            image = cv2.imread(input_path)  # Insert your Image Here
            image_height, image_width, _ = image.shape
            # Convert the BGR image to RGB before processing.
            results = pose.process(cv2.cvtColor(image, cv2.COLOR_BGR2RGB))

            n = 0
            if results.pose_landmarks:
                for index, landmarks in enumerate(results.pose_landmarks.landmark):
                    if (landmarks.x < 1) & (landmarks.y < 1):
                        n += 1
                        print(self.joint_names[index], landmarks.x * image_width, landmarks.y * image_height)
                print('The total number of nodes is:'+str(n))
                sys.stdout.flush()


            # Draw pose landmarks on the image.
            annotated_image = image.copy()
            print(self.mp_pose.POSE_CONNECTIONS)
            self.mp_drawing.draw_landmarks(annotated_image, results.pose_landmarks, self.mp_pose.POSE_CONNECTIONS)
            cv2.imwrite(output_path, annotated_image)


if __name__ == "__main__":
    pd = PoseDetection()
    input_path = '2.jpg'
    output_path = '2.png'
    pd.coordinate_image(input_path, output_path)
