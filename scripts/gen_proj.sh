#!/usr/bin/env bash

unity-editor -batchmode -nographics -logFile - -executeMethod UnityEditor.SyncVS.SyncSolution -projectPath .
