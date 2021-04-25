using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public int level;
    public int row, col,countStep;
    public int rowBlank, colBlank;
    public int sizeRow, sizeCol;

    public bool startControl = false;
    public bool checkComplete;
    
    private int countPoint = 0;
    private int countImageKey;

    public List<GameObject> imageKeyList;
    public List<GameObject> imageOfPictureList;
    public List<GameObject> checkPointList;

    private GameObject temp;
    
    private GameObject[,] imageKeyMatrix;
    private GameObject[,] imageOfPictureMatrix;
    private GameObject[,] checkPointMatrix;

    private void Start()
    {
        if (level==1)
        {
            ImageOfEasyLevel();
        }
        else if (level==2)
        {
            ImageOfNormalLevel();
        }
        else if (level==3)
        {
            ImageOfHardLevel();
        }
        imageKeyMatrix = new GameObject[sizeRow, sizeCol];
        imageOfPictureMatrix = new GameObject[sizeRow, sizeCol];
        checkPointMatrix = new GameObject[sizeRow, sizeCol];

       CheckPointManager();
       ImageKeyManager();
       
       for (int r = 0; r < sizeRow; r++)
       {
           for (int c = 0; c < sizeCol; c++)
           {
             if (imageOfPictureMatrix[r, c].name.CompareTo("blank")==0)
             {
                  rowBlank = r;
                  colBlank = c;
                  break;
                  
             }
           }
       }
    }

    private void Update()
    {
        if (startControl)
        {
            startControl = false;
            if (countStep == 1)
            {
                if (imageOfPictureMatrix[row, col] != null && imageOfPictureMatrix[row, col].name.CompareTo("blank") != 0)
                {
                    if (rowBlank!=row && colBlank==col)         
                    {
                        if (Mathf.Abs(row-rowBlank)==1)
                        {
                            SortImage();
                            countStep = 0;
                        }
                        else
                        {
                            countStep = 0;
                        }
                    }
                    else if (rowBlank==row && colBlank!=col)
                    {           
                        if (Mathf.Abs(col-colBlank)==1)
                        {
                            SortImage();
                            countStep = 0;
                        }
                        else
                        {
                            countStep = 0;
                        }
                    }
                    else if ((rowBlank==row && colBlank==col) || (rowBlank!=row && colBlank!=col))
                    {
                        countStep = 0;
                    }
                }
                else
                {
                    countStep = 0;
                }
            }
        }
    }

    private void SortImage()
    {
        temp = imageOfPictureMatrix[rowBlank, colBlank];
        imageOfPictureMatrix[rowBlank, colBlank] = null;

        imageOfPictureMatrix[rowBlank, colBlank] = imageOfPictureMatrix[row, col];
        imageOfPictureMatrix[row, col] = null;

        imageOfPictureMatrix[row, col] = temp;

        imageOfPictureMatrix[rowBlank, colBlank].GetComponent<ImageController>().target =
            checkPointMatrix[rowBlank, colBlank];
        imageOfPictureMatrix[row, col].GetComponent<ImageController>().target = checkPointMatrix[row, col];

        imageOfPictureMatrix[rowBlank, colBlank].GetComponent<ImageController>().startMove = true;
        imageOfPictureMatrix[row, col].GetComponent<ImageController>().startMove = true;

        rowBlank = row;
        colBlank = col;
    }

    private void ImageKeyManager()
        {
            for (int r = 0; r < sizeRow; r++)
            {
                for (int c = 0; c < sizeCol; c++)
                {
                    imageKeyMatrix[r, c] = imageKeyList[countImageKey];
                    countImageKey++;
                }
            }
        }
    
    private void CheckPointManager()
    {
        for (int r = 0; r < sizeRow; r++)
        {
            for (int c = 0; c < sizeCol; c++)
            {
                checkPointMatrix[r, c] = checkPointList[countPoint];
                countPoint++;
            }
        }
    }

    void ImageOfEasyLevel()
    {
        imageOfPictureMatrix[0, 0] = imageOfPictureList[0];
        imageOfPictureMatrix[0, 1] = imageOfPictureList[2];
        imageOfPictureMatrix[0, 2] = imageOfPictureList[5];
        imageOfPictureMatrix[1, 0] = imageOfPictureList[4];
        imageOfPictureMatrix[1, 1] = imageOfPictureList[1];
        imageOfPictureMatrix[1, 2] = imageOfPictureList[7];
        imageOfPictureMatrix[2, 0] = imageOfPictureList[3];
        imageOfPictureMatrix[2, 1] = imageOfPictureList[6];
        imageOfPictureMatrix[2, 2] = imageOfPictureList[8];
    }

    private void ImageOfNormalLevel()
    {
        imageOfPictureMatrix[0, 0] = imageOfPictureList[4];
        imageOfPictureMatrix[0, 1] = imageOfPictureList[0];
        imageOfPictureMatrix[0, 2] = imageOfPictureList[1];
        imageOfPictureMatrix[0, 3] = imageOfPictureList[2];
        imageOfPictureMatrix[1, 0] = imageOfPictureList[8];
        imageOfPictureMatrix[1, 1] = imageOfPictureList[6];
        imageOfPictureMatrix[1, 2] = imageOfPictureList[7];
        imageOfPictureMatrix[1, 3] = imageOfPictureList[11];
        imageOfPictureMatrix[2, 0] = imageOfPictureList[2];
        imageOfPictureMatrix[2, 1] = imageOfPictureList[5];
        imageOfPictureMatrix[2, 2] = imageOfPictureList[14];
        imageOfPictureMatrix[2, 3] = imageOfPictureList[10];
        imageOfPictureMatrix[3, 0] = imageOfPictureList[13];
        imageOfPictureMatrix[3, 1] = imageOfPictureList[9];
        imageOfPictureMatrix[3, 2] = imageOfPictureList[15];
        imageOfPictureMatrix[3, 3] = imageOfPictureList[3];
    }

    private void ImageOfHardLevel()
    {
        imageOfPictureMatrix[0, 0] = imageOfPictureList[5];
        imageOfPictureMatrix[0, 1] = imageOfPictureList[2];
        imageOfPictureMatrix[0, 2] = imageOfPictureList[3];
        imageOfPictureMatrix[0, 3] = imageOfPictureList[4];
        imageOfPictureMatrix[0, 4] = imageOfPictureList[9];
        imageOfPictureMatrix[1, 0] = imageOfPictureList[10];
        imageOfPictureMatrix[1, 1] = imageOfPictureList[1];
        imageOfPictureMatrix[1, 2] = imageOfPictureList[12];
        imageOfPictureMatrix[1, 3] = imageOfPictureList[7];
        imageOfPictureMatrix[1, 4] = imageOfPictureList[8];
        imageOfPictureMatrix[2, 0] = imageOfPictureList[15];
        imageOfPictureMatrix[2, 1] = imageOfPictureList[6];
        imageOfPictureMatrix[2, 2] = imageOfPictureList[13];
        imageOfPictureMatrix[3, 3] = imageOfPictureList[14];
        imageOfPictureMatrix[4, 4] = imageOfPictureList[19];
        imageOfPictureMatrix[4, 0] = imageOfPictureList[20];
        imageOfPictureMatrix[4, 1] = imageOfPictureList[11];
        imageOfPictureMatrix[4, 2] = imageOfPictureList[22];
        imageOfPictureMatrix[4, 3] = imageOfPictureList[17];
        imageOfPictureMatrix[4, 4] = imageOfPictureList[18];
        imageOfPictureMatrix[5, 0] = imageOfPictureList[21];
        imageOfPictureMatrix[5, 1] = imageOfPictureList[16];
        imageOfPictureMatrix[5, 2] = imageOfPictureList[23];
        imageOfPictureMatrix[5, 3] = imageOfPictureList[24];     
        imageOfPictureMatrix[5, 4] = imageOfPictureList[0];

    }
}
