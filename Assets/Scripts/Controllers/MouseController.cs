using UnityEngine;
using UnityEngine.EventSystems;

public class MouseController : MonoBehaviour
{
    private Ray2D ray;
    private RaycastHit2D hit;
    private InfoPanelManager infoPanelManager;
    private HandOfGod handOfGod;

    private void Start()
    {
        infoPanelManager = FindObjectOfType<InfoPanelManager>();
        handOfGod = new HandOfGod();
    }

    public void Update()
    {
        //If we click left mouse button and we aren't hovering over UI
        if(EventSystem.current.IsPointerOverGameObject() == false)
        {
            hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

            if (Input.GetMouseButtonDown(0))
            {
                //Check if we clicked on a gameObject that has an infoPanel
                if(hit.transform?.GetComponent<IHasInfoPanel>() != null)
                {
                    var entity = hit.transform.GetComponent<IHasInfoPanel>();
                    infoPanelManager.SelectMinion(entity);
                }
                //Check if we clicked a collectable
                else if(hit.transform?.GetComponent<ICollectable>() != null)
                {
                    var collectables = Physics2D.BoxCastAll(hit.transform.position, new Vector2(0.5f, 0.5f), 0, Vector2.zero);
                    foreach (var x in collectables)
                    {
                        if(x.transform.GetComponent<ICollectable>() != null)
                            x.transform.GetComponent<ICollectable>().Collect();
                    }
                }
                //We didnt hit a minion
                else
                {
                    infoPanelManager.DeselectMinion();
                }
            }

            if (Input.GetMouseButtonDown(1))
            {
                //Check if we clicked on a Minion
                if (hit.transform?.GetComponent<Minion>() != null)
                {
                    var entity = hit.transform.GetComponent<Minion>();
                    var nearbyEntities = Physics2D.BoxCastAll(entity.transform.position, new Vector2(2.5f, 2.5f), 0, Vector2.zero);
                    handOfGod.Sacrifice(entity);
                    foreach (var item in nearbyEntities)
                    {
                        if (item.transform.GetComponent<Minion>() != null)
                        {
                            item.transform.GetComponent<Minion>().stats.Sanity -= 50;
                        }
                    }
                }
            }
        }
    }
}

