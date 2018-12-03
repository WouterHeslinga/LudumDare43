using UnityEngine;

public class EatingState : IMinionState
{
    public Minion Owner { get; private set; }
    public MinionStatus Status => MinionStatus.Eating;

    private Vector2 Destination;
    private float eatingDuration = 3;
    private Resource food;

    public void Enter(Minion owner)
    {
        this.Owner = owner;
        //Check if there is any food we can eat
        if(ResourceManager.Instance.EnoughResources(ResourceType.Food, 1))
        {
            var wareHouse = BuildingManager.Instance.GetWareHouse();
            food = wareHouse.GetResource(ResourceType.Food);
            Destination = food.transform.position;
        }
        //If no food put this state on a cooldown
        else
        {
            Owner.stats.EatingCooldown = 5;
            Owner.CheckForNewJob();
        }
    }

    public void Exit()
    {
        
    }

    public void Update()
    {
        if (Vector2.Distance(Owner.transform.position, Destination) > .5f)
            MoveTowardsDestination();
        else
            EatFood();
    }

    private void EatFood()
    {
        if (food == null)
            Owner.CheckForNewJob();

        eatingDuration -= Time.deltaTime;
        if(eatingDuration <= 0)
        {
            Owner.stats.Hunger = 100;
            Owner.stats.Sanity += 20;
            food.Destroy();
            Owner.CheckForNewJob();
        }
    }

    private void MoveTowardsDestination()
    {
        Owner.Animator.SetBool("IsMoving", true);
        LookAtDestination();
        Owner.transform.position = Vector2.MoveTowards(Owner.transform.position, Destination, Owner.stats.CurrentSpeed * Time.deltaTime);
    }

    private void LookAtDestination()
    {
        Vector2 direction = Destination - (Vector2)Owner.transform.position;
        direction.Normalize();

        float zRot = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Owner.transform.rotation = Quaternion.Euler(0f, 0f, zRot - 90);
    }
}