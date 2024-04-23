#!/bin/bash

PROJECT_ROOT=..
GAME_CLIENT_ROOT=$PROJECT_ROOT
CONFIG_ROOT=$PROJECT_ROOT/Config
LUBAN_DLL=$CONFIG_ROOT/Luban/src/Luban/bin/Debug/net8.0/Luban.dll
CONF_ROOT=.

echo "=================== gen client ==================="
dotnet $LUBAN_DLL \
    --conf $CONF_ROOT/luban.conf \
    --customTemplateDir $CONFIG_ROOT/Luban/CustomTemplate \
    -t client \
    -d bin \
    -d json \
    -c cs-bin \
    -x bin.outputDataDir=$GAME_CLIENT_ROOT/Assets/GameRes/Config/Bytes \
    -x json.outputDataDir=$GAME_CLIENT_ROOT/Assets/GameRes/Config/Json \
    -x outputCodeDir=$GAME_CLIENT_ROOT/Assets/GameScript/HotUpdate/Config/Generate

read -p "Press any key to continue..." key
