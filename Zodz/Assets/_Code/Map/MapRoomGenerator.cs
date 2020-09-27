using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MapRoomGenerator : MonoBehaviour
{
    public MapSettings globalMapSettings;
    [HideInInspector]public GameObject roomParent;
    public PlayerStats playerObject;
    public PoolContainer pooler;
    public AstarPath path;
    [Header("Optional")]
    public GameObject loadingScreen;

    [HideInInspector]public Reward currentReward;

    private int currentRoomIndex = 0;
    private MapDecoration currentDeco;

    private void Start() {
        GenerateMap();
    }

    private void GenerateMap(){
        //setando custo
        int costForEnemies = globalMapSettings.totalEntitiesCost;

        //limpar mapa antigo se houver
        if(roomParent != null) Destroy(roomParent); 
        roomParent = new GameObject("RoomRoot");
        roomParent.transform.position = Vector3.zero;
        MapRoom targetRoom = null;

        //colocando base
        if(currentRoomIndex < (float)(globalMapSettings.numberOfRooms)/2.00f){
            targetRoom = globalMapSettings.startRoomSet.GetRandomRoom();
        }else{
            targetRoom = globalMapSettings.endRoomSet.GetRandomRoom();
        }        
        GameObject roomBase = Instantiate(targetRoom.roomBase,Vector3.zero,Quaternion.identity,roomParent.transform);

        //escolhendo e colocando decoracao
        MapDecoration decoration = Instantiate<MapDecoration>(targetRoom.GetRandomDecoration(),Vector3.zero,Quaternion.identity,roomParent.transform);
        decoration.ClearPointers();
        currentDeco = decoration;
        //configurando portas
        for(int i = 0; i < decoration.doors.Length;i++){
            decoration.doors[i].SetLocked(true);
            decoration.doors[i].SetupDoor(this,null);
            //setar recompensas
        }

        //spawn de inimigos
        MapEnemySet targetSet = currentRoomIndex <= (float)globalMapSettings.numberOfRooms/2 ? 
            globalMapSettings.startEnemySet : globalMapSettings.endEnemySet;

        for(int i = 0; i < decoration.pointers.Length; i++){
            MapPointer randomPointer = decoration.GetRandomAvailablePointer();
            //verifica todos inimigos possiveis para esse ponto e pega um aleatorio
            if(randomPointer == null)
                break; //sair do for por falta de pontos
            
            MapEntity targetEntity = targetSet.GetRandomEntity(globalMapSettings.mapDifficulty,
                randomPointer.pointSize,costForEnemies);
            if(targetEntity != null){
                costForEnemies -= targetEntity.entityCost;
                MapEntity spawnedEntity = Instantiate<MapEntity>(targetEntity,
                    randomPointer.transform.position,
                        Quaternion.identity,roomParent.transform);
                randomPointer.used = true;
            }            
        }

        playerObject.transform.position = decoration.playerSpawn.position;        
        path.Scan();
    }

    public void RoomCleared(){
        //spawnar recompensa e abrir portas
        if(currentReward){
            
        }
        for(int i = 0; i < currentDeco.doors.Length;i++){
            currentDeco.doors[i].SetLocked(false);
        }
    }

    public void AdvanceRoom(Reward nextReward){ //para EnemyCounter chamar
        currentReward = nextReward;
        if(currentRoomIndex >= globalMapSettings.numberOfRooms){
            SceneManager.LoadScene(globalMapSettings.destinationScene.Value);
        }else{
            //next room
            currentRoomIndex++;
            GenerateMap();
        }        
    }
}
