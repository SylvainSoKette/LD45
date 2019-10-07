using UnityEngine;

enum tileState {
    IDLE,
    EXPLORING,
    HARVESTABLE,
}


public class TileBehavior : MonoBehaviour
{
    public static float minSpawnTime = 3.0f;
    public static float maxSpawnTime = 12.0f;

    public Material matForest;
    public Material matHill;
    public Material matMountain;
    public Material matPlain;
    public Material matRocky;
    public Material matUnexplored;

    private tileType type = tileType.UNEXPLORED;
    private tileState state = tileState.IDLE;
    private float exploration = 0.0f;

    private int posX;
    private int posY;

    public Transform pickablePrefab;
    private float ressourceSpawnTime;
    private float ressourceProgress = 0.0f;

    // utility references
    private Shader shader;
    private GameObject hud;
    private GameObject board;
    private BoardBehavior boardBehavior;

    private Transform progressBar;
    private Transform progressBarContent;

    void Start()
    {
        hud = GameObject.Find("HUD");
        // board = GameObject.Find("Board");
        Shader shader = GetComponent<Renderer>().material.shader;

        board = GameObject.Find("Board");
        boardBehavior = board.GetComponent<BoardBehavior>();

        progressBar = this.transform.Find("ProgressBarCanvas");
        this.progressBar.gameObject.SetActive(false);
        progressBarContent = this.transform.Find("ProgressBarCanvas/progress");
    }

    void Update()
    {
        if (this.state == tileState.EXPLORING) {
            this.progressBar.gameObject.SetActive(true);
            this.exploration += Time.deltaTime * boardBehavior.GetExplorationSpeed();
            if (this.exploration >= 1.0f) {
                this.Explore();
            }
        } else if (this.type != tileType.UNEXPLORED && this.state == tileState.IDLE) {
            this.ressourceProgress += Time.deltaTime;
            if (this.ressourceProgress >= this.ressourceSpawnTime)
            {
                this.GenerateRessource();
            }
        }

        float progress = (float)exploration / (float)1.0f;
        progressBarContent.localScale = new Vector3(progress, 1, 1);
    }

    public void Init(int x, int y, bool bStartingTile)
    {
        this.GetComponent<Renderer>().material = matUnexplored;
        this.transform.SetPositionAndRotation(
            new Vector3(x, y, 0),
            new Quaternion(0, 180, 0, 0)
        );
        this.posX = x;
        this.posY = y;
        this.name = "Tile_" + this.posX + "_" + this.posY;

        if (bStartingTile) {
            this.Explore(true);
        }
    }

    tileType getRandomType()
    {
        float value = Random.value;
        if (value > 0.9f) {
            return tileType.MOUNTAIN;
        } else if (value > 0.8f) {
            return tileType.ROCKY;
        } else if (value > 0.6f) {
            return tileType.HILL;
        } else if (value > 0.3f) {
            return tileType.FOREST;
        } else {
            return tileType.PLAIN;
        }
    }

    string getTileDescription()
    {
        switch(this.type) {
            case tileType.UNEXPLORED:
                return "unexplored !";
            case tileType.FOREST:
                return "forest !";
            case tileType.HILL:
                return "hill !";
            case tileType.ROCKY:
                return "rocky !";
            case tileType.MOUNTAIN:
                return "mountain !";
            case tileType.PLAIN:
                return "plain !";
            default:
                return "Something's obviously broken in this game !";
        }
    }

    float GetSpawnTime() {
        return Random.Range(minSpawnTime, maxSpawnTime);
    }

