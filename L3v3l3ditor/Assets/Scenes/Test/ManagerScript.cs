﻿using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using TbsFramework.Gui;
using TbsFramework.Cells;
using TbsFramework.Grid;
using TbsFramework.Players;
using TbsFramework.Units;
using TbsFramework.Grid.UnitGenerators;
using TbsFramework.Test.Scripts;

public class ManagerScript : MonoBehaviour
{
    public GameObject[] itemsToPickFrom;


    // Hide these variables from Unity editor.
    [HideInInspector]
    public bool playerPlaced = false;
    [HideInInspector]
    public bool saveLoadMenuOpen = false;

    public Animator itemUIAnimation;
    public Animator optionUIAnimation;
    public Animator saveUIAnimation;
    public Animator loadUIAnimation;
    public MeshFilter mouseObject;
    public MouseScript user;
    //public Mesh playerMarker;
    public Slider rotSlider;
    public GameObject rotUI;
    public InputField levelNameSave;
    public InputField levelNameLoad;
    //public Text levelMessage;
    //public Animator messageAnim;

    private bool itemPositionIn = true;
    private bool optionPositionIn = true;
    private bool saveLoadPositionIn = false;
    private LevelEditor level;

    public static int whichObj = 0;
    GameObject guiController;
    GameObject manager;
    GameObject cellGrid;
    GameObject players;

   

    GameObject units;
    


    // Start is called before the first frame update
    void Start()
    {
        //rotSlider.onValueChanged.AddListener(delegate { RotationValueChange(); }); // set up listener for rotation slider value change
        CreateEditor(); // create new instance of level.
    }


    public void ActivateGame()
    {
        cellGrid = GameObject.Find("CellGrid");
        players = GameObject.Find("Players");
        units = GameObject.Find("Units");

        players.GetComponent<CellGrid>().Begin();
        cellGrid.GetComponent<CellGrid>().Begin();
        units.GetComponent<CellGrid>().Begin();
    }

    LevelEditor CreateEditor()
    {
        level = new LevelEditor();
        level.editorObjects = new List<EditorObject.Data>(); // make new list of editor object data.
        return level;
    }

    //Rotating an object and saving the info
    void RotationValueChange()
    {
        user.rotObject.transform.localEulerAngles = new Vector3(0, rotSlider.value, 0); // rotate the object.
        user.rotObject.GetComponent<EditorObject>().data.rot = user.rotObject.transform.rotation; // save rotation info to object's editor object data.
    }

    /// <summary>
    /// Selecting certain menus
    /// </summary>
    public void SlideItemMenu()
    {
        if (itemPositionIn == false)
        {
            itemUIAnimation.SetTrigger("ItemMenuIn"); // slide menu into screen
            itemPositionIn = true; // indicate menu in screen view.
        }
        else
        {
            itemUIAnimation.SetTrigger("ItemMenuOut"); // slide menu out of screen
            itemPositionIn = false; // indicate menu off screen
        }
    }

    public void SlideOptionMenu()
    {
        if (optionPositionIn == false)
        {
            optionUIAnimation.SetTrigger("OptionMenuIn"); // slide menu into screen
            optionPositionIn = true; // indicate menu in screen view.
        }
        else
        {
            optionUIAnimation.SetTrigger("OptionMenuOut"); // slide menu out of screen
            optionPositionIn = false; // indicate menu off screen
        }
    }

    public void ChooseSave()
    {
        if (saveLoadPositionIn == false)
        {
            saveUIAnimation.SetTrigger("SaveLoadIn"); // slide menu into screen
            saveLoadPositionIn = true; // indicate menu on screen
            saveLoadMenuOpen = true; // indicate save menu open to prevent camera movement
        }
        else
        {
            saveUIAnimation.SetTrigger("SaveLoadOut"); // slide menu off screen
            saveLoadPositionIn = false; // indicate menu off screen
            saveLoadMenuOpen = false; // indicate save menu off screen, allow camera movement
        }
    }

