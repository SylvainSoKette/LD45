using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public enum tileType {
    UNEXPLORED,
    PLAIN,
    HILL,
    ROCKY,
    MOUNTAIN,
    FOREST
}

public enum location {
    LEFT,
    RIGHT,
    TOP,
    DOWN
}

public enum ressource {
    DIRT,
    CLAY,
    STICK,
    BRICK,
    STONE,
    LEAF,
    FRUIT,
    FOOD,
}


public class BoardBehavior : MonoBehaviour
{
    public Transform prefab;
    public int mapWidth = 8;
    public int mapHeight = 4;
    public bool bExploring = false;
    public float explorationSpeed = 0.3f;

    private Dictionary<ressource, int> ressources;
    private int days;
    private bool won = false;

    public AudioSource pickupSound;
    public AudioSource buildingSound;
    public AudioSource errorSound;

    private StorageHutBuilding storageHutBuilding;
    public GameObject storageHutButton;
    private Text storageHutButtonText;

    private FirePitBuilding firePitBuilding;
    public GameObject firePitButton;
    private Text firePitButtonText;

    private HouseBuilding houseBuilding;
    public GameObject houseButton;
    private Text houseButtonText;

    private KitchenBuilding kitchenBuilding;
    public GameObject kitchenButton;
    private Text kitchenButtonText;

    private MonumentBuilding monumentBuilding;
    public GameObject monumentButton;
    private Text monumentButtonText;

    public GameObject winScreen;
    public GameObject score;

    void Start() {
        ressources = new Dictionary<ressource, int>();
        foreach(ressource res in ressource.GetValues(typeof(ressource))) {
            ressources.Add(res, 0);
        }
        days = 0;

        MakeTile(0, 0, true);

        storageHutBuilding = new StorageHutBuilding();
        storageHutButtonText = storageHutButton.GetComponentInChildren<Text>();
        firePitBuilding = new FirePitBuilding();
        firePitButtonText = firePitButton.GetComponentInChildren<Text>();
        houseBuilding = new HouseBuilding();
        houseButtonText = houseButton.GetComponentInChildren<Text>();
        kitchenBuilding = new KitchenBuilding();
        kitchenButtonText = kitchenButton.GetComponentInChildren<Text>();
        monumentBuilding = new MonumentBuilding();
        monumentButtonText = monumentButton.GetComponentInChildren<Text>();
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            SceneManager.LoadScene(0);
        }

        // update buttons
        storageHutButtonText.text = storageHutBuilding.GetLevel().ToString();
        firePitButtonText.text = firePitBuilding.GetLevel().ToString();
        houseButtonText.text = houseBuilding.GetLevel().ToString();
        kitchenButtonText.text = kitchenBuilding.GetLevel().ToString();
        monumentButtonText.text = monumentBuilding.GetLevel().ToString();

        if (monumentBuilding.GetLevel() > 0) {
            winScreen.SetActive(true);
        }
    }

    public void MakeTile(int x, int y, bool bStartingTile = false) {
        if (x <= mapWidth &&
            x >= -mapWidth &&
            y <= mapHeight &&
            y >= -mapHeight)
        {
            Transform tile = Instantiate(prefab, Vector3.zero, Quaternion.identity);
            tile.GetComponent<TileBehavior>().Init(x, y, bStartingTile);
        }
    }

    private int GetLowest(int value1, int value2) {
        if (value1 < value2) {
            return value1;
        } else {
            return value2;
        }
    }

    public void IncrementDay() {
        if (!won) {
            this.days += 1;
        
            if (firePitBuilding.GetLevel() > 0) {
                ressource clay = ressource.CLAY;
                int maxPossible = GetLowest(firePitBuilding.GetLevel(), GetRessource(clay));
                UseRessource(clay, maxPossible);
                IncrementRessource(ressource.BRICK, maxPossible);
            }

            if (kitchenBuilding.GetLevel() > 0) {
                ressource fruit = ressource.FRUIT;
                int maxPossible = GetLowest(kitchenBuilding.GetLevel(), GetRessource(fruit));
                UseRessource(fruit, maxPossible);
                IncrementRessource(ressource.FOOD, maxPossible);
            }
        }
    }

    public int GetDays() {
        return this.days;
    }

    public void IncrementRessource(ressource res, int quantity = 1) {
        this.pickupSound.Play(0);
        if (this.ressources[res] < GetMaxRessources()) {
            
            this.ressources[res] += quantity;
        }
    }

    public void UseRessource(ressource res, int quantity = 1) {
        this.ressources[res] -= quantity;
    }

    public int GetRessource(ressource res) {
        return this.ressources[res];
    }

    public void PlayBuildingSound() {
        this.buildingSound.Play(0);
    }

    public int GetMaxRessources() {
        return 5 + 5 * storageHutBuilding.GetLevel();
    }

    public void ClickStorageHutBuilding() {
        if (storageHutBuilding.MeetRequirements()) {
            this.storageHutBuilding.Upgrade();
            this.buildingSound.Play(0);
        } else {
            this.errorSound.Play(0);
        }
    }

    public void ClickFirePitBuilding() {
        if (firePitBuilding.MeetRequirements()) {
            this.firePitBuilding.Upgrade();
            this.buildingSound.Play(0);
        } else {
            this.errorSound.Play(0);
        }
    }

    public float GetExplorationSpeed() {
        return this.explorationSpeed + 0.1f * houseBuilding.GetLevel();
    }

    public void ClickHouseBuilding() {
        if (houseBuilding.MeetRequirements()) {
            this.houseBuilding.Upgrade();
            this.buildingSound.Play(0);
        } else {
            this.errorSound.Play(0);
        }
    }

    public void ClickKitchenBuilding() {
        if (kitchenBuilding.MeetRequirements()) {
            this.kitchenBuilding.Upgrade();
            this.buildingSound.Play(0);
        } else {
            this.errorSound.Play(0);
        }
    }

    public void ClickMonumentBuilding() {
        if (monumentBuilding.MeetRequirements()) {
            this.monumentBuilding.Upgrade();
            this.buildingSound.Play(0);
            score.GetComponent<Text>().text = "Score : "+GetDays()+" days";
        } else {
            this.errorSound.Play(0);
        }
    }
}
