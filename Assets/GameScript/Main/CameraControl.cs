using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.Events;

public class CameraControl : MonoBehaviour
{
    //public Slider XX, YY, ZZ, XXX, YYY, ZZZ;
    float up_edge;
    float down_edge;
    float right_edge;
    float left_edge;

    float up_down_edge_plus;
    float right_left_edge_plus;

    public GameObject ui_camera;


    public Vector2 last_position = new Vector2(0, 0);
    public Vector2 now_position = new Vector2(0, 0);

    float move_x = 0;
    float move_y = 0;

   public float x_move_speed = 0.2f;
    public float y_move_speed = 0.2f;


    float rotate_x = 0;
    float rotate_y = 0;

    public float x_rotate_speed = 2f;
    public float y_rotate_speed = 2f;

    public GameObject MoveCenter;
    public GameObject RotateLCenter;
    public GameObject RotateHCenter;
    public GameObject View;

    public GameObject MapCenter;

    public UnityEvent CameraToBattleField = new UnityEvent();
    public UnityEvent CameraToStoryMap = new UnityEvent();

    // public Camera Camera;

    // public GameObject FLOOR;

    // Start is called before the first frame update

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    void Start()
    {
        Input.simulateMouseWithTouches = true;
        CameraToBattleField.AddListener(ToBattleField);
        CameraToStoryMap.AddListener(ToStoryMap);

    }

