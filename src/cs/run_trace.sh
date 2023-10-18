#!/bin/bash
ROOT=$(realpath ../..)
DATA=$ROOT/data

docker build -t langshowdown-cs-trace -f DockerfileTrace .

# ---------------------------------
# Get top 10 methods by CPU time. Not fine-grained enough to find bottlenecks.
# docker run --rm -it -v $DATA:/app/data langshowdown-cs-trace ./dotnet-trace collect \
#     -o data/trace.nettrace \
#     -- ./out/cs data/tsp/tsp_85900_1

# docker run --rm -v $DATA:/app/data langshowdown-cs-trace ./dotnet-trace report \
#     data/trace.nettrace topN -n 10 > trace.top10.txt
# ---------------------------------

# ---------------------------------
# See system calls
docker run --rm -v $DATA:/app/data langshowdown-cs-trace strace -c \
    ./out/cs data/tsp/tsp_85900_1
