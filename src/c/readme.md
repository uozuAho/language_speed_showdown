# C twoopt impl

# perf
```sh
./buildPerf.sh
./runPerf.sh
# todo: make this work in docker:
valgrind --tool=callgrind ./twoopt ../../data/tsp/tsp_1000_1
callgrind_annotate callgrind.out.12581 > annotated.txt
```

# todo
- see above todo
- get rid of gperf
- compare valgrind output to C# perf. what's slow?
- profile in release mode?
- maybe: get VS code debugging working
