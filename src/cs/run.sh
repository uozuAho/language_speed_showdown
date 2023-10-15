#!/bin/bash
ROOT=$(realpath ../..)
DATA=$ROOT/data

docker build -t langshowdown-cs -f Dockerfile .
docker run --rm -v $DATA:/app/data langshowdown-cs data/tsp/tsp_85900_1
