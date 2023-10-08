FROM rust:1.64-alpine
WORKDIR /app
COPY . ./
RUN cargo rustc -- -C debuginfo=0 -C opt-level=3 -C target-cpu=native \
    -C lto=fat -C codegen-units=1 -C panic=abort
ENTRYPOINT ["twoopt"]
