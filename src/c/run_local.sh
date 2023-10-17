#!/bin/sh
set -e

# compile for speed
gcc twoopt.c -o twoopt -O3 -s -static -flto -march=native -mtune=native \
    -fomit-frame-pointer -fno-signed-zeros -fno-trapping-math -fassociative-math

# run
# ./twoopt ../../data/tsp/tsp_1000_1
./twoopt ../../data/tsp/tsp_85900_1

# profile
# gcc twoopt.c -o twoopt -g -O3 -static -flto -march=native -mtune=native \
#     -fomit-frame-pointer -fno-signed-zeros -fno-trapping-math -fassociative-math
# valgrind --tool=callgrind --callgrind-out-file=callgrind.out ./twoopt ../../data/tsp/tsp_85900_1
# callgrind_annotate --auto=yes callgrind.out > callgrind.local.annotated.txt

# other things to play with....

# see assembly
# gcc twoopt.c -o twoopt -O3 -g -flto -march=native -mtune=native \
#     -fomit-frame-pointer -fno-signed-zeros -fno-trapping-math -fassociative-math
# objdump -drwlC -Mintel twoopt > twoopt.asm
