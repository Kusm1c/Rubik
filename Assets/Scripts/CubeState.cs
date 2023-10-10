using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CubeState
{
    public static Array sideNames = Enum.GetValues(typeof(SideName));
    public List<GameObject> OrangeSideCubes { get; private set; } = new();
    public List<GameObject> GreenSideCubes { get; private set; } = new();
    public List<GameObject> RedSideCubes { get; private set; } = new();
    public List<GameObject> BlueSideCubes { get; private set; } = new();
    public List<GameObject> WhiteSideCubes { get; private set; } = new();
    public List<GameObject> YellowSideCubes { get; private set; } = new();

    //Clear cube state function
    public void ClearCubeState()
    {
        OrangeSideCubes.Clear();
        GreenSideCubes.Clear();
        RedSideCubes.Clear();
        BlueSideCubes.Clear();
        WhiteSideCubes.Clear();
        YellowSideCubes.Clear();
    }
    
    public void RotateOrangeSide(bool clockWise)
    {
        RotateSide(OrangeSideCubes, clockWise);
    }
    
    public void RotateGreenSide(bool clockWise)
    {
        RotateSide(GreenSideCubes, clockWise);
    }
    
    public void RotateRedSide(bool clockWise)
    {
        RotateSide(RedSideCubes, clockWise);
    }
    
    public void RotateBlueSide(bool clockWise)
    {
        RotateSide(BlueSideCubes, clockWise);
    }
    
    public void RotateWhiteSide(bool clockWise)
    {
        RotateSide(WhiteSideCubes, clockWise);
    }
    
    public void RotateYellowSide(bool clockWise)
    {
        RotateSide(YellowSideCubes, clockWise);
    }
    
    private void RotateSide(List<GameObject> sideCubes, bool clockWise)
    {
        if (clockWise)
        {
            GameObject tempCube = sideCubes[0];
            sideCubes.RemoveAt(0);
            sideCubes.Add(tempCube);
        }
        else
        {
            GameObject tempCube = sideCubes[^1];
            sideCubes.RemoveAt(sideCubes.Count - 1);
            sideCubes.Insert(0, tempCube);
        }
    }
    
    public CubeState GetCurrentCubeState()
    {
        Debug.Log("GetCurrentCubeState started");
        CubeState currentState = new()
        {
            OrangeSideCubes = OrangeSideCubes,
            GreenSideCubes = GreenSideCubes,
            RedSideCubes = RedSideCubes,
            BlueSideCubes = BlueSideCubes,
            WhiteSideCubes = WhiteSideCubes,
            YellowSideCubes = YellowSideCubes
        };
        Debug.Log("GetCurrentCubeState finished");
        return currentState;
    }

    public List<GameObject> GetSideByIndex(int sideIndex)
    {
        return sideIndex switch
        {
            0 => OrangeSideCubes,
            1 => GreenSideCubes,
            2 => RedSideCubes,
            3 => BlueSideCubes,
            4 => WhiteSideCubes,
            5 => YellowSideCubes,
            _ => null
        };
    }
    
    public override bool Equals(object obj)
    {
        if (obj == null || GetType() != obj.GetType())
        {
            return false;
        }

        CubeState otherState = (CubeState)obj;

        return OrangeSideCubes.SequenceEqual(otherState.OrangeSideCubes) &&
               GreenSideCubes.SequenceEqual(otherState.GreenSideCubes) &&
               RedSideCubes.SequenceEqual(otherState.RedSideCubes) &&
               BlueSideCubes.SequenceEqual(otherState.BlueSideCubes) &&
               WhiteSideCubes.SequenceEqual(otherState.WhiteSideCubes) &&
               YellowSideCubes.SequenceEqual(otherState.YellowSideCubes);
    }

    public override int GetHashCode()
    {
        unchecked
        {
            int hash = 17;

            foreach (var cube in OrangeSideCubes)
            {
                hash = hash * 23 + cube.GetHashCode();
            }

            foreach (var cube in GreenSideCubes)
            {
                hash = hash * 23 + cube.GetHashCode();
            }

            foreach (var cube in RedSideCubes)
            {
                hash = hash * 23 + cube.GetHashCode();
            }

            foreach (var cube in BlueSideCubes)
            {
                hash = hash * 23 + cube.GetHashCode();
            }

            foreach (var cube in WhiteSideCubes)
            {
                hash = hash * 23 + cube.GetHashCode();
            }

            foreach (var cube in YellowSideCubes)
            {
                hash = hash * 23 + cube.GetHashCode();
            }

            return hash;
        }
    }
    
    public bool RedSideIsSolved()
    {
        List<GameObject> redSide = GetSideByIndex((int)SideName.Red);
        for (int i = 0; i < redSide.Count; i++)
        {
            if (redSide[i].transform.position != Cube.instance.solvedCubeState.GetSideByIndex((int)SideName.Red)[i].transform.position)
            {
                return false;
            }
        }
        
        return true;
    }
    
    public bool OrangeSideIsSolved()
    {
        List<GameObject> orangeSide = GetSideByIndex((int)SideName.Orange);
        for (int i = 0; i < orangeSide.Count; i++)
        {
            if (orangeSide[i].transform.position != Cube.instance.solvedCubeState.GetSideByIndex((int)SideName.Orange)[i].transform.position)
            {
                return false;
            }
        }
        
        return true;
    }
    
    public bool GreenSideIsSolved()
    {
        List<GameObject> greenSide = GetSideByIndex((int)SideName.Green);
        for (int i = 0; i < greenSide.Count; i++)
        {
            if (greenSide[i].transform.position != Cube.instance.solvedCubeState.GetSideByIndex((int)SideName.Green)[i].transform.position)
            {
                return false;
            }
        }
        
        return true;
    }
    
    public bool BlueSideIsSolved()
    {
        List<GameObject> blueSide = GetSideByIndex((int)SideName.Blue);
        for (int i = 0; i < blueSide.Count; i++)
        {
            if (blueSide[i].transform.position != Cube.instance.solvedCubeState.GetSideByIndex((int)SideName.Blue)[i].transform.position)
            {
                return false;
            }
        }
        
        return true;
    }
    
    public bool WhiteSideIsSolved()
    {
        List<GameObject> whiteSide = GetSideByIndex((int)SideName.White);
        for (int i = 0; i < whiteSide.Count; i++)
        {
            if (whiteSide[i].transform.position != Cube.instance.solvedCubeState.GetSideByIndex((int)SideName.White)[i].transform.position)
            {
                return false;
            }
        }
        
        return true;
    }
    
    public bool YellowSideIsSolved()
    {
        List<GameObject> yellowSide = GetSideByIndex((int)SideName.Yellow);
        for (int i = 0; i < yellowSide.Count; i++)
        {
            if (yellowSide[i].transform.position != Cube.instance.solvedCubeState.GetSideByIndex((int)SideName.Yellow)[i].transform.position)
            {
                return false;
            }
        }
        
        return true;
    }

    public bool IsSolved()
    {
        foreach (var side in sideNames)
        {
            List<GameObject> sideCubes = GetSideByIndex((int)side);
            for (int i = 0; i < sideCubes.Count; i++)
            {
                if (sideCubes[i].transform.position != Cube.instance.solvedCubeState.GetSideByIndex((int)side)[i].transform.position)
                {
                    return false;
                }
            }
        }
        
        return true;
    }
}

public enum SideName
{
    Blue,
    Green,
    Red,
    Orange,
    White,
    Yellow
}