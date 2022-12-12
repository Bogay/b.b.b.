#!/usr/bin/env bash

${UNITY_EXECUTABLE:-xvfb-run --auto-servernum --server-args='-screen 0 640x480x24' unity-editor} \
    -username $UNITY_EMAIL \
    -password $UNITY_PASSWORD \
    -batchmode \
    -nographics \
    -logFile - \
    -executeMethod UnityEditor.SyncVS.SyncSolution \
    -projectPath .
