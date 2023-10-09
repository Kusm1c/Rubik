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