    public void ChooseLoad()
    {
        if (saveLoadPositionIn == false)
        {
            loadUIAnimation.SetTrigger("SaveLoadIn"); // slide menu into screen
            saveLoadPositionIn = true; // indicate menu on screen
            saveLoadMenuOpen = true; // indicate load menu open, prevent camera movement.
        }
        else
        {
            loadUIAnimation.SetTrigger("SaveLoadOut"); // slide menu off screen
            saveLoadPositionIn = false; // indicate menu off screen
            saveLoadMenuOpen = false; // indicate load menu off screen, allow camera movement.
        }
    }


    /// <summary>
    /// Choosing an object
    /// </summary>
    /// 

    public void ChooseP1_Unit()
    {
        Debug.Log("P1Unit Selected");
        user.itemOption = MouseScript.ItemList.Unit;
        //int index = 0;
        //GameObject unit = Instantiate(itemsToPickFrom[index]);
        whichObj = 0;
    }

    public void ChooseP2_Unit()
    {
        Debug.Log("P2Unit Selected");
        user.itemOption = MouseScript.ItemList.Unit;
        //int index = 1;
        //GameObject unit = Instantiate(itemsToPickFrom[index]);
        whichObj = 1;
    }

    public void ChooseObstacles()
    {
        Debug.Log("Obstacle Selected");
        user.itemOption = MouseScript.ItemList.Unit;
        //int index = 2;
        //GameObject unit = Instantiate(itemsToPickFrom[index]);
        whichObj = 2;
    }

    //public void ChooseCylinder()
    //{
    //  user.itemOption = MouseScript.ItemList.Cylinder; // set object to place as cylinder
    //GameObject cylinder = GameObject.CreatePrimitive(PrimitiveType.Cylinder); // create object, get new object's mesh and set mouse object's mesh to that, then destroy
    //mouseObject.mesh = cylinder.GetComponent<MeshFilter>().mesh;
    //Destroy(cylinder);
    //}

    //public void ChooseCube()
    //{
    //  user.itemOption = MouseScript.ItemList.Cube; // set object to place as cube
    // GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube); // create object, get new object's mesh and set mouse object's mesh to that, then destroy
    // mouseObject.mesh = cube.GetComponent<MeshFilter>().mesh;
    // Destroy(cube);
    //}

    //public void ChooseSphere()
    //{
    //  user.itemOption = MouseScript.ItemList.Sphere; // set object to place as sphere
    // GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere); // create object, get new object's mesh and set mouse object's mesh to that, then destroy
    // mouseObject.mesh = sphere.GetComponent<MeshFilter>().mesh;
    // Destroy(sphere);
    //}

    //public void ChoosePlayerStart()
    //{
    //  user.itemOption = MouseScript.ItemList.Player; // set object to place as player marker
    // mouseObject.mesh = playerMarker; // set mouse object's mesh to playerMarker (player object mesh).
    //}


    /// <summary>
    /// Choosing an option for level manipulation
    /// </summary>
    public void ChooseCreate()
    {
        user.manipulateOption = MouseScript.LevelManipulation.Create; // set mode to create
        user.mr.enabled = true; // show mouse object mesh
        //rotUI.SetActive(false); // disable rotation ui
    }

    public void ChooseRotate()
    {
        user.manipulateOption = MouseScript.LevelManipulation.Rotate; // set mode to rotate
        user.mr.enabled = false; // hide mouse mesh
        rotUI.SetActive(true); // enable rotation ui
    }

    public void ChooseDestroy()
    {
        user.manipulateOption = MouseScript.LevelManipulation.Destroy; // set mode to destroy
        user.mr.enabled = false; // hide mouse mesh
        //rotUI.SetActive(false); // disable rotation ui
    }



    // Saving a level
    public void SaveLevel()
    {


        // Gather all objects with EditorObject component
        EditorObject[] foundObjects = FindObjectsOfType<EditorObject>();
        foreach (EditorObject obj in foundObjects)
            level.editorObjects.Add(obj.data); // add these objects to the list of editor objects

        string json = JsonUtility.ToJson(level); // write the level data to json
        string folder = Application.dataPath + "/LevelData/"; // create a folder
        string levelFile = "";

        //set a default file name if no name given
        if (levelNameSave.text == "")
            levelFile = "new_level.json";
        else
            levelFile = levelNameSave.text + ".json";

        //Create new directory if LevelData directory does not yet exist.
        if (!Directory.Exists(folder))
            Directory.CreateDirectory(folder);

        string path = Path.Combine(folder, levelFile); // set filepath

        //Overwrite file with same name, if applicable
        if (File.Exists(path))
            File.Delete(path);

        // create and save file
        File.WriteAllText(path, json);

        //Remove save menu
        saveUIAnimation.SetTrigger("SaveLoadOut");
        saveLoadPositionIn = false;
        saveLoadMenuOpen = false;
        levelNameSave.text = ""; // clear input field
        levelNameSave.DeactivateInputField(); // remove focus from input field.

        //Display message
        //levelMessage.text = levelFile + " saved to LevelData folder.";
        //messageAnim.Play("MessageFade", 0, 0);
    }


