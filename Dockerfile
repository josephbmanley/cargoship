FROM barichello/godot-ci:mono-3.2.3 as builder

COPY . /build
WORKDIR /build

RUN nuget restore cargoship.sln

RUN godot --export server /build/cargoship

FROM barichello/godot-ci:mono-3.2.3 as final

# Create Runtime User
RUN useradd -d /cargoship cargoship

# Add pck file
COPY --from=builder /build/ /cargoship/

CMD /usr/local/bin/godot --main-pack /cargoship/cargoship.pck --empty-server-timeout=300
