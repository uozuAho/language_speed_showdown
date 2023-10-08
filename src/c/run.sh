#!/bin/bash
ROOT=$(realpath ../..)
DATA=$ROOT/data
docker run --rm -v $DATA:/app/data langshowdown-c data/tsp/tsp_85900_1
