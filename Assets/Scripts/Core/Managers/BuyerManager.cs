using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuyerManager : Singleton<BuyerManager>
{
    [Header("Buyer Settings")]
    public Transform[] buyingPoints;
    public BuyerController[] buyers;
    public Transform[] startPoints;

    [Header("Spawn Settings")]
    public float minSpawnInterval = 5f;
    public float maxSpawnInterval = 10f;

    // Object pooling lists
    private Queue<BuyerController> availableBuyers = new Queue<BuyerController>();
    private List<BuyerController> activeBuyers = new List<BuyerController>();
    private HashSet<Transform> occupiedBuyingPoints = new HashSet<Transform>();

    private void Start()
    {
        InitializeBuyerPool();
        StartCoroutine(SpawnBuyersRoutine());
    }

    /// <summary>
    /// Initialize the buyer pool by adding all buyers to the available queue
    /// </summary>
    private void InitializeBuyerPool()
    {
        foreach (var buyer in buyers)
        {
            if (buyer != null)
            {
                buyer.gameObject.SetActive(false);
                availableBuyers.Enqueue(buyer);
            }
        }
    }

    /// <summary>
    /// Coroutine that spawns buyers every 5-10 seconds
    /// </summary>
    private IEnumerator SpawnBuyersRoutine()
    {
        while (true)
        {
            float waitTime = Random.Range(minSpawnInterval, maxSpawnInterval);
            yield return new WaitForSeconds(waitTime);

            SpawnBuyer();
        }
    }

    /// <summary>
    /// Spawns a buyer from the pool and assigns them to an available buying point
    /// </summary>
    public void SpawnBuyer()
    {
        // Check if we have available buyers and buying points
        if (availableBuyers.Count == 0)
        {
            Debug.LogWarning("No available buyers in the pool!");
            return;
        }
        var spawnpoint = startPoints[Random.Range(0, startPoints.Length)].position;
        Transform availablePoint = GetAvailableBuyingPoint();
        if (availablePoint == null)
        {
            Debug.LogWarning("No available buying points!");
            return;
        }

        // Get buyer from pool
        BuyerController buyer = availableBuyers.Dequeue();
        buyer.buyingPoint = availablePoint;

        // Assign buying point and mark as occupied
        occupiedBuyingPoints.Add(availablePoint);
        activeBuyers.Add(buyer);

        // Position and activate buyer
        spawnpoint.y = buyer.transform.position.y;
        buyer.transform.position = spawnpoint;
        buyer.targetPoint = availablePoint;
        buyer.gameObject.SetActive(true);
        buyer.stateMachine.TransitionTo(buyer.idleState);

        // If BuyerController has a method to set target, call it here
        // buyer.SetTarget(availablePoint);

        // Debug.Log($"Spawned buyer at buying point: {availablePoint.name}");
    }

    /// <summary>
    /// Returns a buyer to the pool (object pooling)
    /// </summary>
    /// <param name="buyer">The buyer to return to the pool</param>
    public void ReturnBuyerToPool(BuyerController buyer)
    {
        if (buyer == null) return;

        // Remove from active list
        if (activeBuyers.Contains(buyer))
        {
            activeBuyers.Remove(buyer);
        }

        // Find and free the occupied buying point
        Transform buyerPoint = buyer.buyingPoint;
        if (buyerPoint != null && occupiedBuyingPoints.Contains(buyerPoint))
        {
            occupiedBuyingPoints.Remove(buyerPoint);
        }

        // Reset buyer state and return to pool
        buyer.gameObject.SetActive(false);
        availableBuyers.Enqueue(buyer);

        Debug.Log("Buyer returned to pool");
    }

    /// <summary>
    /// Gets an available (unoccupied) buying point
    /// </summary>
    /// <returns>Available Transform or null if none available</returns>
    private Transform GetAvailableBuyingPoint()
    {
        foreach (var point in buyingPoints)
        {
            if (point != null && !occupiedBuyingPoints.Contains(point))
            {
                return point;
            }
        }
        return null;
    }

    /// <summary>
    /// Public method to manually return a buyer to pool (can be called from BuyerController)
    /// </summary>
    /// <param name="buyer">The buyer to return</param>
    public void ReleaseBuyer(BuyerController buyer)
    {
        ReturnBuyerToPool(buyer);
    }

    /// <summary>
    /// Get count of available buyers in pool
    /// </summary>
    public int GetAvailableBuyersCount()
    {
        return availableBuyers.Count;
    }

    /// <summary>
    /// Get count of active buyers
    /// </summary>
    public int GetActiveBuyersCount()
    {
        return activeBuyers.Count;
    }

    /// <summary>
    /// Force return all active buyers to pool (useful for cleanup or reset)
    /// </summary>
    public void ReturnAllBuyersToPool()
    {
        // Create a copy of the list to avoid modification during iteration
        var buyersToReturn = new List<BuyerController>(activeBuyers);

        foreach (var buyer in buyersToReturn)
        {
            ReturnBuyerToPool(buyer);
        }
    }
}
