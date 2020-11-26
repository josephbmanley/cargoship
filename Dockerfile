FROM barichello/godot-ci:mono-3.2.3 as builder

COPY . /build
WORKDIR /build

RUN nuget restore cargoship.sln

RUN godot --export server /build/cargoship

FROM barichello/godot-ci:mono-3.2.3 as final

# Install dependencies
RUN apt-get install -y libxcursor-dev

# Create Runtime User
RUN useradd -d /cargoship cargoship

ENV DEDICATED_SERVER="true"

# Add pck file
COPY --from=builder /build/ /cargoship/

CMD /cargoship/cargoship
