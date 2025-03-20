using System.Collections.Generic;
using System.IO;
using BepInEx;
using BepInEx.Logging;
using HarmonyLib;
using UnityEngine;
using RhythmRift.Enemies;
using RhythmRift;
using Unity.Mathematics;
using System;
using Shared.RhythmEngine;

namespace SpawnFix;

[BepInPlugin("main.rotn.plugins.spawn_fix", "Spawn Fix", "1.0.0.0")]
public class SpawnFixPlugin : BaseUnityPlugin
{
    internal static new ManualLogSource Logger;

    private void Awake()
    {
        // Plugin startup logic
        Logger = base.Logger;

        Harmony.CreateAndPatchAll(typeof(SpawnFixPlugin));

        Logger.LogInfo($"Plugin {MyPluginInfo.PLUGIN_GUID} is loaded!");

        Shared.BugSplatAccessor.Instance.BugSplat.ShouldPostException = ex => false;
    }

    [HarmonyPatch( typeof(RREnemyController), "SpawnEnemy", new[]{ typeof(SpawnEnemyData), typeof(Guid), typeof(FmodTimeCapsule), typeof(int2) }  )]
    [HarmonyPrefix]
    public static bool FixSpawnPositions( ref SpawnEnemyData __0, ref int2 __3 ){
        if( __0.ShouldSpawnWithPoofStatusEffect ) __3.y = 1;
        return true;
    }

}
