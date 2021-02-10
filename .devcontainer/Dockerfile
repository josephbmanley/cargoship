FROM barichello/godot-ci:mono-3.2.3

RUN apt update -y && apt install -y sudo make bsdmainutils

RUN adduser vscode && usermod -aG sudo vscode

RUN echo "vscode ALL=(ALL) NOPASSWD: ALL" > /etc/sudoers.d/vscode

RUN mkdir -p /home/vscode/.local/share/godot/templates/3.2.3.stable.mono \
&& mv /root/.local/share/godot/templates/3.2.3.stable.mono/* /home/vscode/.local/share/godot/templates/3.2.3.stable.mono

USER vscode