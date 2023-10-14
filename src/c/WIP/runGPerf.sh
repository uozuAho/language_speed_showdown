#!/bin/bash
ROOT=$(realpath ../..)
DATA=$ROOT/data
docker run --rm -v $DATA:/app/data -e CPUPROFILE=/app/data/c.prof \
    -e LD_PRELOAD=/usr/local/lib/libprofiler.so \  # doesn't output anything without this. sus
    langshowdown-c-perf ./twoopt tsp_1000_1
