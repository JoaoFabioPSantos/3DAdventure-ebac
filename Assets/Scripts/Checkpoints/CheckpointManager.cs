using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Studio.Core.Singleton;

public class CheckpointManager : Singleton<CheckpointManager>
{
    public int lastCheckpointKey = 0;
    public List<CheckpointBase> checkpoints;

    public void LoadCheckpoint(int loadCheckpoint)
    {
        lastCheckpointKey = loadCheckpoint;
    }

    public bool HasCheckpoint()
    {
        return lastCheckpointKey > 0;
    }

    public int GetLastCheckPoint()
    {
        return lastCheckpointKey;
    }

    public void SaveCheckpoint(int checkpointKey)
    {
        if (checkpointKey>lastCheckpointKey)
        {
            lastCheckpointKey = checkpointKey;
        }
    }

    public Vector3 GetPositionRespawn()
    {
        var checkpoint = checkpoints.Find(i => i.key == lastCheckpointKey);
        return checkpoint.transform.position;
    }
}
