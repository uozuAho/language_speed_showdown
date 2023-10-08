FROM rust:1.64-alpine
WORKDIR /app
COPY . ./
RUN cargo build --profile gofast
ENTRYPOINT ["/app/target/gofast/language_showdown_rust"]