    void Explore(bool bStartingTile = false) {
        // set variables
        this.exploration = 1.0f;
        this.state = tileState.IDLE;
        this.ressourceSpawnTime = GetSpawnTime();
        GameObject.Find("Board").GetComponent<BoardBehavior>().bExploring = false;

        // update type
        if (bStartingTile) {
            this.type = tileType.PLAIN;
        } else {
            this.type = getRandomType();
            this.progressBar.gameObject.SetActive(false);
        }

        // update material
        switch (this.type) {
        case tileType.FOREST:
            this.GetComponent<Renderer>().material = matForest;
            break;
        case tileType.HILL:
            this.GetComponent<Renderer>().material = matHill;
            break;
        case tileType.ROCKY:
            this.GetComponent<Renderer>().material = matRocky;
            break;
        case tileType.MOUNTAIN:
            this.GetComponent<Renderer>().material = matMountain;
            break;
        case tileType.PLAIN:
            this.GetComponent<Renderer>().material = matPlain;
            break;
        default:
            this.GetComponent<Renderer>().material = matUnexplored;
            break;
        }

        // create new neighbor if necessary
        foreach(location loc in location.GetValues(typeof(location))) {
            int x = GetNeighborX(loc);
            int y = GetNeighborY(loc);
            if (GameObject.Find("Tile_" + x + "_" + y) == null) {
                GameObject.Find("Board").GetComponent<BoardBehavior>().MakeTile(x, y);
            }
        }
    }

    int GetNeighborX(location loc) {
        if (loc == location.LEFT) {
            return this.posX + -1;
        } else if (loc == location.RIGHT) {
            return this.posX + 1;
        } else {
            return this.posX;
        }
    }

    int GetNeighborY(location loc) {
        if (loc == location.TOP) {
            return this.posY + -1;
        } else if (loc == location.DOWN) {
            return this.posY + 1;
        } else {
            return this.posY;
        }
    }

    public void GenerateRessource() {
        this.state = tileState.HARVESTABLE;
        Transform res = Instantiate(pickablePrefab, Vector3.zero, Quaternion.identity);
        res.GetComponent<PickableBehavior>().Init(this.posX, this.posY, RandomResType());
    }

    private ressource RandomResType() {
        float rand = Random.value;

        switch(this.type) {
            case tileType.FOREST:
                if (rand > 0.6) {
                    return ressource.STICK; // 40%
                } else if (rand > 0.2) {
                    return ressource.LEAF; // 40%
                } else {
                    return ressource.FRUIT; // 20%
                }
            case tileType.HILL:
                if (rand > 0.6) {
                    return ressource.DIRT; // 40%
                } else if (rand > 0.2) {
                    return ressource.STONE; // 40%
                } else {
                    return ressource.CLAY; // 20%
                }
            case tileType.MOUNTAIN:
                if (rand > 0.4) {
                    return ressource.STONE; // 60%
                } else {
                    return ressource.DIRT; // 40%
                }
            case tileType.PLAIN:
                if (rand > 0.5) {
                    return ressource.STICK; // 50%
                } else if (rand > 0.1) {
                    return ressource.DIRT; // 30%
                } else {
                    return ressource.LEAF; // 10%
                }
            case tileType.ROCKY:
                if (rand > 0.4) {
                    return ressource.CLAY; // 60%
                } else if (rand > 0.2) {
                    return ressource.DIRT; // 20%
                } else {
                    return ressource.STONE; // 20%
                }
            default:
                return ressource.DIRT;
        }
    }

    public void Harvest() {
        this.state = tileState.IDLE;
        this.ressourceProgress = 0.0f;
        this.ressourceSpawnTime = GetSpawnTime();
    }

    void OnMouseOver() {
        // tile mouse hover effect
        GetComponent<Renderer>().material.SetFloat("_Metallic", 0.9f);

        // click on tile stuff
        if (Input.GetMouseButtonDown(0)) {
            if (this.type == tileType.UNEXPLORED) {
                GameObject board = GameObject.Find("Board");
                if (this.state != tileState.EXPLORING && !board.GetComponent<BoardBehavior>().bExploring) {
                    board.GetComponent<BoardBehavior>().bExploring = true;
                    this.state = tileState.EXPLORING;
                }
            }
        }
    }

    void OnMouseExit() {
        GetComponent<Renderer>().material.SetFloat("_Metallic", 0.0f);
    }
}
