using UnityEngine;
using UnityEngine.UI;


public class StorageHutBuilding {
    private int level = 0;

    public static int dirtCost = 5;
    public static int stickCost = 5;
    public static int leafCost = 5;

    private GameObject board;
    private BoardBehavior boardBehavior;

    public StorageHutBuilding()
    {
        board = GameObject.Find("Board");
        boardBehavior = board.GetComponent<BoardBehavior>();
    }

    public bool MeetRequirements()
    {
        if (boardBehavior.GetRessource(ressource.DIRT) < dirtCost) return false;
        if (boardBehavior.GetRessource(ressource.STICK) < stickCost) return false;
        if (boardBehavior.GetRessource(ressource.LEAF) < leafCost) return false;
        return true;
    }

    public void Upgrade()
    {
        boardBehavior.UseRessource(ressource.DIRT, dirtCost);
        boardBehavior.UseRessource(ressource.STICK, stickCost);
        boardBehavior.UseRessource(ressource.LEAF, leafCost);
        level += 1;
    }

    public int GetLevel()
    {
        return this.level;
    }
}


public class StorageButton : MonoBehaviour
{
    public GameObject tooltip;
    private Text tooltipText;

    void Start() {
        tooltip.SetActive(false);
        tooltipText = tooltip.GetComponentInChildren<Text>();
    }

    string GetDescription() {
        return "A storage hut\n+5 max ressources\n\nCost:\n- "+StorageHutBuilding.dirtCost+" dirt\n- "+StorageHutBuilding.stickCost+" stick\n- "+StorageHutBuilding.leafCost+" leaf";
    }

    public void OnMouseOver() {
        tooltip.SetActive(true);
        tooltipText.text = GetDescription();
    }

    public void OnMouseExit() {
        tooltip.SetActive(false);
    }
}
