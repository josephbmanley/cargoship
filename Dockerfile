FROM ubuntu:focal AS builder

ENV GODOT_VERSION="3.2.2"

RUN mkdir /var/config
WORKDIR /var/config

RUN apt-get install libx11-dev libxcursor-dev libxinerama-dev libgl1-mesa-dev libglu-dev \
        libasound2-dev libpulse-dev libudev-dev libxi-dev libxrandr-dev yasm

RUN wget -q https://dot.net/v1/dotnet-install.sh
RUN chmod +x dotnet-install.sh; ./dotnet-install.sh

# Install Godot
RUN wget -q https://downloads.tuxfamily.org/godotengine/${GODOT_VERSION}/mono/Godot_v${GODOT_VERSION}-stable_mono_linux_headless_64.zip
RUN unzip Godot_v${GODOT_VERSION}-stable_mono_linux_headless_64.zip
RUN mv Godot_v${GODOT_VERSION}-stable_mono_linux_headless_64/Godot_v${GODOT_VERSION}-stable_mono_linux_headless.64 /usr/local/bin/godot
RUN chmod +x /usr/local/bin/godot
RUN mv Godot_v${GODOT_VERSION}-stable_mono_linux_headless_64/GodotSharp /usr/local/bin/GodotSharp

RUN /usr/local/bin/godot --help

#FROM alpine:3 AS final

#RUN apk add scons pkgconf gcc g++ libx11-dev libxcursor-dev libxinerama-dev libxi-dev libxrandr-dev \
#    libexecinfo-dev ca-certificates wget unzip bash

#RUN wget -q https://dot.net/v1/dotnet-install.sh
#RUN chmod +x dotnet-install.sh; ./dotnet-install.sh