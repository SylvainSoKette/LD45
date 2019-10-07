using UnityEngine;
using UnityEngine.UI;


public class KitchenBuilding {
    private int level = 0;

    public static int stoneCost = 4;
    public static int brickCost = 10;

    private GameObject board;
    private BoardBehavior boardBehavior;

    public KitchenBuilding()
    {
        board = GameObject.Find("Board");
        boardBehavior = board.GetComponent<BoardBehavior>();
    }

    public bool MeetRequirements()
    {
        if (boardBehavior.GetRessource(ressource.STONE) < stoneCost) return false;
        if (boardBehavior.GetRessource(ressource.BRICK) < brickCost) return false;
        return true;
    }

    public void Upgrade()
    {
        boardBehavior.UseRessource(ressource.STONE, stoneCost);
        boardBehavior.UseRessource(ressource.BRICK, brickCost);
        level += 1;
    }

    public int GetLevel()
    {
        return this.level;
    }
}


public class KitchenButton : MonoBehaviour
{
    public GameObject tooltip;
    private Text tooltipText;

    void Start() {
        tooltip.SetActive(false);
        tooltipText = tooltip.GetComponentInChildren<Text>();
    }

    string GetDescription() {
        return "A nice kitchen\nMakes food from\nfruits every day\n\nCost:\n- "+KitchenBuilding.stoneCost+" stone\n- "+KitchenBuilding.brickCost+" brick";
    }

    public void OnMouseOver() {
        tooltip.SetActive(true);
        tooltipText.text = GetDescription();
    }

    public void OnMouseExit() {
        tooltip.SetActive(false);
    }
}