    // Update is called once per frame
    void Update()
    {

        if (MoveCenter.transform.localPosition.x >= 10f)
        {
            MoveCenter.transform.localPosition = new Vector3(10f, MoveCenter.transform.position.y, MoveCenter.transform.position.z);
        }
        if (MoveCenter.transform.localPosition.x <= -10f)
        {
            MoveCenter.transform.localPosition = new Vector3(-10f, MoveCenter.transform.position.y, MoveCenter.transform.position.z);
        }
        if (MoveCenter.transform.localPosition.z >= 10f)
        {
            MoveCenter.transform.localPosition = new Vector3(MoveCenter.transform.position.x, MoveCenter.transform.position.y, 10f);
        }
        if (MoveCenter.transform.localPosition.z <= -10f)
        {
            MoveCenter.transform.localPosition = new Vector3(MoveCenter.transform.position.x, MoveCenter.transform.position.y, -10f);
        }




        float angle = RotateHCenter.transform.localEulerAngles.x;
        //  Debug.Log(angle.ToString());

        if (angle < 300 && angle > 270)
        {
            // Debug.Log("1");

            RotateHCenter.transform.localRotation = Quaternion.Euler(300, 0, 0);
        }

        if (angle > 0 && angle < 30)
        {
            // Debug.Log("2");

            RotateHCenter.transform.localRotation = Quaternion.Euler(0, 0, 0);
        }






        if (Input.GetKeyDown("a"))
            {
                RotateHCenter.transform.RotateAround(new Vector3(0, 0, 0), Vector3.up, -1f);

            }
            if (Input.GetKeyDown("d"))
            {
                RotateHCenter.transform.RotateAround(new Vector3(0, 0, 0), Vector3.up, 1f);

            }
            if (Input.GetKeyDown("w"))
            {
                RotateHCenter.transform.Rotate(1, 0, 0);

            }
            if (Input.GetKeyDown("s"))
            {
                RotateHCenter.transform.Rotate(-1, 0, 0);


            }
            if (View.transform.localPosition.z < 10f)
            {
                // Debug.Log(LitiCamera.transform.localPosition.z);

                View.transform.localPosition = new Vector3(0, 10f,0);
            }
            if (View.transform.localPosition.z > 20f)
            {
                // Debug.Log(LitiCamera.transform.localPosition.z);

                View.transform.localPosition = new Vector3(0, 20f, 0);
            }


            /*

                        if (Input.touchCount <= 0)
                        {
                            return;
                        }

                        if ((Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Moved))
                        {
                            if (!EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId))
                            {



                                if (Input.GetTouch(0).position.x >= 1000 )
                                {
                                    var deltaposition = Input.GetTouch(0).deltaPosition;
                                    RotateCenter.transform.Rotate(-deltaposition.y * 0.1f, 0, 0);
                                    MoveCenter.transform.RotateAround(MoveCenter.transform.position, Vector3.up, deltaposition.x * 0.2f);
                                }

                                if (Input.GetTouch(0).position.x < 900 )
                                {

                                    var deltaposition = Input.GetTouch(0).deltaPosition;

                                    float beiLv = 0.3f * View.transform.localPosition.z / 100f;

                                    var MoveVector = new Vector3(deltaposition.x * beiLv, 0f, deltaposition.y * beiLv);

                                    MoveCenter.transform.Translate(MoveVector, Space.Self);

                                }


                            }


                        }
                        */
            /*触屏代码

            if ((Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Moved))
            {
                if (!EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId))
                {



                    if (Input.GetTouch(0).position.x >= 1000 && IsGyro == false)
                    {
                        var deltaposition = Input.GetTouch(0).deltaPosition;
                        RotateCenter.transform.Rotate(-deltaposition.y * 0.1f, 0, 0);
                        MoveCenter.transform.RotateAround(MoveCenter.transform.position, Vector3.up, deltaposition.x * 0.2f);
                    }

                    if (Input.GetTouch(0).position.x < 900 && IsGyro == false)
                    {




                        var deltaposition = Input.GetTouch(0).deltaPosition;

                        float beiLv = 0.3f * LITI_camera_moveview.transform.localPosition.z / 100f;

                        var MoveVector = new Vector3(deltaposition.x * beiLv, 0f, deltaposition.y * beiLv);

                        MoveCenter.transform.Translate(MoveVector, Space.Self);



                    }


                }
            }




            Touch newTouch1 = Input.GetTouch(0);
            Touch newTouch2 = newTouch1;
            if (Input.touchCount == 2)
            {
                newTouch2 = Input.GetTouch(1);


                if (newTouch2.phase == TouchPhase.Began)
                {
                    oldTouch2 = newTouch2;
                    oldTouch1 = newTouch1;
                    return;

                }

                float oldDistance = Vector2.Distance(oldTouch1.position, oldTouch2.position);
                float newDistance = Vector2.Distance(newTouch1.position, newTouch2.position);

                float offset = newDistance - oldDistance;

                float scaleFactor = offset / 20f;


                if (!EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId))
                {
                    //   Vector3 newPosition = LitiCamera.transform.localPosition;

                    //   newPosition -=new Vector3 (0,0,scaleFactor);

                    LITI_camera_moveview.transform.Translate(0, 0, scaleFactor, Space.Self);

                    //   var deltaposition = Input.GetTouch(0).deltaPosition;

                    //  var ScreenMoveVector = new Vector3(deltaposition.x, deltaposition.y, 0);

                    //   var WorldMoveVector = Camera.main.ScreenToWorldPoint(ScreenMoveVector);

                    //   Vector3 MoveVector = new Vector3(WorldMoveVector.x, WorldMoveVector.y, 0f);
                    //
                    //    PingYiCenter.transform.Translate(MoveVector, Space.World);

                }

                oldTouch1 = newTouch1;
                oldTouch2 = newTouch2;

            }


            */

        if (Input.GetMouseButton(0))
        {
            //  Debug.Log("鼠标按下");

            move_x = Input.GetAxis("Mouse X") * x_move_speed;
            move_y = Input.GetAxis("Mouse Y") * y_move_speed;

            //Debug.Log(x.ToString()+"     "+y.ToString());

            //  MoveCenter.transform.Translate(-move_x, 0, -move_y);
            var MoveVector = new Vector3(-move_x, 0f, -move_y);

            MoveCenter.transform.Translate(MoveVector, Space.Self);

        }

        if (Input.GetMouseButton(1))
        {
            //  Debug.Log("鼠标按下");

            rotate_x = Input.GetAxis("Mouse X") * x_rotate_speed;
            rotate_y = Input.GetAxis("Mouse Y") * y_rotate_speed;

            //Debug.Log(x.ToString()+"     "+y.ToString());

            MoveCenter.transform.Rotate(0, rotate_x, 0);
            RotateHCenter.transform.Rotate(rotate_y, 0, 0);


          

        }

        

    }

    void LateUpdate()
    {
        //  last_position = new Vector2(ui_camera.GetComponent<Transform>().position.x, ui_camera.GetComponent<Transform>().position.z);





    }

    void ToBattleField()
    {
        MapCenter.GetComponent<Animation>().clip = MapCenter.GetComponent<Animation>().GetClip("CameraAnimationToBattle");
        MapCenter.GetComponent<Animation>().Play();
    }


    void ToStoryMap()
    {
        MapCenter.GetComponent<Animation>().clip = MapCenter.GetComponent<Animation>().GetClip("CameraAnimationToStoryMap");
        MapCenter.GetComponent<Animation>().Play();
    }
}
