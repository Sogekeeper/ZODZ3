using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WorldSettings", menuName = "World/World Settings", order = 1)]
public class WorldSettings : ScriptableObject
{
    [System.Serializable]
    public class Pathway{
        public Location end1;
        public Location end2;
        public int distance;
        public float difficulty = 1;

        public Pathway(Location firstLocation, Location secondLocation, int _distance, float _difficulty){
            end1 = firstLocation;
            end2 = secondLocation;
            distance = _distance;
            difficulty = _difficulty;
        }

        public bool CheckEnds(Location targetEnd1, Location targetEnd2){
            if((end1 == targetEnd1 && end2 == targetEnd2) || (end2 == targetEnd1 && end1 == targetEnd2)){
                return true;
            }else
                return false;
        }
    }

    public int maxDistanceFactor = 7;
    public float maxDifficultyFactor = 5;
    public int minRoomAmount;
    public int maxRoomAmount;
    public Location origin;
    public Location currentLocation {get;private set;}
    public Location destination {get;private set;} //null on cities
    public Pathway currentPathway{get;private set;} //null on cities
    public Location[] locations;

    public List<Pathway> pathways {get; private set;}

    [ContextMenu("Initialize World")]
    public void InitializeWorld(Location startLocation){
        origin = startLocation;
        currentLocation = startLocation;
        GeneratePathways();
    }

    public void SetCurrentLocation(Location current){
        currentLocation = current;
    }
    public void SetDestination(Location current){
        destination = current;
    }
    public void SetPathway(Pathway current){
        currentPathway = current;
    }

    private void GeneratePathways(){
        pathways = new List<Pathway>();
        for(int i = 0; i < locations.Length; i++){
            if(locations[i].neighbors == null) continue; //skip checking neighbors
            for(int y = 0; y < locations[i].neighbors.Length;y++){
                //if pathway already exists, skip math
                if(GetPathway(locations[i],locations[i].neighbors[y]) != null) continue;

                float diffValue = Random.Range(1,maxDifficultyFactor);
                float diffSecondaryFactor = Random.Range(0,4);
                diffSecondaryFactor = (origin.distanceFactor - locations[i].neighbors[y].distanceFactor) < maxDistanceFactor/2 ? 
                    diffSecondaryFactor : diffSecondaryFactor * -1;
                diffValue = Mathf.Clamp(diffValue+diffSecondaryFactor,1,maxDifficultyFactor);
                int roomValue = Random.Range(minRoomAmount,maxRoomAmount);
                int secondaryRoomValue = Random.Range(0,maxRoomAmount/3);
                secondaryRoomValue = (origin.distanceFactor - locations[i].neighbors[y].distanceFactor) < maxDistanceFactor/2 ? 
                    secondaryRoomValue : secondaryRoomValue * -1;
                roomValue = Mathf.Clamp(roomValue+secondaryRoomValue,minRoomAmount,maxRoomAmount);
                pathways.Add(new Pathway(locations[i],locations[i].neighbors[y],roomValue,diffValue));
                
            }
        }
    }

    public Pathway GetPathway(Location targetEnd1, Location targetEnd2){
        if(pathways == null) pathways = new List<Pathway>();
        for(int i = 0; i < pathways.Count;i++){
            if(pathways[i].CheckEnds(targetEnd1, targetEnd2)){
                return pathways[i];
            }
        }
        return null;
    }

}
