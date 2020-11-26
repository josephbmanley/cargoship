FROM barichello/godot-ci:mono-3.2.3 as builder

COPY . /build
WORKDIR /build

RUN wget https://packages.microsoft.com/config/ubuntu/20.10/packages-microsoft-prod.deb -O packages-microsoft-prod.deb
RUN dpkg -i packages-microsoft-prod.deb

RUN apt-get update
RUN apt-get install -y apt-transport-https && \
        apt-get update && \
        apt-get install -y dotnet-sdk-5.0

RUN dotnet restore

RUN godot --export server /build/cargoship

FROM barichello/godot-ci:mono-3.2.3 as final

# Create Runtime User
RUN useradd -d /cargoship cargoship

# Add pck file
COPY --from=builder /build/cargoship.pck /cargoship/cargoship.pck

CMD /usr/local/bin/godot --main-pack /cargoship/cargoship.pck --empty-server-timeout=300
