#!/bin/bash
set -e

docker build -t langshowdown-c-vg -f DockerfileValgrind .

ROOT=$(realpath ../..)
DATA=$ROOT/data

docker run --rm -v $DATA:/app/data langshowdown-c-vg valgrind \
    --tool=callgrind --callgrind-out-file=/app/data/callgrind.out \
    ./twoopt /app/data/tsp/tsp_85900_1

docker run --rm -v $DATA:/app/data langshowdown-c-vg callgrind_annotate \
    --auto=yes /app/data/callgrind.out > callgrind.annotated.txt
