FROM ubuntu:focal AS builder

ENV GODOT_VERSION="3.2.2"
ENV DEBIAN_FRONTEND=noninteractive

RUN apt-get -y update; apt-get -y install libx11-dev libxcursor-dev libxinerama-dev libgl1-mesa-dev libglu-dev \
        libasound2-dev libpulse-dev libudev-dev libxi-dev libxrandr-dev yasm wget unzip

RUN wget -q https://dot.net/v1/dotnet-install.sh
RUN chmod +x dotnet-install.sh; ./dotnet-install.sh

# Install Godot
RUN wget -q https://downloads.tuxfamily.org/godotengine/${GODOT_VERSION}/mono/Godot_v${GODOT_VERSION}-stable_mono_linux_headless_64.zip
RUN unzip Godot_v${GODOT_VERSION}-stable_mono_linux_headless_64.zip
RUN mv Godot_v${GODOT_VERSION}-stable_mono_linux_headless_64/Godot_v${GODOT_VERSION}-stable_mono_linux_headless.64 /usr/local/bin/godot
RUN chmod +x /usr/local/bin/godot
RUN mv Godot_v${GODOT_VERSION}-stable_mono_linux_headless_64/GodotSharp /usr/local/bin/GodotSharp

RUN wget -q https://downloads.tuxfamily.org/godotengine/${GODOT_VERSION}/mono/Godot_v${GODOT_VERSION}-stable_mono_export_templates.tpz

RUN mkdir ~/.cache \
    && mkdir -p ~/.config/godot \
    && mkdir -p ~/.local/share/godot/templates/${GODOT_VERSION}.stable.mono \
    && unzip Godot_v${GODOT_VERSION}-stable_mono_export_templates.tpz \
    && mv templates/* ~/.local/share/godot/templates/${GODOT_VERSION}.stable.mono

RUN mkdir /var/cargoship
COPY . /var/cargoship
WORKDIR /var/cargoship
RUN mkdir builds

RUN /usr/local/bin/godot --export builds/cargoship.x86_64

FROM ubuntu:focal AS final

ENV GODOT_VERSION="3.2.2"
ENV DEBIAN_FRONTEND=noninteractive

RUN apt-get -y update; apt-get -y install libx11-dev libxcursor-dev libxinerama-dev libgl1-mesa-dev libglu-dev \
        libasound2-dev libpulse-dev libudev-dev libxi-dev libxrandr-dev yasm wget unzip

RUN wget -q https://dot.net/v1/dotnet-install.sh
RUN chmod +x dotnet-install.sh; ./dotnet-install.sh


RUN wget -q https://downloads.tuxfamily.org/godotengine/${GODOT_VERSION}/mono/Godot_v${GODOT_VERSION}-stable_mono_linux_server_64.zip
RUN unzip Godot_v${GODOT_VERSION}-stable_mono_linux_server_64.zip
RUN ls -l