    // Loading a level
    public void LoadLevel()
    {
        Debug.Log("Level loaded");

        string folder = Application.dataPath + "/LevelData/";
        string levelFile = "";

        //set a default file name if no name given
        if (levelNameLoad.text == "")
            levelFile = "new_level.json";
        else
            levelFile = levelNameLoad.text + ".json";

        string path = Path.Combine(folder, levelFile); // set filepath

        if (File.Exists(path)) // if the file could be found in LevelData
        {
            // The objects currently in the level will be deleted
            EditorObject[] foundObjects = FindObjectsOfType<EditorObject>();
            foreach (EditorObject obj in foundObjects)
                Destroy(obj.gameObject);

            playerPlaced = false; // since objects are being destroyed, go ahead and say player placed is false

            string json = File.ReadAllText(path); // provide text from json file
            level = JsonUtility.FromJson<LevelEditor>(json); // level information filled from json file
            CreateFromFile(); // create objects from level data.

            /*players = new GameObject("Players");
            units = new GameObject("Units");
            cellGrid = new GameObject("CellGrid");


            for (int i = 0; i < Dimensions.players; i++)
            {
                var player = new GameObject(string.Format("Player_{0}", players.transform.childCount));
                player.AddComponent<HumanPlayer>();
                player.GetComponent<Player>().PlayerNumber = players.transform.childCount;
                player.transform.parent = players.transform;
            }


            var cellGridScript = cellGrid.AddComponent<CellGrid>();

            cellGrid.GetComponent<CellGrid>().PlayersParent = players.transform;



            var unitGenerator = cellGrid.AddComponent<CustomUnitGenerator>();
            unitGenerator.UnitsParent = units.transform;
            unitGenerator.CellsParent = cellGrid.transform;

            var sampleCell = GameObject.Find("Unit1");

            var cellDimensions = sampleCell.GetComponent<Cell>().GetCellDimensions();

            var gridInfo = new GridInfo();
            //gridInfo.Cells = ret;
            gridInfo.Dimensions = new Vector3(cellDimensions.x * (Dimensions.rows - 1), cellDimensions.y, cellDimensions.z * (Dimensions.cols - 1));
            gridInfo.Center = gridInfo.Dimensions / 2;

            var camera = Camera.main;
            var cameraObject = GameObject.Find("Main Camera");

            camera = cameraObject.GetComponent<Camera>();
            camera.transform.position = new Vector3(gridInfo.Center.x, gridInfo.Center.y + (3.5f * Dimensions.rows), gridInfo.Center.z);


            var rotationVector = new Vector3(0f, 0f, 0f);

            camera.transform.Rotate(rotationVector);

            camera.transform.parent = null;
            camera.transform.SetAsFirstSibling();


            guiController = new GameObject("GUIController");

            guiController.SetActive(true);
            var guiControllerScript = guiController.AddComponent<GUIController>();
            guiControllerScript.CellGrid = cellGridScript;*/

        }
        else // if file could not be found.
        {
            loadUIAnimation.SetTrigger("SaveLoadOut"); // remove menu
            saveLoadPositionIn = false; // indicate menu not on screen
            saveLoadMenuOpen = false; // indicate camera can move.
            //levelMessage.text = levelFile + " could not be found!"; // send message
            //messageAnim.Play("MessageFade", 0, 0);
            levelNameLoad.DeactivateInputField(); // remove focus from input field
        }
    }

