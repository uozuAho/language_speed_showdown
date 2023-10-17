#!/bin/bash
# meh, output is too coarse to be useful, use rider?
ROOT=$(realpath ../..)
DATA=$ROOT/data

docker build -t langshowdown-cs -f Dockerfile .

docker run --rm -it -v $DATA:/app/data langshowdown-cs ./dotnet-trace collect \
    -o data/trace.nettrace \
    -- ./out/cs data/tsp/tsp_85900_1

docker run --rm -v $DATA:/app/data langshowdown-cs ./dotnet-trace report \
    data/trace.nettrace topN -n 10 > trace.top10.txt
