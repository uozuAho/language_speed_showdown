#!/bin/bash
ROOT=$(realpath ../..)
DATA=$ROOT/data
docker run --rm -v $DATA:/App/data langshowdown-cs data/tsp/tsp_85900_1