    public void PlayLevel()
    {
        //cellGrid = GameObject.Find("CellGrid");
        //cellGrid.GetComponent<CellGrid>().enabled =true;
        guiController = GameObject.Find("GUIController");
        guiController.GetComponent<GUIController>().enabled = true;

        //guiController.GetComponent<GUIController>().enable();
        //guiController.GetComponent<
        //manager = GameObject.Find("Manager");
        //var guiControllerScript = manager.AddComponent<GUIController>();

    }

    public void EditLevel()
    {
        //cellGrid = GameObject.Find("CellGrid");
        //cellGrid.GetComponent<CellGrid>().enabled = false;
        guiController = GameObject.Find("GUIController");
        guiController.GetComponent<GUIController>().enabled = false;
    }

    // create objects based on data within level.
    private void CreateFromFile()
    {
        GameObject newTest; // make a new object.

        for (int i = 0; i < level.editorObjects.Count | level.editorObjects.Count!= null; i++)
        {
            if (level.editorObjects[i].objectType == EditorObject.ObjectType.Unit) 
            {
                newTest = Instantiate(itemsToPickFrom[0]);
                newTest.transform.position = level.editorObjects[i].pos; // set position from data in level
                newTest.transform.rotation = level.editorObjects[i].rot; // set rotation from data in level.
                newTest.layer = 9; // assign to SpawnedObjects layer.

                newTest.GetComponent<Unit>().PlayerNumber = 0;
                //newUnit.GetComponent<Unit>().Cell = selectedCell;
                units = GameObject.Find("Units");

                newTest.transform.SetParent(units.transform);

                //Add editor object component and feed data.
                EditorObject eo = newTest.AddComponent<EditorObject>();
                eo.data.pos = newTest.transform.position;
                eo.data.rot = newTest.transform.rotation;
                eo.data.objectType = EditorObject.ObjectType.Unit;
            }
            
           else if (level.editorObjects[i].objectType == EditorObject.ObjectType.Unit2) // if a cylinder object
            {
                newTest = Instantiate(itemsToPickFrom[1]);
                newTest.transform.position = level.editorObjects[i].pos; // set position from data in level
                newTest.transform.rotation = level.editorObjects[i].rot; // set rotation from data in level.
                newTest.layer = 9; // assign to SpawnedObjects layer.

                newTest.GetComponent<Unit>().PlayerNumber = 0;
                //newUnit2.GetComponent<Unit>().Cell = selectedCell;
                units = GameObject.Find("Units");

                newTest.transform.SetParent(units.transform);

                //Add editor object component and feed data.
                EditorObject eo = newTest.AddComponent<EditorObject>();
                eo.data.pos = newTest.transform.position;
                eo.data.rot = newTest.transform.rotation;
                eo.data.objectType = EditorObject.ObjectType.Unit2;
            }
            
            else if (level.editorObjects[i].objectType == EditorObject.ObjectType.Obstacle)
            {
                newTest = Instantiate(itemsToPickFrom[2]);
                newTest.transform.position = level.editorObjects[i].pos; // set position from data in level
                newTest.transform.rotation = level.editorObjects[i].rot; // set rotation from data in level.
                newTest.layer = 9; // assign to SpawnedObjects layer.

                //Add editor object component and feed data.
                EditorObject eo = newTest.AddComponent<EditorObject>();
                eo.data.pos = newTest.transform.position;
                eo.data.rot = newTest.transform.rotation;
                eo.data.objectType = EditorObject.ObjectType.Obstacle;
            }

            else if (level.editorObjects[i].objectType == EditorObject.ObjectType.Cell)
            {
                newTest = Instantiate(itemsToPickFrom[3]);
                newTest.transform.position = level.editorObjects[i].pos; // set position from data in level
                newTest.transform.rotation = level.editorObjects[i].rot; // set rotation from data in level.
                //newCell.layer = 9; // assign to SpawnedObjects layer.
                cellGrid = GameObject.Find("CellGrid");
                newTest.transform.SetParent(cellGrid.transform);
                //Add editor object component and feed data.
                EditorObject eo = newTest.AddComponent<EditorObject>();
                eo.data.pos = newTest.transform.position;
                eo.data.rot = newTest.transform.rotation;
                eo.data.objectType = EditorObject.ObjectType.Cell;
            }

        }
    }

}
