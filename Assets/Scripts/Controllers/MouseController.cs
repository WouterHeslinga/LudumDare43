using UnityEngine;
using UnityEngine.EventSystems;

public class MouseController : MonoBehaviour
{
    private Ray2D ray;
    private RaycastHit2D hit;
    private InfoPanelManager infoPanelManager;

    //TODO: Remove after testing
    private JobQueue jobQueue;

    private void Start()
    {
        infoPanelManager = FindObjectOfType<InfoPanelManager>();

        jobQueue = FindObjectOfType<JobQueue>();
    }

    public void Update()
    {
        //If we click left mouse button and we aren't hovering over UI
        if(Input.GetMouseButtonDown(0) && EventSystem.current.IsPointerOverGameObject() == false)
        {
            hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

            //Check if we clicked on a gameObject that has an infoPanel
            if(hit.transform?.GetComponent<IHasInfoPanel>() != null)
            {
                var entity = hit.transform.GetComponent<IHasInfoPanel>();
                infoPanelManager.SelectMinion(entity);
            }
            //Check if we clicked a collectable
            else if(hit.transform?.GetComponent<ICollectable>() != null)
            {
                var collectable = hit.transform.GetComponent<ICollectable>();
                collectable.Collect();
            }
            //We didnt hit a minion
            else
            {
                infoPanelManager.DeselectMinion();

                jobQueue.Enqueue(new Job(Camera.main.ScreenToWorldPoint(Input.mousePosition), 1));
            }
        }
    }
}

