using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MapRoomGenerator : MonoBehaviour
{   
    [System.Serializable]
    public class RewardInfo{
        public Reward targetReward;
        public int chance = 50;
    }

    public MapSettings globalMapSettings;
    public WorldSettings world;
    public PlayerStats playerObject;
    public PoolContainer pooler;
    public AstarPath path;
    public QuestGroup[] questGroups;
    public RewardInfo[] possibleRewards;
    [Header("Optional")]
    public GameObject loadingScreen;
    public TextMeshProUGUI roomCounter;

    [HideInInspector]public GameObject roomParent;
    [HideInInspector]public Reward currentReward;

    private int currentRoomIndex = 1;
    private MapDecoration currentDeco;
    private Reward lastReward;
    private List<MapRoom> questRooms = new List<MapRoom>();

    private void Start() {
        questRooms = new List<MapRoom>();
        currentReward = GetRandomReward();
        currentRoomIndex = 1;
        ProcessQuestRooms();
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
        //se currentIndex = 2, usar sala quest[0], se quest.count = 10, ultima quest fica em quest[9], valor maximo pro curIndex de quest seria entao 11
        if(currentRoomIndex >= 2 && currentRoomIndex <= questRooms.Count + 1){
            targetRoom = questRooms[currentRoomIndex - 2];
        }

        GameObject roomBase = Instantiate(targetRoom.roomBase,Vector3.zero,Quaternion.identity,roomParent.transform);

        //escolhendo e colocando decoracao
        MapDecoration decoration = Instantiate<MapDecoration>(targetRoom.GetRandomDecoration(),Vector3.zero,Quaternion.identity,roomParent.transform);
        decoration.ClearPointers();
        currentDeco = decoration;
        //configurando portas
        for(int i = 0; i < decoration.doors.Length;i++){
            decoration.doors[i].SetLocked(true);
            decoration.doors[i].SetupDoor(this,GetRandomReward());
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
        UpdateRoomCounter();   
        path.Scan();
    }

    public void RoomCleared(){
        //spawnar recompensa e abrir portas
        if(!playerObject) return;
        if(currentReward){
            Reward rr = pooler.SpawnTargetObject(currentReward.thisPoolObject,1,transform).GetComponent<Reward>();
            rr.transform.position = playerObject.transform.position;
        }
        for(int i = 0; i < currentDeco.doors.Length;i++){
            currentDeco.doors[i].SetLocked(false);
        }
    }

    public void AdvanceRoom(Reward nextReward){ //para EnemyCounter chamar
        currentReward = nextReward;
        if(currentRoomIndex >= globalMapSettings.numberOfRooms){
            SceneManager.LoadScene(globalMapSettings.destinationScene.Value);
            world.SetDestination(null);
        }else{
            //next room
            currentRoomIndex++;
            GenerateMap();
        }        
    }

    public Reward GetRandomReward(){
        int chanceRoll = Random.Range(0,100);
        for(int i = 0; i < possibleRewards.Length; i++){
            if(possibleRewards[i].chance <= chanceRoll && (!lastReward || possibleRewards[i].targetReward != lastReward)){
                lastReward = possibleRewards[i].targetReward;
                return possibleRewards[i].targetReward;
            }
        }
        return possibleRewards[0].targetReward;
    }

    private void ProcessQuestRooms(){
        for(int i = 0; i < questGroups.Length;i++){
            for(int y = 0; y < questGroups[i].quests.Count; y++){
                ProcessMission(questGroups[i].quests[y].GetCurrentMission());
            }
        }
        if(globalMapSettings.numberOfRooms <= questRooms.Count){//quase impossivel
            globalMapSettings.numberOfRooms = questRooms.Count+1; 
        }
    }

    private void ProcessMission(Mission mission){
        for(int i = 0; i < mission.outcomes.Length;i++){
            for(int y = 0; y < mission.outcomes[i].goals.Length; y++){
                Mission.Goal goal = mission.outcomes[i].goals[y];
                if(!goal.forcedRoom) continue;
                if(!goal.locationRequirement2){//only 1 requirement
                    if(goal.locationRequirement1 == world.currentPathway.end1 || goal.locationRequirement1 == world.currentPathway.end2){
                        questRooms.Add(goal.forcedRoom);
                    }
                }else{
                    if((goal.locationRequirement1 == world.currentPathway.end1 && goal.locationRequirement2 == world.currentPathway.end2) ||
                    (goal.locationRequirement2 == world.currentPathway.end1 && goal.locationRequirement1 == world.currentPathway.end2)){
                        questRooms.Add(goal.forcedRoom);
                    }
                }
            }
        }
    }

    private void UpdateRoomCounter(){
        if(!roomCounter) return;
        roomCounter.SetText("Room: "+currentRoomIndex.ToString()+"/"+globalMapSettings.numberOfRooms.ToString());
    }
}
