#!/bin/sh
set -e

# gcc twoopt.c -o twoopt -O3 -s -static -flto -march=native -mtune=native \
#     -fomit-frame-pointer -fno-signed-zeros -fno-trapping-math -fassociative-math

# gcc twoopt.c -o twoopt -g
gcc twoopt.c -o twoopt -g -O3 -static -flto -march=native -mtune=native \
    -fomit-frame-pointer -fno-signed-zeros -fno-trapping-math -fassociative-math

# ./twoopt ../../data/tsp/tsp_1000_1
# ./twoopt ../../data/tsp/tsp_85900_1

# callgrind
valgrind --tool=callgrind --callgrind-out-file=callgrind.out ./twoopt ../../data/tsp/tsp_85900_1
callgrind_annotate --auto=yes callgrind.out > callgrind.local.annotated.txt
