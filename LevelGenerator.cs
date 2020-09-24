using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelGenerator : MonoBehaviour
{

    public GameObject layoutRoom;
    public Color startColor, EndColor,shopColor;

    public int distanceToEnd;
    public bool includeShop;
    public int minDistanceToShop, maxDistranceToShop;

    public Transform generatorPoint;

    public enum Direction { up,right,down,left };
    public Direction selectedDirection;

    public float xOffset = 18f,yOffset = 10f;
    public LayerMask whatisROOM;

    private GameObject endRoom,shopRoom;
    private GameObject startRoom;

    private List<GameObject> layoutRoomObject = new List<GameObject>();

    public RoomPrefabs Room;

    private List<GameObject> generatedOutline = new List<GameObject>();

    public RoomCenterScripts centerStart, centerEnd,centershop;
    public RoomCenterScripts[] potentialCenters;

    // Start is called before the first frame update
    void Start()
    {
        GameObject newwRoom = Instantiate(layoutRoom, generatorPoint.position, generatorPoint.rotation);
        newwRoom.GetComponent<SpriteRenderer>().color = startColor;
        selectedDirection = (Direction)Random.Range(0, 4);
        MoveGenerationPoint();
        startRoom = newwRoom;


        for (int i = 0; i < distanceToEnd; i++)
        {
            selectedDirection = (Direction)Random.Range(0, 4);
            GameObject newRoom = Instantiate(layoutRoom, generatorPoint.position, generatorPoint.rotation);

            layoutRoomObject.Add(newRoom);

            if(i+1 == distanceToEnd)
            {
                newRoom.GetComponent<SpriteRenderer>().color = EndColor;
                layoutRoomObject.RemoveAt(layoutRoomObject.Count - 1);

                endRoom = newRoom;
            }
            MoveGenerationPoint();

            while(Physics2D.OverlapCircle(generatorPoint.position,.2f,whatisROOM))
            {
                MoveGenerationPoint();
            }

        }

        if (includeShop)
        {
            int shopSelector = Random.Range(minDistanceToShop, maxDistranceToShop + 1);
            shopRoom = layoutRoomObject[shopSelector];
            layoutRoomObject.RemoveAt(shopSelector);
            shopRoom.GetComponent<SpriteRenderer>().color = shopColor;
        }

        //creat roon outline
        CreateRoomoutline(Vector3.zero);
        foreach(GameObject room in layoutRoomObject)
        {
            CreateRoomoutline(room.transform.position);
        }


        CreateRoomoutline(endRoom.transform.position);


        if (includeShop)
        {
            CreateRoomoutline(shopRoom.transform.position);
        }

        foreach(GameObject outline in generatedOutline)
        {
            bool generaterCentor = true;

            if(outline.transform.position == startRoom.transform.position)
            {
                Instantiate(centerStart, outline.transform.position, outline.transform.rotation).theRoom = outline.GetComponent<Room>();
                generaterCentor = false;
            }
            if(outline.transform.position == endRoom.transform.position)
            {
                Instantiate(centerEnd, outline.transform.position, outline.transform.rotation).theRoom = outline.GetComponent<Room>();
                generaterCentor = false;
            }
            if (includeShop)
            {
                if (outline.transform.position == shopRoom.transform.position)
                {
                    Instantiate(centershop, outline.transform.position, outline.transform.rotation).theRoom = outline.GetComponent<Room>();
                    generaterCentor = false;
                }
            }
            if (generaterCentor)
            {
            int centerSelect = Random.Range(0, potentialCenters.Length);

            Instantiate(potentialCenters[centerSelect], outline.transform.position, outline.transform.rotation).theRoom = outline.GetComponent<Room>();
            }
            



        }
    }

    // Update is called once per frame
    void Update()
    {
#if UNITY_EDITOR

        if (Input.GetKey(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
#endif
    }

    public void MoveGenerationPoint()
    {
        switch (selectedDirection)
        {
            case Direction.up:
                generatorPoint.position += new Vector3(0f, yOffset, 0f);
                break;
            case Direction.down:
                generatorPoint.position += new Vector3(0f, -yOffset, 0f);
                break;
            case Direction.right:
                generatorPoint.position += new Vector3(xOffset, 0f, 0f);
                break;
            case Direction.left:
                generatorPoint.position += new Vector3(-xOffset, 0f, 0f);
                break;
        }
    }

    public void CreateRoomoutline(Vector3 RoomPosition)
    {
        bool RoomAbove = Physics2D.OverlapCircle(RoomPosition + new Vector3(0f, yOffset, 0f), .2f, whatisROOM);
        bool RoomBelow = Physics2D.OverlapCircle(RoomPosition + new Vector3(0f,- yOffset, 0f), .2f, whatisROOM);
        bool RoomLeft = Physics2D.OverlapCircle(RoomPosition + new Vector3(-xOffset, 0f, 0f), .2f, whatisROOM);
        bool RoomRight = Physics2D.OverlapCircle(RoomPosition + new Vector3(xOffset, 0f, 0f), .2f, whatisROOM);

        int directionCount = 0;
        if (RoomAbove)
        {
            directionCount++;
        }
        if (RoomBelow)
        {
            directionCount++;
        }
        if (RoomLeft)
        {
            directionCount++;
        }
        if (RoomRight)
        {
            directionCount++;
        }

        switch (directionCount)
        {
            case 0:
                Debug.LogError("Found no Room Error!!!");
                break;
            case 1:
                if (RoomAbove)
                {
                   generatedOutline.Add( Instantiate(Room.singleup, RoomPosition, transform.rotation));
                }
                if (RoomBelow)
                {
                    generatedOutline.Add(Instantiate(Room.singleDown, RoomPosition, transform.rotation));
                }
                if (RoomLeft)
                {
                    generatedOutline.Add(Instantiate(Room.singleLeft, RoomPosition, transform.rotation));
                }
                if (RoomRight)
                {
                    generatedOutline.Add(Instantiate(Room.singleRight, RoomPosition, transform.rotation));
                }


                break;
            case 2:
                if (RoomAbove && RoomBelow)
                {
                    generatedOutline.Add(Instantiate(Room.doubleUpDown, RoomPosition, transform.rotation));
                }
                if (RoomLeft && RoomRight)
                {
                    generatedOutline.Add(Instantiate(Room.doubleLeftRight, RoomPosition, transform.rotation));
                }
                if (RoomAbove && RoomRight)
                {
                    generatedOutline.Add(Instantiate(Room.doubleUpRight, RoomPosition, transform.rotation));
                }
                if (RoomRight && RoomBelow)
                {
                    generatedOutline.Add(Instantiate(Room.doubleRightDown, RoomPosition, transform.rotation));
                }
                if (RoomBelow && RoomLeft)
                {
                    generatedOutline.Add(Instantiate(Room.doubleDownLeft, RoomPosition, transform.rotation));
                }
                if (RoomAbove && RoomLeft)
                {
                    generatedOutline.Add(Instantiate(Room.doubleLeftUp, RoomPosition, transform.rotation));
                }


                break;
            case 3:
                if (RoomAbove && RoomRight && RoomBelow)
                {
                    generatedOutline.Add(Instantiate(Room.tripleUpRightDown, RoomPosition, transform.rotation));
                }
                if (RoomLeft && RoomBelow && RoomRight)
                {
                    generatedOutline.Add(Instantiate(Room.tripleRightDownLeft, RoomPosition, transform.rotation));
                }
                if (RoomAbove && RoomBelow && RoomLeft)
                {
                    generatedOutline.Add(Instantiate(Room.tripleDownLeftUp, RoomPosition, transform.rotation));
                }
                if (RoomAbove && RoomLeft && RoomRight)
                {
                    generatedOutline.Add(Instantiate(Room.tripleLeftUpRight, RoomPosition, transform.rotation));
                }


                break;
            case 4:
                if (RoomAbove && RoomBelow && RoomLeft && RoomRight)
                {
                    generatedOutline.Add(Instantiate(Room.fourway, RoomPosition, transform.rotation));
                }


                break;
        }
    }
}

[System.Serializable]
public class RoomPrefabs
{
    public GameObject singleup, singleDown ,singleRight , singleLeft,
        doubleUpDown,doubleLeftRight,doubleUpRight,doubleRightDown,doubleDownLeft,doubleLeftUp,
        tripleUpRightDown, tripleRightDownLeft, tripleDownLeftUp, tripleLeftUpRight,
        fourway;
}
