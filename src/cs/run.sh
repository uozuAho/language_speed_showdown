#!/bin/bash
DATA=../../data/tsp_85900_1
docker run --rm -v $DATA:/data langshowdown-cs /data
