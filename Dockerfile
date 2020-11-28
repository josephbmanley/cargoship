FROM alpine:3

RUN apk add scons pkgconf gcc g++ libx11-dev libxcursor-dev libxinerama-dev libxi-dev libxrandr-dev \
    libexecinfo-dev ca-certificates wget

RUN wget -q https://curl.haxx.se/ca/cacert.pem; cert-sync --user cacert.pem

RUN wget -q https://dot.net/v1/dotnet-install.sh; chmod +x dotnet-install.sh; sh dotnet-install.sh