FROM barichello/godot-ci:mono-3.2.3 as builder

COPY . /build
WORKDIR /build

RUN nuget restore cargoship.sln

RUN godot --export server /build/cargoship

FROM barichello/godot-ci:mono-3.2.3 as final

# Install dependencies
RUN apt-get --assume-yes update
RUN apt-get --assume-yes install libx11-dev libxcursor-dev libxinerama-dev libgl1-mesa-dev libglu-dev \
        libasound2-dev libpulse-dev libudev-dev libxi-dev libxrandr-dev yasm

# Create Runtime User
RUN useradd -d /cargoship cargoship

ENV DEDICATED_SERVER="true"

# Add pck file
COPY --from=builder /build/ /cargoship/

CMD /cargoship/cargoship --no-window
