FROM alpine:3

ENV GODOT_VERSION="3.2.2"

RUN apk add scons pkgconf gcc g++ libx11-dev libxcursor-dev libxinerama-dev libxi-dev libxrandr-dev \
    libexecinfo-dev ca-certificates wget unzip

RUN wget -q https://dot.net/v1/dotnet-install.sh; chmod +x dotnet-install.sh; sh dotnet-install.sh

# Install Godot
RUN wget -q https://downloads.tuxfamily.org/godotengine/${GODOT_VERSION}/mono/Godot_v${GODOT_VERSION}-stable_mono_linux_headless_64.zip
RUN unzip Godot_v${GODOT_VERSION}-stable_mono_linux_headless_64.zip; mv Godot_v${GODOT_VERSION}-stable_mono_linux_server.64 /usr/local/bin/godot \
        chmod +x /usr/local/bin/godot; mv data_Godot_v${GODOT_VERSION}-stable_mono_linux_server_64 /usr/local/bin

RUN godot --help