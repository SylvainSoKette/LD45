using UnityEngine;

public class PickableBehavior : MonoBehaviour
{
    public Material matDirt;
    public Material matClay;
    public Material matStick;
    public Material matBrick;
    public Material matStone;
    public Material matLeaf;
    public Material matFruit;

    int posX;
    int posY;
    ressource type;

    public void Init(int x, int y, ressource type)
    {
        this.posX = x;
        this.posY = y;
        
        this.type = ressource.CLAY;
        this.type = type;
        switch(this.type) {
            case ressource.DIRT:
                SetMaterial(matDirt);
                break;
            case ressource.CLAY:
                SetMaterial(matClay);
                break;
            case ressource.STICK:
                SetMaterial(matStick);
                break;
            case ressource.BRICK:
                SetMaterial(matBrick);
                break;
            case ressource.STONE:
                SetMaterial(matStone);
                break;
            case ressource.LEAF:
                SetMaterial(matLeaf);
                break;
            case ressource.FRUIT:
                SetMaterial(matFruit);
                break;
        }

        this.transform.SetPositionAndRotation(
            new Vector3(x, y, -0.2f),
            Quaternion.identity
        );
    }

    void SetMaterial(Material mat) {
        this.GetComponent<Renderer>().material = mat;
    }

    public void Collect() {
        GameObject.Find("Tile_" + this.posX + "_" + this.posY).GetComponent<TileBehavior>().Harvest();
        BoardBehavior boardBehavior = GameObject.FindObjectOfType<BoardBehavior>();
        boardBehavior.IncrementRessource(this.type);
        Destroy(gameObject);
    }

    void OnMouseOver() {
        // highlight effect
        GetComponent<Renderer>().material.SetFloat("_Metallic", 0.9f);
        this.Collect();
    }

    void OnMouseExit() {
        // stop highlight effect
        GetComponent<Renderer>().material.SetFloat("_Metallic", 0.0f);
    }
}
