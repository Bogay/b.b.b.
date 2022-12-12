#!/usr/bin/env bash

unity-editor -username $UNITY_EMAIL -password $UNITY_PASSWORD -batchmode -nographics -logFile - -executeMethod UnityEditor.SyncVS.SyncSolution -projectPath .
