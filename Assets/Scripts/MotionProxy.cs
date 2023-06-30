using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Mediapipe.Unity.PoseTracking
{
    public class MotionProxy
    {
        private static MotionProxy _Instance;
        private Vector3 oneone_position;
        private Vector3 onethree_position;
        private Vector3 onefive_position;
        private Vector3 zero_position;
        private Vector3 onetwo_position;
        private Vector3 median_position;
        private Vector3 tmp_Vector = Vector3.zero;
        private int frame = 0;
        private float tmp_decision_backward = 0;
        private float tmp_decision_forward = 0;
        private int pose_index = 0;  //default is head
        private List<Vector3> angle_list = new List<Vector3>();
        //private List<float> angle_list = new List<float>(new float[] {});
        public static MotionProxy GetInstance() 
        { 
            // If there is an instance, and it's not me, delete myself.
            
            if (_Instance == null) 
            { 
                _Instance = new MotionProxy();
            }
            return _Instance;
        }


        NormalizedLandmarkList poseLandmarks;

        // Start is called before the first frame update
        void Start()
        {
            // 



        }

        // Update is called once per frame
        void Update()
        {
            
        }

        public void SetPoseLandmark(NormalizedLandmarkList val)
        {
            if (val == null) return;
            poseLandmarks = val;


        }
        public void SetLeftHand()
        {
            pose_index = 15;
        }
        public void SetRightHand()
        {
            pose_index = 16;
        }
        public float GetHorizontalMove()
        {
            if (poseLandmarks == null) return 0.0f;



            zero_position = new Vector3(poseLandmarks.Landmark[pose_index].X, 0, poseLandmarks.Landmark[pose_index].Z);


            if (frame % 4 == 0)
            {

                Vector3 vec_diff = (zero_position - tmp_Vector);
                angle_list.Add(vec_diff);


                if (angle_list.Count > 4) angle_list.RemoveAt(0);

                if (angle_list.Count == 4)
                {
                    tmp_decision_forward = 1;
                    tmp_decision_backward = 1;

                    foreach (Vector3 angle in angle_list)
                    {


                        if (Mathf.Abs(angle[2]) < 0.01)
                        {
                            tmp_decision_forward *= 0;
                        }

                        else
                        {
                            tmp_decision_forward *= angle[2];
                        }

                        if (Mathf.Abs(angle[0]) < 0.005)
                        {

                            tmp_decision_backward *= 0;
                        }
                        else
                        {
                            tmp_decision_backward *= angle[0];
                        }

                    }

                }
                tmp_Vector = zero_position;

            }
            frame += 1;
            
            if (tmp_decision_forward > 0 || Mathf.Abs(tmp_decision_forward) > Mathf.Abs(tmp_decision_backward))
            {

                return +2.0f;
            }
            else if (Mathf.Abs(tmp_decision_backward) > 0 || Mathf.Abs(tmp_decision_forward) < Mathf.Abs(tmp_decision_backward)) 
            { 
                return -2.0f;
            } 

            else
            {
                return 0.0f;
            }

           
            return 0.0f;


        }


    }
}
