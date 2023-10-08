FROM gcc:latest
WORKDIR /app
COPY . ./
RUN gcc twoopt.c -o twoopt -O3 -s -static -flto -march=native -mtune=native \
    -fomit-frame-pointer -fno-signed-zeros -fno-trapping-math -fassociative-math
ENTRYPOINT ["/app/twoopt"]
