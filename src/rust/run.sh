#!/bin/bash
ROOT=$(realpath ../..)
DATA=$ROOT/data
docker run --rm -v $DATA:/app/data langshowdown-rust data/tsp/tsp_85900_1
